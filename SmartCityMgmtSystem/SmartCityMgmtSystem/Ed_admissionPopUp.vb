Public Class Ed_admissionPopUp
    Public SemOrClass As Boolean
    Public Value As Integer

    Private Sub Ed_admissionPopUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Text = 1
        ComboBox2.Text = 1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (RadioButton1.Checked) Then
            SemOrClass = False
            Value = ComboBox1.Text
        End If
        If (RadioButton2.Checked) Then
            SemOrClass = True
            Value = ComboBox2.Text
        End If
        Me.DialogResult = DialogResult.OK
    End Sub
End Class