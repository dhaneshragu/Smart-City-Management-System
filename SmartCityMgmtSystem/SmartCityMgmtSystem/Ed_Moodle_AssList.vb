﻿Imports System.Data.SqlClient
Public Class Ed_Moodle_AssList
    Public RoomID As Integer
    Public Seq_no As Integer
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim labels As Ed_LeftPanelItem() = New Ed_LeftPanelItem(8) {}

        ' Create labels and set properties
        For i As Integer = 0 To 7
            labels(i) = New Ed_LeftPanelItem()
            labels(i).Label1.Text = "CA " & (i + 1)
            AddHandler labels(i).Label1.Click, AddressOf Label_Click ' Add click event handler
        Next
        ' Add labels to the FlowLayoutPanel
        For Each Label As Ed_LeftPanelItem In labels
            FlowLayoutPanel1.Controls.Add(Label)
        Next
    End Sub
    Private Sub Label_Click(sender As Object, e As EventArgs)
        Globals.viewChildForm(Panel1, New Ed_Moodle_CourseAss(Panel1))
    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class