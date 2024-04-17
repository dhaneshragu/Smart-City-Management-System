Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
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
    Public Property status As String
    Public Property instituteID As Integer
    Dim institutes As EdInstitution()
    Private Sub DisplayInst(institutes As EdInstitution())
        ' Clear existing institute items from the FlowLayoutPanel
        FlowLayoutPanel1.Controls.Clear()


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
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Fetch institutes data from the database
        institutes = handler.GetAllInstitutions()
        Dim result = handler.GetLastStudentRequest(Ed_GlobalDashboard.Ed_Profile.Ed_User_ID)
        Dim selectedInstitute As String = ""

        For Each inst In institutes
            If inst.Inst_ID = Ed_GlobalDashboard.Ed_Profile.Ed_Affiliation Then
                selectedInstitute = inst.Inst_Name
                Exit For
            End If
        Next

        ' Set Label2.Text to the name of the selected institute
        Label2.Text = selectedInstitute
        status = result.Status
        instituteID = result.InstituteID
        DisplayInst(institutes)
    End Sub
    Private Sub Edit_Label_Click(sender As Object, e As EventArgs)
        'Globals.viewChildForm(callingPanel, New Ed_Institute_Edit(callingPanel))
    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim searchText As String = TextBox1.Text.Trim() ' Get the search text from the textbox

        ' Filter institues based on the matching institute  name'
        Dim filteredInstitutes As EdInstitution() = institutes.Where(Function(institute) institute.Inst_Name.Contains(searchText)).ToArray()
        DisplayInst(filteredInstitutes)
    End Sub

End Class