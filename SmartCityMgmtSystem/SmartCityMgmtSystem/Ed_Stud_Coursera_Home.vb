Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Ed_Coursera_Handler
Public Class Ed_Stud_Coursera_Home
    Dim handler As New Ed_Coursera_Handler()
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim courses As Course() = handler.GetCourses()

        ' Create Ed_Coursera_Course_ListItem objects and set properties
        Dim labels As Ed_Coursera_Course_ListItem() = New Ed_Coursera_Course_ListItem(courses.Length - 1) {}

        For i As Integer = 0 To courses.Length - 1
            labels(i) = New Ed_Coursera_Course_ListItem()
            labels(i).CourseID = courses(i).CourseID
            labels(i).Label6.Text = courses(i).Name ' Set course name
            labels(i).Label1.Text = courses(i).TeacherName ' Set instructor name
            labels(i).Label2.Text = courses(i).Institution ' Set affiliation
            labels(i).Label4.Text = courses(i).Rating.ToString() ' Set rating
            labels(i).Label3.Text = courses(i).Fees.ToString() ' Set fees
            labels(i).CourseItem = courses(i)
        Next

        ' Add Ed_Coursera_Course_ListItem objects to the FlowLayoutPanel
        For Each edCourseListItem As Ed_Coursera_Course_ListItem In labels
            FlowLayoutPanel1.Controls.Add(edCourseListItem)
        Next
    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub

    Private Sub Label6_Click_1(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub
End Class