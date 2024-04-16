Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class AddOrWithdraw
    Public Property accn As Integer = 1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim amountToAdd As Decimal = TextBox1.Text.Trim()
        Dim note As String = TextBox3.Text.Trim()

        If Not Decimal.TryParse(TextBox1.Text, amountToAdd) Then
            MessageBox.Show("Invalid amount.")
            Return
        End If

        If amountToAdd < 0 Then
            MessageBox.Show("Amount cannot be negative.")
            Return
        End If

        If String.IsNullOrWhiteSpace(note) Then
            note = "Credited"
        End If

        ' Update balance by adding the amount
        UpdateBalance(amountToAdd, note)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim amountToWithdraw As Decimal = TextBox6.Text.Trim()
        Dim note As String = TextBox4.Text.Trim()

        If Not Decimal.TryParse(TextBox6.Text, amountToWithdraw) Then
            MessageBox.Show("Invalid amount.")
            Return
        End If

        If amountToWithdraw < 0 Then
            MessageBox.Show("Amount cannot be negative.")
            Return
        End If

        If String.IsNullOrWhiteSpace(note) Then
            note = "Debited"
        End If

        ' Check if there are sufficient funds to withdraw
        Dim currentBalance As Decimal = GetCurrentBalance(accn)
        If currentBalance >= amountToWithdraw Then
            ' Update balance by withdrawing the amount
            UpdateBalance(-amountToWithdraw, note)
        Else
            MessageBox.Show("Insufficient funds.")
        End If
    End Sub

    Private Sub UpdateBalance(amount As Decimal, note As String)
        Try
            ' Connect to the database
            Using connection As MySqlConnection = Globals.GetDBConnection()
                connection.Open()

                ' Begin a database transaction
                Dim transaction As MySqlTransaction = connection.BeginTransaction()

                Try
                    ' Update balance
                    UpdateBalance(connection, transaction, accn, amount)

                    ' Insert transaction record
                    InsertTransaction(connection, transaction, accn, accn, (-1) * amount, note)

                    ' Commit the transaction
                    transaction.Commit()

                    MessageBox.Show("Transaction successful.")
                Catch ex As Exception
                    ' Rollback the transaction if any error occurs
                    transaction.Rollback()
                    MessageBox.Show($"Error performing transaction: {ex.Message}")
                End Try
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try
    End Sub

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

    Private Sub InsertTransaction(connection As MySqlConnection, transaction As MySqlTransaction, senderAccountNumber As Integer, receiverAccountNumber As Integer, amount As Decimal, note As String)
        ' Insert a record into the transactions table
        Dim query As String = "INSERT INTO transactions (sender_account, receiver_account, amount, message, time) VALUES (@SenderAccountNumber, @ReceiverAccountNumber, @Amount, @Note, NOW())"
        Try
            Using command As MySqlCommand = New MySqlCommand(query, connection, transaction)
                command.Parameters.AddWithValue("@SenderAccountNumber", accn)
                command.Parameters.AddWithValue("@ReceiverAccountNumber", accn)
                command.Parameters.AddWithValue("@Amount", amount)
                command.Parameters.AddWithValue("@Note", note)
                command.ExecuteNonQuery()
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
End Class
