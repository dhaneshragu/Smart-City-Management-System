Public Class Ed_MinisterDashboard
    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub Ed_MinisterDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ed_list As New Ed_Institute_List(childformPanel)
        ed_list.user_type = "Admin"
        Globals.viewChildForm(childformPanel, ed_list)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim ed_list As New Ed_Institute_List(childformPanel)
        ed_list.user_type = "Admin"
        Globals.viewChildForm(childformPanel, ed_list)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Globals.viewChildForm(childformPanel, New Ed_Add_Institution)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(Ed_RoleSelect)
    End Sub
End Class
