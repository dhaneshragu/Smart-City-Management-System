Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Common

Public Class UserProfilePage
    Private uid As Integer
    Dim imageBytes As Byte()

    Public Sub New(uid As Integer)
        InitializeComponent()
        Me.uid = uid ' Store the passed email ID in a private field
        Dim cmd As String = "SELECT * FROM users WHERE user_id = @uid"
        Dim conStr As String = Globals.getdbConnectionString()
        Using connection As New MySqlConnection(conStr)
            Using sqlCommand As New MySqlCommand(cmd, connection)
                sqlCommand.Parameters.AddWithValue("@uid", uid)
                ' Execute the query and retrieve the user ID
                connection.Open()
                Using reader As MySqlDataReader = sqlCommand.ExecuteReader()
                    If reader.Read() Then
                        TextBox1.Text = reader("name").ToString()
                        TextBox3.Text = reader("gender").ToString()
                        TextBox4.Text = reader("phone_number").ToString()
                        TextBox5.Text = reader("address").ToString()
                        If reader("occupation") IsNot Nothing AndAlso Not IsDBNull(reader("occupation")) Then
                            TextBox7.Text = reader("occupation").ToString()
                        End If
                        If reader("guardian_uid") IsNot Nothing AndAlso Not IsDBNull(reader("guardian_uid")) Then
                            TextBox8.Text = reader("guardian_uid").ToString()
                        End If
                        Dim res As Object = reader("profile_photo")
                        If res IsNot Nothing AndAlso Not IsDBNull(res) Then
                            ' Convert the byte array retrieved from the database to an Image
                            Dim imageBytes As Byte() = DirectCast(res, Byte())
                            Using ms As New MemoryStream(imageBytes)
                                PictureBox2.Image = Image.FromStream(ms)
                            End Using
                        End If

                        Dim dobString As String = reader("dob").ToString()
                        ' Parse the date string into a DateTime object
                        Dim dob As DateTime = DateTime.ParseExact(dobString, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                        ' Set the parsed date to the DateTimePicker control
                        DateTimePicker1.Value = dob.Date
                        'MessageBox.Show(dobString)
                        'Dim age As Integer = DateTime.Now.Year - dob.Year
                        TextBox2.Text = reader("age").ToString()
                    Else
                        MessageBox.Show("Invalid UID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                End Using
            End Using
        End Using
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim cmd As String
        Dim selectedDate As DateTime = DateTimePicker1.Value
        ' Format the selected date to match MySQL date format ("YYYY-MM-DD")
        Dim formattedDate As String = selectedDate.ToString("yyyy-MM-dd")
        Dim voter As Integer = 0
        Dim voted As Integer = 0
        cmd = "UPDATE users SET name = @name, dob = @dob, age = @age,
    profile_photo = @profile, gender = @gender, phone_number = @phno,
    occupation = @occupation, guardian_uid = @guid, address = @address 
    WHERE user_id = @uid"
        Dim conStr As String = Globals.getdbConnectionString()
        Using connection As New MySqlConnection(conStr)
            Using sqlCommand As New MySqlCommand(cmd, connection)
                sqlCommand.Parameters.AddWithValue("@uid", uid)
                sqlCommand.Parameters.AddWithValue("@name", TextBox1.Text)
                sqlCommand.Parameters.AddWithValue("@dob", formattedDate)
                sqlCommand.Parameters.AddWithValue("@age", TextBox2.Text)
                sqlCommand.Parameters.AddWithValue("@profile", imageBytes)
                sqlCommand.Parameters.AddWithValue("@gender", TextBox3.Text)
                sqlCommand.Parameters.AddWithValue("@phno", TextBox4.Text)
                sqlCommand.Parameters.AddWithValue("@address", TextBox5.Text)
                sqlCommand.Parameters.AddWithValue("@occupation", TextBox7.Text)
                sqlCommand.Parameters.AddWithValue("@guid", TextBox8.Text)

                Try
                    connection.Open()
                    sqlCommand.ExecuteNonQuery()
                    MessageBox.Show("User details updated successfully.")
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        OpenFileDialog1.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Get the selected file path
            Dim imagePath As String = OpenFileDialog1.FileName
            ' Display the selected image in a PictureBox control
            PictureBox2.Image = Image.FromFile(imagePath)
            ' Convert the image to a byte array
            imageBytes = File.ReadAllBytes(imagePath)

        End If
    End Sub
End Class