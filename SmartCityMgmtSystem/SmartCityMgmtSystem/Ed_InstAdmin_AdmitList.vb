Imports System.Data.SqlClient
Imports System.Security
Imports SmartCityMgmtSystem.Ed_Institute_Handler
Public Class Ed_InstAdmin_AdmitList
    Dim handler As New Ed_Institute_Handler()

    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Call the GetAllRequests function to get the admission list
        Dim admissions As Admissions() = handler.GetAllRequests(Ed_GlobalDashboard.Ed_Profile.Ed_User_ID)

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

            If Not String.IsNullOrEmpty(admission.RecentActivity) AndAlso admission.RecentActivity.Length > 0 Then
                listItem.Label1.Text = admission.RecentActivity
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

    ' Add the GetAllRequests function here
End Class
