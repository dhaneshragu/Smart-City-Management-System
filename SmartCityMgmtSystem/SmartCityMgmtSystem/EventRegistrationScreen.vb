Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class EventRegistrationScreen


    Public Property uid As Integer
    Public Property u_name As String



    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is in the "EditBut" column and not a header cell
        If e.ColumnIndex = DataGridView1.Columns("EditBut").Index AndAlso e.RowIndex >= 0 Then
            ' Retrieve the vendor name from the selected row
            Dim vendorName As String = DataGridView1.Rows(e.RowIndex).Cells("Column1").Value.ToString()

            ' Call a method to retrieve the vendor ID based on the vendor name
            Dim vendorID As Integer = GetVendorID(vendorName)

            ' Retrieve the cost from the DataTable based on the vendor name
            Dim cost As Integer = GetCostFromDB(vendorName)

            ' Populate the vendor ID in TextBox4
            TextBox4.Text = vendorID.ToString()
            Label15.Text = cost.ToString()
            Label16.Text = cost.ToString()


            MessageBox.Show("Vendor ID: " & TextBox4.Text, "Edit Entry", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf e.ColumnIndex = DataGridView1.Columns("DeleteBut").Index AndAlso e.RowIndex >= 0 Then
            ' Perform the action for the "DeleteButton" column
            MessageBox.Show("Delete button clicked for row " & e.RowIndex.ToString(), "Delete Entry", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Function GetVendorID(ByVal vendorName As String) As Integer
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand
        Dim vendorID As Integer = -1

        Try
            Con.Open()

            ' Use parameterized query to prevent SQL injection
            Dim query As String = "SELECT vendorID FROM Vendor WHERE vendorName = @VendorName;"
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@VendorName", vendorName)

            ' Execute the query to retrieve the vendor ID
            vendorID = Convert.ToInt32(cmd.ExecuteScalar())
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try

        Return vendorID
    End Function


    Private Function GetCostFromDB(ByVal vendorName As String) As Integer
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand
        Dim cost As Integer = -1

        Try
            Con.Open()

            ' Use parameterized query to prevent SQL injection
            Dim query As String = "SELECT serviceCost FROM Vendor WHERE vendorName = @VendorName;"
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@VendorName", vendorName)

            ' Execute the query to retrieve the cost
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                cost = Convert.ToInt32(result)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try

        Return cost
    End Function



    Private Sub LoadandBindDataGridView(ByVal startDate As Date, ByVal endDate As Date, ByVal eventType As String)
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
        Dim query As String = "SELECT v.vendorID, v.vendorName, v.specialisation, v.rating, v.experience " &
                          "FROM Vendor v " &
                          "LEFT JOIN eventBookings eb ON v.vendorID = eb.vendorID " &
                          "WHERE v.specialisation = @EventType " &
                          "AND (eb.startdate IS NULL OR eb.enddate < @EventEndDate OR eb.startdate > @EventStartDate);"

        cmd = New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@EventType", eventType)
        cmd.Parameters.AddWithValue("@EventStartDate", startDate)
        cmd.Parameters.AddWithValue("@EventEndDate", endDate)

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
        DataGridView1.Columns(0).DataPropertyName = "vendorName"
        'DataGridView1.Columns(2).DataPropertyName = "specialisation"
        DataGridView1.Columns(1).DataPropertyName = "rating"
        DataGridView1.Columns(2).DataPropertyName = "experience"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub


    Private Sub InsertEventBooking(ByVal specialisation As String, ByVal startDate As Date, ByVal endDate As Date, ByVal vendorID As Integer, ByVal customerID As Integer, ByVal password As String)
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand

        Try
            Con.Open()

            ' Use parameterized query to prevent SQL injection
            Dim query As String = "INSERT INTO eventBookings (specialisation, startdate, enddate, vendorID, customerID, password) " &
                              "VALUES (@Specialisation, @StartDate, @EndDate, @VendorID, @CustomerID, @Password);"

            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@Specialisation", specialisation)
            cmd.Parameters.AddWithValue("@StartDate", startDate)
            cmd.Parameters.AddWithValue("@EndDate", endDate)
            cmd.Parameters.AddWithValue("@VendorID", vendorID)
            cmd.Parameters.AddWithValue("@CustomerID", customerID)
            cmd.Parameters.AddWithValue("@Password", password)

            ' Execute the SQL command
            cmd.ExecuteNonQuery()

            MessageBox.Show("Event booking inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
    End Sub


    Dim Mysqlconn As New MySqlConnection
    Dim sqlCmd As New MySqlCommand
    Dim sqlCmd2 As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlRd2 As MySqlDataReader
    Dim sqlDt As New DataTable
    Dim Dta As New MySqlDataAdapter
    Dim SqlQuery As String
    Dim SqlQuery2 As String


    Private Sub Viewtable()
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=123456;database=TelephoneDatabase;"
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = "Select * from UserData;"

        sqlRd = sqlCmd.ExecuteReader
        sqlDt.Load(sqlRd)

        sqlRd.Close()
        Mysqlconn.Close()
        MessageBox.Show("Successful Connection")
        DataGridView1.DataSource = sqlDt
    End Sub

    Private Sub Insert_table()
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"
        Mysqlconn.Open()

        'SqlQuery = "Insert into UserData(name,IITG_email,phonenumber,role,password,department,plan_id,expiry_date,talktimeLeft,dataLeft,user_visibility) values ('Aasneh','p.aasneh@iitg.ac.in','7021901677','Student','Aasneh','CSE',0,'03-03-2024',0,0,'Public');"


        sqlCmd.Connection = Mysqlconn
        sqlCmd2.Connection = Mysqlconn

        sqlCmd.CommandText = SqlQuery
        sqlCmd2.CommandText = SqlQuery2

        sqlRd = sqlCmd.ExecuteReader
        sqlRd.Close()
        sqlRd2 = sqlCmd2.ExecuteReader
        sqlRd2.Close()
        'sqlDt.Load(sqlRd)


        Mysqlconn.Close()
        'MessageBox.Show("Successful Connection")
        'DataGridView1.DataSource = sqlDt




    End Sub

    Private Sub EventRegistrationScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        ' Add options to the ComboBox
        ComboBox1.Items.Add("Marriage")
        ComboBox1.Items.Add("Religious Rites")
        ComboBox1.Items.Add("Orchestra")
        ComboBox1.Items.Add("Campaign")
        ComboBox1.Items.Add("Lecture")
        ComboBox1.Items.Add("Movie Screening")
        ComboBox1.Items.Add("Drama")
        ComboBox1.Items.Add("Exhibition")
        ComboBox1.Items.Add("Art Gallery")

        TextBox1.Text = u_name
        TextBox2.Text = uid




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
        End If

        If False Then
            ' Add an empty row to the DataGridView
            Dim row0 As New DataGridViewRow()
            DataGridView1.Rows.Add(row0)
            ' Set values for the first three columns in the current row
            DataGridView1.Rows(0).Cells("Column1").Value = "ABC Traders"
            DataGridView1.Rows(0).Cells("Column2").Value = "4.7"
            DataGridView1.Rows(0).Cells("Column3").Value = "52"

            ' Add an empty row to the DataGridView
            Dim row1 As New DataGridViewRow()
            DataGridView1.Rows.Add(row1)
            ' Set values for the first three columns in the current row
            DataGridView1.Rows(1).Cells("Column1").Value = "Ramesh and Sons"
            DataGridView1.Rows(1).Cells("Column2").Value = "3.7"
            DataGridView1.Rows(1).Cells("Column3").Value = "142"

            ' Add an empty row to the DataGridView
            Dim row2 As New DataGridViewRow()
            DataGridView1.Rows.Add(row2)
            ' Set values for the first three columns in the current row
            DataGridView1.Rows(2).Cells("Column1").Value = "Modern Trade Center"
            DataGridView1.Rows(2).Cells("Column2").Value = "4.88"
            DataGridView1.Rows(2).Cells("Column3").Value = "12"


        End If

    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim CustomerName As String = u_name
        Dim CustomerID As String = uid
        Dim ContactNo As String = TextBox3.Text
        Dim EventStartDate As Date = DateTimePicker1.Value
        Dim EventEndDate As Date = DateTimePicker2.Value
        Dim EventType As String = ComboBox1.SelectedItem.ToString()
        Dim VendorID As String = TextBox4.Text
        Dim Password As String = TextBox5.Text



        InsertEventBooking(EventType, EventStartDate, EventEndDate, CInt(VendorID), CInt(CustomerID), Password)


        EventDashboard.Show()
        Me.Close()




    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim EventStartDate1 As Date = DateTimePicker1.Value
        Dim EventEndDate1 As Date = DateTimePicker2.Value
        Dim EventType1 As String = ComboBox1.SelectedItem.ToString()
        LoadandBindDataGridView(EventStartDate1, EventEndDate1, EventType1)
    End Sub
End Class