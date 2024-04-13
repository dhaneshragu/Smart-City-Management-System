Public Class Ed_ECourseApproveItem
    Public innerpanel As Panel
    Public CourseID As Integer
    Public CourseItem As Ed_Coursera_Handler.Course
    Private courseraHandler As New Ed_Coursera_Handler()
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim form As New Ed_Coursera_Course_Enroll(CourseID, innerpanel, True)
        form.CourseItem = CourseItem
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, form)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (Button1.Text = "Approve") Then
            courseraHandler.UpdateApprovalStatusToApproved(CourseID)
            Button1.Text = "Approved"
            Button3.Hide()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (Button3.Text = "Reject") Then
            courseraHandler.UpdateApprovalStatusToRejected(CourseID)
            Button3.Text = "Rejected"
            Button1.Hide()
        End If
    End Sub
End Class
