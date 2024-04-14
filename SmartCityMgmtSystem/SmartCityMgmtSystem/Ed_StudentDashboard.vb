Public Class Ed_StudentDashboard
    Private Sub PictureButtonvb1_Click(sender As Object, e As EventArgs) Handles PictureButtonvb1.Click
        ' Replace with your desired functionality
        Dim form As New Ed_Stud_Coursera()
        Ed_GlobalDashboard.innerpanel = form.childformPanel
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(form)
        Me.Close()
    End Sub

    Private Sub PictureButtonvb1_Hover(sender As Object, e As EventArgs) Handles PictureButtonvb1.Hover
        ' Handle the Hover event of the user control
        PictureButtonvb1.BackColor = Color.White
    End Sub

    Private Sub PictureButtonvb1_Leave(sender As Object, e As EventArgs) Handles PictureButtonvb1.Leave
        ' Handle the Leave event of the user control
        PictureButtonvb1.BackColor = Color.FromArgb(196, 224, 229)
    End Sub

    Private Sub PictureButtonvb2_Click(sender As Object, e As EventArgs) Handles PictureButtonvb2.Click
        If (Ed_GlobalDashboard.Ed_Profile.Ed_User_Type = Ed_GlobalDashboard.UserType.Student) Then
            Dim form As New Ed_Stud_Moodle()
            Ed_GlobalDashboard.innerpanel = form.childformPanel
            Ed_GlobalDashboard.OpenFormInGlobalEdPanel(form)
            Me.Close()
        Else
            MessageBox.Show("Only students are allowed to enter.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
        ' Replace with your desired functionality

    End Sub

    Private Sub PictureButtonvb2_Hover(sender As Object, e As EventArgs) Handles PictureButtonvb2.Hover
        ' Handle the Hover event of the user control
        PictureButtonvb2.BackColor = Color.White
    End Sub

    Private Sub PictureButtonvb2_Leave(sender As Object, e As EventArgs) Handles PictureButtonvb2.Leave
        ' Handle the Leave event of the user control
        PictureButtonvb2.BackColor = Color.FromArgb(196, 224, 229)
    End Sub

    Private Sub PictureButtonvb3_Click(sender As Object, e As EventArgs) Handles PictureButtonvb3.Click
        ' Replace with your desired functionality
        If (Ed_GlobalDashboard.Ed_Profile.Ed_User_Type = Ed_GlobalDashboard.UserType.Student) Then
            Dim form As New Ed_Stud_Institute()
            Ed_GlobalDashboard.innerpanel = form.childformPanel
            Ed_GlobalDashboard.OpenFormInGlobalEdPanel(form)
            Me.Close()
        Else
            MessageBox.Show("Only students are allowed to enter.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Private Sub PictureButtonvb3_Hover(sender As Object, e As EventArgs) Handles PictureButtonvb3.Hover
        ' Handle the Hover event of the user control
        PictureButtonvb3.BackColor = Color.White
    End Sub

    Private Sub PictureButtonvb3_Leave(sender As Object, e As EventArgs) Handles PictureButtonvb3.Leave
        ' Handle the Leave event of the user control
        PictureButtonvb3.BackColor = Color.FromArgb(196, 224, 229)
    End Sub

    Private Sub PictureButtonvb4_Click(sender As Object, e As EventArgs) Handles PictureButtonvb4.Click
        ' Replace with your desired functionality
        Dim form As New Ed_Stud_EntranceExam()
        Ed_GlobalDashboard.innerpanel = form.childformPanel
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(form)
        Me.Close()
    End Sub

    Private Sub PictureButtonvb4_Hover(sender As Object, e As EventArgs) Handles PictureButtonvb4.Hover
        ' Handle the Hover event of the user control
        PictureButtonvb4.BackColor = Color.White
    End Sub

    Private Sub PictureButtonvb4_Leave(sender As Object, e As EventArgs) Handles PictureButtonvb4.Leave
        ' Handle the Leave event of the user control
        PictureButtonvb4.BackColor = Color.FromArgb(196, 224, 229)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(New Ed_RoleSelect())
        Me.Close()
    End Sub

    Private Sub Ed_StudentDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Ed_GlobalDashboard.Ed_Profile.Ed_Name
    End Sub

    Private Sub PictureButtonvb3_Load(sender As Object, e As EventArgs) Handles PictureButtonvb3.Load

    End Sub
End Class
