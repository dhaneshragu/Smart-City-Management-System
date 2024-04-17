Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ListView
Imports MySql.Data.MySqlClient

Public Class BankingLogin
    Public Property uid As Integer = -1
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox3.PasswordChar = "*"
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, TransportationAdminHome)
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Close()
        Dim createAccountForm As New BankingCreateAccount()
        createAccountForm.Show()
    End Sub

    Dim userid As String
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        userid = TextBox1.Text.Trim()
        Dim accountNumber As String = TextBox2.Text.Trim()
        Dim password As String = TextBox3.Text.Trim()

        If String.IsNullOrEmpty(userid) OrElse String.IsNullOrEmpty(accountNumber) OrElse String.IsNullOrEmpty(password) Then
            MessageBox.Show("Please enter username, account number, and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Dim query As String = "SELECT u.name, a.balance, a.account_number FROM account a INNER JOIN users u ON a.user_id = u.user_id WHERE a.user_id = @UserId AND a.account_number = @AccountNumber AND a.password = @Password"
            Using conn As MySqlConnection = Globals.GetDBConnection()
                conn.Open()
                Using command As New MySqlCommand(query, conn)
                    command.Parameters.AddWithValue("@UserId", userid)
                    command.Parameters.AddWithValue("@AccountNumber", accountNumber)
                    command.Parameters.AddWithValue("@Password", password)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            Dim userName As String = reader.GetString("name")
                            Dim bankBalance As String = reader.GetInt32("balance")
                            Dim accno As String = reader.GetInt32("account_number")
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ' Navigate to the dashboard and pass user information
                            Dim dashboardForm = New BankingDashboard With
                                {
                                    .uid = Convert.ToInt32(userid),
                                    .balance = bankBalance,
                                    .u_name = userName,
                                    .accountNumber = accno
                                }
                            dashboardForm.Show()
                            Me.Close()
                        Else
                            MessageBox.Show("Invalid username, account number, or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            TextBox3.Clear()
                            TextBox3.Focus()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
        Dim createAccountForm = New HomePageDashboard With
            {
                .uid = uid
            }
        createAccountForm.Show()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim query As String = "SELECT email FROM users WHERE user_id = @UserId"
        Dim emailid As String = ""

        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Please enter your user ID.")
        Else
            Dim userid As String = TextBox1.Text.Trim()

            Using conn As MySqlConnection = Globals.GetDBConnection()
                conn.Open()

                Using command As New MySqlCommand(query, conn)
                    command.Parameters.AddWithValue("@UserId", userid)
                    command.Parameters.AddWithValue("@email", emailid)

                    Using reader As MySqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            emailid = reader.GetString("email")
                        Else
                            MessageBox.Show("User not found.")
                        End If
                    End Using
                End Using
            End Using

            If Not String.IsNullOrEmpty(emailid) Then
                Dim getForm As New UserGetOtp(emailid, "", 2)
                getForm.Show()
                Me.Close()
            End If
        End If
    End Sub

End Class
