Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Ed_Moodle_Handler

Public Class Ed_Moodle_CourseList
    Public RoomID As Integer
    Public Cur_All As String
    Dim handler As New Ed_Moodle_Handler()
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Get the current user's ID (assuming it's stored in some variable)
        Dim studentId As Integer = Ed_GlobalDashboard.userID ' Adjust as needed

        ' Retrieve current year courses for the student
        Dim courses As MoodleCourse() = handler.GetStudentCourses(studentId)

        ' Check if there are any courses
        If courses IsNot Nothing AndAlso courses.Length > 0 Then
            ' Create labels and set properties based on course details
            For Each course As MoodleCourse In courses
                Dim courseLabel As New Ed_LeftPanelItem()
                courseLabel.course = course
                courseLabel.callingPanel = Panel1
                courseLabel.Label1.Text = course.Name ' Assuming Label1 is used to display course names
                courseLabel.Tag = course ' Store the MoodleCourse object in the Tag property for later retrieval
                FlowLayoutPanel1.Controls.Add(courseLabel) ' Add label to the FlowLayoutPanel
            Next
        Else
            MessageBox.Show("No courses found for the current year.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub


    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class