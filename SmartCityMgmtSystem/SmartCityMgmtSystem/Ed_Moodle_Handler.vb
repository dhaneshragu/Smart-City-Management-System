Imports MySql.Data.MySqlClient
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

    Public Function AddCourse(ByVal passKey As String, ByVal profId As Integer, ByVal instId As Integer, ByVal year As Integer, ByVal mClass As Integer, ByVal mSem As Integer, ByVal name As String) As Integer
        Dim Con = Globals.GetDBConnection()
        Con.Open()


        'query to obtain type of institution'
        Dim queryInst As String = "SELECT Inst_Type FROM ed_institution WHERE Inst_ID = @instId"
        Dim cmdInst As New MySqlCommand(queryInst, Con)
        cmdInst.Parameters.AddWithValue("@instId", instId)
        Dim instType As String = cmdInst.ExecuteScalar()
        Dim roomId As Integer = 0

        'if insttype  is SCHOOL, the insert into class else into sem'
        If instType = "School" Then
            Dim query As String = "INSERT INTO moodle_course (Room_ID,Pass_Key, Prof_ID, Inst_ID, Year, Class, Name) " &
                         "VALUES (@roomID,@passKey, @profId, @instId, @year, @class, @name); "

            Dim cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@passKey", passKey)
            cmd.Parameters.AddWithValue("@profId", profId)
            cmd.Parameters.AddWithValue("@instId", instId)
            cmd.Parameters.AddWithValue("@year", year)
            cmd.Parameters.AddWithValue("@class", mClass)
            cmd.Parameters.AddWithValue("@name", name)

            'Set room id to be maximum +1'

            Dim queryRoomId As String = "SELECT MAX(Room_ID) FROM moodle_course"
            Dim cmdRoomId As New MySqlCommand(queryRoomId, Con)
            Dim result As Object = cmdRoomId.ExecuteScalar()
            If result IsNot Nothing Then
                roomId = Convert.ToInt32(result) + 1
            End If

            cmd.Parameters.AddWithValue("@roomID", roomId)

            cmd.ExecuteNonQuery()

            Con.Close()

        Else
            Dim query As String = "INSERT INTO moodle_course (Room_ID,Pass_Key, Prof_ID, Inst_ID, Year, Sem, Name) " &
                         "VALUES (@roomID,@passKey, @profId, @instId, @year, @sem, @name); "

            Dim cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@passKey", passKey)
            cmd.Parameters.AddWithValue("@profId", profId)
            cmd.Parameters.AddWithValue("@instId", instId)
            cmd.Parameters.AddWithValue("@year", year)
            cmd.Parameters.AddWithValue("@sem", mSem)
            cmd.Parameters.AddWithValue("@name", name)

            'Set room id to be maximum +1'

            Dim queryRoomId As String = "SELECT MAX(Room_ID) FROM moodle_course"
            Dim cmdRoomId As New MySqlCommand(queryRoomId, Con)
            Dim result As Object = cmdRoomId.ExecuteScalar()
            If result IsNot Nothing Then
                roomId = Convert.ToInt32(result) + 1
            End If

            cmd.Parameters.AddWithValue("@roomID", roomId)

            cmd.ExecuteNonQuery()

            Con.Close()

        End If



        Return roomId


    End Function





    Public Function GetTeacherCourses(ByVal Prof_ID As Integer) As MoodleCourse()
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim courses As New List(Of MoodleCourse)()

        'retrieve list of courses where prof_id = Prof_ID'

        Dim query As String = "SELECT * FROM moodle_course WHERE Prof_ID = @Prof_ID"


        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@Prof_ID", Prof_ID)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim course As New MoodleCourse()
                    course.RoomID = If(reader("Room_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Room_ID")), 0)
                    course.PassKey = If(reader("Pass_Key") IsNot DBNull.Value, reader("Pass_Key").ToString(), "")
                    course.ProfID = Prof_ID
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

    Public Function AddCourseContent(ByVal roomID As Integer, ByVal contentName As String, ByVal contentType As String, ByVal videoLink As String, ByVal content As String)
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim query As String = "INSERT INTO moodle_coursecontent (Room_ID, Content_Name, Content_Type, Video_Link, Content, Seq_no) " &
                          "VALUES (@roomID, @contentName, @contentType, @videoLink, @content, @seqNo)"

        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@roomID", roomID)
        cmd.Parameters.AddWithValue("@contentName", contentName)
        cmd.Parameters.AddWithValue("@contentType", contentType)
        cmd.Parameters.AddWithValue("@videoLink", videoLink)
        cmd.Parameters.AddWithValue("@content", content)

        'set seqNo to be maximum seqNo+1 of that roomID'
        Dim querySeqNo As String = "SELECT MAX(Seq_no) FROM moodle_coursecontent WHERE Room_ID = @roomID"
        Dim cmdSeqNo As New MySqlCommand(querySeqNo, Con)
        cmdSeqNo.Parameters.AddWithValue("@roomID", roomID)
        Dim result As Object = cmdSeqNo.ExecuteScalar()
        Dim seqNo As Integer = 0
        'check if result is null'
        If result Is DBNull.Value Then
            seqNo = 1
        ElseIf result IsNot Nothing Then
            seqNo = Convert.ToInt32(result) + 1
        End If
        cmd.Parameters.AddWithValue("@seqNo", seqNo)

        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

        Con.Close()

    End Function

    Public Function UpdateCourseContent(ByVal roomID As Integer, ByVal seqNo As Integer, ByVal contentName As String, ByVal contentType As String, ByVal videoLink As String, ByVal content As String)
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim query As String = "UPDATE moodle_coursecontent SET Content_Name = @contentName, Content_Type = @contentType, Video_Link = @videoLink, Content = @content WHERE Room_ID = @roomID AND Seq_no = @seqNo"

        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@roomID", roomID)
        cmd.Parameters.AddWithValue("@seqNo", seqNo)
        cmd.Parameters.AddWithValue("@contentName", contentName)
        cmd.Parameters.AddWithValue("@contentType", contentType)
        cmd.Parameters.AddWithValue("@videoLink", videoLink)
        cmd.Parameters.AddWithValue("@content", content)

        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

        Con.Close()

    End Function
    Public Function DeleteCourseContent(ByVal roomID As Integer, ByVal seqNo As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim query As String = "DELETE FROM moodle_coursecontent WHERE Room_ID = @roomID AND Seq_no = @seqNo"

        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@roomID", roomID)
        cmd.Parameters.AddWithValue("@seqNo", seqNo)

        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

        Con.Close()

    End Function

    Public Function LoadCourseContent(ByVal roomID As Integer, ByVal seqNo As Integer) As RoomContent
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim content As New RoomContent()

        Dim query As String = "SELECT * FROM moodle_coursecontent WHERE Room_ID = @roomID AND Seq_no = @seqNo"

        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@roomID", roomID)
            cmd.Parameters.AddWithValue("@seqNo", seqNo)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    content.RoomID = If(reader("Room_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Room_ID")), 0)
                    content.ContentName = If(reader("Content_Name") IsNot DBNull.Value, reader("Content_Name").ToString(), "")
                    content.ContentType = If(reader("Content_Type") IsNot DBNull.Value, reader("Content_Type").ToString(), "")
                    content.VideoLink = If(reader("Video_Link") IsNot DBNull.Value, reader("Video_Link").ToString(), "")
                    content.Content = If(reader("Content") IsNot DBNull.Value, reader("Content").ToString(), "")
                    content.SeqNo = If(reader("Seq_no") IsNot DBNull.Value, Convert.ToInt32(reader("Seq_no")), 0)
                End If
            End Using
        End Using

        Con.Close()  
        Return content
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
        Public Property AssName As String
    End Class

    Public Function AssStatus(ByVal studentID As Integer, ByVal Roomid As Integer, ByVal seno As Integer) As StudentAssRecord
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim submitTime As New DateTime(2024, 4, 14, 12, 30, 0)

        Dim AssRec As New StudentAssRecord()
        Dim query As String = "SELECT sc.*, cc.Content_Name " &
                      "FROM moodle_studentcourse AS sc " &
                      "LEFT JOIN moodle_coursecontent AS cc ON sc.Room_ID = cc.Room_ID " &
                      "WHERE sc.Room_ID = @roomid AND sc.Seq_no = @seqno AND sc.Student_ID = @Stuid"

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
                    AssRec.AssName = If(Not reader.IsDBNull(reader.GetOrdinal("Content_Name")), reader.GetString(reader.GetOrdinal("Content_Name")), AssRec.Seq_no.ToString())

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

    Public Function GetAssMarks(ByVal stuid As Integer, ByVal Roomid As Integer) As StudentAssRecord()
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim assignments As New List(Of StudentAssRecord)()

        ' Query to fetch assignments of courses the student is enrolled in
        Dim query As String = "SELECT sc.*, cc.Content_Name " &
                      "FROM moodle_studentcourse AS sc " &
                      "LEFT JOIN moodle_coursecontent AS cc ON sc.Room_ID = cc.Room_ID and sc.Seq_no = cc.Seq_no " &
                      "WHERE sc.Room_ID = @roomid AND sc.Student_ID = @studentID and cc.Content_Type = 'Assignment'"

        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@studentID", stuid)
            cmd.Parameters.AddWithValue("@roomid", Roomid)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim AssRec As New StudentAssRecord()
                    AssRec.Room_ID = If(reader.IsDBNull(reader.GetOrdinal("Room_ID")), -1, Convert.ToInt32(reader("Room_ID")))
                    AssRec.Seq_no = If(reader.IsDBNull(reader.GetOrdinal("Seq_no")), -1, Convert.ToInt32(reader("Seq_no")))
                    AssRec.Student_ID = If(reader.IsDBNull(reader.GetOrdinal("Student_ID")), -1, Convert.ToInt32(reader("Student_ID")))
                    AssRec.FileData = If(reader.IsDBNull(reader.GetOrdinal("File")), Nothing, DirectCast(reader("File"), Byte()))
                    AssRec.Submit_Time = If(reader.IsDBNull(reader.GetOrdinal("Submit_Time")), DateTime.MinValue, Convert.ToDateTime(reader("Submit_Time")))
                    AssRec.Marks = If(reader.IsDBNull(reader.GetOrdinal("Marks")), -1, Convert.ToInt32(reader("Marks")))
                    AssRec.AssName = If(Not reader.IsDBNull(reader.GetOrdinal("Content_Name")), reader.GetString(reader.GetOrdinal("Content_Name")), AssRec.Seq_no.ToString())

                    assignments.Add(AssRec)
                End While
            End Using
        End Using

        Con.Close()

        
        Return assignments.ToArray()
    End Function

    Public Function GetAssignmentsSubmitted(ByVal roomID As Integer, ByVal seqNo As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim assignments As New List(Of StudentAssRecord)()

        ' Query to fetch assignments of course'
        Dim query As String = "Select * from moodle_studentcourse where Room_ID = @roomid and Seq_no = @seqno"

        Using cmd As New MySqlCommand(query, Con)

            cmd.Parameters.AddWithValue("@roomid", roomID)
            cmd.Parameters.AddWithValue("@seqno", seqNo)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim AssRec As New StudentAssRecord()
                    AssRec.Room_ID = If(reader.IsDBNull(reader.GetOrdinal("Room_ID")), -1, Convert.ToInt32(reader("Room_ID")))
                    AssRec.Seq_no = If(reader.IsDBNull(reader.GetOrdinal("Seq_no")), -1, Convert.ToInt32(reader("Seq_no")))
                    AssRec.Student_ID = If(reader.IsDBNull(reader.GetOrdinal("Student_ID")), -1, Convert.ToInt32(reader("Student_ID")))
                    AssRec.FileData = If(reader.IsDBNull(reader.GetOrdinal("File")), Nothing, DirectCast(reader("File"), Byte()))
                    AssRec.Submit_Time = If(reader.IsDBNull(reader.GetOrdinal("Submit_Time")), DateTime.MinValue, Convert.ToDateTime(reader("Submit_Time")))
                    AssRec.Marks = If(reader.IsDBNull(reader.GetOrdinal("Marks")), -1, Convert.ToInt32(reader("Marks")))

                    assignments.Add(AssRec)
                End While
            End Using
        End Using

        Con.Close()


        Return assignments.ToArray()
    End Function


    Public Function UpdateMarks(ByVal marks As Integer, ByVal roomId As Integer, ByVal stuID As Integer, ByVal seqNo As Integer) As Integer
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim query As String = "UPDATE moodle_studentcourse SET Marks = @marks WHERE Room_ID = @roomID AND Student_ID = @stuID AND Seq_no = @seqNo"

        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@roomID", roomId)
        cmd.Parameters.AddWithValue("@stuID", stuID)
        cmd.Parameters.AddWithValue("@seqNo", seqNo)
        cmd.Parameters.AddWithValue("@marks", marks)
        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

        Con.Close()

        Return rowsAffected
    End Function

End Class
