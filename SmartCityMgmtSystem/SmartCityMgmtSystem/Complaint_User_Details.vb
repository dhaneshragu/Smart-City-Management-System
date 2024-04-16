Public Class Complaint_User_Details
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label9.Text = u_name
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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        HomePageDashboard.Show()
        Me.Close()
    End Sub
End Class
