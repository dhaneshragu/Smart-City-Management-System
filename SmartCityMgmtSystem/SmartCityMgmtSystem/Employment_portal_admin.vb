Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Bcpg

Public Class Employment_portal_admin

    Public Property uid As Integer = 4
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Employment Portal"
        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT A.job_id as Job_ID, 
                 J.job_desc as Description,
                 A.applicant_id as ApplicantID,
                 J.department as Dept,
                 A.apply_time as Apply_Time,
                 A.status_application as Status
                 FROM employment_applications as A JOIN employment_jobs as J
                 WHERE (A.job_id = J.job_id) AND (J.job_poster_id = @poster)"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@poster", uid)

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
        DataGridView1.Columns(1).DataPropertyName = "Job_ID"
        DataGridView1.Columns(2).DataPropertyName = "Description"
        DataGridView1.Columns(3).DataPropertyName = "ApplicantID"
        DataGridView1.Columns(4).DataPropertyName = "Dept"
        DataGridView1.Columns(5).DataPropertyName = "Apply_Time"
        DataGridView1.Columns(6).DataPropertyName = "Status"


        DataGridView1.DataSource = dataTable
        DataGridView1.Visible = True
    End Sub


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            If TypeOf DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex) Is DataGridViewCheckBoxCell Then
                Dim currentCheckbox As DataGridViewCheckBoxCell = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)

                ' Uncheck all other checkboxes in the same column
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Index <> e.RowIndex Then
                        Dim otherCheckbox As DataGridViewCheckBoxCell = row.Cells(e.ColumnIndex)
                        otherCheckbox.Value = False
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, Employment_portal_admin_add)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()

        ' Create an instance of employment_portal_admin_withdraw.vb and display it
        Dim employmentPortalAdminWithdrawForm As New Employment_portal_admin_withdraw()
        employmentPortalAdminWithdrawForm.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim userid As Integer = -1
        For Each row As DataGridViewRow In DataGridView1.Rows
            Dim isSelected As Boolean = Convert.ToBoolean(row.Cells("checkboxcolumn").Value)
            If isSelected Then
                userid = row.Cells("Column4").Value
            End If
        Next
        Dim usercredentials As New Employment_portal_admin_usercredentials()
        usercredentials.userid = userid
        usercredentials.Show()
    End Sub
End Class
