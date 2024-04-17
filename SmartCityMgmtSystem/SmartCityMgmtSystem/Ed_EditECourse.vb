Imports System.Data.SqlClient

Public Class Ed_EditECourse


    Public CourseItem As Ed_Coursera_Handler.Course
    Dim handler As New Ed_Coursera_Handler()
    Public Property summary As String = ""
    Private callingPanel As Panel
    Public Sub New(panel As Panel)
        InitializeComponent()
        callingPanel = panel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim enterSumForm As New Ed_Teacher_AddSummary()
        enterSumForm.summary = summary
        enterSumForm.ShowDialog() ' Show as dialog if needed
        summary = enterSumForm.summary
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click


        Dim form As New Ed_Teacher_Coursera_Course_Content(callingPanel)
        form.CourseItem = CourseItem
        Globals.viewChildForm(callingPanel, form)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        Dim courseName As String = TextBox2.Text
        Dim fees As Integer = Integer.Parse(TextBox5.Text)
        Dim category As String = ComboBox1.Text
        Dim introVidLink As String = TextBox7.Text
        Dim summary As String = Me.summary


        handler.UpdateCourse(CourseItem.CourseID, courseName, category, summary, introVidLink, fees)


        MessageBox.Show("Successfully Updated the Course!")



        Dim form As New Ed_Teacher_Coursera_Course_Content(callingPanel)
        form.CourseItem = handler.GetCourseDetails(CourseItem.CourseID)
        Globals.viewChildForm(callingPanel, form)
        Me.Close()
    End Sub

    Private Sub Ed_EditECourse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Retrieve textbox values from the course item'
        TextBox2.Text = CourseItem.Name
        ComboBox1.Text = CourseItem.Category
        TextBox5.Text = CourseItem.Fees
        TextBox7.Text = CourseItem.IntroVideoLink
        Me.summary = CourseItem.Syllabus



    End Sub
End Class