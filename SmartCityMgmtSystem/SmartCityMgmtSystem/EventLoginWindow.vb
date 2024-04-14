Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports MySql.Data.MySqlClient
Public Class EventLoginWindow

    Public Property uid As Integer
    Public Property u_name As String


    Private Sub EventLoginWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = uid
        TextBox2.PasswordChar = "*"
    End Sub

    Private Function CheckCredentials(ByVal CustomerID As Integer, ByVal Password As String) As Boolean
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try



        Dim query As String = "SELECT COUNT(*) FROM eventBookings WHERE customerID = @CustomerID AND password = @Password"
        cmd = New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@CustomerID", CustomerID)
        cmd.Parameters.AddWithValue("@Password", Password)

        Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

        Return count > 0

    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim CustomerID As Integer = TextBox1.Text
        Dim Password As String = TextBox2.Text

        If CheckCredentials(CustomerID, Password) Then
            ' Record exists, so you can pass parameters to another form
            Dim EventCustomerScreenForm As New EventCustomerScreen With {
                .uid = CustomerID,
                .password = Password
            }
            Me.ParentForm.Close()

            Dim EventDashboard As New EventDashboard With {
                .uid = CustomerID,
                .u_name = u_name
            }
            EventDashboard.Show()
            Globals.viewChildForm(EventDashboard.childformPanel, EventCustomerScreenForm)
            Me.Close()
        Else
            MessageBox.Show("Invalid CustomerID or Password")
        End If




    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim pass As String = ""
        Dim mail As String = ""
        Dim uid As Integer = -1
        If Not String.IsNullOrWhiteSpace(TextBox1.Text) Then
            Try
                uid = Convert.ToInt32(TextBox1.Text)
                Dim conStr As String = Globals.getdbConnectionString()
                Dim cmd As String = "SELECT email FROM users WHERE user_id = @uid"
                Using connection As New MySqlConnection(conStr)
                    Using sqlCommand As New MySqlCommand(cmd, connection)
                        sqlCommand.Parameters.AddWithValue("@uid", TextBox1.Text)
                        ' Execute the query and retrieve the user ID
                        connection.Open()
                        Using reader As MySqlDataReader = sqlCommand.ExecuteReader()
                            If reader.Read() Then
                                mail = Convert.ToString(reader("email"))
                            End If
                        End Using
                    End Using
                End Using

                Dim otp = New UserGetOtp(mail, pass, 1)
                otp.Show()
                Me.Close()
            Catch ex As Exception
                MessageBox.Show("Error converting TextBox1.Text to integer: " & ex.Message)
                TextBox1.Clear()
                TextBox2.Clear()
            End Try
        Else
            MessageBox.Show("Please enter your User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
End Class