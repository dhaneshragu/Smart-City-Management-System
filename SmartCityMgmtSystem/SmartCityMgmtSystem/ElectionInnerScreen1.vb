Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreen1
    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Public Property innerPanel As Panel

    Dim electionInnerScreenVoter As ElectionInnerScreenVoter = Nothing
    Dim electionInnerScreenCitizenViolation As ElectionInnerScreenCitizenViolation = Nothing
    Dim electionVoterNominate As ElectionVoterNominate = Nothing
    Dim electionCitizenKYC As ElectionCitizenKYC = Nothing
    Dim electionCitizenResults As ElectionCitizenResults = Nothing
    Dim electionCitizenTimeline As ElectionCitizenTimeline = Nothing
    Dim electionInnerScreenCastVote As ElectionInnerScreenCastVote = Nothing
    Dim electionInnerScreenPastElections As ElectionInnerScreenPastElections = Nothing
    Dim electionInnerScreenCitizenCoC As ElectionInnerScreenCitizenCoC = Nothing
    Dim electionInnerScreenViewStatistics As ElectionInnerScreenViewStatistics = Nothing

    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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

    Private Function AreDatesEqual(dates() As String) As Boolean
        ' Check if the array is empty or has only one element
        If dates.Length <= 1 Then
            Return True ' If array has 0 or 1 elements, dates are equal by default
        End If

        ' Convert the first date to a Date object
        Dim firstDate As Date
        If Not Date.TryParse(dates(0), firstDate) Then
            Return False ' Return false if parsing the first date fails
        End If

        ' Iterate through the array starting from the second element
        For i As Integer = 1 To dates.Length - 1
            ' Convert the current date to a Date object for comparison
            Dim currentDate As Date
            If Not Date.TryParse(dates(i), currentDate) Then
                Return False ' Return false if parsing any date fails
            End If

            ' Check if the current date is not equal to the first date
            If currentDate <> firstDate Then
                ' If not, return false
                Return False
            End If
        Next

        ' If all dates are the same, return true
        Return True
    End Function

    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles Panel1.Click
        electionInnerScreenVoter?.Dispose()
        electionInnerScreenVoter = New ElectionInnerScreenVoter With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenVoter)
    End Sub

    Private Sub Panel3_Click(sender As Object, e As EventArgs) Handles Panel3.Click
        electionCitizenKYC?.Dispose()
        electionCitizenKYC = New ElectionCitizenKYC With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionCitizenKYC)
    End Sub

    Private Sub Panel5_Click(sender As Object, e As EventArgs) Handles Panel5.Click

        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Dim announced As Integer = 0 ' Default value in case there are no rows in election_time
        Dim lastElectionIDQuery As String = "SELECT announced FROM election_time ORDER BY election_id DESC LIMIT 1;"
        cmd = New MySqlCommand(lastElectionIDQuery, Con)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()
        If reader.Read() Then
            announced = Convert.ToInt32(reader("announced"))
        End If
        reader.Close()


        If announced = 1 Then
            electionCitizenResults?.Dispose()
            electionCitizenResults = New ElectionCitizenResults With {
                .innerPanel = innerPanel,
                .uid = uid,
                .u_name = u_name
            }
            Globals.viewChildForm(innerPanel, ElectionCitizenResults)
        Else
            MessageBox.Show("Results haven't been announced yet. View Election Timeline to know the dates.")
        End If



    End Sub

    Private Sub Panel8_Click(sender As Object, e As EventArgs) Handles Panel8.Click
        electionCitizenTimeline?.Dispose()
        electionCitizenTimeline = New ElectionCitizenTimeline With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionCitizenTimeline)
    End Sub

    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Dim lastElectionID As Integer = 0 ' Default value in case there are no rows in election_time
        Dim lastElectionIDQuery As String = "SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;"
        cmd = New MySqlCommand(lastElectionIDQuery, Con)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()
        If reader.Read() Then
            lastElectionID = Convert.ToInt32(reader("election_id"))
        End If
        reader.Close()

        If lastElectionID = 0 Then
            MessageBox.Show("No elections have been scheduled yet. You can nominate yourself when elections are scheduled.")
            Exit Sub
        End If

        ' You can access the selected date and time like this:
        'Dim dt As DateTime = DateTime.Now
        'Dim year As Integer = dt.Year
        'Dim month As Integer = dt.Month
        'Dim day As Integer = dt.Day
        'Dim current_date As String = year.ToString + "-" + month.ToString + "-" + day.ToString
        Dim current_date As String = "2024-04-18"

        Dim nominationStartDate As DateTime = DateTime.MinValue
        Dim nominationEndDate As DateTime = DateTime.MinValue

        ' Retrieve the value of nomination_start from the last row of the election_time table
        Dim selectQuery As String = "SELECT nomination_start FROM election_time ORDER BY election_id DESC LIMIT 1;"
        cmd = New MySqlCommand(selectQuery, Con)
        Dim result As Object = cmd.ExecuteScalar()

        Dim selectQuery1 As String = "SELECT nomination_end FROM election_time ORDER BY election_id DESC LIMIT 1;"
        cmd = New MySqlCommand(selectQuery1, Con)
        Dim result1 As Object = cmd.ExecuteScalar()

        If result IsNot Nothing AndAlso Not IsDBNull(result) Then
            nominationStartDate = Convert.ToDateTime(result)
        End If

        If result1 IsNot Nothing AndAlso Not IsDBNull(result1) Then
            nominationEndDate = Convert.ToDateTime(result1)
        End If

        'MessageBox.Show(nominationStartDate.ToString("yyyy-MM-dd") + "," + nominationEndDate.ToString("yyyy-MM-dd"))

        Dim check As Boolean = AreDatesIncreasing({current_date, nominationStartDate.ToString("yyyy-MM-dd")})
        Dim check1 As Boolean = AreDatesIncreasing({nominationStartDate.ToString("yyyy-MM-dd"), current_date, nominationEndDate.ToString("yyyy-MM-dd")})

        ' MessageBox.Show(check)
        'MessageBox.Show(check1)

        If check = True Then
            MessageBox.Show("Nominations haven't begun yet.")
        ElseIf check1 = True Then

            electionVoterNominate?.Dispose()
            electionVoterNominate = New ElectionVoterNominate With {
                .innerPanel = innerPanel,
                .uid = uid,
                .u_name = u_name
            }
            Globals.viewChildForm(innerPanel, electionVoterNominate)
        Else
            MessageBox.Show("No nominations are accepted now.")
        End If

    End Sub

    Private Sub Panel4_Click(sender As Object, e As EventArgs) Handles Panel4.Click

        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Dim lastElectionID As Integer = 0 ' Default value in case there are no rows in election_time
        Dim lastElectionIDQuery As String = "SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;"
        cmd = New MySqlCommand(lastElectionIDQuery, Con)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()
        If reader.Read() Then
            lastElectionID = Convert.ToInt32(reader("election_id"))
        End If
        reader.Close()

        If lastElectionID = 0 Then
            MessageBox.Show("No elections have been scheduled yet.")
            Exit Sub
        End If

        Dim current_date As String = "2024-04-25"

        Dim election As DateTime = DateTime.MinValue

        ' Retrieve the value of nomination_start from the last row of the election_time table
        Dim selectQuery As String = "SELECT election FROM election_time ORDER BY election_id DESC LIMIT 1;"
        cmd = New MySqlCommand(selectQuery, Con)
        Dim result As Object = cmd.ExecuteScalar()

        If result IsNot Nothing AndAlso Not IsDBNull(result) Then
            election = Convert.ToDateTime(result)
        End If

        Dim check As Boolean = AreDatesEqual({current_date, election.ToString("yyyy-MM-dd")})
        'MessageBox.Show(current_date + "," + election.ToString("yyyy-MM-dd") + "," & check)

        If check Then

            Dim voter As Integer = 0 ' Default value in case there are no rows in election_time
            Dim voterQuery As String = "SELECT voter FROM users WHERE user_id = " & uid & " ;"
            cmd = New MySqlCommand(voterQuery, Con)
            reader = cmd.ExecuteReader()
            If reader.Read() Then
                voter = Convert.ToInt32(reader("voter"))
            End If
            reader.Close()

            If voter = 1 Then
                Dim voted As Integer = 0 ' Default value in case there are no rows in election_time
                Dim votedQuery As String = "SELECT voted FROM users WHERE user_id = " & uid & " ;"
                cmd = New MySqlCommand(votedQuery, Con)
                reader = cmd.ExecuteReader()
                If reader.Read() Then
                    voted = Convert.ToInt32(reader("voted"))
                End If
                reader.Close()
                If voted = 0 Then
                    electionInnerScreenCastVote?.Dispose()
                    electionInnerScreenCastVote = New ElectionInnerScreenCastVote With {
                        .innerPanel = innerPanel,
                        .uid = uid,
                        .u_name = u_name
                    }
                    Globals.viewChildForm(innerPanel, electionInnerScreenCastVote)
                Else
                    MessageBox.Show("You have already voted in this election.")
                End If
            Else
                MessageBox.Show("You haven't been registered as a voter. Please register to cast your valuable vote.")
                Exit Sub
            End If


        Else
            MessageBox.Show("It's not time for voting yet. View the Election Timeline to know the dates.")
        End If
    End Sub

    Private Sub Panel7_Click(sender As Object, e As EventArgs) Handles Panel7.Click
        electionInnerScreenPastElections?.Dispose()
        electionInnerScreenPastElections = New ElectionInnerScreenPastElections With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenPastElections)
    End Sub

    Private Sub Panel9_Click(sender As Object, e As EventArgs) Handles Panel9.Click
        electionInnerScreenCitizenCoC?.Dispose()
        electionInnerScreenCitizenCoC = New ElectionInnerScreenCitizenCoC With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenCitizenCoC)
    End Sub

    Private Sub Panel10_Click(sender As Object, e As EventArgs) Handles Panel10.Click

        ' Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand
        Dim announced As Integer

        Try
            Con.Open()


            ' Retrieve the value of the election_id column from the last row of the election_time table
            Dim electionID As Integer = 0 ' Default value in case there are no rows in election_time
            cmd = New MySqlCommand("SELECT COUNT(*) AS count FROM election_time;", Con)
            reader = cmd.ExecuteReader()
            If reader.Read() Then
                electionID = Convert.ToInt32(reader("count"))
            End If
            reader.Close()

            If electionID = 0 Then
                MessageBox.Show("No elections have been conducted yet.")
                Exit Sub
            End If

            Dim query As String = "SELECT announced
                                FROM election_time 
                                WHERE election_id = @electionId;"
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@electionId", electionID)
            reader = cmd.ExecuteReader()
            If reader.Read() Then
                announced = Convert.ToInt32(reader("announced"))
            End If
            reader.Close()

            If announced = 1 Then
                MessageBox.Show("There are no active elections currently.")
            Else
                electionInnerScreenCitizenViolation?.Dispose()
                electionInnerScreenCitizenViolation = New ElectionInnerScreenCitizenViolation With {
                    .innerPanel = innerPanel,
                    .uid = uid,
                    .u_name = u_name
                }

                Globals.viewChildForm(innerPanel, electionInnerScreenCitizenViolation)
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close the database connection
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try

    End Sub

    Private Sub Panel6_Click(sender As Object, e As EventArgs) Handles Panel6.Click
        electionInnerScreenViewStatistics?.Dispose()
        electionInnerScreenViewStatistics = New ElectionInnerScreenViewStatistics With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenViewStatistics)
    End Sub
End Class