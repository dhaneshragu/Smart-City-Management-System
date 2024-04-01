Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class EventLoginWindow

    Public Property uid As Integer
    Public Property u_name As String


    Private Sub EventLoginWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = uid
        TextBox2.PasswordChar = "*"
    End Sub

    Private Function CheckCredentials(ByVal CustomerID As Integer, ByVal Password As String) As Boolean
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try



        Dim query As String = "SELECT COUNT(*) FROM eventBookings WHERE customerID = @CustomerID AND password = @Password"
        cmd = New MySqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@CustomerID", CustomerID)
        cmd.Parameters.AddWithValue("@Password", Password)

        Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

        Return count > 0

    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim CustomerID As Integer = TextBox1.Text
        Dim Password As String = TextBox2.Text

        If CheckCredentials(CustomerID, Password) Then
            ' Record exists, so you can pass parameters to another form
            Dim EventCustomerScreenForm As New EventCustomerScreen With {
                .uid = CustomerID,
                .password = Password
            }
            Me.ParentForm.Close()

            Dim EventDashboard As New EventDashboard With {
                .uid = CustomerID,
                .u_name = u_name
            }
            EventDashboard.Show()
            Globals.viewChildForm(EventDashboard.childformPanel, EventCustomerScreenForm)
            Me.Close()
        Else
            MessageBox.Show("Invalid CustomerID or Password")
        End If




    End Sub
End Class