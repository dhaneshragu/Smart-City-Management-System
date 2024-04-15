Imports System.Data.SqlClient
Public Class Ed_Institute_Edit
    Private callingPanel As Panel
    Dim handler As New Ed_Institute_Handler
    Public inst_id As Integer
    Public Sub New(panel As Panel, id As Integer)
        InitializeComponent()
        callingPanel = panel
        inst_id = id
    End Sub
    Private Sub Label7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim ed_list As New Ed_Minister_Institute_List(callingPanel, "Admin")
        Globals.viewChildForm(callingPanel, ed_list)
    End Sub



    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        'Retrieve details and add institution'
        Dim name As String = TextBox2.Text
        Dim address As String = TextBox3.Text
        Dim principal_id As Integer = 0 ' Initialize with a default value
        If Not String.IsNullOrEmpty(TextBox6.Text) Then
            If Integer.TryParse(TextBox6.Text, principal_id) Then
                'do  nothing'

            Else
                MsgBox("Invalid input for Principal ID.")
            End If
        Else
            MsgBox("PrincipalID is empty.")
        End If

        'Retrieve text in combobox1'
        Dim inst_type As String = ComboBox1.Text
        Dim inst_photo As Byte() = Nothing

        If name = "" Or address = "" Or inst_type = "" Then
            MsgBox("Please fill all fields")
        Else
            Dim inst_id = handler.UpdateInstitution(Me.inst_id, name, address, principal_id, inst_photo, inst_type)
            MsgBox("Updated successfully")

            Dim ed_list As New Ed_Minister_Institute_List(callingPanel, "Admin")
            Globals.viewChildForm(callingPanel, ed_list)

        End If


    End Sub

    Private Sub Ed_Institute_Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'retrive institue details and display in textboxes '
        Dim institute As Ed_Institute_Handler.EdInstitution = handler.GetInstitutionbyID(inst_id)
        TextBox2.Text = institute.Inst_Name
        TextBox3.Text = institute.Inst_Address
        TextBox6.Text = institute.Inst_PrincipalID
        ComboBox1.Text = institute.Inst_Type

    End Sub
End Class