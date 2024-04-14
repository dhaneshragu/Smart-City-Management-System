Imports MySql.Data.MySqlClient

Public Class Employment_portal_apply

    Public uid As Integer
    Public u_name As String

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Employment Portal"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim cmd As New MySqlCommand
        Dim cmd1 As New MySqlCommand
        Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())

            Dim apply_datetime As DateTime = DateTime.Now
            Dim apply_date As Date = Date.Now

            Dim stmnt1 As String = "SELECT * FROM employment_jobs
                                WHERE job_id = @jobid AND deadline < @deadline"

            Dim stmnt As String = "INSERT INTO employment_applications 
                               VALUES (@jobid, @userid, @status_apply,@apply_time)"

            cmd1 = New MySqlCommand(stmnt1, con)
            cmd = New MySqlCommand(stmnt, con)
            cmd.Parameters.AddWithValue("@jobid", job_id.Text)
            cmd1.Parameters.AddWithValue("@jobid", job_id.Text)
            cmd.Parameters.AddWithValue("@userid", uid)
            cmd.Parameters.AddWithValue("@status_apply", "Applied")
            cmd.Parameters.AddWithValue("@apply_time", apply_datetime)
            cmd1.Parameters.AddWithValue("@deadline", apply_datetime)



            Try
                con.Open()
                Dim reader As MySqlDataReader = cmd1.ExecuteReader
                If reader.Read Then
                    Try
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("Applied Successfully successfully.")
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message)
                    End Try
                Else
                    MessageBox.Show("Deadline has passed")
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub
End Class
