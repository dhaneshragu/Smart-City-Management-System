Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class TransportAdminTGLog

    Private Sub LoadandBindDataGridView()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT toll_entries.vehicle_id, toll_entries.ft_id AS Fastag_ID, vehicle_reg.vehicle_type, toll_entries.lane_id, toll_entries.timestamp FROM toll_entries JOIN vehicle_reg ON toll_entries.vehicle_id = vehicle_reg.vehicle_id", Con)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "vehicle_id"
        DataGridView1.Columns(1).DataPropertyName = "Fastag_ID"
        DataGridView1.Columns(2).DataPropertyName = "vehicle_type"
        DataGridView1.Columns(3).DataPropertyName = "lane_id"
        DataGridView1.Columns(4).DataPropertyName = "timestamp"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
    End Sub

End Class