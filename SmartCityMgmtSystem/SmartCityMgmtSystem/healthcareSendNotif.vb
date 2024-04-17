Imports System.Data.SqlClient
Imports SmartCityMgmtSystem.Globals
Public Class healthcareSendNotif
    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        Dim ministry, rec_uid, title, msg As Int16
        ministry = 0
        rec_uid = 0
        title = 0
        msg = 0

        Dim intMinistry, intReceiver As Integer

        If Not String.IsNullOrEmpty(TextBox1.Text) Then
            Dim userInput As String = TextBox1.Text

            If Integer.TryParse(userInput, intMinistry) Then
                ministry = 1
            Else
                MessageBox.Show("The Ministry ID should contain an integer value")
            End If
        Else
            ' The TextBox is empty
            MessageBox.Show("Please enter Ministry ID.")
        End If

        If Not String.IsNullOrEmpty(TextBox6.Text) Then
            Dim userInput As String = TextBox6.Text

            If Integer.TryParse(userInput, intReceiver) Then
                rec_uid = 1
            Else
                MessageBox.Show("The Receiver UID should contain an integer value")
            End If
        Else
            ' The TextBox is empty
            MessageBox.Show("Please enter Receiver UID.")
        End If

        If Not String.IsNullOrEmpty(TextBox2.Text) Then
            title = 1
        Else
            ' The TextBox is empty
            MessageBox.Show("Please enter notification title.")
        End If

        If Not String.IsNullOrEmpty(TextBox3.Text) Then
            msg = 1
        Else
            ' The TextBox is empty
            MessageBox.Show("Please enter notification message.")
        End If

        If ministry = 1 And rec_uid = 1 And title = 1 And msg = 1 Then
            SendNotifications(3, intReceiver, TextBox2.Text, TextBox3.Text)
            MessageBox.Show("Notification sent successfully.")
            TextBox1.Text = ""
            TextBox6.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox6.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub


End Class