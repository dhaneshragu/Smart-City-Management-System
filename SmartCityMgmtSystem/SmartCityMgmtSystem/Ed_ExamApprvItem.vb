Public Class Ed_ExamApprvItem
    Public examID As Integer
    Public studentID As Integer
    Private entranceExamHandler As New Ed_EntranceExam_Handler()
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (Button2.Text = "Approve") Then
            entranceExamHandler.approve_pending_request(examID, studentID, DateTime.Now.Year)
            Button1.Hide()
            Button2.Text = "Approved"
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (Button1.Text = "Reject") Then
            entranceExamHandler.reject_pending_request(examID, studentID, DateTime.Now.Year)
            Button2.Hide()
            Button1.Text = "Rejected"
        End If
    End Sub
End Class
