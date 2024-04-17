Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreenAdminResults

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Dim electionInnerScreenAdmin As ElectionInnerScreenAdmin = Nothing

    Public Property innerPanel As Panel

    Private Sub LoadandBindDataGridView()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand

        Try
            Con.Open()


            Dim lastElectionID As Integer = 0 ' Default value in case there are no rows in election_time
            Dim lastElectionIDQuery As String = "SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;"
            cmd = New MySqlCommand(lastElectionIDQuery, Con)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                lastElectionID = Convert.ToInt32(reader("election_id"))
            End If
            reader.Close()

            MessageBox.Show(lastElectionID)

            Dim updateCmd As New MySqlCommand("SELECT users.name, m.ministry_name, cr.votes, t.votes_received, cr.status
                                            FROM candidate_register cr
                                            JOIN ministries m ON cr.ministry_id = m.ministry_id
                                            JOIN turnout t ON cr.election_id = t.election_id AND cr.ministry_id = t.ministry_id
                                            JOIN users ON users.user_id = cr.candidate_uid
                                            JOIN (
                                                SELECT ministry_id, MAX(votes) AS max_votes
                                                FROM candidate_register
                                                WHERE election_id = @electionID and status = 'Approved'
                                                GROUP BY ministry_id
                                            ) max_votes_per_ministry ON cr.ministry_id = max_votes_per_ministry.ministry_id AND cr.votes = max_votes_per_ministry.max_votes
                                            WHERE cr.election_id = @electionID and cr.status = 'Approved';", Con)
            updateCmd.Parameters.AddWithValue("@electionID", lastElectionID)
            reader = updateCmd.ExecuteReader
            ' Create a DataTable to store the data
            Dim dataTable As New DataTable()

            'Fill the DataTable with data from the SQL table
            dataTable.Load(reader)
            reader.Close()
            Con.Close()

            'IMP: Specify the Column Mappings from DataGridView to SQL Table
            DataGridView1.AutoGenerateColumns = False
            DataGridView1.Columns(0).DataPropertyName = "ministry_name"
            DataGridView1.Columns(1).DataPropertyName = "name"
            DataGridView1.Columns(2).DataPropertyName = "votes"
            DataGridView1.Columns(3).DataPropertyName = "votes_received"

            ' Bind the data to DataGridView
            DataGridView1.DataSource = dataTable

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub
    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'For i As Integer = 1 To 8
        ' Add an empty row to the DataGridView
        'Dim' row As New DataGridViewRow()
        'DataGridView1.Rows.Add(row)

        ' Set values for the first three columns in the current row
        'DataGridView1.Rows(i - 1).Cells("Column1").Value = "DummyVal"
        'DataGridView1.Rows(i - 1).Cells("Column2").Value = "DummyVal"
        'DataGridView1.Rows(i - 1).Cells("Column3").Value = "DummyVal"
        'Next
        LoadandBindDataGridView()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        electionInnerScreenAdmin?.Dispose()
        electionInnerScreenAdmin = New ElectionInnerScreenAdmin With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenAdmin)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand

        Try
            Con.Open()

            Dim lastElectionID As Integer = 0 ' Default value in case there are no rows in election_time
            Dim lastElectionIDQuery As String = "SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;"
            cmd = New MySqlCommand(lastElectionIDQuery, Con)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                lastElectionID = Convert.ToInt32(reader("election_id"))
            End If
            reader.Close()

            If lastElectionID = 0 Then
                MessageBox.Show("No elections have been conducted yet. Conduct elections to release results.")
                Exit Sub
            End If

            Dim announced As Integer = 0 ' Default value in case there are no rows in election_time
            Dim announcedQuery As String = "SELECT announced FROM election_time ORDER BY election_id DESC LIMIT 1;"
            cmd = New MySqlCommand(lastElectionIDQuery, Con)
            reader = cmd.ExecuteReader()
            If reader.Read() Then
                lastElectionID = Convert.ToInt32(reader("election_id"))
            End If
            reader.Close()

            If announced = 0 Then
                Dim current_date As String = "2024-04-27"
                Dim resultAnnouncementDate As DateTime = DateTime.MinValue

                ' Retrieve the value of nomination_start from the last row of the election_time table
                Dim selectQuery As String = "SELECT results_announcement FROM election_time ORDER BY election_id DESC LIMIT 1;"
                cmd = New MySqlCommand(selectQuery, Con)
                Dim result As Object = cmd.ExecuteScalar()

                ' Check if the result is not null and assign it to nominationStartDate
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    resultAnnouncementDate = Convert.ToDateTime(result)
                End If

                ' Check if the current date is after the nomination start date
                If resultAnnouncementDate <> DateTime.MinValue AndAlso DateTime.Parse(current_date) >= resultAnnouncementDate Then
                    Dim updateCmd As New MySqlCommand("UPDATE election_time SET announced = 1 WHERE election_id = @electionID;", Con)
                    updateCmd.Parameters.AddWithValue("@electionID", lastElectionID)
                    Dim rowsAffected As Integer = updateCmd.ExecuteNonQuery()

                    If rowsAffected > 0 Then

                        Dim updateVoter As New MySqlCommand("UPDATE users SET voted = 0;")
                        updateCmd.ExecuteNonQuery()
                        MessageBox.Show("Results have been released.")


                    Else
                        MessageBox.Show("Failed to release results. Please try again later.")
                    End If
                Else
                    MessageBox.Show("It's not the time to release results yet. Please wait patiently.")
                End If
            End If




        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try






    End Sub
End Class