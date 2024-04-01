Imports System.Data.SqlClient
Public Class UserBankDetails

    Public Property uname As String = "Me"
    Public Property accNumber As Integer = 1

    Private Sub UserBankDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Text = uname
        Label5.Text = accNumber
    End Sub

End Class