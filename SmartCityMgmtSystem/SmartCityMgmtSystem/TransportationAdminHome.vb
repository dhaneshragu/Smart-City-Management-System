Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class TransportationAdminHome
    Public Property uid As Integer = 1
    Public Property u_name As String = "Dhanesh"
    Private transportAddsecys As TransportAddSecys = Nothing
    Private transportSendNotification As TransportSendNotification = Nothing
    Private transportationBusSchedulesAdmin As TransportationBusSchedulesAdmin = Nothing
    Private transportManageFastagAdmin As TransportationManageFastagAdmin = Nothing
    Private transportFRAdmin As TransportFRAdmin = Nothing
    Private transportAdminDLReq As TransportAdminDLReq = Nothing
    Private transportAdminVRReq As TransportAdminVRReq = Nothing
    Private transportAdminTGLog As TransportAdminTGLog = Nothing
    Private transportAdminRSReq As TransportAdminRSReq = Nothing
    Private transportMangeBusStopAdmin As TransportMangeBusStopAdmin = Nothing
    Private transportManageTollGatesAdmin As TransportManageTollGatesAdmin = Nothing
    Public innerPanel As Panel


    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    End Sub


    Private Sub PictureBox6_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox6.Click
        'Only Minister can access this
        If uid <> 5 Then
            MessageBox.Show("You are not authorized to access this page", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        'View the TransportAddSecys screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        transportAddsecys?.Dispose()
        transportAddsecys = New TransportAddSecys()
        Globals.viewChildForm(innerPanel, transportAddsecys)
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        Dim allowedUIDs As New List(Of Integer) From {5} ' Transportation minister
        Dim query As String = "SELECT DISTINCT uid FROM admin_officers where position ='Public Transport Officer'"

        Try
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
        transportationBusSchedulesAdmin?.Dispose()
        transportationBusSchedulesAdmin = New TransportationBusSchedulesAdmin()
        Globals.viewChildForm(innerPanel, transportationBusSchedulesAdmin)
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Dim allowedUIDs As New List(Of Integer) From {5} ' Transportation minister
        Dim query As String = "SELECT DISTINCT uid FROM admin_officers where position ='Toll Plaza Officer'"

        Try
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
        transportManageFastagAdmin?.Dispose()
        transportManageFastagAdmin = New TransportationManageFastagAdmin()
        Globals.viewChildForm(innerPanel, transportManageFastagAdmin)
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        'All admins can view this
        transportFRAdmin?.Dispose()
        transportFRAdmin = New TransportFRAdmin()
        Globals.viewChildForm(innerPanel, transportFRAdmin)
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        'Driving License
        Dim allowedUIDs As New List(Of Integer) From {5} ' Transportation minister
        Dim query As String = "SELECT DISTINCT uid FROM admin_officers where position ='R.T.O. Officer'"

        Try
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
        transportAdminDLReq?.Dispose()
        transportAdminDLReq = New TransportAdminDLReq()
        Globals.viewChildForm(innerPanel, transportAdminDLReq)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        'Vehicle Registration
        Dim allowedUIDs As New List(Of Integer) From {5} ' Transportation minister
        Dim query As String = "SELECT DISTINCT uid FROM admin_officers where position ='R.T.O. Officer'"

        Try
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
        transportAdminVRReq?.Dispose()
        transportAdminVRReq = New TransportAdminVRReq()
        Globals.viewChildForm(innerPanel, transportAdminVRReq)
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        'Toll Gate
        Dim allowedUIDs As New List(Of Integer) From {5} ' Transportation minister
        Dim query As String = "SELECT DISTINCT uid FROM admin_officers where position ='Toll Plaza Officer'"

        Try
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
        transportAdminTGLog?.Dispose()
        transportAdminTGLog = New TransportAdminTGLog()
        Globals.viewChildForm(innerPanel, transportAdminTGLog)
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        ' Ride Sharing
        Dim allowedUIDs As New List(Of Integer) From {5} ' Transportation minister
        Dim query As String = "SELECT DISTINCT uid FROM admin_officers where position ='Ride Sharing Regulator'"

        Try
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
        transportAdminRSReq?.Dispose()
        transportAdminRSReq = New TransportAdminRSReq()
        Globals.viewChildForm(innerPanel, transportAdminRSReq)
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        'Bus stops
        Dim allowedUIDs As New List(Of Integer) From {5} ' Transportation minister
        Dim query As String = "SELECT DISTINCT uid FROM admin_officers where position ='Public Transport Officer'"

        Try
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
        transportMangeBusStopAdmin?.Dispose()
        transportMangeBusStopAdmin = New TransportMangeBusStopAdmin()
        Globals.viewChildForm(innerPanel, transportMangeBusStopAdmin)
    End Sub

    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        'Manage Toll Gates
        Dim allowedUIDs As New List(Of Integer) From {5} ' Transportation minister
        Dim query As String = "SELECT DISTINCT uid FROM admin_officers where position ='Toll Plaza Officer'"

        Try
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
        transportManageTollGatesAdmin?.Dispose()
        transportManageTollGatesAdmin = New TransportManageTollGatesAdmin()
        Globals.viewChildForm(innerPanel, transportManageTollGatesAdmin)
    End Sub

    Private Sub TransportationAdminHome_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Dispose all the child forms when the parent form is closed
        transportAddsecys?.Dispose()
        transportationBusSchedulesAdmin?.Dispose()
        transportManageFastagAdmin?.Dispose()
        transportFRAdmin?.Dispose()
        transportAdminDLReq?.Dispose()
        transportAdminVRReq?.Dispose()
        transportAdminTGLog?.Dispose()
        transportAdminRSReq?.Dispose()
        transportMangeBusStopAdmin?.Dispose()
        transportManageTollGatesAdmin?.Dispose()
        transportSendNotification?.Dispose()
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
        'All admins can view notification screen
        transportSendNotification?.Dispose()
        transportSendNotification = New TransportSendNotification()
        Globals.viewChildForm(innerPanel, transportSendNotification)
    End Sub
End Class