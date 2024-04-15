Public Class Ed_newVenue
    Public venueName As String
    Public venueAddress As String
    Public venueCapacity As Integer
    Private Sub Ed_newVenue_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        venueName = RichTextBox1.Text
        venueAddress = RichTextBox2.Text
        If Integer.TryParse(RichTextBox3.Text, venueCapacity) Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            ' The text does not represent an integer
            MessageBox.Show("The Capacity is not an integer.", "Integer Check", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
End Class