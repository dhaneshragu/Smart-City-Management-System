Imports System.Web.UI.WebControls

Public Class Ed_ResourceLinkItem
    Public content As Ed_Coursera_Handler.CourseContent
    Public CourseItem As Ed_Coursera_Handler.Course
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click


        Dim form As New Ed_CourseResource(Ed_GlobalDashboard.innerpanel, "Coursera")
        form.content = content
        form.CourseItem = CourseItem
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, form)
    End Sub

    Private Sub Ed_ResourceLinkItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
