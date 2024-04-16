Imports System.Data.SqlClient
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Public Class TransportAdminTGLog
    Dim filterByVehicleID As Boolean
    Dim filterByVehicleType As Boolean
    Dim filterByFastagID As Boolean
    Dim filterByLaneID As Boolean
    Dim filterByDate As Boolean

    Dim vehicleIdValue As Integer
    Dim fastagIdValue As Integer
    Dim vehicleTypeValue As Integer
    Dim vehicleType As String
    Dim laneIdValue As Integer
    Dim btnFilterClick As Boolean
    ' Create a DataTable to store the data
    Dim filterExpression As New StringBuilder()

    Dim dataTable1 As New DataTable()
    Private Sub LoadandBindDataGridView()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        dataTable1.Clear()

        Try
            Con.Open()

            Dim query As String = "SELECT toll_entries.vehicle_id,
                           toll_entries.ft_id AS Fastag_ID,
                           vehicle_reg.vehicle_type as vehicle_type,
                           toll_entries.lane_id,
                           toll_entries.timestamp as Date
                           FROM toll_entries 
                           JOIN vehicle_reg ON toll_entries.vehicle_id = vehicle_reg.vehicle_id"


            ' Set the final query text
            cmd = New MySqlCommand(query, Con)


            ' Execute the query and load data into the DataTable
            reader = cmd.ExecuteReader

            dataTable1.Load(reader)
            reader.Close()

            ' Check if the column 'vehicle_type_name' already exists in the DataTable before adding it
            If dataTable1.Columns("vehicle_type_name") Is Nothing Then
                Dim newColumn As DataColumn = New DataColumn("vehicle_type_name", GetType(String))
                dataTable1.Columns.Add(newColumn)
            End If

            If dataTable1.Rows.Count > 0 Then
                For Each row As DataRow In dataTable1.Rows
                    Dim id As Integer = If(Not IsDBNull(row("vehicle_type")), Convert.ToInt32(row("vehicle_type")), "")
                    Dim name As String = (TransportGlobals.GetVehicleType(id)).ToString()
                    row("vehicle_type_name") = name
                Next
            End If


            Dim filteredRows() As DataRow = dataTable1.Select(BuildFilterExpression())
            Dim filteredRowsList As New List(Of DataRow)()


            ' Create a new DataTable to store the filtered results
            Dim filteredDataTable As DataTable = dataTable1.Clone()

            ' Copy filtered rows to the new DataTable

            For Each row As DataRow In filteredRows
                filteredDataTable.ImportRow(row)
            Next

            'IMP: Specify the Column Mappings from DataGridView to SQL Table
            DataGridView1.AutoGenerateColumns = False
            DataGridView1.Columns(0).DataPropertyName = "vehicle_id"
            DataGridView1.Columns(1).DataPropertyName = "Fastag_ID"
            DataGridView1.Columns(2).DataPropertyName = "vehicle_type_name"
            DataGridView1.Columns(3).DataPropertyName = "lane_id"
            DataGridView1.Columns(4).DataPropertyName = "Date"

            If btnFilterClick Then

                ' Bind the data to DataGridView
                DataGridView1.DataSource = filteredDataTable

            Else
                DataGridView1.DataSource = dataTable1
            End If
            DataGridView1.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try
    End Sub

    Private Function BuildFilterExpression() As String
        filterExpression.Clear()

        If filterByVehicleID AndAlso Not String.IsNullOrEmpty(txtvehicleID.Text) Then
            If Integer.TryParse(txtvehicleID.Text, vehicleIdValue) Then
                filterExpression.Append($"vehicle_id = {vehicleIdValue} AND ")
            Else
                MessageBox.Show("Please enter a valid Vehicle ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

        If filterByFastagID AndAlso Not String.IsNullOrEmpty(txtFastagID.Text) Then
            If Integer.TryParse(txtFastagID.Text, fastagIdValue) Then
                filterExpression.Append($"Fastag_ID = {fastagIdValue} AND ")
            Else
                MessageBox.Show("Please enter a valid Fastag ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

        If filterByVehicleType AndAlso VTypeCb.SelectedItem IsNot Nothing Then
            vehicleType = VTypeCb.SelectedItem.ToString()
            filterExpression.Append($"vehicle_type_name = '{vehicleType}' AND ")
        End If

        If filterByLaneID AndAlso laneidCb.SelectedItem IsNot Nothing Then
            Dim laneIdString As String = laneidCb.SelectedItem.ToString()
            If Integer.TryParse(laneIdString, laneIdValue) Then
                filterExpression.Append($"lane_id = {laneIdValue} AND ")
            Else
                MessageBox.Show("Please enter a valid Lane ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If


        ' Remove the trailing "AND" if there are any conditions in the filter expression
        If filterExpression.Length > 0 Then
            filterExpression.Remove(filterExpression.Length - 5, 5) ' Remove " AND "
        End If

        Return filterExpression.ToString()
    End Function

    Private Sub BtnFilter_Click(sender As Object, e As EventArgs) Handles btnfilter.Click
        btnFilterClick = True
        ' Check if dataTable1 is not null
        If dataTable1 IsNot Nothing Then
            ' Boolean variables to determine if each column is used for filtering
            filterByVehicleID = Not String.IsNullOrEmpty(txtvehicleID.Text) AndAlso Integer.TryParse(txtvehicleID.Text, vehicleIdValue)
            If VTypeCb.SelectedItem IsNot Nothing Then
                filterByVehicleType = Not String.IsNullOrEmpty(VTypeCb.SelectedItem.ToString())
            Else
                ' Handle the case when VTypeCb.SelectedItem is null (if needed)
                filterByVehicleType = False
            End If

            filterByFastagID = Not String.IsNullOrEmpty(txtFastagID.Text) AndAlso Integer.TryParse(txtFastagID.Text, fastagIdValue)
            If laneidCb.SelectedItem IsNot Nothing Then
                filterByLaneID = Not String.IsNullOrEmpty(laneidCb.SelectedItem.ToString())
            Else
                filterByLaneID = False
            End If

            LoadandBindDataGridView()
        End If
        btnFilterClick = False
    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' Populate ComboBox with vehicle types until GetVehicleType returns non-null
        Dim vtypeid As Integer = 1
        Dim vehicleType As String = TransportGlobals.GetVehicleType(vtypeid)
        While vehicleType <> "Unknown"
            VTypeCb.Items.Add(vehicleType)
            vtypeid += 1
            vehicleType = TransportGlobals.GetVehicleType(vtypeid)
        End While


        Dim connection As MySqlConnection = Globals.GetDBConnection()
        ' Define SQL query to retrieve lane IDs
        Dim query As String = "SELECT DISTINCT lane_id FROM tollboothdb" ' Replace your_table_name with the actual table name

        ' Open connection
        connection.Open()

        ' Create command
        Using command As New MySqlCommand(query, connection)
            ' Execute command and get data reader
            Using reader As MySqlDataReader = command.ExecuteReader()
                ' Clear existing items in ComboBox
                laneidCb.Items.Clear()
                ' Loop through each lane ID retrieved from the database
                While reader.Read()
                    ' Add lane ID to ComboBox
                    laneidCb.Items.Add(reader("lane_id").ToString())
                End While
            End Using
        End Using

        ' Close connection (assuming it's no longer needed)
        connection.Close()

        LoadandBindDataGridView()
    End Sub


End Class