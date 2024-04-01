Public Class Ed_GlobalDashboard
    Public innerpanel As Panel
    Public userID As Integer
    Public userName As String
    Private hasLoaded As Boolean = False
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
    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If Not hasLoaded Then
            Globals.viewChildForm(Panel1, New Ed_RoleSelect())
        End If
    End Sub
End Class
