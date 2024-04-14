Imports System.Data.SqlClient

Public Class PayBills
    Public Property uid As Integer = 1
    ' Dictionary to track payment status for each bill
    Private paymentStatus As New Dictionary(Of String, Boolean)()

    Private Sub PayBills_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize payment status for each bill
        paymentStatus.Add("Electricity", False)
        paymentStatus.Add("Internet", False)
        paymentStatus.Add("Water", False)
        paymentStatus.Add("Gas", False)
        paymentStatus.Add("TV Bill", False)

        ' Check if it's the 1st day of the month
        If DateTime.Now.Day = 1 Then
            ' Re-enable buttons
            Button1.Visible = True
            Button2.Visible = True
            Button3.Visible = True
            Button4.Visible = True
            Button5.Visible = True
        Else
            ' Disable buttons
            Button1.Visible = False
            Button2.Visible = False
            Button3.Visible = False
            Button4.Visible = False
            Button5.Visible = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Electricity bill payment
        If Not paymentStatus("Electricity") Then
            Dim pay As New PaymentGateway() With {
                .uid = uid,
                .readonly_prop = True
            }
            pay.TextBox1.Text = 10
            pay.TextBox2.Text = 700
            pay.TextBox3.Text = "Electricity Bill Payment."

            If pay.ShowDialog() = DialogResult.OK Then
                ' If payment is successful, update payment status and button text
                paymentStatus("Electricity") = True
                MessageBox.Show("Payment successful! Electricity Bill for this month has been paid.")
                ' Hide the button after payment
                Button1.Visible = False
            Else
                MessageBox.Show("Payment failed.")
            End If
        Else
            MessageBox.Show("Electricity bill for this month has already been paid.")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Internet bill payment
        If Not paymentStatus("Electricity") Then
            Dim pay As New PaymentGateway() With {
                .uid = uid,
                .readonly_prop = True
            }
            pay.TextBox1.Text = 10
            pay.TextBox2.Text = 500
            pay.TextBox3.Text = "Internet Bill Payment."

            If pay.ShowDialog() = DialogResult.OK Then
                ' If payment is successful, update payment status and button text
                paymentStatus("Internet") = True
                MessageBox.Show("Payment successful! Internet Bill for this month has been paid.")
                ' Hide the button after payment
                Button1.Visible = False
            Else
                MessageBox.Show("Payment failed.")
            End If
        Else
            MessageBox.Show("Internet bill for this month has already been paid.")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Water bill payment
        If Not paymentStatus("Electricity") Then
            Dim pay As New PaymentGateway() With {
                .uid = uid,
                .readonly_prop = True
            }
            pay.TextBox1.Text = 10
            pay.TextBox2.Text = 100
            pay.TextBox3.Text = "Water Bill Payment."

            If pay.ShowDialog() = DialogResult.OK Then
                ' If payment is successful, update payment status and button text
                paymentStatus("Water") = True
                MessageBox.Show("Payment successful! Water Bill for this month has been paid.")
                ' Hide the button after payment
                Button1.Visible = False
            Else
                MessageBox.Show("Payment failed.")
            End If
        Else
            MessageBox.Show("Water bill for this month has already been paid.")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Gas bill payment
        If Not paymentStatus("Electricity") Then
            Dim pay As New PaymentGateway() With {
                .uid = uid,
                .readonly_prop = True
            }
            pay.TextBox1.Text = 10
            pay.TextBox2.Text = 500
            pay.TextBox3.Text = "Gas Bill Payment."

            If pay.ShowDialog() = DialogResult.OK Then
                ' If payment is successful, update payment status and button text
                paymentStatus("Gas") = True
                MessageBox.Show("Payment successful! Gas Bill for this month has been paid.")
                ' Hide the button after payment
                Button1.Visible = False
            Else
                MessageBox.Show("Payment failed.")
            End If
        Else
            MessageBox.Show("Gas bill for this month has already been paid.")
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Electricity bill payment
        If Not paymentStatus("Electricity") Then
            Dim pay As New PaymentGateway() With {
                .uid = uid,
                .readonly_prop = True
            }
            pay.TextBox1.Text = 10
            pay.TextBox2.Text = 700
            pay.TextBox3.Text = "Electricity Bill Payment."

            If pay.ShowDialog() = DialogResult.OK Then
                ' If payment is successful, update payment status and button text
                paymentStatus("Electricity") = True
                MessageBox.Show("Payment successful! Electricity Bill for this month has been paid.")
                ' Hide the button after payment
                Button1.Visible = False
            Else
                MessageBox.Show("Payment failed.")
            End If
        Else
            MessageBox.Show("Electricity bill for this month has already been paid.")
        End If
    End Sub

End Class
