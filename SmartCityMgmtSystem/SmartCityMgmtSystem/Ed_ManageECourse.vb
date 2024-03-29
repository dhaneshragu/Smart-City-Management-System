Imports System.Data.SqlClient
Public Class Ed_ManageECourse
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim labels As Ed_Teacher_CourseraItem() = New Ed_Teacher_CourseraItem(21) {}

        ' Create labels and set properties
        For i As Integer = 0 To 20
            labels(i) = New Ed_Teacher_CourseraItem()
            labels(i).CourseID = i

        Next

        ' Add labels to the FlowLayoutPanel
        For Each Ed_Teacher_CourseraItem As Ed_Teacher_CourseraItem In labels
            FlowLayoutPanel1.Controls.Add(Ed_Teacher_CourseraItem)
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
End Class