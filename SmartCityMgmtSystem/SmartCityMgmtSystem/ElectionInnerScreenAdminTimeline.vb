Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreenAdminTimeline

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Dim electionInnerScreenAdmin As ElectionInnerScreenAdmin = Nothing
    Public Property innerPanel As Panel
    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


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

    Private Function AreDatesIncreasing(dates() As String) As Boolean
        ' Iterate through the array starting from the second element
        For i As Integer = 1 To dates.Length - 1
            ' Convert the current and previous dates to Date objects for comparison
            Dim currentDate As Date
            Dim previousDate As Date
            If Date.TryParse(dates(i), currentDate) AndAlso Date.TryParse(dates(i - 1), previousDate) Then
                ' Check if the current date is less than or equal to the previous date
                If currentDate <= previousDate Then
                    ' If not, return false
                    Return False
                End If
            Else
                ' If any date parsing fails, return false
                Return False
            End If
        Next

        ' If all dates are in increasing order, return true
        Return True
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' You can access the selected date and time like this:
        'Dim dt As DateTime = DateTime.Now
        'Dim year As Integer = dt.Year
        'Dim month As Integer = dt.Month
        'Dim day As Integer = dt.Day
        'Dim current_date As String = year.ToString + "-" + month.ToString + "-" + day.ToString
        Dim current_date As String = "2024-04-15"

        Dim selectedDateTime2 As DateTime = DateTimePicker2.Value
        Dim nomination_start As String = selectedDateTime2.ToString("yyyy-MM-dd")

        Dim selectedDateTime3 As DateTime = DateTimePicker3.Value
        Dim nomination_end As String = selectedDateTime3.ToString("yyyy-MM-dd")

        Dim selectedDateTime4 As DateTime = DateTimePicker4.Value
        Dim campaigning_start As String = selectedDateTime4.ToString("yyyy-MM-dd")

        Dim selectedDateTime5 As DateTime = DateTimePicker5.Value
        Dim campaigning_end As String = selectedDateTime5.ToString("yyyy-MM-dd")

        Dim selectedDateTime6 As DateTime = DateTimePicker6.Value
        Dim election_date As String = selectedDateTime6.ToString("yyyy-MM-dd")

        Dim selectedDateTime7 As DateTime = DateTimePicker7.Value
        Dim results_announcement As String = selectedDateTime7.ToString("yyyy-MM-dd")

        Dim allDates() As String = {current_date, nomination_start, nomination_end, campaigning_start, campaigning_end, election_date, results_announcement}

        Dim correct_dates As Boolean = AreDatesIncreasing(allDates)

        'MessageBox.Show(correct_dates)

        If correct_dates Then
            ' Get connection from globals
            Dim Con = Globals.GetDBConnection()
            Dim cmd As MySqlCommand
            Dim electionTimeRowCount As Integer = 0 ' Variable to store the number of rows in election_time table

            Try
                Con.Open()

                ' Execute query to count rows in election_time table
                cmd = New MySqlCommand("SELECT COUNT(*) FROM election_time;", Con)
                electionTimeRowCount = Convert.ToInt32(cmd.ExecuteScalar())
                MessageBox.Show("election time row count " & electionTimeRowCount)

                If electionTimeRowCount > 0 Then
                    ' Get connection from globals
                    'Dim Con = Globals.GetDBConnection()
                    'Dim cmd As MySqlCommand
                    Dim resultsAnnouncement As DateTime = DateTime.MinValue ' Variable to store the value of the results_announcement column

                    Try
                        ' Execute query to select the results_announcement column of the last row in the election_time table
                        Dim selectQuery As String = "SELECT results_announcement FROM election_time ORDER BY election_id DESC LIMIT 1;"
                        cmd = New MySqlCommand(selectQuery, Con)
                        Dim result As Object = cmd.ExecuteScalar()

                        ' Check if the result is not null and assign it to resultsAnnouncement
                        If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                            resultsAnnouncement = CDate(result)
                            Dim previousDate As String = resultsAnnouncement.ToString("yyyy-MM-dd")
                            'MessageBox.Show(previousDate + "," + current_date)
                            Dim addDates() As String = {previousDate, current_date}
                            Dim check As Boolean = AreDatesIncreasing(addDates)

                            'MessageBox.Show(check)

                            If check Then
                                ' Create the INSERT statement to add a new row

                                ' Increment the electionTimeRowCount by 1 for the new ID
                                Dim newID As Integer = electionTimeRowCount + 1
                                Dim insertQuery As String = "INSERT INTO election_time VALUES (@electionID, @nomination_start, @nomination_end, @campaigning_start, @campaigning_end, @election, @results_announcement, 0);"

                                ' Create the command and set parameters
                                cmd = New MySqlCommand(insertQuery, Con)
                                cmd.Parameters.AddWithValue("@electionID", newID)
                                cmd.Parameters.AddWithValue("@nomination_start", nomination_start)
                                cmd.Parameters.AddWithValue("@nomination_end", nomination_end)
                                cmd.Parameters.AddWithValue("@campaigning_start", campaigning_start)
                                cmd.Parameters.AddWithValue("@campaigning_end", campaigning_end)
                                cmd.Parameters.AddWithValue("@election", election_date)
                                cmd.Parameters.AddWithValue("@results_announcement", results_announcement)

                                ' Execute the INSERT statement
                                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                                If rowsAffected > 0 Then
                                    MessageBox.Show("New election scheduled with ID: " & newID.ToString())
                                    Dim insertQuery1 As String = "INSERT INTO code_of_conduct VALUES (@electionID, @code_of_conduct_text);"
                                    cmd = New MySqlCommand(insertQuery1, Con)
                                    cmd.Parameters.AddWithValue("@electionID", newID)
                                    cmd.Parameters.AddWithValue("@code_of_conduct_text", "Code of Conduct hasn't been published yet. We are extremely sorry for the delay.")
                                    Dim rowsAffected1 As Integer = cmd.ExecuteNonQuery()
                                Else
                                    MessageBox.Show("Failed to insert new row.")
                                End If
                            Else
                                MessageBox.Show("Sorry, an election is/would be in progress. A new election can be scheduled only after the results of the previously scheduled election are announced i.e. an election can be scheduled after " + previousDate + ".")
                            End If
                        End If

                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        ' Close the connection
                        If Con.State = ConnectionState.Open Then
                            Con.Close()
                        End If
                    End Try

                    ' Now, resultsAnnouncement contains the value of the results_announcement column of the last row in the election_time table
                Else

                    MessageBox.Show("Came till here")
                    Dim newID As Integer = electionTimeRowCount + 1
                    Dim insertQuery As String = "INSERT INTO election_time VALUES (@electionID, @nomination_start, @nomination_end, @campaigning_start, @campaigning_end, @election, @results_announcement, 0);"

                    ' Create the command and set parameters
                    cmd = New MySqlCommand(insertQuery, Con)
                    cmd.Parameters.AddWithValue("@electionID", newID)
                    cmd.Parameters.AddWithValue("@nomination_start", nomination_start)
                    cmd.Parameters.AddWithValue("@nomination_end", nomination_end)
                    cmd.Parameters.AddWithValue("@campaigning_start", campaigning_start)
                    cmd.Parameters.AddWithValue("@campaigning_end", campaigning_end)
                    cmd.Parameters.AddWithValue("@election", election_date)
                    cmd.Parameters.AddWithValue("@results_announcement", results_announcement)

                    ' Execute the INSERT statement
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()



                    If rowsAffected > 0 Then
                        MessageBox.Show("New election scheduled with ID: " & newID.ToString())
                        Dim insertQuery1 As String = "INSERT INTO code_of_conduct VALUES (@electionID, @code_of_conduct_text);"
                        cmd = New MySqlCommand(insertQuery1, Con)
                        cmd.Parameters.AddWithValue("@electionID", newID)
                        cmd.Parameters.AddWithValue("@code_of_conduct_text", "Code of Conduct hasn't been published yet. We are extremely sorry for the delay.")
                        Dim rowsAffected1 As Integer = cmd.ExecuteNonQuery()
                    Else
                        MessageBox.Show("Failed to insert new row.")
                    End If
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
            MessageBox.Show("The dates are not in increasing order. Please correct the dates and try again.")
        End If

    End Sub

End Class