Imports System.Data.SqlClient
Public Class Ed_Teacher_EditResource

    Private newFont As Font = New Font("Arial", 12)
    Private newFontColor As Color = Color.Black
    Public content As Ed_Coursera_Handler.CourseContent
    Public CourseItem As Ed_Coursera_Handler.Course
    Dim handler As New Ed_Coursera_Handler()

    'Constructor that takes content as a parametr'
    Public Sub New(content As Ed_Coursera_Handler.CourseContent)
        InitializeComponent()
        Me.content = content
    End Sub

    Private Sub btnFont_Click(sender As Object, e As EventArgs) Handles btnFont.Click
        ' Open font dialog to select font
        Dim fontDialog As New FontDialog()
        If fontDialog.ShowDialog() = DialogResult.OK Then
            newFont = fontDialog.Font
        End If
    End Sub

    Private Sub btnFontColor_Click(sender As Object, e As EventArgs) Handles btnFontColor.Click
        ' Open color dialog to select font color
        Dim colorDialog As New ColorDialog()
        If colorDialog.ShowDialog() = DialogResult.OK Then
            newFontColor = colorDialog.Color
        End If
    End Sub

    Private Sub RichTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles RichTextBox1.KeyDown
        ' Apply font and color to newly typed text
        Dim startIndex As Integer = RichTextBox1.SelectionStart
        Dim length As Integer = RichTextBox1.SelectionLength
        RichTextBox1.SelectionFont = newFont
        RichTextBox1.SelectionColor = newFontColor
        RichTextBox1.Select(startIndex, length)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Update CourseContent Details by calling handler updatecoursecontent'
        Dim resourceName As String = TextBox2.Text
        Dim videoLink As String = TextBox1.Text
        Dim textContent As String = RichTextBox1.Rtf

        handler.UpdateCourseContent(Me.content.CourseID, Me.content.SeqNo, resourceName, "Resource", videoLink, textContent)

        Me.Close()

        Dim form As New Ed_Teacher_Course_Resource(Ed_GlobalDashboard.innerpanel, "Coursera")

        form.CourseItem = CourseItem
        form.content = handler.GetCourseContent(Me.content.CourseID, Me.content.SeqNo)
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, form)


    End Sub

    Private Sub Ed_Teacher_EditResource_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Load the content of the course content'
        Me.AutoScroll = True
        RichTextBox1.Rtf = content.Content
        TextBox1.Text = content.VideoLink
        TextBox2.Text = content.ContentName


    End Sub
End Class