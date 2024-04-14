Imports MySql.Data.MySqlClient

Public Class Service_History
    Public Property uid As Integer = 1
    Public Property u_name As String = "Ashish Bharti"
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label3.Text = "Unique Identification No: " & uid
        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()
                Dim cur_date As DateTime = DateTime.Now.Date
                Dim sql As String = "SELECT ss.provider_id AS serviceID,
                                    ss.Department As department,
                                    ss.schedule_date AS scheduleDate,
                                    (SELECT service_desc 
                                        FROM service_desc sd
                                        WHERE sd.user_id = ss.provider_id) AS description,
                                    ss.service_charge AS Charge,
                                    ss.start_time AS StartTime
                                    From services_scheduled ss
                                    WHERE requester_id = @userID
                                    AND schedule_date < @cur_date;"


                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@userID", uid)
                    cmd.Parameters.AddWithValue("@cur_date", cur_date)
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
        DataGridView1.Columns(1).DataPropertyName = "description"
        DataGridView1.Columns(2).DataPropertyName = "department"
        DataGridView1.Columns(3).DataPropertyName = "Charge"
        DataGridView1.Columns(4).DataPropertyName = "scheduleDate"
        DataGridView1.Columns(5).DataPropertyName = "StartTime"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, TransportationAdminHome)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim servicePortal = New Service_Portal() With {
            .uid = uid,
            .u_name = u_name
        }
        Me.Close()
        servicePortal.Show()

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim selectedOption As String = ComboBox2.SelectedItem.ToString()

        Dim sortBy As String = ""
        Select Case selectedOption
            Case "By Price"
                sortBy = "ss.service_charge"
            Case "By Date/Time"
                sortBy = "ss.schedule_date"
            Case Else
                ' Handle unexpected selection
                Exit Sub
        End Select

        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()
                Dim cur_date As DateTime = DateTime.Now.Date
                Dim sql As String = $"SELECT ss.provider_id AS serviceID,
                                    ss.Department As department,
                                    ss.schedule_date AS scheduleDate,
                                    (SELECT service_desc 
                                        FROM service_desc sd
                                        WHERE sd.user_id = ss.provider_id) AS description,
                                    ss.service_charge AS Charge,
                                    ss.start_time AS StartTime
                                    FROM services_scheduled ss
                                    WHERE requester_id = @userID
                                    AND schedule_date < @cur_date
                                    ORDER BY {sortBy};"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@userID", uid)
                    cmd.Parameters.AddWithValue("@cur_date", cur_date)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        ' Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "serviceID"
        DataGridView1.Columns(1).DataPropertyName = "description"
        DataGridView1.Columns(2).DataPropertyName = "department"
        DataGridView1.Columns(3).DataPropertyName = "Charge"
        DataGridView1.Columns(4).DataPropertyName = "scheduleDate"
        DataGridView1.Columns(5).DataPropertyName = "StartTime"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

End Class
