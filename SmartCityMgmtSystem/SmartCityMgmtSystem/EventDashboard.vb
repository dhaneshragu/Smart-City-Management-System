Public Class EventDashboard

    Public Property uid As Integer
    Public Property u_name As String

    Private Sub EventDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label3.Text = uid
    End Sub



    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) 
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        ' Check if the form is already open
        'EventRegistrationScreen?.Dispose()

        ' Create a new instance of the form
        Dim EventRegistrationScreen = New EventRegistrationScreen With {
            .uid = uid,
            .u_name = u_name
        }

        Globals.viewChildForm(childformPanel, EventRegistrationScreen)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        ' Check if the form is already open
        'EventLoginWindow?.Dispose()

        ' Create a new instance of the form
        Dim EventLoginWindow = New EventLoginWindow With {
            .uid = uid,
            .u_name = u_name
        }

        Globals.viewChildForm(childformPanel, EventLoginWindow)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim EventVendorRegistrationScreen = New EventVendorRegistrationScreen With {
            .uid = uid,
            .u_name = u_name
        }

        Globals.viewChildForm(childformPanel, EventVendorRegistrationScreen)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Create a new instance of the form
        Dim EventVendorLoginWindow = New EventVendorLoginWindow With {
            .uid = uid,
            .u_name = u_name
        }

        Globals.viewChildForm(childformPanel, EventVendorLoginWindow)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        HomePageDashboard.Dispose()

        Dim home = New HomePageDashboard With {
            .uid = uid
        }
        home.Show()
        Me.Close()
    End Sub

    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub
End Class
