Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Ed_Coursera_Handler
Public Class Ed_Teacher_Moodle_CourseAss

    Public Property RoomID As Integer
    Public Property Seq_no As Integer
    Public Property ResourceName As String
    Public Property VideoLink As String
    Public Property TextContent As String

    Private callingPanel As Panel


    Public Property course As Ed_Moodle_Handler.MoodleCourse
    Public Property content As Ed_Moodle_Handler.RoomContent

    Dim handler As New Ed_Moodle_Handler()

    Public Sub New(panel As Panel)
        InitializeComponent()
        callingPanel = panel

    End Sub

    'Private Sub RichTextBox_ContentsResized(sender As Object, e As ContentsResizedEventArgs)
    '    ' Adjust the size of the RichTextBox to fit its content
    '    Dim richTextBox As RichTextBox = DirectCast(sender, RichTextBox)
    '    richTextBox.Height = e.NewRectangle.Height
    'End Sub
    Private Sub Ed_Teacher_Moodle_CourseAss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AutoScroll = True

        'TextContent = "Problem Statement: Implementing Binary Search" & vbCrLf & vbCrLf & "Description:" & vbCrLf & vbCrLf & "You are tasked with implementing the binary search algorithm in a programming language of your choice. Binary search is a fast and efficient searching algorithm used to find an element within a sorted array by repeatedly dividing the search interval in half. Your implementation should handle both sorted arrays of integers and floating-point numbers."
        TextContent = content.Content
        'AddHandler RichTextBox1.ContentsResized, AddressOf RichTextBox_ContentsResized

        RichTextBox1.Text = TextContent
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim form As New Ed_Teacher_Moodle_CourseContent(callingPanel)
        form.CourseContent = Course
        Globals.viewChildForm(callingPanel, form)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim edit_assgn As New Ed_Teacher_EditAssgn()
        edit_assgn.ShowDialog() ' Show as dialog if needed
    End Sub
End Class