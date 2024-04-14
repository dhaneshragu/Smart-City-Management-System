Imports MySql.Data.MySqlClient

Public Class Ed_GlobalDashboard
    Public innerpanel As Panel
    Public userID As Integer
    Public userName As String
    Private hasLoaded As Boolean = False
    Public PreviousVisibility As Boolean = False
    Public Structure Profile
        Public Ed_User_ID As Integer
        Public Ed_Username As String
        Public Ed_User_Type As UserType
        Public Ed_User_Role As UserRole?
        Public Ed_Affiliation As Integer?
        Public Ed_Name As String
        Public Ed_DOB As Date?
        Public Ed_Class As Integer?
        Public Ed_Sem As Integer?
        Public Ed_LastEduQlf As String
    End Structure

    Public Enum UserType
        Student
        Teacher
        Admin
        Others
    End Enum

    Public Enum UserRole
        Minister
        Principal
        EcourseAdmin
        Bus
        Security
    End Enum

    Public Class CertificateData
        Public Property Inst_ID As Integer
        Public Property Student_ID As Integer
        Public Property Type As String
        Public Property sClass As Integer
        Public Property sSem As Integer
        Public Property Year As Integer
        Public Property Certificate As Byte()
        Public Property Course_ID As Integer
        Public Property CertName As String
    End Class


    Public Ed_Profile As Profile
    Public Sub OpenFormInGlobalEdPanel(ByVal formToShow As Form)
        ' Clear the panel before adding a new form
        Panel1.Controls.Clear()

        ' Set the form properties
        formToShow.TopLevel = False
        formToShow.FormBorderStyle = FormBorderStyle.None
        formToShow.Dock = DockStyle.Fill

        ' Add the form to the panel
        Panel1.Controls.Add(formToShow)

        ' Show the form
        formToShow.Show()
    End Sub
    Private Sub Ed_GlobalDashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        hasLoaded = True
        Globals.viewChildForm(Panel1, New Ed_RoleSelect())
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
    Private Sub Ed_GlobalDashboard_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        ' Create a list to store references to forms to close
        Dim formsToClose As New List(Of Form)

        ' Identify forms to close and add them to the list
        For Each form As Form In Application.OpenForms
            If form IsNot Me Then
                formsToClose.Add(form)
            End If
        Next

        ' Close the identified forms
        For Each form As Form In formsToClose
            form.Close()
        Next
    End Sub
    Private Sub Form1_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Me.Visible AndAlso hasLoaded AndAlso Me.PreviousVisibility = False Then
            Globals.viewChildForm(Panel1, New Ed_RoleSelect())
        End If
        Me.PreviousVisibility = Me.Visible
    End Sub

    Public Function InsertCertificate(certData As CertificateData) As Boolean
        ' SQL query to insert data into the database
        Using Con = Globals.GetDBConnection()
            Con.Open()
            Dim query As String = "INSERT INTO ed_certificates (CertName, Inst_ID, Student_ID, Type, Class, Sem, Year, Certificate, Course_ID) " &
                              "VALUES (@CertName, @Inst_ID, @Student_ID, @Type, @Class, @Sem, @Year, @Certificate, @Course_ID)"

            ' Create a MySqlCommand object with the SQL query and connection
            Using command As New MySqlCommand(query, Con)
                ' Add parameters to the command
                command.Parameters.AddWithValue("@CertName", certData.CertName)
                command.Parameters.AddWithValue("@Inst_ID", certData.Inst_ID)
                command.Parameters.AddWithValue("@Student_ID", certData.Student_ID)
                command.Parameters.AddWithValue("@Type", certData.Type)
                command.Parameters.AddWithValue("@Class", certData.sClass)
                command.Parameters.AddWithValue("@Sem", certData.sSem)
                command.Parameters.AddWithValue("@Year", certData.Year)
                command.Parameters.AddWithValue("@Certificate", certData.Certificate)

                Try
                    ' Execute the SQL command
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    ' If one or more rows were affected, return true (insert successful)
                    Return rowsAffected > 0
                Catch ex As Exception
                    ' Handle exception
                    Console.WriteLine("Error inserting data into database: " & ex.Message)
                    Return False
                End Try
            End Using
        End Using
    End Function
End Class
