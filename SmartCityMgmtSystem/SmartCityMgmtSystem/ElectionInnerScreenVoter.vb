Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreenVoter
    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreen1)
    End Sub

    Private Function CompareUserInfo(ByVal name As String, ByVal uid As Integer, ByVal age As Integer, ByVal house_number As Integer, ByVal ward_number As Integer) As Boolean
        ' Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand
        Dim query As String = "SELECT name, age, house_number, ward_number FROM users WHERE user_id = @uid"

        Dim matchFound As Boolean = False

        Try
            Con.Open()
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@uid", uid)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then ' Row with the given uid found
                Dim db_name As String = reader("name").ToString()
                Dim db_age As Integer = Convert.ToInt32(reader("age"))
                Dim db_house_number As Integer = Convert.ToInt32(reader("house_number"))
                Dim db_ward_number As Integer = Convert.ToInt32(reader("ward_number"))

                'MessageBox.Show(db_name + "," & db_age & "," & db_house_number & "," & db_ward_number)
                'MessageBox.Show(name + "," & age & "," & house_number & "," & ward_number)

                ' Compare the retrieved values with the provided values
                If name = db_name AndAlso age = db_age AndAlso house_number = db_house_number AndAlso ward_number = db_ward_number Then
                    matchFound = True
                End If
            End If
        Catch ex As Exception
            ' Handle exceptions (e.g., display error message)
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close the connection
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try

        Return matchFound
    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' Check if the checkbox is checked
        If CheckBox1.Checked Then
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
                MessageBox.Show("Please enter all details.")
                Exit Sub
            End If

            Dim name As String = TextBox1.Text.ToString
            Dim uid As Integer = Integer.Parse(TextBox2.Text)
            Dim age As Integer = Integer.Parse(TextBox3.Text)
            Dim house_number As Integer = Integer.Parse(TextBox4.Text)
            Dim ward_number As Integer = Integer.Parse(TextBox5.Text)

            If ElectionDashboard.uid = uid Then
                Dim check As Boolean = CompareUserInfo(name, uid, age, house_number, ward_number)

                If check Then
                    If age >= 18 Then
                        Dim Con = Globals.GetDBConnection()
                        Dim cmd As MySqlCommand
                        Dim voter As Integer = 0 ' Variable to store the number of rows in election_time table
                        Try
                            Con.Open()

                            ' Execute query to count rows in election_time table
                            cmd = New MySqlCommand("SELECT voter FROM users WHERE user_id = @uid;", Con)
                            cmd.Parameters.AddWithValue("@uid", uid)
                            voter = Convert.ToInt32(cmd.ExecuteScalar())
                        Catch ex As Exception
                            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            ' Close the connection
                            If Con.State = ConnectionState.Open Then
                                Con.Close()
                            End If
                        End Try

                        If voter = 0 Then

                            Try
                                Con.Open()
                                cmd = New MySqlCommand("UPDATE users SET voter = 1 WHERE user_id = @uid;", Con)
                                cmd.Parameters.AddWithValue("@uid", uid)
                                cmd.ExecuteNonQuery()
                                MessageBox.Show("You have successfully registered as a voter!")
                            Catch ex As Exception
                                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Finally
                                ' Close the connection
                                If Con.State = ConnectionState.Open Then
                                    Con.Close()
                                End If
                            End Try
                        Else
                            MessageBox.Show("You have already registered as a voter.")
                        End If
                    Else
                        MessageBox.Show("You can't register as a voter as you are not a citizen above 18 years.")
                    End If
                Else
                    MessageBox.Show("The information that you have provided doesn't match with the information in our database. Please verify and try again later.")
                End If
            Else
                MessageBox.Show("You can only register yourselves. The UID entered doesn't match with your UID.")
            End If
        Else
            MessageBox.Show("Checkbox is not checked. Please check it and try again.")
        End If

    End Sub
End Class