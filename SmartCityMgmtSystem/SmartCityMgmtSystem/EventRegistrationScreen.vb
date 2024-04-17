Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class EventRegistrationScreen


    Public Property uid As Integer
    Public Property u_name As String

    Dim cost As Integer


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is in the "EditBut" column and not a header cell
        If e.ColumnIndex = DataGridView1.Columns("EditBut").Index AndAlso e.RowIndex >= 0 Then
            ' Retrieve the vendor name from the selected row
            Dim vendorName As String = DataGridView1.Rows(e.RowIndex).Cells("Column1").Value.ToString()

            ' Call a method to retrieve the vendor ID based on the vendor name
            Dim vendorID As Integer = GetVendorID(vendorName)

            ' Retrieve the cost from the DataTable based on the vendor name
            cost = GetCostFromDB(vendorName)

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
            Dim query As String = "SELECT vendorID FROM vendor WHERE vendorName = @VendorName;"
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
            Dim query As String = "SELECT serviceCost FROM vendor WHERE vendorName = @VendorName;"
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

    Private Function GetVendorUID(ByVal vendorID As Integer) As Integer
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand
        Dim vendorUID As Integer = -1

        Try
            Con.Open()

            ' Use parameterized query to prevent SQL injection
            Dim query As String = "SELECT vendor_UID FROM vendor WHERE vendorID = @VendorID;"
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@VendorID", vendorID)

            ' Execute the query to retrieve the vendor UID
            vendorUID = Convert.ToInt32(cmd.ExecuteScalar())
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try

        Return vendorUID
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
        Dim query As String = "SELECT DISTINCT v.vendorID, v.vendorName, v.specialisation, v.rating, v.experience " &
                          "FROM vendor v " &
                          "LEFT JOIN eventbookings eb ON v.vendorID = eb.vendorID " &
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
        Dim TransactionID As String = "453518413251515"
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand

        Try
            Con.Open()


            ' Query to retrieve account number based on UID
            Dim accountQuery As String = "SELECT account_number FROM account WHERE user_id = @UID;"
            cmd = New MySqlCommand(accountQuery, Con)
            cmd.Parameters.AddWithValue("@UID", uid)

            ' Execute the query to fetch the account number
            Dim accountNumber As Integer = Convert.ToInt32(cmd.ExecuteScalar())


            ' Query to retrieve the transactionID of the last transaction for the given account number
            Dim transactionQuery As String = "SELECT transaction_id FROM transactions WHERE sender_account = @AccountNumber ORDER BY time DESC LIMIT 1;"
            cmd = New MySqlCommand(transactionQuery, Con)
            cmd.Parameters.AddWithValue("@AccountNumber", accountNumber)

            ' Execute the query to fetch the last transactionID
            Dim lastTransactionID As String = Convert.ToString(cmd.ExecuteScalar())




            ' Use parameterized query to prevent SQL injection
            Dim query As String = "INSERT INTO eventbookings (specialisation, startdate, enddate, vendorID, customerID, password,transactionID) " &
                              "VALUES (@Specialisation, @StartDate, @EndDate, @VendorID, @CustomerID, @Password,@TransactionID);"

            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@Specialisation", specialisation)
            cmd.Parameters.AddWithValue("@StartDate", startDate)
            cmd.Parameters.AddWithValue("@EndDate", endDate)
            cmd.Parameters.AddWithValue("@VendorID", vendorID)
            cmd.Parameters.AddWithValue("@CustomerID", customerID)
            cmd.Parameters.AddWithValue("@Password", password)
            cmd.Parameters.AddWithValue("@TransactionID", lastTransactionID)

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
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=123456;database=TelephoneDatabase;"
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

    Private Function password_strength_check() As Boolean
        Dim password As String = TextBox5.Text
        Dim password_len As Integer = password.Length

        ' Checking lower alphabet in string 
        Dim hasLower As Boolean = False, hasUpper As Boolean = False
        Dim hasDigit As Boolean = False, specialChar As Boolean = False
        Dim normalChars As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 "

        For i As Integer = 0 To password_len - 1
            If Char.IsLower(password(i)) Then
                hasLower = True
            End If
            If Char.IsUpper(password(i)) Then
                hasUpper = True
            End If
            If Char.IsDigit(password(i)) Then
                hasDigit = True
            End If

            Dim special As Integer = password.IndexOfAny(normalChars.ToCharArray())
            If special <> -1 Then
                specialChar = True
            End If
        Next

        If hasLower AndAlso hasUpper AndAlso hasDigit AndAlso specialChar AndAlso (password_len >= 8) Then
            Label12.Text = "Strong"
            Label12.ForeColor = Color.Green
            'Console.WriteLine("Strong")
            Return True
        ElseIf (hasLower OrElse hasUpper) AndAlso specialChar AndAlso (password_len >= 6) Then
            Label12.Text = "Moderate"
            Label12.ForeColor = Color.Blue
            'Console.WriteLine("Moderate")
            Return True
        Else
            Label12.Text = "Weak"
            Label12.ForeColor = Color.Red
            'Console.WriteLine("Weak")
            Return False
        End If
    End Function


    Public Function GetPasswordFromUserId() As String
        Dim password As String = ""

        ' Get connection from globals
        Dim con As MySqlConnection = Globals.GetDBConnection()

        Try
            con.Open()

            ' Query to retrieve password from the users table based on user_id
            Dim query As String = "SELECT password FROM users WHERE user_id = @UserId;"
            Dim cmd As New MySqlCommand(query, con)
            cmd.Parameters.AddWithValue("@UserId", uid)

            ' Execute the query to fetch the password
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                password = reader.GetString(0)
            End If
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try

        Return password
    End Function

    Public Sub UpdateVendorExperience(ByVal vendorId As Integer)
        ' Get connection from globals
        Dim con As MySqlConnection = Globals.GetDBConnection()

        Try
            con.Open()

            ' Query to update the experience column of the vendor table
            Dim query As String = "UPDATE vendor SET experience = experience + 1 WHERE vendorID = @VendorId;"
            Dim cmd As New MySqlCommand(query, con)
            cmd.Parameters.AddWithValue("@VendorId", vendorId)

            ' Execute the query to update the experience
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
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
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True



        TextBox5.PasswordChar = "*"
        Dim Password As String
        Password = GetPasswordFromUserId()
        TextBox5.Text = Password
        TextBox5.ReadOnly = True

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
        '///////////////////////////////////////////////////////////////////////////////
        Dim ContactNo As String = TextBox3.Text
        ' Check if the entered contact number is numeric
        If Not IsNumeric(ContactNo) Then
            MessageBox.Show("Contact number must be numeric.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit the function if the contact number is not numeric
        End If

        ' Check if the contact number has exactly 10 digits
        If ContactNo.Length <> 10 Then
            MessageBox.Show("Contact number must have exactly 10 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit the function if the contact number does not have 10 digits
        End If

        '///////////////////////////////////////////////////////////////////////////////

        Dim EventStartDate As Date = DateTimePicker1.Value.Date
        Dim EventEndDate As Date = DateTimePicker2.Value.Date
        ' Check if the event end date is not smaller than the event start date
        If EventEndDate < EventStartDate Then
            MessageBox.Show("Error: Event end date cannot be smaller than event start date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit from the function
        End If


        '///////////////////////////////////////////////////////////////////////////////
        'Dim EventType As String = ComboBox1.SelectedItem.ToString()
        Dim EventType As String = If(ComboBox1.SelectedItem IsNot Nothing, ComboBox1.SelectedItem.ToString(), "")

        ' Check if the event type is empty
        If String.IsNullOrEmpty(EventType) Then
            MessageBox.Show("Error: Please select an event type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit from the function
        End If

        '///////////////////////////////////////////////////////////////////////////////

        Dim VendorID As String = TextBox4.Text
        Dim VendorIDINT As Integer
        If Integer.TryParse(TextBox4.Text, VendorIDINT) Then
            ' Conversion successful, VendorID now holds the integer value
            ' You can use VendorID further in your code
        Else
            ' Conversion failed, handle the error here
            MessageBox.Show("Invalid Vendor ID. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        '///////////////////////////////////////////////////////////////////////////////
        Dim Password As String
        'Password = GetPasswordFromUserId()
        Password = TextBox5.Text
        ' Check if the password is empty
        If String.IsNullOrEmpty(Password) Then
            MessageBox.Show("Error: Please enter a password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit from the function
        End If

        ' Check the strength of the password
        If Not password_strength_check() Then
            MessageBox.Show("Error: Please enter a strong password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit from the function
        End If

        '///////////////////////////////////////////////////////////////////////////////






        Dim isPaymentSuccessful As Boolean = False


        Dim pay = New PaymentGateway() With {
            .uid = uid,
            .readonly_prop = True
        }
        pay.TextBox1.Text = GetVendorUID(VendorIDINT)
        pay.TextBox2.Text = cost
        pay.TextBox3.Text = "Payment for Event Registration"
        If (pay.ShowDialog() = DialogResult.OK) Then
            MessageBox.Show("Payment successful!")
            isPaymentSuccessful = True
            Me.Close()
        Else
            MessageBox.Show("Payment failed.")
            isPaymentSuccessful = False
        End If

        If isPaymentSuccessful Then
            UpdateVendorExperience(VendorIDINT)
            InsertEventBooking(EventType, EventStartDate, EventEndDate, CInt(VendorID), CInt(CustomerID), Password)
            'EventDashboard.Show()
            Me.Close()
        Else
            ' Redirect to the dashboard or display a message
            MessageBox.Show("Payment was not successful. Redirecting to dashboard...", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Add code here to redirect to the dashboard or perform any other action
            'EventDashboard.Show()
            Me.Close()
        End If










    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim EventStartDate1 As Date = DateTimePicker1.Value
        Dim EventEndDate1 As Date = DateTimePicker2.Value
        Dim EventType1 As String = ComboBox1.SelectedItem.ToString()
        LoadandBindDataGridView(EventStartDate1, EventEndDate1, EventType1)
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        ' Your tracing logic here
        password_strength_check()
    End Sub


End Class