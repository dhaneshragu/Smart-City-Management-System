Imports MySql.Data.MySqlClient

Public Class Complaints_User_Com_History
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"

    Private Sub LoadandBindDataGridView()
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            ' Open the connection
            Con.Open()

            ' Create a MySqlCommand object with the query and connection
            cmd = New MySqlCommand("SELECT * FROM Complaints WHERE user_id = @userid", Con)
            cmd.Parameters.AddWithValue("@userId", uid)
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
        Label9.Text = u_name
        LoadandBindDataGridView()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, TransportationAdminHome)
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim Complaint_User_Details = New Complaint_User_Details() With {
            .uid = uid,
            .u_name = u_name
        }
        Complaint_User_Details.Show()
        Me.Close()


    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim Complaint_User_Dashboard = New Complaint_User_Dashboard() With {
            .uid = uid,
            .u_name = u_name
        }
        Complaint_User_Dashboard.Show()

        Me.Close()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim Complaints_Lodge_Complaint = New Complaints_Lodge_Complaint() With {
            .uid = uid,
            .u_name = u_name
        }
        Complaints_Lodge_Complaint.Show()

        Me.Close()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Complaints_User_Com_History = New Complaints_User_Com_History() With {
            .uid = uid,
            .u_name = u_name
        }
        Complaints_User_Com_History.Show()
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            ' Get the clicked row
            Dim clickedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            complaint_details_users.Show()

        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        HomePageDashboard.Show()
        Me.Close()
    End Sub

    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub
End Class
