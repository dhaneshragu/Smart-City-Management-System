Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient


Public Class Ed_Minister_Institute_ListItem
    Public instituteID As Integer
    Public callingPanel As Panel




    Private Sub Ed_Institute_ListItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Globals.viewChildForm(callingPanel, New Ed_Institute_Edit(callingPanel, instituteID))
    End Sub
End Class
