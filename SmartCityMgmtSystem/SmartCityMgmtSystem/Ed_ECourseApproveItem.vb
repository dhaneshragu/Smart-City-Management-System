Public Class Ed_ECourseApproveItem
    Public innerpanel As Panel
    Public CourseID As Integer
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, New Ed_Coursera_Course_Enroll(CourseID, innerpanel, True))
    End Sub

    Private Sub Ed_ECourseApproveItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
