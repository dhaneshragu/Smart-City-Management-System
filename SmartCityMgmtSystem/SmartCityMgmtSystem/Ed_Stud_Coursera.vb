Public Class Ed_Stud_Coursera
    Private currentlyOpenChildForm As Form = Nothing

    Private Sub Ed_Stud_Coursera_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Ed_GlobalDashboard.userName
        CloseCurrentChildForm()
        currentlyOpenChildForm = New Ed_Stud_Coursera_Home()
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        CloseCurrentChildForm()
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(New Ed_StudentDashboard())
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        CloseCurrentChildForm()
        currentlyOpenChildForm = New Ed_Stud_Coursera_Home()
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CloseCurrentChildForm()
        currentlyOpenChildForm = New Ed_Stud_MyCourses()
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CloseCurrentChildForm()
        Dim form As New Ed_CertificateList()
        form.EC_Insti = "Coursera"
        currentlyOpenChildForm = form
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
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
