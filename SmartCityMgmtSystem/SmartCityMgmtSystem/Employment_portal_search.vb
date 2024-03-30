Imports MySql.Data.MySqlClient

Public Class Employment_portal_search

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Employment Portal"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()

        ' Create an instance of employment_portal.vb and display it
        Dim employmentPortalForm As New Employment_portal()
        employmentPortalForm.Show()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT job_id as ID, 
                             job_desc as Description, 
                             department as Department,
                             salary as Salary,
                             deadline as Deadline,
                             qualification as Qualification 
                             FROM employment_jobs
                             WHERE (job_id = @jobid OR @jobid IS NULL) 
                             AND (LOWER(job_desc) LIKE LOWER(@jobdesc) OR @jobdesc IS NULL) 
                             AND (LOWER(department) LIKE LOWER(@dept) OR @dept IS NULL) 
                             AND (salary >= @salary OR @salary IS NULL) 
                             AND (LOWER(qualification) LIKE LOWER(@qualification) OR @qualification IS NULL)
                             AND (deadline > @deadline)"


                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@jobid", If(String.IsNullOrEmpty(Job_ID.Text), DBNull.Value, Job_ID.Text))
                    cmd.Parameters.AddWithValue("@jobdesc", "%" & Job_Desc.Text.ToLower() & "%")
                    cmd.Parameters.AddWithValue("@dept", If(String.IsNullOrEmpty(Dept.Text), DBNull.Value, "%" & Dept.Text.ToLower() & "%"))
                    cmd.Parameters.AddWithValue("@salary", If(String.IsNullOrEmpty(Salary.Text), 0, Salary.Text))
                    cmd.Parameters.AddWithValue("@deadline", Date.Now())
                    cmd.Parameters.AddWithValue("@qualification", If(String.IsNullOrEmpty(Qual.Text), DBNull.Value, "%" & Qual.Text.ToLower() & "%"))


                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "ID"
        DataGridView1.Columns(1).DataPropertyName = "Description"
        DataGridView1.Columns(2).DataPropertyName = "Department"
        DataGridView1.Columns(3).DataPropertyName = "Salary"
        DataGridView1.Columns(4).DataPropertyName = "Deadline"
        DataGridView1.Columns(5).DataPropertyName = "Qualification"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
        DataGridView1.Visible = True
    End Sub
End Class
