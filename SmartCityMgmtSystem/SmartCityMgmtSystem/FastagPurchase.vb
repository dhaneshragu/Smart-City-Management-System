Public Class FastagPurchase
    Public Property ftid As Integer = 2
    Public Sub SetDetails(ft_id As Integer, valdity As Integer,
                           Optional drvlicensenum As String = "12345",
                          Optional dt As String = "21st March 2024",
                           Optional fare As Integer = 0)
        ' Set the labels
        lbldrv.Text = "     " & drvlicensenum
        lbldt.Text = "       " & dt
        lblfare.Text = "       ₹" & fare
        Label2.Text = "      " & valdity & " Months Validity"
        ftid = ft_id
    End Sub

    Private Sub btnview_Click(sender As Object, e As EventArgs) Handles btnview.Click


    End Sub
End Class
