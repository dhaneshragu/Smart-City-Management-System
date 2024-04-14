Public Class Service_Provider
    Public Property uid As Integer = 1
    Public Property u_name As String = "Ashish Bharti"
    Private servicePost As Service_Post = Nothing
    Private serviceWithdraw As Service_Withdraw = Nothing
    Private serviceOffered As Service_View_Offered = Nothing
    Private scheduledServices As Service_Scheduled = Nothing
    Private serviceleave As Service_Leave = Nothing
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label3.Text = "Unique Identification No: " & uid
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim servicePortal = New Service_Portal() With {
            .uid = uid,
            .u_name = u_name
        }
        Me.Hide()
        servicePortal.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        servicePost?.Dispose()
        servicePost = New Service_Post() With {
            .uid = uid,
            .u_name = u_name
        }
        'Me.Hide()
        'servicePost.Show()
        Globals.viewChildForm(childformPanel, servicePost)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        serviceWithdraw?.Dispose()
        serviceWithdraw = New Service_Withdraw() With {
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, serviceWithdraw)
        'Me.Hide()
        'serviceWithdraw.Show()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        serviceOffered?.Dispose()
        serviceOffered = New Service_View_Offered() With {
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, serviceOffered)
        'Me.Hide()
        'serviceOffered.Show()

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        scheduledServices?.Dispose()
        scheduledServices = New Service_Scheduled() With {
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, scheduledServices)
        'Me.Hide()
        'scheduledServices.Show()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        serviceleave?.Dispose()
        serviceleave = New Service_Leave() With {
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, serviceleave)
        'Me.Hide()
        'scheduledServices.Show()
    End Sub
End Class
