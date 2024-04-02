Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Public Class Healthcare_DonateBlood
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
        LoadandBindDataGridView()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub
End Class