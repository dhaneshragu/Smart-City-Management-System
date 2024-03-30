Imports MySql.Data.MySqlClient

Public Class Employment_portal_applicationstatus
    Public Property uid As Integer = 10
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Employment Portal"
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, TransportationAdminHome)
    End Sub

    Private Sub StatusBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles StatusBox.SelectedIndexChanged
        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT A.job_id as Job_ID, 
                     J.job_desc as Description, 
                     A.status_application as Status,
                     A.apply_time as Apply_Time
                     FROM employment_applications as A JOIN employment_jobs as J
                     WHERE (A.job_id = J.job_id) AND (A.applicant_id = @applicant) 
                     AND (LOWER(A.status_application) LIKE LOWER(@status) OR @status IS NULL)"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@applicant", uid)
                    cmd.Parameters.AddWithValue("@status", If(String.IsNullOrEmpty(StatusBox.Text), DBNull.Value, "%" & StatusBox.Text.ToLower() & "%"))

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        dataTable.Load(reader)
                        MessageBox.Show("Read Success")
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "Job_ID"
        DataGridView1.Columns(1).DataPropertyName = "Description"
        DataGridView1.Columns(2).DataPropertyName = "Status"
        DataGridView1.Columns(3).DataPropertyName = "Apply_Time"

        DataGridView1.DataSource = dataTable
        DataGridView1.Visible = True
    End Sub
End Class
