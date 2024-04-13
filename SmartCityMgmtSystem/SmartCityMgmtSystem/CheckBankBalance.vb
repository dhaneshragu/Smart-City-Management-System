Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class CheckBankBalance
    Public Property balance As Integer = 1
    Public Property accnum As Integer = 1
    Private Sub CheckBankBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label5.Text = balance
        Label3.Text = accnum
    End Sub

End Class