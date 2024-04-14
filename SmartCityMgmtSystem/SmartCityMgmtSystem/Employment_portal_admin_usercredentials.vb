Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Employment_portal_admin_usercredentials

    Public Property userid As Integer

    Public Sub Fill_Details(uid As Integer)
        'Me.userid = uid ' Store the passed email ID in a private field
        Dim cmd As String = "SELECT * FROM users WHERE user_id = @uid"
        Dim conStr As String = Globals.getdbConnectionString()
        Using connection As New MySqlConnection(conStr)
            Using sqlCommand As New MySqlCommand(cmd, connection)
                sqlCommand.Parameters.AddWithValue("@uid", uid)
                ' Execute the query and retrieve the user ID
                connection.Open()
                Using reader As MySqlDataReader = sqlCommand.ExecuteReader()
                    If reader.Read() Then
                        username.Text = reader("name").ToString()
                        If reader("gender") IsNot Nothing AndAlso Not IsDBNull(reader("gender")) Then
                            gender.Text = reader("gender").ToString()
                        End If
                        user_id.Text = uid
                        If reader("email") IsNot Nothing AndAlso Not IsDBNull(reader("email")) Then
                            email.Text = reader("email").ToString()
                        End If
                        If reader("phone_number") IsNot Nothing AndAlso Not IsDBNull(reader("phone_number")) Then
                            phone_number.Text = reader("phone_number").ToString()
                        End If
                        If reader("address") IsNot Nothing AndAlso Not IsDBNull(reader("address")) Then
                            address.Text = reader("address").ToString()
                        End If
                        Dim res As Object = reader("profile_photo")
                        If res IsNot Nothing AndAlso Not IsDBNull(res) Then
                            ' Convert the byte array retrieved from the database to an Image
                            Dim imageBytes As Byte() = DirectCast(res, Byte())
                            Using ms As New MemoryStream(imageBytes)
                                PictureBox1.Image = Image.FromStream(ms)
                            End Using
                        End If
                        If reader("age") IsNot Nothing AndAlso Not IsDBNull(reader("age")) Then
                            age.Text = reader("age").ToString()
                        End If
                    Else
                        MessageBox.Show("Invalid UID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                End Using
            End Using
        End Using
    End Sub

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

        Fill_Details(userid)

    End Sub

End Class
