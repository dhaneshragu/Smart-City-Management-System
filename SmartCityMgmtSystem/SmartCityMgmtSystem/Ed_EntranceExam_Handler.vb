Imports MySql.Data.MySqlClient

Public Class Ed_EntranceExam_Handler
    Public Structure EEDetails
        Public Exam_ID As Integer
        Public Name As String
        Public Syllabus As String
        Public Date_Time As Date
        Public Venue_List As String
        Public About As String
        Public Exam_application As String
        Public Exam_results As String
        Public Contact_Info As String
        Public Contact_email As String
        Public Notice_Message As String
    End Structure

    Public Structure EEVenue
        Public EE_Venue_ID As Integer
        Public Name As String
        Public Address As String
        Public Capacity As Integer
    End Structure
    Public Structure EEStudentAdmit
        Public Exam_ID As Integer
        Public Student_ID As Integer
        Public Venue_ID As Integer
        Public Year As Integer
        Public Result_Rank As Integer
        Public Date_Time As Date
        Public Admit_Card As Byte()
        Public Exam_Status As String
        Public ApprvStatus As String
        Public User_Name As String
        Public Phone_Number As String
        Public fee_Status As Boolean
    End Structure

    ' Function to get entrance exam details by examID
    Public Function get_ee_details(examID As Integer) As EEDetails
        Dim details As New EEDetails()

        ' Your MySQL connection string
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to retrieve entrance exam details
        Dim query As String = "SELECT * FROM ee_details WHERE Exam_ID = @ExamID"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@ExamID", examID)
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        details.Exam_ID = Convert.ToInt32(reader("Exam_ID"))
                        details.Name = reader("Name").ToString()
                        details.Syllabus = reader("Syllabus").ToString()
                        details.Date_Time = Convert.ToDateTime(reader("Date_Time"))
                        details.Venue_List = reader("Venue_List").ToString()
                        details.About = reader("About").ToString()
                        details.Exam_application = reader("Exam_application").ToString()
                        details.Exam_results = reader("Exam_results").ToString()
                        details.Contact_Info = reader("Contact_Info").ToString()
                        details.Contact_email = reader("Contact_email").ToString()
                        details.Notice_Message = reader("Notice_Message").ToString()
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Display error message
            MessageBox.Show("Error occurred while retrieving exam details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return details
    End Function
    Public Function get_venue_by_id(venueId As Integer) As EEVenue?
        Dim venue As New EEVenue()

        ' Your MySQL connection string
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to retrieve a venue by ID
        Dim query As String = "SELECT * FROM ee_venues WHERE EE_Venue_ID = @venueId"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@venueId", venueId)
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    ' Check if a venue is found
                    If reader.Read() Then
                        venue = New EEVenue()
                        venue.EE_Venue_ID = Convert.ToInt32(reader("EE_Venue_ID"))
                        venue.Name = reader("Name").ToString()
                        venue.Address = reader("Address").ToString()
                        venue.Capacity = Convert.ToInt32(reader("Capacity"))
                    End If
                    reader.Close()
                End Using
            End Using
        Catch ex As Exception
            ' Display error message (optional)
            ' MessageBox.Show("Error occurred while retrieving venue: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return venue
    End Function

    ' Function to get all venues
    Public Function get_all_venues() As List(Of EEVenue)
        Dim venues As New List(Of EEVenue)()

        ' Your MySQL connection string
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to retrieve all venues
        Dim query As String = "SELECT * FROM ee_venues"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim venue As New EEVenue()
                        venue.EE_Venue_ID = Convert.ToInt32(reader("EE_Venue_ID"))
                        venue.Name = reader("Name").ToString()
                        venue.Address = reader("Address").ToString()
                        venue.Capacity = Convert.ToInt32(reader("Capacity"))
                        venues.Add(venue)
                    End While
                End Using
            End Using
        Catch ex As Exception
            ' Display error message
            MessageBox.Show("Error occurred while retrieving venues: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return venues
    End Function

    ' Function to add a new venue
    Public Sub add_new_venue(name As String, address As String, capacity As Integer)
        ' Your MySQL connection string
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to insert a new venue
        Dim query As String = "INSERT INTO ee_venues (Name, Address, Capacity) VALUES (@Name, @Address, @Capacity)"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Name", name)
                    command.Parameters.AddWithValue("@Address", address)
                    command.Parameters.AddWithValue("@Capacity", capacity)
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("New venue added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            End Using
        Catch ex As Exception
            ' Display error message
            MessageBox.Show("Error occurred while adding new venue: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub drop_venue(name As String, address As String, capacity As Integer)
        ' Your MySQL connection string
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to delete a venue
        Dim query As String = "DELETE FROM ee_venues WHERE Name = @Name AND Address = @Address AND Capacity = @Capacity"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Name", name)
                    command.Parameters.AddWithValue("@Address", address)
                    command.Parameters.AddWithValue("@Capacity", capacity)
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Venue deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            End Using
        Catch ex As Exception
            ' Display error message
            MessageBox.Show("Error occurred while deleting venue: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function extract_pending_student_admits(examID As Integer) As List(Of EEStudentAdmit)
        Dim pendingAdmits As New List(Of EEStudentAdmit)()

        ' Your MySQL connection string
        Dim connectionString As String = Globals.getdbConnectionString()

        ' Get the current year
        Dim currentYear As Integer = DateTime.Now.Year

        ' MySQL query to retrieve pending student admits with user details
        Dim query As String = "SELECT s.*, u.name AS User_Name, u.phone_number FROM ee_studentadmit AS s INNER JOIN users AS u ON s.Student_ID = u.User_ID WHERE s.ApprvStatus = 'pending' AND s.Year = @Year AND s.Exam_Status = 'inprogress' AND s.Exam_ID = @examID"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Year", currentYear)
                    command.Parameters.AddWithValue("@examID", examID)
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim studentAdmit As New EEStudentAdmit()

                        ' Check if the field is not DBNull.Value before converting
                        If Not IsDBNull(reader("Exam_ID")) Then
                            studentAdmit.Exam_ID = Convert.ToInt32(reader("Exam_ID"))
                        End If

                        If Not IsDBNull(reader("Student_ID")) Then
                            studentAdmit.Student_ID = Convert.ToInt32(reader("Student_ID"))
                        End If

                        If Not IsDBNull(reader("Venue_ID")) Then
                            studentAdmit.Venue_ID = Convert.ToInt32(reader("Venue_ID"))
                        End If

                        If Not IsDBNull(reader("Year")) Then
                            studentAdmit.Year = Convert.ToInt32(reader("Year"))
                        End If

                        If Not IsDBNull(reader("Result_Rank")) Then
                            studentAdmit.Result_Rank = Convert.ToInt32(reader("Result_Rank"))
                        End If

                        If Not IsDBNull(reader("Date_Time")) Then
                            studentAdmit.Date_Time = Convert.ToDateTime(reader("Date_Time"))
                        End If

                        If Not IsDBNull(reader("Admit_Card")) Then
                            ' Assuming Admit_Card is a byte array
                            studentAdmit.Admit_Card = DirectCast(reader("Admit_Card"), Byte())
                        End If

                        If Not IsDBNull(reader("Exam_Status")) Then
                            studentAdmit.Exam_Status = reader("Exam_Status").ToString()
                        End If

                        If Not IsDBNull(reader("ApprvStatus")) Then
                            studentAdmit.ApprvStatus = reader("ApprvStatus").ToString()
                        End If

                        ' Retrieve user details
                        If Not IsDBNull(reader("User_Name")) Then
                            studentAdmit.User_Name = reader("User_Name").ToString()
                        End If

                        If Not IsDBNull(reader("Phone_Number")) Then
                            studentAdmit.Phone_Number = reader("phone_number").ToString()
                        End If

                        pendingAdmits.Add(studentAdmit)
                    End While

                End Using
            End Using
        Catch ex As Exception
            ' Display error message
            MessageBox.Show("Error occurred while extracting pending student admits: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return pendingAdmits
    End Function
    Public Sub approve_pending_request(examID As Integer, studentID As Integer, year As Integer)
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to update the status to approved and the exam_status to rejected
        Dim query As String = "UPDATE ee_studentadmit SET ApprvStatus = 'approved' WHERE Exam_ID = @ExamID AND Student_ID = @StudentID AND Year = @Year AND ApprvStatus = 'pending'"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@ExamID", examID)
                    command.Parameters.AddWithValue("@StudentID", studentID)
                    command.Parameters.AddWithValue("@Year", year)
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Pending request approved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error occurred while approving pending request: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub reject_pending_request(examID As Integer, studentID As Integer, year As Integer)
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to update the status to rejected and the exam_status to rejected
        Dim query As String = "UPDATE ee_studentadmit SET ApprvStatus = 'rejected', Exam_Status = 'rejected' WHERE Exam_ID = @ExamID AND Student_ID = @StudentID AND Year = @Year AND ApprvStatus = 'pending'"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@ExamID", examID)
                    command.Parameters.AddWithValue("@StudentID", studentID)
                    command.Parameters.AddWithValue("@Year", year)
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Pending request rejected successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error occurred while rejecting pending request: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub update_ee_details(examID As Integer, details As EEDetails)
        ' Your MySQL connection string
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to update entrance exam details
        Dim query As String = "UPDATE ee_details SET Name = @Name, Syllabus = @Syllabus, Date_Time = @DateTime, Venue_List = @VenueList, About = @About, Exam_application = @ExamApplication, Exam_results = @ExamResults, Contact_Info = @ContactInfo, Contact_email = @ContactEmail, Notice_Message = @NoticeMessage WHERE Exam_ID = @ExamID"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    ' Add parameters
                    command.Parameters.AddWithValue("@Name", details.Name)
                    command.Parameters.AddWithValue("@Syllabus", details.Syllabus)
                    command.Parameters.AddWithValue("@DateTime", details.Date_Time)
                    command.Parameters.AddWithValue("@VenueList", details.Venue_List)
                    command.Parameters.AddWithValue("@About", details.About)
                    command.Parameters.AddWithValue("@ExamApplication", details.Exam_application)
                    command.Parameters.AddWithValue("@ExamResults", details.Exam_results)
                    command.Parameters.AddWithValue("@ContactInfo", details.Contact_Info)
                    command.Parameters.AddWithValue("@ContactEmail", details.Contact_email)
                    command.Parameters.AddWithValue("@NoticeMessage", details.Notice_Message)
                    command.Parameters.AddWithValue("@ExamID", examID)

                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Entrance exam details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            End Using
        Catch ex As Exception
            ' Display error message
            MessageBox.Show("Error occurred while updating entrance exam details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function get_ee_studentadmit(studentID As Integer, examID As Integer, year As Integer) As EEStudentAdmit
        Dim studentAdmit As New EEStudentAdmit()

        ' Your MySQL connection string
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to retrieve student admit details
        Dim query As String = "SELECT * FROM ee_studentadmit WHERE Student_ID = @StudentID AND Exam_ID = @ExamID AND Year = @year"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@StudentID", studentID)
                    command.Parameters.AddWithValue("@ExamID", examID)
                    command.Parameters.AddWithValue("@year", DateTime.Now.Year)
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        ' Check if the field is not DBNull.Value before converting
                        If Not IsDBNull(reader("Exam_ID")) Then
                            studentAdmit.Exam_ID = Convert.ToInt32(reader("Exam_ID"))
                        End If

                        If Not IsDBNull(reader("Student_ID")) Then
                            studentAdmit.Student_ID = Convert.ToInt32(reader("Student_ID"))
                        End If

                        If Not IsDBNull(reader("Venue_ID")) Then
                            studentAdmit.Venue_ID = Convert.ToInt32(reader("Venue_ID"))
                        End If

                        If Not IsDBNull(reader("Year")) Then
                            studentAdmit.Year = Convert.ToInt32(reader("Year"))
                        End If

                        If Not IsDBNull(reader("Result_Rank")) Then
                            studentAdmit.Result_Rank = Convert.ToInt32(reader("Result_Rank"))
                        End If

                        If Not IsDBNull(reader("Date_Time")) Then
                            studentAdmit.Date_Time = Convert.ToDateTime(reader("Date_Time"))
                        End If

                        If Not IsDBNull(reader("Admit_Card")) Then
                            ' Assuming Admit_Card is a byte array
                            studentAdmit.Admit_Card = DirectCast(reader("Admit_Card"), Byte())
                        End If

                        If Not IsDBNull(reader("Exam_Status")) Then
                            studentAdmit.Exam_Status = reader("Exam_Status").ToString()
                        End If

                        If Not IsDBNull(reader("ApprvStatus")) Then
                            studentAdmit.ApprvStatus = reader("ApprvStatus").ToString()
                        End If
                        If Not IsDBNull(reader("feeStatus")) Then
                            studentAdmit.fee_Status = reader("feeStatus").ToString()
                        End If
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Display error message
            MessageBox.Show("Error occurred while retrieving student admit details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return studentAdmit
    End Function

    Public Function get_ee_completed_studentadmit(studentID As Integer, examID As Integer) As EEStudentAdmit
        Dim studentAdmit As New EEStudentAdmit()
        ' Your MySQL connection string
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to retrieve student admit details
        Dim query As String = "SELECT * FROM ee_studentadmit WHERE Student_ID = @StudentID AND Exam_ID = @ExamID AND Exam_Status = 'completed'"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@StudentID", studentID)
                    command.Parameters.AddWithValue("@ExamID", examID)
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        ' Check if the field is not DBNull.Value before converting
                        If Not IsDBNull(reader("Exam_ID")) Then
                            studentAdmit.Exam_ID = Convert.ToInt32(reader("Exam_ID"))
                        End If

                        If Not IsDBNull(reader("Student_ID")) Then
                            studentAdmit.Student_ID = Convert.ToInt32(reader("Student_ID"))
                        End If

                        If Not IsDBNull(reader("Venue_ID")) Then
                            studentAdmit.Venue_ID = Convert.ToInt32(reader("Venue_ID"))
                        End If

                        If Not IsDBNull(reader("Year")) Then
                            studentAdmit.Year = Convert.ToInt32(reader("Year"))
                        End If

                        If Not IsDBNull(reader("Result_Rank")) Then
                            studentAdmit.Result_Rank = Convert.ToInt32(reader("Result_Rank"))
                        End If

                        If Not IsDBNull(reader("Date_Time")) Then
                            studentAdmit.Date_Time = Convert.ToDateTime(reader("Date_Time"))
                        End If

                        If Not IsDBNull(reader("Admit_Card")) Then
                            ' Assuming Admit_Card is a byte array
                            studentAdmit.Admit_Card = DirectCast(reader("Admit_Card"), Byte())
                        End If

                        If Not IsDBNull(reader("Exam_Status")) Then
                            studentAdmit.Exam_Status = reader("Exam_Status").ToString()
                        End If

                        If Not IsDBNull(reader("ApprvStatus")) Then
                            studentAdmit.ApprvStatus = reader("ApprvStatus").ToString()
                        End If
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Display error message
            MessageBox.Show("Error occurred while retrieving student admit details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return studentAdmit
    End Function

    Public Sub AddStudentAdmit(studentID As Integer, examID As Integer, year As Integer)
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to insert a new entry into ee_studentadmit table
        Dim query As String = "INSERT INTO ee_studentadmit (Exam_ID, Student_ID, Year) VALUES (@ExamID, @StudentID, @Year)"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    ' Add parameters
                    command.Parameters.AddWithValue("@ExamID", examID)
                    command.Parameters.AddWithValue("@StudentID", studentID)
                    command.Parameters.AddWithValue("@Year", year)

                    ' Open connection and execute the query
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Application sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            End Using
        Catch ex As Exception
            ' Display error message
            MessageBox.Show("Error occurred while sending the application: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub UpdateFeeStatus(studentID As Integer, examID As Integer, year As Integer)
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to update the fee_Status column to true
        Dim query As String = "UPDATE ee_studentadmit SET feeStatus = 1 WHERE Student_ID = @StudentID AND Exam_ID = @ExamID AND Year = @Year"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    ' Add parameters
                    command.Parameters.AddWithValue("@StudentID", studentID)
                    command.Parameters.AddWithValue("@ExamID", examID)
                    command.Parameters.AddWithValue("@Year", year)

                    ' Open connection and execute the query
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Fee paid successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            End Using
        Catch ex As Exception
            ' Display error message
            MessageBox.Show("Error occurred while paying the fee: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Function GetUserPassword(userID As Integer) As String
        Dim password As String = String.Empty

        ' Your MySQL connection string
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to retrieve the password of the user
        Dim query As String = "SELECT password FROM users WHERE user_ID = @UserID"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    ' Add the parameter for userID
                    command.Parameters.AddWithValue("@UserID", userID)

                    ' Open connection and execute the query
                    connection.Open()
                    Dim result As Object = command.ExecuteScalar()

                    ' Check if the result is not null and convert it to string
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        password = result.ToString()
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Display error message
            MessageBox.Show("Error occurred while retrieving user password: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return password
    End Function

    Public Function get_user_picture(userId As Integer) As Byte()
        Dim pictureData As Byte() = Nothing ' Initialize as null

        ' Replace "your_connection_string" with your actual connection string
        Dim connectionString As String = Globals.getdbConnectionString()

        ' MySQL query to retrieve picture for a specific user
        Dim query As String = "SELECT profile_photo FROM users WHERE user_id = @userId"

        Try
            Using connection As New MySqlConnection(connectionString)
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@userId", userId)
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    ' Check if a user is found
                    If reader.Read() Then
                        ' Check if profile_photo is not null
                        If Not reader.IsDBNull(0) Then
                            Dim bufferSize As Integer = Convert.ToInt32(reader.GetBytes(0, 0, Nothing, 0, Int32.MaxValue))
                            pictureData = New Byte(bufferSize - 1) {}
                            reader.GetBytes(0, 0, pictureData, 0, bufferSize)
                        End If
                    End If
                    reader.Close()
                End Using
            End Using
        Catch ex As Exception
            ' Handle potential exceptions (optional)
        End Try

        Return pictureData
    End Function
    Public Function GetLowestUnusedVenueAndUpdateCapacity() As EEVenue
        Dim venue As New EEVenue()

        Dim connectionString As String = Globals.getdbConnectionString()
        Dim query As String = "SELECT * FROM ee_venues WHERE Capacity > UsedCapacity ORDER BY EE_Venue_ID ASC LIMIT 1"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                connection.Open()
                Dim reader As MySqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    venue.EE_Venue_ID = Convert.ToInt32(reader("EE_Venue_ID"))
                    venue.Name = reader("Name").ToString()
                    venue.Address = reader("Address").ToString()
                    venue.Capacity = Convert.ToInt32(reader("Capacity"))

                    ' Update UsedCapacity by 1
                    Dim updateQuery As String = "UPDATE ee_venues SET UsedCapacity = UsedCapacity + 1 WHERE EE_Venue_ID = @venueID"
                    reader.Close()
                    command.CommandText = updateQuery
                    command.Parameters.AddWithValue("@venueID", venue.EE_Venue_ID)
                    command.ExecuteNonQuery()
                End If
            End Using
        End Using

        Return venue
    End Function
    Public Sub UpdateExamVenueID(ByVal studentID As Integer, ByVal examID As Integer, ByVal year As Integer, ByVal newVenueID As Integer)
        Dim connectionString As String = Globals.getdbConnectionString()
        Dim query As String = "UPDATE ee_studentadmit SET Venue_ID = @newVenueID WHERE Student_ID = @studentID AND Exam_ID = @examID AND Year = @year"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@newVenueID", newVenueID)
                command.Parameters.AddWithValue("@studentID", studentID)
                command.Parameters.AddWithValue("@examID", examID)
                command.Parameters.AddWithValue("@year", year)

                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class
