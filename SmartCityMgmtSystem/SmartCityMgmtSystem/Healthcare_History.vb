Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Healthcare_History
    Public Property uid As Integer = 13
    Private Sub d1_Click(sender As Object, e As EventArgs) Handles d1.Click
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT * FROM appointments INNER JOIN hospitalbill on appointments.Appointment_id = hospitalbill.Appointment_id where patient_id = @Value", Con)
        cmd.Parameters.AddWithValue("@Value", uid)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).HeaderText = "Date"
        DataGridView1.Columns(0).DataPropertyName = "time"
        DataGridView1.Columns(1).HeaderText = "Hospital name"
        DataGridView1.Columns(1).DataPropertyName = "hospital_ID"
        DataGridView1.Columns(2).HeaderText = "Doctor name"
        DataGridView1.Columns(2).DataPropertyName = "doctor_ID"
        DataGridView1.Columns(3).HeaderText = "Status"
        DataGridView1.Columns(3).DataPropertyName = "status"
        DataGridView1.Columns(4).HeaderText = "Cost"
        DataGridView1.Columns(4).DataPropertyName = "total_fees"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub



    Private Sub d2_Click(sender As Object, e As EventArgs) Handles d2.Click
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT * FROM pharmacybill where patient_id = @Value ", Con)
        cmd.Parameters.AddWithValue("@Value", uid)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).HeaderText = "Date"
        DataGridView1.Columns(0).DataPropertyName = "Billing_time"
        DataGridView1.Columns(1).HeaderText = "Pharmacy name"
        DataGridView1.Columns(1).DataPropertyName = "pharmacy_id"
        DataGridView1.Columns(2).HeaderText = "Medicine"
        DataGridView1.Columns(2).DataPropertyName = "medicine_id"
        DataGridView1.Columns(3).HeaderText = "Quantity"
        DataGridView1.Columns(3).DataPropertyName = ""
        DataGridView1.Columns(4).HeaderText = "Cost"
        DataGridView1.Columns(3).DataPropertyName = "total_price"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub



    Private Sub d3_Click(sender As Object, e As EventArgs) Handles d3.Click
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT * FROM blood_donation where patient_id = @Value", Con)
        cmd.Parameters.AddWithValue("@Value", uid)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).HeaderText = "Date"
        DataGridView1.Columns(0).DataPropertyName = "time"
        DataGridView1.Columns(1).HeaderText = "Hospital name"
        DataGridView1.Columns(1).DataPropertyName = "hospital_ID"
        DataGridView1.Columns(2).HeaderText = "Blood Group"
        DataGridView1.Columns(2).DataPropertyName = "Blood_grp"
        DataGridView1.Columns(3).HeaderText = "Status"
        DataGridView1.Columns(3).DataPropertyName = "status"
        DataGridView1.Columns(4).HeaderText = "Cost"
        For Each row As DataGridViewRow In DataGridView1.Rows
            row.Cells(4).Value = "0"
        Next

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub



End Class