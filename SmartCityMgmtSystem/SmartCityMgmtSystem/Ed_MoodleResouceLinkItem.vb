Public Class Ed_MoodleResouceLinkItem
    Public Property callingPanel As Panel
    Public Property content As Ed_Moodle_Handler.RoomContent
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        If content.ContentType = "Assignment" Then
            ' Open assignment form
            Dim assignmentForm As New Ed_Moodle_CourseAss(callingPanel)
            assignmentForm.RoomID = content.RoomID
            assignmentForm.content = content
            Globals.viewChildForm(callingPanel, assignmentForm)

        Else

            Dim resourceForm As New Ed_Moodle_CourseResource(callingPanel, "Moodle")
            resourceForm.CourseID = content.RoomID
            resourceForm.content = content
            Globals.viewChildForm(callingPanel, resourceForm)
        End If
    End Sub

    Private Sub Ed_MoodleResouceLinkItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
