Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports System.IO

Public Class UserToUserTransaction
    Public Property accn As Integer = 1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim amount As Decimal = TextBox1.Text.Trim()
        Dim receiverAccountNumber As Integer = TextBox2.Text.Trim()
        Dim note As String = TextBox3.Text.Trim()

        If Not Decimal.TryParse(TextBox1.Text, amount) Then
            MessageBox.Show("Invalid amount.")
            Return
        End If

        If amount < 0 Then
            MessageBox.Show("Amount cannot be negative.")
            Return
        End If

        If Not Integer.TryParse(TextBox2.Text, receiverAccountNumber) Then
            MessageBox.Show("Invalid receiver account number.")
            Return
        End If

        note = TextBox3.Text

        Try
            Using connection As MySqlConnection = Globals.GetDBConnection()
                Dim query As String = "SELECT COUNT(*) FROM account WHERE account_number = @AccountNumber"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@AccountNumber", receiverAccountNumber)
                    connection.Open()
                    Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())

                    If count > 0 Then
                        If PerformTransfer(amount, receiverAccountNumber, note) Then
                            MessageBox.Show("Transfer successful.")
                        Else
                            MessageBox.Show($"Transfer failed. Insufficient balance for account number: {receiverAccountNumber}.")
                        End If
                    Else
                        MessageBox.Show($"Receiver account number {receiverAccountNumber} not found.")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try
    End Sub

    Private Function GetCurrentBalance(accountNumber As Integer) As Decimal
        ' Fetch the current balance from the database based on the account number
        Dim balance As Decimal

        Try
            Using connection As MySqlConnection = Globals.GetDBConnection()
                Dim query As String = "SELECT balance FROM account WHERE account_number = @AccountNumber"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@AccountNumber", accn)
                    connection.Open()
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        balance = Convert.ToDecimal(result)
                    Else
                        ' Handle the case where the account number doesn't exist
                        Throw New Exception("Account number not found.")
                    End If
                End Using
            End Using
        Catch ex As Exception '{ex.Message}
            MessageBox.Show($"Error: {ex.Message}")
        End Try

        Return balance
    End Function




    Private Sub UpdateBalance(connection As MySqlConnection, transaction As MySqlTransaction, accountNumber As Integer, amount As Decimal)
        ' Update the balance in the database
        Dim query As String = "UPDATE account SET balance = balance + @Amount WHERE account_number = @AccountNumber"
        Try
            Using command As MySqlCommand = New MySqlCommand(query, connection, transaction)
                command.Parameters.AddWithValue("@Amount", amount)
                command.Parameters.AddWithValue("@AccountNumber", accountNumber)
                command.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try
    End Sub

    Private Function PerformTransfer(amount As Decimal, receiverAccountNumber As Integer, note As String) As Boolean
        'Dim senderAccountNumber As Integer = 1 ' Assuming the sender's account number is stored somewhere in your application
        Dim senderBalance As Decimal = GetCurrentBalance(accn)

        If senderBalance >= amount Then
            ' Connect to the database
            Using connection As MySqlConnection = Globals.GetDBConnection()
                connection.Open()

                ' Begin a database transaction
                Dim transaction As MySqlTransaction = connection.BeginTransaction()

                Try
                    ' Update sender's balance
                    UpdateBalance(connection, transaction, accn, -amount)

                    ' Update receiver's balance
                    UpdateBalance(connection, transaction, receiverAccountNumber, amount)

                    ' Insert transaction record
                    InsertTransaction(connection, transaction, accn, receiverAccountNumber, amount, note)

                    ' Commit the transaction
                    transaction.Commit()

                    Return True
                Catch ex As Exception
                    ' Rollback the transaction if any error occurs
                    transaction.Rollback()
                    MessageBox.Show($"Error performing transfer: {ex.Message}")
                    Return False
                End Try
            End Using
        Else
            Return False
        End If
    End Function

    Private Sub InsertTransaction(connection As MySqlConnection, transaction As MySqlTransaction, senderAccountNumber As Integer, receiverAccountNumber As Integer, amount As Decimal, note As String)
        ' Insert a record into the transactions table
        Dim query As String = "INSERT INTO transactions (sender_account, receiver_account, amount, message, time) VALUES (@SenderAccountNumber, @ReceiverAccountNumber, @Amount, @Note, NOW())"
        Try
            Using command As MySqlCommand = New MySqlCommand(query, connection, transaction)
                command.Parameters.AddWithValue("@SenderAccountNumber", accn)
                command.Parameters.AddWithValue("@ReceiverAccountNumber", receiverAccountNumber)
                command.Parameters.AddWithValue("@Amount", amount)
                command.Parameters.AddWithValue("@Note", note)
                command.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try
    End Sub

End Class
