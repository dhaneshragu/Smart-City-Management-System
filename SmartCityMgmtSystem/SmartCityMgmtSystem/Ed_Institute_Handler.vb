Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports MySql.Data.MySqlClient
Imports SmartCityMgmtSystem.Ed_GlobalDashboard

Public Class Ed_Institute_Handler
    Public Class EdInstitution
        Public Property Inst_ID As Integer
        Public Property Inst_Name As String
        Public Property Inst_Address As String
        Public Property Inst_PrincipalID As Integer
        Public Property Inst_PrincipalName As String ' New property for principal's name
        Public Property Inst_PrincipalContact As String ' New property for principal's contact
        Public Property Inst_PrincipalEmail As String ' New property for principal's email
        Public Property Inst_Photo As Byte() ' Byte array for blob data
        Public Property Inst_Type As String ' Assuming you only need the string representation of enum


        ' Constructor to initialize the class
        Public Sub New(ByVal id As Integer, ByVal name As String, ByVal address As String, ByVal principalID As Integer, ByVal principalName As String, ByVal principalContact As String, ByVal principalEmail As String, ByVal photo As Byte(), ByVal type As String)
            Me.Inst_ID = id
            Me.Inst_Name = name
            Me.Inst_Address = address
            Me.Inst_PrincipalID = principalID
            Me.Inst_PrincipalName = principalName
            Me.Inst_PrincipalContact = principalContact
            Me.Inst_PrincipalEmail = principalEmail
            Me.Inst_Photo = photo
            Me.Inst_Type = type
        End Sub
    End Class
    Public Class Admissions
        Public Property StudentID As Integer
        Public Property StudentName As String
        Public Property RecentActivity As String
        Public Property DateOfBirth As Date
        Public Property ContactNo As String
        Public Property EmailAddress As String
        Public Property InstituteID As Integer
        Public Property Year As Integer
    End Class



    Public Function GetAllInstitutions() As EdInstitution()
        Dim institutions As New List(Of EdInstitution)()

        Dim connectionString As String = GetDBConnectionString()

        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "SELECT ed_inst.Inst_ID, ed_inst.Inst_Name, ed_inst.Inst_Address, ed_inst.Inst_Principal, ed_inst.Inst_Photo, ed_inst.Inst_Type, users.name AS PrincipalName, users.phone_number AS PrincipalContact, users.email AS PrincipalEmail " &
                                  "FROM ed_institution AS ed_inst " &
                                  "LEFT JOIN users ON ed_inst.Inst_Principal = users.user_id"

            Using command As New MySqlCommand(query, connection)
                Using reader As MySqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim id As Integer = If(reader("Inst_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Inst_ID")), 0)
                        Dim name As String = If(reader("Inst_Name") IsNot DBNull.Value, Convert.ToString(reader("Inst_Name")), "")
                        Dim address As String = If(reader("Inst_Address") IsNot DBNull.Value, Convert.ToString(reader("Inst_Address")), "")
                        Dim principalID As Integer = If(reader("Inst_Principal") IsNot DBNull.Value, Convert.ToInt32(reader("Inst_Principal")), 0)
                        Dim photo As Byte() = Nothing ' Initialize to Nothing initially

                        ' Check for DBNull before attempting to cast
                        If Not reader.IsDBNull(reader.GetOrdinal("Inst_Photo")) Then
                            photo = DirectCast(reader("Inst_Photo"), Byte())
                        End If

                        Dim type As String = If(reader("Inst_Type") IsNot DBNull.Value, Convert.ToString(reader("Inst_Type")), "")
                        Dim principalName As String = If(reader("PrincipalName") IsNot DBNull.Value, Convert.ToString(reader("PrincipalName")), "")
                        Dim principalContact As String = If(reader("PrincipalContact") IsNot DBNull.Value, Convert.ToString(reader("PrincipalContact")), "")
                        Dim principalEmail As String = If(reader("PrincipalEmail") IsNot DBNull.Value, Convert.ToString(reader("PrincipalEmail")), "")

                        Dim institution As New EdInstitution(id, name, address, principalID, principalName, principalContact, principalEmail, photo, type)
                        institutions.Add(institution)
                    End While
                End Using
            End Using
        End Using

        Return institutions.ToArray()
    End Function


    Public Function GetAllRequests(principalID As Integer) As Admissions()
        Dim requests As New List(Of Admissions)()

        Dim connectionString As String = GetDBConnectionString()

        Dim query As String = "SELECT ea.Student_ID, u.name AS StudentName, ep.Ed_LastEduQlf AS RecentActivity, u.dob AS DateOfBirth, u.phone_number AS ContactNo, u.email AS EmailAddress, ea.Inst_ID AS InstituteID, ea.Year " &
               "FROM ed_admission AS ea " &
               "INNER JOIN users AS u ON ea.Student_ID = u.user_id " &
               "INNER JOIN ed_profile AS ep ON ea.Student_ID = ep.Ed_User_ID " &
               "WHERE ea.Inst_ID IN (SELECT Inst_ID FROM ed_institution WHERE Inst_Principal = @PrincipalID) AND ea.Appr_Status = 'Pending'"

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@PrincipalID", principalID)
                Using reader As MySqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim admission As New Admissions()
                        admission.StudentID = If(reader("Student_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Student_ID")), 0)
                        admission.StudentName = If(reader("StudentName") IsNot DBNull.Value, Convert.ToString(reader("StudentName")), "")
                        admission.RecentActivity = If(reader("RecentActivity") IsNot DBNull.Value, Convert.ToString(reader("RecentActivity")), "")
                        admission.DateOfBirth = If(reader("DateOfBirth") IsNot DBNull.Value, Convert.ToDateTime(reader("DateOfBirth")), DateTime.MinValue)
                        admission.ContactNo = If(reader("ContactNo") IsNot DBNull.Value, Convert.ToString(reader("ContactNo")), "")
                        admission.EmailAddress = If(reader("EmailAddress") IsNot DBNull.Value, Convert.ToString(reader("EmailAddress")), "")
                        admission.InstituteID = If(reader("InstituteID") IsNot DBNull.Value, Convert.ToInt32(reader("InstituteID")), 0)
                        admission.Year = If(reader("Year") IsNot DBNull.Value, Convert.ToInt32(reader("Year")), 0)
                        requests.Add(admission)
                    End While
                End Using
            End Using
        End Using

        Return requests.ToArray()
    End Function



    Public Shared Function GetDBConnectionString() As String
        ' Call the method from Globals.vb to get the connection string
        Return Globals.getdbConnectionString()
    End Function
    Public Function GetLastStudentRequest(studentID As Integer) As (Status As String, InstituteID As Integer)
        Dim status As String = ""
        Dim instituteID As Integer = 0

        Dim connectionString As String = GetDBConnectionString()
        Dim query As String = "SELECT Appr_Status, Inst_ID FROM ed_admission WHERE Student_ID = @StudentID ORDER BY Date DESC LIMIT 1"

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@StudentID", studentID)
                Using reader As MySqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        status = If(reader("Appr_Status") IsNot DBNull.Value, Convert.ToString(reader("Appr_Status")), "")
                        instituteID = If(reader("Inst_ID") IsNot DBNull.Value, Convert.ToInt32(reader("Inst_ID")), 0)
                    End If
                End Using
            End Using
        End Using

        Return (status, instituteID)
    End Function

    Public Sub InsertCertificate(certData As CertificateData)
        ' SQL query to insert data into the database
        Using Con = Globals.GetDBConnection()
            Con.Open()
            Dim query As String = "INSERT INTO ed_certificates (Inst_ID, Student_ID, Type, Class, Sem, Year, Certificate) " &
                              "VALUES (@Inst_ID, @Student_ID, @Type, @Class, @Sem, @Year, @Certificate)"

            ' Create a MySqlCommand object with the SQL query and connection
            Using command As New MySqlCommand(query, Con)
                ' Add parameters to the command
                command.Parameters.AddWithValue("@Inst_ID", certData.Inst_ID)
                command.Parameters.AddWithValue("@Student_ID", certData.Student_ID)
                command.Parameters.AddWithValue("@Type", certData.Type)
                command.Parameters.AddWithValue("@Class", certData.sClass)
                command.Parameters.AddWithValue("@Sem", certData.sSem)
                command.Parameters.AddWithValue("@Year", certData.Year)
                command.Parameters.AddWithValue("@Certificate", certData.Certificate)
                Dim rowsAffected As Integer = command.ExecuteNonQuery()

            End Using
        End Using
    End Sub

    Public Function GetCertificatesByType(ByVal studentID As Integer, TypeOfCert As String) As List(Of CertificateData)
        ' Get the database connection
        Dim Con = Globals.GetDBConnection()

        ' Open the database connection
        Con.Open()

        ' List to store CertificateData objects
        Dim certificates As New List(Of CertificateData)()

        ' SQL query to select certificates from the database
        Dim query As String = "SELECT * FROM ed_certificates WHERE Student_ID = @studentID AND Type = @Typeofcert"

        ' Create a MySqlCommand object
        Using cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@studentID", studentID)
            cmd.Parameters.AddWithValue("@Typeofcert", TypeOfCert)

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


End Class
