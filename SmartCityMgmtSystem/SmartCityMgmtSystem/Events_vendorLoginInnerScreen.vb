Imports System.ComponentModel.DataAnnotations
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class Events_vendorLoginInnerScreen
    Public Property uid As Integer
    Public Property password As String

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
        Dim query As String = "SELECT  eb.customerID AS CustomerID, eb.transactionID AS TransactionID FROM eventBookings AS eb INNER JOIN  Vendor AS v ON eb.vendorID = v.vendorID WHERE v.vendorID = @VendorID AND v.password = @Password"

        cmd = New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@VendorID", uid)
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
        DataGridView1.Columns(0).DataPropertyName = "CustomerID"
        'DataGridView1.Columns(2).DataPropertyName = "specialisation"
        DataGridView1.Columns(1).DataPropertyName = "TransactionID"
        'DataGridView1.Columns(2).DataPropertyName = "Time"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub


    'Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    ' Dummy Data, Change it to LoadandBindDataGridView() 
    '    'For i As Integer = 1 To 8
    '    '    ' Add an empty row to the DataGridView
    '    '    Dim row As New DataGridViewRow()
    '    '    DataGridView1.Rows.Add(row)

    '    '    ' Set values for the first three columns in the current row
    '    '    DataGridView1.Rows(i - 1).Cells("SlNo").Value = i
    '    '    DataGridView1.Rows(i - 1).Cells("CustomerID").Value = i * 1000
    '    '    DataGridView1.Rows(i - 1).Cells("TransactionID").Value = 3 * i + 4 * (i) * (i)

    '    '    ' Set timestamp for the "Time" column in the current row
    '    '    Dim currentTime As DateTime = DateTime.Now.AddHours(i * 37) ' Add 37 hours for each row
    '    '    DataGridView1.Rows(i - 1).Cells("Time").Value = currentTime
    '    'Next

    '    ' Add a button column for "Invoice"
    '    Dim invoiceButtonColumn As New DataGridViewButtonColumn()
    '    invoiceButtonColumn.HeaderText = "Invoice"
    '    invoiceButtonColumn.Text = "Show"
    '    invoiceButtonColumn.Name = "Invoice"
    '    invoiceButtonColumn.UseColumnTextForButtonValue = True
    '    DataGridView1.Columns.Add(invoiceButtonColumn)
    'End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is in the "Invoice" column and the clicked item is a button
        If e.ColumnIndex = DataGridView1.Columns("Invoice").Index AndAlso e.RowIndex >= 0 Then
            ' Handle button click for the "Invoice" column
            ' Here you can write code to handle what happens when the button is clicked
            MessageBox.Show("Invoice button clicked for row " & (e.RowIndex + 1))
        End If
    End Sub

    Private Sub Events_vendorLoginInnerScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
        'Add a button column for "Invoice"
        Dim invoiceButtonColumn As New DataGridViewButtonColumn()
        invoiceButtonColumn.HeaderText = "Invoice"
        invoiceButtonColumn.Text = "Show"
        invoiceButtonColumn.Name = "Invoice"
        invoiceButtonColumn.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Add(invoiceButtonColumn)
    End Sub
End Class
