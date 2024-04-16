Imports System.Data.SqlClient
Imports System.Globalization
Imports MySql.Data.MySqlClient
Public Class PayforLoan
    Public Property uid As Integer = 1
    Public Property accountNumber As Integer = 1
    Dim totalamount As Decimal = 3564216

    Private Sub PayforLoan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Display user details
        DisplayUserLoans(accountNumber)
        'UpdateLoanDetails()
        ' Check if the user is a defaulter
        CheckLoanDefault()
        CheckLastPaymentMonth()
    End Sub


    Private Sub DisplayUserLoans(accountNumber As Integer)
        Try
            Using connection As MySqlConnection = Globals.GetDBConnection()
                connection.Open()
                Dim query As String = "SELECT * FROM bankloans WHERE AccountNumber = @AccountNumber"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@AccountNumber", accountNumber)

                    Using reader As MySqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            ' Retrieve values from the database
                            Dim loanId As Integer = reader.GetInt32("LoanId")
                            'Dim startDate As Date = Convert.ToDateTime(reader.GetValue(5)) ' Convert to DateTime
                            Dim status As String = reader.GetString("Status")
                            Dim monthsLeft As Integer = reader.GetInt32("MonthsLeft")
                            Dim amountPaidAlready As Decimal = reader.GetDecimal("AmountPaidAlready")
                            'Dim lastPaidMonth As Date = Convert.ToDateTime(reader.GetValue("LastPaidMonth")) ' Convert to DateTi

                            If reader("StartDate") IsNot Nothing AndAlso Not IsDBNull(reader("StartDate")) Then
                                Dim dobString As String = reader("StartDate").ToString()
                                Try
                                    Dim dob As DateTime = DateTime.ParseExact(dobString, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                                    DateTimePicker1.Value = dob
                                Catch ex As Exception
                                    ' Handle parsing errors
                                    MessageBox.Show("Error parsing StartDate: " & ex.Message)
                                End Try
                            End If

                            If reader("LastPaidMonth") IsNot Nothing AndAlso Not IsDBNull(reader("LastPaidMonth")) Then
                                Dim dobString As String = reader("LastPaidMonth").ToString()
                                Try
                                    Dim dob As DateTime = DateTime.ParseExact(dobString, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                                    DateTimePicker2.Value = dob
                                Catch ex As Exception
                                    ' Handle parsing errors
                                    MessageBox.Show("Error parsing LastPaidMonth: " & ex.Message)
                                End Try
                            End If


                            ' Set values to labels
                            Label24.Text = loanId.ToString()
                            Label25.Text = accountNumber.ToString()
                            Label26.Text = uid.ToString()
                            'Label29.Text = startDate.ToString() ' Format date as needed
                            Label30.Text = status
                            Label31.Text = monthsLeft.ToString()
                            Label33.Text = "₹" & amountPaidAlready.ToString()
                            'Label35.Text = lastPaidMonth.ToString() ' Format date as needed

                        Else
                            ' No data found for the account number
                            MessageBox.Show("No loan data found for the account number.")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            'Console.WriteLine("An error occurred: " & ex.Message)
            MessageBox.Show("An error occurred while fetching loan data: " & ex.Message)
        End Try
    End Sub



    Private Function GetLastPaymentMonth(userID As Integer) As Integer

        Try
            Using connection As MySqlConnection = Globals.GetDBConnection()
                Dim query As String = "SELECT MONTH(LastPaidMonth) FROM bankloans WHERE UserID = @UserID"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@UserID", uid)
                    connection.Open()

                    ' Execute the SQL query and get the last payment month
                    Dim lastPaymentMonthObj As Object = command.ExecuteScalar()

                    If lastPaymentMonthObj IsNot Nothing AndAlso Not DBNull.Value.Equals(lastPaymentMonthObj) Then
                        ' If last payment month is found, return it
                        Return Convert.ToInt32(lastPaymentMonthObj)
                    Else
                        Return 0
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Handle any errors
            Console.WriteLine("An error occurred: " & ex.Message)
            Return 0
        End Try
    End Function

    Private Sub CheckLastPaymentMonth()
        ' Get the last payment months for each bill type from the database
        Dim lmp As Integer = GetLastPaymentMonth(uid)


        If lmp = DateTime.Now.Month Then
            Button2.Enabled = False
            Button2.Text = "Paid"
        Else
            Button2.Enabled = True
            Button2.Text = "Pay"
        End If
    End Sub
    'Private Sub PayBills_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '   CheckLastPaymentMonth()
    'End Sub

    Private Sub UpdateLoanStatus(userID As Integer, newStatus As String)


        Try
            Using connection As MySqlConnection = Globals.GetDBConnection()
                Dim query As String = "UPDATE bankloans SET Status = @NewStatus WHERE UserID = @UserID"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@NewStatus", newStatus)
                    command.Parameters.AddWithValue("@UserID", userID)
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while updating loan status: " & ex.Message)
        End Try
    End Sub


    Private Sub CheckLoanDefault()
        ' Check if the user is a defaulter (unable to pay the loan for a month)
        Dim lastPaymentMonth As Integer = GetLastPaymentMonth(uid)

        If Math.Abs(lastPaymentMonth - DateTime.Now.Month) >= 2 Then
            UpdateLoanStatus(uid, "Defaulter")
            ' The user is a defaulter
            MessageBox.Show("You are now a defaulter. Please make the payment to avoid further consequences.")

            ' Update loan status to "defaulters"
        Else
            ' Check if the user is a defaulter based on exceeding loan term and amount
            Dim monthsLeft As Integer = GetMonthsLeft(uid)
            Dim amountPaidAlready As Decimal = GetAmountPaidAlready(uid)

            If monthsLeft < 0 AndAlso amountPaidAlready > totalamount Then
                ' The user is a defaulter due to exceeding loan term and amount
                UpdateLoanStatus(uid, "Defaulter")
                MessageBox.Show("You are now a defaulter due to exceeding the loan term and total amount. Please make the payment to avoid further consequences.")

                ' Update loan status to "Defaulter"
            End If
        End If
    End Sub



    Private Sub UpdateLoanDetails(amount As Integer)
        'Dim emi As Decimal = 59404
        Try
            Dim query As String = "UPDATE bankloans SET LastPaidMonth = @LastPaidMonth, AmountPaidAlready = AmountPaidAlready + @AmountPaid, MonthsLeft = MonthsLeft - 1, Status = CASE WHEN MonthsLeft > 60 AND AmountPaidAlready >= @TotalAmount THEN 'Defaulters' ELSE 'Active' END WHERE UserID = @UserID"
            Using connection As MySqlConnection = Globals.GetDBConnection()
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@LastPaidMonth", DateTime.Now)
                    command.Parameters.AddWithValue("@AmountPaid", amount)
                    command.Parameters.AddWithValue("@UserID", uid)
                    command.Parameters.AddWithValue("@TotalAmount", totalamount)
                    connection.Open()
                    command.ExecuteNonQuery()

                    ' Refresh loan details after update
                    DisplayUserLoans(accountNumber)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while updating loan details: " & ex.Message)
        End Try
    End Sub


    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim lastPaymentMonth As Integer = GetLastPaymentMonth(uid)
        Dim time As Integer = 0
        time = Math.Abs(lastPaymentMonth - DateTime.Now.Month)
        Dim i As Integer = time
        Dim amount As Integer = i * 59404


        Dim pay = New PaymentGateway() With {
            .uid = uid,
            .readonly_prop = True
        }

        pay.TextBox1.Text = 10
        pay.TextBox2.Text = amount
        pay.TextBox3.Text = "Loan Monthly Bill Payment."
        If (pay.ShowDialog() = DialogResult.OK) Then
            MessageBox.Show("Payment successful!")
            MessageBox.Show("Please reload the current page.")
            ' Update loan details after successful payment
            UpdateLoanDetails(amount)
            UpdateLoanStatus(uid, "Active")
        Else
            MessageBox.Show("Payment failed.")
        End If
    End Sub

    Private Function GetMonthsLeft(userID As Integer) As Integer
        Dim monthsLeft As Integer = 0
        Try
            Dim query As String = "SELECT MonthsLeft FROM bankloans WHERE UserID = @UserID"
            Using connection As MySqlConnection = Globals.GetDBConnection()
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@UserID", userID)
                    connection.Open()
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        monthsLeft = Convert.ToInt32(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while retrieving months left: " & ex.Message)
        End Try
        Return monthsLeft
    End Function

    Private Function GetAmountPaidAlready(userID As Integer) As Decimal
        Dim amountPaidAlready As Decimal = 0
        Try
            Dim query As String = "SELECT AmountPaidAlready FROM bankloans WHERE UserID = @UserID"
            Using connection As MySqlConnection = Globals.GetDBConnection()
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@UserID", userID)
                    connection.Open()
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        amountPaidAlready = Convert.ToDecimal(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while retrieving amount paid already: " & ex.Message)
        End Try
        Return amountPaidAlready
    End Function


End Class