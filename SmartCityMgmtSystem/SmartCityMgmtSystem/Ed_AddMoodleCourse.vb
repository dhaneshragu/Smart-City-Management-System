Imports System.Data.SqlClient
Public Class Ed_AddMoodleCourse
    Dim handler As New Ed_Moodle_Handler()
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim passkey As String = TextBox2.Text
        Dim year As String = TextBox3.Text

        Dim class_sem As String = TextBox6.Text
        Dim name As String = TextBox7.Text

        Dim teacherID As Integer = Ed_GlobalDashboard.Ed_Profile.Ed_User_ID
        Dim instID As Integer = Ed_GlobalDashboard.Ed_Profile.Ed_Affiliation

        Dim roomID As Integer = handler.AddCourse(passkey, teacherID, instID, year, class_sem, class_sem, name)


        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""

        MessageBox.Show("Successfully Added a Course with RoomID : " & roomID)


    End Sub
End Class