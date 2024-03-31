Imports MySql.Data.MySqlClient

Public Class Employment_portal_admin_usercredentials

    Public Property userid As Integer

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

        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT * from users
            WHERE user_id = @userid"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@userid", userid)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            MessageBox.Show("Read Success")
                            user_id.Text = reader.GetString("user_id")
                            username.Text = reader.GetString("name")
                            email.Text = reader.GetString("email")
                            age.Text = reader.GetString("age")
                            gender.Text = reader.GetString("gender")
                            phone_number.Text = reader.GetString("phone_number")


                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub

End Class
