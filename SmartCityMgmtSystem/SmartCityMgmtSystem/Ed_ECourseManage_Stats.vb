Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports SmartCityMgmtSystem.Ed_Coursera_Handler
Public Class Ed_ECourseManage_Stats
    Dim handler As New Ed_Coursera_Handler()
    Dim courses As Course()
    Private Sub DisplayInst(courses As Ed_Coursera_Handler.Course())
        ' Clear existing institute items from the FlowLayoutPanel
        FlowLayoutPanel1.Controls.Clear()


        Dim labels As Ed_ECourseStatDropItem() = New Ed_ECourseStatDropItem(courses.Length - 1) {}

        For i As Integer = 0 To courses.Length - 1
            labels(i) = New Ed_ECourseStatDropItem()
            labels(i).CourseID = courses(i).CourseID
            labels(i).Label6.Text = courses(i).Name ' Set course name
            labels(i).Label1.Text = courses(i).TeacherName ' Set instructor name
            labels(i).Label2.Text = courses(i).Institution ' Set affiliation
            labels(i).Label4.Text = courses(i).Rating.ToString() ' Set rating
            labels(i).Label8.Text = courses(i).EnrolledStudents
            labels(i).Label7.Text = courses(i).CompletedStudents
        Next
        For Each edCourseListItem As Ed_ECourseStatDropItem In labels
            FlowLayoutPanel1.Controls.Add(edCourseListItem)
        Next
    End Sub
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        courses = handler.GetApprovedCoursesWithCounts()

        DisplayInst(courses)

    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim searchText As String = TextBox1.Text.Trim() ' Get the search text from the textbox

        ' Filter institues based on the matching institute  name'
        Dim filteredInstitutes As Ed_Coursera_Handler.Course() = courses.Where(Function(institute) institute.Name.Contains(searchText)).ToArray()
        DisplayInst(filteredInstitutes)
    End Sub


End Class