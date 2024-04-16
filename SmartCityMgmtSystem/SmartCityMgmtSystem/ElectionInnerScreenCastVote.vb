Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports Mysqlx
Public Class ElectionInnerScreenCastVote

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Public Property innerPanel As Panel
    Dim electionInnerScreen1 As ElectionInnerScreen1 = Nothing
    Dim current_election_id As Integer
    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        helpOptionsAdd()
    End Sub

    Private Sub helpOptionsAdd()
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Retrieve the value of the election_id column from the last row of the election_time table
        Dim lastElectionID As Integer = -1 ' Default value in case there are no rows in election_time
        cmd = New MySqlCommand("SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            lastElectionID = Convert.ToInt32(reader("election_id"))
        End If
        reader.Close()

        current_election_id = lastElectionID

        For index As Integer = 1 To 9
            OptionsAdd(lastElectionID, index)
        Next

        MessageBox.Show("All options are loaded. You can now vote.")

    End Sub

    Private Sub OptionsAdd(ByVal electionId As Integer, ByVal ministryID As Integer)
        ' Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
            Dim query As String = "SELECT user_id, name 
                            FROM candidate_register 
                            JOIN users ON users.user_id = candidate_register.candidate_uid
                            WHERE election_id = @electionId AND ministry_id = @ministryID AND status = 'Approved';"
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@electionId", electionId)
            cmd.Parameters.AddWithValue("@ministryID", ministryID)
            reader = cmd.ExecuteReader()

            Select Case ministryID
                Case 1 To 9 ' Check all cases from 1 to 9
                    Dim comboBoxIndex As Integer = ministryID ' Determine the ComboBox index based on ministryID
                    Dim comboBox As ComboBox = CType(Me.Controls("ComboBox" & comboBoxIndex.ToString()), ComboBox) ' Get the ComboBox by index
                    comboBox.Items.Clear() ' Clear ComboBox items
                    While reader.Read()
                        Dim userId As String = reader("user_id").ToString() ' Get user_id
                        Dim name As String = reader("name").ToString() ' Get name
                        Dim itemString As String = userId & " - " & name ' Concatenate user_id and name into one string
                        comboBox.Items.Add(itemString) ' Add concatenated string to ComboBox
                    End While
                    reader.Close()
            End Select

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close the database connection
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        electionInnerScreen1?.Dispose()
        electionInnerScreen1 = New ElectionInnerScreen1 With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreen1)
    End Sub
    Private Function UpdateQuery(query As String) As Boolean

        Using conn As MySqlConnection = Globals.GetDBConnection()
            Try
                conn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                    'MessageBox.Show("Row updated successfully.", "Update Entry", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return True
                End Using
            Catch ex As Exception
                MessageBox.Show("Error executing query: " & ex.Message, "Query Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Using
        Return False
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Dim voted As Integer = 0 ' Default value in case there are no rows in election_time
        Dim voterQuery As String = "SELECT voted FROM users WHERE user_id = " & ElectionDashboard.uid & " ;"
        cmd = New MySqlCommand(voterQuery, Con)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()
        If reader.Read() Then
            voted = Convert.ToInt32(reader("voted"))
        End If
        reader.Close()

        If voted = 1 Then
            MessageBox.Show("You have already voted.")
            Exit Sub
        End If

        If ComboBox1.SelectedIndex <> -1 Then
            Dim selectedItem As String = ComboBox1.SelectedItem.ToString()
            Dim parts() As String = selectedItem.Split("-"c) ' Split the selected item by the hyphen
            Dim selectedEntity1 As String = parts(0).Trim() ' Get the first entity
            Dim selectedEntity2 As String = parts(1).Trim() ' Get the second entity
            'MessageBox.Show("Selected Entity1: " & selectedEntity1 & vbCrLf & "Selected Entity2: " & selectedEntity2)

            Dim updateCandidateVotes As String = "UPDATE candidate_register SET votes = votes + 1 WHERE election_id = " & current_election_id & " AND candidate_uid = " & selectedEntity1 & ";"
            Dim updateTurnout As String = "UPDATE turnout SET votes_received = votes_received + 1 WHERE election_id = " & current_election_id & " AND ministry_id = 1;"

            UpdateQuery(updateCandidateVotes)
            UpdateQuery(updateTurnout)

        End If

        If ComboBox2.SelectedIndex <> -1 Then
            Dim selectedItem As String = ComboBox2.SelectedItem.ToString()
            Dim parts() As String = selectedItem.Split("-"c) ' Split the selected item by the hyphen
            Dim selectedEntity1 As String = parts(0).Trim() ' Get the first entity
            Dim selectedEntity2 As String = parts(1).Trim() ' Get the second entity
            'MessageBox.Show("Selected Entity1: " & selectedEntity1 & vbCrLf & "Selected Entity2: " & selectedEntity2

            Dim updateCandidateVotes As String = "UPDATE candidate_register SET votes = votes + 1 WHERE election_id = " & current_election_id & " AND candidate_uid = " & selectedEntity1 & ";"
            Dim updateTurnout As String = "UPDATE turnout SET votes_received = votes_received + 1 WHERE election_id = " & current_election_id & " AND ministry_id = 2;"

            UpdateQuery(updateCandidateVotes)
            UpdateQuery(updateTurnout)

        End If

        If ComboBox3.SelectedIndex <> -1 Then
            Dim selectedItem As String = ComboBox3.SelectedItem.ToString()
            Dim parts() As String = selectedItem.Split("-"c) ' Split the selected item by the hyphen
            Dim selectedEntity1 As String = parts(0).Trim() ' Get the first entity
            Dim selectedEntity2 As String = parts(1).Trim() ' Get the second entity
            'MessageBox.Show("Selected Entity1: " & selectedEntity1 & vbCrLf & "Selected Entity2: " & selectedEntity2

            Dim updateCandidateVotes As String = "UPDATE candidate_register SET votes = votes + 1 WHERE election_id = " & current_election_id & " AND candidate_uid = " & selectedEntity1 & ";"
            Dim updateTurnout As String = "UPDATE turnout SET votes_received = votes_received + 1 WHERE election_id = " & current_election_id & " AND ministry_id = 3;"

            UpdateQuery(updateCandidateVotes)
            UpdateQuery(updateTurnout)

        End If


        If ComboBox4.SelectedIndex <> -1 Then
            Dim selectedItem As String = ComboBox4.SelectedItem.ToString()
            Dim parts() As String = selectedItem.Split("-"c) ' Split the selected item by the hyphen
            Dim selectedEntity1 As String = parts(0).Trim() ' Get the first entity
            Dim selectedEntity2 As String = parts(1).Trim() ' Get the second entity
            'MessageBox.Show("Selected Entity1: " & selectedEntity1 & vbCrLf & "Selected Entity2: " & selectedEntity2

            Dim updateCandidateVotes As String = "UPDATE candidate_register SET votes = votes + 1 WHERE election_id = " & current_election_id & " AND candidate_uid = " & selectedEntity1 & ";"
            Dim updateTurnout As String = "UPDATE turnout SET votes_received = votes_received + 1 WHERE election_id = " & current_election_id & " AND ministry_id = 4;"

            UpdateQuery(updateCandidateVotes)
            UpdateQuery(updateTurnout)

        End If


        If ComboBox5.SelectedIndex <> -1 Then
            Dim selectedItem As String = ComboBox5.SelectedItem.ToString()
            Dim parts() As String = selectedItem.Split("-"c) ' Split the selected item by the hyphen
            Dim selectedEntity1 As String = parts(0).Trim() ' Get the first entity
            Dim selectedEntity2 As String = parts(1).Trim() ' Get the second entity
            'MessageBox.Show("Selected Entity1: " & selectedEntity1 & vbCrLf & "Selected Entity2: " & selectedEntity2

            Dim updateCandidateVotes As String = "UPDATE candidate_register SET votes = votes + 1 WHERE election_id = " & current_election_id & " AND candidate_uid = " & selectedEntity1 & ";"
            Dim updateTurnout As String = "UPDATE turnout SET votes_received = votes_received + 1 WHERE election_id = " & current_election_id & " AND ministry_id = 5;"

            UpdateQuery(updateCandidateVotes)
            UpdateQuery(updateTurnout)

        End If

        If ComboBox6.SelectedIndex <> -1 Then
            Dim selectedItem As String = ComboBox6.SelectedItem.ToString()
            Dim parts() As String = selectedItem.Split("-"c) ' Split the selected item by the hyphen
            Dim selectedEntity1 As String = parts(0).Trim() ' Get the first entity
            Dim selectedEntity2 As String = parts(1).Trim() ' Get the second entity
            'MessageBox.Show("Selected Entity1: " & selectedEntity1 & vbCrLf & "Selected Entity2: " & selectedEntity2

            Dim updateCandidateVotes As String = "UPDATE candidate_register SET votes = votes + 1 WHERE election_id = " & current_election_id & " AND candidate_uid = " & selectedEntity1 & ";"
            Dim updateTurnout As String = "UPDATE turnout SET votes_received = votes_received + 1 WHERE election_id = " & current_election_id & " AND ministry_id = 6;"

            UpdateQuery(updateCandidateVotes)
            UpdateQuery(updateTurnout)

        End If

        If ComboBox7.SelectedIndex <> -1 Then
            Dim selectedItem As String = ComboBox7.SelectedItem.ToString()
            Dim parts() As String = selectedItem.Split("-"c) ' Split the selected item by the hyphen
            Dim selectedEntity1 As String = parts(0).Trim() ' Get the first entity
            Dim selectedEntity2 As String = parts(1).Trim() ' Get the second entity
            'MessageBox.Show("Selected Entity1: " & selectedEntity1 & vbCrLf & "Selected Entity2: " & selectedEntity2

            Dim updateCandidateVotes As String = "UPDATE candidate_register SET votes = votes + 1 WHERE election_id = " & current_election_id & " AND candidate_uid = " & selectedEntity1 & ";"
            Dim updateTurnout As String = "UPDATE turnout SET votes_received = votes_received + 1 WHERE election_id = " & current_election_id & " AND ministry_id = 7;"

            UpdateQuery(updateCandidateVotes)
            UpdateQuery(updateTurnout)

        End If

        If ComboBox8.SelectedIndex <> -1 Then
            Dim selectedItem As String = ComboBox8.SelectedItem.ToString()
            Dim parts() As String = selectedItem.Split("-"c) ' Split the selected item by the hyphen
            Dim selectedEntity1 As String = parts(0).Trim() ' Get the first entity
            Dim selectedEntity2 As String = parts(1).Trim() ' Get the second entity
            'MessageBox.Show("Selected Entity1: " & selectedEntity1 & vbCrLf & "Selected Entity2: " & selectedEntity2

            Dim updateCandidateVotes As String = "UPDATE candidate_register SET votes = votes + 1 WHERE election_id = " & current_election_id & " AND candidate_uid = " & selectedEntity1 & ";"
            Dim updateTurnout As String = "UPDATE turnout SET votes_received = votes_received + 1 WHERE election_id = " & current_election_id & " AND ministry_id = 8;"

            UpdateQuery(updateCandidateVotes)
            UpdateQuery(updateTurnout)

        End If

        If ComboBox9.SelectedIndex <> -1 Then
            Dim selectedItem As String = ComboBox9.SelectedItem.ToString()
            Dim parts() As String = selectedItem.Split("-"c) ' Split the selected item by the hyphen
            Dim selectedEntity1 As String = parts(0).Trim() ' Get the first entity
            Dim selectedEntity2 As String = parts(1).Trim() ' Get the second entity
            'MessageBox.Show("Selected Entity1: " & selectedEntity1 & vbCrLf & "Selected Entity2: " & selectedEntity2

            Dim updateCandidateVotes As String = "UPDATE candidate_register SET votes = votes + 1 WHERE election_id = " & current_election_id & " AND candidate_uid = " & selectedEntity1 & ";"
            Dim updateTurnout As String = "UPDATE turnout SET votes_received = votes_received + 1 WHERE election_id = " & current_election_id & " AND ministry_id = 9;"

            UpdateQuery(updateCandidateVotes)
            UpdateQuery(updateTurnout)

        End If

        MessageBox.Show("All your votes have been updated in the database. Thanks for voting!")

        Dim updateVoted As String = "UPDATE users SET voted = 1 WHERE user_id = " & ElectionDashboard.uid & ";"
        UpdateQuery(updateVoted)

        MessageBox.Show("You have voted!")

        electionInnerScreen1?.Dispose()
        electionInnerScreen1 = New ElectionInnerScreen1 With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreen1)

    End Sub
End Class