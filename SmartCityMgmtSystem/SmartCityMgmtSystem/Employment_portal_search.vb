Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Employment_portal_search

    Public uid As Integer
    Public u_name As String


    Public Sub btnclick()
        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT j.job_id as ID, 
                     j.job_desc as Description, 
                     j.department as Department,
                     j.salary as Salary,
                     j.deadline as Deadline,
                     j.qualification as Qualification 
                     FROM employment_jobs as j JOIN employment_qualifications as q
                     WHERE (j.qualification = q.qualification) AND 
                     (j.job_id = @jobid OR @jobid IS NULL) 
                     AND (LOWER(j.job_desc) LIKE LOWER(@jobdesc) OR @jobdesc IS NULL) 
                     AND (LOWER(j.department) LIKE LOWER(@dept) OR @dept IS NULL) 
                     AND (j.salary >= @salary OR @salary IS NULL) 
                     AND (q.priority <=  (SELECT priority from employment_qualifications WHERE qualification = @qualification) OR @qualification IS NULL)
                     AND (j.deadline > @deadline)"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@jobid", If(String.IsNullOrEmpty(Job_ID.Text), DBNull.Value, Job_ID.Text))
                    cmd.Parameters.AddWithValue("@jobdesc", "%" & Job_Desc.Text.ToLower() & "%")
                    cmd.Parameters.AddWithValue("@dept", If(String.IsNullOrEmpty(Dept.Text), DBNull.Value, "%" & Dept.Text.ToLower() & "%"))
                    cmd.Parameters.AddWithValue("@salary", If(String.IsNullOrEmpty(Salary.Text), 0, Salary.Text))
                    cmd.Parameters.AddWithValue("@deadline", Date.Now())
                    cmd.Parameters.AddWithValue("@qualification", Qual.Text)

                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)

                    If dataTable.Rows.Count = 0 Then
                        MessageBox.Show("No matching records found.")
                    Else
                        MessageBox.Show("Search successful.")
                    End If
                End Using
            End Using
        Catch ex As MySqlException When ex.Number = 0 ' MySQL error number for server errors
            MessageBox.Show("Error: Unable to connect to the database server. Please check your internet connection or contact support.")
        Catch ex As MySqlException
            MessageBox.Show("Error: Database error occurred. Please try again later.")
        Catch ex As Exception
            MessageBox.Show("An unexpected error occurred. Please contact support.")
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


    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Employment Portal"
        Label2.Text = u_name
        Label3.Text = uid
        DataGridView1.AllowUserToAddRows = False

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

                    If dataTable.Rows.Count = 0 Then
                        MessageBox.Show("No matching records found.")
                    Else
                        MessageBox.Show("Search successful.")
                    End If
                End Using
            End Using
        Catch ex As MySqlException When ex.Number = 0 ' MySQL error number for server errors
            MessageBox.Show("Error: Unable to connect to the database server. Please check your internet connection or contact support.")
        Catch ex As MySqlException
            MessageBox.Show("Error: Database error occurred. Please try again later.")
        Catch ex As Exception
            MessageBox.Show("An unexpected error occurred. Please contact support.")
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

        Dim list As New List(Of String)
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "Select
                                  SUBSTRING_INDEX(SUBSTRING_INDEX(employment_jobs.job_desc, ' ', numbers.n), ' ', -1) desc_words FROM
                                  (SELECT 1 n UNION ALL SELECT 2
                                  UNION ALL SELECT 3 UNION ALL SELECT 4) numbers INNER JOIN employment_jobs
                                  On CHAR_LENGTH(employment_jobs.job_desc)
                                  -CHAR_LENGTH(REPLACE(employment_jobs.job_desc, ' ', ''))>=numbers.n-1
                                  ORDER BY job_desc;"

                Dim cmd As New MySqlCommand(sql, con)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(reader.GetString(0))
                    End While
                    'MessageBox.Show("Read Success")
                End Using

            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        ' Create a new AutoCompleteStringCollection
        Dim autoCompleteSource As New AutoCompleteStringCollection()

        ' Iterate through each string in the list, split it, and add the items to the AutoCompleteStringCollection
        For Each item As String In list
            Dim itemsArray As String() = item.Split(","c)
            autoCompleteSource.AddRange(itemsArray)
        Next

        ' Set AutoCompleteCustomSource, AutoCompleteMode, and AutoCompleteSource properties of ComboBox
        Job_Desc.AutoCompleteCustomSource = autoCompleteSource
        Job_Desc.AutoCompleteMode = AutoCompleteMode.Suggest
        Job_Desc.AutoCompleteSource = Windows.Forms.AutoCompleteSource.CustomSource



        Dim list_dept As New List(Of String)
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT SUBSTRING_INDEX(SUBSTRING_INDEX(department, ',', n.digit+1), ',', -1) AS split_value
FROM employment_jobs
JOIN (
    SELECT 0 AS digit UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4 UNION ALL
    SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9
) AS n ON LENGTH(REPLACE(department, ',' , '')) <= LENGTH(department)-n.digit
ORDER BY employment_jobs.department, n.digit;"

                Dim cmd As New MySqlCommand(sql, con)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        list_dept.Add(reader.GetString(0))
                    End While
                    'MessageBox.Show("Read Success")
                End Using

            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        ' Create a new AutoCompleteStringCollection
        Dim autoCompleteSource2 As New AutoCompleteStringCollection()

        ' Iterate through each string in the list, split it, and add the items to the AutoCompleteStringCollection
        For Each item As String In list_dept
            Dim itemsArray As String() = item.Split(","c)
            autoCompleteSource2.AddRange(itemsArray)
        Next

        ' Set AutoCompleteCustomSource, AutoCompleteMode, and AutoCompleteSource properties of ComboBox
        Dept.AutoCompleteCustomSource = autoCompleteSource2
        Dept.AutoCompleteMode = AutoCompleteMode.Suggest
        Dept.AutoCompleteSource = Windows.Forms.AutoCompleteSource.CustomSource




    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()

        ' Create an instance of employment_portal.vb and display it
        Dim employmentPortalForm As New Employment_portal() With {
            .uid = uid,
            .u_name = u_name
        }
        employmentPortalForm.Show()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        btnclick()
    End Sub
End Class