Imports System.Data.SqlClient
Public Class HealthcareAdminHome

    Public Property uid As Integer = 10
    Public Property u_name As String = "AdminHC"

    Private healthcareAssignDoctorAdmin As HealthcareAssignDoctorAdmin = Nothing
    Private healthcareHealthRecordAdmin As HealthcareHealthRecordAdmin = Nothing
    Private healthcareApproveAppointmentAdmin As HealthcareApproveAppointmentAdmin = Nothing
    Private healthcareBirthDeathCertificateAdmin As HealthcareBirthDeathCertificateAdmin = Nothing
    Private healthcareManageDoctorAdmin As HealthcareManageDoctorAdmin = Nothing
    Private healthcareManageStaffAdmn As HealthcareManageStaffAdmn = Nothing
    Private healthcareManageAmbulanceAdmin As HealthcareManageAmbulanceAdmin = Nothing
    Private healthcareMedicineInventoryAdmin As HealthcareMedicineInventoryAdmin = Nothing
    Private healthcareManageBloodAppointmentAdmin As HealthcareManageBloodAppointmentAdmin = Nothing
    Private healthcareManageBloodBankAdmin As HealthcareManageBloodBankAdmin = Nothing
    Private healthcareSendNotif1 As HealthcareSendNotiff = Nothing
    Public innerPanel As Panel


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

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        healthcareManageStaffAdmn?.Dispose()
        healthcareManageStaffAdmn = New HealthcareManageStaffAdmn()
        Globals.viewChildForm(innerPanel, healthcareManageStaffAdmn)
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        healthcareManageAmbulanceAdmin?.Dispose()
        healthcareManageAmbulanceAdmin = New HealthcareManageAmbulanceAdmin()
        Globals.viewChildForm(innerPanel, healthcareManageAmbulanceAdmin)
    End Sub

    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        healthcareMedicineInventoryAdmin?.Dispose()
        healthcareMedicineInventoryAdmin = New HealthcareMedicineInventoryAdmin()
        Globals.viewChildForm(innerPanel, healthcareMedicineInventoryAdmin)
    End Sub

    Private Sub PictureBox13_Click(sender As Object, e As EventArgs) Handles PictureBox13.Click
        healthcareManageBloodAppointmentAdmin?.Dispose()
        healthcareManageBloodAppointmentAdmin = New HealthcareManageBloodAppointmentAdmin()
        Globals.viewChildForm(innerPanel, healthcareManageBloodAppointmentAdmin)
    End Sub

    Private Sub PictureBox12_Click(sender As Object, e As EventArgs) Handles PictureBox12.Click
        healthcareManageBloodBankAdmin?.Dispose()
        healthcareManageBloodBankAdmin = New HealthcareManageBloodBankAdmin()
        Globals.viewChildForm(innerPanel, healthcareManageBloodBankAdmin)
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
        'All admins can view notification screen
        healthcareSendNotif1?.Dispose()
        healthcareSendNotif1 = New HealthcareSendNotiff()
        Globals.viewChildForm(innerPanel, healthcareSendNotif1)
    End Sub
End Class