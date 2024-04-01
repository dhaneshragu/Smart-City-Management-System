Imports System.Data.SqlClient
Public Class Ed_EditECourse

    Public Property CourseID As Integer

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
        Globals.viewChildForm(callingPanel, New Ed_Teacher_Coursera_Course_Content(CourseID, callingPanel))
    End Sub
End Class