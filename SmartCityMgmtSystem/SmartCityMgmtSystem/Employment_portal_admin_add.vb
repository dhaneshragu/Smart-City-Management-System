Imports MySql.Data.MySqlClient

Public Class Employment_portal_admin_add

    Public Property uid As Integer = 4

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Employment Portal"
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, TransportationAdminHome)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim cmd As New MySqlCommand
        Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())

            Dim apply_date As DateTime = DateTime.Now

            Dim stmnt As String = "INSERT INTO employment_jobs 
     (job_desc,job_poster_id,department,salary,deadline,qualification) 
     VALUES (@jobdesc, @posterid, @dept,@salary,@deadline,@qual)"

            cmd = New MySqlCommand(stmnt, con)
            cmd.Parameters.AddWithValue("@jobdesc", Job_desc.Text)
            cmd.Parameters.AddWithValue("@posterid", uid)
            cmd.Parameters.AddWithValue("@dept", Job_dept.Text)
            cmd.Parameters.AddWithValue("@salary", Salary.Text)
            cmd.Parameters.AddWithValue("@deadline", Convert.ToDateTime(DateTimePicker1.Value))
            cmd.Parameters.AddWithValue("@qual", Qualification.Text)
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                MessageBox.Show("Job Listed successfully.")
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try

        End Using
    End Sub
End Class
