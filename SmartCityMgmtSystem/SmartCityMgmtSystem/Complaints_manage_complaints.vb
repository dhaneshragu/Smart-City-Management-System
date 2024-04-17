Imports System.Security.Cryptography
Imports MySql.Data.MySqlClient

Public Class Complaints_manage_complaints
    Public Property uid As Integer = -1
    Public Property pid As Integer = -1
    Public Property u_name As String = "Hello"

    Public Property status_complaint As String = "Hello"
    Private Sub LoadandBindDataGridView()

        ' Assuming you have three RichTextBox controls named richTextBoxCount1, richTextBoxCount2, and richTextBoxCount3
        Dim departmentMap As New Dictionary(Of Integer, String)()
        ' Populate the dictionary with user ID and department mappings
        departmentMap.Add(552, "Police")
        departmentMap.Add(553, "Water Supply")
        departmentMap.Add(554, "Electricity")
        departmentMap.Add(555, "Hospital")
        departmentMap.Add(556, "Official Employee Complaints")
        Dim department As String = departmentMap(uid)

        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            ' Open the connection
            Con.Open()

            ' Create a MySqlCommand object with the query and connection
            cmd = New MySqlCommand("SELECT * FROM complaints WHERE Department_Name = @name and status=@status_1", Con)
            ' Add parameter for Department_Name
            cmd.Parameters.AddWithValue("@name", department)

            Dim Status_now As String = "Now"

            If status_complaint = ">  In Progress Complaints" Then
                Status_now = "In Progress"
            ElseIf status_complaint = ">  Open Complaints" Then
                Status_now = "Open"
            Else
                Status_now = "Resolved"


            End If
            cmd.Parameters.AddWithValue("@status_1", Status_now)
            reader = cmd.ExecuteReader()

            ' Create a DataTable to store the data
            Dim dataTable As New DataTable()

            ' Fill the DataTable with data from the SQL table
            dataTable.Load(reader)

            ' Close the reader and connection
            reader.Close()
            Con.Close()

            ' Specify the Column Mappings from DataGridView to SQL Table
            DataGridView1.AutoGenerateColumns = False
            DataGridView1.DataSource = dataTable
            DataGridView1.Columns(0).DataPropertyName = "ComplaintID"
            DataGridView1.Columns(1).DataPropertyName = "Timestamp"
            DataGridView1.Columns(2).DataPropertyName = "Department_Name"
            DataGridView1.Columns(3).DataPropertyName = "ComplaintTitle"
            DataGridView1.Columns(4).DataPropertyName = "Status"
            DataGridView1.Columns(5).DataPropertyName = "Priority"

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close the connection if it's still open
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try

    End Sub
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
        Label9.Text = u_name
        Label4.Text = status_complaint

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, TransportationAdminHome)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim Complaints_admin_dashboard = New Complaints_admin_dashboard() With {
            .uid = uid,
            .u_name = u_name
        }
        Complaints_admin_dashboard.Show()
        Me.Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim Complaint_Admin_details = New Complaint_Admin_details() With {
            .uid = uid,
            .u_name = u_name
        }
        Complaint_Admin_details.Show()
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            ' Get the value of the clicked cell in the first column
            pid = CInt(DataGridView1.Rows(e.RowIndex).Cells(0).Value)
            Dim Complaint_admin_complaints_details = New Complaint_admin_complaints_details() With {
                .uid = uid,
                .u_name = u_name,
                .pid = pid
            }
            Complaint_admin_complaints_details.Show()
            Me.Close()
            ' Open the Sach page and pass the parameters
        End If
    End Sub

    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim HomePageDashboard = New HomePageDashboard() With {
            .uid = uid
            }
        HomePageDashboard.Show()
        Me.Close()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub
End Class
