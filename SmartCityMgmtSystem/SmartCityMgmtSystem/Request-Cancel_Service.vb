Imports System.Data.SqlClient
Imports System.Runtime.Remoting
Imports MySql.Data.MySqlClient
Public Class Request_Cancel_Service
    Public Property uid As Integer = 1
    Dim serv_charge As Integer = 0
    Public Property u_name As String = "Ashish Bharti"
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
        String.IsNullOrWhiteSpace(TextBox2.Text) OrElse
        String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Please fill in all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub ' Exit the event handler if inputs are missing
        End If

        Dim entereddDate As Date = DateTimePicker1.Value.Date

        Dim difference As TimeSpan = entereddDate - DateTime.Now.Date

        If difference.TotalDays > 7 Or difference.TotalDays < 0 Then
            MessageBox.Show("Please select a date within a week from the current date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim temp_charge As Integer
        Dim gstartTime As TimeSpan
        Dim gendTime As TimeSpan
        Dim Department1 As String

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
                            temp_charge = row.Field(Of Integer)("charge")
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

        Dim dayBooleanMap As New Dictionary(Of String, List(Of Boolean))()

        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim selectSql As String = "SELECT Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday FROM service_leave WHERE user_id = @serviceID;"

                Using cmd As New MySqlCommand(selectSql, con)
                    cmd.Parameters.AddWithValue("@serviceID", TextBox1.Text)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim days() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}

                            For i As Integer = 0 To reader.FieldCount - 1
                                Dim booleanValues As New List(Of Boolean)()
                                Dim day As String = days(i)

                                booleanValues.Add(reader.GetBoolean(i))
                                dayBooleanMap.Add(day, booleanValues)
                            Next
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
        Dim dayOfWeek As String = entereddDate.DayOfWeek.ToString()
        If dayBooleanMap.ContainsKey(dayOfWeek) AndAlso dayBooleanMap(dayOfWeek)(0) Then
            MessageBox.Show($"The {dayOfWeek} is marked as leave.")
            Return
        End If
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
                        cmd.Parameters.AddWithValue("@charge", temp_charge)
                        cmd.Parameters.AddWithValue("@startTime", timeSpan)
                        cmd.Parameters.AddWithValue("@date", selectedDateString)
                        cmd.Parameters.AddWithValue("@dept", Department1)
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("Service Requested Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using
                End Using
                serv_charge += temp_charge
                ReloadDataGridView()
            Catch ex As Exception
                MessageBox.Show("Another User has Requested Service in this slot. Please Choose another Slot ")
                Exit Sub
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
                If rowsToRemove.Count > 0 Then
                    MessageBox.Show("Service Cancelled Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
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
        Dim total_bill As Integer = fine + serv_charge
        MessageBox.Show("Total Bill: " & total_bill, "Proceeding to Payment gateway...", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Dim paymentGateway As New PaymentGateway() With {
            .uid = uid,
            .readonly_prop = False
        }
        If (paymentGateway.ShowDialog() = DialogResult.OK) Then
            MessageBox.Show("Payment Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
             Me.Close()
        
        Else
            MessageBox.Show("Payment Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

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
        DataGridView1.AllowUserToAddRows = False
    End Sub
End Class