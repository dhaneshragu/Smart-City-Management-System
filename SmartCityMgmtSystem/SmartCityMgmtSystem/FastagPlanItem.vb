Public Class FastagPlanItem
    Public Property fastag_id As Integer = 1
    Public Property uid As Integer = 11
    Public Sub SetDetails(fastagid As Integer, vehtype As String,
                           Optional validity As String = "Valid for 3 months",
                           Optional fare As Integer = 0)
        ' Set the labels
        lblvehtype.Text = "       " & vehtype
        lblvalidity.Text = "       " & validity
        lblfare.Text = "       ₹" & fare
        fastag_id = fastagid
    End Sub
    Private Function getDLID() As Integer
        'Get the driving license ID given the UID
        Dim dl_id As Integer = 0

        Return dl_id


    End Function
    Private Sub btnview_Click(sender As Object, e As EventArgs) Handles btnview.Click
        'Add to fastag purchases
        Dim dl_id As Integer = getDLID()
        Dim query As String = "INSERT INTO fastag_purchases (fastag_id, user_id, purchase_date) VALUES (@fastag_id, @user_id, @purchase_date)"

    End Sub
End Class
