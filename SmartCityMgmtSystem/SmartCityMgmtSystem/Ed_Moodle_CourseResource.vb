Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports SmartCityMgmtSystem.Ed_Coursera_Handler


Public Class Ed_Moodle_CourseResource

    Public Property CourseID As Integer
    Public Property ResourceName As String
    Public Property VideoLink As String
    Public Property TextContent As String

    Private callingPanel As Panel
    Private Course_type As String
    Public Property content As Ed_Moodle_Handler.RoomContent
    Public Property course As Ed_Moodle_Handler.MoodleCourse
    Dim handler As New Ed_Moodle_Handler()

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
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AutoScroll = True
        course = handler.LoadCourseDetails(CourseID)

        AddHandler RichTextBox1.ContentsResized, AddressOf RichTextBox_ContentsResized

        RichTextBox1.Rtf = content.Content
        Label1.Text = content.ContentName
        Dim youtubeUrl As String = content.VideoLink ' Your YouTube video URL
        Dim videoId As String = ExtractYouTubeVideoId(youtubeUrl)

        If Not String.IsNullOrEmpty(videoId) Then
            Dim embedUrl As String = $"https://www.youtube.com/embed/{videoId}"
            Dim html As String = $"<!DOCTYPE html><html><head><meta http-equiv='X-UA-Compatible' content='IE=edge'></head><body style='margin:0'><iframe width='713' height='347' src='{embedUrl}' frameborder='0' allowfullscreen></iframe></body></html>"
            WebBrowser1.DocumentText = html
        Else
            MessageBox.Show("Invalid YouTube URL")
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


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim form As New Ed_Moodle_CourseContent(callingPanel)
        form.CourseContent = course
        Globals.viewChildForm(callingPanel, form)
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub
End Class