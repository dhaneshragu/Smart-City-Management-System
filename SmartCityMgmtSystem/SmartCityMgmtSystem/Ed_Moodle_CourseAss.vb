Imports System.Data.SqlClient
Imports System.IO
Public Class Ed_Moodle_CourseAss

    Public Property RoomID As Integer
    Public Property Seq_no As Integer
    Public Property ResourceName As String
    Public Property VideoLink As String
    Public Property TextContent As String

    Private callingPanel As Panel
    Private Course_type As String

    Public Property course As Ed_Moodle_Handler.MoodleCourse
    Public Property content As Ed_Moodle_Handler.RoomContent

    Dim handler As New Ed_Moodle_Handler()

    Public Sub New(panel As Panel)
        InitializeComponent()
        callingPanel = panel
    End Sub





    Private Sub Label6_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "All Files|*.*"
        openFileDialog.Title = "Select a File"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Dim selectedFilePath As String = openFileDialog.FileName

            ' Display the selected file in the WebBrowser control
            'WebBrowser1.Navigate(selectedFilePath)
        End If

    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        course = handler.LoadCourseDetails(RoomID)
        Dim form As New Ed_Moodle_CourseContent(callingPanel)
        form.CourseContent = course
        Globals.viewChildForm(callingPanel, form)
    End Sub

    Private Sub Ed_Moodle_CourseAss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        course = handler.LoadCourseDetails(RoomID)

        Label1.Text = course.Name
        Label2.Text = content.ContentName
        RichTextBox1.Text = content.Content

    End Sub
End Class