Imports System.Data.SqlClient
Public Class EventPayment_Interface
    Private Property uid As Integer
    Private Sub EventPayment_Interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim pay = New PaymentGateway() With {
                .uid = uid
              }


        If (pay.ShowDialog() = DialogResult.OK) Then
            MessageBox.Show("Payment Successful!..")
            Me.Close()
        Else
            MessageBox.Show("Payment Failed..")
        End If

    End Sub
End Class