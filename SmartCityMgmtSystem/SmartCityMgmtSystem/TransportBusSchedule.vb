Imports System.Data.SqlClient
Imports System.Numerics
Imports MySql.Data.MySqlClient
Imports SmartCityMgmtSystem.RideSharingMain
Public Class TransportBusSchedule
    Public Property uid = 11
    Public Property u_name = "Dhanesh"
    Dim day As Integer
    Dim primaryKeyBuy As String

    Private Sub ShowBuyOption(ByVal txt As String)
        If ComboBox1.SelectedIndex <> -1 And ComboBox3.SelectedIndex <> -1 Then
            ' Change this to DB logic later, update capacity=capacity -1, check capacity not 0, increase fr bus_revenue
            Dim cur_capacity(6), cur_fare As Integer
            Dim cur_day(6), formattedTime As String
            Dim TimeValue As TimeSpan
            cur_day(6) = "mon"
            cur_day(5) = "tue"
            cur_day(4) = "wed"
            cur_day(3) = "thu"
            cur_day(2) = "fri"
            cur_day(1) = "sat"
            cur_day(0) = "sun"
            Using conn As New MySqlConnection(Globals.getdbConnectionString())
                conn.Open()

                Dim query As String = "SELECT * FROM bus_capacity bc JOIN bus_schedules bs ON bc.bus_no=bs.bus_no having bc.bus_no=" & txt
                Using command As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            TimeValue = reader.GetTimeSpan("starting_time")
                            formattedTime = DateTime.Today.Add(TimeValue).ToString("hh:mm tt")
                            cur_fare = reader.GetInt32("bus_fare")
                            cur_capacity(0) = reader.GetInt32("mon")
                            cur_capacity(1) = reader.GetInt32("tue")
                            cur_capacity(2) = reader.GetInt32("wed")
                            cur_capacity(3) = reader.GetInt32("thu")
                            cur_capacity(4) = reader.GetInt32("fri")
                            cur_capacity(5) = reader.GetInt32("sat")
                            cur_capacity(6) = reader.GetInt32("sun")
                        End While
                    End Using
                End Using
            End Using
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to buy this ticket?", "Confirm Buying", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                If cur_capacity(6 - day) >= 1 Then
                    Dim pay = New PaymentGateway() With {
                                    .uid = uid,
                                    .readonly_prop = True
                                }
                    pay.TextBox1.Text = 5
                    pay.TextBox2.Text = cur_fare
                    pay.TextBox3.Text = "bus-ticket"
                    If (pay.ShowDialog() = DialogResult.OK) Then
                        Dim success As Boolean = Globals.ExecuteUpdateQuery("update bus_capacity set " & cur_day(day) & " = " & cur_day(day) & "-1 where bus_no =" & primaryKeyBuy)
                        If success Then
                            MessageBox.Show("1 Ticket bought for Bus No. " & txt & " on " & TextBox1.Text & " scheduled for " & formattedTime & ".", "Ticket Bought", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Globals.ExecuteUpdateQuery("update admin_records set bus_revenue = bus_revenue + " & cur_fare)
                        End If
                    Else
                        MessageBox.Show("Payment failed")
                    End If
                Else
                    MessageBox.Show("Seats are full!", "Seats Full", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Else
            MessageBox.Show("Please select both Source and Destination")
        End If
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is in the "BuyBut" column and not a header cell
        If e.ColumnIndex = DataGridView1.Columns("BuyBut").Index AndAlso e.RowIndex >= 0 Then
            'Show Edit Option
            primaryKeyBuy = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            ShowBuyOption(DataGridView1.Rows(e.RowIndex).Cells(0).Value)
            LoadandBindDataGridView()
        End If
    End Sub
    ' Function to retrieve place data from the database
    Private Function GetPlacesFromDatabase() As List(Of Place)
        Dim places As New List(Of Place)()

        Using conn As New MySqlConnection(Globals.getdbConnectionString())
            conn.Open()

            Dim query As String = "SELECT * FROM placedb"
            Using command As New MySqlCommand(query, conn)
                Using reader As MySqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim id As Integer = reader.GetInt32("id")
                        Dim name As String = reader.GetString("place_name")
                        Dim place As New Place(id, name)
                        places.Add(place)
                    End While
                End Using
            End Using
        End Using

        Return places
    End Function

    Private Sub LoadandBindDataGridView()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand
        Dim i As Integer

        If TextBox1.Text = "Monday" Then
            day = 6
        End If
        If TextBox1.Text = "Tuesday" Then
            day = 5
        End If
        If TextBox1.Text = "Wednesday" Then
            day = 4
        End If
        If TextBox1.Text = "Thursday" Then
            day = 3
        End If
        If TextBox1.Text = "Friday" Then
            day = 2
        End If
        If TextBox1.Text = "Saturday" Then
            day = 1
        End If
        If TextBox1.Text = "Sunday" Then
            day = 0
        End If

        Dim cur_day(6) As String
        cur_day(6) = "mon"
        cur_day(5) = "tue"
        cur_day(4) = "wed"
        cur_day(3) = "thu"
        cur_day(2) = "fri"
        cur_day(1) = "sat"
        cur_day(0) = "sun"

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Dim selectedPlace1 As Place = DirectCast(ComboBox1.SelectedItem, Place)
        Dim selectedPlace2 As Place = DirectCast(ComboBox3.SelectedItem, Place)
        If ComboBox1.SelectedIndex = -1 Or ComboBox3.SelectedIndex = -1 Then
            cmd = New MySqlCommand("SELECT bs.bus_no,bs.starting_time,bs.bus_fare,bs.days_operating,bc." & cur_day(day) & " as cur_capacity FROM bus_capacity bc INNER JOIN bus_schedules bs ON bc.bus_no=bs.bus_no HAVING (bs.days_operating & power(2, " & day & ")) <> 0", Con)
        Else
            cmd = New MySqlCommand("SELECT bs.bus_no,bs.starting_time,bs.bus_fare,bs.days_operating,bs.src_id,bs.dest_id,bc." & cur_day(day) & " as cur_capacity FROM bus_capacity bc INNER JOIN bus_schedules bs ON bc.bus_no=bs.bus_no HAVING bs.src_id = " & selectedPlace2.ID & " and bs.dest_id = " & selectedPlace1.ID & " and (bs.days_operating & power(2, " & day & " )) <> 0 ", Con)
        End If
        'MessageBox.Show(DirectCast(ComboBox1.SelectedValue, Integer))
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()
        DataGridView1.Rows.Clear()

        i = 0
        While (reader.Read())
            Dim row As New DataGridViewRow()
            DataGridView1.Rows.Add(row)
            Dim timeValue As TimeSpan = reader.GetTimeSpan("starting_time")
            Dim formattedTime As String = DateTime.Today.Add(timeValue).ToString("hh:mm tt")
            DataGridView1.Rows(i).Cells("Column1").Value = reader.GetInt16("bus_no")
            DataGridView1.Rows(i).Cells("Column2").Value = formattedTime
            DataGridView1.Rows(i).Cells("Column3").Value = reader.GetString("bus_fare")
            DataGridView1.Rows(i).Cells("Column4").Value = reader.GetString("cur_capacity")
            i = i + 1
        End While

        'Fill the DataTable with data from the SQL table
        reader.Close()
        Con.Close()
    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = DateTime.Now.ToString("dddd")
        'Load the places from database and populate the combobox
        Dim places As List(Of Place) = GetPlacesFromDatabase()
        'To bind the places as their names in the combobox and their IDs as values
        ComboBox1.DataSource = places.ToList()
        ComboBox1.DisplayMember = "Name"
        ComboBox1.ValueMember = "ID"

        ComboBox3.DataSource = places.ToList()
        ComboBox3.DisplayMember = "Name"
        ComboBox3.ValueMember = "ID"

        ComboBox1.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1

        LoadandBindDataGridView()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        ' Prevent selecting the same place in both ComboBoxes
        Dim selectedPlace1 As Place = DirectCast(ComboBox1.SelectedItem, Place)
        Dim selectedPlace2 As Place = DirectCast(ComboBox3.SelectedItem, Place)

        If selectedPlace1 IsNot Nothing AndAlso selectedPlace2 IsNot Nothing AndAlso selectedPlace1.ID = selectedPlace2.ID Then
            ComboBox3.SelectedIndex = -1 ' Clear selection from ComboBox2
        End If
        LoadandBindDataGridView()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Prevent selecting the same place in both ComboBoxes
        Dim selectedPlace1 As Place = DirectCast(ComboBox1.SelectedItem, Place)
        Dim selectedPlace2 As Place = DirectCast(ComboBox3.SelectedItem, Place)

        If selectedPlace1 IsNot Nothing AndAlso selectedPlace2 IsNot Nothing AndAlso selectedPlace1.ID = selectedPlace2.ID Then
            ComboBox1.SelectedIndex = -1 ' Clear selection from ComboBox1
        End If
        LoadandBindDataGridView()
    End Sub

End Class