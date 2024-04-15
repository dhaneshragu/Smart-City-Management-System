Public Class Complaint_Admin_details
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label9.Text = u_name
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, TransportationAdminHome)
    End Sub

    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        HomePageDashboard.Show()
        Me.Close()
    End Sub
End Class
