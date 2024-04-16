Imports MySql.Data.MySqlClient

Public Class Complaints_admin_dashboard
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"
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
        Dim cmd As MySqlCommand
        Dim count1 As Integer = 0
        Dim count2 As Integer = 0
        Dim count3 As Integer = 0

        Try
            ' Open the connection
            Con.Open()

            cmd = New MySqlCommand("SELECT COUNT(*) FROM Complaints WHERE Department_Name = @main_1", Con)
            cmd.Parameters.AddWithValue("@main_1", department)
            count1 = Convert.ToInt32(cmd.ExecuteScalar())
            RichTextBox1.Text = count1
            RichTextBox1.SelectionAlignment = HorizontalAlignment.Center

            cmd = New MySqlCommand("SELECT COUNT(*) FROM Complaints WHERE Department_Name = @main_1 and Status='Open'", Con)
            cmd.Parameters.AddWithValue("@main_1", department)
            count1 = Convert.ToInt32(cmd.ExecuteScalar())
            RichTextBox2.Text = count1
            RichTextBox2.SelectionAlignment = HorizontalAlignment.Center


        Catch ex As Exception
            ' Handle exceptions
            MessageBox.Show("Error: " & ex.Message)

        Finally
            ' Close the connection
            Con.Close()
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

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Complaint_Admin_details.Show()

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Complaints_manage_complaints = New Complaints_manage_complaints With {
            .uid = uid,
            .u_name = u_name
        }
        Complaints_manage_complaints.Show()

        Me.Close()
    End Sub

    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        HomePageDashboard.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub
End Class
