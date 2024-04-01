Public Class Ed_TeacherDashboard
    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Ed_GlobalDashboard.OpenFormInGlobalEdPanel(Ed_RoleSelect)
    End Sub

    Private Sub Ed_TeacherDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Globals.viewChildForm(childformPanel, Ed_ManageECourse)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Globals.viewChildForm(childformPanel, New Ed_AddECourse(childformPanel))
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Globals.viewChildForm(childformPanel, Ed_ManageECourse)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Globals.viewChildForm(childformPanel, Ed_AddMoodleCourse)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Dim cur_course As New Ed_Teacher_Moodle_CourseList()
        cur_course.Cur_All = "Current"
        Globals.viewChildForm(childformPanel, cur_course)

    End Sub
End Class
