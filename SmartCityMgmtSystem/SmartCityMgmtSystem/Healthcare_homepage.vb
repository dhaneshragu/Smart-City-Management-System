Public Class Healthcare_homepage

    'To be passed from Login Dashboard
    Public Property uid As Integer = 10

    Public Property u_name As String = "AdminHC"
    Private healthcare_BookAppointment As Healthcare_BookAppointment = Nothing
    Private healthcareAdminHome As HealthcareAdminHome = Nothing
    Private healthcare_DonateBlood As Healthcare_DonateBlood = Nothing
    Private healthcare_Pharmacy As Healthcare_Pharmacy = Nothing
    Private healthcare_History As Healthcare_History = Nothing
    Private healthcare_Emergency As Healthcare_Emergency = Nothing

    Private Sub book_appointment_Click(sender As Object, e As EventArgs) Handles book_appointment.Click
        ' Navigate to hc_B_Appointment page
        healthcare_BookAppointment?.Dispose()
        healthcare_BookAppointment = New Healthcare_BookAppointment()
        Globals.viewChildForm(childformPanel, healthcare_BookAppointment)
        book_appointment.BackColor = Color.FromArgb(0, 180, 0)
        donate_blood.BackColor = Color.FromArgb(88, 133, 175)
        history.BackColor = Color.FromArgb(88, 133, 175)
        pharmacy.BackColor = Color.FromArgb(88, 133, 175)
        emergency.BackColor = Color.Red
        hc_admin.BackColor = Color.FromArgb(88, 133, 175)
    End Sub

    Private Sub donate_blood_Click(sender As Object, e As EventArgs) Handles donate_blood.Click
        healthcare_DonateBlood?.Dispose()
        healthcare_DonateBlood = New Healthcare_DonateBlood()
        Globals.viewChildForm(childformPanel, healthcare_DonateBlood)
        book_appointment.BackColor = Color.FromArgb(88, 133, 175)
        donate_blood.BackColor = Color.FromArgb(0, 180, 0)
        history.BackColor = Color.FromArgb(88, 133, 175)
        pharmacy.BackColor = Color.FromArgb(88, 133, 175)
        emergency.BackColor = Color.Red
        hc_admin.BackColor = Color.FromArgb(88, 133, 175)
    End Sub

    Private Sub history_Click(sender As Object, e As EventArgs) Handles history.Click
        healthcare_History?.Dispose()
        healthcare_History = New Healthcare_History()
        Globals.viewChildForm(childformPanel, healthcare_History)
        book_appointment.BackColor = Color.FromArgb(88, 133, 175)
        donate_blood.BackColor = Color.FromArgb(88, 133, 175)
        history.BackColor = Color.FromArgb(0, 180, 0)
        pharmacy.BackColor = Color.FromArgb(88, 133, 175)
        emergency.BackColor = Color.Red
        hc_admin.BackColor = Color.FromArgb(88, 133, 175)
    End Sub

    Private Sub pharmacy_Click(sender As Object, e As EventArgs) Handles pharmacy.Click
        healthcare_Pharmacy?.Dispose()
        healthcare_Pharmacy = New Healthcare_Pharmacy()
        Globals.viewChildForm(childformPanel, healthcare_Pharmacy)
        book_appointment.BackColor = Color.FromArgb(88, 133, 175)
        donate_blood.BackColor = Color.FromArgb(88, 133, 175)
        history.BackColor = Color.FromArgb(88, 133, 175)
        pharmacy.BackColor = Color.FromArgb(0, 180, 0)
        emergency.BackColor = Color.Red
        hc_admin.BackColor = Color.FromArgb(88, 133, 175)
    End Sub

    Private Sub emergency_Click(sender As Object, e As EventArgs) Handles emergency.Click
        ' Navigate to hc_Emergency page (if you have one)
        ' Add similar logic for emergency page if needed
        healthcare_Emergency?.Dispose()
        healthcare_Emergency = New Healthcare_Emergency()
        Globals.viewChildForm(childformPanel, healthcare_Emergency)
        book_appointment.BackColor = Color.FromArgb(88, 133, 175)
        donate_blood.BackColor = Color.FromArgb(88, 133, 175)
        history.BackColor = Color.FromArgb(88, 133, 175)
        pharmacy.BackColor = Color.FromArgb(88, 133, 175)
        emergency.BackColor = Color.FromArgb(0, 180, 0)
        hc_admin.BackColor = Color.FromArgb(88, 133, 175)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim home = New HomePageDashboard() With {
            .uid = uid
        }
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
        book_appointment.BackColor = Color.FromArgb(88, 133, 175)
        donate_blood.BackColor = Color.FromArgb(88, 133, 175)
        history.BackColor = Color.FromArgb(88, 133, 175)
        pharmacy.BackColor = Color.FromArgb(88, 133, 175)
        hc_admin.BackColor = Color.FromArgb(0, 180, 0)
        emergency.BackColor = Color.Red
    End Sub

    Private Sub Healthcare_homepage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = u_name
    End Sub


End Class