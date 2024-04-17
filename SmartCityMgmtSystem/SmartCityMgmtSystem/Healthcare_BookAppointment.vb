Imports System.Data.SqlClient
Imports Google.Protobuf.WellKnownTypes
Imports MySql.Data.MySqlClient
Public Class Healthcare_BookAppointment
    Public Property uid As Integer = 130
    Public Property hos_id As Integer = -1
    Public Property dep_id As Integer = -1

    Dim prev_hos_Button As Button = Nothing

    Dim prev_dep_Button As Button = Nothing
    Private Sub LoadandBindDataGridView()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT * FROM hospitaldb", Con)
        reader = cmd.ExecuteReader()
        Dim i As Integer = 0
        'Fill the DataTable with data from the SQL table
        If reader.HasRows Then
            While reader.Read()
                Dim Value As String = reader("hospital_name").ToString()
                Dim Value1 As String = reader("location").ToString()
                Dim Value2 As String = reader("hospital_id").ToString()
                Dim button As New Windows.Forms.Button()
                button.BackColor = Color.LightBlue
                button.Width = 200
                button.Height = 100
                button.Location = New Point(50 + i, 50)
                button.Name = Value2
                AddHandler button.Click, AddressOf Hospital_Click
                i += 230
                ' Create a label for the hospital name
                Dim lblHospital As New Windows.Forms.Label()
                lblHospital.Text = Value
                lblHospital.Size = New Size(180, 30)
                lblHospital.Font = New Font("Arial", 16, FontStyle.Bold)
                lblHospital.Location = New Point(10, 10)
                button.Controls.Add(lblHospital)

                ' Create a label for the location
                Dim lblLocation As New Windows.Forms.Label()
                lblLocation.Text = Value1
                lblLocation.Size = New Size(180, 30)
                lblLocation.Font = New Font("Times New Roman", 12, FontStyle.Regular)
                lblLocation.Location = New Point(10, 50)
                button.Controls.Add(lblLocation)

                ' Add the button to the form
                Panel2.Controls.Add(button)
                'button.Text = Value & Environment.NewLine & Value1
                button.Margin = New Padding(20) ' Set the spacing between buttons

            End While
        End If
        Con.Close()
    End Sub

    Private Sub LoadandBindDataGridView2(ByVal hospital_Id As Integer)
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT * FROM department WHERE hospital_id = @Value", Con)
        cmd.Parameters.AddWithValue("@Value", hospital_Id)
        reader = cmd.ExecuteReader()
        Dim i As Integer = 0
        'Fill the DataTable with data from the SQL table
        If reader.HasRows Then
            While reader.Read()
                Dim Value As String = reader("department_name").ToString()
                Dim Value2 As String = reader("department_id").ToString()
                Dim button As New Windows.Forms.Button()
                button.BackColor = Color.LightSkyBlue
                button.Width = 200
                button.Height = 80
                button.Location = New Point(50 + i, 10)
                button.Name = Value2
                AddHandler button.Click, AddressOf Department_Click
                i += 230
                ' Create a label for the hospital name
                Dim lblDepartment As New Windows.Forms.Label()
                lblDepartment.Text = Value
                lblDepartment.Size = New Size(180, 30)
                lblDepartment.Font = New Font("Arial", 16, FontStyle.Bold)
                lblDepartment.Location = New Point(10, 10)
                button.Controls.Add(lblDepartment)

                ' Add the button to the form
                Panel1.Controls.Add(button)
                'button.Text = Value & Environment.NewLine & Value1
                button.Margin = New Padding(20) ' Set the spacing between buttons

            End While
        End If
        Con.Close()
    End Sub
    Private Sub ClearPanel(panel As Panel)
        ' Remove all controls from the panel
        For i As Integer = panel.Controls.Count - 1 To 0 Step -1
            panel.Controls(i).Dispose()
        Next
    End Sub
    Private Sub Hospital_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim hospital_Button As Button = DirectCast(sender, Button)
        If prev_hos_Button IsNot Nothing Then
            prev_hos_Button.BackColor = Color.LightBlue
        End If
        hospital_Button.BackColor = Color.FromArgb(117, 187, 220)
        hos_id = hospital_Button.Name
        Dim hospital_Id As String = hospital_Button.Name
        prev_hos_Button = hospital_Button
        dep_id = -1
        ClearPanel(Panel1)
        ' Update the current question ID and display the question text and optisons
        LoadandBindDataGridView2(hospital_Id)
    End Sub
    Private Sub Department_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim department_Button As Button = DirectCast(sender, Button)
        If prev_dep_Button IsNot Nothing Then
            prev_dep_Button.BackColor = Color.LightSkyBlue
        End If
        department_Button.BackColor = SystemColors.Highlight
        dep_id = department_Button.Name
        prev_dep_Button = department_Button
        Dim department_Id As String = department_Button.Name
    End Sub
    Private Sub healthInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If hos_id = -1 Then
            MessageBox.Show("Select a Hospital")
        Else
            If dep_id = -1 Then
                MessageBox.Show("Select a Department")
            Else
                Dim cmd As New MySqlCommand
                Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())

                    Dim apply_date As DateTime = DateTime.Now

                    Dim stmnt As String = "INSERT INTO appointments VALUES (NULL, @doctor_ID, @hospital_ID,@patient_ID,@time,@symtoms,@status)"

                    cmd = New MySqlCommand(stmnt, con)
                    cmd.Parameters.AddWithValue("@doctor_ID", 100)
                    cmd.Parameters.AddWithValue("@hospital_ID", hos_id)
                    cmd.Parameters.AddWithValue("@patient_ID", uid)
                    cmd.Parameters.AddWithValue("@time", DateTimePicker1.Value)
                    cmd.Parameters.AddWithValue("@symtoms", RichTextBox1.Text)
                    cmd.Parameters.AddWithValue("@status", "pending")

                    Try
                        con.Open()
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("submitted appointment")
                        RichTextBox1.Text = ""
                        hos_id = -1
                        dep_id = -1
                        prev_dep_Button.BackColor = Color.LightBlue
                        prev_hos_Button.BackColor = Color.LightBlue
                        prev_hos_Button = Nothing
                        prev_dep_Button = Nothing
                        ClearPanel(Panel1)
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message)
                    End Try
                    con.Close()
                End Using
            End If
        End If
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class
