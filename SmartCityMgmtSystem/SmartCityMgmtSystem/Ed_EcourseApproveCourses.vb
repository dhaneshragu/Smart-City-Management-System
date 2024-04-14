Imports System.Data.SqlClient

Public Class Ed_EcourseApproveCourses
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Create an instance of Ed_Coursera_Handler to access its methods
        Dim courseraHandler As New Ed_Coursera_Handler()

        ' Call the GetPendingCourses function to retrieve pending courses
        Dim pendingCourses As Ed_Coursera_Handler.Course() = courseraHandler.GetPendingCourses()

        ' Create Ed_ECourseApproveItem controls for each pending course and add them to FlowLayoutPanel
        For Each course As Ed_Coursera_Handler.Course In pendingCourses
            Dim approveItem As New Ed_ECourseApproveItem()
            ' Set properties of Ed_ECourseApproveItem control based on course information
            approveItem.CourseID = course.CourseID
            approveItem.CourseItem = course
            approveItem.Label6.Text = course.Name
            approveItem.Label1.Text = course.TeacherName
            approveItem.Label3.Text = course.Fees
            ' Add Ed_ECourseApproveItem control to FlowLayoutPanel
            FlowLayoutPanel1.Controls.Add(approveItem)
        Next
    End Sub
End Class
