Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Ed_Institute_Handler
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Ed_EcourseApproveCourses
    Dim courseraHandler As New Ed_Coursera_Handler()
    Dim pendingCourses As Ed_Coursera_Handler.Course()

    Private Sub DisplayInst(pendingCourses As Ed_Coursera_Handler.Course())
        ' Clear existing institute items from the FlowLayoutPanel
        FlowLayoutPanel1.Controls.Clear()


        For Each course As Ed_Coursera_Handler.Course In pendingCourses
            Dim approveItem As New Ed_ECourseApproveItem()
            ' Set properties of Ed_ECourseApproveItem control based on course information
            approveItem.CourseID = course.CourseID
            approveItem.CourseItem = course
            approveItem.Label6.Text = course.Name
            approveItem.Label1.Text = course.TeacherName
            approveItem.Label3.Text = course.Fees
            ' Add Ed_ECourseApproveItem control to FlowLayoutPanel
            FlowLayoutPanel1.Controls.Add(approveItem)
        Next
    End Sub
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        pendingCourses = courseraHandler.GetPendingCourses()
        DisplayInst(pendingCourses)


    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim searchText As String = TextBox1.Text.Trim() ' Get the search text from the textbox

        ' Filter institues based on the matching institute  name'
        Dim filteredInstitutes As Ed_Coursera_Handler.Course() = pendingCourses.Where(Function(institute) institute.Name.Contains(searchText)).ToArray()
        DisplayInst(filteredInstitutes)
    End Sub
End Class
