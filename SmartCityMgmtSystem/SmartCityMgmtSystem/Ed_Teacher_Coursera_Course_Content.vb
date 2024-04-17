Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports SmartCityMgmtSystem.Ed_Coursera_Handler
Public Class Ed_Teacher_Coursera_Course_Content



    Private callingPanel As Panel
    Public Property ResourceName As String
    Public Property VideoLink As String
    Public Property TextContent As String
    Public CourseItem As Ed_Coursera_Handler.Course
    Dim handler As New Ed_Coursera_Handler()

    ' Constructor that accepts a Panel parameter
    Public Sub New(panel As Panel)
        InitializeComponent()
        callingPanel = panel
    End Sub
    Private Sub RichTextBox_ContentsResized(sender As Object, e As ContentsResizedEventArgs)
        ' Adjust the size of the RichTextBox to fit its content
        Dim richTextBox As RichTextBox = DirectCast(sender, RichTextBox)
        richTextBox.Height = e.NewRectangle.Height
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
        Dim form As New Ed_EditECourse(callingPanel)
        form.CourseItem = CourseItem
        Globals.viewChildForm(callingPanel, form)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, New Ed_ManageECourse())
        Me.Close()
    End Sub




    Private Sub Ed_Teacher_Coursera_Course_Content_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AutoScroll = True


        AddHandler RichTextBox1.ContentsResized, AddressOf RichTextBox_ContentsResized

        RichTextBox1.Text = CourseItem.Syllabus
        FlowLayoutPanel1.Height = RichTextBox1.Height + 391



        Label1.Text = CourseItem.Name
        Label2.Text = CourseItem.TeacherName
        Label3.Text = CourseItem.Institution
        RichTextBox1.Rtf = CourseItem.Syllabus
        Dim youtubeUrl As String = CourseItem.IntroVideoLink
        Dim videoId As String = ExtractYouTubeVideoId(youtubeUrl)

        If Not String.IsNullOrEmpty(videoId) Then
            Dim embedUrl As String = $"https://www.youtube.com/embed/{videoId}"
            Dim html As String = $"<!DOCTYPE html><html><head><meta http-equiv='X-UA-Compatible' content='IE=edge'></head><body style='margin:0'><iframe width='737' height='265' src='{embedUrl}' frameborder='0' allowfullscreen></iframe></body></html>"
            WebBrowser1.DocumentText = html
        Else
            MessageBox.Show("Invalid YouTube URL")
        End If



        Dim contents As CourseContent() = handler.GetCourseContents(CourseItem.CourseID)

        ' Create Ed_ResourceLinkItem objects and set properties
        Dim labels As Ed_Teacher_ResourceLinkItem() = New Ed_Teacher_ResourceLinkItem(contents.Length - 1) {}

        For i As Integer = 0 To contents.Length - 1
            labels(i) = New Ed_Teacher_ResourceLinkItem()
            labels(i).content = contents(i)
            labels(i).CourseItem = CourseItem
            labels(i).Label1.Text = contents(i).ContentName


        Next

        FlowLayoutPanel1.Controls.Clear()
        ' Add Ed_ResourceLinkItem objects to the FlowLayoutPanel
        For Each Ed_ResourceLinkItem As Ed_Teacher_ResourceLinkItem In labels
            FlowLayoutPanel1.Controls.Add(Ed_ResourceLinkItem)
        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim add_res_form As New Ed_Teacher_AddResource()
        add_res_form.CourseItem = CourseItem
        add_res_form.ShowDialog()
    End Sub

End Class