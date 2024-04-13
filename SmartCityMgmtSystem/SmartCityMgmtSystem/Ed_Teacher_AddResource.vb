Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Ed_Coursera_Handler


Public Class Ed_Teacher_AddResource


    Private newFont As Font = New Font("Arial", 12)
    Private newFontColor As Color = Color.Black


    Public CourseItem As Ed_Coursera_Handler.Course
    Dim handler As New Ed_Coursera_Handler()



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
        'Retrieve content from textboxes and call handler to add resource'
        Dim resourceName As String = TextBox2.Text
        Dim videoLink As String = TextBox1.Text
        Dim textContent As String = RichTextBox1.Rtf


        handler.AddCourseContent(CourseItem.CourseID, resourceName, "Resource", videoLink, textContent)


        Me.Close()
        Dim form As New Ed_Teacher_Coursera_Course_Content(Ed_GlobalDashboard.innerpanel)
        form.CourseItem = CourseItem
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, form)

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub
End Class