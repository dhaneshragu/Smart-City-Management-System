Imports System.Data.SqlClient
Public Class HealthcareAdminHome

    Public Property uid As Integer = 10
    Public Property u_name As String = "AdminHC"

    Private healthcareAssignDoctorAdmin As HealthcareAssignDoctorAdmin = Nothing
    Private healthcareHealthRecordAdmin As HealthcareHealthRecordAdmin = Nothing
    Private healthcareApproveAppointmentAdmin As HealthcareApproveAppointmentAdmin = Nothing
    Private healthcareBirthDeathCertificateAdmin As HealthcareBirthDeathCertificateAdmin = Nothing
    Private healthcareManageDoctorAdmin As HealthcareManageDoctorAdmin = Nothing
    Private healthcareHedalthRecordAdmin As HealthcareHealthRecordAdmin = Nothing
    Private healthcareHealtfhRecordAdmin As HealthcareHealthRecordAdmin = Nothing
    Public innerPanel As Panel

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub


    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        healthcareAssignDoctorAdmin?.Dispose()
        healthcareAssignDoctorAdmin = New HealthcareAssignDoctorAdmin()
        Globals.viewChildForm(innerPanel, healthcareAssignDoctorAdmin)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        healthcareApproveAppointmentAdmin?.Dispose()
        healthcareApproveAppointmentAdmin = New HealthcareApproveAppointmentAdmin()
        Globals.viewChildForm(innerPanel, healthcareApproveAppointmentAdmin)
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        healthcareManageDoctorAdmin?.Dispose()
        healthcareManageDoctorAdmin = New HealthcareManageDoctorAdmin()
        Globals.viewChildForm(innerPanel, healthcareManageDoctorAdmin)
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        healthcareHealthRecordAdmin?.Dispose()
        healthcareHealthRecordAdmin = New HealthcareHealthRecordAdmin()
        Globals.viewChildForm(innerPanel, healthcareHealthRecordAdmin)
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        healthcareBirthDeathCertificateAdmin?.Dispose()
        healthcareBirthDeathCertificateAdmin = New HealthcareBirthDeathCertificateAdmin()
        Globals.viewChildForm(innerPanel, healthcareBirthDeathCertificateAdmin)
    End Sub
End Class