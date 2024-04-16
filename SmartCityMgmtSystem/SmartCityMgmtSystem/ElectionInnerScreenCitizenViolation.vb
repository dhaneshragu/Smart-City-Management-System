Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class ElectionInnerScreenCitizenViolation
    Public Property LastElectionIDpass As Integer
    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Public Property innerPanel As Panel
    Dim electionInnerScreen1 As ElectionInnerScreen1 = Nothing
    Dim electionInnerScreenCitizenViolationPR As ElectionInnerScreenCitizenViolationPR = Nothing

    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        helpOptionsAdd()
        ComboBox1.SelectedIndex = -1
        RichTextBox1.Text = ""
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

        populateComboBox(lastElectionID)
        LastElectionIDpass = lastElectionID
        MessageBox.Show("All nominated and approved candidates are provided in the drop down list. You can now report a violation against anyone of them.")

    End Sub

    Private Sub populateComboBox(ByVal electionId As Integer)
        ' Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
            Dim query As String = "SELECT candidate_uid, name
                                    FROM candidate_register
                                    JOIN users ON users.user_id = candidate_register.candidate_uid
                                    WHERE election_id = @electionID and status = 'Approved';"
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@electionId", electionId)
            reader = cmd.ExecuteReader()

            While reader.Read()
                Dim userId As String = reader("candidate_uid").ToString() ' Get user_id
                Dim name As String = reader("name").ToString() ' Get name
                Dim itemString As String = userId & " - " & name ' Concatenate user_id and name into one string
                ComboBox1.Items.Add(itemString) ' Add concatenated string to ComboBox
            End While

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

    Private Function InsertQuery(query As String) As Boolean
        Using conn As MySqlConnection = Globals.GetDBConnection()
            Try
                conn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Your report has been filed.", "Reporting Violations", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        Dim selectedValue As Object = ComboBox1.SelectedItem
        If selectedValue Is Nothing Then
            MessageBox.Show("Please select the candidate against whom you wish to report violation.")
            Exit Sub
        End If

        If RichTextBox1.Text = "" Then
            MessageBox.Show("Please write the violation.")
            Exit Sub
        End If

        Dim selectedItem As String = ComboBox1.SelectedItem.ToString()
        Dim parts() As String = selectedItem.Split("-"c) ' Split the selected item by the hyphen
        Dim candidate_uid As String = parts(0).Trim() ' Get the first entity
        Dim candidate_name As String = parts(1).Trim() ' Get the second entity

        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Retrieve the value of the election_id column from the last row of the election_time table
        Dim lastElectionID As Integer = 0 'Default value in case there are no rows in election_time
        cmd = New MySqlCommand("SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            lastElectionID = Convert.ToInt32(reader("election_id"))
        End If
        reader.Close()

        Dim lastReportID As Integer = 0
        cmd = New MySqlCommand("Select COUNT(*) as count FROM violations_reported;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            lastReportID = Convert.ToInt32(reader("count"))
        End If
        reader.Close()
        lastReportID = lastReportID + 1

        Dim insertReportQuery = "INSERT INTO violations_reported VALUES(" & lastElectionID & "," & lastReportID & "," & uid &
                                "," & candidate_uid & "," & """" & RichTextBox1.Text.ToString & """," & " '' );"

        InsertQuery(insertReportQuery)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        electionInnerScreenCitizenViolationPR?.Dispose()
        electionInnerScreenCitizenViolationPR = New ElectionInnerScreenCitizenViolationPR With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenCitizenViolationPR)
    End Sub
End Class