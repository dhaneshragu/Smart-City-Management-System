Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Service_Post
    Public Property uid As Integer = 1
    Public Property u_name As String = "Ashish Bharti"
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Service_Post_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        If String.IsNullOrEmpty(TextBox1.Text) OrElse
        String.IsNullOrEmpty(TextBox2.Text) OrElse
        String.IsNullOrEmpty(TextBox3.Text) OrElse
        String.IsNullOrEmpty(TextBox4.Text) OrElse
        String.IsNullOrEmpty(TextBox5.Text) OrElse
        String.IsNullOrEmpty(ComboBox1.Text) Then
            MessageBox.Show("Please fill in all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub ' Exit the event handler if any input field is null or empty
        End If

        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "INSERT INTO service_desc
                                        VALUES (@userID,@description,@offered,
                                           @department,
                                            @charge, 
                                        @startTime,
                                        @endTime);"
                Dim starthourInput As String = TextBox5.Text
                Dim endhourInput As String = TextBox4.Text
                ' Parse the input string to a double value
                Dim starthour As Double = Double.Parse(starthourInput)
                Dim endhour As Double = Double.Parse(endhourInput)
                ' Convert the hour value to a TimeSpan object
                Dim timeSpan1 As String = TimeSpan.FromHours(starthour).ToString("hh\:mm\:ss")
                Dim timeSpan2 As String = TimeSpan.FromHours(endhour).ToString("hh\:mm\:ss")
                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@userID", uid)
                    cmd.Parameters.AddWithValue("@description", TextBox1.Text)
                    cmd.Parameters.AddWithValue("@offered", TextBox2.Text)
                    cmd.Parameters.AddWithValue("@department", ComboBox1.Text)
                    cmd.Parameters.AddWithValue("@charge", TextBox3.Text)
                    cmd.Parameters.AddWithValue("@startTime", timeSpan1)
                    cmd.Parameters.AddWithValue("@endTime", timeSpan2)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Service Posted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                End Using

                ' Insert a single record with all values set to 0
                Dim insertSql As String = $"INSERT INTO service_leave (user_id, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday) VALUES (@selectedUserID, 0, 0, 0, 0, 0, 0, 0)"

                Using cmd As New MySqlCommand(insertSql, con)
                    cmd.Parameters.AddWithValue("@selectedUserID", uid)
                    cmd.ExecuteNonQuery()
                End Using

            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        ComboBox1.Text = ""
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs)
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        ComboBox1.Text = ""
    End Sub
End Class