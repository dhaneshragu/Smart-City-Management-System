Public Class Ed_Stud_EntranceExam
    Private currentlyOpenChildForm As Form = Nothing

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        CloseCurrentChildForm()
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(New Ed_StudentDashboard())
        Me.Close()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        CloseCurrentChildForm()
        OpenEntranceScreen("Engineering Entrance Exam", 1)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CloseCurrentChildForm()
        OpenEntranceScreen("Medical Entrance Exam", 2)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CloseCurrentChildForm()
        OpenEntranceScreen("Arts Entrance Exam", 3)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        CloseCurrentChildForm()
        OpenEntranceScreen("Commerce Entrance Exam", 4)
    End Sub

    Private Sub PictureButtonvb1_Click(sender As Object, e As EventArgs) Handles PictureButtonvb1.Click
        CloseCurrentChildForm()
        OpenEntranceScreen("Engineering Entrance Exam", 1)
    End Sub

    Private Sub PictureButtonvb4_Click(sender As Object, e As EventArgs) Handles PictureButtonvb4.Click
        CloseCurrentChildForm()
        OpenEntranceScreen("Medical Entrance Exam", 2)
    End Sub

    Private Sub PictureButtonvb3_Click(sender As Object, e As EventArgs) Handles PictureButtonvb3.Click
        CloseCurrentChildForm()
        OpenEntranceScreen("Arts Entrance Exam", 3)
    End Sub

    Private Sub PictureButtonvb2_Click(sender As Object, e As EventArgs) Handles PictureButtonvb2.Click
        CloseCurrentChildForm()
        OpenEntranceScreen("Commerce Entrance Exam", 4)
    End Sub

    Private Sub OpenEntranceScreen(labelText As String, examID As Integer)
        Dim entranceScreen As New Ed_EntranceInnerScreen()
        entranceScreen.labelText = labelText
        entranceScreen.examID = examID
        currentlyOpenChildForm = entranceScreen
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, currentlyOpenChildForm)
    End Sub
    Private Sub CloseCurrentChildForm()
        ' Check if there's a currently open child form
        If currentlyOpenChildForm IsNot Nothing Then
            ' Close the currently open child form
            currentlyOpenChildForm.Close()
            currentlyOpenChildForm = Nothing
        End If
    End Sub

    Private Sub Ed_Stud_EntranceExam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Ed_GlobalDashboard.userName
    End Sub

    Private Sub PictureButtonvb1_Load(sender As Object, e As EventArgs) Handles PictureButtonvb1.Load

    End Sub
End Class
