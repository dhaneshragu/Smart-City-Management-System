Public Class Employment_portal_admin

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Employment Portal"
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, Employment_portal_admin_add)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()

        ' Create an instance of employment_portal_admin_withdraw.vb and display it
        Dim employmentPortalAdminWithdrawForm As New Employment_portal_admin_withdraw()
        employmentPortalAdminWithdrawForm.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim usercredentials As New Employment_portal_admin_usercredentials()
        usercredentials.Show()
    End Sub
End Class
