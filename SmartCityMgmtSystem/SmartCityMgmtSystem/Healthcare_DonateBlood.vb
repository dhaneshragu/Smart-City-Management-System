Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Google.Protobuf.WellKnownTypes
Imports MySql.Data.MySqlClient
Public Class Healthcare_DonateBlood
    Public Property uid As Integer = 130
    Public Property hos_id As Integer = -1
    Public Property b_grp As String = "O+"

    Dim prev_hos_Button As Windows.Forms.Button = Nothing
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
                lblHospital.Size = New Size(180, 30)
                lblLocation.Font = New Font("Times New Roman", 12, FontStyle.Regular)
                lblLocation.Location = New Point(10, 50)
                button.Controls.Add(lblLocation)

                ' Add the button to the form
                Panel1.Controls.Add(button)
                'button.Text = Value & Environment.NewLine & Value1
                button.Margin = New Padding(20) ' Set the spacing between buttons

            End While
        End If
        Con.Close()
    End Sub
    Private Sub Healthcare_DonateBlood_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("AB+")
        ComboBox1.Items.Add("A+")
        ComboBox1.Items.Add("B+")
        ComboBox1.Items.Add("O+")
        ComboBox1.Items.Add("AB-")
        ComboBox1.Items.Add("A-")
        ComboBox1.Items.Add("B-")
        ComboBox1.Items.Add("O-")
        ComboBox1.SelectedIndex = 0
        DateTimePicker1.ShowUpDown = True
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss"
        LoadandBindDataGridView()
    End Sub

    Private Sub Hospital_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim hospital_Button As Windows.Forms.Button = DirectCast(sender, Windows.Forms.Button)
        If prev_hos_Button IsNot Nothing Then
            prev_hos_Button.BackColor = Color.LightBlue
        End If
        hospital_Button.BackColor = Color.DeepSkyBlue
        hos_id = hospital_Button.Name
        Dim hospital_Id As String = hospital_Button.Name
        prev_hos_Button = hospital_Button
    End Sub

    Private Sub d1_Click(sender As Object, e As EventArgs) Handles d1.Click
        If hos_id = -1 Then
            MessageBox.Show("Select a Hospital")
        Else
            Dim number As Integer
            If Integer.TryParse(RichTextBox3.Text, number) Then
                Dim cmd As New MySqlCommand
                Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())

                    Dim apply_date As DateTime = DateTime.Now

                    Dim stmnt As String = "INSERT INTO blood_donation VALUES (NULL,  @hospital_ID,@patient_ID,@time,@status,@blood_grp)"

                    cmd = New MySqlCommand(stmnt, con)
                    cmd.Parameters.AddWithValue("@hospital_ID", hos_id)
                    cmd.Parameters.AddWithValue("@patient_ID", uid)
                    cmd.Parameters.AddWithValue("@time", DateTimePicker1.Value)
                    cmd.Parameters.AddWithValue("@status", "pending")
                    cmd.Parameters.AddWithValue("@blood_grp", b_grp)

                    Try
                        con.Open()
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("submitted appointment")
                        RichTextBox3.Text = ""
                        hos_id = -1
                        prev_hos_Button.BackColor = Color.LightBlue
                        prev_hos_Button = Nothing
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message)
                    End Try
                    con.Close()
                End Using
            Else
                MessageBox.Show("Age is not a valid integer")
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        b_grp = ComboBox1.SelectedItem.ToString()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub
End Class