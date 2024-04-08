Imports System.Data.SqlClient
Public Class Ed_Stud_Moodle_JoinCourse
    Dim handler As New Ed_Moodle_Handler()
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Get the studentId from Ed_Globaldashboard.userid
        Dim studentId As Integer = Ed_GlobalDashboard.userID ' Assuming Ed_Globaldashboard.userid holds the student ID

        ' Get the roomId and passKey from TextBox1 and TextBox2
        Dim roomId As Integer
        Dim passKey As String

        ' Validate and parse roomId
        If Not Integer.TryParse(TextBox1.Text, roomId) Then
            MessageBox.Show("Invalid Room ID. Please enter a valid numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Validate passKey
        passKey = TextBox2.Text ' Assuming pass key is directly taken as string from TextBox2

        ' Call the JoinCourse function with the provided parameters
        handler.JoinCourse(roomId, passKey, studentId)
    End Sub

End Class