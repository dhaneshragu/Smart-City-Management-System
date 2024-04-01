Imports System.Data.SqlClient
Public Class Ed_Institute_List

    Public Property user_type As String
    Private callingPanel As Panel

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(panel As Panel)
        InitializeComponent()
        callingPanel = panel
    End Sub

    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim labels As Ed_Institute_ListItem() = New Ed_Institute_ListItem(21) {}

        ' Create labels and set properties
        For i As Integer = 0 To 20
            labels(i) = New Ed_Institute_ListItem()

            If (Me.user_type = "Admin") Then
                labels(i).Button2.Text = "Edit"
                AddHandler labels(i).Button2.Click, AddressOf Edit_Label_Click ' Add click event handler
            End If
        Next

        ' Add labels to the FlowLayoutPanel
        For Each Ed_Institute_ListItem As Ed_Institute_ListItem In labels
            FlowLayoutPanel1.Controls.Add(Ed_Institute_ListItem)
        Next
    End Sub
    Private Sub Edit_Label_Click(sender As Object, e As EventArgs)
        Globals.viewChildForm(callingPanel, New Ed_Institute_Edit(callingPanel))
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub

    Private Sub Label6_Click_1(sender As Object, e As EventArgs)

    End Sub
End Class