Imports System.Data.MySqlClient
Imports MySql.Data.MySqlClient
Public Class TransportationDashboard
    'To be passed from Login Dashboard
    Public Property uid As Integer = 1
    Public Property u_name As String = "Dhanesh"
    Private rideSharingMainForm As RideSharingMain = Nothing
    Private transportationAdminHome As TransportationAdminHome = Nothing
    Private transportBusSchedule As TransportBusSchedule = Nothing
    Private transportTollPlaza As TransportTollPlaza = Nothing
    Private transportdrivingLicenseReq As TransportDrivingLicenseReq = Nothing
    Private transportVehicleRegReq As TransportVehicleRegReq = Nothing
    Private transportEnterTG As TransportTGEnter = Nothing

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label3.Text = "Unique Identification No: " & uid
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        'Check Authentication and allow only the transport department admins to access this page
        Dim allowedUIDs As New List(Of Integer) From {5} ' Transportation minister
        Dim query As String = "SELECT DISTINCT uid FROM admin_officers"

        Try
            ' Assuming conn is your MySqlConnection object
            Using conn As New MySqlConnection(Globals.getdbConnectionString())
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim uid As Integer = Convert.ToInt32(reader("uid"))
                            allowedUIDs.Add(uid)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle exceptions here by showing a message box
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If Not allowedUIDs.Contains(uid) Then
            MessageBox.Show("You are not authorized to access this page", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        transportationAdminHome?.Dispose()
        transportationAdminHome = New TransportationAdminHome With {
            .innerPanel = childformPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, transportationAdminHome)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'View the Cab Sharing Screen
        ' Check if the form is already open
        rideSharingMainForm?.Dispose()

        ' Create a new instance of the form
        rideSharingMainForm = New RideSharingMain With {
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, rideSharingMainForm)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'View the Bus Schedule Screen
        transportBusSchedule?.Dispose()
        transportBusSchedule = New TransportBusSchedule() With
         {
                .uid = uid,
                .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, transportBusSchedule)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'View Enter TG Screen
        transportEnterTG?.Dispose()
        transportEnterTG = New TransportTGEnter() With {
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, transportEnterTG)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'View DL req form
        transportdrivingLicenseReq?.Dispose()
        transportdrivingLicenseReq = New TransportDrivingLicenseReq() With {
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, transportdrivingLicenseReq)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        'View Veh Reg Form
        transportVehicleRegReq?.Dispose()
        transportVehicleRegReq = New TransportVehicleRegReq() With {
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, transportVehicleRegReq)
    End Sub

    Private Sub TransportationDashboard_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Dispose all the child forms when the parent form is closed
        transportationAdminHome?.Dispose()
        transportBusSchedule?.Dispose()
        transportTollPlaza?.Dispose()
        transportdrivingLicenseReq?.Dispose()
        transportVehicleRegReq?.Dispose()
        rideSharingMainForm?.Dispose()
        transportEnterTG?.Dispose()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        'View the Toll Plaza Screen
        transportTollPlaza?.Dispose()
        transportTollPlaza = New TransportTollPlaza() With {
        .uid = uid,
        .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, transportTollPlaza)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'Go back to Homepage
        Dim homepage As New HomePageDashboard With
        {
            .uid = uid
        }
        homepage.Show()
        Me.Close()
    End Sub
End Class
