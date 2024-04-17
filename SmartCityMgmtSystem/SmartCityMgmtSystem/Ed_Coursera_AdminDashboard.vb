Public Class Ed_Coursera_AdminDashboard
    Private currentlyOpenChildForm As Form = Nothing
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        CloseCurrentChildForm()
        currentlyOpenChildForm = New Ed_EcourseApproveCourses()
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        CloseCurrentChildForm()
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(New Ed_RoleSelect())
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CloseCurrentChildForm()
        currentlyOpenChildForm = New Ed_ECourseManage_Stats()
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Ed_Coursera_AdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Ed_GlobalDashboard.Ed_Profile.Ed_Name

    End Sub
    Private Sub CloseCurrentChildForm()
        ' Check if there's a currently open child form
        If currentlyOpenChildForm IsNot Nothing Then
            ' Close the currently open child form
            currentlyOpenChildForm.Close()
            currentlyOpenChildForm = Nothing
        End If
    End Sub


End Class
