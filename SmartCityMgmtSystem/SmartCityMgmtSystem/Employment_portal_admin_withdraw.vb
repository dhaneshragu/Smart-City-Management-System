Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient

Public Class Employment_portal_admin_withdraw

    Public uid As Integer
    Public u_name As String

    Public Sub pageload()
        DataGridView1.AllowUserToAddRows = False
        Me.Text = "Employment Portal"
        Label2.Text = u_name
        Label3.Text = uid
        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT * FROM employment_jobs
            WHERE job_poster_id = @poster"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@poster", uid)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        dataTable.Load(reader)
                        'MessageBox.Show("Read Success")
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(1).DataPropertyName = "job_id"
        DataGridView1.Columns(2).DataPropertyName = "job_desc"
        DataGridView1.Columns(3).DataPropertyName = "department"
        DataGridView1.Columns(4).DataPropertyName = "salary"
        DataGridView1.Columns(5).DataPropertyName = "qualification"
        DataGridView1.Columns(6).DataPropertyName = "deadline"



        DataGridView1.DataSource = dataTable
        DataGridView1.Visible = True
    End Sub

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        pageload()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()

        ' Create an instance of employment_portal.vb and display it
        Dim employmentPortalAdminForm As New Employment_portal_admin() With {
            .uid = uid,
            .u_name = u_name
        }
        employmentPortalAdminForm.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim jobid As Integer = -1

        For Each row As DataGridViewRow In DataGridView1.Rows
            Dim isSelected As Boolean = Convert.ToBoolean(row.Cells("checkboxcolumn").Value)
            If isSelected Then
                jobid = row.Cells("Column1").Value
            End If
        Next


        If jobid = -1 Then
            MessageBox.Show("Please select a job to remove")
            Return
        End If

        Dim cmd1 As New MySqlCommand
        Dim cmd2 As New MySqlCommand

        Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())

            Dim stmnt1 As String = "DELETE FROM employment_jobs WHERE job_id = @jobid"
            Dim stmnt2 As String = "DELETE FROM employment_applications WHERE job_id = @jobid"

            cmd1 = New MySqlCommand(stmnt1, con)
            cmd2 = New MySqlCommand(stmnt2, con)

            cmd1.Parameters.AddWithValue("@jobid", jobid)
            cmd2.Parameters.AddWithValue("@jobid", jobid)

            Try
                con.Open()
                cmd2.ExecuteNonQuery()
                cmd1.ExecuteNonQuery()
                MessageBox.Show("Deleted successfully.")
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try

        End Using

        pageload()

    End Sub
End Class
