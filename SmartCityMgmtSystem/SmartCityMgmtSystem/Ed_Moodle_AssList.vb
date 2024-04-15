Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Ed_Moodle_Handler
Public Class Ed_Moodle_AssList
    Public RoomID As Integer
    Public Seq_no As Integer
    Dim handler As New Ed_Moodle_Handler()
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim contents As RoomContent()
        contents = handler.GetEnrolledAssignments(Ed_GlobalDashboard.userID)

        ' Check if there are any contents
        If contents IsNot Nothing AndAlso contents.Length > 0 Then
            ' Create labels and set properties based on content details
            For Each content As RoomContent In contents
                Dim contentItem As New Ed_MoodleResouceLinkItem()
                contentItem.callingPanel = Panel1
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


    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub FlowLayoutPanel1_Paint_1(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub
End Class