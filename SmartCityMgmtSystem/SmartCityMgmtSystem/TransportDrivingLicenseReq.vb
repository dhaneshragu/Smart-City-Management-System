Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Common
Imports Mysqlx.XDevAPI.Relational

Public Class TransportDrivingLicenseReq
    Public Property uid As Integer
    Public Property u_name As String
    Private payClicked As Boolean = False
    Private userDlId As Integer
    Private highestValidTill As DateTime
    Private renewal As Boolean = False
    Private newReq As Boolean = False
    Private Sub LoadAndBindData()
        Try
            Using connection As MySqlConnection = Globals.GetDBConnection()
                connection.Open()
                Dim dataTable2 As New DataTable()
                ' Load user data
                Dim userDataQuery As String = "SELECT name, age, profile_photo, gender, address FROM users WHERE user_id = @UserId"
                Using userDataCmd As New MySqlCommand(userDataQuery, connection)
                    userDataCmd.Parameters.AddWithValue("@UserId", uid)

                    Using reader As MySqlDataReader = userDataCmd.ExecuteReader()
                        If reader.Read() Then
                            Nametb.Text = u_name
                            Agetb.Text = If(Not IsDBNull(reader("age")), reader("age").ToString(), "NULL")
                            Addresstb.Text = If(Not IsDBNull(reader("address")), reader("address").ToString(), "NULL")

                            ' Load profile photo
                            If Not IsDBNull(reader("profile_photo")) Then
                                Dim profilePhotoData As Byte() = DirectCast(reader("profile_photo"), Byte())
                                If profilePhotoData IsNot Nothing AndAlso profilePhotoData.Length > 0 Then
                                    Using ms As New System.IO.MemoryStream(profilePhotoData)
                                        PictureBox1.Image = Image.FromStream(ms)
                                    End Using
                                End If
                            End If
                            PictureBox1.Image = PictureBox1.Image
                            ' Load profile photo
                            If Not IsDBNull(reader("profile_photo")) Then
                                Dim profilePhotoData As Byte() = DirectCast(reader("profile_photo"), Byte())
                                If profilePhotoData IsNot Nothing AndAlso profilePhotoData.Length > 0 Then
                                    Using ms As New System.IO.MemoryStream(profilePhotoData)
                                        PictureBox1.Image = Image.FromStream(ms)
                                    End Using
                                End If
                            End If

                            LabelName.Text = u_name
                            LabelAge.Text = If(Not IsDBNull(reader("age")), reader("age").ToString(), "NULL")
                            LabelAddress.Text = If(Not IsDBNull(reader("address")), reader("address").ToString(), "NULL")
                            LabelGender.Text = If(Not IsDBNull(reader("gender")), reader("gender").ToString(), "NULL")

                            If Not IsDBNull(reader("profile_photo")) Then
                                Dim profilePhotoData As Byte() = DirectCast(reader("profile_photo"), Byte())
                                If profilePhotoData IsNot Nothing AndAlso profilePhotoData.Length > 0 Then
                                    Using ms As New System.IO.MemoryStream(profilePhotoData)
                                        PictureBox2.Image = Image.FromStream(ms)
                                    End Using
                                End If
                            End If
                        Else
                            MessageBox.Show("No user data is available", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    End Using
                End Using

                ' Load driver's license entries
                Dim dlEntriesQuery As String = "SELECT dl_id, vehicle_type, issued_on, valid_till, test_status, fee_paid, req_type FROM dl_entries WHERE uid = @UserId"
                Using dlEntriesCmd As New MySqlCommand(dlEntriesQuery, connection)
                    dlEntriesCmd.Parameters.AddWithValue("@UserId", uid)

                    Dim reader As MySqlDataReader = dlEntriesCmd.ExecuteReader()
                    dataTable2.Load(reader)
                    ' Access data from the DataTable
                    If dataTable2.Rows.Count > 0 Then
                        Dim row As DataRow = dataTable2.Rows(0)
                        Dim dlId As Integer
                        If Not IsDBNull(row("dl_id")) Then
                            dlId = Convert.ToInt32(row("dl_id"))
                            LabelDLID.Text = dlId.ToString()
                        Else
                            LabelDLID.Text = ""
                        End If
                        Dim lowestissuedOn As DateTime = If(Not IsDBNull(row("issued_on")), DirectCast(row("issued_on"), DateTime), DateTime.MaxValue)
                        highestValidTill = If(Not IsDBNull(row("valid_till")), DirectCast(row("valid_till"), DateTime), DateTime.MinValue)
                        VTypeLB.Items.Clear()
                        For Each dr As DataRow In dataTable2.Rows
                            If Convert.ToInt32(dr("fee_paid")) = 1 And Convert.ToString(dr("test_status")) = "pass" Then
                                Panel4.Visible = True
                                Panel7.Visible = True
                                ' Access data from the current row
                                Dim vtype_id As Integer = Convert.ToInt32(dr("vehicle_type"))
                                Dim vType As String = TransportGlobals.GetVehicleType(vtype_id)
                                VTypeLB.Items.Add(vType)
                                Dim issuedOn As DateTime = If(Not IsDBNull(dr("issued_on")), DirectCast(dr("issued_on"), DateTime), DateTime.MaxValue)
                                Dim validTill As DateTime = If(Not IsDBNull(dr("valid_till")), DirectCast(dr("valid_till"), DateTime), DateTime.MinValue)
                                ' Update the highest valid_till date
                                If Not IsDBNull(dr("issued_on")) Then
                                    If validTill > highestValidTill Then
                                        highestValidTill = validTill
                                    End If
                                End If

                                ' Update the lowest issued_on date
                                If Not IsDBNull(dr("valid_till")) Then

                                    If issuedOn < lowestissuedOn Then
                                        lowestissuedOn = issuedOn
                                    End If
                                End If
                            End If
                        Next
                        If lowestissuedOn <> DateTime.MaxValue Then
                            LabelIssuedD.Text = lowestissuedOn.ToString()
                        Else
                            LabelIssuedD.Text = ""
                        End If
                        If highestValidTill <> DateTime.MinValue Then
                            LabelValidTill.Text = highestValidTill.ToString()
                        Else
                            LabelValidTill.Text = ""
                        End If
                    End If
                End Using
                connection.Close()

                If payClicked Then
                    Dim insertStatement As String = "INSERT INTO dl_entries (dl_id, uid, vehicle_type, fee_paid, req_type) 
                                                    VALUES (@Value1, @Value2, @Value3, @Value4, @Value5) 
                                                    ON DUPLICATE KEY UPDATE 
                                                    uid = VALUES(uid), 
                                                    fee_paid = VALUES(fee_paid), 
                                                    test_status = CASE 
                                                                     WHEN dl_entries.test_status = 'fail' THEN NULL
                                                                     ELSE dl_entries.test_status 
                                                                END,
                                                    issued_on = CASE 
                                                                   WHEN dl_entries.test_status IS NULL THEN NULL
                                                                   ELSE dl_entries.issued_on
                                                                END,
                                                    valid_till = CASE 
                                                                    WHEN dl_entries.test_status IS NULL THEN NULL
                                                                    ELSE dl_entries.valid_till
                                                                END; "
                    If VTypeCb.SelectedItem IsNot Nothing Then
                        'Dim isValidRow As Boolean = False
                        Dim vtype As String = VTypeCb.SelectedItem.ToString()
                        Dim vTypeId As Integer = TransportGlobals.GetVehicleTypeID(vtype)

                        Dim foundRowIndex As Integer = -1 ' Initialize with -1 to indicate not found
                        For Each row1 As DataRow In dataTable2.Rows
                            Dim columnValue As Integer = Convert.ToInt32(row1("vehicle_type"))
                            If columnValue = vTypeId Then
                                foundRowIndex = dataTable2.Rows.IndexOf(row1)
                                Exit For
                            End If
                        Next
                        Dim row As Integer = Convert.ToInt32(foundRowIndex)
                        Dim isValid As Boolean = True
                        Dim reqProceed As Boolean = False
                        If dataTable2.Rows.Count > 0 Then
                            If row <> -1 Then

                                Dim r As DataRow = dataTable2.Rows(row)
                                If Not IsDBNull(r("test_status")) Then
                                    Dim testStatus As String = r("test_status").ToString()

                                    ' Process the test status
                                    If testStatus = "pass" Then
                                        If highestValidTill < DateTime.Today Then
                                            Dim Result As DialogResult = MessageBox.Show("Your Driving License has Expired!!.Do you want to renew now?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
                                            If Result = DialogResult.OK Then
                                                Dim pay = New PaymentGateway() With {
                                                        .uid = uid,
                                                        .readonly_prop = True
                                                }
                                                pay.TextBox1.Text = 5
                                                pay.TextBox2.Text = 100
                                                pay.TextBox3.Text = $"Driving License Renewal Fee "
                                                If (pay.ShowDialog() = DialogResult.OK) Then
                                                    'MessageBox.Show("Payment Successful!")
                                                    renewal = True
                                                Else
                                                    MessageBox.Show("payment failed!")
                                                    Exit Sub
                                                End If
                                            Else
                                                isValid = False
                                            End If
                                        Else
                                            MessageBox.Show("You already have a driving license for the vehicle type " & vtype, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            isValid = False
                                        End If
                                    ElseIf testStatus = "fail" Then
                                        If r("req_type") = "fresh" Then
                                            newReq = True
                                        End If

                                        Dim result As DialogResult = MessageBox.Show("Your previous request for this vehicle type was rejected. Do you want to apply again?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                                        If result = DialogResult.OK Then
                                            Dim pay = New PaymentGateway() With {
                                                        .uid = uid,
                                                        .readonly_prop = True
                                                }
                                            pay.TextBox1.Text = 5
                                            pay.TextBox2.Text = 100
                                            pay.TextBox3.Text = $"Driving License Registration Fee for Vehicle Type : {vtype}"
                                            If (pay.ShowDialog() = DialogResult.OK) Then
                                                'MessageBox.Show("Payment Successful!")
                                                reqProceed = True
                                            Else
                                                MessageBox.Show("payment failed!")
                                                Exit Sub
                                            End If
                                        Else
                                            isValid = False
                                        End If
                                    Else
                                            MessageBox.Show("You Already placed a request for vehicle type :" & vtype, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        isValid = False
                                    End If
                                End If
                            Else
                                Dim pay = New PaymentGateway() With {
                                                   .uid = uid,
                                                   .readonly_prop = True
                                }
                                pay.TextBox1.Text = 5
                                pay.TextBox2.Text = 100
                                pay.TextBox3.Text = $"Driving License Registration Fee for Vehicle Type : {vtype}"
                                If (pay.ShowDialog() = DialogResult.OK) Then
                                    'MessageBox.Show("Payment Successful!")
                                    reqProceed = True
                                Else
                                    MessageBox.Show("payment failed!")
                                    Exit Sub
                                End If
                            End If
                        Else
                            Dim pay = New PaymentGateway() With {
                                                   .uid = uid,
                                                   .readonly_prop = True
                                }
                            pay.TextBox1.Text = 5
                            pay.TextBox2.Text = 100
                            pay.TextBox3.Text = $"Driving License Registration Fee for Vehicle Type : {vtype}"
                            If (pay.ShowDialog() = DialogResult.OK) Then
                                'MessageBox.Show("Payment Successful!")
                                reqProceed = True
                            Else
                                MessageBox.Show("payment failed!")
                                Exit Sub
                            End If
                        End If

                        If isValid AndAlso reqProceed Then
                            connection.Open()
                            Using command As New MySqlCommand(insertStatement, connection)
                                command.Parameters.AddWithValue("@Value2", uid)
                                command.Parameters.AddWithValue("@Value3", vTypeId)
                                command.Parameters.AddWithValue("@Value4", 1)
                                Dim row1 As Integer = Convert.ToInt32(foundRowIndex)
                                Dim isValid1 As Boolean = True
                                Dim reqProceed1 As Boolean = False
                                If dataTable2.Rows.Count > 0 Then
                                    command.Parameters.AddWithValue("@Value1", dataTable2.Rows(0)("dl_id"))
                                    If newReq Then
                                        command.Parameters.AddWithValue("@Value5", "fresh")
                                    Else
                                        command.Parameters.AddWithValue("@Value5", "renew")
                                    End If
                                Else
                                    command.Parameters.AddWithValue("@Value1", GenerateRandomId())
                                    command.Parameters.AddWithValue("@Value5", "fresh")
                                End If
                                command.ExecuteNonQuery()
                                Globals.ExecuteUpdateQuery("update admin_records set reg_dl_revenue = reg_dl_revenue + 100")
                                connection.Close()
                            End Using
                        End If

                        Dim renewStatement As String = "UPDATE dl_entries SET valid_till = @Value1 WHERE dl_id = @a"
                        If renewal Then
                            Dim id As Integer = dataTable2.Rows(0)("dl_id")
                            connection.Open()
                            Using command As New MySqlCommand(renewStatement, connection)
                                command.Parameters.AddWithValue("@Value1", DateTime.Today.AddYears(10))
                                command.Parameters.AddWithValue("@a", id)
                                command.ExecuteNonQuery()
                            End Using
                            Globals.ExecuteUpdateQuery("update admin_records set reg_dl_revenue = reg_dl_revenue + 100")
                            connection.Close()
                            renewal = False
                        End If
                    End If
                    payClicked = False
                End If

            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Shared RandomGenerator As New Random()

    Private Function GenerateRandomId() As String
        Dim id As String
        Dim idExists As Boolean

        Do
            ' Generate random five-digit number for the postfix
            id = RandomGenerator.Next(10000, 100000)
            ' Check if the generated ID exists in the database
            idExists = CheckIdExists(id)
        Loop While idExists

        Return id
    End Function

    Private Function CheckIdExists(id As String) As Boolean
        ' Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim command As New MySqlCommand("SELECT COUNT(*) FROM dl_entries WHERE dl_id = @id ", Con)

        Try
            Con.Open()
            command.Parameters.AddWithValue("@id", id)
            Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
            Return count > 0
        Catch ex As Exception
            MessageBox.Show("Error checking ID existence: " & ex.Message)
            Return True ' Assume ID exists to avoid inserting duplicates (you may handle this differently based on your application's requirements)
        Finally
            Con.Close()
        End Try
    End Function

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Panel7.Visible = False
        Panel4.Visible = False
        LoadAndBindData()

        ' Populate ComboBox with vehicle types until GetVehicleType returns non-null
        Dim vtypeid As Integer = 1
        Dim vehicleType As String = TransportGlobals.GetVehicleType(vtypeid)

        While vehicleType <> "Unknown"
            VTypeCb.Items.Add(vehicleType)
            vtypeid += 1
            vehicleType = TransportGlobals.GetVehicleType(vtypeid)
        End While
    End Sub

    Private Sub Paytb_Click(sender As Object, e As EventArgs) Handles Paytb.Click
        ' Check if the selected value in VTypeCb combo box is one of its items
        If Not VTypeCb.Items.Contains(VTypeCb.Text) Then
            MessageBox.Show("Invalid vehicle type selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub ' Exit the sub if the value is not valid
        End If
        payClicked = True
        LoadAndBindData()
        VTypeCb.SelectedIndex = -1
    End Sub

    Private Sub Canceltb_Click(sender As Object, e As EventArgs) Handles Canceltb.Click
        VTypeCb.SelectedIndex = -1
    End Sub

    Private Sub LabelDLID_Click(sender As Object, e As EventArgs) Handles LabelDLID.Click

    End Sub
End Class
