Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Ed_Moodle_Handler
Public Class Ed_Teacher_Moodle_CourseContent
    Public CourseContent As Ed_Moodle_Handler.MoodleCourse
    Private callingPanel As Panel
    Dim handler As New Ed_Moodle_Handler()


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
        If CourseDetails IsNot Nothing Then
            ' Set the labels with the retrieved data
            Label1.Text = CourseDetails.Name
        Else
            MessageBox.Show("No course details found for the specified Room_ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Ed_Teacher_Moodle_CourseContent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim labels As Ed_Teacher_Moodle_ResourceLinkItem() = New Ed_Teacher_Moodle_ResourceLinkItem(8) {}


        Dim RoomID As Integer = CourseContent.RoomID
        LoadCourseDetails(RoomID)

        ' Declare contents outside of the If block
        Dim contents As RoomContent()
        contents = handler.GetRoomContents(RoomID)

        ' Check if there are any contents
        If contents IsNot Nothing AndAlso contents.Length > 0 Then
            ' Create labels and set properties based on content details
            For Each content As RoomContent In contents
                'if content is a resource'  
                If content.ContentType = "Resource" Then
                    Dim contentItem As New Ed_Teacher_Moodle_ResourceLinkItem()
                    contentItem.callingPanel = callingPanel
                    contentItem.Label1.Text = content.ContentName ' Assuming Label1 is used to display content names
                    ' Set other properties of contentItem based on content details if needed
                    contentItem.content = content
                    ' Add contentItem to the FlowLayoutPanel
                    FlowLayoutPanel1.Controls.Add(contentItem)

                Else
                    Dim contentItem As New Ed_Teacher_Moodle_ResourceLinkItem()
                    contentItem.callingPanel = callingPanel
                    contentItem.Label1.Text = content.ContentName ' Assuming Label1 is used to display content names
                    ' Set other properties of contentItem based on content details if needed
                    contentItem.content = content
                    ' Add contentItem to the FlowLayoutPanel
                    FlowLayoutPanel2.Controls.Add(contentItem)
                End If
            Next
        Else
            MessageBox.Show("No course content found for the specified Room_ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim add_res_form As New Ed_Teacher_Moodle_AddResource()
        add_res_form.callingPanel = callingPanel
        add_res_form.CourseItem = CourseContent
        add_res_form.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim add_assgn_form As New Ed_Teacher_AddAssgn()
        add_assgn_form.callingPanel = callingPanel
        add_assgn_form.CourseItem = CourseContent
        add_assgn_form.ShowDialog()
    End Sub
End Class