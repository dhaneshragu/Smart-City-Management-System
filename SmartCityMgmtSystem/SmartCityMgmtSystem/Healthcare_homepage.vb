Imports MySql.Data.MySqlClient

Public Class Healthcare_homepage

    'To be passed from Login Dashboard
    Public Property uid As Integer = 130
    Public Property patient_id As Integer = 1
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
        healthcare_BookAppointment = New Healthcare_BookAppointment() With {
            .uid = patient_id
        }
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
        healthcare_DonateBlood = New Healthcare_DonateBlood() With {
            .uid = patient_id
        }
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
        healthcare_History = New Healthcare_History() With {
            .uid = patient_id
        }
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
        healthcare_Pharmacy = New Healthcare_Pharmacy() With {
            .uid = patient_id
        }
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
        healthcare_Emergency = New Healthcare_Emergency() With {
            .uid = patient_id
        }
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
        If uid = 4 Then
            hc_admin.Visible = True
        End If

        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT * FROM patient WHERE uid = @uid", Con)
        cmd.Parameters.AddWithValue("@uid", uid)
        reader = cmd.ExecuteReader()

        'Fill the DataTable with data from the SQL table
        If reader.HasRows Then
            While reader.Read()
                Dim Value As Integer = reader("patient_id").ToString()
                patient_id = Value
            End While
            Con.Close()
        Else
            Con.Close()
            Dim stmnt As String = "INSERT INTO patient VALUES (NULL, @uid)"

            cmd = New MySqlCommand(stmnt, con)
            cmd.Parameters.AddWithValue("@uid", uid)
            Try
                Con.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try

            cmd = New MySqlCommand("SELECT * FROM patient WHERE uid = @uid", Con)
            cmd.Parameters.AddWithValue("@uid", uid)
            reader = cmd.ExecuteReader()

            If reader.HasRows Then
                While reader.Read()
                    Dim Value As Integer = reader("patient_id").ToString()
                    patient_id = Value
                End While
            End If
            Con.Close()
        End If


        Label2.Text = u_name
    End Sub


End Class