Public Class Employment_portal_admin_withdraw

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Employment Portal"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()

        ' Create an instance of employment_portal.vb and display it
        Dim employmentPortalAdminForm As New Employment_portal_admin()
        employmentPortalAdminForm.Show()
    End Sub
End Class
