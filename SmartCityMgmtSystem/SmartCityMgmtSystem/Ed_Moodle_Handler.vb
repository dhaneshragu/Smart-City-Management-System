﻿Imports MySql.Data.MySqlClient
Imports SmartCityMgmtSystem.Ed_Coursera_Handler

Public Class Ed_Moodle_Handler
    Public Class MoodleCourse
        Public Property RoomID As Integer
        Public Property PassKey As String
        Public Property ProfID As Integer
        Public Property InstID As Integer
        Public Property Year As Integer
        Public Property mClass As Integer
        Public Property mSem As Integer
        Public Property Name As String
        Public Property ProfName As String
        Public Property InstName As String
    End Class

    Public Sub JoinCourse(ByVal roomId As Integer, ByVal passKey As String, ByVal studentId As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        ' Check if the pass key is valid for the given room ID
        Dim queryPassKey As String = "SELECT Pass_Key FROM moodle_course WHERE Room_ID = @roomId"
        Dim cmdPassKey As New MySqlCommand(queryPassKey, Con)
        cmdPassKey.Parameters.AddWithValue("@roomId", roomId)
        Dim passKeyResult As Object = cmdPassKey.ExecuteScalar()

        If passKeyResult IsNot Nothing AndAlso passKeyResult.ToString() = passKey Then
            ' Pass key is valid, so insert the enrollment entry
            Dim queryInsert As String = "INSERT INTO moodle_enrollments (Room_ID, Student_ID) VALUES (@roomId, @studentId)"
            Dim cmdInsert As New MySqlCommand(queryInsert, Con)
            cmdInsert.Parameters.AddWithValue("@roomId", roomId)
            cmdInsert.Parameters.AddWithValue("@studentId", studentId)
            cmdInsert.ExecuteNonQuery()
            MessageBox.Show("Student successfully enrolled in the course.", "Enrollment Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Invalid pass key for the given room ID.", "Enrollment Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        Con.Close()
    End Sub

    Public Function GetStudentCourses(ByVal studentId As Integer) As MoodleCourse()
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim courses As New List(Of MoodleCourse)()

        Dim query As String = "SELECT mc.* FROM moodle_course mc INNER JOIN moodle_enrollments me ON mc.Room_ID = me.Room_ID WHERE me.Student_ID = @studentId"

        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@studentId", studentId)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim course As New MoodleCourse()

                    course.RoomID = If(reader("Room_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Room_ID")), 0)
                    course.PassKey = If(reader("Pass_Key") IsNot DBNull.Value, reader("Pass_Key").ToString(), "")
                    course.ProfID = If(reader("Prof_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Prof_ID")), 0)
                    course.InstID = If(reader("Inst_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Inst_ID")), 0)
                    course.Year = If(reader("Year") IsNot DBNull.Value, Convert.ToInt32(reader("Year")), 0)
                    course.mClass = If(reader("Class") IsNot DBNull.Value, Convert.ToInt32(reader("Class")), 0)
                    course.mSem = If(reader("Sem") IsNot DBNull.Value, Convert.ToInt32(reader("Sem")), 0)
                    course.Name = If(reader("Name") IsNot DBNull.Value, reader("Name").ToString(), "")

                    courses.Add(course)
                End While
            End Using
        End Using

        Con.Close()

        Return courses.ToArray()
    End Function

    Public Function GetCurrentYearCourses(ByVal studentId As Integer) As MoodleCourse()
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim courses As New List(Of MoodleCourse)()

        Dim query As String = "SELECT mc.* FROM moodle_course mc " &
                          "INNER JOIN moodle_enrollments me ON mc.Room_ID = me.Room_ID " &
                          "WHERE me.Student_ID = @studentId AND YEAR(mc.Year) = YEAR(CURDATE())"

        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@studentId", studentId)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim course As New MoodleCourse()

                    course.RoomID = If(reader("Room_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Room_ID")), 0)
                    course.PassKey = If(reader("Pass_Key") IsNot DBNull.Value, reader("Pass_Key").ToString(), "")
                    course.ProfID = If(reader("Prof_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Prof_ID")), 0)
                    course.InstID = If(reader("Inst_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Inst_ID")), 0)
                    course.Year = If(reader("Year") IsNot DBNull.Value, Convert.ToInt32(reader("Year")), 0)
                    course.mClass = If(reader("Class") IsNot DBNull.Value, Convert.ToInt32(reader("Class")), 0)
                    course.mSem = If(reader("Sem") IsNot DBNull.Value, Convert.ToInt32(reader("Sem")), 0)
                    course.Name = If(reader("Name") IsNot DBNull.Value, reader("Name").ToString(), "")

                    courses.Add(course)
                End While
            End Using
        End Using

        Con.Close()

        Return courses.ToArray()
    End Function

    Public Class RoomContent
        Public Property RoomID As Integer
        Public Property ContentName As String
        Public Property ContentType As String
        Public Property VideoLink As String
        Public Property Content As String
        Public Property SeqNo As Integer
    End Class

    Public Function GetRoomContents(ByVal roomID As Integer) As RoomContent()
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim contents As New List(Of RoomContent)()

        Dim query As String = "SELECT * FROM moodle_coursecontent WHERE Room_ID = @roomID"

        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@roomID", roomID)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim content As New RoomContent()

                    content.RoomID = If(reader("Room_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Room_ID")), 0)
                    content.ContentName = If(reader("Content_Name") IsNot DBNull.Value, reader("Content_Name").ToString(), "")
                    content.ContentType = If(reader("Content_Type") IsNot DBNull.Value, reader("Content_Type").ToString(), "")
                    content.VideoLink = If(reader("Video_Link") IsNot DBNull.Value, reader("Video_Link").ToString(), "")
                    content.Content = If(reader("Content") IsNot DBNull.Value, reader("Content").ToString(), "")
                    content.SeqNo = If(reader("Seq_no") IsNot DBNull.Value, Convert.ToInt32(reader("Seq_no")), 0)

                    contents.Add(content)
                End While
            End Using
        End Using

        Con.Close()

        Return contents.ToArray()
    End Function

    Public Function LoadCourseDetails(ByVal roomID As Integer) As MoodleCourse
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim course As New MoodleCourse()

        Dim query As String = "SELECT mc.*, ep.Ed_Name, ei.Inst_Name FROM moodle_course mc " &
                          "INNER JOIN ed_profile ep ON ep.Ed_User_ID = mc.Prof_ID " &
                          "INNER JOIN ed_institution ei ON ei.Inst_ID = mc.Inst_ID " &
                          "WHERE mc.Room_ID = @roomID"
        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@roomID", roomID)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    course.RoomID = If(reader("Room_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Room_ID")), 0)
                    course.PassKey = If(reader("Pass_Key") IsNot DBNull.Value, reader("Pass_Key").ToString(), "")
                    course.ProfID = If(reader("Prof_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Prof_ID")), 0)
                    course.InstID = If(reader("Inst_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Inst_ID")), 0)
                    course.Year = If(reader("Year") IsNot DBNull.Value, Convert.ToInt32(reader("Year")), 0)
                    course.mClass = If(reader("Class") IsNot DBNull.Value, Convert.ToInt32(reader("Class")), 0)
                    course.mSem = If(reader("Sem") IsNot DBNull.Value, Convert.ToInt32(reader("Sem")), 0)
                    course.Name = If(reader("Name") IsNot DBNull.Value, reader("Name").ToString(), "")
                    course.ProfName = If(reader("Ed_Name") IsNot DBNull.Value, reader("Ed_Name").ToString(), "")
                    course.InstName = If(reader("Inst_Name") IsNot DBNull.Value, reader("Inst_Name").ToString(), "")
                End If
            End Using
        End Using

        Con.Close()

        Return course
    End Function

    Public Function GetEnrolledAssignments(ByVal studentID As Integer) As RoomContent()
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim assignments As New List(Of RoomContent)()

        ' Query to fetch assignments of courses the student is enrolled in
        Dim query As String = "SELECT mc.Room_ID, mc.Content_Name, mc.Content_Type, mc.Video_Link, mc.Content, mc.Seq_no " &
                          "FROM moodle_coursecontent mc " &
                          "INNER JOIN moodle_enrollments ms ON mc.Room_ID = ms.Room_ID " &
                          "WHERE ms.Student_ID = @studentID AND mc.Content_Type = 'Assignment'"

        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@studentID", studentID)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim assignment As New RoomContent()
                    assignment.RoomID = If(reader("Room_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Room_ID")), 0)
                    assignment.ContentName = If(reader("Content_Name") IsNot DBNull.Value, reader("Content_Name").ToString(), "")
                    assignment.ContentType = If(reader("Content_Type") IsNot DBNull.Value, reader("Content_Type").ToString(), "")
                    assignment.VideoLink = If(reader("Video_Link") IsNot DBNull.Value, reader("Video_Link").ToString(), "")
                    assignment.Content = If(reader("Content") IsNot DBNull.Value, reader("Content").ToString(), "")
                    assignment.SeqNo = If(reader("Seq_no") IsNot DBNull.Value, Convert.ToInt32(reader("Seq_no")), 0)

                    assignments.Add(assignment)
                End While
            End Using
        End Using

        Con.Close()

        Return assignments.ToArray()
    End Function

    Public Sub AssLoad(roomid As Integer, seno As Integer, stuid As Integer)
        ' SQL query to insert data into the database
        Using Con = Globals.GetDBConnection()
            Con.Open()
            Dim query As String = "INSERT INTO moodle_studentcourse (Room_ID, Seq_no, Student_ID) " &
                     "SELECT @roomid, @seqno, @Stuid " &
                     "FROM DUAL " &
                     "WHERE NOT EXISTS ( " &
                     "    SELECT 1 " &
                     "    FROM moodle_studentcourse " &
                     "    WHERE Room_ID = @roomid " &
                     "    AND Seq_no = @seqno " &
                     "    AND Student_ID = @Stuid " &
                     ");"


            ' Create a MySqlCommand object with the SQL query and connection
            Using command As New MySqlCommand(query, Con)
                ' Add parameters to the command
                command.Parameters.AddWithValue("@roomid", roomid)
                command.Parameters.AddWithValue("@seqno", seno)
                command.Parameters.AddWithValue("@Stuid", stuid)
                Dim rowsAffected As Integer = command.ExecuteNonQuery()

            End Using
        End Using
    End Sub

    Public Class StudentAssRecord
        Public Property Room_ID As Integer
        Public Property Seq_no As Integer
        Public Property Student_ID As Integer
        Public Property FileData As Byte()
        Public Property Submit_Time As Date
        Public Property Marks As Integer

        Public Sub New(ByVal roomID As Integer, ByVal seqNo As Integer, ByVal studentID As Integer, ByVal fileData As Byte(), ByVal submitTime As DateTime, ByVal marks As Integer)
            Me.Room_ID = roomID
            Me.Seq_no = seqNo
            Me.Student_ID = studentID
            Me.FileData = fileData
            Me.Submit_Time = submitTime
            Me.Marks = marks
        End Sub
        Private Sub New()
        End Sub

    End Class

    Public Function AssStatus(ByVal studentID As Integer, ByVal Roomid As Integer, ByVal seno As Integer) As StudentAssRecord
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim submitTime As New DateTime(2024, 4, 14, 12, 30, 0)

        Dim AssRec As New StudentAssRecord(-1, -1, -1, Nothing, submitTime, -1)

        Dim query As String = "SELECT * FROM moodle_studentcourse WHERE Room_ID = @roomid AND Seq_no = @seqno AND Student_ID = @Stuid"
        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@roomid", Roomid)
            cmd.Parameters.AddWithValue("@seqno", seno)
            cmd.Parameters.AddWithValue("@Stuid", studentID)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    AssRec.Room_ID = If(reader.IsDBNull(reader.GetOrdinal("Room_ID")), -1, Convert.ToInt32(reader("Room_ID")))
                    AssRec.Seq_no = If(reader.IsDBNull(reader.GetOrdinal("Seq_no")), -1, Convert.ToInt32(reader("Seq_no")))
                    AssRec.Student_ID = If(reader.IsDBNull(reader.GetOrdinal("Student_ID")), -1, Convert.ToInt32(reader("Student_ID")))
                    AssRec.FileData = If(reader.IsDBNull(reader.GetOrdinal("File")), Nothing, DirectCast(reader("File"), Byte()))
                    AssRec.Submit_Time = If(reader.IsDBNull(reader.GetOrdinal("Submit_Time")), DateTime.MinValue, Convert.ToDateTime(reader("Submit_Time")))
                    AssRec.Marks = If(reader.IsDBNull(reader.GetOrdinal("Marks")), -1, Convert.ToInt32(reader("Marks")))

                End If

            End Using
        End Using

        Con.Close()
        Return AssRec
    End Function
    Public Sub UpdateFileDataAndSubmitTime(ByVal studentID As Integer, ByVal roomID As Integer, ByVal seqNo As Integer, ByVal newFileData As Byte())
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        ' Get the current system time
        Dim currentSubmitTime As DateTime = DateTime.Now

        ' Query to update FileData and Submit_Time for a specific student, room, and sequence number
        Dim query As String = "UPDATE moodle_studentcourse SET File = @NewFileData, Submit_Time = @CurrentSubmitTime " &
                              "WHERE Room_ID = @RoomID AND Seq_no = @SeqNo AND Student_ID = @StudentID"

        Using cmd As New MySqlCommand(query, Con)
            ' Set FileData parameter
            If newFileData IsNot Nothing AndAlso newFileData.Length > 0 Then
                cmd.Parameters.AddWithValue("@NewFileData", newFileData)
            Else
                cmd.Parameters.AddWithValue("@NewFileData", DBNull.Value)
            End If

            cmd.Parameters.AddWithValue("@CurrentSubmitTime", currentSubmitTime)
            cmd.Parameters.AddWithValue("@RoomID", roomID)
            cmd.Parameters.AddWithValue("@SeqNo", seqNo)
            cmd.Parameters.AddWithValue("@StudentID", studentID)

            cmd.ExecuteNonQuery()
        End Using

        Con.Close()
    End Sub








End Class
