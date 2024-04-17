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
        Dim completionCounts As Dictionary(Of Integer, Integer) = handler.GetStudentCourseCompletionCounts(Ed_GlobalDashboard.userID)

        ' Get the total content counts for the student's courses
        Dim contentCounts As Dictionary(Of Integer, Integer) = handler.GetStudentCourseContentCounts(Ed_GlobalDashboard.userID)

        ' Use the completionCounts and contentCounts dictionaries as needed

        Dim labels As Ed_CourseProgress() = New Ed_CourseProgress(courses.Length - 1) {}

        For i As Integer = 0 To courses.Length - 1
            Dim courseIdToCheck As Integer = courses(i).CourseID ' Replace 123 with the actual course ID you want to check

            ' Initialize comp and total variables
            Dim comp As Integer = If(completionCounts.ContainsKey(courseIdToCheck), completionCounts(courseIdToCheck), 0)
            Dim total As Integer = If(contentCounts.ContainsKey(courseIdToCheck), contentCounts(courseIdToCheck), 0)

            ' Use the values of comp and total as needed...
            labels(i) = New Ed_CourseProgress()
            labels(i).CourseID = courses(i).CourseID
            labels(i).CourseItem = courses(i)
            If total = 0 Then
                labels(i).ProgressBar1.Value = 0
            Else
                labels(i).ProgressBar1.Value = comp / total * 100

            End If


        Next

        ' Create a list to hold labels that do not have ProgressBar1 value of 100
        Dim newList As New List(Of Ed_CourseProgress)()

        ' Iterate through the array of labels
        For Each label As Ed_CourseProgress In labels
            ' Check if the ProgressBar1 value is not 100
            If label.ProgressBar1.Value <> 100 Then
                ' Add the label to the new list
                newList.Add(label)
            Else
                handler.CompleteCourse(label.CourseItem.Name, label.CourseItem.Affiliation, Ed_GlobalDashboard.userID, label.CourseID)
            End If
        Next

        ' Convert the list back to an array
        labels = newList.ToArray()




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
            labels(i).ProgressBar1.Value = 100

        Next

        FlowLayoutPanel1.Controls.Clear()
        ' Add Ed_CourseProgress objects to the FlowLayoutPanel
        For Each Ed_CourseProgress As Ed_CourseProgress In labels
            FlowLayoutPanel1.Controls.Add(Ed_CourseProgress)
        Next
    End Sub
End Class