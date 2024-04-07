Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Relational

Public Class TransportDrivingLicenseReq
    Public Property uid As Integer
    Public Property u_name As String
    Private currentDlId As Integer
    Private payClicked As Boolean = False
    Private userDlId As Integer
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
                        Dim highestvalidTill As DateTime = If(Not IsDBNull(row("valid_till")), DirectCast(row("valid_till"), DateTime), DateTime.MinValue)
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
                                    If validTill > highestvalidTill Then
                                        highestvalidTill = validTill
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
                        If highestvalidTill <> DateTime.MinValue Then
                            LabelValidTill.Text = highestvalidTill.ToString()
                        Else
                            LabelValidTill.Text = ""
                        End If
                    End If
                End Using


                ' Load current driver's license ID
                Dim dlIdQuery As String = "SELECT MAX(dl_id) as max_dl_id FROM dl_entries "
                Using dlIdCmd As New MySqlCommand(dlIdQuery, connection)
                    Dim reader As MySqlDataReader = dlIdCmd.ExecuteReader()
                    If reader.Read() Then
                        currentDlId = If(Not IsDBNull(reader("max_dl_id")), Convert.ToInt32(reader("max_dl_id")), 0)
                    End If
                    reader.Close()
                End Using


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
                        For Each row As DataRow In dataTable2.Rows
                            Dim columnValue As Integer = Convert.ToInt32(row("vehicle_type"))
                            If columnValue = vTypeId Then
                                foundRowIndex = dataTable2.Rows.IndexOf(row)
                                Exit For
                            End If
                        Next


                        Using command As New MySqlCommand(insertStatement, connection)
                            command.Parameters.AddWithValue("@Value2", uid)
                            command.Parameters.AddWithValue("@Value3", vTypeId)
                            command.Parameters.AddWithValue("@Value4", 1)
                            Dim row As Integer = Convert.ToInt32(foundRowIndex)
                            Dim isValid As Boolean = True
                            If dataTable2.Rows.Count > 0 Then
                                command.Parameters.AddWithValue("@Value1", dataTable2.Rows(0)("dl_id"))
                                command.Parameters.AddWithValue("@Value5", "renew")
                                If row <> -1 Then

                                    Dim r As DataRow = dataTable2.Rows(row)
                                    If Not IsDBNull(r("test_status")) Then
                                        Dim testStatus As String = r("test_status").ToString()

                                        ' Process the test status
                                        If testStatus = "pass" Then
                                            MessageBox.Show("You already have a driving license for the vehicle type " & vtype, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        ElseIf testStatus = "fail" Then
                                            Dim result As DialogResult = MessageBox.Show("Your previous request for this vehicle type was rejected. Do you want to apply again?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                                            If result = DialogResult.OK Then
                                                MessageBox.Show("Payment request will be sent", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Else
                                                isValid = False
                                            End If
                                        End If
                                    Else
                                        MessageBox.Show("You Already placed a request for vehicle type :" & vtype, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    End If
                                Else
                                    MessageBox.Show("Payment request will be sent", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Else
                                currentDlId += 1
                                command.Parameters.AddWithValue("@Value1", currentDlId)
                                command.Parameters.AddWithValue("@Value5", "fresh")
                            End If
                            If isValid Then
                                command.ExecuteNonQuery()
                            End If
                        End Using
                    End If
                    payClicked = False
                End If
                connection.Close()
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


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
        payClicked = True
        LoadAndBindData()
    End Sub

    Private Sub Canceltb_Click(sender As Object, e As EventArgs) Handles Canceltb.Click
        VTypeCb.SelectedIndex = -1
    End Sub

    Private Sub LabelDLID_Click(sender As Object, e As EventArgs) Handles LabelDLID.Click

    End Sub
End Class
