Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Ed_Coursera_Handler
Public Class Ed_Stud_MyCourses
    Dim handler As New Ed_Coursera_Handler()
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1_Click(sender, e)
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Button1.BackColor = Color.FromArgb(88, 133, 175)
        Button2.BackColor = Color.FromArgb(40, 68, 114)

        Dim courses As Course() = handler.GetInProgressCourses(Ed_GlobalDashboard.userID)

        ' Create Ed_CourseProgress objects and set properties
        Dim labels As Ed_CourseProgress() = New Ed_CourseProgress(courses.Length - 1) {}

        For i As Integer = 0 To courses.Length - 1
            labels(i) = New Ed_CourseProgress()
            labels(i).CourseID = courses(i).CourseID
            labels(i).CourseItem = courses(i)

        Next

        FlowLayoutPanel1.Controls.Clear()
        ' Add Ed_CourseProgress objects to the FlowLayoutPanel
        For Each Ed_CourseProgress As Ed_CourseProgress In labels
            FlowLayoutPanel1.Controls.Add(Ed_CourseProgress)
        Next







    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button2.BackColor = Color.FromArgb(88, 133, 175)
        Button1.BackColor = Color.FromArgb(40, 68, 114)

        Dim courses As Course() = handler.GetCompletedCourses(Ed_GlobalDashboard.userID)

        ' Create Ed_CourseProgress objects and set properties
        Dim labels As Ed_CourseProgress() = New Ed_CourseProgress(courses.Length - 1) {}

        For i As Integer = 0 To courses.Length - 1
            labels(i) = New Ed_CourseProgress()
            labels(i).CourseID = courses(i).CourseID
            labels(i).CourseItem = courses(i)

        Next

        FlowLayoutPanel1.Controls.Clear()
        ' Add Ed_CourseProgress objects to the FlowLayoutPanel
        For Each Ed_CourseProgress As Ed_CourseProgress In labels
            FlowLayoutPanel1.Controls.Add(Ed_CourseProgress)
        Next
    End Sub
End Class