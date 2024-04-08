Public Class Ed_CourseProgress
    Public CourseID As Integer
    Public CourseItem As Ed_Coursera_Handler.Course
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim form As New Ed_Coursera_CourseContent(CourseID, Ed_GlobalDashboard.innerpanel)
        form.CourseItem = CourseItem
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, form)
    End Sub

    Private Sub Ed_CourseProgress_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label6.Text = CourseItem.Name
        Label1.Text = CourseItem.TeacherName
    End Sub

End Class
