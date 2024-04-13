Public Class Ed_LeftPanelItem
    Public Property callingPanel As Panel
    Public Property course As Ed_Moodle_Handler.MoodleCourse
    Public Property user_type As String = "Student"
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        If user_type = "Student" Then
            Dim form As New Ed_Moodle_CourseContent(callingPanel)
            form.CourseContent = course
            Globals.viewChildForm(callingPanel, form)
        Else
            Dim form As New Ed_Teacher_Moodle_CourseContent(callingPanel)
            form.CourseContent = course
            Globals.viewChildForm(callingPanel, form)
        End If



    End Sub
End Class
