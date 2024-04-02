Public Class ElectionDashboard

    Public Property LoggedInUserId As Integer = 2
    Public Property ElectionCommissionerId As Integer = 8
    Private Sub election_Click(sender As Object, e As EventArgs) Handles election.Click
        Globals.viewChildForm(childformPanel, ElectionInnerScreen1)
    End Sub

    Private Sub ElectionDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub admin_Click(sender As Object, e As EventArgs) Handles admin.Click
        If LoggedInUserId = ElectionCommissionerId Then
            ' If the logged-in user is the election commissioner, open one page
            Globals.viewChildForm(childformPanel, ElectionInnerScreenAdmin)
        Else
            ' Otherwise, open a different page
            Globals.viewChildForm(childformPanel, ElectionInnerScreenAdminAccDenied)
        End If
    End Sub

    Private Sub rti_Click(sender As Object, e As EventArgs) Handles rti.Click
        Globals.viewChildForm(childformPanel, ElectionInnerScreenCitizenRTI)
    End Sub

    Private Sub organizational_structure_Click(sender As Object, e As EventArgs) Handles organizational_structure.Click
        Globals.viewChildForm(childformPanel, ElectionInnerScreen2)
    End Sub

    Private Sub about_us_Click(sender As Object, e As EventArgs) Handles about_us.Click
        Globals.viewChildForm(childformPanel, ElectionInnerScreenWelcomeScreen)
    End Sub
End Class
