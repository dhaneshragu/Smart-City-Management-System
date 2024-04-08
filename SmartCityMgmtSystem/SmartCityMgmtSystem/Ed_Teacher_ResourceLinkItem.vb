Imports System.Web.UI.WebControls

Public Class Ed_Teacher_ResourceLinkItem
    Public content As Ed_Coursera_Handler.CourseContent
    Public CourseItem As Ed_Coursera_Handler.Course

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim form As New Ed_Teacher_Course_Resource(Ed_GlobalDashboard.innerpanel, "Coursera")
        form.content = Content
        form.CourseItem = CourseItem
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, form)
    End Sub
End Class
