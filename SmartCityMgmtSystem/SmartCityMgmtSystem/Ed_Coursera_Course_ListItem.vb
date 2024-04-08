Imports System.Runtime.InteropServices

Public Class Ed_Coursera_Course_ListItem
    Public innerpanel As Panel
    Public CourseID As Integer
    Public CourseItem As Ed_Coursera_Handler.Course
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Ed_Coursera_Course_ListItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        innerpanel = Ed_GlobalDashboard.innerpanel

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim form As New Ed_Coursera_Course_Enroll(CourseID, innerpanel)
        form.CourseItem = CourseItem
        Globals.viewChildForm(innerpanel, form)
    End Sub
End Class
