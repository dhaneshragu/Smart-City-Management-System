Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Ed_Moodle_Handler
Public Class Ed_Teacher_Moodle_CourseList
    Public RoomID As Integer
    Public Cur_All As String
    Dim handler As New Ed_Moodle_Handler()
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim labels As Ed_LeftPanelItem() = New Ed_LeftPanelItem(8) {}

        '' Create labels and set properties
        'For i As Integer = 0 To 7
        '    labels(i) = New Ed_LeftPanelItem()
        '    labels(i).Label1.Text = "Course " & (i + 1)
        '    AddHandler labels(i).Label1.Click, AddressOf Label_Click ' Add click event handler
        'Next
        '' Add labels to the FlowLayoutPanel
        'For Each Label As Ed_LeftPanelItem In labels
        '    FlowLayoutPanel1.Controls.Add(Label)
        'Next


        ' Get the current user's ID (assuming it's stored in some variable)
        Dim profId As Integer = Ed_GlobalDashboard.Ed_Profile.Ed_User_ID ' Adjust as needed

        ' Retrieve current year courses for the prof
        Dim courses As MoodleCourse() = handler.GetTeacherCourses(profId)

        ' Check if there are any courses
        If courses IsNot Nothing AndAlso courses.Length > 0 Then
            ' Create labels and set properties based on course details
            For Each course As MoodleCourse In courses
                Dim courseLabel As New Ed_LeftPanelItem()
                courseLabel.user_type = "Teacher"
                courseLabel.course = course
                courseLabel.callingPanel = Panel1
                courseLabel.Label1.Text = course.Name ' Assuming Label1 is used to display course names
                courseLabel.Tag = course ' Store the MoodleCourse object in the Tag property for later retrieval
                FlowLayoutPanel1.Controls.Add(courseLabel) ' Add label to the FlowLayoutPanel
            Next
        Else
            MessageBox.Show("No courses found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    'Private Sub Label_Click(sender As Object, e As EventArgs)
    '    Globals.viewChildForm(Panel1, New Ed_Teacher_Moodle_CourseContent(RoomID, Panel1))
    'End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub FlowLayoutPanel1_Paint()

    End Sub
End Class