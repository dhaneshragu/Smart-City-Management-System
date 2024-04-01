Imports MySql.Data.MySqlClient

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

            content.ContentName = If(reader("Content_Name") IsNot DBNull.Value, reader("Content_Name").ToString(), "")
            content.ContentType = If(reader("Content_Type") IsNot DBNull.Value, reader("Content_Type").ToString(), "")
            content.VideoLink = If(reader("Video_Link") IsNot DBNull.Value, reader("Video_Link").ToString(), "")
            content.Content = If(reader("Content") IsNot DBNull.Value, reader("Content").ToString(), "")
            content.SeqNo = If(reader("Seq_no") IsNot DBNull.Value, Convert.ToInt32(reader("Seq_no")), 0)

            contents.Add(content)
        End While

        Return contents.ToArray()
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





End Class
