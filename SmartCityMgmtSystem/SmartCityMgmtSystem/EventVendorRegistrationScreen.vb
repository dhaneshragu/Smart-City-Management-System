
Imports MySql.Data.MySqlClient
Public Class EventVendorRegistrationScreen

    Public Property uid As Integer
    Public Property u_name As String

    'I need to update the vendor table(insert into the vendor table;


    Private Function GetVendorID() As Integer
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand
        Dim vendorID As Integer = -1

        Try
            Con.Open()

            ' Use parameterized query to prevent SQL injection
            Dim query As String = "SELECT COUNT(*) FROM vendor"
            cmd = New MySqlCommand(query, Con)

            ' Execute the query to retrieve the vendor ID
            vendorID = Convert.ToInt32(cmd.ExecuteScalar())

            vendorID += 1
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try

        Return vendorID
    End Function
    Private Sub InsertVendor(ByVal specialisation As String, ByVal vendorName As String, ByVal startDate As Date, ByVal endDate As Date, ByVal vendorID As Integer, ByVal ServiceCost As Integer, ByVal password As String)
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand

        Try
            Con.Open()

            ' Use parameterized query to prevent SQL injection
            Dim query As String = "INSERT INTO vendor (vendorID, vendorName, specialisation, rating, experience, password,serviceCost,startdate,enddate,vendor_UID) " &
                              "VALUES (@VendorID, @VendorName, @Specialisation, @Rating, @Experience, @Password,@ServiceCost,@StartDate,@EndDate,@vendorUID);"
            'Initially building it in such a way that the cost is decided by the vendor instead of it being fixed by the ministry 
            'and the vendor ID shall be the username 
            'This shall be extended later to include a custom username and govt fixated service cost/hr
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@VendorId", vendorID)
            cmd.Parameters.AddWithValue("VendorName", vendorName)
            cmd.Parameters.AddWithValue("@Specialisation", specialisation)
            cmd.Parameters.AddWithValue("@Rating", 0)
            cmd.Parameters.AddWithValue("@Experience", 0)
            cmd.Parameters.AddWithValue("@StartDate", startDate)
            cmd.Parameters.AddWithValue("@EndDate", endDate)
            cmd.Parameters.AddWithValue("@ServiceCost", ServiceCost)
            cmd.Parameters.AddWithValue("@Password", password)
            cmd.Parameters.AddWithValue("@vendorUID", uid)

            ' Execute the SQL command
            cmd.ExecuteNonQuery()

            MessageBox.Show("Vendor inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
    End Sub
    Private Sub EventVendorRegistrationScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cost As Integer
        TextBox1.Text = u_name
        TextBox2.Text = uid
        TextBox4.Text = cost
        'Error Handling needs to be done to check the situation in which this aint an int
        TextBox5.PasswordChar = "*"
        Dim Password As String
        Password = GetPasswordFromUserId()
        TextBox5.Text = Password
        TextBox5.ReadOnly = True

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
            Label10.Text = "Strong"
            Label10.ForeColor = Color.Green
            'Console.WriteLine("Strong")
            Return True
        ElseIf (hasLower OrElse hasUpper) AndAlso specialChar AndAlso (password_len >= 6) Then
            Label10.Text = "Moderate"
            Label10.ForeColor = Color.Blue
            'Console.WriteLine("Moderate")
            Return True
        Else
            Label10.Text = "Weak"
            Label10.ForeColor = Color.Red
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim VendorName As String = TextBox1.Text
        Dim VendorID As String = GetVendorID()
        '//////////////////////////////////////////////////////////////////////////////////

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
        '//////////////////////////////////////////////////////////////////////////////////
        Dim EventStartDate As Date = DateTimePicker1.Value.Date
        Dim EventEndDate As Date = DateTimePicker2.Value.Date

        ' Check if the event end date is not smaller than the event start date
        If EventEndDate < EventStartDate Then
            MessageBox.Show("Error: Event end date cannot be smaller than event start date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit from the function
        End If
        '//////////////////////////////////////////////////////////////////////////////////
        Dim EventType As String = If(ComboBox1.SelectedItem IsNot Nothing, ComboBox1.SelectedItem.ToString(), "")

        ' Check if the event type is empty
        If String.IsNullOrEmpty(EventType) Then
            MessageBox.Show("Error: Please select an event type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit from the function
        End If
        '//////////////////////////////////////////////////////////////////////////////////

        Dim ServiceCost As String = TextBox4.Text
        ' Check if the ServiceCost is empty
        If String.IsNullOrEmpty(ServiceCost) Then
            MessageBox.Show("Error: Please enter the service cost.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit from the function
        End If

        ' Check if the ServiceCost is numeric
        Dim numericServiceCost As Decimal
        If Not Decimal.TryParse(ServiceCost, numericServiceCost) Then
            MessageBox.Show("Error: Please enter a valid numeric service cost.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit from the function
        End If
        '//////////////////////////////////////////////////////////////////////////////////
        Dim Password As String = TextBox5.Text

        If String.IsNullOrEmpty(Password) Then
            MessageBox.Show("Error: Please enter a password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit from the function
        End If

        ' Check the strength of the password
        If Not password_strength_check() Then
            MessageBox.Show("Error: Please enter a strong password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit from the function
        End If

        '//////////////////////////////////////////////////////////////////////////////////

        'Error Handling needs to be done to check the situation in which this aint an int
        InsertVendor(EventType, VendorName, EventStartDate, EventEndDate, CInt(VendorID), CInt(ServiceCost), Password)
        MessageBox.Show("You have been registered and your Vendor ID is : " & VendorID, "Vendor Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'EventDashboard.Show()
        Me.Close()
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        ' Your tracing logic here
        password_strength_check()
    End Sub

    'Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
    '    Dim EventStartDate1 As Date = DateTimePicker1.Value
    '    Dim EventEndDate1 As Date = DateTimePicker2.Value
    '    Dim EventType1 As String = ComboBox1.SelectedItem.ToString()
    '    LoadandBindDataGridView(EventStartDate1, EventEndDate1, EventType1)
    'End Sub
End Class