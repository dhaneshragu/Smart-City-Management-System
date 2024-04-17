Imports MySql.Data.MySqlClient

Public Class Complaint_User_Dashboard
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"
    Private Sub LoadandBindDataGridView()
        ' Assuming you have three RichTextBox controls named richTextBoxCount1, richTextBoxCount2, and richTextBoxCount3

        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand
        Dim count1 As Integer = 0
        Dim count2 As Integer = 0
        Dim count3 As Integer = 0

        Try
            ' Open the connection
            Con.Open()

            ' Count for status = 'open'
            cmd = New MySqlCommand("SELECT COUNT(*) FROM complaints WHERE user_id = @userId AND status = 'Open'", Con)
            cmd.Parameters.AddWithValue("@userId", uid)
            count1 = Convert.ToInt32(cmd.ExecuteScalar())
            RichTextBox1.Text = count1
            RichTextBox1.SelectionAlignment = HorizontalAlignment.Center

            ' Count for status = 'inprocess'
            cmd = New MySqlCommand("SELECT COUNT(*) FROM complaints WHERE user_id = @userId AND status = 'In Progress'", Con)
            cmd.Parameters.AddWithValue("@userId", uid)
            count2 = Convert.ToInt32(cmd.ExecuteScalar())
            RichTextBox2.Text = count2
            RichTextBox2.SelectionAlignment = HorizontalAlignment.Center

            cmd = New MySqlCommand("SELECT COUNT(*) FROM complaints WHERE user_id = @userId AND status = 'Resolved'", Con)
            cmd.Parameters.AddWithValue("@userId", uid)
            count2 = Convert.ToInt32(cmd.ExecuteScalar())
            RichTextBox3.Text = count2
            RichTextBox3.SelectionAlignment = HorizontalAlignment.Center


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


    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Complaints_User_Com_History = New Complaints_User_Com_History With {
    .uid = uid,
    .u_name = u_name,
    .status_complaint = ">  In Progress Complaints"
}
        Complaints_User_Com_History.Show()

        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Complaints_User_Com_History = New Complaints_User_Com_History With {
            .uid = uid,
            .u_name = u_name,
            .status_complaint = ">  Resolved Complaints"
        }
        Complaints_User_Com_History.Show()

        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim HomePageDashboard = New HomePageDashboard() With {
            .uid = uid
            }
        HomePageDashboard.Show()
        Me.Close()
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Complaints_User_Com_History = New Complaints_User_Com_History With {
            .uid = uid,
            .u_name = u_name,
            .status_complaint = ">  Open Complaints"
        }
        Complaints_User_Com_History.Show()

        Me.Close()
    End Sub
End Class
