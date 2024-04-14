Public Class Ed_Teacher_Moodle_ResourceLinkItem
    Public Property callingPanel As Panel
    Public Property content As Ed_Moodle_Handler.RoomContent
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        If content.ContentType = "Assignment" Then
            ' Open assignment form
            Dim assignmentForm As New Ed_Teacher_Moodle_CourseAss(callingPanel)
            assignmentForm.RoomID = content.RoomID
            assignmentForm.content = content
            Globals.viewChildForm(callingPanel, assignmentForm)

        Else

            Dim resourceForm As New Ed_Teacher_Moodle_CourseResource(callingPanel, "Moodle")
            resourceForm.CourseID = content.RoomID
            resourceForm.content = content
            Globals.viewChildForm(callingPanel, resourceForm)
        End If
    End Sub
End Class
