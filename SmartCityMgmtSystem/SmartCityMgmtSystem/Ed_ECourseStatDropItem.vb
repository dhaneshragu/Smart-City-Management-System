Public Class Ed_ECourseStatDropItem
    Public CourseID As Integer
    Private courseraHandler As New Ed_Coursera_Handler()
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (Button2.Text = "Drop Course") Then
            Dim intValue As Integer = Integer.Parse(Label8.Text)
            If (intValue = 0) Then
                courseraHandler.DeleteCourse(CourseID)
                Button2.Text = "Dropped"
            Else
                MessageBox.Show("Cannot drop the course because students are enrolled in it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub
End Class
