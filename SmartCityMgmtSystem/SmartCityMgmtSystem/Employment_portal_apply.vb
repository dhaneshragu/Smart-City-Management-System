Imports MySql.Data.MySqlClient

Public Class Employment_portal_apply

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Employment Portal"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim cmd As New MySqlCommand
        Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())

            Dim apply_date As DateTime = DateTime.Now

            Dim stmnt As String = "INSERT INTO employment_applications VALUES (@jobid, @userid, @status_apply,@apply_time)"

            cmd = New MySqlCommand(stmnt, con)
            cmd.Parameters.AddWithValue("@jobid", job_id.Text)
            cmd.Parameters.AddWithValue("@userid", user_id.Text)
            cmd.Parameters.AddWithValue("@status_apply", "Applied")
            cmd.Parameters.AddWithValue("@apply_time", apply_date)

            Try
                con.Open()
                cmd.ExecuteNonQuery()
                MessageBox.Show("Applied Successfully successfully.")
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try

        End Using
    End Sub
End Class
