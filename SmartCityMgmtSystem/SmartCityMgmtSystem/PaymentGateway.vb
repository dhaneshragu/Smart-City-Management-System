Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports Mysqlx

Public Class PaymentGateway

    Public Property uid As Integer
    'Set this true if you want to make the fields readonly (for automatic payments)
    Public Property readonly_prop As Boolean = False

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox4.PasswordChar = "*"
        TextBox1.ReadOnly = readonly_prop
        TextBox2.ReadOnly = readonly_prop
        TextBox3.ReadOnly = readonly_prop
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim paymentSuccessful As Boolean = False
        paymentSuccessful = ProcessPayment()
        ' Set DialogResult based on payment success
        If paymentSuccessful Then
            Me.DialogResult = DialogResult.OK
        Else
            Me.DialogResult = DialogResult.Cancel
        End If
        'MessageBox.Show(paymentSuccessful)

        Me.Close()
    End Sub
    Private Function ProcessPayment() As Boolean
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

        cmd = New MySqlCommand("SELECT account_number, password, balance FROM account WHERE user_id = " & uid, Con)
        reader = cmd.ExecuteReader
        If reader.Read() Then
            acc_from = reader("account_number")
            pass = reader("password")
            from_bal = reader("balance")
        End If
        reader.Close()

        If Not Integer.TryParse(TextBox1.Text, receiver) Then
            ' Conversion failed, show an error message
            MessageBox.Show("Invalid integer format. Please enter a valid UID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        ElseIf Not Integer.TryParse(TextBox2.Text, amt) Or amt < 0 Then
            ' Conversion failed, show an error message
            MessageBox.Show("Invalid integer format. Please enter a valid amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        ElseIf String.IsNullOrWhiteSpace(TextBox4.Text) Then
            MessageBox.Show("Please enter your password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf TextBox4.Text <> pass Then
            MessageBox.Show("Incorrect password!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf from_bal < amt Then
            MessageBox.Show("Insufficient balance!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            cmd = New MySqlCommand("SELECT account_number FROM account WHERE user_id = " & receiver, Con)
            reader = cmd.ExecuteReader
            If reader.Read() Then
                acc_to = reader("account_number")
                reader.Close()

                Dim q As String = "INSERT INTO transactions (sender_account, receiver_account, time, amount, message) 
                        VALUES (@sa, @ra, @time, @amt, @msg)"
                Dim bal_from As String = "UPDATE account SET balance = balance - " & amt & " WHERE user_id = " & uid
                Dim bal_to As String = "UPDATE account SET balance = balance + " & amt & " WHERE user_id = " & receiver
                'Dim conStr As String = Globals.getdbConnectionString()
                'Using connection As New MySqlConnection(conStr)
                Using sqlCommand As New MySqlCommand(q, Con)
                    sqlCommand.Parameters.AddWithValue("@sa", acc_from)
                    sqlCommand.Parameters.AddWithValue("@ra", acc_to)
                    sqlCommand.Parameters.AddWithValue("@time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    sqlCommand.Parameters.AddWithValue("@amt", amt)
                    sqlCommand.Parameters.AddWithValue("@msg", TextBox3.Text)
                    Try
                        Dim success As Boolean = Globals.ExecuteUpdateQuery(bal_from)
                        Globals.ExecuteUpdateQuery(bal_to)
                        sqlCommand.ExecuteNonQuery()
                        MessageBox.Show("Transaction successful.", "Success", MessageBoxButtons.OK)
                        ans = True
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message)
                    End Try
                End Using
            Else
                MessageBox.Show("Reciever does not have a bank account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Me.Close()
        End If
        Return ans

    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        'Get the name of the user with the given UID and display it in Label6
        Dim uid As Integer
        If Integer.TryParse(TextBox1.Text, uid) Then
            Dim query As String = "SELECT name FROM users WHERE user_id = @uid"
            Using connection As New MySqlConnection(Globals.getdbConnectionString())
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@uid", uid)
                    Try
                        connection.Open()
                        Dim reader As MySqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            Label6.Text = "Name: " & reader.GetString("name")
                        Else
                            Label6.Text = "Name: Not Found"
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error getting user name: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            End Using
        End If
    End Sub
End Class