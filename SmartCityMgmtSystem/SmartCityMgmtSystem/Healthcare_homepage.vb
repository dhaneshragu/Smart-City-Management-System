Public Class Healthcare_homepage

    'To be passed from Login Dashboard
    Public Property uid As Integer = 10
    Public Property u_name As String = "AdminHC"
    Private healthcare_BookAppointment As Healthcare_BookAppointment = Nothing
    Private healthcareAdminHome As HealthcareAdminHome = Nothing
    Private healthcare_DonateBlood As Healthcare_DonateBlood = Nothing
    Private healthcare_Pharmacy As Healthcare_Pharmacy = Nothing
    Private healthcare_History As Healthcare_History = Nothing

    Private Sub book_appointment_Click(sender As Object, e As EventArgs) Handles book_appointment.Click
        ' Navigate to hc_B_Appointment page
        healthcare_BookAppointment?.Dispose()
        healthcare_BookAppointment = New Healthcare_BookAppointment()
        Globals.viewChildForm(childformPanel, healthcare_BookAppointment)
    End Sub

    Private Sub donate_blood_Click(sender As Object, e As EventArgs) Handles donate_blood.Click
        healthcare_DonateBlood?.Dispose()
        healthcare_DonateBlood = New Healthcare_DonateBlood()
        Globals.viewChildForm(childformPanel, healthcare_DonateBlood)
    End Sub

    Private Sub history_Click(sender As Object, e As EventArgs) Handles history.Click
        healthcare_History?.Dispose()
        healthcare_History = New Healthcare_History()
        Globals.viewChildForm(childformPanel, healthcare_History)
    End Sub

    Private Sub pharmacy_Click(sender As Object, e As EventArgs) Handles pharmacy.Click
        healthcare_Pharmacy?.Dispose()
        healthcare_Pharmacy = New Healthcare_Pharmacy()
        Globals.viewChildForm(childformPanel, healthcare_Pharmacy)
    End Sub

    Private Sub emergency_Click(sender As Object, e As EventArgs) Handles emergency.Click
        ' Navigate to hc_Emergency page (if you have one)
        ' Add similar logic for emergency page if needed
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim home = New HomePageDashboard
        home.Show()
        Me.Close()
    End Sub

    Private Sub hc_admin_Click(sender As Object, e As EventArgs) Handles hc_admin.Click

        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        healthcareAdminHome?.Dispose()
        healthcareAdminHome = New HealthcareAdminHome With {
            .innerPanel = childformPanel
        }
        Globals.viewChildForm(childformPanel, healthcareAdminHome)
    End Sub

    Private Sub Healthcare_homepage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


End Class
