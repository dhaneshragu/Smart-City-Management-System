Imports System.Data.SqlClient
Public Class Ed_ManageEntrExam

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, New Ed_ManageEntrHome())
        Me.Close()
    End Sub

    Private Sub Ed_ManageEntrExam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim labels As Ed_ExamApprvItem() = New Ed_ExamApprvItem(21) {}

        ' Create labels and set properties
        For i As Integer = 0 To 10
            labels(i) = New Ed_ExamApprvItem()
        Next

        ' Add labels to the FlowLayoutPanel
        For Each Ed_ExamApprvItem As Ed_ExamApprvItem In labels
            FlowLayoutPanel1.Controls.Add(Ed_ExamApprvItem)
        Next
        Dim labels1 As Ed_VenueItem() = New Ed_VenueItem(21) {}

        ' Create labels and set properties
        For i As Integer = 0 To 10
            labels1(i) = New Ed_VenueItem()
        Next

        ' Add labels to the FlowLayoutPanel
        For Each Ed_VenueItem As Ed_VenueItem In labels1
            FlowLayoutPanel2.Controls.Add(Ed_VenueItem)
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim enterSumForm As New Ed_Teacher_AddSummary()
        enterSumForm.ShowDialog() ' Show as dialog if needed
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim enterSumForm As New Ed_Teacher_AddSummary()
        enterSumForm.ShowDialog() ' Show as dialog if needed
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim addnewVenue As New Ed_newVenue()
        addnewVenue.ShowDialog()
    End Sub
End Class