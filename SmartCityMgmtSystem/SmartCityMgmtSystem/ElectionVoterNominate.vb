Imports System.Data.SqlClient
Imports System.IO
Imports MySql.Data.MySqlClient
Public Class ElectionVoterNominate
    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Public Property innerPanel As Panel
    Dim electionInnerScreen1 As ElectionInnerScreen1 = Nothing

    ' Create a new dictionary with Integer keys and String values
    Dim ministryToId As New Dictionary(Of String, Integer)

    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ministryToId.Add("Employment", 1)
        ministryToId.Add("Education", 2)
        ministryToId.Add("Health", 3)
        ministryToId.Add("Transport", 4)
        ministryToId.Add("Culture", 5)
        ministryToId.Add("Power", 6)
        ministryToId.Add("Finance", 7)
        ministryToId.Add("Broadcasting", 8)
        ministryToId.Add("IT", 9)
    End Sub

    'To execute a Insert Query returns True if succesful
    Private Function InsertQuery(query As String) As Boolean

        Using conn As MySqlConnection = Globals.GetDBConnection()
            Try
                conn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Your nomination has been filed successfully!", "Filing Nomination", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return True
                End Using
            Catch ex As Exception
                MessageBox.Show("Error executing query: " & ex.Message, "Query Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Using
        Return False
    End Function

    Private Function CompareUserInfo(ByVal uid As Integer, ByVal electionID As Integer) As Boolean
        ' Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand
        Dim query As String = "SELECT COUNT(*) AS count
                                FROM candidate_register
                                WHERE election_id = @election_id
                                AND candidate_uid = @candidate_uid;
                                "
        Dim matchFound As Boolean = False

        Try
            Con.Open()
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@candidate_uid", uid)
            cmd.Parameters.AddWithValue("@election_id", electionID)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then ' Row with the given uid found
                Dim count As Integer = Convert.ToInt32(reader("count"))

                'MessageBox.Show(db_name + "," & db_age & "," & db_house_number & "," & db_ward_number)
                'MessageBox.Show(name + "," & age & "," & house_number & "," & ward_number)

                ' Compare the retrieved values with the provided values
                If count = 0 Then
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        electionInnerScreen1?.Dispose()
        electionInnerScreen1 = New ElectionInnerScreen1 With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreen1)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("Fill all the details.")
            Exit Sub
        End If

        If TextBox2.Text = "" Then
            MessageBox.Show("Fill all the details.")
            Exit Sub
        End If

        If RichTextBox1.Text = "" Then
            MessageBox.Show("Fill all the details.")
            Exit Sub
        End If

        Dim position As String
        Dim selectedValue As Object = ComboBox1.SelectedItem
        If selectedValue IsNot Nothing Then
            position = selectedValue.ToString()
        Else
            MessageBox.Show("Select the position you want to contest for.")
        End If

        Dim citname As String = TextBox1.Text.ToString
        Dim cituid As String = TextBox2.Text.ToString
        Dim citagenda As String = RichTextBox1.Text.ToString

        If uid = cituid Then
            Dim Con = Globals.GetDBConnection()
            Dim cmd As MySqlCommand
            Dim election_id As Integer = 0 ' Variable to store the number of rows in election_time table

            Try
                Con.Open()
                ' Execute query to count rows in election_time table
                cmd = New MySqlCommand("SELECT COUNT(*) FROM election_time;", Con)
                election_id = Convert.ToInt32(cmd.ExecuteScalar())

                Dim check As Boolean = CompareUserInfo(Convert.ToInt32(cituid), election_id)

                If check Then
                    Dim updatequery As String = "INSERT INTO candidate_register(election_id, candidate_uid, ministry_id, agenda, status)
                                        VALUES(" & election_id & ", " & ElectionDashboard.uid & "," & ministryToId(position) &
                                            ",""" & citagenda & """, ""Pending"");"


                    InsertQuery(updatequery)
                Else
                    MessageBox.Show("You have already nominated yourself for this election.")
                End If

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ' Close the connection
                If Con.State = ConnectionState.Open Then
                    Con.Close()
                End If
            End Try

        Else
            MessageBox.Show("Sorry! The UID entered doesn't match with your UID. Please take note that you can only nominate yourselves.")
        End If



    End Sub
End Class