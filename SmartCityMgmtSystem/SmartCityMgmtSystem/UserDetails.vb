Imports System.Globalization
Imports System.IO
Imports System.Security.Cryptography
Imports MySql.Data.MySqlClient

Public Class UserDetails
    Private ReadOnly email As String
    Private ReadOnly password As String
    Dim imageBytes As Byte()
    Dim age As Integer = 0

    Public Sub New(email As String, password As String)
        InitializeComponent()
        Me.email = email ' Store the passed email ID in a private field
        Me.password = password
    End Sub
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox2.Text = 0
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

    Private Sub SaveDetails_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim cmd As String
        Dim selectedDate As DateTime = DateTimePicker1.Value
        ' Format the selected date to match MySQL date format ("YYYY-MM-DD")
        Dim formattedDate As String = selectedDate.ToString("yyyy-MM-dd")

        Dim voter As Integer = 0
        Dim voted As Integer = 0
        If String.IsNullOrEmpty(TextBox1.Text) Then
            MessageBox.Show("Please enter your full name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf ComboBox1.SelectedItem Is Nothing Then
            MessageBox.Show("Please select your gender.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComboBox1.Focus() ' Set focus to the ComboBox
        ElseIf String.IsNullOrEmpty(TextBox4.Text) Then
            MessageBox.Show("Please enter your phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf TextBox4.Text.Length <> 10 Then
            MessageBox.Show("Please enter a valid 10-digit phone number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf String.IsNullOrEmpty(TextBox5.Text) Then
            MessageBox.Show("Please enter your address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim gender As String = ComboBox1.SelectedItem.ToString()
            cmd = "INSERT INTO users (name, email, dob, age, profile_photo, gender, password,
            phone_number, occupation, voter, voted, address) 
            VALUES (@name, @email, @dob, @age, @profile, @gender, @pass,
            @phno, @occupation, @voter, @voted, @address)"
            Dim conStr As String = Globals.getdbConnectionString()
            Using connection As New MySqlConnection(conStr)
                Using sqlCommand As New MySqlCommand(cmd, connection)
                    sqlCommand.Parameters.AddWithValue("@name", TextBox1.Text)
                    sqlCommand.Parameters.AddWithValue("@email", email)
                    sqlCommand.Parameters.AddWithValue("@dob", formattedDate)
                    sqlCommand.Parameters.AddWithValue("@age", age)
                    sqlCommand.Parameters.AddWithValue("@profile", imageBytes)
                    sqlCommand.Parameters.AddWithValue("@gender", gender)
                    sqlCommand.Parameters.AddWithValue("@pass", password)
                    sqlCommand.Parameters.AddWithValue("@phno", TextBox4.Text)
                    sqlCommand.Parameters.AddWithValue("@address", TextBox5.Text)
                    sqlCommand.Parameters.AddWithValue("@occupation", TextBox7.Text)
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
                        MessageBox.Show("Your Unique Identification number is: " + uid.ToString)
                        home.Show()
                        Me.Close()
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message)
                    End Try
                End Using
            End Using
        End If
        'Dim success As Boolean = Globals.ExecuteInsertQuery(cmd)
        'Dim home = New HomePageDashboard()
        'home.Show()
        'Me.Close()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Dim selectedDate As DateTime = DateTimePicker1.Value
        ' Format the selected date to match MySQL date format ("YYYY-MM-DD")
        Dim formattedDate As String = selectedDate.ToString("yyyy-MM-dd")
        Dim birthDate As Date = DateTime.ParseExact(formattedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
        ' Get the current date
        Dim currentDate As Date = Date.Now
        ' Calculate the age
        age = currentDate.Year - birthDate.Year
        If birthDate.Month > currentDate.Month Then
            age -= 1
        End If
        TextBox2.Text = age.ToString
    End Sub
End Class
