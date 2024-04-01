Public Class lib_dash
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim lib_search = New lib_search() With {
            .uid = uid,
            .u_name = u_name
        }
        lib_search.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim lib_ebooks = New lib_ebooks() With {
            .uid = uid,
            .u_name = u_name
        }
        lib_ebooks.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim lib_borrowed = New lib_borrowed() With {
            .uid = uid,
            .u_name = u_name
        }
        lib_borrowed.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim lib_request = New lib_request() With {
            .uid = uid,
            .u_name = u_name
        }
        lib_request.Show()
        Me.Close()
    End Sub

    Private Sub lib_dash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = u_name
    End Sub
End Class