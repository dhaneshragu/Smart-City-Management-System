Public Class Employment_portal
    Public uid As Integer
    Public u_name As String
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Employment Portal"
        Label2.Text = u_name
        Label3.Text = uid
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        'Globals.viewChildForm(childformPanel, Employment_portal_search)
        Me.Hide()

        ' Create an instance of employment_portal_search.vb and display it
        Dim employmentPortalSearchForm As New Employment_portal_search() With {
            .uid = uid,
            .u_name = u_name
        }
        employmentPortalSearchForm.Show()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, Employment_portal_apply)
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, Employment_portal_applicationstatus)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()

        Dim employmentadmin = New Employment_portal_admin() With {
            .uid = uid,
            .u_name = u_name
        }
        employmentadmin.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()

        Dim homepage = New HomePage() With {
            .uid = uid,
            .u_name = u_name
        }
        homepage.Show()
    End Sub
End Class
