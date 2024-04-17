Imports System.Data.SqlClient
Public Class Ed_AddECourse
    Private callingPanel As Panel
    Public Property summary As String = ""
    Dim handler As New Ed_Coursera_Handler()
    Public Sub New(panel As Panel)
        InitializeComponent()
        callingPanel = panel
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim enterSumForm As New Ed_Teacher_AddSummary()
        enterSumForm.ShowDialog() ' Show as dialog if needed
        summary = enterSumForm.summary

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' calling handler's AddCourse method'

        Dim teacherID As Integer = Ed_GlobalDashboard.Ed_Profile.Ed_User_ID
        Dim affiliation As Integer = Ed_GlobalDashboard.Ed_Profile.Ed_Affiliation
        Dim courseName As String = TextBox2.Text
        Dim teacherName As String = Ed_GlobalDashboard.Ed_Profile.Ed_Name
        Dim fees As Integer = Integer.Parse(TextBox5.Text)
        Dim category As String = ComboBox1.Text
        Dim introVidLink As String = TextBox7.Text
        Dim summary As String = Me.summary


        handler.AddCourse(affiliation, courseName, category, teacherName, teacherID, summary, introVidLink, "Pending", fees)


        'Empty all textboxes'
        TextBox2.Text = ""
        ComboBox1.Text = ""
        TextBox5.Text = ""
        TextBox7.Text = ""
        Me.summary = ""

        MessageBox.Show("Successfully Added a Course!")



    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Ed_AddECourse_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox6_TextChanged_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs)

    End Sub


End Class