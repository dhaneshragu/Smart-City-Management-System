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

            Dim stmnt1 As String = "SELECT * FROM employment_jobs WHERE job_id = @jobid AND deadline > @deadline"
            Dim stmnt As String = "INSERT INTO employment_applications VALUES (@jobid, @userid, @status_apply, @apply_time)"

            Dim con2 As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
            cmd1 = New MySqlCommand(stmnt1, con)
            cmd = New MySqlCommand(stmnt, con2)

            cmd.Parameters.AddWithValue("@jobid", job_id.Text)
            cmd1.Parameters.AddWithValue("@jobid", job_id.Text)
            cmd.Parameters.AddWithValue("@userid", uid)
            cmd.Parameters.AddWithValue("@status_apply", "Applied")
            cmd.Parameters.AddWithValue("@apply_time", apply_datetime)
            cmd1.Parameters.AddWithValue("@deadline", apply_datetime)

            Try
                con.Open()
                Dim reader As MySqlDataReader = cmd1.ExecuteReader()
                If reader.Read() Then
                    con.Close()
                    Try
                        con2.Open()
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("Applied Successfully.")
                        con2.Close()
                    Catch ex As MySqlException When ex.Number = 1062 ' MySQL error number for duplicate key
                        MessageBox.Show("Error: You have already applied for this job.")
                        ' Log the exception details

                    Catch ex As MySqlException When ex.Number = 0 ' MySQL error number for server errors
                        MessageBox.Show("Error: Unable to connect to the database server. Please check your internet connection or contact support.")
                        ' Log the exception details

                    Catch ex As MySqlException
                        MessageBox.Show("Error: Database error occurred. Please try again later.")
                        ' Log the exception details

                    End Try
                Else
                    MessageBox.Show("Requested Job Opening Not available.")
                End If
            Catch ex As MySqlException When ex.Number = 0 ' MySQL error number for server errors
                MessageBox.Show("Error: Unable to connect to the database server. Please check your internet connection or contact support.")
                ' Log the exception details

            Catch ex As MySqlException
                MessageBox.Show("Error: Database error occurred. Please try again later.")
                ' Log the exception details

            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred. Please contact support.")
                ' Log the exception details

            End Try
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        job_id.Clear()
    End Sub
End Class
