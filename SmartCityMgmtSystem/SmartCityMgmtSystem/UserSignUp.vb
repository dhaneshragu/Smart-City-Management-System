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

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If String.IsNullOrWhiteSpace(TextBox2.Text) OrElse String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf (TextBox3.Text = TextBox4.Text) Then
            Dim email As String = TextBox2.Text ' Store the email ID in a variable
            Dim password As String = TextBox3.Text ' Store the password in a variable
            'Dim cmd As String
            'cmd = "INSERT INTO users (email, password) VALUES ('" & TextBox2.Text & "', '" & TextBox3.Text & "')"
            'Dim success As Boolean = Globals.ExecuteInsertQuery(cmd)
            Dim otp = New UserGetOtp(email, password, 0)
            otp.Show()
            Me.Close()
        Else
            MessageBox.Show("Password not matching!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()

    End Sub

End Class
