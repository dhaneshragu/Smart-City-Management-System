Imports MySql.Data.MySqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class HomePageDashboard
    Public Property uid As Integer
    Dim nm As String
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim cmd As String = "SELECT name FROM users WHERE user_id = @uid"
        Dim conStr As String = Globals.getdbConnectionString()
        Using connection As New MySqlConnection(conStr)
            Using sqlCommand As New MySqlCommand(cmd, connection)
                sqlCommand.Parameters.AddWithValue("@uid", uid)
                ' Execute the query and retrieve the user ID
                connection.Open()
                Dim result = sqlCommand.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    nm = Convert.ToString(result)
                End If
            End Using
        End Using
        Label3.Text = uid
        Label2.Text = nm
    End Sub
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        'MessageBox.Show(nm)
        Dim HomePageForm = New HomePage With {
            .uid = uid,
            .u_name = nm
        }
        Globals.viewChildForm(childformPanel, HomePageForm)
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Dim upp As New UserProfilePage(uid)
        Globals.viewChildForm(childformPanel, upp)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim login As New UserLogin()
        login.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim notif As New NotificationInnerScreen With {
            .uid = uid
        }
        Globals.viewChildForm(childformPanel, notif)
    End Sub

    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub
End Class
