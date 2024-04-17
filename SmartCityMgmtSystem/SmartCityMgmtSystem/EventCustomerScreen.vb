Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class EventCustomerScreen
    Public Property uid As Integer
    Public Property password As String
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is in the "EditBut" column and not a header cell
        If e.ColumnIndex = DataGridView1.Columns("EditBut").Index AndAlso e.RowIndex >= 0 Then
            ' Prompt the user to enter the rating
            Dim ratingInput As String = InputBox("Enter the rating (0-5):", "Edit Rating")

            ' Validate the rating
            Dim rating As Integer
            If Integer.TryParse(ratingInput, rating) AndAlso rating >= 0 AndAlso rating <= 5 Then
                ' Get the vendor name from the DataGridView
                Dim vendorName As String = DataGridView1.Rows(e.RowIndex).Cells("Column2").Value.ToString()
                ' Update the corresponding vendor's rating in the vendor table
                UpdateVendorRating(vendorName, rating)
                MessageBox.Show("Rating updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Please enter a valid rating between 0 and 5.", "Invalid Rating", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            ' Check if the clicked cell is in the "DeleteBut" column and not a header cell
        ElseIf e.ColumnIndex = DataGridView1.Columns("DeleteBut").Index AndAlso e.RowIndex >= 0 Then
            ' Perform the action for the "DeleteButton" column
            MessageBox.Show("Delete button clicked for row " & e.RowIndex.ToString(), "Delete Entry", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub


    Private Sub UpdateVendorRating(ByVal vendorName As String, ByVal rating As Integer)
        ' Get connection from globals
        Dim con As MySqlConnection = Globals.GetDBConnection()

        Try
            con.Open()

            ' Query to update the rating column of the vendor table based on vendor name
            Dim query As String = "UPDATE vendor SET rating = @Rating WHERE vendorName = @VendorName;"
            Dim cmd As New MySqlCommand(query, con)
            cmd.Parameters.AddWithValue("@Rating", rating)
            cmd.Parameters.AddWithValue("@VendorName", vendorName)

            ' Execute the query to update the rating
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
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
        Dim query As String = "SELECT DISTINCT eb.startdate AS EventStartDate,  v.vendorName AS VendorName, eb.transactionID AS TransactionID FROM eventbookings AS eb INNER JOIN  vendor AS v ON eb.vendorID = v.vendorID WHERE eb.customerID = @CustomerID  AND eb.password = @Password"

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