Imports Microsoft.VisualBasic.ApplicationServices

Public Class Ed_RoleSelect

    Private Sub pictureButtonvb1_Click(sender As Object, e As EventArgs) Handles PictureButtonvb1.Click
        ' Handle the Click event of the user control
        ' Create an instance of Ed_LoginHandle
        Dim loginHandle As New Ed_LoginHandle()
        ' Call the GetEdProfileByUserID function
        loginHandle.GetEdProfileByUserID(Ed_GlobalDashboard.userID)
        Dim form As New Ed_StudentDashboard()
        Ed_GlobalDashboard.innerpanel = form.childformPanel
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(form)
        Me.Close()
    End Sub

    Private Sub pictureButtonvb1_Hover(sender As Object, e As EventArgs) Handles PictureButtonvb1.Hover
        ' Handle the Hover event of the user control
        PictureButtonvb1.BackColor = Color.White
    End Sub

    Private Sub pictureButtonvb1_Leave(sender As Object, e As EventArgs) Handles PictureButtonvb1.Leave
        ' Handle the Leave event of the user control
        PictureButtonvb1.BackColor = Color.FromArgb(196, 224, 229)
    End Sub
    Private Sub pictureButtonvb2_Click(sender As Object, e As EventArgs) Handles PictureButtonvb2.Click
        ' Handle the Click event of the user control

        Dim loginHandle As New Ed_LoginHandle()
        loginHandle.GetEdProfileByUserID(Ed_GlobalDashboard.userID)
        If (Ed_GlobalDashboard.Ed_Profile.Ed_User_Type = Ed_GlobalDashboard.UserType.Teacher) Then
            Dim form As New Ed_TeacherDashboard()
            Ed_GlobalDashboard.innerpanel = form.childformPanel
            Ed_GlobalDashboard.OpenFormInGlobalEdPanel(form)
            Me.Close()
        Else
            Dim message As String = "Sorry, you are not qualified to enter as a teacher."
            Dim title As String = "Access Denied"
            Dim buttons As MessageBoxButtons = MessageBoxButtons.OK
            Dim icon As MessageBoxIcon = MessageBoxIcon.Exclamation
            MessageBox.Show(message, title, buttons, icon)
        End If

    End Sub

    Private Sub pictureButtonvb2_Hover(sender As Object, e As EventArgs) Handles PictureButtonvb2.Hover
        ' Handle the Hover event of the user control
        PictureButtonvb2.BackColor = Color.White
    End Sub

    Private Sub pictureButtonvb2_Leave(sender As Object, e As EventArgs) Handles PictureButtonvb2.Leave
        ' Handle the Leave event of the user control
        PictureButtonvb2.BackColor = Color.FromArgb(196, 224, 229)
    End Sub
    Private Sub pictureButtonvb3_Click(sender As Object, e As EventArgs) Handles PictureButtonvb3.Click
        ' Handle the Click event of the user control


        Dim loginHandle As New Ed_LoginHandle()
        loginHandle.GetEdProfileByUserID(Ed_GlobalDashboard.userID)
        If (Ed_GlobalDashboard.Ed_Profile.Ed_User_Type = Ed_GlobalDashboard.UserType.Admin) Then
            ' Handle different cases inside
            If (Ed_GlobalDashboard.Ed_Profile.Ed_User_Role = Ed_GlobalDashboard.UserRole.Principal) Then
                Dim form As New Ed_Institute_AdminDashboard()
                Ed_GlobalDashboard.innerpanel = form.childformPanel
                Ed_GlobalDashboard.OpenFormInGlobalEdPanel(form)
                Me.Close()
            End If
            If (Ed_GlobalDashboard.Ed_Profile.Ed_User_Role = Ed_GlobalDashboard.UserRole.EcourseAdmin) Then
                Dim form As New Ed_Coursera_AdminDashboard()
                Ed_GlobalDashboard.innerpanel = form.childformPanel
                Ed_GlobalDashboard.OpenFormInGlobalEdPanel(form)
                Me.Close()
            End If
            If (Ed_GlobalDashboard.Ed_Profile.Ed_User_Role = Ed_GlobalDashboard.UserRole.Minister) Then
                Dim form As New Ed_MinisterDashboard()
                Ed_GlobalDashboard.innerpanel = form.childformPanel
                Ed_GlobalDashboard.OpenFormInGlobalEdPanel(form)
                Me.Close()
            End If
        Else
            Dim message As String = "Sorry, you are not qualified to enter as an admin."
            Dim title As String = "Access Denied"
            Dim buttons As MessageBoxButtons = MessageBoxButtons.OK
            Dim icon As MessageBoxIcon = MessageBoxIcon.Exclamation

            MessageBox.Show(message, title, buttons, icon)
        End If
    End Sub

    Private Sub pictureButtonvb3_Hover(sender As Object, e As EventArgs) Handles PictureButtonvb3.Hover
        ' Handle the Hover event of the user control
        PictureButtonvb3.BackColor = Color.White
    End Sub

    Private Sub pictureButtonvb3_Leave(sender As Object, e As EventArgs) Handles PictureButtonvb3.Leave
        ' Handle the Leave event of the user control
        PictureButtonvb3.BackColor = Color.FromArgb(196, 224, 229)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim homepage = New HomePageDashboard With {
            .uid = Ed_GlobalDashboard.userID
        }
        Me.ParentForm.Hide()
        Me.Close()
        homepage.Show()
    End Sub

    Private Sub Ed_RoleSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Ed_GlobalDashboard.userName
    End Sub
End Class
