Public Class Ed_Stud_Institute
    Private currentlyOpenChildForm As Form = Nothing
    Private Sub Ed_Stud_Institute_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Ed_GlobalDashboard.userName
        currentlyOpenChildForm = New Ed_Institute_List()
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        CloseCurrentChildForm()
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(New Ed_StudentDashboard())
        Me.Close()
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        CloseCurrentChildForm()
        currentlyOpenChildForm = New Ed_Institute_List()
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CloseCurrentChildForm()
        currentlyOpenChildForm = New Ed_Inst_FeeDetails()
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        CloseCurrentChildForm()
        Dim form As New Ed_CertificateList()
        form.EC_Insti = "Institute"
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
