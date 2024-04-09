Imports System.Web.UI.WebControls
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Web

Public Class Ed_Coursera_Handler
    Public Class Course
        Public Property CourseID As Integer
        Public Property Affiliation As Integer

        Public Property Institution As String
        Public Property Name As String
        Public Property Category As String
        Public Property TeacherName As String
        Public Property TeacherID As Integer
        Public Property Syllabus As String
        Public Property IntroVideoLink As String
        Public Property ApprStatus As String
        Public Property Fees As Integer
        Public Property Rating As Double
        Public Property RatingCount As Integer

        Public Sub New()
            ' Default constructor
        End Sub

        ' Constructor with parameters to initialize properties
        Public Sub New(ByVal courseId As Integer, ByVal affiliation As Integer, ByVal name As String, ByVal category As String, ByVal teacherName As String, ByVal teacherId As Integer, ByVal syllabus As String, ByVal introVideoLink As String, ByVal apprStatus As String, ByVal fees As Integer, ByVal rating As Double, ByVal ratingCount As Integer)
            Me.CourseID = courseId
            Me.Affiliation = affiliation
            Me.Name = name
            Me.Category = category
            Me.TeacherName = teacherName
            Me.TeacherID = teacherId
            Me.Syllabus = syllabus
            Me.IntroVideoLink = introVideoLink
            Me.ApprStatus = apprStatus
            Me.Fees = fees
            Me.Rating = rating
            Me.RatingCount = ratingCount
        End Sub
    End Class

    Public Class CourseContent
        Public Property CourseID As Integer
        Public Property ContentName As String
        Public Property ContentType As String
        Public Property VideoLink As String
        Public Property Content As String
        Public Property SeqNo As Integer

        Public Sub New()
            ' Default constructor
        End Sub

        ' Constructor with parameters to initialize properties
        Public Sub New(ByVal courseId As Integer, ByVal contentName As String, ByVal contentType As String, ByVal videoLink As String, ByVal content As String, ByVal seqNo As Integer)
            Me.CourseID = courseId
            Me.ContentName = contentName
            Me.ContentType = contentType
            Me.VideoLink = videoLink
            Me.Content = content
            Me.SeqNo = seqNo
        End Sub
    End Class

    Public Function GetCourses() As Course()

        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim courses As New List(Of Course)()

        Dim query As String = "SELECT ec_course.Course_ID, ec_course.Affiliation, ec_course.Name, ec_course.Category, ec_course.Teacher_Name, ec_course.Teacher_ID, ec_course.SYLLABUS, ec_course.Intro_Video_link, ec_course.Appr_Status, ec_course.Fees, ec_course.Rating, ec_course.Rating_Count, ed_institution.Inst_Name " &
                              "FROM ec_course " &
                              "INNER JOIN ed_institution ON ec_course.Affiliation = ed_institution.Inst_ID"

        Dim cmd As New MySqlCommand(query, Con)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()
        While reader.Read()
            Dim course As New Course()

            course.CourseID = If(reader("Course_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Course_ID")), 0)
            course.Affiliation = If(reader("Affiliation") IsNot DBNull.Value, Convert.ToInt32(reader("Affiliation")), 0)
            course.Name = If(reader("Name") IsNot DBNull.Value, reader("Name").ToString(), "")
            course.Category = If(reader("Category") IsNot DBNull.Value, reader("Category").ToString(), "")
            course.TeacherName = If(reader("Teacher_Name") IsNot DBNull.Value, reader("Teacher_Name").ToString(), "")
            course.TeacherID = If(reader("Teacher_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Teacher_ID")), 0)
            course.Syllabus = If(reader("SYLLABUS") IsNot DBNull.Value, reader("SYLLABUS").ToString(), "")
            course.IntroVideoLink = If(reader("Intro_Video_link") IsNot DBNull.Value, reader("Intro_Video_link").ToString(), "")
            course.ApprStatus = If(reader("Appr_Status") IsNot DBNull.Value, reader("Appr_Status").ToString(), "")
            course.Fees = If(reader("Fees") IsNot DBNull.Value, Convert.ToInt32(reader("Fees")), 0)
            course.Rating = If(reader("Rating") IsNot DBNull.Value, Convert.ToDouble(reader("Rating")), 0.0)
            course.RatingCount = If(reader("Rating_Count") IsNot DBNull.Value, Convert.ToInt32(reader("Rating_Count")), 0)
            course.Institution = If(reader("Inst_Name") IsNot DBNull.Value, reader("Inst_Name").ToString(), "")

            courses.Add(course)
        End While

        Return courses.ToArray()

    End Function

    Public Function AddCourse(ByVal affiliation As Integer, ByVal name As String, ByVal category As String, ByVal teacherName As String, ByVal teacherID As Integer, ByVal syllabus As String, ByVal introVideoLink As String, ByVal apprStatus As String, ByVal fees As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim query As String = "INSERT INTO ec_course (Course_ID,Affiliation, Name, Category, Teacher_Name, Teacher_ID, SYLLABUS, Intro_Video_link, Appr_Status, Fees, Rating, Rating_Count) VALUES (@courseid, @affiliation, @name, @category, @teacherName, @teacherID, @syllabus, @introVideoLink, @apprStatus, @fees, @rating, @ratingCount)"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@affiliation", affiliation)
        cmd.Parameters.AddWithValue("@name", name)
        cmd.Parameters.AddWithValue("@category", category)
        cmd.Parameters.AddWithValue("@teacherName", teacherName)
        cmd.Parameters.AddWithValue("@teacherID", teacherID)
        cmd.Parameters.AddWithValue("@syllabus", syllabus)
        cmd.Parameters.AddWithValue("@introVideoLink", introVideoLink)
        cmd.Parameters.AddWithValue("@apprStatus", apprStatus)
        cmd.Parameters.AddWithValue("@fees", fees)
        'set null value for rating and ratingcount'

        cmd.Parameters.AddWithValue("@rating", 0)

        cmd.Parameters.AddWithValue("@ratingCount", 0)

        'Set CourseID to the last inserted ID +1'
        Dim query2 As String = "SELECT MAX(Course_ID) FROM ec_course"
        Dim cmd2 As New MySqlCommand(query2, Con)
        Dim reader As MySqlDataReader = cmd2.ExecuteReader()
        Dim courseID As Integer = 0
        If reader.Read() Then
            courseID = If(reader("MAX(Course_ID)") IsNot DBNull.Value, Convert.ToInt32(reader("MAX(Course_ID)")) + 1, 100)
        End If
        reader.Close()

        cmd.Parameters.AddWithValue("@courseid", courseID)


        cmd.ExecuteNonQuery()
        Con.Close()
    End Function

    Public Function UpdateCourse(ByVal courseID As Integer, ByVal name As String, ByVal category As String, ByVal syllabus As String, ByVal introVideoLink As String, ByVal fees As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim query As String = "UPDATE ec_course SET  Name = @name, Category = @category,  SYLLABUS = @syllabus, Intro_Video_link = @introVideoLink,  Fees = @fees WHERE Course_ID = @courseID"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@courseID", courseID)

        cmd.Parameters.AddWithValue("@name", name)
        cmd.Parameters.AddWithValue("@category", category)

        cmd.Parameters.AddWithValue("@syllabus", syllabus)
        cmd.Parameters.AddWithValue("@introVideoLink", introVideoLink)

        cmd.Parameters.AddWithValue("@fees", fees)

        cmd.ExecuteNonQuery()
        Con.Close()
    End Function

    Public Function GetTeacherCourses(ByVal teacherID As Integer) As Course()

        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim courses As New List(Of Course)()

        Dim query As String = "SELECT ec_course.Course_ID, ec_course.Affiliation, ec_course.Name, ec_course.Category, ec_course.Teacher_Name, ec_course.Teacher_ID, ec_course.SYLLABUS, ec_course.Intro_Video_link, ec_course.Appr_Status, ec_course.Fees, ec_course.Rating, ec_course.Rating_Count, ed_institution.Inst_Name " &
                              "FROM ec_course " &
                              "INNER JOIN ed_institution ON ec_course.Affiliation = ed_institution.Inst_ID " &
                               "WHERE ec_course.Teacher_ID = @teacherID"


        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@teacherID", teacherID)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()
        While reader.Read()
            Dim course As New Course()

            course.CourseID = If(reader("Course_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Course_ID")), 0)
            course.Affiliation = If(reader("Affiliation") IsNot DBNull.Value, Convert.ToInt32(reader("Affiliation")), 0)
            course.Name = If(reader("Name") IsNot DBNull.Value, reader("Name").ToString(), "")
            course.Category = If(reader("Category") IsNot DBNull.Value, reader("Category").ToString(), "")
            course.TeacherName = If(reader("Teacher_Name") IsNot DBNull.Value, reader("Teacher_Name").ToString(), "")
            course.TeacherID = If(reader("Teacher_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Teacher_ID")), 0)
            course.Syllabus = If(reader("SYLLABUS") IsNot DBNull.Value, reader("SYLLABUS").ToString(), "")
            course.IntroVideoLink = If(reader("Intro_Video_link") IsNot DBNull.Value, reader("Intro_Video_link").ToString(), "")
            course.ApprStatus = If(reader("Appr_Status") IsNot DBNull.Value, reader("Appr_Status").ToString(), "")
            course.Fees = If(reader("Fees") IsNot DBNull.Value, Convert.ToInt32(reader("Fees")), 0)
            course.Rating = If(reader("Rating") IsNot DBNull.Value, Convert.ToDouble(reader("Rating")), 0.0)
            course.RatingCount = If(reader("Rating_Count") IsNot DBNull.Value, Convert.ToInt32(reader("Rating_Count")), 0)
            course.Institution = If(reader("Inst_Name") IsNot DBNull.Value, reader("Inst_Name").ToString(), "")

            courses.Add(course)
        End While

        Return courses.ToArray()

    End Function



    Public Function GetCourseDetails(ByVal courseID As Integer) As Course
        Dim course As New Course()

        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim query As String = "SELECT ec_course.Course_ID, ec_course.Affiliation, ec_course.Name, ec_course.Category, ec_course.Teacher_Name, ec_course.Teacher_ID, ec_course.SYLLABUS, ec_course.Intro_Video_link, ec_course.Appr_Status, ec_course.Fees, ec_course.Rating, ec_course.Rating_Count, ed_institution.Inst_Name " &
                          "FROM ec_course " &
                          "INNER JOIN ed_institution ON ec_course.Affiliation = ed_institution.Inst_ID " &
                          "WHERE ec_course.Course_ID = @courseID"

        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@courseID", courseID)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        If reader.Read() Then
            course.CourseID = If(reader("Course_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Course_ID")), 0)
            course.Affiliation = If(reader("Affiliation") IsNot DBNull.Value, Convert.ToInt32(reader("Affiliation")), 0)
            course.Name = If(reader("Name") IsNot DBNull.Value, reader("Name").ToString(), "")
            course.Category = If(reader("Category") IsNot DBNull.Value, reader("Category").ToString(), "")
            course.TeacherName = If(reader("Teacher_Name") IsNot DBNull.Value, reader("Teacher_Name").ToString(), "")
            course.TeacherID = If(reader("Teacher_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Teacher_ID")), 0)
            course.Syllabus = If(reader("SYLLABUS") IsNot DBNull.Value, reader("SYLLABUS").ToString(), "")
            course.IntroVideoLink = If(reader("Intro_Video_link") IsNot DBNull.Value, reader("Intro_Video_link").ToString(), "")
            course.ApprStatus = If(reader("Appr_Status") IsNot DBNull.Value, reader("Appr_Status").ToString(), "")
            course.Fees = If(reader("Fees") IsNot DBNull.Value, Convert.ToInt32(reader("Fees")), 0)
            course.Rating = If(reader("Rating") IsNot DBNull.Value, Convert.ToDouble(reader("Rating")), 0.0)
            course.RatingCount = If(reader("Rating_Count") IsNot DBNull.Value, Convert.ToInt32(reader("Rating_Count")), 0)
            course.Institution = If(reader("Inst_Name") IsNot DBNull.Value, reader("Inst_Name").ToString(), "")
        End If

        Con.Close()
        Return course
    End Function


    Public Function GetCourseContents(ByVal courseId As Integer) As CourseContent()
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim contents As New List(Of CourseContent)()

        Dim query As String = "SELECT Content_Name, Content_Type, Video_Link, Content, Seq_no FROM ec_coursecontent WHERE Course_ID = @courseId"

        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@courseId", courseId)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        While reader.Read()
            Dim content As New CourseContent()

            content.CourseID = courseId
            content.ContentName = If(reader("Content_Name") IsNot DBNull.Value, reader("Content_Name").ToString(), "")
            content.ContentType = If(reader("Content_Type") IsNot DBNull.Value, reader("Content_Type").ToString(), "")
            content.VideoLink = If(reader("Video_Link") IsNot DBNull.Value, reader("Video_Link").ToString(), "")
            content.Content = If(reader("Content") IsNot DBNull.Value, reader("Content").ToString(), "")
            content.SeqNo = If(reader("Seq_no") IsNot DBNull.Value, Convert.ToInt32(reader("Seq_no")), 0)

            contents.Add(content)
        End While

        Return contents.ToArray()
    End Function

    Public Function AddCourseContent(ByVal courseId As Integer, ByVal contentName As String, ByVal contentType As String, ByVal videoLink As String, ByVal content As String)
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim seqNo As Integer = 0
        Dim query As String = "INSERT INTO ec_coursecontent (Course_ID, Content_Name, Content_Type, Video_Link, Content, Seq_no) VALUES (@courseId, @contentName, @contentType, @videoLink, @content, @seqNo)"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@courseId", courseId)
        cmd.Parameters.AddWithValue("@contentName", contentName)
        cmd.Parameters.AddWithValue("@contentType", contentType)
        cmd.Parameters.AddWithValue("@videoLink", videoLink)
        cmd.Parameters.AddWithValue("@content", content)

        'obtain maximum seqNo for given courseID'
        Dim query2 As String = "SELECT MAX(Seq_no) FROM ec_coursecontent WHERE Course_ID = @courseId"
        Dim cmd2 As New MySqlCommand(query2, Con)
        cmd2.Parameters.AddWithValue("@courseId", courseId)
        Dim reader As MySqlDataReader = cmd2.ExecuteReader()
        If reader.Read() Then
            seqNo = If(reader("MAX(Seq_no)") IsNot DBNull.Value, Convert.ToInt32(reader("MAX(Seq_no)")) + 1, 100)
        End If
        reader.Close()

        cmd.Parameters.AddWithValue("@seqNo", seqNo)
        cmd.ExecuteNonQuery()
        Con.Close()
    End Function

    Public Function UpdateCourseContent(ByVal courseId As Integer, ByVal seqNo As Integer, ByVal contentName As String, ByVal contentType As String, ByVal videoLink As String, ByVal content As String)
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim query As String = "UPDATE ec_coursecontent SET Content_Name = @contentName, Content_Type = @contentType, Video_Link = @videoLink, Content = @content WHERE Course_ID = @courseId AND Seq_no = @seqNo"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@courseId", courseId)
        cmd.Parameters.AddWithValue("@seqNo", seqNo)
        cmd.Parameters.AddWithValue("@contentName", contentName)
        cmd.Parameters.AddWithValue("@contentType", contentType)
        cmd.Parameters.AddWithValue("@videoLink", videoLink)
        cmd.Parameters.AddWithValue("@content", content)
        cmd.ExecuteNonQuery()
        Con.Close()
    End Function

    Public Function GetCourseContent(ByVal courseId As Integer, ByVal seqNo As Integer) As CourseContent
        Dim content As New CourseContent()

        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim query As String = "SELECT Content_Name, Content_Type, Video_Link, Content FROM ec_coursecontent WHERE Course_ID = @courseId AND Seq_no = @seqNo"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@courseId", courseId)
        cmd.Parameters.AddWithValue("@seqNo", seqNo)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        If reader.Read() Then
            content.CourseID = courseId
            content.ContentName = If(reader("Content_Name") IsNot DBNull.Value, reader("Content_Name").ToString(), "")
            content.ContentType = If(reader("Content_Type") IsNot DBNull.Value, reader("Content_Type").ToString(), "")
            content.VideoLink = If(reader("Video_Link") IsNot DBNull.Value, reader("Video_Link").ToString(), "")
            content.Content = If(reader("Content") IsNot DBNull.Value, reader("Content").ToString(), "")
            content.SeqNo = seqNo
        End If

        Con.Close()
        Return content
    End Function

    Public Function DeleteCourseContent(ByVal courseId As Integer, ByVal seqNo As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim query As String = "DELETE FROM ec_coursecontent WHERE Course_ID = @courseId AND Seq_no = @seqNo"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@courseId", courseId)
        cmd.Parameters.AddWithValue("@seqNo", seqNo)
        cmd.ExecuteNonQuery()
        Con.Close()
    End Function

    Public Function GetInProgressCourses(ByVal studentId As Integer) As Course()
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim courses As New List(Of Course)()

        Dim query As String = "SELECT ec_course.Course_ID, ec_course.Affiliation, ec_course.Name, ec_course.Category, ec_course.Teacher_Name, ec_course.Teacher_ID, ec_course.SYLLABUS, ec_course.Intro_Video_link, ec_course.Appr_Status, ec_course.Fees, ec_course.Rating, ec_course.Rating_Count, ed_institution.Inst_Name " &
                          "FROM ec_course " &
                          "INNER JOIN ed_institution ON ec_course.Affiliation = ed_institution.Inst_ID " &
                          "INNER JOIN ec_studentcourse ON ec_course.Course_ID = ec_studentcourse.Course_ID " &
                          "WHERE ec_studentcourse.Student_ID = @studentId AND ec_studentcourse.Completion_Status = 'In-Progress'"

        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@studentId", studentId)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        While reader.Read()
            Dim course As New Course()

            course.CourseID = If(reader("Course_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Course_ID")), 0)
            course.Affiliation = If(reader("Affiliation") IsNot DBNull.Value, Convert.ToInt32(reader("Affiliation")), 0)
            course.Name = If(reader("Name") IsNot DBNull.Value, reader("Name").ToString(), "")
            course.Category = If(reader("Category") IsNot DBNull.Value, reader("Category").ToString(), "")
            course.TeacherName = If(reader("Teacher_Name") IsNot DBNull.Value, reader("Teacher_Name").ToString(), "")
            course.TeacherID = If(reader("Teacher_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Teacher_ID")), 0)
            course.Syllabus = If(reader("SYLLABUS") IsNot DBNull.Value, reader("SYLLABUS").ToString(), "")
            course.IntroVideoLink = If(reader("Intro_Video_link") IsNot DBNull.Value, reader("Intro_Video_link").ToString(), "")
            course.ApprStatus = If(reader("Appr_Status") IsNot DBNull.Value, reader("Appr_Status").ToString(), "")
            course.Fees = If(reader("Fees") IsNot DBNull.Value, Convert.ToInt32(reader("Fees")), 0)
            course.Rating = If(reader("Rating") IsNot DBNull.Value, Convert.ToDouble(reader("Rating")), 0.0)
            course.RatingCount = If(reader("Rating_Count") IsNot DBNull.Value, Convert.ToInt32(reader("Rating_Count")), 0)
            course.Institution = If(reader("Inst_Name") IsNot DBNull.Value, reader("Inst_Name").ToString(), "")

            courses.Add(course)
        End While

        Return courses.ToArray()
    End Function

    Public Function GetCompletedCourses(ByVal studentId As Integer) As Course()
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim courses As New List(Of Course)()

        Dim query As String = "SELECT ec_course.Course_ID, ec_course.Affiliation, ec_course.Name, ec_course.Category, ec_course.Teacher_Name, ec_course.Teacher_ID, ec_course.SYLLABUS, ec_course.Intro_Video_link, ec_course.Appr_Status, ec_course.Fees, ec_course.Rating, ec_course.Rating_Count, ed_institution.Inst_Name " &
                          "FROM ec_course " &
                          "INNER JOIN ed_institution ON ec_course.Affiliation = ed_institution.Inst_ID " &
                          "INNER JOIN ec_studentcourse ON ec_course.Course_ID = ec_studentcourse.Course_ID " &
                          "WHERE ec_studentcourse.Student_ID = @studentId AND ec_studentcourse.Completion_Status = 'Completed'"

        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@studentId", studentId)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        While reader.Read()
            Dim course As New Course()

            course.CourseID = If(reader("Course_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Course_ID")), 0)
            course.Affiliation = If(reader("Affiliation") IsNot DBNull.Value, Convert.ToInt32(reader("Affiliation")), 0)
            course.Name = If(reader("Name") IsNot DBNull.Value, reader("Name").ToString(), "")
            course.Category = If(reader("Category") IsNot DBNull.Value, reader("Category").ToString(), "")
            course.TeacherName = If(reader("Teacher_Name") IsNot DBNull.Value, reader("Teacher_Name").ToString(), "")
            course.TeacherID = If(reader("Teacher_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Teacher_ID")), 0)
            course.Syllabus = If(reader("SYLLABUS") IsNot DBNull.Value, reader("SYLLABUS").ToString(), "")
            course.IntroVideoLink = If(reader("Intro_Video_link") IsNot DBNull.Value, reader("Intro_Video_link").ToString(), "")
            course.ApprStatus = If(reader("Appr_Status") IsNot DBNull.Value, reader("Appr_Status").ToString(), "")
            course.Fees = If(reader("Fees") IsNot DBNull.Value, Convert.ToInt32(reader("Fees")), 0)
            course.Rating = If(reader("Rating") IsNot DBNull.Value, Convert.ToDouble(reader("Rating")), 0.0)
            course.RatingCount = If(reader("Rating_Count") IsNot DBNull.Value, Convert.ToInt32(reader("Rating_Count")), 0)
            course.Institution = If(reader("Inst_Name") IsNot DBNull.Value, reader("Inst_Name").ToString(), "")

            courses.Add(course)
        End While

        Return courses.ToArray()
    End Function

    Public Function GetStudentCourseCompletionRecords(ByVal studentID As Integer, ByVal courseID As Integer) As Integer()
        Dim seqs As New List(Of Integer)()

        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim query As String = "SELECT Seq_No FROM ec_studcoursecompletion WHERE Student_ID = @studentID AND Course_ID = @courseID"

        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@studentId", studentID)
        cmd.Parameters.AddWithValue("@courseID", courseID)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()


        While reader.Read()
            seqs.Add(Convert.ToInt32(reader("Seq_No")))
        End While


        Return seqs.ToArray()
    End Function


    Public Function CompleteResource(ByVal studentID As Integer, ByVal courseID As Integer, ByVal SeqNo As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim query As String = "INSERT INTO ec_studcoursecompletion VALUES (@CourseID, @StudentID, @SeqID)"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@StudentID", studentID)
        cmd.Parameters.AddWithValue("@CourseID", courseID)
        cmd.Parameters.AddWithValue("@SeqID", SeqNo)
        cmd.ExecuteNonQuery()
        Con.Close()
    End Function

    Public Function GetStudentCourseCompletionCounts(ByVal studentID As Integer) As Dictionary(Of Integer, Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        ' Dictionary to store course IDs and their respective completion counts
        Dim completionCounts As New Dictionary(Of Integer, Integer)()

        ' SQL query to get course IDs and their completion counts for a particular student
        Dim query As String = "SELECT ec_studentcourse.Course_ID, COUNT(ec_studcoursecompletion.Student_ID) AS TotalEntries " &
                          "FROM ec_studentcourse " &
                          "LEFT JOIN ec_studcoursecompletion ON ec_studcoursecompletion.Course_ID = ec_studentcourse.Course_ID " &
                          "AND ec_studcoursecompletion.Student_ID = ec_studentcourse.Student_ID " &
                          "WHERE ec_studentcourse.Student_ID = @studentID " &
                          "GROUP BY ec_studentcourse.Course_ID " &
                          "ORDER BY ec_studentcourse.Course_ID"

        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@studentID", studentID)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim courseID As Integer = Convert.ToInt32(reader("Course_ID"))
                    Dim completionCount As Integer = Convert.ToInt32(reader("TotalEntries"))
                    completionCounts.Add(courseID, completionCount)
                End While
            End Using
        End Using

        Con.Close()

        Return completionCounts
    End Function


    Public Function GetStudentCourseContentCounts(ByVal studentID As Integer) As Dictionary(Of Integer, Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        ' Dictionary to store course IDs and their respective completion counts
        Dim completionCounts As New Dictionary(Of Integer, Integer)()

        ' SQL query to get course IDs and their completion counts for a particular student
        Dim query As String = "SELECT ec_studentcourse.Course_ID, COUNT(ec_coursecontent.Seq_no) AS TotalEntries " &
                          "FROM ec_studentcourse " &
                          "LEFT JOIN ec_coursecontent ON ec_coursecontent.Course_ID = ec_studentcourse.Course_ID " &
                          "GROUP BY ec_studentcourse.Course_ID " &
                          "ORDER BY ec_studentcourse.Course_ID"

        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@studentID", studentID)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim courseID As Integer = Convert.ToInt32(reader("Course_ID"))
                    Dim completionCount As Integer = Convert.ToInt32(reader("TotalEntries"))
                    completionCounts.Add(courseID, completionCount)
                End While
            End Using
        End Using

        Con.Close()

        Return completionCounts
    End Function

    Public Function CompleteCourse(ByVal studentID As Integer, ByVal courseID As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim query As String = "UPDATE ec_studentcourse SET Completion_Status = 'Completed' WHERE Student_ID = @studentID AND Course_ID = @courseID"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@StudentID", studentID)
        cmd.Parameters.AddWithValue("@CourseID", courseID)
        cmd.ExecuteNonQuery()
        Con.Close()
    End Function

    Public Function RateCourse(ByVal studentID As Integer, ByVal courseID As Integer, ByVal rating As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim query As String = "UPDATE ec_studentcourse SET Rating = @Rate WHERE Student_ID = @studentID AND Course_ID = @courseID"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@StudentID", studentID)
        cmd.Parameters.AddWithValue("@CourseID", courseID)
        cmd.Parameters.AddWithValue("@Rate", rating)
        cmd.ExecuteNonQuery()
        Con.Close()
        GenerateCertificateAndSave(studentID, "E-Course", DateTime.Now.Year, courseID)
    End Function

    Public Sub GenerateCertificateAndSave(studentID As Integer, CertType As String, year As Integer, courseID As Integer)
        ' Generate the content for the PDF
        Dim content As String = $"Student ID: {studentID}{Environment.NewLine}" &
                                $"Certificate Type: {CertType}{Environment.NewLine}" &
                                $"Year: {year}{Environment.NewLine}" &
                                $"CourseID: {courseID}{Environment.NewLine}"

        Dim filePath As String = "file.pdf"

        ' Write the text to the PDF file
        File.WriteAllText(filePath, content)

        Dim pdfBytes As Byte() = File.ReadAllBytes(filePath)

        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim query As String = "INSERT INTO ed_certificates (Certificate, Student_ID, Type, Year, Course_ID) VALUES (@pdf, @studentID, @Type, @year, @courseID)"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@studentID", studentID)
        cmd.Parameters.AddWithValue("@courseID", courseID)
        cmd.Parameters.AddWithValue("@pdf", pdfBytes)
        cmd.Parameters.AddWithValue("@year", year)
        cmd.Parameters.AddWithValue("@Type", "E-Course")
        cmd.ExecuteNonQuery()
        Con.Close()


    End Sub

    Public Function GetCertificates(ByVal studentID As Integer) As List(Of Byte())
        Dim certificates As New List(Of Byte())()

        ' Get the database connection
        Dim Con = Globals.GetDBConnection()

        ' Open the database connection
        Con.Open()

        ' SQL query to select certificates from the database
        Dim query As String = "SELECT Certificate FROM ed_certificates where Student_ID = @studentID"

        ' Create a MySqlCommand object
        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@studentID", studentID)
            ' Execute the SQL command
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                ' Iterate through the results
                While reader.Read()
                    ' Extract the certificate bytes from the reader
                    Dim certificateBytes As Byte() = DirectCast(reader("Certificate"), Byte())

                    ' Add the certificate bytes to the list
                    certificates.Add(certificateBytes)
                End While
            End Using
        End Using

        ' Close the database connection
        Con.Close()

        ' Return the list of certificate bytes
        Return certificates
    End Function

End Class
