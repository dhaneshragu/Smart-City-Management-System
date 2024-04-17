Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox
Imports MySql.Data.MySqlClient
Public Class FastagPurchase
    Public Property ftid As Integer = 2
    Public Property uid As Integer = 11
    Public Sub SetDetails(id As Integer, ft_id As Integer, valdity As Integer,
                           Optional drvlicensenum As String = "12345",
                          Optional dt As String = "21st March 2024",
                           Optional fare As Integer = 0)
        ' Set the labels
        lbldrv.Text = "     " & drvlicensenum
        lbldt.Text = "       " & dt
        lblfare.Text = "       ₹" & fare
        Label2.Text = "      " & valdity & " Months Validity"
        Label3.Text = "     " & ft_id
        ftid = ft_id
    End Sub

    Private Sub btnview_Click(sender As Object, e As EventArgs) Handles btnview.Click
        'Check the lbldt and validity months and display if the validity is expired
        Dim dt As Date = Date.Parse(lbldt.Text.Trim())
        Dim validity As Integer = Integer.Parse(Label2.Text.Trim().Split(" ")(0))
        Dim expiry As Date = dt.AddMonths(validity)
        If expiry < Date.Now Then
            Dim res As DialogResult = MessageBox.Show("Your fastag has expired. Do you want to renew your Fastag with a surcharge of ₹100?", "Renew Fastag", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If res = DialogResult.Yes Then
                Dim paymentGateway As New PaymentGateway() With
           {
           .uid = uid,
           .readonly_prop = True
           }
                paymentGateway.TextBox1.Text = 5 'Transport minister UID
                paymentGateway.TextBox2.Text = 100 'Renewal Rupees
                paymentGateway.TextBox3.Text = "Fastag Renewal for driving license: " & lbldrv.Text.Trim()
                'If payment is successful, add to fastag purchases
                If paymentGateway.ShowDialog() = DialogResult.OK Then '
                    'Update the fastag_purchases table with the new expiry date using date from visual basic
                    Dim query As String = "UPDATE fastag_purchases SET bought_on = '" & Date.Now.ToString("yyyy-MM-dd") & "' WHERE purchase_id = " & ftid & " AND uid = " & uid
                    If Globals.ExecuteUpdateQuery(query) Then
                        MessageBox.Show("Fastag renewed successfully", "Fastag Renewed", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Globals.ExecuteUpdateQuery("UPDATE admin_records set toll_revenue = toll_revenue + " & 100)
                    Else
                        MessageBox.Show("Error renewing fastag", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        Else
            'Show Yes or no messagebox and ask whether if they want to top up it and if yes, open the payment gateway
            Dim res As DialogResult = MessageBox.Show("Your fastag is still valid. Do you want to top up your Fastag?", "Top Up Fastag", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If res = DialogResult.Yes Then
                'Ask the user for topup amount from Transport_Enter_Fee Form
                Dim topup As Integer = 0
                Dim topupForm As New Transport_EnterFee()
                topupForm.Label1.Text = "Top-Up: "
                If topupForm.ShowDialog() = DialogResult.OK Then
                    topup = topupForm.NumericUpDown1.Value
                End If
                If topup <= 0 Then
                    MessageBox.Show("Invalid top up amount", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                Dim paymentGateway As New PaymentGateway() With
            {
            .uid = uid,
            .readonly_prop = True
            }
                paymentGateway.TextBox1.Text = 5 'Transport minister UID

                'Check if topoup is greater than 0 rupees
                paymentGateway.TextBox2.Text = topup 'Amount to top up
                paymentGateway.TextBox3.Text = "Fastag Topup for driving license: " & lbldrv.Text.Trim()
                'If payment is successful, add to fastag purchases
                If paymentGateway.ShowDialog() = DialogResult.OK Then
                    'Update the amount left in the fastag purchases
                    Dim query As String = "UPDATE fastag_purchases SET amt_left = amt_left + " & topup & " WHERE purchase_id = " & ftid & " AND uid = " & uid
                    If Globals.ExecuteUpdateQuery(query) Then
                        MessageBox.Show("Fastag topped up successfully", "Fastag Topped Up", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Refresh()
                        Globals.ExecuteUpdateQuery("UPDATE admin_records set toll_revenue = toll_revenue + " & topup)
                    Else
                        MessageBox.Show("Error topping up fastag", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End
                    End If

                End If
            End If
        End If
    End Sub
End Class
