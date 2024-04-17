Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Ed_Coursera_Handler
Imports SmartCityMgmtSystem.Ed_Institute_Handler
Public Class Ed_Minister_Institute_List

    Dim handler As New Ed_Institute_Handler()

    ' Fetch institutes data from the database
    Dim institutes As EdInstitution() = handler.GetAllInstitutions()

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

    Private Sub DisplayInst(institutes As EdInstitution())
        ' Clear existing institute items from the FlowLayoutPanel
        FlowLayoutPanel1.Controls.Clear()


        ' Create and populate Ed_Institute_ListItem controls
        For Each institute As EdInstitution In institutes
            Dim listItem As New Ed_Minister_Institute_ListItem()
            listItem.instituteID = institute.Inst_ID
            listItem.callingPanel = callingPanel

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


        DisplayInst(institutes)


    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim searchText As String = TextBox1.Text.Trim() ' Get the search text from the textbox

        ' Filter institues based on the matching institute  name'
        Dim filteredInstitutes As EdInstitution() = institutes.Where(Function(institute) institute.Inst_Name.Contains(searchText)).ToArray()
        DisplayInst(filteredInstitutes)
    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub

End Class