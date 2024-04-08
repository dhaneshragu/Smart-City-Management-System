Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class Ed_Coursera_Course_Enroll

    Private CourseID As Integer
    Private callingPanel As Panel
    Public CourseItem As Ed_Coursera_Handler.Course
    Public Property eCourseAdmin As Boolean



    ' Constructor that accepts a Panel parameter
    Public Sub New(courseID As Integer, panel As Panel)
        InitializeComponent()
        courseID = courseID
        callingPanel = panel
        eCourseAdmin = False
    End Sub
    Public Sub New(courseID As Integer, panel As Panel, isCourseAdmin As Boolean)
        InitializeComponent()
        courseID = courseID
        callingPanel = panel
        eCourseAdmin = isCourseAdmin
    End Sub
    Private Sub RichTextBox_ContentsResized(sender As Object, e As ContentsResizedEventArgs)
        ' Adjust the size of the RichTextBox to fit its content
        Dim richTextBox As RichTextBox = DirectCast(sender, RichTextBox)
        richTextBox.Height = e.NewRectangle.Height
    End Sub
    Private Sub Ed_Coursera_CourseContent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AutoScroll = True
        AddHandler RichTextBox1.ContentsResized, AddressOf RichTextBox_ContentsResized

        If (eCourseAdmin) Then
            Button2.Hide()
        End If
        Label1.Text = CourseItem.Name
        Label2.Text = CourseItem.TeacherName
        Label3.Text = CourseItem.Institution
        RichTextBox1.Text = CourseItem.Syllabus
        Dim youtubeUrl As String = CourseItem.IntroVideoLink
        Dim videoId As String = ExtractYouTubeVideoId(youtubeUrl)

        If Not String.IsNullOrEmpty(videoId) Then
            Dim embedUrl As String = $"https://www.youtube.com/embed/{videoId}"
            Dim html As String = $"<!DOCTYPE html><html><head><meta http-equiv='X-UA-Compatible' content='IE=edge'></head><body style='margin:0'><iframe width='1022' height='363' src='{embedUrl}' frameborder='0' allowfullscreen></iframe></body></html>"
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
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Label6_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If (eCourseAdmin) Then
            Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, New Ed_EcourseApproveCourses())
        Else
            Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, New Ed_Stud_Coursera_Home())
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim PayForm As New Ed_FeePopup()
        PayForm.Show()
    End Sub
End Class