Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Healthcare_Emergency
    Public Property uid As Integer = 130
    Public Property hos_id As Integer = 1
    Private Sub search_hospital()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT * FROM hospitaldb WHERE location = @Value", Con)
        cmd.Parameters.AddWithValue("@Value", RichTextBox1.Text)
        reader = cmd.ExecuteReader()
        Dim i As Integer = 0
        'Fill the DataTable with data from the SQL table
        If reader.HasRows Then
            While reader.Read()
                hos_id = reader("hospital_id").ToString()
            End While
        End If
        Con.Close()
    End Sub

    Private Sub d1_MouseEnter(sender As Object, e As EventArgs) Handles d1.MouseEnter
        ' Change the background color of d1 when mouse enters
        d1.BackColor = SystemColors.MenuHighlight ' Change to your desired color
    End Sub

    Private Sub d1_MouseLeave(sender As Object, e As EventArgs) Handles d1.MouseLeave
        ' Change the background color of d1 when mouse leaves
        d1.BackColor = SystemColors.Highlight ' Change to your default color
    End Sub

    Private Sub d1_Click(sender As Object, e As EventArgs) Handles d1.Click

        If String.IsNullOrWhiteSpace(RichTextBox1.Text) Then
            ' Display a message box if RichtextBox1 is empty
            MessageBox.Show("Please type your current address.", "Address Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            search_hospital()
            Dim cmd As New MySqlCommand
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())

                Dim apply_date As DateTime = DateTime.Now

                Dim stmnt As String = "INSERT INTO ambulanceappointment VALUES (NULL,@Location, @hospital_ID, @Ambulance_id, @patient_ID, @time)"

                cmd = New MySqlCommand(stmnt, con)
                cmd.Parameters.AddWithValue("@doctor_ID", 100)
                cmd.Parameters.AddWithValue("@hospital_ID", hos_id)
                cmd.Parameters.AddWithValue("@patient_ID", uid)
                cmd.Parameters.AddWithValue("@time", Now())
                cmd.Parameters.AddWithValue("@Ambulance_id", 100)
                cmd.Parameters.AddWithValue("@Location", RichTextBox1.Text)
                Try
                    con.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Request Sent")
                    RichTextBox1.Text = ""
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
                con.Close()
            End Using
        End If
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub
End Class