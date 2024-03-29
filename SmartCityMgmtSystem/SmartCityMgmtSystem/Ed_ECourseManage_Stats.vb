Imports System.Data.SqlClient
Public Class Ed_ECourseManage_Stats
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim labels As Ed_ECourseStatDropItem() = New Ed_ECourseStatDropItem(21) {}

        ' Create labels and set properties
        For i As Integer = 0 To 20
            labels(i) = New Ed_ECourseStatDropItem()
        Next

        ' Add labels to the FlowLayoutPanel
        For Each Ed_ECourseStatDropItem As Ed_ECourseStatDropItem In labels
            FlowLayoutPanel1.Controls.Add(Ed_ECourseStatDropItem)
        Next
    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub

    Private Sub Label6_Click_1(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub
End Class