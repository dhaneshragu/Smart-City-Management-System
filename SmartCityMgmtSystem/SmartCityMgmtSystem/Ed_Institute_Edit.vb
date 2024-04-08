Imports System.Data.SqlClient
Public Class Ed_Institute_Edit
    Private callingPanel As Panel

    Public Sub New(panel As Panel)
        InitializeComponent()
        callingPanel = panel
    End Sub
    Private Sub Label7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim ed_list As New Ed_Institute_List(callingPanel, "Admin")
        Globals.viewChildForm(callingPanel, ed_list)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ed_list As New Ed_Institute_List(callingPanel, "Admin")
        Globals.viewChildForm(callingPanel, ed_list)
    End Sub
End Class