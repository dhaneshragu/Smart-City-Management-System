Public Class Employment_portal_admin_usercredentials

    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' Calculate the center position of the screen
        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
        Dim formWidth As Integer = Me.Width
        Dim formHeight As Integer = Me.Height

        Dim posX As Integer = (screenWidth - formWidth) \ 2
        Dim posY As Integer = (screenHeight - formHeight) \ 2

        ' Set the form's location to the calculated position
        Me.StartPosition = FormStartPosition.Manual
        Me.Location = New System.Drawing.Point(posX, posY)
        Me.Text = "User Credentials"
    End Sub

End Class
