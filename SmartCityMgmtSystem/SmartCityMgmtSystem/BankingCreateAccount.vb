Imports MySql.Data.MySqlClient

Public Class BankingCreateAccount

    'Private lastAccountNumber As Integer = 10000000

    'Private Function GenerateNextAccountNumber() As Integer
    '   lastAccountNumber += 1
    'Return lastAccountNumber
    'End Function

    Private Function GenerateNextAccountNumber() As Integer
        ' Get connection from globals
        Dim Con As MySqlConnection = Globals.GetDBConnection()

        ' Initialize the next account number
        Dim nextAccountNumber As Integer = 0

        Try
            Con.Open()

            ' Create a MySqlCommand to get the maximum account number from the account table
            Dim cmd As MySqlCommand = Con.CreateCommand()
            cmd.CommandText = "SELECT MAX(account_number) FROM account"

            ' Execute the SQL query and fetch the maximum account number
            Dim result As Object = cmd.ExecuteScalar()

            ' Check if the result is not null and convert it to an integer
            If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                nextAccountNumber = Convert.ToInt32(result)
            End If

            ' Increment the account number by 1
            nextAccountNumber += 1
        Catch ex As Exception
            MessageBox.Show("Error fetching next account number: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close the connection
            Con.Close()
        End Try

        ' Return the next account number
        Return nextAccountNumber
    End Function


    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, TransportationAdminHome)
    End Sub

    Private Function HashPassword(password As String) As String

        Return password
    End Function

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        'button
        Dim userId As Integer
        If Not Integer.TryParse(TextBox1.Text, userId) Then
            MessageBox.Show("Please enter a valid User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim email As String = TextBox2.Text.Trim()
        Dim password As String = TextBox3.Text
        Dim confirmPassword As String = TextBox4.Text

        ' Validate password and confirm password
        If password <> confirmPassword Then
            MessageBox.Show("Passwords do not match. Please re-enter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox3.Focus()
            Return
        End If

        ' Generate a random account number
        Dim accountNumber As Integer = GenerateNextAccountNumber()

        ' Hash the password before storing it (you should implement this securely)
        Dim hashedPassword As String = HashPassword(password)

        ' Create the INSERT query for the account table
        Dim query As String = $"INSERT INTO account (user_id, account_number, password, balance) VALUES ({userId}, {accountNumber}, '{hashedPassword}',5000)"

        ' Execute the insert query using Globals function
        If Globals.ExecuteInsertQuery(query) Then
            MessageBox.Show("Account created successfully. Please click a picture of your account number to login beacuase this page will be redirected to login page. Account Number: " & accountNumber, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Else
            MessageBox.Show("Failed to create account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        Me.Close()
        Dim createAccountForm As New BankingLogin()
        createAccountForm.Show()
    End Sub

End Class
