Public Class EventDashboard

    Public Property uid As Integer = 1121205
    Public Property u_name As String = "Abhi"

    Private Sub EventDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label3.Text = uid
    End Sub



    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim EventRegistrationScreenForm = New EventRegistrationScreen
        ' Check if the form is already open
        EventRegistrationScreenForm?.Dispose()

        ' Create a new instance of the form
        EventRegistrationScreenForm = New EventRegistrationScreen With {
            .uid = uid,
            .u_name = u_name
        }

        Globals.viewChildForm(childformPanel, EventRegistrationScreenForm)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Globals.viewChildForm(childformPanel, EventVendorRegistrationScreen)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim home = New HomePageDashboard With {
            .uid = uid
        }
        home.Show()
        Me.Close()
    End Sub
End Class
