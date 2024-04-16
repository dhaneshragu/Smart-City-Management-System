Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Relational
Imports Org.BouncyCastle.Bcpg

Public Class Employment_portal_admin

    Public uid As Integer
    Public u_name As String


    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting

        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Column6").Value = "Rejected" Then
                row.DefaultCellStyle.BackColor = Color.RosyBrown
            End If
            If row.Cells("Column6").Value = "Accepted" Then
                row.DefaultCellStyle.BackColor = Color.LightGreen
            End If
            If row.Cells("Column6").Value = "Applied" Then
                row.DefaultCellStyle.BackColor = Color.White
            End If
        Next
    End Sub

    Public Sub pageload()
        DataGridView1.AllowUserToAddRows = False
        Me.Text = "Employment Portal"
        Label2.Text = u_name
        Label3.Text = uid

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
                        'MessageBox.Show("Read Success")
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


        Try
            Using con1 As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con1.Open()

                Dim sql As String = "SELECT COUNT(*) as Num_Applicants_Applied
                                    FROM employment_applications as A 
                                    JOIN employment_jobs as J ON A.job_id = J.job_id
                                    WHERE J.job_poster_id = @poster
                                    AND A.status_application = 'Applied';"

                Using cmd1 As New MySqlCommand(sql, con1)
                    cmd1.Parameters.AddWithValue("@poster", uid)

                    Dim count As Integer = Convert.ToInt32(cmd1.ExecuteScalar())
                    RichTextBox1.Text = "Hey there, " & count.ToString() & " applications are pending to be reviewed"

                    'MessageBox.Show("Read Success")
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try


        AddHandler DataGridView1.CellFormatting, AddressOf DataGridView1_CellFormatting

    End Sub

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        pageload()
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

        Dim add = New Employment_portal_admin_add() With {
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, add)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()

        ' Create an instance of employment_portal_admin_withdraw.vb and display it
        Dim employmentPortalAdminWithdrawForm As New Employment_portal_admin_withdraw() With {
            .uid = uid,
            .u_name = u_name
        }
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

        If userid = -1 Then
            MessageBox.Show("Please select a candidate first")
            Return
        End If

        Dim usercredentials As New Employment_portal_admin_usercredentials()
        usercredentials.userid = userid
        usercredentials.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim jobid As Integer = -1
        Dim userid As Integer = -1
        For Each row As DataGridViewRow In DataGridView1.Rows
            Dim isSelected As Boolean = Convert.ToBoolean(row.Cells("checkboxcolumn").Value)
            If isSelected Then
                userid = row.Cells("Column4").Value
                jobid = row.Cells("Column2").Value
            End If
        Next

        If userid = -1 Then
            MessageBox.Show("Please select a candidate first")
            Return
        End If

        Dim cmd As New MySqlCommand
        Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())

            Dim stmnt As String = "UPDATE employment_applications
    SET status_application = @status
    WHERE job_id = @jobid AND applicant_id = @applicant"

            cmd = New MySqlCommand(stmnt, con)
            cmd.Parameters.AddWithValue("@jobid", jobid)
            cmd.Parameters.AddWithValue("@applicant", userid)
            cmd.Parameters.AddWithValue("@status", "Accepted")
            Try
                con.Open()
                cmd.ExecuteNonQuery()

                MessageBox.Show("Candidate Accepted")
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using


        Dim Notification As String = "Congratulations! You have been accepted in jobid " & jobid.ToString()

        Globals.SendNotifications(uid, userid, "Job Acceptance", Notification)
        MessageBox.Show("Sent notification")

        pageload()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim jobid As Integer = -1
        Dim userid As Integer = -1
        For Each row As DataGridViewRow In DataGridView1.Rows
            Dim isSelected As Boolean = Convert.ToBoolean(row.Cells("checkboxcolumn").Value)
            If isSelected Then
                userid = row.Cells("Column4").Value
                jobid = row.Cells("Column2").Value
            End If
        Next


        If userid = -1 Then
            MessageBox.Show("Please select a candidate first")
            Return
        End If

        Dim cmd As New MySqlCommand
        Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())

            Dim stmnt As String = "UPDATE employment_applications
    SET status_application = @status
    WHERE job_id = @jobid AND applicant_id = @applicant"

            cmd = New MySqlCommand(stmnt, con)
            cmd.Parameters.AddWithValue("@jobid", jobid)
            cmd.Parameters.AddWithValue("@applicant", userid)
            cmd.Parameters.AddWithValue("@status", "Rejected")
            Try
                con.Open()
                cmd.ExecuteNonQuery()

                MessageBox.Show("Candidate Rejected")
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using


        Dim Notification As String = "Sorry! You have been rejected from jobid " & jobid.ToString()

        Globals.SendNotifications(uid, userid, "Job Rejected", Notification)
        MessageBox.Show("Sent notification")

        pageload()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()

        Dim employment_portal = New Employment_portal() With {
            .uid = uid,
            .u_name = u_name
        }
        employment_portal.Show()
    End Sub
End Class
