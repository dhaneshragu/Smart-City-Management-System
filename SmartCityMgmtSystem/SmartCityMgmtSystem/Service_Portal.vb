Imports Org.BouncyCastle.Asn1.Ocsp
Imports MySql.Data.MySqlClient
Imports System.Runtime.InteropServices
Public Class Service_Portal
    'To be passed from Login Dashboard
    Public Property uid As Integer = 1
    Public Property u_name As String = "Ashish Bharti"
    Private searchService As Service_search_results = Nothing
    Private requestCancelService As Request_Cancel_Service = Nothing
    Private serviceHistory As Service_History = Nothing
    Private loginServiceProvider As Service_Provider = Nothing
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label3.Text = "Unique Identification No: " & uid
        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT user_id AS ID,
       service_desc As Description,
       department As Department,
       service_offered As ServiceOffered,
       service_charge As Charge,
       start_time As StartTime,
       end_time As EndTime
                From service_desc;"


                Using cmd As New MySqlCommand(sql, con)

                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "ID"
        DataGridView1.Columns(1).DataPropertyName = "Description"
        DataGridView1.Columns(2).DataPropertyName = "ServiceOffered"
        DataGridView1.Columns(3).DataPropertyName = "Department"
        DataGridView1.Columns(4).DataPropertyName = "Charge"
        DataGridView1.Columns(5).DataPropertyName = "StartTime"
        DataGridView1.Columns(6).DataPropertyName = "EndTime"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
        'DataGridView1.Visible = True
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        loginServiceProvider?.Dispose()
        loginServiceProvider = New Service_Provider() With {
            .uid = uid,
            .u_name = u_name
        }
        Me.Hide()
        loginServiceProvider.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        searchService?.Dispose()
        searchService = New Service_search_results() With {
            .uid = uid,
            .u_name = u_name
        }
        Me.Hide()
        searchService.Show()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        requestCancelService?.Dispose()
        requestCancelService = New Request_Cancel_Service() With {
            .uid = uid,
            .u_name = u_name
        }
        'requestCancelService.Show()
        Globals.viewChildForm(childformPanel, requestCancelService)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        serviceHistory?.Dispose()
        serviceHistory = New Service_History() With {
            .uid = uid,
            .u_name = u_name
        }
        Me.Hide()
        serviceHistory.Show()

    End Sub

    Private Sub Service_Portal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        searchService?.Dispose()
        requestCancelService?.Dispose()
        serviceHistory?.Dispose()
        loginServiceProvider?.Dispose()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim homepage As New HomePageDashboard With
        {
            .uid = uid
        }
        homepage.Show()
        Me.Close()
    End Sub
End Class
