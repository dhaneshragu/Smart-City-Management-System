Imports System.Data.SqlClient
Public Class ElectionInnerScreenAdmin
    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"

    Public Property innerPanel As Panel

    Dim electionInnerScreenAdminNomination As ElectionInnerScreenAdminNomination = Nothing
    Dim electionInnerScreenAdminTimeline As ElectionInnerScreenAdminTimeline = Nothing
    Dim electionInnerScreenAdminVotes As ElectionInnerScreenAdminVotes = Nothing
    Dim electionInnerScreenAdminResults As ElectionInnerScreenAdminResults = Nothing
    Dim electionInnerScreenAdminRTI As ElectionInnerScreenAdminRTI = Nothing
    Dim electionInnerScreenAdminCoC As ElectionInnerScreenAdminCoC = Nothing
    Dim electionInnerScreenAdminViolation As ElectionInnerScreenAdminViolation = Nothing

    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles Panel1.Click
        electionInnerScreenAdminNomination?.Dispose()
        electionInnerScreenAdminNomination = New ElectionInnerScreenAdminNomination With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenAdminNomination)
    End Sub

    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        electionInnerScreenAdminTimeline?.Dispose()
        electionInnerScreenAdminTimeline = New ElectionInnerScreenAdminTimeline With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenAdminTimeline)
    End Sub

    Private Sub Panel3_Click(sender As Object, e As EventArgs) Handles Panel3.Click
        electionInnerScreenAdminVotes?.Dispose()
        electionInnerScreenAdminVotes = New ElectionInnerScreenAdminVotes With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenAdminVotes)
    End Sub

    Private Sub Panel4_Click(sender As Object, e As EventArgs) Handles Panel4.Click
        electionInnerScreenAdminResults?.Dispose()
        electionInnerScreenAdminResults = New ElectionInnerScreenAdminResults With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenAdminResults)
    End Sub

    Private Sub Panel5_Click(sender As Object, e As EventArgs) Handles Panel5.Click
        electionInnerScreenAdminRTI?.Dispose()
        electionInnerScreenAdminRTI = New ElectionInnerScreenAdminRTI With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenAdminRTI)
    End Sub

    Private Sub Panel6_Click(sender As Object, e As EventArgs) Handles Panel6.Click
        electionInnerScreenAdminCoC?.Dispose()
        electionInnerScreenAdminCoC = New ElectionInnerScreenAdminCoC With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenAdminCoC)
    End Sub

    Private Sub Panel7_Click(sender As Object, e As EventArgs) Handles Panel7.Click
        electionInnerScreenAdminViolation?.Dispose()
        electionInnerScreenAdminViolation = New ElectionInnerScreenAdminViolation With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenAdminViolation)
    End Sub

    Private Sub ElectionInnerScreenAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class