Imports System.Data.SqlClient

Public Class Ed_EditECourse


    Public CourseItem As Ed_Coursera_Handler.Course
    Dim handler As New Ed_Coursera_Handler()

    Private callingPanel As Panel
    Public Sub New(panel As Panel)
        InitializeComponent()
        callingPanel = panel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim enterSumForm As New Ed_Teacher_AddSummary()
        enterSumForm.ShowDialog() ' Show as dialog if needed
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click


        Dim form As New Ed_Teacher_Coursera_Course_Content(callingPanel)
        form.CourseItem = CourseItem
        Globals.viewChildForm(callingPanel, form)
    End Sub
End Class