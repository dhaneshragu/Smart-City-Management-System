Imports System.Data.SqlClient
Public Class Ed_ManageEntrHome
    Private Sub Ed_ManageEntrHome_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub
    Private Sub PictureButtonvb1_Click(sender As Object, e As EventArgs) Handles PictureButtonvb1.Click
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, New Ed_ManageEntrExam(1))
        Me.Close()
    End Sub

    Private Sub PictureButtonvb4_Click(sender As Object, e As EventArgs) Handles PictureButtonvb4.Click
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, New Ed_ManageEntrExam(2))
        Me.Close()
    End Sub

    Private Sub PictureButtonvb3_Click(sender As Object, e As EventArgs) Handles PictureButtonvb3.Click
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, New Ed_ManageEntrExam(3))
        Me.Close()
    End Sub

    Private Sub PictureButtonvb2_Click(sender As Object, e As EventArgs) Handles PictureButtonvb2.Click
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, New Ed_ManageEntrExam(4))
        Me.Close()
    End Sub
End Class