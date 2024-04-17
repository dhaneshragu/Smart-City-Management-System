Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Healthcare_History
    Public Property uid As Integer = 13
    Private Sub d1_Click(sender As Object, e As EventArgs) Handles d1.Click
        ' Set d1's background color to green
        d1.BackColor = Color.Green
        ' Set d2 and d3's background color to the default color
        d2.BackColor = SystemColors.Highlight
        d3.BackColor = SystemColors.Highlight
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        cmd = New MySqlCommand("SELECT *
                          FROM appointments r
                          JOIN hospitaldb m ON r.hospital_ID = m.hospital_ID
                          JOIN doctordb p ON r.doctor_ID = p.doctor_ID
                          where patient_id = @Value ", Con)
        ' cmd = New MySqlCommand("SELECT * FROM appointments where patient_id = @Value", Con)
        cmd.Parameters.AddWithValue("@Value", uid)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()
        ' Set font and size for the text in DataGridView
        DataGridView1.DefaultCellStyle.Font = New Font("Arial", 14, FontStyle.Bold) ' 



        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).HeaderText = "Date"
        DataGridView1.Columns(0).DataPropertyName = "date"
        DataGridView1.Columns(1).HeaderText = "Hospital name"
        DataGridView1.Columns(1).DataPropertyName = "hospital_name"
        DataGridView1.Columns(2).HeaderText = "Doctor name"
        DataGridView1.Columns(2).DataPropertyName = "name"
        DataGridView1.Columns(3).HeaderText = "Status"
        DataGridView1.Columns(3).DataPropertyName = "status"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub



    Private Sub d2_Click(sender As Object, e As EventArgs) Handles d2.Click
        ' Set d1's background color to green
        d1.BackColor = SystemColors.Highlight
        ' Set d2 and d3's background color to the default color
        d2.BackColor = Color.Green
        d3.BackColor = SystemColors.Highlight
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT *
                          FROM pharmacybill r
                          JOIN medicine m ON r.medicine_id = m.medicine_id
                          JOIN pharmacydb p ON r.pharmacy_id = p.pharmacy_id
                          where patient_id = @Value ", Con)
        'cmd = New MySqlCommand("SELECT * FROM pharmacybill INNER JOIN pharmacydb ON pharmacybill.pharmacy_id=pharmacydb.pharmacy_id where patient_id = @Value ", Con)
        cmd.Parameters.AddWithValue("@Value", uid)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()
        ' Set font and size for the text in DataGridView
        DataGridView1.DefaultCellStyle.Font = New Font("Arial", 14, FontStyle.Bold) ' 


        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).HeaderText = "Date"
        DataGridView1.Columns(0).DataPropertyName = "Billing_time"
        DataGridView1.Columns(1).HeaderText = "Pharmacy name"
        DataGridView1.Columns(1).DataPropertyName = "pharmacy_name"
        DataGridView1.Columns(2).HeaderText = "Medicine"
        DataGridView1.Columns(2).DataPropertyName = "medicine_name"
        DataGridView1.Columns(3).HeaderText = "Quantity"
        DataGridView1.Columns(3).DataPropertyName = "quantity"



        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub



    Private Sub d3_Click(sender As Object, e As EventArgs) Handles d3.Click
        ' Set d1's background color to green
        d1.BackColor = SystemColors.Highlight
        ' Set d2 and d3's background color to the default color
        d2.BackColor = SystemColors.Highlight
        d3.BackColor = Color.Green
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT * FROM blood_donation INNER JOIN hospitaldb ON blood_donation.hospital_ID = hospitaldb.hospital_ID where patient_id = @Value", Con)
        cmd.Parameters.AddWithValue("@Value", uid)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()
        ' Set font and size for the text in DataGridView
        DataGridView1.DefaultCellStyle.Font = New Font("Arial", 14, FontStyle.Bold) '


        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).HeaderText = "Date"
        DataGridView1.Columns(0).DataPropertyName = "time"
        DataGridView1.Columns(1).HeaderText = "Hospital name"
        DataGridView1.Columns(1).DataPropertyName = "hospital_name"
        DataGridView1.Columns(2).HeaderText = "Blood Group"
        DataGridView1.Columns(2).DataPropertyName = "Blood_grp"
        DataGridView1.Columns(3).HeaderText = "Status"
        DataGridView1.Columns(3).DataPropertyName = "status"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Healthcare_History_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class