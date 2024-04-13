Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Ed_Institute_Handler
Public Class Ed_Institute_List

    Dim handler As New Ed_Institute_Handler()

    Private callingPanel As Panel
    Public Property user_type As String
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(panel As Panel, usertype As String)
        InitializeComponent()
        callingPanel = panel
        user_type = usertype
    End Sub

    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Get the database connection string
        Dim connectionString As String = Globals.getdbConnectionString()

        ' Fetch institutes data from the database
        Dim institutes As EdInstitution() = handler.GetAllInstitutions()
        Dim result = handler.GetLastStudentRequest(Ed_GlobalDashboard.Ed_Profile.Ed_User_ID)

        ' Now you can access the status and institute ID from the result
        Dim status As String = result.Status
        Dim instituteID As Integer = result.InstituteID

        ' Create and populate Ed_Institute_ListItem controls
        For Each institute As EdInstitution In institutes
            Dim listItem As New Ed_Institute_ListItem()
            listItem.instituteID = institute.Inst_ID
            If (Ed_GlobalDashboard.Ed_Profile.Ed_Affiliation > 0) Then
                listItem.Button2.Text = "Apply For Transfer"
            End If
            If listItem.instituteID = instituteID Then
                If status = "Pending" Then
                    listItem.Button2.Text = "Application Sent"
                    listItem.Button2.BackColor = Color.FromArgb(153, 102, 0)
                End If
                If status = "Approved" Then
                    listItem.Button2.Text = "Approved"
                    listItem.Button2.BackColor = Color.SeaGreen
                End If
            End If
            If institute.Inst_Name.Length > 0 Then
                listItem.Label6.Text = institute.Inst_Name
            End If

            If institute.Inst_Address.Length > 0 Then
                listItem.Label1.Text = institute.Inst_Address
            End If

            If institute.Inst_PrincipalContact.Length > 0 Then
                listItem.Label2.Text = institute.Inst_PrincipalContact
            End If

            If institute.Inst_PrincipalEmail.Length > 0 Then
                listItem.Label3.Text = institute.Inst_PrincipalEmail
            End If

            If institute.Inst_Type.Length > 0 Then
                listItem.Label4.Text = institute.Inst_Type
            End If

            ' Set institute image if Inst_Photo is not null and has data
            If institute.Inst_Photo IsNot Nothing AndAlso institute.Inst_Photo.Length > 0 Then
                Using ms As New System.IO.MemoryStream(institute.Inst_Photo)
                    listItem.PictureBox1.Image = Image.FromStream(ms)
                End Using
            End If


            ' Add controls to FlowLayoutPanel
            FlowLayoutPanel1.Controls.Add(listItem)
        Next
    End Sub
    Private Sub Edit_Label_Click(sender As Object, e As EventArgs)
        Globals.viewChildForm(callingPanel, New Ed_Institute_Edit(callingPanel))
    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub
End Class