Imports MySql.Data.MySqlClient

Public Class Ed_Stud_ListItem
    Public instituteID As Integer
    Public studentID As Integer
    Public year As Integer
    Public ClassValue As Integer
    Public Sem As Integer
    Private handler As New Ed_Institute_Handler()
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Update the admission status to "Approved"
        If (Button1.Text = "Approve") Then
            UpdateAdmissionStatus(instituteID, studentID, year, "Approved")
            ' Update the affiliation in the ed_profile table
            UpdateAffiliation()
            Button1.Text = "Approved"
            Button2.Hide()
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (Button2.Text = "Reject") Then
            UpdateAdmissionStatus(instituteID, studentID, year, "Rejected")
            Button2.Text = "Rejected"
            Button1.Hide()
        End If
    End Sub

    Private Sub UpdateAdmissionStatus(instituteID As Integer, studentID As Integer, year As Integer, status As String)
        ' Update the admission status in the ed_admission table
        Dim connectionString As String = Globals.getdbConnectionString()
        Dim query As String = "UPDATE ed_admission SET Appr_Status = @Status WHERE Inst_ID = @InstituteID AND Student_ID = @StudentID AND Year = @Year"

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@Status", status)
                command.Parameters.AddWithValue("@InstituteID", instituteID)
                command.Parameters.AddWithValue("@StudentID", studentID)
                command.Parameters.AddWithValue("@Year", year)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub UpdateAffiliation()
        ' Update the affiliation in the ed_profile table
        Dim result = handler.GetLastStudentRequestSemClass(studentID)
        ClassValue = result.classValue
        Sem = result.Semester
        Dim connectionString As String = Globals.getdbConnectionString()
        Dim query As String = "UPDATE ed_profile SET Ed_Affiliation = @InstituteID, Ed_Class = @class, Ed_Sem = @sem WHERE Ed_User_ID = @StudentID"

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@InstituteID", instituteID)
                command.Parameters.AddWithValue("@StudentID", studentID)
                command.Parameters.AddWithValue("@class", ClassValue)
                command.Parameters.AddWithValue("@sem", Sem)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class
