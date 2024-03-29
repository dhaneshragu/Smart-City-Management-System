Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class Ed_Teacher_Course_Resource
    Public Property CourseID As Integer
    Public Property ResourceName As String
    Public Property VideoLink As String
    Public Property TextContent As String

    Private callingPanel As Panel
    Private Course_type As String

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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If Course_type = "Coursera" Then
            Globals.viewChildForm(callingPanel, New Ed_Teacher_Coursera_Course_Content(CourseID, callingPanel))
            'Else
            '    Globals.viewChildForm(callingPanel, New Ed_Moodle_CourseContent(CourseID, callingPanel))
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

    Private Sub Ed_Teacher_Course_Resource_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AutoScroll = True
        TextContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras suscipit, enim vel dapibus lobortis, sem velit dapibus eros, vel tristique enim dolor ut sem. Fusce eget faucibus urna, vel vulputate felis. Ut condimentum euismod lacus, eget finibus turpis lobortis in. Proin sit amet justo eu felis porttitor consectetur a a libero. Maecenas in pulvinar quam. Cras dictum ligula nec eros congue, et porta orci fringilla. Curabitur a magna lacinia, accumsan mauris ac, suscipit urna. Curabitur commodo malesuada imperdiet. Quisque quis lorem quis diam congue blandit vitae quis purus. Sed vel mi suscipit, sagittis quam a, tempus nunc. Mauris vitae pharetra eros. Proin eget viverra mi. Aenean iaculis rhoncus massa. Maecenas ultricies semper est, ut vestibulum velit varius nec. Morbi non ipsum blandit, sollicitudin lectus sit amet, ultricies turpisPellentesque in dictum magna. In purus justo, commodo eget sapien vitae, mollis vehicula est. Etiam suscipit, mauris vel mollis iaculis, nisi metus porttitor erat, vel pretium leo sapien nec ligula. Ut tempor tortor et quam finibus tincidunt eu sed elit. Etiam et sagittis ex, vel rhoncus leo. Quisque elementum lacinia lorem in pulvinar. Donec libero metus, fringilla in ipsum quis, luctus facilisis arcu. Integer ultricies felis non erat pharetra, ac viverra erat fermentumSuspendisse imperdiet tempus pellentesque. Aliquam vestibulum, enim in hendrerit viverra, massa turpis tincidunt ipsum, sit amet sagittis leo arcu sed felis. Nulla interdum est sapien, vitae finibus eros tempor eu. Aenean libero eros, dapibus nec hendrerit non, vehicula id augue. Nulla condimentum porta lectus nec fermentum. Cras facilisis at est eu volutpat. Integer nisl libero, consectetur sed lorem eget, dictum varius turpis. Curabitur non lectus augue. Nulla eu sapien vitae ex dapibus tempor vel sed purus. Maecenas a ipsum sed leo elementum dictum eget finibus odio. Donec vulputate massa erat. Maecenas aliquam augue ultricies, euismod lorem eu, pretium magna. Curabitur sit amet lacus mi. Curabitur ultricies ornare magna vel congue. Proin id ante eget felis pretium varius sed ut magnaDonec lobortis ipsum id tristique cursus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Curabitur ipsum nisi, placerat vel massa a, consectetur viverra augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse vestibulum sed tortor non imperdiet. Suspendisse eget viverra sem, vel hendrerit sapien. Proin posuere consectetur lorem sit amet pellentesque. Fusce pretium dui quis pretium cursus. Nulla ut diam venenatis lorem vestibulum finibus sit amet eget est. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed pellentesque, ligula id ultricies porta, neque orci pharetra turpis, vel fringilla nunc nibh in neque. In feugiat condimentum odio eget lobortis. Aenean malesuada felis massa, quis rhoncus nunc dignissim a. Vivamus ut dui imperdiet, mollis nisi vel, sollicitudin nulla Donec tincidunt lacinia sem, sed maximus magna condimentum vitae. Phasellus a risus turpis. Praesent vehicula nisi sed enim mollis tincidunt viverra in nunc. Ut vitae viverra lectus, varius volutpat nibh. Phasellus porttitor ultrices lobortis. Proin eu ultrices ipsum. Maecenas aliquet leo at diam ultrices, hendrerit dictum ligula"

        AddHandler RichTextBox1.ContentsResized, AddressOf RichTextBox_ContentsResized

        RichTextBox1.Text = TextContent


        ResourceName = "Testing 123"
        VideoLink = "https://www.youtube.com/watch?v=I0czvJ_jikg&list=RDI0czvJ_jikg&start_radio=1"
        Label1.Text = ResourceName
        RichTextBox1.Text = TextContent
        Dim youtubeUrl As String = "https://www.youtube.com/watch?v=WVOiDcFUg_I" ' Your YouTube video URL
        Dim videoId As String = ExtractYouTubeVideoId(youtubeUrl)

        If Not String.IsNullOrEmpty(videoId) Then
            Dim embedUrl As String = $"https://www.youtube.com/embed/{videoId}"
            Dim html As String = $"<!DOCTYPE html><html><head><meta http-equiv='X-UA-Compatible' content='IE=edge'></head><body style='margin:0'><iframe width='1207' height='386' src='{embedUrl}' frameborder='0' allowfullscreen></iframe></body></html>"
            WebBrowser1.DocumentText = html
        Else
            MessageBox.Show("Invalid YouTube URL")
        End If
    End Sub
End Class