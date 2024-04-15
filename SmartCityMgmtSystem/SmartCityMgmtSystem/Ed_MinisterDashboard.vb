Public Class Ed_MinisterDashboard
    Private currentlyOpenChildForm As Form = Nothing
    Private Sub Ed_MinisterDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Ed_GlobalDashboard.Ed_Profile.Ed_Name
        currentlyOpenChildForm = New Ed_Minister_Institute_List(childformPanel, "Admin")
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        CloseCurrentChildForm()
        currentlyOpenChildForm = New Ed_Minister_Institute_List(childformPanel, "Admin")
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        CloseCurrentChildForm()
        currentlyOpenChildForm = New Ed_Add_Institution()
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        CloseCurrentChildForm()
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(New Ed_RoleSelect())
        Me.Close()
    End Sub
    Private Sub CloseCurrentChildForm()
        ' Check if there's a currently open child form
        If currentlyOpenChildForm IsNot Nothing Then
            ' Close the currently open child form
            currentlyOpenChildForm.Close()
            currentlyOpenChildForm = Nothing
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        CloseCurrentChildForm()
        currentlyOpenChildForm = New Ed_ManageEntrHome()
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub
End Class
