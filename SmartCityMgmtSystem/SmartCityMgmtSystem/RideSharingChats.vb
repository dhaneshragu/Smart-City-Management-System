Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO
Imports System.Runtime.Remoting.Channels
Imports System.Threading
Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Common
Imports Org.BouncyCastle.Ocsp
Public Class RideSharingChats
    'To pass information from the parent form in form of property
    Public Property uid As Integer = 1
    Public Property capacity As Integer = 1
    Public Property u_name As String = "Dhanesh"
    Public Property poster_uid As Integer = 1     'UID of the poster of this post
    Public Property req_id As Integer = 1
    Public DriverNote As String = ""
    Public VehicleNumber As String = ""


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' For checkbox column - only for poster - allow editing
        If poster_uid = uid Then
            If e.RowIndex >= 0 AndAlso e.ColumnIndex = DataGridView1.Columns("Approve").Index Then
                ' Check if the clicked cell is in the "Approve" column
                Dim cell As DataGridViewCheckBoxCell = TryCast(DataGridView1.Rows(e.RowIndex).Cells("Approve"), DataGridViewCheckBoxCell)

                If cell IsNot Nothing Then
                    ' Manually toggle the checkbox value
                    If cell.Value = "added" Then
                        cell.Value = "not-added"
                        capacity += 1
                        Dim query As String = "UPDATE ride_sharing_entries SET capacity = capacity + 1 where req_id = " & req_id & ";"
                        If Globals.ExecuteInsertQuery(query) Then
                            query = "UPDATE ridesharing_chats_users SET status = 'not-added' WHERE req_id = " & req_id & " AND uid = " & Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("Id").Value) & ";"
                            If Globals.ExecuteInsertQuery(query) Then
                                Globals.SendNotifications(4, Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("Id").Value), "Ride Sharing Request Rejected", "Your request to join " & u_name & "'s Ride Sharing has been rejected by them. You will be receiving the refund for your payment soon.")
                                LoadandBindDataGridView()
                            End If
                        End If

                    Else
                        capacity -= 1
                        If (capacity < 0) Then
                            MessageBox.Show("All seats are filled. Remove some other person to add this person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            capacity += 1
                        Else
                            If CheckIfFeePaid(Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("Id").Value), req_id) Then
                                cell.Value = "added"
                                Dim query As String = "UPDATE ride_sharing_entries SET capacity = capacity - 1 WHERE req_id = " & req_id & ";"
                                If Globals.ExecuteInsertQuery(query) Then
                                    query = "UPDATE ridesharing_chats_users SET status = 'added' WHERE req_id = " & req_id & " AND uid = " & Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("Id").Value) & ";"
                                    If Globals.ExecuteInsertQuery(query) Then
                                        Globals.SendNotifications(4, Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("Id").Value), "Ride Sharing Request Accepted", "Your request to join " & u_name & "'s Ride Sharing has been approved by them. Happy Car Pooling!")
                                        LoadandBindDataGridView()
                                    End If
                                End If
                            Else
                                MessageBox.Show("This user hasn't paid fee yet, Can't Accept", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                capacity += 1
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub AddChat(name As String,
                        postNum As Integer,
                           Optional datetime As String = "",
                          Optional comment As String = "",
                          Optional image As Image = Nothing,
                          Optional isRider As Boolean = True)
        Dim ChatBubble As ChatBubbleItem
        ChatBubble = New ChatBubbleItem()
        With ChatBubble
            .Name = "chat_" & postNum
            If Not isRider Then
                .Margin = New Padding(PostsPanel.Width - ChatBubble.Width - 25, 12, 0, 3)
            End If
        End With
        ChatBubble.SetDetails(name, datetime, comment, image, isRider)
        PostsPanel.Controls.Add(ChatBubble)
    End Sub


    Private Sub LoadChats()

        'Clear all chats
        PostsPanel.Controls.Clear()
        Dim query As String = "SELECT rs.post_id, rs.uid, users.name, rs.time_stamp, rs.msg
                        FROM ride_sharing_chats rs
                        JOIN users ON users.user_id = rs.uid
                        WHERE rs.req_id = " & req_id.ToString() & ";"
        Using connection As New MySqlConnection(Globals.getdbConnectionString())
            Using command As New MySqlCommand(query, connection)
                connection.Open()

                ' Execute the command
                Using reader As MySqlDataReader = command.ExecuteReader()
                    ' Read each row
                    While reader.Read()
                        ' Access columns by name or index
                        Dim postuid As Integer = Convert.ToInt32(reader("uid"))
                        Dim role As Boolean = If(uid = postuid, False, True)
                        Dim userName As String = reader("name").ToString()
                        Dim timeStamp As String = Convert.ToDateTime(reader("time_stamp")).ToString("dd MMM yy, hh:mm tt") 'Like 12 March 24,08:45PM
                        Dim message As String = reader("msg").ToString()
                        Dim postNum As Integer = Convert.ToInt32(reader("post_id"))
                        Dim profilePhoto As Image = Globals.GetPicture("SELECT profile_photo FROM users WHERE user_id = " & postuid, "profile_photo")
                        'Add Chat
                        AddChat(userName, postNum, timeStamp, message, profilePhoto, role) 'role = 0 for sender, 1 for receiver
                    End While

                End Using
            End Using
        End Using
    End Sub



    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        ' Check if the current cell belongs to the "fee_paid" column and the value is 0
        If e.ColumnIndex = DataGridView1.Columns("Column2").Index AndAlso e.Value IsNot Nothing AndAlso e.Value.ToString() = "True" Then
            ' Set the cell value to "Paid"
            e.Value = "Yes"
        ElseIf e.ColumnIndex = DataGridView1.Columns("Column2").Index AndAlso e.Value IsNot Nothing AndAlso e.Value.ToString() <> "True" Then
            ' Set the cell value to "Not Paid"
            e.Value = "No"
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
        Dim query As String = "SELECT rcu.uid, users.name, rcu.fee_paid, rcu.status, rcu.role " &
                      "FROM ridesharing_chats_users AS rcu " &
                      "JOIN users ON users.user_id = rcu.uid " &
                      "WHERE rcu.req_id = " & req_id & " AND rcu.role = 1;"
        cmd = New MySqlCommand(query, Con)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()



        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "name"
        DataGridView1.Columns(1).DataPropertyName = "fee_paid"
        DataGridView1.Columns(2).DataPropertyName = "status"
        DataGridView1.Columns(3).DataPropertyName = "uid"
        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
        ' Loop through the rows to set Tag to uid from SQL command
        Con.Close()
    End Sub
    'Checks if the UID already exists in the ride_sharing_users table
    Public Shared Function CheckIfUidExists(uid As Integer, req_id As Integer) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM ridesharing_chats_users WHERE uid = @Uid AND req_id = @Req ;"
        Try
            Using connection As New MySqlConnection(Globals.getdbConnectionString())
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Uid", uid)
                    command.Parameters.AddWithValue("@Req", req_id)
                    connection.Open()
                    Dim result As Integer = Convert.ToInt32(command.ExecuteScalar())
                    Return result > 0
                End Using
            End Using
        Catch ex As Exception
            ' Display an error message
            MessageBox.Show("An error occurred while checking if UID exists: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Return True assuming UID does not exist due to error
            Return True
        End Try
    End Function
    'Check if fee is paid
    Public Shared Function CheckIfFeePaid(uid As Integer, req_id As Integer) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM ridesharing_chats_users WHERE req_id = @Reqq AND uid = @Uid AND fee_paid = 1;"
        Try
            Using connection As New MySqlConnection(Globals.getdbConnectionString())
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Uid", uid)
                    command.Parameters.AddWithValue("@Reqq", req_id)
                    connection.Open()

                    Dim result As Integer = Convert.ToInt32(command.ExecuteScalar())
                    Return result > 0
                End Using
            End Using
        Catch ex As Exception
            ' Display an error message
            MessageBox.Show("An error occurred while checking if UID paid fees: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Return True assuming UID does not exist due to error
            Return True
        End Try
    End Function
    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Set the Button Text based on fee paid
        If CheckIfFeePaid(uid, req_id) Then
            Button1.Text = "Withdraw"
        Else
            Button1.Text = "Pay"
        End If

        'Get the vehicle Details, TODO: Vehicle Picture Fetching
        Dim query As String = "SELECT vehicle_type, vehicle_pic FROM vehicle_reg WHERE vehicle_id = @VehicleNumber;"
        Using connection As New MySqlConnection(Globals.getdbConnectionString())
            Using command As New MySqlCommand(query, connection)
                ' Use parameterized query to prevent SQL injection
                command.Parameters.AddWithValue("@VehicleNumber", VehicleNumber)

                Try
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Dim vehicleType As Integer = Convert.ToInt32(reader("vehicle_type"))
                        Label4.Text = TransportGlobals.GetVehicleType(vehicleType)
                        If Not reader.IsDBNull(reader.GetOrdinal("vehicle_pic")) Then
                            Dim photoData As Byte() = DirectCast(reader("vehicle_pic"), Byte())
                            Using ms As New MemoryStream(photoData)
                                picbox.Image = Image.FromStream(ms)
                            End Using
                        End If
                    Else
                        ' Handle case where no record is found for the given vehicle ID
                        MessageBox.Show("Vehicle information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    reader.Close() ' Close the reader after fetching data
                Catch ex As Exception
                    MessageBox.Show("Error fetching vehicle details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using

        'For requester, show the approve button
        If poster_uid <> uid Then
            Button3.Visible = False
            Button1.Visible = True
            ' For the poster of the post
        Else
            Button3.Visible = True
            Button1.Visible = False
        End If

        'Load the more details 
        Label2.Text = VehicleNumber
        RichTextBox2.Text = DriverNote

        'Load the Ride List Datagridview
        LoadandBindDataGridView()


        ' Load Chats
        LoadChats()

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Set the dialog box to be always on top
        Dim dialogBox As New Transport_EnterFee With {
            .TopMost = True
        }

        ' Show the dialog box
        Dim result As DialogResult = dialogBox.ShowDialog()

        ' Process the result if needed
        If result = DialogResult.OK Then

            Dim query As String = "UPDATE ride_sharing_entries SET fare_per_person =" & dialogBox.NumericUpDown1.Value & " WHERE req_id=" & req_id & ";"
            If Globals.ExecuteUpdateQuery(query) Then
                MsgBox("Fare per person Is updated to ₹" & dialogBox.NumericUpDown1.Value)
            End If
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        If RichTextBox1.Text.Trim() <> "" Then
            Dim comment As String = RichTextBox1.Text
            RichTextBox1.Clear()
            Dim role As Boolean = (poster_uid = uid)
            Dim currentTimestamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            Dim query As String = "INSERT INTO ride_sharing_chats (req_id,uid,msg,time_stamp) VALUES (" & req_id & "," & uid & ",'" & comment & "','" & currentTimestamp & "');"
            If Globals.ExecuteInsertQuery(query) Then
                LoadChats()
                If Not CheckIfUidExists(uid, req_id) Then
                    'Insert only if it doesn't exist already
                    query = "INSERT INTO ridesharing_chats_users (req_id,uid,role) VALUES (" & req_id & "," & uid & "," & If(role, 0, 1) & ");"
                    If Globals.ExecuteInsertQuery(query) Then
                        LoadandBindDataGridView()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        LoadChats()
        LoadandBindDataGridView()
    End Sub
    Private Function getFare() As Integer
        'Get the fare per person
        Dim query As String = "SELECT fare_per_person FROM ride_sharing_entries WHERE req_id = " & req_id & ";"
        Dim fare As Integer = 0
        Using connection As New MySqlConnection(Globals.getdbConnectionString())
            Using command As New MySqlCommand(query, connection)
                Try
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        fare = Convert.ToInt32(reader("fare_per_person"))
                    End If
                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("Error getting fare: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    connection.Close()
                End Try
            End Using
        End Using

    End Function

    Private Function ProcessPaymentFromMinistry() As Boolean
        Dim ans As Boolean = False
        Dim acc_from, acc_to, receiver, amt, from_bal As Integer
        Dim pass As String = ""
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand
        Try
            Con.Open()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT account_number, password, balance FROM account WHERE user_id = 5", Con)
        reader = cmd.ExecuteReader
        If reader.Read() Then
            acc_from = reader("account_number")
            pass = reader("password")
            from_bal = reader("balance")
        End If
        reader.Close()

        receiver = uid
        amt = getFare()
        If from_bal < amt Then
            MessageBox.Show("Insufficient balance in transport department, Can't process your refund.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            cmd = New MySqlCommand("SELECT account_number FROM account WHERE user_id = " & receiver, Con)
            reader = cmd.ExecuteReader
            If reader.Read() Then
                acc_to = reader("account_number")
                reader.Close()

                Dim q As String = "INSERT INTO transactions (sender_account, receiver_account, time, amount, message) 
                        VALUES (@sa, @ra, @time, @amt, @msg)"
                Dim bal_from As String = "UPDATE account SET balance = balance - " & amt & " WHERE user_id = " & 5
                Dim bal_to As String = "UPDATE account SET balance = balance + " & amt & " WHERE user_id = " & receiver
                'Dim conStr As String = Globals.getdbConnectionString()
                'Using connection As New MySqlConnection(conStr)
                Using sqlCommand As New MySqlCommand(q, Con)
                    sqlCommand.Parameters.AddWithValue("@sa", acc_from)
                    sqlCommand.Parameters.AddWithValue("@ra", acc_to)
                    sqlCommand.Parameters.AddWithValue("@time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    sqlCommand.Parameters.AddWithValue("@amt", amt)
                    sqlCommand.Parameters.AddWithValue("@msg", "Refund for Ride Sharing Payment")
                    Try
                        Dim success As Boolean = Globals.ExecuteUpdateQuery(bal_from)
                        Globals.ExecuteUpdateQuery(bal_to)
                        sqlCommand.ExecuteNonQuery()
                        ans = True
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message)
                    End Try
                End Using
            Else
                MessageBox.Show("Reciever does not have a bank account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
        Return ans

    End Function
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        'Pay or withdraw fees
        If Button1.Text = "Pay" Then
            'Call the payment gateway
            Dim paymentGateway As New PaymentGateway() With
            {
            .uid = uid,
            .readonly_prop = True,
            .TopMost = True
            }

            'Pay to the UID of the driver
            paymentGateway.TextBox1.Text = poster_uid
            paymentGateway.TextBox2.Text = getFare() 'Amount to pay
            paymentGateway.TextBox3.Text = "Ride Sharing Amount payment"
            If paymentGateway.ShowDialog() = DialogResult.OK Then
                Dim query As String = "UPDATE ridesharing_chats_users SET fee_paid = 1 WHERE req_id = " & req_id & " AND uid = " & uid & ";"
                If Globals.ExecuteUpdateQuery(query) Then
                    MsgBox("Fee Paid Successfully")
                    LoadandBindDataGridView()
                    Button1.Text = "Withdraw"
                End If
            End If

        Else 'Automatically remove accept status also
            'Transfer money from transport minister to user with amount as the fee paid
            If ProcessPaymentFromMinistry() Then
                Dim query As String = "UPDATE ridesharing_chats_users SET fee_paid = 0,status='not-added' WHERE req_id = " & req_id & " AND uid = " & uid & ";"
                If Globals.ExecuteUpdateQuery(query) Then
                    MsgBox("Fee Payment Withdrawn")
                    LoadandBindDataGridView()
                    Button1.Text = "Pay"
                    'Send notifications that you have received a refund with details about RideSharing Post
                    Globals.SendNotifications(4, uid, "Ride Sharing Fee Refund", "You have received a refund for the Ride Sharing Request Number " & req_id & ".")
                End If
            End If
        End If
    End Sub
End Class