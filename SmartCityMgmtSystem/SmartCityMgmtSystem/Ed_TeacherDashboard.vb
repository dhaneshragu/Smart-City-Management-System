Public Class Ed_TeacherDashboard
    Private currentlyOpenChildForm As Form = Nothing

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        CloseCurrentChildForm()
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(New Ed_RoleSelect())
        Me.Close()
    End Sub

    Private Sub Ed_TeacherDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Ed_GlobalDashboard.Ed_Profile.Ed_Name
        currentlyOpenChildForm = New Ed_ManageECourse()
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        CloseCurrentChildForm()
        currentlyOpenChildForm = New Ed_AddECourse(childformPanel)
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        CloseCurrentChildForm()
        currentlyOpenChildForm = New Ed_ManageECourse()
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        CloseCurrentChildForm()
        currentlyOpenChildForm = New Ed_AddMoodleCourse()
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        CloseCurrentChildForm()
        Dim cur_course As New Ed_Teacher_Moodle_CourseList()
        cur_course.Cur_All = "Current"
        currentlyOpenChildForm = cur_course
        Globals.viewChildForm(childformPanel, currentlyOpenChildForm)
    End Sub
    Private Sub CloseCurrentChildForm()
        ' Check if there's a currently open child form
        If currentlyOpenChildForm IsNot Nothing Then
            ' Close the currently open child form
            currentlyOpenChildForm.Close()
            currentlyOpenChildForm = Nothing
        End If
    End Sub
End Class
