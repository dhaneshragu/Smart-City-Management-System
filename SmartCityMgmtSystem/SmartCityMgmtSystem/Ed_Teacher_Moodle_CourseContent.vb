Imports System.Data.SqlClient
Public Class Ed_Teacher_Moodle_CourseContent
    Private RoomID As Integer
    Private callingPanel As Panel


    ' Constructor that accepts a Panel parameter
    Public Sub New(roomID As Integer, panel As Panel)
        InitializeComponent()
        roomID = roomID
        callingPanel = panel
    End Sub

    Private Sub Ed_Teacher_Moodle_CourseContent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim labels As Ed_Teacher_Moodle_ResourceLinkItem() = New Ed_Teacher_Moodle_ResourceLinkItem(8) {}

        ' Create labels and set properties
        For i As Integer = 0 To 7
            labels(i) = New Ed_Teacher_Moodle_ResourceLinkItem()
            labels(i).Label1.Text = "Resource " & (i + 1)
            AddHandler labels(i).Label1.Click, AddressOf Resource_Label_Click ' Add click event handler
        Next
        ' Add labels to the FlowLayoutPanel
        For Each Label As Ed_Teacher_Moodle_ResourceLinkItem In labels
            FlowLayoutPanel1.Controls.Add(Label)
        Next

        ' Create labels and set properties
        For i As Integer = 0 To 7
            labels(i) = New Ed_Teacher_Moodle_ResourceLinkItem()
            labels(i).Label1.Text = "Assignment " & (i + 1)
            AddHandler labels(i).Label1.Click, AddressOf Assgn_Label_Click ' Add click event handler
        Next
        ' Add labels to the FlowLayoutPanel
        For Each Label As Ed_Teacher_Moodle_ResourceLinkItem In labels
            FlowLayoutPanel2.Controls.Add(Label)
        Next
    End Sub
    Private Sub Resource_Label_Click(sender As Object, e As EventArgs)
        Dim resourceForm As New Ed_Teacher_Moodle_CourseResource(callingPanel, "Moodle")
        resourceForm.Name = "HELLO"
        Globals.viewChildForm(callingPanel, resourceForm)
    End Sub

    Private Sub Assgn_Label_Click(sender As Object, e As EventArgs)
        Dim resourceForm As New Ed_Teacher_Moodle_CourseAss(callingPanel, "Moodle")
        resourceForm.Name = "HELLO"
        Globals.viewChildForm(callingPanel, resourceForm)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim add_res_form As New Ed_Teacher_AddResource()
        add_res_form.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim add_assgn_form As New Ed_Teacher_AddAssgn()
        add_assgn_form.ShowDialog()
    End Sub
End Class