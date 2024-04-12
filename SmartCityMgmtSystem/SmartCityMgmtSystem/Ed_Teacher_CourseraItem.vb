Public Class Ed_Teacher_CourseraItem



    Public CourseID As Integer
    Public CourseItem As Ed_Coursera_Handler.Course


    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim form As New Ed_Teacher_Coursera_Course_Content(Ed_GlobalDashboard.innerpanel)
        form.CourseItem = CourseItem
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, form)
    End Sub
End Class
