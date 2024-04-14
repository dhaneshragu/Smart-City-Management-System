Public Class Ed_FeePopup
    Public UserID As Integer
    Public UserName As String
    Public PayingTo As String
    Public PurposeOfPayment As String
    Public Amount As String
    Private entranceExamHandler As New Ed_EntranceExam_Handler()

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Retrieve the original password for the user


        ' Check if the entered password matches the original password

        Me.DialogResult = DialogResult.OK
            Me.Close()

    End Sub


    Private Sub Ed_FeePopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).TabStop = False
            End If
        Next
        TextBox1.Text = UserID.ToString
        TextBox2.Text = UserName
        TextBox3.Text = PayingTo
        TextBox6.Text = PurposeOfPayment
        TextBox7.Text = Amount
    End Sub
End Class