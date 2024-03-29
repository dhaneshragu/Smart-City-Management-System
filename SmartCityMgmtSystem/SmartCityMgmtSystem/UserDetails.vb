Imports System.IO
Imports System.Security.Cryptography
Imports MySql.Data.MySqlClient

Public Class UserDetails
    Private ReadOnly email As String
    Private ReadOnly password As String
    Dim imageBytes As Byte()

    Public Sub New(email As String, password As String)
        InitializeComponent()
        Me.email = email ' Store the passed email ID in a private field
        Me.password = password
    End Sub
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

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

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim cmd As String
        Dim selectedDate As DateTime = DateTimePicker1.Value
        ' Format the selected date to match MySQL date format ("YYYY-MM-DD")
        Dim formattedDate As String = selectedDate.ToString("yyyy-MM-dd")
        Dim voter As Integer = 0
        Dim voted As Integer = 0
        Dim gid As Integer? = Nothing
        If Not String.IsNullOrEmpty(TextBox8.Text) Then
            gid = Convert.ToInt32(TextBox8.Text)
        End If
        cmd = "INSERT INTO users (name, email, dob, age, profile_photo, gender, password,
            phone_number, occupation, guardian_uid, voter, voted, address) 
            VALUES (@name, @email, @dob, @age, @profile, @gender, @pass,
            @phno, @occupation, @guid, @voter, @voted, @address)"
        Dim conStr As String = Globals.getdbConnectionString()
        Using connection As New MySqlConnection(conStr)
            Using sqlCommand As New MySqlCommand(cmd, connection)
                sqlCommand.Parameters.AddWithValue("@name", TextBox1.Text)
                sqlCommand.Parameters.AddWithValue("@email", email)
                sqlCommand.Parameters.AddWithValue("@dob", formattedDate)
                sqlCommand.Parameters.AddWithValue("@age", Convert.ToInt32(TextBox2.Text))
                sqlCommand.Parameters.AddWithValue("@profile", imageBytes)
                sqlCommand.Parameters.AddWithValue("@gender", TextBox3.Text)
                sqlCommand.Parameters.AddWithValue("@pass", password)
                sqlCommand.Parameters.AddWithValue("@phno", TextBox4.Text)
                sqlCommand.Parameters.AddWithValue("@address", TextBox5.Text)
                sqlCommand.Parameters.AddWithValue("@occupation", TextBox7.Text)
                sqlCommand.Parameters.AddWithValue("@guid", gid)
                sqlCommand.Parameters.AddWithValue("@voter", voter)
                sqlCommand.Parameters.AddWithValue("@voted", voted)

                Try
                    connection.Open()
                    Dim uid As Integer = -1
                    sqlCommand.ExecuteNonQuery()
                    MessageBox.Show("User details saved successfully.")
                    Dim q As String = "SELECT user_id FROM users WHERE email = @email"

                    Using getUid As New MySqlCommand(q, connection)
                        getUid.Parameters.AddWithValue("@email", email)
                        Dim result = getUid.ExecuteScalar()

                        If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                            uid = Convert.ToInt32(result)
                        End If
                    End Using
                    Dim home = New HomePageDashboard With {
                        .uid = uid
                    }
                    home.Show()
                        Me.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using
        'Dim success As Boolean = Globals.ExecuteInsertQuery(cmd)
        'Dim home = New HomePageDashboard()
        'home.Show()
        'Me.Close()
    End Sub
End Class
