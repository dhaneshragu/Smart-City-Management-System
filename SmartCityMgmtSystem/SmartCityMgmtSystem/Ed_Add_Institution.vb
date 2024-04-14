Imports System.Data.SqlClient
Public Class Ed_Add_Institution

    Dim handler As New Ed_Institute_Handler
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
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
            Dim inst_id = handler.AddInstitution(name, address, principal_id, inst_photo, inst_type)
            MsgBox("Institution with ID: " & inst_id & " added successfully")

            'Set all textboxes to be empty'
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox6.Text = ""

        End If

    End Sub
End Class