Imports MySql.Data.MySqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class BankingDashboard
    Public Property uid As Integer = 1
    Public Property u_name As String = "Me"
    Public Property balance As Integer = 1
    Public Property accountNumber As Integer = 1

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label3.Text = uid
        If uid <> 10 Then
            Button10.Visible = False
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, TransportationAdminHome)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ' Create an instance of UserBankDetails form
            Dim userBankDetailsForm As New UserBankDetails()

            ' Set properties of UserBankDetails form
            userBankDetailsForm.uname = u_name
            userBankDetailsForm.accNumber = accountNumber

            ' Display UserBankDetails form
            Globals.viewChildForm(childformPanel, userBankDetailsForm)
            'userBankDetailsForm.Show()
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Using connection As MySqlConnection = Globals.GetDBConnection()
                Dim query As String = "SELECT balance FROM account WHERE account_number = @AccountNumber"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@AccountNumber", accountNumber)
                    connection.Open()
                    ' Execute the query to retrieve the balance
                    Dim result As Object = command.ExecuteScalar()
                    ' Check if result is not null
                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        ' Convert the result to the appropriate type (assuming balance is of type Integer)
                        balance = Convert.ToInt32(result)
                    Else
                        ' Handle the case where the balance is null or DBNull
                        MessageBox.Show("Balance not found for the given account number.")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try
        Try
            Dim userBankDetailsForm As New CheckBankBalance()
            userBankDetailsForm.balance = balance
            userBankDetailsForm.accnum = accountNumber

            Globals.viewChildForm(childformPanel, userBankDetailsForm)
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            ' Create an instance of ViewBankTransactions form
            Dim viewTransactionsForm As New ViewBankTransactions()

            ' Set the accn property to the current account number
            viewTransactionsForm.accn = accountNumber ' Assuming accountNumber is the property storing the current account number

            ' Display ViewBankTransactions form
            Globals.viewChildForm(childformPanel, viewTransactionsForm)
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'Globals.viewChildForm(childformPanel, ViewBankTransactions)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Globals.viewChildForm(childformPanel, PayBills)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        Globals.viewChildForm(childformPanel, RechargeFastTag)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
        Dim createAccountForm = New HomePageDashboard With
            {
                .uid = uid
            }
        createAccountForm.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        'Globals.viewChildForm(childformPanel, UserToUserTransaction)
        Try
            Dim viewTransactionsForm As New UserToUserTransaction()

            viewTransactionsForm.accn = accountNumber

            Globals.viewChildForm(childformPanel, viewTransactionsForm)
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            Dim viewTransactionsForm As New AddOrWithdraw()

            viewTransactionsForm.accn = accountNumber

            Globals.viewChildForm(childformPanel, viewTransactionsForm)
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim adminTransactions As New BankAdminTransactions()
        Globals.viewChildForm(childformPanel, adminTransactions)
    End Sub
End Class
