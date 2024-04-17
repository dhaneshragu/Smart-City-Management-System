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
    Public Property AssStatus As Ed_Moodle_Handler.StudentAssRecord

    Dim handler As New Ed_Moodle_Handler()

    Public Sub New(panel As Panel)
        InitializeComponent()
        callingPanel = panel
    End Sub





    Private Sub Label6_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Upload" Then
            Dim openFileDialog1 As New OpenFileDialog()

            openFileDialog1.Filter = "PDF Files|*.pdf"
            openFileDialog1.Title = "Select a PDF File"
            Dim uploadedFileBytes As Byte() = New Byte() {}

            If openFileDialog1.ShowDialog() = DialogResult.OK Then
                Dim filePath As String = openFileDialog1.FileName

                ' Read the file and convert it to byte array
                Try
                    Using fileStream As FileStream = File.OpenRead(filePath)
                        ReDim uploadedFileBytes(fileStream.Length - 1)
                        fileStream.Read(uploadedFileBytes, 0, uploadedFileBytes.Length)
                    End Using

                    MessageBox.Show("PDF file uploaded successfully and converted to bytes.")
                Catch ex As Exception
                    MessageBox.Show("Error reading file: " & ex.Message)
                End Try
            End If
            AssStatus.FileData = uploadedFileBytes

        Else
            Dim button As Button = DirectCast(sender, Button)

            ' Get the certificate data from the button tag
            Dim certificateData As Byte() = DirectCast(AssStatus.FileData, Byte())
            Dim filePath As String = "file.pdf"
            File.WriteAllBytes(filePath, certificateData)
            Using tmp As New FileStream(filePath, FileMode.Create)
                tmp.Write(certificateData, 0, certificateData.Length)
            End Using
            Process.Start(filePath)

        End If



    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        course = handler.LoadCourseDetails(RoomID)
        Dim form As New Ed_Moodle_CourseContent(callingPanel)
        form.CourseContent = course
        Globals.viewChildForm(callingPanel, form)
        Me.Close()
    End Sub

    Private Sub Ed_Moodle_CourseAss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        course = handler.LoadCourseDetails(RoomID)
        handler.AssLoad(content.RoomID, content.SeqNo, Ed_GlobalDashboard.userID)
        AssStatus = handler.AssStatus(Ed_GlobalDashboard.userID, content.RoomID, content.SeqNo)
        Button1.Text = "Turn-In"

        If Not AssStatus.FileData Is Nothing Then
            Button1.Text = "Undo Turn-In"
            Button2.Text = "Download Submission"
        End If

        Label1.Text = course.Name
        Label2.Text = content.ContentName
        RichTextBox1.Rtf = content.Content

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Undo Turn-In" Then
            AssStatus.FileData = Nothing
        End If
        handler.UpdateFileDataAndSubmitTime(AssStatus.Student_ID, AssStatus.Room_ID, AssStatus.Seq_no, AssStatus.FileData)
        Ed_Moodle_CourseAss_Load(sender, e)
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub
End Class