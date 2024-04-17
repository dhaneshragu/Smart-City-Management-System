Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient

Public Class ApplyForLoan
    Public Property uid As Integer = 1
    Public Property accountNumber As Integer = 1
    'Dim totalamount As Decimal = 3564216
    Public Property valid As Integer = -1

    Private Function IsUserPresentInLoansTable(userID As Integer) As Boolean
        'Dim connectionString As String = "Your_Connection_String_Here"
        Try
            Using connection As MySqlConnection = Globals.GetDBConnection()
                connection.Open()

                Dim query As String = "SELECT COUNT(*) FROM bankloans WHERE UserID = @UserID"
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@UserID", userID)

                    Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                    Return count > 0
                End Using
            End Using
        Catch ex As Exception
            'MessageBox.Show("An error occurred while checking user in loans table: " & ex.Message)
            Return False
        End Try
    End Function

    Private Sub ApplyForLoan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Panel3.Hide()
        ' Check if the user is eligible for a loan
        If Not IsEligibleForLoan(uid) Then
            ' If not eligible, show message and disable Apply button
            'MessageBox.Show("You are not eligible to apply for a loan.")
            Panel1.Hide()
            Label5.Visible = True
            Button1.Visible = False
            Return ' Exit the method if not eligible
        Else
            Label5.Visible = False
        End If

    End Sub


    Private Function IsEligibleForLoan(userID As Integer) As Boolean
        'Dim connectionString As String = "Your_Connection_String_Here"
        Try
            Using connection As MySqlConnection = Globals.GetDBConnection()
                connection.Open()

                Dim query As String = "SELECT age, occupation FROM users WHERE user_id = @UserID"
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@UserID", userID)

                    Using reader As MySqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            Dim age As Integer = reader.GetInt32("age")
                            Dim occupation As String = reader.GetString("occupation")

                            ' Check eligibility conditions
                            If age >= 18 AndAlso (Not String.IsNullOrWhiteSpace(occupation) AndAlso Not occupation.Equals("Student")) Then
                                Return True ' Eligible for loan
                            End If
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while checking eligibility for the loan: " & ex.Message)
        End Try

        Return False ' Not eligible for loan
    End Function

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        ' Display confirmation message box
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to apply for a loan?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' Check if user clicked Yes
        If result = DialogResult.Yes Then
            ' Calculate monthly installment (EMI)
            Dim loanAmount As Decimal = 3000000 ' 30 lakhs
            Dim periodInMonths As Integer = 60
            Dim interestRate As Decimal = 0.07 ' 7%
            Dim emi As Decimal = 59404

            ' Get current date
            Dim startDate As Date = Date.Today
            Dim lastPaidMonth As Date = startDate.AddMonths(-1)
            Dim status As String = "Active" ' Loan status
            Dim userID As Integer = uid ' Get user ID from session or login
            Dim accNumber As Integer = accountNumber
            Dim monthsLeft As Integer = periodInMonths
            Dim amountPaidAlready As Decimal
            'Dim amountToBePaid As Decimal = totalamount - amountPaidAlready

            Dim rateOfInterest As Decimal = interestRate

            Try
                Using connection As MySqlConnection = Globals.GetDBConnection()
                    connection.Open()

                    Dim query As String = "INSERT INTO bankloans (AccountNumber, UserID, EMIPerMonth, PeriodInMonths, StartDate, Status, MonthsLeft, AmountPaidAlready, RateOfInterest, LastPaidMonth) VALUES (@AccountNumber, @UserID, @EMIPerMonth, @PeriodInMonths, @StartDate, @Status, @MonthsLeft, @AmountPaidAlready, @RateOfInterest, @LastPaidMonth)"
                    Using command As MySqlCommand = New MySqlCommand(query, connection)
                        command.Parameters.AddWithValue("@AccountNumber", accNumber)
                        command.Parameters.AddWithValue("@UserID", userID)
                        command.Parameters.AddWithValue("@EMIPerMonth", emi)
                        command.Parameters.AddWithValue("@PeriodInMonths", periodInMonths)
                        command.Parameters.AddWithValue("@StartDate", startDate)
                        command.Parameters.AddWithValue("@Status", status)
                        command.Parameters.AddWithValue("@MonthsLeft", monthsLeft)
                        command.Parameters.AddWithValue("@AmountPaidAlready", amountPaidAlready)
                        'command.Parameters.AddWithValue("@AmountToBePaid", amountToBePaid)
                        command.Parameters.AddWithValue("@RateOfInterest", rateOfInterest)
                        ' Calculate the last paid month (one month before the start date)
                        ' Insert the last paid month into the database
                        command.Parameters.AddWithValue("@LastPaidMonth", lastPaidMonth)


                        command.ExecuteNonQuery()

                    End Using
                End Using
                ' Get current form position and size
                MessageBox.Show("Please Reload The Current Page")


            Catch ex As Exception
                MessageBox.Show("An error occurred while applying for the loan: " & ex.Message)
            End Try
        End If
    End Sub

End Class
