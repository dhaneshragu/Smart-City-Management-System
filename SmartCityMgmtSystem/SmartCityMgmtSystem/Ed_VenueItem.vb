Imports Google.Protobuf.WellKnownTypes

Public Class Ed_VenueItem
    Private entranceExamHandler As New Ed_EntranceExam_Handler()
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (Button1.Text = "Drop") Then
            Dim capacity As Integer
            If Integer.TryParse(Label1.Text, capacity) Then
                entranceExamHandler.drop_venue(Label2.Text, Label3.Text, capacity)
                Button1.Text = "Dropped"
                ' Update the UI accordingly or refresh the page
            Else
                MessageBox.Show("Capacity must be a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("The venue has already been dropped. Refresh the page to see changes.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub
End Class
