Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Service_Scheduled
    Public Property uid As Integer = 1
    Public Property u_name As String = "Ashish Bharti"

    Private Sub Service_Scheduled_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                                    schedule_date AS scheduleDate,
                                    start_time AS startTime
                                    From services_scheduled ss
                                    WHERE provider_id = @serviceID
                                    AND schedule_date >= @cur_date
                                    ORDER BY schedule_date ASC,
                                    start_time ASC;"

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
        DataGridView1.Columns(5).DataPropertyName = "startTime"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable

        If DataGridView1.Rows.Count > 0 Then
            ' Access the first row and retrieve values from specific cells
            Dim userID As Integer = Convert.ToInt32(DataGridView1.Rows(0).Cells("Column1").Value)
            Dim depat As String = Convert.ToString(DataGridView1.Rows(0).Cells("Column3").Value)
            Dim dateValue As DateTime = Convert.ToDateTime(DataGridView1.Rows(0).Cells("Column5").Value)

            RichTextBox9.Text = depat
            DateTimePicker2.Value = dateValue
            RichTextBox7.Text = userID
        End If
    End Sub
End Class