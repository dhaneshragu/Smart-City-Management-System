Public Class Ed_Stud_EntranceExam
    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(Ed_StudentDashboard)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        OpenEntranceScreen("Engineering Entrance Exam", 1)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenEntranceScreen("Medical Entrance Exam", 2)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenEntranceScreen("Arts Entrance Exam", 3)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        OpenEntranceScreen("Commerce Entrance Exam", 4)
    End Sub

    Private Sub PictureButtonvb1_Click(sender As Object, e As EventArgs) Handles PictureButtonvb1.Click
        OpenEntranceScreen("Engineering Entrance Exam", 1)
    End Sub

    Private Sub PictureButtonvb4_Click(sender As Object, e As EventArgs) Handles PictureButtonvb4.Click
        OpenEntranceScreen("Medical Entrance Exam", 2)
    End Sub

    Private Sub PictureButtonvb3_Click(sender As Object, e As EventArgs) Handles PictureButtonvb3.Click
        OpenEntranceScreen("Arts Entrance Exam", 3)
    End Sub

    Private Sub PictureButtonvb2_Click(sender As Object, e As EventArgs) Handles PictureButtonvb2.Click
        OpenEntranceScreen("Commerce Entrance Exam", 4)
    End Sub

    Private Sub OpenEntranceScreen(labelText As String, examID As Integer)
        Dim entranceScreen As New Ed_EntranceInnerScreen()
        entranceScreen.labelText = labelText
        entranceScreen.examID = examID
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, entranceScreen)
    End Sub


    Private Sub Ed_Stud_EntranceExam_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
