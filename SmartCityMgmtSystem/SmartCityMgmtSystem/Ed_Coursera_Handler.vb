Imports System.Web.UI.WebControls
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Web
Imports System.Text
Imports SmartCityMgmtSystem.Ed_GlobalDashboard
Imports System.Data.SqlClient

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
        Public Property EnrolledStudents As Integer
        Public Property CompletedStudents As Integer

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
    Public Sub DeleteCourse(ByVal courseId As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim query As String = "DELETE FROM ec_course WHERE Course_ID = @courseId"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@courseId", courseId)

        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
        Con.Close()

        If rowsAffected > 0 Then
            MessageBox.Show("Course with ID " & courseId.ToString() & " deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("No records deleted. Course with ID " & courseId.ToString() & " not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Public Function GetCourses() As Course()

        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim courses As New List(Of Course)()

        Dim query As String = "SELECT ec_course.Course_ID, ec_course.Affiliation, ec_course.Name, ec_course.Category, ec_course.Teacher_Name, ec_course.Teacher_ID, ec_course.SYLLABUS, ec_course.Intro_Video_link, ec_course.Appr_Status, ec_course.Fees, ec_course.Rating, ec_course.Rating_Count, ed_institution.Inst_Name " &
                              "FROM ec_course " &
                              "INNER JOIN ed_institution ON ec_course.Affiliation = ed_institution.Inst_ID
                              WHERE ec_course.Appr_Status = 'Approved'"


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

    Public Function GetApprovedCoursesWithCounts() As Course()

        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim courses As New List(Of Course)()

        Dim query As String = "SELECT ec_course.Course_ID, ec_course.Affiliation, ec_course.Name, ec_course.Category, ec_course.Teacher_Name, ec_course.Teacher_ID, ec_course.SYLLABUS, ec_course.Intro_Video_link, ec_course.Appr_Status, ec_course.Fees, ec_course.Rating, ec_course.Rating_Count, ed_institution.Inst_Name,
                                    (SELECT COUNT(*) FROM ec_studentcourse WHERE ec_studentcourse.Course_ID = ec_course.Course_ID) AS EnrolledStudents,
                                    (SELECT COUNT(*) FROM ec_studentcourse WHERE ec_studentcourse.Course_ID = ec_course.Course_ID AND ec_studentcourse.Completion_Status = 'Completed') AS CompletedStudents
                           FROM ec_course 
                           INNER JOIN ed_institution ON ec_course.Affiliation = ed_institution.Inst_ID
                           WHERE ec_course.Appr_Status = 'Approved'"

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
            course.EnrolledStudents = If(reader("EnrolledStudents") IsNot DBNull.Value, Convert.ToInt32(reader("EnrolledStudents")), 0)
            course.CompletedStudents = If(reader("CompletedStudents") IsNot DBNull.Value, Convert.ToInt32(reader("CompletedStudents")), 0)

            courses.Add(course)
        End While

        reader.Close()
        Con.Close()

        Return courses.ToArray()
    End Function


    Public Function AddCourse(ByVal affiliation As Integer, ByVal name As String, ByVal category As String, ByVal teacherName As String, ByVal teacherID As Integer, ByVal syllabus As String, ByVal introVideoLink As String, ByVal apprStatus As String, ByVal fees As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim query As String = "INSERT INTO ec_course (Course_ID, Affiliation, Name, Category, Teacher_Name, Teacher_ID, SYLLABUS, Intro_Video_link, Appr_Status, Fees, Rating, Rating_Count) VALUES (@courseid, @affiliation, @name, @category, @teacherName, @teacherID, @syllabus, @introVideoLink, @apprStatus, @fees, @rating, @ratingCount)"
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
        Dim query2 As String = "Select MAX(Course_ID) FROM ec_course"
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
        Dim query As String = "UPDATE ec_course Set  Name = @name, Category = @category,  SYLLABUS = @syllabus, Intro_Video_link = @introVideoLink,  Fees = @fees WHERE Course_ID = @courseID"
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

        Dim query As String = "Select ec_course.Course_ID, ec_course.Affiliation, ec_course.Name, ec_course.Category, ec_course.Teacher_Name, ec_course.Teacher_ID, ec_course.SYLLABUS, ec_course.Intro_Video_link, ec_course.Appr_Status, ec_course.Fees, ec_course.Rating, ec_course.Rating_Count, ed_institution.Inst_Name " &
                              "FROM ec_course " &
                              "INNER JOIN ed_institution On ec_course.Affiliation = ed_institution.Inst_ID " &
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

        Dim query As String = "Select ec_course.Course_ID, ec_course.Affiliation, ec_course.Name, ec_course.Category, ec_course.Teacher_Name, ec_course.Teacher_ID, ec_course.SYLLABUS, ec_course.Intro_Video_link, ec_course.Appr_Status, ec_course.Fees, ec_course.Rating, ec_course.Rating_Count, ed_institution.Inst_Name " &
                          "FROM ec_course " &
                          "INNER JOIN ed_institution On ec_course.Affiliation = ed_institution.Inst_ID " &
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

        Dim query As String = "Select Content_Name, Content_Type, Video_Link, Content, Seq_no FROM ec_coursecontent WHERE Course_ID = @courseId"

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
        Dim query2 As String = "Select MAX(Seq_no) FROM ec_coursecontent WHERE Course_ID = @courseId"
        Dim cmd2 As New MySqlCommand(query2, Con)
        cmd2.Parameters.AddWithValue("@courseId", courseId)
        Dim reader As MySqlDataReader = cmd2.ExecuteReader()
        If reader.Read() Then
            seqNo = If(reader("MAX(Seq_no)") IsNot DBNull.Value, Convert.ToInt32(reader("MAX(Seq_no)")) + 1, 1)
        End If
        reader.Close()

        cmd.Parameters.AddWithValue("@seqNo", seqNo)
        cmd.ExecuteNonQuery()
        Con.Close()
    End Function

    Public Function UpdateCourseContent(ByVal courseId As Integer, ByVal seqNo As Integer, ByVal contentName As String, ByVal contentType As String, ByVal videoLink As String, ByVal content As String)
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim query As String = "UPDATE ec_coursecontent Set Content_Name = @contentName, Content_Type = @contentType, Video_Link = @videoLink, Content = @content WHERE Course_ID = @courseId And Seq_no = @seqNo"
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
        Dim query As String = "Select Content_Name, Content_Type, Video_Link, Content FROM ec_coursecontent WHERE Course_ID = @courseId And Seq_no = @seqNo"
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
        Dim query As String = "DELETE FROM ec_coursecontent WHERE Course_ID = @courseId And Seq_no = @seqNo"
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

        Dim query As String = "Select ec_course.Course_ID, ec_course.Affiliation, ec_course.Name, ec_course.Category, ec_course.Teacher_Name, ec_course.Teacher_ID, ec_course.SYLLABUS, ec_course.Intro_Video_link, ec_course.Appr_Status, ec_course.Fees, ec_course.Rating, ec_course.Rating_Count, ed_institution.Inst_Name " &
                          "FROM ec_course " &
                          "INNER JOIN ed_institution On ec_course.Affiliation = ed_institution.Inst_ID " &
                          "INNER JOIN ec_studentcourse On ec_course.Course_ID = ec_studentcourse.Course_ID " &
                          "WHERE ec_studentcourse.Student_ID = @studentId And ec_studentcourse.Completion_Status = 'In-Progress'"

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
                          "LEFT JOIN ec_coursecontent ON ec_coursecontent.Course_ID = ec_studentcourse.Course_ID  where ec_studentcourse.Student_ID = @stuid " &
                          "GROUP BY ec_studentcourse.Course_ID " &
                          "ORDER BY ec_studentcourse.Course_ID"

        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@stuid", studentID)
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

    Public Function CompleteCourse(ByVal certname As String, ByVal instid As Integer, ByVal studentID As Integer, ByVal courseID As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim query As String = "UPDATE ec_studentcourse SET Completion_Status = 'Completed' WHERE Student_ID = @studentID AND Course_ID = @courseID"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@StudentID", studentID)
        cmd.Parameters.AddWithValue("@CourseID", courseID)
        cmd.ExecuteNonQuery()
        Con.Close()
        GenerateCertificateAndSave(instid, studentID, "E-Course", DateTime.Now.Year, courseID, certname)
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
        UpdateCourseRating(courseID)
    End Function

    Public Function UpdateCourseRating(ByVal courseID As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        ' Calculate the average rating for the course
        Dim avgRating As Double = 0
        Dim ratingCount As Integer = 0

        Dim queryAvgRating As String = "SELECT AVG(Rating) AS AvgRating, COUNT(Rating) AS RatingCount FROM ec_studentcourse WHERE Course_ID = @CourseID AND Rating IS NOT NULL"
        Using cmdAvgRating As New MySqlCommand(queryAvgRating, Con)
            cmdAvgRating.Parameters.AddWithValue("@CourseID", courseID)
            Using reader As MySqlDataReader = cmdAvgRating.ExecuteReader()
                If reader.Read() Then
                    avgRating = Convert.ToDouble(reader("AvgRating"))
                    ratingCount = Convert.ToInt32(reader("RatingCount"))
                End If
            End Using
        End Using

        ' Update the ec_course table with the average rating and rating count
        Dim queryUpdateRating As String = "UPDATE ec_course SET Rating = @AvgRating, Rating_Count = @RatingCount WHERE Course_ID = @CourseID"
        Using cmdUpdateRating As New MySqlCommand(queryUpdateRating, Con)
            cmdUpdateRating.Parameters.AddWithValue("@AvgRating", avgRating)
            cmdUpdateRating.Parameters.AddWithValue("@RatingCount", ratingCount)
            cmdUpdateRating.Parameters.AddWithValue("@CourseID", courseID)
            cmdUpdateRating.ExecuteNonQuery()
        End Using

        Con.Close()
    End Function


    Public Sub GenerateCertificateAndSave(instid As Integer, studentID As Integer, CertType As String, year As Integer, courseID As Integer, certName As String)
        Using Con = Globals.GetDBConnection()
            Con.Open()
            Dim query As String = "INSERT INTO ed_certificates (Inst_ID, CertName, Student_ID, Type, Year, Course_ID) VALUES (@instid, @certName, @studentID, @Type, @year, @courseID)"
            Dim cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@instid", instid)
            cmd.Parameters.AddWithValue("@certName", certName)
            cmd.Parameters.AddWithValue("@studentID", studentID)
            cmd.Parameters.AddWithValue("@courseID", courseID)
            cmd.Parameters.AddWithValue("@year", year)
            cmd.Parameters.AddWithValue("@Type", "E-Course")
            cmd.ExecuteNonQuery()
            Con.Close()
        End Using
    End Sub

    Public Function GetCertificates(ByVal studentID As Integer) As List(Of CertificateData)
        ' Get the database connection
        Dim Con = Globals.GetDBConnection()

        ' Open the database connection
        Con.Open()

        ' List to store CertificateData objects
        Dim certificates As New List(Of CertificateData)()

        ' SQL query to select certificates from the database
        Dim query As String = "SELECT * FROM ed_certificates WHERE Student_ID = @studentID AND Type = 'E-Course'"

        ' Create a MySqlCommand object
        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@studentID", studentID)

            ' Execute the SQL command
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                ' Iterate through the results
                While reader.Read()
                    ' Create a new CertificateData object
                    Dim certData As New CertificateData()

                    ' Set properties of CertificateData object
                    certData.Inst_ID = If(Not IsDBNull(reader("Inst_ID")), Convert.ToInt32(reader("Inst_ID")), 0)
                    certData.Student_ID = If(Not IsDBNull(reader("Student_ID")), Convert.ToInt32(reader("Student_ID")), 0)
                    certData.Type = If(Not IsDBNull(reader("Type")), reader("Type").ToString(), String.Empty)
                    certData.sClass = If(Not IsDBNull(reader("Class")), Convert.ToInt32(reader("Class")), 0)
                    certData.sSem = If(Not IsDBNull(reader("Sem")), Convert.ToInt32(reader("Sem")), 0)
                    certData.Year = If(Not IsDBNull(reader("Year")), Convert.ToInt32(reader("Year")), 0)
                    certData.Certificate = If(Not IsDBNull(reader("Certificate")), DirectCast(reader("Certificate"), Byte()), Nothing)
                    certData.Course_ID = If(Not IsDBNull(reader("Course_ID")), Convert.ToInt32(reader("Course_ID")), 0)
                    certData.CertName = If(Not IsDBNull(reader("CertName")), reader("CertName").ToString(), "NO NAME")


                    ' Add the CertificateData object to the list
                    certificates.Add(certData)
                End While
            End Using
        End Using

        ' Close the database connection
        Con.Close()

        ' Return the list of CertificateData objects
        Return certificates

    End Function

    Public Function GetPendingCourses() As Course()
        Dim Con = Globals.GetDBConnection()
        Con.Open()
        Dim pendingCourses As New List(Of Course)()

        Dim query As String = "SELECT ec_course.Course_ID, ec_course.Affiliation, ec_course.Name, ec_course.Category, ec_course.Teacher_Name, ec_course.Teacher_ID, ec_course.SYLLABUS, ec_course.Intro_Video_link, ec_course.Appr_Status, ec_course.Fees, ec_course.Rating, ec_course.Rating_Count, ed_institution.Inst_Name " &
                          "FROM ec_course " &
                          "INNER JOIN ed_institution ON ec_course.Affiliation = ed_institution.Inst_ID " &
                          "WHERE ec_course.Appr_Status = 'pending'"

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

            pendingCourses.Add(course)
        End While

        reader.Close()
        Con.Close()

        Return pendingCourses.ToArray()
    End Function

    Public Sub UpdateApprovalStatusToApproved(ByVal courseId As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim query As String = "UPDATE ec_course SET Appr_Status = 'Approved' WHERE Course_ID = @courseId"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@courseId", courseId)

        cmd.ExecuteNonQuery()
        Con.Close()
    End Sub

    Public Sub UpdateApprovalStatusToRejected(ByVal courseId As Integer)
        Dim Con = Globals.GetDBConnection()
        Con.Open()

        Dim query As String = "UPDATE ec_course SET Appr_Status = 'Rejected' WHERE Course_ID = @courseId"
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@courseId", courseId)

        cmd.ExecuteNonQuery()
        Con.Close()
    End Sub

    Public Function CheckRecordExists(ByVal studentID As Integer, ByVal courseID As Integer) As Integer
        Dim recordExists As Integer = 0 ' Default value if record doesn't exist

        ' Create a database connection
        Dim Con = Globals.GetDBConnection()

        ' Open the database connection
        Con.Open()

        ' SQL query to check if record exists
        Dim query As String = "SELECT EXISTS (" &
                          "SELECT 1 " &
                          "FROM ec_studentcourse " &
                          "WHERE Student_ID = @studentID AND Course_ID = @courseID" &
                          ") AS RecordExists"

        ' Create a MySqlCommand object
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@studentID", studentID)
        cmd.Parameters.AddWithValue("@courseID", courseID)

        ' Execute the SQL command
        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        ' Check if any rows are returned
        If reader.Read() Then
            ' Get the value of RecordExists column (0 or 1)
            recordExists = If(reader("RecordExists") IsNot DBNull.Value, Convert.ToInt32(reader("RecordExists")), 0)
        End If

        ' Close the database connection
        Con.Close()

        ' Return 1 if record exists, otherwise return 0
        Return recordExists
    End Function

    Public Function InsertStudentCourseRecord(ByVal studentID As Integer, ByVal courseID As Integer, ByVal completionStatus As String, ByVal feeamt As Integer, paidon As Date, startdate As Date) As Boolean
        ' Create a variable to track whether the insertion was successful
        Dim success As Boolean = False

        ' Create a database connection
        Dim Con = Globals.GetDBConnection()

        ' Open the database connection
        Con.Open()

        ' SQL query to insert a record into ec_studentcourse table
        Dim query As String = "INSERT INTO ec_studentcourse (Student_ID, Course_ID, Completion_Status, Fee_Amt, Paid_On, Start_Date) " &
                          "VALUES (@studentID, @courseID, @completionStatus, @fee, @paidon, @startdate)"

        ' Create a MySqlCommand object
        Dim cmd As New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@studentID", studentID)
        cmd.Parameters.AddWithValue("@courseID", courseID)
        cmd.Parameters.AddWithValue("@completionStatus", completionStatus)
        cmd.Parameters.AddWithValue("@paidon", paidon)
        cmd.Parameters.AddWithValue("@fee", feeamt)
        cmd.Parameters.AddWithValue("@startdate", startdate)

        ' Execute the SQL command
        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

        ' Check if any rows were affected (successful insertion)
        If rowsAffected > 0 Then
            success = True
        End If

        ' Close the database connection
        Con.Close()

        ' Return the success status
        Return success
    End Function

    Public Function GetInstituteAndCourseName(courseID As Integer) As Tuple(Of String, String)
        Dim instituteName As String = ""
        Dim courseName As String = ""

        Try
            Using con As MySqlConnection = Globals.GetDBConnection()
                con.Open()
                Dim query As String = "SELECT ed_institution.Inst_Name, ec_course.Name " &
                                  "FROM ec_course " &
                                  "INNER JOIN ed_institution ON ec_course.Affiliation = ed_institution.Inst_ID " &
                                  "WHERE ec_course.Course_ID = @CourseID"

                Using command As New MySqlCommand(query, con)
                    command.Parameters.AddWithValue("@CourseID", courseID)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            instituteName = reader.GetString("Inst_Name")
                            courseName = reader.GetString("Name")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Could not fetch Course/Institute name")
        End Try

        Return New Tuple(Of String, String)(instituteName, courseName)
    End Function







End Class
