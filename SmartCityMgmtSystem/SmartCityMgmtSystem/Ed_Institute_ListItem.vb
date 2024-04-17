Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient


Public Class Ed_Institute_ListItem
    Public instituteID As Integer
    Private SemOrClass As Boolean
    Private Value As Integer
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Get the student ID
        Dim studentID As Integer = Ed_GlobalDashboard.Ed_Profile.Ed_User_ID

        ' Check if the student has already applied to this institute
        If StudentAlreadyApplied(studentID) Then
            MessageBox.Show("You have already applied to some institute.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            ' Apply for the institute
            Dim admPopUp As New Ed_admissionPopUp()
            admPopUp.ShowDialog()
            If (admPopUp.DialogResult = DialogResult.OK) Then
                SemOrClass = admPopUp.SemOrClass
                Value = admPopUp.Value
                If (SemOrClass) Then
                    Ed_GlobalDashboard.Ed_Profile.Ed_Sem = Value
                    Ed_GlobalDashboard.Ed_Profile.Ed_Class = 0
                Else
                    Ed_GlobalDashboard.Ed_Profile.Ed_Class = Value
                    Ed_GlobalDashboard.Ed_Profile.Ed_Sem = 0
                End If
                ApplyToInstitute(studentID, instituteID)
                ' Change the button text
                Button2.Text = "Application Sent"
                Button2.BackColor = Color.FromArgb(153, 102, 0)
            End If
        End If
    End Sub
    Private Function StudentAlreadyApplied(studentID As Integer) As Boolean
        Dim connectionString As String = GetDBConnectionString()
        Dim query As String = "SELECT COUNT(*) FROM ed_admission WHERE Student_ID = @StudentID AND Appr_Status = 'Pending'"

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@StudentID", studentID)
                Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function

    Private Sub ApplyToInstitute(studentID As Integer, instituteID As Integer)
        Dim connectionString As String = GetDBConnectionString()
        Dim query As String = "INSERT INTO ed_admission (Inst_ID, Student_ID, Date, Class, Sem, Year, Appr_Status) VALUES (@InstituteID, @StudentID, CURDATE(), @Class, @Sem, @Year, 'Pending')"

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@InstituteID", instituteID)
                command.Parameters.AddWithValue("@StudentID", studentID)
                command.Parameters.AddWithValue("@Class", Ed_GlobalDashboard.Ed_Profile.Ed_Class) ' Update with actual class value
                command.Parameters.AddWithValue("@Sem", Ed_GlobalDashboard.Ed_Profile.Ed_Sem) ' Update with actual semester value
                command.Parameters.AddWithValue("@Year", DateTime.Now.Year)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Function GetDBConnectionString() As String
        ' Call the method from Globals.vb to get the connection string
        Return Globals.getdbConnectionString()
    End Function

    Private Sub Ed_Institute_ListItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
