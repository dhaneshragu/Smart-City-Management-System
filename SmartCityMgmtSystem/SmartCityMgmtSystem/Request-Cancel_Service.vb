Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Request_Cancel_Service
    Public Property uid As Integer = 1
    Public Property u_name As String = "Ashish Bharti"
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim entereddDate As Date = DateTimePicker1.Value.Date

        Dim difference As TimeSpan = entereddDate - DateTime.Now.Date

        If difference.TotalDays > 7 Or difference.TotalDays < 0 Then
            MessageBox.Show("Please select a date within a week from the current date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim gstartTime As TimeSpan
        Dim gendTime As TimeSpan
        Dim Department1 As String
        Dim serv_charge As Integer
        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT service_charge as charge, department AS dept, start_time AS startTime, end_time AS endTime FROM service_desc WHERE user_id = @serviceID;"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@serviceID", TextBox1.Text) ' Replace userIdValue with the actual user ID

                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)

                    ' Check if the dataTable contains any rows
                    If dataTable.Rows.Count > 0 Then
                        Dim messageBuilder As New System.Text.StringBuilder()

                        ' Iterate through the rows and retrieve the startTime and endTime values
                        For Each row As DataRow In dataTable.Rows
                            Dim startTime As TimeSpan = row.Field(Of TimeSpan)("startTime")
                            Dim endTime As TimeSpan = row.Field(Of TimeSpan)("endTime")
                            Department1 = row.Field(Of String)("dept")
                            serv_charge = row.Field(Of Integer)("charge")
                            gstartTime = startTime
                            gendTime = endTime
                        Next
                    Else
                        ' No rows found in the dataTable
                        MessageBox.Show("No service found for given service ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
        Dim hourInput As String = TextBox3.Text

        ' Parse the input string to a double value
        Dim hour As Double = Double.Parse(hourInput)

        ' Convert the hour value to a TimeSpan object
        Dim timeSpan As TimeSpan = TimeSpan.FromHours(hour)

        If timeSpan < gstartTime Or timeSpan >= gendTime + TimeSpan.FromHours(1) Then
            MessageBox.Show("Requested Service Provider is unavailable during this hour", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim dataTable1 As New DataTable()
            Try
                Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                    con.Open()

                    Dim sql As String = "INSERT INTO services_scheduled
                                        VALUES (@userID,@serviceID,@charge,
                                           @startTime,
                                            @date, 
                                        @dept);"
                    Dim selectedDateString As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")
                    Using cmd As New MySqlCommand(sql, con)
                        cmd.Parameters.AddWithValue("@userID", TextBox2.Text)
                        cmd.Parameters.AddWithValue("@serviceID", TextBox1.Text)
                        cmd.Parameters.AddWithValue("@charge", serv_charge)
                        cmd.Parameters.AddWithValue("@startTime", timeSpan)
                        cmd.Parameters.AddWithValue("@date", selectedDateString)
                        cmd.Parameters.AddWithValue("@dept", Department1)
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("Service Requested Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using
                End Using
                ReloadDataGridView()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try


            Try
                Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                    con.Open()
                    Dim selectedDateString As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")
                    Dim sql As String = "INSERT INTO services_invoice 
                                        VALUES (@userID,@serviceID,@charge,
                                        CONCAT(DATE_FORMAT(@date, '%Y-%m-%d'), ' ', TIME_FORMAT(@paytime, '%H:%i:%s')));"

                    Using cmd As New MySqlCommand(sql, con)
                        cmd.Parameters.AddWithValue("@userID", TextBox2.Text)
                        cmd.Parameters.AddWithValue("@serviceID", TextBox1.Text)
                        cmd.Parameters.AddWithValue("@charge", serv_charge)
                        cmd.Parameters.AddWithValue("@paytime", timeSpan)
                        cmd.Parameters.AddWithValue("@date", selectedDateString)
                        cmd.ExecuteNonQuery()

                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            DateTimePicker1.Value = DateTime.Now
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Dim fine As Integer
        Dim scheduledate As Date
        Dim startTime As TimeSpan
        Dim serviceID As Integer
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()
                Dim rowsToRemove As New List(Of DataGridViewRow)
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If CBool(row.Cells("Column6").Value) Then
                        serviceID = CInt(row.Cells("Column1").Value)
                        scheduledate = CDate(row.Cells("Column4").Value)
                        startTime = CType(row.Cells("Column5").Value, TimeSpan)
                        fine = fine + CInt(row.Cells("Column3").Value) / 4
                        Dim sql As String = "DELETE FROM services_scheduled
                                             WHERE requester_id = @userID AND 
                                                provider_id = @serviceID AND
                                                schedule_date = @date AND
                                                start_time = @startTime;"


                        Using cmd As New MySqlCommand(sql, con)
                            cmd.Parameters.AddWithValue("@userID", uid)
                            cmd.Parameters.AddWithValue("@serviceID", serviceID)
                            cmd.Parameters.AddWithValue("@date", scheduledate)
                            cmd.Parameters.AddWithValue("@startTime", startTime)
                            cmd.ExecuteNonQuery()

                        End Using
                        rowsToRemove.Add(row)
                    End If
                Next
                For Each row As DataGridViewRow In rowsToRemove
                    DataGridView1.Rows.Remove(row)
                Next
                MessageBox.Show("Service Cancelled Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()
                Dim selectedDateString As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")
                Dim sql As String = "INSERT INTO services_invoice 
                                        VALUES (@userID,@serviceID,@charge,
                                        CONCAT(DATE_FORMAT(@date, '%Y-%m-%d'), ' ', TIME_FORMAT(@paytime, '%H:%i:%s')));"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@userID", uid)
                    cmd.Parameters.AddWithValue("@serviceID", serviceID)
                    cmd.Parameters.AddWithValue("@charge", fine)
                    cmd.Parameters.AddWithValue("@paytime", DateTime.Now.TimeOfDay)
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.Date)
                    cmd.ExecuteNonQuery()

                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        DateTimePicker1.Value = DateTime.Now
    End Sub
    Private Sub ReloadDataGridView()
        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT
                provider_id AS serviceID,
                service_charge AS charge,
                Department AS depat,
                start_time AS startTime,
                schedule_date AS scheduleDate
                From services_scheduled
                WHERE requester_id = @userID;"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@userID", uid)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "serviceID"
        DataGridView1.Columns(1).DataPropertyName = "depat"
        DataGridView1.Columns(2).DataPropertyName = "charge"
        DataGridView1.Columns(3).DataPropertyName = "scheduleDate"
        DataGridView1.Columns(4).DataPropertyName = "StartTime"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub Request_Cancel_Service_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT
                provider_id AS serviceID,
                service_charge AS charge,
                Department AS depat,
                start_time AS startTime,
                schedule_date AS scheduleDate
                From services_scheduled
                WHERE requester_id = @userID;"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@userID", uid)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "serviceID"
        DataGridView1.Columns(1).DataPropertyName = "depat"
        DataGridView1.Columns(2).DataPropertyName = "charge"
        DataGridView1.Columns(3).DataPropertyName = "scheduleDate"
        DataGridView1.Columns(4).DataPropertyName = "StartTime"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub
End Class