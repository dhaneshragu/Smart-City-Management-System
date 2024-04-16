Public Class ElectionDashboard
    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Public Property ElectionCommissionerId As Integer = 8

    Dim electionInnerScreen1 As ElectionInnerScreen1 = Nothing
    Dim electionInnerScreenAdmin As ElectionInnerScreenAdmin = Nothing
    Dim electionInnerScreenAdminAccDenied As ElectionInnerScreenAdminAccDenied = Nothing
    Dim electionInnerScreenCitizenRTI As ElectionInnerScreenCitizenRTI = Nothing


    'Public Property LoggedInUserId As Integer = 105

    Private Sub ElectionDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label3.Text = uid
    End Sub

    Private Sub election_Click(sender As Object, e As EventArgs) Handles election.Click
        electionInnerScreen1?.Dispose()
        electionInnerScreen1 = New ElectionInnerScreen1 With {
            .innerPanel = childformPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, electionInnerScreen1)
    End Sub

    Private Sub admin_Click(sender As Object, e As EventArgs) Handles admin.Click
        If uid = ElectionCommissionerId Then
            electionInnerScreenAdmin?.Dispose()
            electionInnerScreenAdmin = New ElectionInnerScreenAdmin With {
                .innerPanel = childformPanel,
                .uid = uid,
                .u_name = u_name
            }
            ' If the logged-in user is the election commissioner, open one page
            Globals.viewChildForm(childformPanel, electionInnerScreenAdmin)
        Else
            electionInnerScreenAdminAccDenied?.Dispose()
            electionInnerScreenAdminAccDenied = New ElectionInnerScreenAdminAccDenied With {
                .innerPanel = childformPanel,
                .uid = uid,
                .u_name = u_name
            }
            ' Otherwise, open a different page
            Globals.viewChildForm(childformPanel, electionInnerScreenAdminAccDenied)
        End If
    End Sub

    Private Sub rti_Click(sender As Object, e As EventArgs) Handles rti.Click
        electionInnerScreenCitizenRTI?.Dispose()
        electionInnerScreenCitizenRTI = New ElectionInnerScreenCitizenRTI With {
            .innerPanel = childformPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, electionInnerScreenCitizenRTI)
    End Sub

    Private Sub organizational_structure_Click(sender As Object, e As EventArgs) Handles organizational_structure.Click
        Globals.viewChildForm(childformPanel, ElectionInnerScreen2)
    End Sub

    Private Sub about_us_Click(sender As Object, e As EventArgs) Handles about_us.Click
        Globals.viewChildForm(childformPanel, ElectionInnerScreenWelcomeScreen)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim home = New HomePageDashboard() With {
            .uid = uid
        }
        home.Show()
        Me.Close()
    End Sub
End Class
