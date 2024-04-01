Imports System.Data.SqlClient
Public Class TransportationAdminHome
    Public Property uid As Integer = 1
    Public Property u_name As String = "Dhanesh"
    Private transportAddsecys As TransportAddSecys = Nothing
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
        'View the TransportAddSecys screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        transportAddsecys?.Dispose()
        transportAddsecys = New TransportAddSecys()
        Globals.viewChildForm(innerPanel, transportAddsecys)
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        transportationBusSchedulesAdmin?.Dispose()
        transportationBusSchedulesAdmin = New TransportationBusSchedulesAdmin()
        Globals.viewChildForm(innerPanel, transportationBusSchedulesAdmin)
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        transportManageFastagAdmin?.Dispose()
        transportManageFastagAdmin = New TransportationManageFastagAdmin()
        Globals.viewChildForm(innerPanel, transportManageFastagAdmin)
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        transportFRAdmin?.Dispose()
        transportFRAdmin = New TransportFRAdmin()
        Globals.viewChildForm(innerPanel, transportFRAdmin)
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        transportAdminDLReq?.Dispose()
        transportAdminDLReq = New TransportAdminDLReq()
        Globals.viewChildForm(innerPanel, transportAdminDLReq)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        transportAdminVRReq?.Dispose()
        transportAdminVRReq = New TransportAdminVRReq()
        Globals.viewChildForm(innerPanel, transportAdminVRReq)
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        transportAdminTGLog?.Dispose()
        transportAdminTGLog = New TransportAdminTGLog()
        Globals.viewChildForm(innerPanel, transportAdminTGLog)
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        transportAdminRSReq?.Dispose()
        transportAdminRSReq = New TransportAdminRSReq()
        Globals.viewChildForm(innerPanel, transportAdminRSReq)
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        transportMangeBusStopAdmin?.Dispose()
        transportMangeBusStopAdmin = New TransportMangeBusStopAdmin()
        Globals.viewChildForm(innerPanel, transportMangeBusStopAdmin)
    End Sub

    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
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
    End Sub

End Class