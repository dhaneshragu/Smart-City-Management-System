Public Class Ed_Institute_AdminDashboard
    Private currentlyOpenChildForm As Form = Nothing
    Private Sub Ed_Institute_AdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        currentlyOpenChildForm = New Ed_InstAdmin_AdmitList()
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
End Class
