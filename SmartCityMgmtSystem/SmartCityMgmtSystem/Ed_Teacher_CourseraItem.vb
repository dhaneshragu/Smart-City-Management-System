Public Class Ed_Teacher_CourseraItem

    Public CourseID As Integer
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, New Ed_Teacher_Coursera_Course_Content(CourseID, Ed_GlobalDashboard.innerpanel))
    End Sub
End Class
