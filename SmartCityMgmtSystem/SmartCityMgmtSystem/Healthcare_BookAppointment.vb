Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Healthcare_BookAppointment

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
                Dim button As New Windows.Forms.Button()
                button.BackColor = Color.LightBlue
                button.Width = 200
                button.Height = 100
                button.Location = New Point(50 + i, 50)
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

    Private Sub LoadandBindDataGridView2()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT * FROM department WHERE hospital_id = 1", Con)

        reader = cmd.ExecuteReader()
        Dim i As Integer = 0
        'Fill the DataTable with data from the SQL table
        If reader.HasRows Then
            While reader.Read()
                Dim Value As String = reader("department_name").ToString()
                Dim button As New Windows.Forms.Button()
                button.BackColor = Color.LightSkyBlue
                button.Width = 200
                button.Height = 80
                button.Location = New Point(50 + i, 10)
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
    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
        LoadandBindDataGridView2()
    End Sub


End Class