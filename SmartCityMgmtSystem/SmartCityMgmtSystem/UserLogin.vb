Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Common
Imports System.Security.Cryptography

Public Class UserLogin

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

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
                        Dim result = sqlCommand.ExecuteScalar()
                        If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                            pass = Convert.ToString(result)
                        End If
                    End Using
                End Using

                If TextBox3.Text = pass Then
                    Dim home = New HomePageDashboard With {
                        .uid = uid
                    }
                    home.Show()
                    Me.Close()
                Else
                    MessageBox.Show("Incorrect password or UserID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                End If

            Catch ex As Exception
                MessageBox.Show("Error converting TextBox1.Text to integer: " & ex.Message)
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
            End Try
        ElseIf Not String.IsNullOrWhiteSpace(TextBox2.Text) Then
            Dim cmd As String = "SELECT user_id, password FROM users WHERE email = @email"
            Using connection As New MySqlConnection(conStr)
                Using sqlCommand As New MySqlCommand(cmd, connection)
                    sqlCommand.Parameters.AddWithValue("@email", TextBox2.Text)
                    ' Execute the query and retrieve the user ID
                    connection.Open()
                    Using reader As MySqlDataReader = sqlCommand.ExecuteReader()
                        If reader.Read() Then
                            pass = reader("password").ToString()
                            uid = Convert.ToInt32(reader("user_id"))
                        End If
                    End Using
                End Using
            End Using

            If TextBox3.Text = pass Then
                Dim home = New HomePageDashboard With {
                    .uid = uid
                }
                home.Show()
                Me.Close()
            Else
                MessageBox.Show("Incorrect password or EmailID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
            End If
        Else
            MessageBox.Show("Please enter User ID or Email ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim pass As String = ""
        If TextBox2.Text IsNot Nothing AndAlso Not IsDBNull(TextBox2.Text) Then
            Dim otp = New UserGetOtp(TextBox2.Text, pass, 1)
            otp.Show()
            Me.Close()
        Else
            MessageBox.Show("Please enter your Email ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
End Class
