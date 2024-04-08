Public Class Ed_LeftPanelItem
    Public Property callingPanel As Panel
    Public Property course As Ed_Moodle_Handler.MoodleCourse
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim form As New Ed_Moodle_CourseContent(callingPanel)
        form.CourseContent = course
        Globals.viewChildForm(callingPanel, form)

    End Sub
End Class
