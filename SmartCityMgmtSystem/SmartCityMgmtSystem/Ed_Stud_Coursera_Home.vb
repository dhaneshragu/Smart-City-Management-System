Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Ed_Coursera_Handler
Public Class Ed_Stud_Coursera_Home
    Dim handler As New Ed_Coursera_Handler()
    Dim courses As Course() = handler.GetCourses()
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        DisplayCourses(courses)
    End Sub
    Private Sub DisplayCourses(coursesToDisplay As Course())
        ' Clear existing course items from the FlowLayoutPanel
        FlowLayoutPanel1.Controls.Clear()

        ' Create Ed_Coursera_Course_ListItem objects and set properties
        Dim labels As Ed_Coursera_Course_ListItem() = New Ed_Coursera_Course_ListItem(coursesToDisplay.Length - 1) {}

        For i As Integer = 0 To coursesToDisplay.Length - 1
            labels(i) = New Ed_Coursera_Course_ListItem()
            labels(i).CourseID = coursesToDisplay(i).CourseID
            labels(i).Label6.Text = coursesToDisplay(i).Name ' Set course name
            labels(i).Label1.Text = coursesToDisplay(i).TeacherName ' Set instructor name
            labels(i).Label2.Text = coursesToDisplay(i).Institution ' Set affiliation
            labels(i).Label4.Text = coursesToDisplay(i).Rating.ToString() ' Set rating
            labels(i).Label3.Text = coursesToDisplay(i).Fees.ToString() ' Set fees
            labels(i).CourseItem = coursesToDisplay(i)
        Next

        ' Add Ed_Coursera_Course_ListItem objects to the FlowLayoutPanel
        For Each edCourseListItem As Ed_Coursera_Course_ListItem In labels
            FlowLayoutPanel1.Controls.Add(edCourseListItem)
        Next
    End Sub
    Private Function FilterCoursesByCategory(coursesToFilter As Course(), category As String) As Course()
        ' Filter courses based on the selected category
        Return coursesToFilter.Where(Function(course) course.Category = category).ToArray()
    End Function


    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub

    Private Sub Label_Click(sender As Object, e As EventArgs) Handles Label4.Click, Label5.Click, Label6.Click, Label7.Click, Label8.Click, Label9.Click, Label12.Click, Label13.Click, Label15.Click, Label16.Click, Label17.Click
        Dim label As Label = DirectCast(sender, Label) ' Get the label that was clicked
        Dim category As String = label.Text ' Get the category from the label text

        ' Filter courses based on the selected category
        Dim filteredCourses As Course() = FilterCoursesByCategory(courses, category)

        ' Display the filtered courses
        DisplayCourses(filteredCourses)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim searchText As String = TextBox1.Text.Trim() ' Get the search text from the textbox

        ' Filter courses based on the matching course name
        Dim filteredCourses As Course() = courses.Where(Function(course) course.Name.Contains(searchText)).ToArray()
        DisplayCourses(filteredCourses)
    End Sub
End Class