Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Service_View_Offered
    Public Property uid As Integer = 1
    Public Property u_name As String = "Ashish Bharti"
    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim sortBy As String = ComboBox2.SelectedItem.ToString()

        Dim dataTable As New DataTable()
        Dim todaydate As DateTime = DateTime.Now.Date
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = ""

                If sortBy = "By Price" Then
                    sql = "SELECT
                        requester_id AS userID,
                        service_charge AS charge,
                        (SELECT service_desc FROM service_desc sd WHERE sd.user_id = ss.provider_id) AS descr,
                        Department AS depat,
                        schedule_date AS scheduleDate
                        From services_scheduled ss
                        WHERE provider_id = @serviceID
                        AND schedule_date < @cur_date
                        ORDER BY service_charge;"
                ElseIf sortBy = "By Date/Time" Then
                    sql = "SELECT
                        requester_id AS userID,
                        service_charge AS charge,
                        (SELECT service_desc FROM service_desc sd WHERE sd.user_id = ss.provider_id) AS descr,
                        Department AS depat,
                        schedule_date AS scheduleDate
                        From services_scheduled ss
                        WHERE provider_id = @serviceID
                        AND schedule_date < @cur_date
                        ORDER BY schedule_date;"
                End If

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@serviceID", uid)
                    cmd.Parameters.AddWithValue("@cur_date", todaydate)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        ' Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "userID"
        DataGridView1.Columns(1).DataPropertyName = "descr"
        DataGridView1.Columns(2).DataPropertyName = "depat"
        DataGridView1.Columns(3).DataPropertyName = "charge"
        DataGridView1.Columns(4).DataPropertyName = "scheduleDate"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub


    Private Sub Service_View_Offered_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dataTable As New DataTable()
        Dim todaydate As DateTime = DateTime.Now.Date
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT
                                    requester_id AS userID,
                                    service_charge AS charge,
                                    (SELECT service_desc FROM service_desc sd WHERE sd.user_id = ss.provider_id) AS descr,
                                    Department AS depat,
                                    schedule_date AS scheduleDate
                                    From services_scheduled ss
                                    WHERE provider_id = @serviceID
                                    AND schedule_date < @cur_date;"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@serviceID", uid)
                    cmd.Parameters.AddWithValue("@cur_date", todaydate)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "userID"
        DataGridView1.Columns(1).DataPropertyName = "descr"
        DataGridView1.Columns(2).DataPropertyName = "depat"
        DataGridView1.Columns(3).DataPropertyName = "charge"
        DataGridView1.Columns(4).DataPropertyName = "scheduleDate"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub
End Class