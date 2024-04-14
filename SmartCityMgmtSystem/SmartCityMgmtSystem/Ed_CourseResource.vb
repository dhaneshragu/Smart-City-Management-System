Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class Ed_CourseResource
    Public Property CourseID As Integer

    Private callingPanel As Panel
    Private Course_type As String

    Public CourseItem As Ed_Coursera_Handler.Course

    Public content As Ed_Coursera_Handler.CourseContent

    Dim handler As New Ed_Coursera_Handler()

    ' Constructor that accepts a Panel parameter
    Public Sub New(panel As Panel, type As String)
        InitializeComponent()
        callingPanel = panel
        Course_type = type
    End Sub

    Private Sub RichTextBox_ContentsResized(sender As Object, e As ContentsResizedEventArgs)
        ' Adjust the size of the RichTextBox to fit its content
        Dim richTextBox As RichTextBox = DirectCast(sender, RichTextBox)
        richTextBox.Height = e.NewRectangle.Height
    End Sub
    Private Sub Ed_CourseResource_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AutoScroll = True
        Dim completed_resources As Integer() = handler.GetStudentCourseCompletionRecords(Ed_GlobalDashboard.userID, content.CourseID)
        If completed_resources.Contains(content.SeqNo) Then
            Button2.Text = "Completed"
            Button2.Enabled = False
            Button2.BackColor = Color.Green
        End If

        AddHandler RichTextBox1.ContentsResized, AddressOf RichTextBox_ContentsResized
        RichTextBox1.Rtf = content.Content
        Label1.Text = content.ContentName
        Dim youtubeUrl As String = content.VideoLink
        Dim videoId As String = ExtractYouTubeVideoId(youtubeUrl)

        If Not String.IsNullOrEmpty(videoId) Then
            Dim embedUrl As String = $"https://www.youtube.com/embed/{videoId}"
            Dim html As String = $"<!DOCTYPE html><html><head><meta http-equiv='X-UA-Compatible' content='IE=edge'></head><body style='margin:0'><iframe width='1019' height='386' src='{embedUrl}' frameborder='0' allowfullscreen></iframe></body></html>"
            WebBrowser1.DocumentText = html
        Else
            MessageBox.Show("Invalid YouTube URL")
        End If
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If Course_type = "Coursera" Then
            Dim form As New Ed_Coursera_CourseContent(content.CourseID, callingPanel)
            form.CourseItem = CourseItem
            Globals.viewChildForm(callingPanel, form)
            Me.Close()
        End If

    End Sub
    Private Function ExtractYouTubeVideoId(url As String) As String
        Dim regexPattern As String = "(?:https?:\/\/)?(?:www\.)?(?:youtube\.com\/(?:[^\/\n\s]+\/\S+\/|(?:v|e(?:mbed)?)\/|\S*?[?&]v=)|youtu\.be\/)([a-zA-Z0-9_-]{11})"
        Dim match As Match = Regex.Match(url, regexPattern)
        If match.Success Then
            Return match.Groups(1).Value
        Else
            Return Nothing
        End If
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        handler.CompleteResource(Ed_GlobalDashboard.userID, content.CourseID, content.SeqNo)
        Button2.Text = "Completed"
        Button2.Enabled = False
        Button2.BackColor = Color.Green

    End Sub
End Class