Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class UserSignUp

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox3.PasswordChar = "*"
        TextBox4.PasswordChar = "*"
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
        If String.IsNullOrWhiteSpace(TextBox2.Text) OrElse String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf (TextBox3.Text = TextBox4.Text AndAlso CheckPasswordStrength(TextBox4.text)) Then
            Dim email As String = TextBox2.Text ' Store the email ID in a variable
            Dim password As String = TextBox3.Text ' Store the password in a variable
            'Dim cmd As String
            'cmd = "INSERT INTO users (email, password) VALUES ('" & TextBox2.Text & "', '" & TextBox3.Text & "')"
            'Dim success As Boolean = Globals.ExecuteInsertQuery(cmd)
            Dim otp = New UserGetOtp(email, password, 0)
            otp.Show()
            Me.Close()
        Else
            If Not CheckPasswordStrength(TextBox4.Text) Then
                MessageBox.Show("Password must be at least 8 characters long and contain at least 
one number, special character, uppercase letter, and lowercase letter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Passwords not matching!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        TextBox3.UseSystemPasswordChar = CheckBox1.Checked
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        TextBox4.UseSystemPasswordChar = CheckBox2.Checked
    End Sub
End Class
