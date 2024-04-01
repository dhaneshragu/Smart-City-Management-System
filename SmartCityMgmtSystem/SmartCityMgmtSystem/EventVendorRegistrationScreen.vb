Imports System.Data.SqlClient
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
            vendorID *= 10000
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
            Dim query As String = "INSERT INTO vendor (vendorID, vendorName, specialisation, rating, experience, password,serviceCost,startdate,enddate) " &
                              "VALUES (@VendorID, @VendorName, @Specialisation, @Rating, @Experience, @Password,@ServiceCost,@StartDate,@EndDate);"
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



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim VendorName As String = TextBox1.Text
        Dim VendorID As String = GetVendorID()
        Dim ContactNo As String = TextBox3.Text
        Dim EventStartDate As Date = DateTimePicker1.Value
        Dim EventEndDate As Date = DateTimePicker2.Value
        Dim EventType As String = ComboBox1.SelectedItem.ToString()
        Dim ServiceCost As String = TextBox4.Text
        Dim Password As String = TextBox5.Text


        'Error Handling needs to be done to check the situation in which this aint an int
        InsertVendor(EventType, VendorName, EventStartDate, EventEndDate, CInt(VendorID), CInt(ServiceCost), Password)
        MessageBox.Show("You have been registered and your Vendor ID is : " & VendorID, "Vendor Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'EventDashboard.Show()
        Me.Close()
    End Sub

    'Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
    '    Dim EventStartDate1 As Date = DateTimePicker1.Value
    '    Dim EventEndDate1 As Date = DateTimePicker2.Value
    '    Dim EventType1 As String = ComboBox1.SelectedItem.ToString()
    '    LoadandBindDataGridView(EventStartDate1, EventEndDate1, EventType1)
    'End Sub
End Class