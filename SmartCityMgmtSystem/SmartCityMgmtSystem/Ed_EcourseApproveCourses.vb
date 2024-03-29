Imports System.Data.SqlClient
Public Class Ed_EcourseApproveCourses
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim labels As Ed_ECourseApproveItem() = New Ed_ECourseApproveItem(7) {}

        ' Create labels and set properties
        For i As Integer = 0 To 6
            labels(i) = New Ed_ECourseApproveItem()
        Next

        ' Add labels to the FlowLayoutPanel
        For Each Ed_ECourseApproveItem As Ed_ECourseApproveItem In labels
            FlowLayoutPanel1.Controls.Add(Ed_ECourseApproveItem)
        Next
    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Label6_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub FlowLayoutPanel1_Paint_1(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub
End Class