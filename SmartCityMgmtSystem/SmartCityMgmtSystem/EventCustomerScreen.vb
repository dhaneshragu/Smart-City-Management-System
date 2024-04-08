Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class EventCustomerScreen
    Public Property uid As Integer
    Public Property password As String
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is in the "EditBut" column and not a header cell
        If e.ColumnIndex = DataGridView1.Columns("EditBut").Index AndAlso e.RowIndex >= 0 Then
            ' Change this to DB logic later
            MessageBox.Show("Edit button clicked for row " & e.RowIndex.ToString(), "Edit Entry", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Check if the clicked cell is in the "DeleteBut" column and not a header cell
        ElseIf e.ColumnIndex = DataGridView1.Columns("DeleteBut").Index AndAlso e.RowIndex >= 0 Then
            ' Perform the action for the "DeleteButton" column
            MessageBox.Show("Delete button clicked for row " & e.RowIndex.ToString(), "Delete Entry", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

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

        ' Use parameterized query to prevent SQL injection and handle dates properly
        Dim query As String = "SELECT eb.startdate AS EventStartDate,  v.vendorName AS VendorName, eb.transactionID AS TransactionID FROM eventBookings AS eb INNER JOIN  Vendor AS v ON eb.vendorID = v.vendorID WHERE eb.customerID = @CustomerID  AND eb.password = @Password"

        cmd = New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@CustomerID", uid)
        cmd.Parameters.AddWithValue("@Password", password)

        reader = cmd.ExecuteReader()

        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        'DataGridView1.Columns(0).DataPropertyName = "vendorID"
        DataGridView1.Columns(0).DataPropertyName = "EventStartDate"
        'DataGridView1.Columns(2).DataPropertyName = "specialisation"
        DataGridView1.Columns(1).DataPropertyName = "VendorName"
        DataGridView1.Columns(2).DataPropertyName = "TransactionID"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()

        ' Dummy Data, Change it to LoadandBindDataGridView() 
        If False Then
            For i As Integer = 1 To 8
                ' Add an empty row to the DataGridView
                Dim row As New DataGridViewRow()
                DataGridView1.Rows.Add(row)

                ' Set values for the first three columns in the current row
                DataGridView1.Rows(i - 1).Cells("Column1").Value = "DummyVal"
                DataGridView1.Rows(i - 1).Cells("Column2").Value = "DummyVal"
                DataGridView1.Rows(i - 1).Cells("Column3").Value = "DummyVal"
            Next

            ' Add an empty row to the DataGridView
            Dim row0 As New DataGridViewRow()
            DataGridView1.Rows.Add(row0)
            ' Set values for the first three columns in the current row
            DataGridView1.Rows(0).Cells("Column1").Value = "18 April 2024"
            DataGridView1.Rows(0).Cells("Column2").Value = "ABC Traders"
            DataGridView1.Rows(0).Cells("Column3").Value = "121551215451"

            ' Add an empty row to the DataGridView
            Dim row1 As New DataGridViewRow()
            DataGridView1.Rows.Add(row1)
            ' Set values for the first three columns in the current row
            DataGridView1.Rows(1).Cells("Column1").Value = "22 May 2024"
            DataGridView1.Rows(1).Cells("Column2").Value = "Ramesh and Sons"
            DataGridView1.Rows(1).Cells("Column3").Value = "898551215451"

            ' Add an empty row to the DataGridView
            Dim row2 As New DataGridViewRow()
            DataGridView1.Rows.Add(row2)
            ' Set values for the first three columns in the current row
            DataGridView1.Rows(2).Cells("Column1").Value = "26 November 2024"
            DataGridView1.Rows(2).Cells("Column2").Value = "Modern Trade Center"
            DataGridView1.Rows(2).Cells("Column3").Value = "999962454225"
        End If
    End Sub


End Class