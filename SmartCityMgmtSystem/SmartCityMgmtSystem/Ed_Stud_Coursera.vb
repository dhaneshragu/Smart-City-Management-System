Public Class Ed_Stud_Coursera
    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub Ed_Stud_Coursera_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Globals.viewChildForm(childformPanel, New Ed_Stud_Coursera_Home())

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(Ed_StudentDashboard)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Globals.viewChildForm(childformPanel, New Ed_Stud_Coursera_Home())
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Globals.viewChildForm(childformPanel, New Ed_Stud_MyCourses())
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim form As New Ed_CertificateList()
        form.EC_Insti = "Coursera"
        Globals.viewChildForm(childformPanel, form)
    End Sub
End Class
