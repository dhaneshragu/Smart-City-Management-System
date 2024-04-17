Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class UserResetPassword
    Private ReadOnly email As String
    Private ReadOnly password As String

    Public Sub New(email As String, password As String)
        InitializeComponent()
        Me.email = email ' Store the passed email ID in a private field
        Me.password = password
    End Sub
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox1.PasswordChar = "*"
        TextBox2.PasswordChar = "*"
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        'Globals.viewChildForm(childformPanel, TransportationAdminHome)
    End Sub

    Private Function CheckPasswordStrength(password As String) As Boolean
        ' Check if password meets criteria
        Dim hasNumber As Boolean = False
        Dim hasSpecialChar As Boolean = False
        Dim hasUpperCase As Boolean = False
        Dim hasLowerCase As Boolean = False

        ' Define special characters
        Dim specialCharacters As String = "!@#$%^&*()_+-=[]{}|;:',.<>?/~"

        For Each c As Char In password
            If Char.IsDigit(c) Then
                hasNumber = True
            ElseIf specialCharacters.Contains(c) Then
                hasSpecialChar = True
            ElseIf Char.IsUpper(c) Then
                hasUpperCase = True
            ElseIf Char.IsLower(c) Then
                hasLowerCase = True
            End If
        Next

        ' Check all criteria
        Return password.Length >= 8 AndAlso hasNumber AndAlso hasSpecialChar AndAlso hasUpperCase AndAlso hasLowerCase
    End Function

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim uid As Integer = -1
        If (TextBox1.Text.Equals(TextBox2.Text) AndAlso CheckPasswordStrength(TextBox2.Text)) Then
            Dim cmd As String = "UPDATE users SET password= @password WHERE email = @email"
            Dim conStr As String = Globals.getdbConnectionString()
            Using connection As New MySqlConnection(conStr)
                Using sqlCommand As New MySqlCommand(cmd, connection)
                    sqlCommand.Parameters.AddWithValue("@password", TextBox1.Text)
                    sqlCommand.Parameters.AddWithValue("@email", email)
                    Try
                        connection.Open()
                        sqlCommand.ExecuteNonQuery()
                        MessageBox.Show("Password updated successfully.")
                        Dim q As String = "SELECT user_id FROM users WHERE email = @email"

                        Using getUid As New MySqlCommand(q, connection)
                            getUid.Parameters.AddWithValue("@email", email)
                            Dim result = getUid.ExecuteScalar()

                            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                                uid = Convert.ToInt32(result)
                            End If
                        End Using
                        Dim home = New HomePageDashboard With {
                            .uid = uid
                        }
                        home.Show()
                        Me.Close()
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message)
                    End Try
                End Using
            End Using
        Else
            If Not CheckPasswordStrength(TextBox2.Text) Then
                MessageBox.Show("Password must be at least 8 characters long and contain at least 
one number, special character, uppercase letter, and lowercase letter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Passwords not matching!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        TextBox1.UseSystemPasswordChar = CheckBox1.Checked
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        TextBox2.UseSystemPasswordChar = CheckBox2.Checked
    End Sub
End Class
