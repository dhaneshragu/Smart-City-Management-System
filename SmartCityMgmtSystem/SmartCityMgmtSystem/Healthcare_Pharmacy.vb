Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Healthcare_Pharmacy
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

        cmd = New MySqlCommand("SELECT * FROM pharmacydb", Con)
        reader = cmd.ExecuteReader()
        Dim i As Integer = 0
        'Fill the DataTable with data from the SQL table
        If reader.HasRows Then
            While reader.Read()
                Dim Value As String = reader("pharmacy_name").ToString()
                Dim Value1 As String = reader("location").ToString()
                Dim label As New Windows.Forms.Label()
                label.BackColor = Color.LightSkyBlue
                label.Width = 700
                label.Height = 100
                label.Location = New Point(150, 50 + i)

                ' Create a label for the hospital name
                Dim lblHospital As New Windows.Forms.Label()
                lblHospital.Text = Value
                lblHospital.Size = New Size(400, 80)
                lblHospital.Font = New Font("Arial", 24, FontStyle.Bold)
                lblHospital.Location = New Point(10, 30)
                label.Controls.Add(lblHospital)

                ' Create a label for the location
                Dim lblLocation As New Windows.Forms.Label()
                lblLocation.Text = Value1
                lblLocation.Size = New Size(300, 80)
                lblLocation.Font = New Font("Times New Roman", 16, FontStyle.Regular)
                lblLocation.Location = New Point(410, 40)
                label.Controls.Add(lblLocation)

                Dim lblBuy As New Windows.Forms.Button()

                lblBuy.Text = "BUY"
                lblBuy.BackColor = Color.FromArgb(0, 180, 0)
                lblBuy.Width = 100
                lblBuy.Height = 100
                lblBuy.Location = New Point(860, 50 + i)

                i += 120
                Panel1.Controls.Add(lblBuy)
                ' Add the button to the form
                Panel1.Controls.Add(label)
                'button.Text = Value & Environment.NewLine & Value1

            End While
        End If
        Con.Close()
    End Sub

    Private Sub Healthcare_Pharmacy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class