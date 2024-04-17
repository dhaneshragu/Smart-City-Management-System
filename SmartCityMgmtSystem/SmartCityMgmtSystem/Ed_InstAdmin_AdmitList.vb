Imports System.Data.SqlClient
Imports System.Security
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports SmartCityMgmtSystem.Ed_Institute_Handler
Public Class Ed_InstAdmin_AdmitList
    Dim handler As New Ed_Institute_Handler()
    Dim admissions As Admissions()
    Private Sub DisplayAdm(admissions As Admissions())
        ' Clear existing institute items from the FlowLayoutPanel
        FlowLayoutPanel1.Controls.Clear()


        ' Add admission items to the FlowLayoutPanel
        For Each admission As Admissions In admissions

            Dim listItem As New Ed_Stud_ListItem()
            listItem.instituteID = admission.InstituteID
            listItem.studentID = admission.StudentID
            listItem.year = admission.Year
            ' Set properties only if length is greater than 0
            If Not String.IsNullOrEmpty(admission.StudentName) AndAlso admission.StudentName.Length > 0 Then
                listItem.Label6.Text = admission.StudentName
            End If

            If admission.DateOfBirth <> DateTime.MinValue Then
                listItem.Label4.Text = admission.DateOfBirth.ToString("yyyy-MM-dd")
            End If

            If Not String.IsNullOrEmpty(admission.ContactNo) AndAlso admission.ContactNo.Length > 0 Then
                listItem.Label2.Text = admission.ContactNo
            End If

            If Not String.IsNullOrEmpty(admission.EmailAddress) AndAlso admission.EmailAddress.Length > 0 Then
                listItem.Label3.Text = admission.EmailAddress
            End If

            FlowLayoutPanel1.Controls.Add(listItem)
        Next
    End Sub
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Call the GetAllRequests function to get the admission list
        admissions = handler.GetAllRequests(Ed_GlobalDashboard.Ed_Profile.Ed_User_ID)
        DisplayAdm(admissions)



    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim searchText As String = TextBox1.Text.Trim() ' Get the search text from the textbox

        ' Filter institues based on the matching institute  name'
        Dim filteredInstitutes As Admissions() = admissions.Where(Function(admission) admission.StudentName.Contains(searchText)).ToArray()
        DisplayAdm(filteredInstitutes)
    End Sub

    ' Add the GetAllRequests function here
End Class
