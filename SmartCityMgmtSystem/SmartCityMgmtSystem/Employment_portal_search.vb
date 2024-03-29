Public Class Employment_portal_search

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Employment Portal"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()

        ' Create an instance of employment_portal.vb and display it
        Dim employmentPortalForm As New Employment_portal()
        employmentPortalForm.Show()
    End Sub
End Class
