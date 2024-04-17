Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Common
Imports System.Security.Cryptography
Imports System.Web
Imports System.Web.UI.WebControls

Public Class UserLogin

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox3.PasswordChar = "*"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Globals.viewChildForm(childformPanel, UserSignUpPage)
        Dim sign = New UserSignUp()
        sign.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim pass As String = ""
        Dim uid As Integer = -1
        Dim conStr As String = Globals.getdbConnectionString()
        If Not String.IsNullOrWhiteSpace(TextBox1.Text) Then
            Try
                uid = Convert.ToInt32(TextBox1.Text)
                Dim cmd As String = "SELECT password FROM users WHERE user_id = @uid"
                Using connection As New MySqlConnection(conStr)
                    Using sqlCommand As New MySqlCommand(cmd, connection)
                        sqlCommand.Parameters.AddWithValue("@uid", TextBox1.Text)
                        ' Execute the query and retrieve the user ID
                        connection.Open()
                        Using reader As MySqlDataReader = sqlCommand.ExecuteReader()
                            If reader.Read() Then
                                pass = Convert.ToString(reader("password"))
                            End If
                        End Using
                    End Using
                End Using

                If TextBox3.Text <> pass Then
                    MessageBox.Show("Incorrect password or UserID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    TextBox1.Clear()
                    TextBox3.Clear()
                Else
                    Dim home = New HomePageDashboard With {
                        .uid = uid
                    }
                    home.Show()
                    Me.Close()
                End If

            Catch ex As Exception
                MessageBox.Show("Error converting TextBox1.Text to integer: " & ex.Message)
                TextBox1.Clear()
                TextBox3.Clear()
            End Try
        Else
            MessageBox.Show("Please enter your User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                TextBox3.Clear()
            End Try
        Else
            MessageBox.Show("Please enter your User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
End Class
