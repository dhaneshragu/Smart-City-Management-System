Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Ed_Moodle_Handler
Public Class Ed_Moodle_CourseContent
    Private callingPanel As Panel
    Dim handler As New Ed_Moodle_Handler()
    Public CourseContent As Ed_Moodle_Handler.MoodleCourse


    ' Constructor that accepts a Panel parameter
    Public Sub New(panel As Panel)
        InitializeComponent()
        callingPanel = panel
    End Sub

    Private Sub LoadCourseDetails(ByVal roomID As Integer)
        ' Call the function to load course details
        Dim CourseDetails As MoodleCourse = handler.LoadCourseDetails(roomID)
        CourseContent = CourseDetails
        ' Check if course details are retrieved
        If courseDetails IsNot Nothing Then
            ' Set the labels with the retrieved data
            Label1.Text = CourseDetails.Name
            Label2.Text = "Instructor: " & CourseDetails.ProfName
            Label3.Text = "Institute: " & CourseDetails.InstName
        Else
            MessageBox.Show("No course details found for the specified Room_ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub




    Private Sub Ed_Moodle_CourseContent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim RoomID As Integer = CourseContent.RoomID
        LoadCourseDetails(RoomID)

        ' Declare contents outside of the If block
        Dim contents As RoomContent()
        contents = handler.GetRoomContents(RoomID)

        Dim assmarks As StudentAssRecord() = handler.GetAssMarks(Ed_GlobalDashboard.userID, RoomID)
        For Each record As StudentAssRecord In assmarks
            Dim marksValue As String = If(record.Marks <> -1, record.Marks.ToString(), "N/A")

            ' Check if submit time is available
            Dim submitTimeValue As String = If(record.Submit_Time <> DateTime.MinValue, record.Submit_Time.ToString(), "N/A")

            ' Add a new row to the DataGridView and populate it with data from each StudentAssRecord
            DataGridView1.Rows.Add(record.AssName, submitTimeValue, marksValue)
        Next

        ' Check if there are any contents
        If contents IsNot Nothing AndAlso contents.Length > 0 Then
            ' Create labels and set properties based on content details
            For Each content As RoomContent In contents
                Dim contentItem As New Ed_MoodleResouceLinkItem()
                contentItem.callingPanel = callingPanel
                contentItem.Label1.Text = content.ContentName ' Assuming Label1 is used to display content names
                ' Set other properties of contentItem based on content details if needed
                contentItem.content = content
                ' Add contentItem to the FlowLayoutPanel
                FlowLayoutPanel1.Controls.Add(contentItem)
            Next
        Else
            MessageBox.Show("No course content found for the specified Room_ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub


    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class