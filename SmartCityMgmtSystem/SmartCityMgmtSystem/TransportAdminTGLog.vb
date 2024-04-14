Imports System.Data.SqlClient
Imports System.Text
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
                           vehicle_reg.vehicle_type,
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
            DataGridView1.Columns(2).DataPropertyName = "vehicle_type"
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

        If filterByVehicleType AndAlso Not String.IsNullOrEmpty(txtVehicleType.Text) Then
            If Integer.TryParse(txtVehicleType.Text, vehicleTypeValue) Then
                filterExpression.Append($"vehicle_type = {vehicleTypeValue} AND ")
            Else
                MessageBox.Show("Please enter a valid Vehicle Type.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

        If filterByLaneID AndAlso Not String.IsNullOrEmpty(txtLaneID.Text) Then
            If Integer.TryParse(txtLaneID.Text, laneIdValue) Then
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
            filterByVehicleType = Not String.IsNullOrEmpty(txtVehicleType.Text) AndAlso Integer.TryParse(txtVehicleType.Text, vehicleTypeValue)
            filterByFastagID = Not String.IsNullOrEmpty(txtFastagID.Text) AndAlso Integer.TryParse(txtFastagID.Text, fastagIdValue)
            filterByLaneID = Not String.IsNullOrEmpty(txtLaneID.Text) AndAlso Integer.TryParse(txtLaneID.Text, laneIdValue)

            LoadandBindDataGridView()
        End If
        btnFilterClick = False
    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
    End Sub


End Class