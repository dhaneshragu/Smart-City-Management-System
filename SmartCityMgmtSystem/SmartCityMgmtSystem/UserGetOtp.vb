Imports System.Net.Mail

Public Class UserGetOtp
    Private ReadOnly email As String
    Private ReadOnly password As String
    Private ReadOnly nextPage As Integer
    Dim randstr As String

    Public Sub New(email As String, password As String, nextPage As Integer)
        InitializeComponent()
        Me.email = email ' Store the passed email ID in a private field
        Me.password = password
        Me.nextPage = nextPage          '0 for details page, 1 for password reset page

        Dim randomNumber As Integer = New Random().Next(1000, 10000)
        randstr = randomNumber.ToString()

        ' Send the random number to the user's email
        SendEmail(email, randstr)

    End Sub
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub SendEmail(recipientEmail As String, randomNumber As String)
        Try
            Dim smtpClient As New SmtpClient()
            Dim mail As New MailMessage()

            Dim from, pass As String
            from = "smartcityiitguwahati@gmail.com"
            pass = "cypu wiqj ntsh buoy"

            ' Configure SMTP settings
            smtpClient.Host = "smtp.gmail.com" ' SMTP server address
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network
            smtpClient.Port = 587 ' the SMTP port number
            smtpClient.EnableSsl = True ' Enable SSL if required
            smtpClient.UseDefaultCredentials = False
            smtpClient.Credentials = New System.Net.NetworkCredential(from, pass)
            ' SMTP username and password

            ' Configure email message
            mail.From = New MailAddress(from) ' the sender's email address
            mail.To.Add(recipientEmail) ' the recipient's email address
            mail.Subject = "SmartCityGuwahati Verification Code"

            ' HTML content with dynamic random number
            Dim htmlContent As String = "<div style=""font-family: Helvetica,Arial,sans-serif;min-width:1000px;overflow:auto;line-height:2"">" &
            "<div style=""margin:50px auto;width:70%;padding:20px 0"">" &
            "<div style=""border-bottom:1px solid #eee"">" &
            "<a href="""" style=""font-size:1.4em;color: #00466a;text-decoration:none;font-weight:600"">SmartGhy - Smart City Management System for Guwahati</a>" &
            "</div>" &
            "<p style=""font-size:1.1em"">Hi,</p>" &
            "<p>Use the following OTP to complete your Sign Up procedures</p>" &
            "<h2 style=""background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;"">" & randomNumber & "</h2>" &
            "<p style=""font-size:0.9em;"">Regards,<br />Smart City Management System, Guwahati</p>" &
            "<hr style=""border:none;border-top:1px solid #eee"" />" &
            "<div style=""float:right;padding:8px 0;color:#aaa;font-size:0.8em;line-height:1;font-weight:300"">" &
            "<p>SmartGhy Inc</p>" &
            "<p>IIT Guwahati,Amingaon</p>" &
            "<p>Assam</p>" &
            "</div>" &
            "</div>" &
            "</div>"

            ' Set HTML content to the email body
            mail.IsBodyHtml = True
            mail.Body = htmlContent

            ' Send the email
            smtpClient.Send(mail)
            MsgBox("An email with the OTP has been sent to your registered email address.")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        'Globals.viewChildForm(childformPanel, TransportationAdminHome)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If (TextBox1.Text.Equals(randstr) AndAlso nextPage = 0) Then
            Dim details = New UserDetails(email, password)
            details.Show()
            Me.Close()
        ElseIf (TextBox1.Text.Equals(randstr) AndAlso nextPage = 1) Then
            Dim reset = New UserResetPassword(email, password)
            reset.Show()
            Me.Close()
        ElseIf (TextBox1.Text.Equals(randstr) AndAlso nextPage = 2) Then
            Dim reset = New BankPasswordResetvb(email, "")
            reset.Show()
            Me.Close()
        Else
            MessageBox.Show("OTP verification failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

End Class
