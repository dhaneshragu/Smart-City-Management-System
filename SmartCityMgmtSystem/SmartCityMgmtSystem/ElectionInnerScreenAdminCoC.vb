Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreenAdminCoC
    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Dim electionInnerScreenAdmin As ElectionInnerScreenAdmin = Nothing
    Public Property innerPanel As Panel
    Private Sub PreviousCoC()
        ' Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
            ' Retrieve the value of the code_of_conduct_text column from the last row of the code_of_conduct table
            Dim textCoC As String = "" ' Initialize with an empty string
            cmd = New MySqlCommand("SELECT code_of_conduct_text FROM code_of_conduct ORDER BY election_id DESC LIMIT 1;", Con)
            reader = cmd.ExecuteReader()

            If reader.Read() Then
                textCoC = Convert.ToString(reader("code_of_conduct_text")) ' Read the code_of_conduct_text column
            End If
            reader.Close()

            RichTextBox1.Text = textCoC
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try



    End Sub

    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PreviousCoC()
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

        Dim current_date As String = "2024-01-03"
        Dim nominationStartDate As DateTime = DateTime.MinValue

        ' Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand

        Try
            Con.Open()

            ' Retrieve the value of nomination_start from the last row of the election_time table
            Dim selectQuery As String = "SELECT nomination_start FROM election_time ORDER BY election_id DESC LIMIT 1;"
            cmd = New MySqlCommand(selectQuery, Con)
            Dim result As Object = cmd.ExecuteScalar()

            ' Check if the result is not null and assign it to nominationStartDate
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                nominationStartDate = Convert.ToDateTime(result)
            End If

            ' Check if the current date is after the nomination start date
            If nominationStartDate <> DateTime.MinValue AndAlso DateTime.Parse(current_date) < nominationStartDate Then
                ' If current date is after nomination start date, update the code_of_conduct_text
                Dim lastElectionID As Integer =0 ' Default value in case there are no rows in election_time
                Dim lastElectionIDQuery As String = "SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;"
                cmd = New MySqlCommand(lastElectionIDQuery, Con)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    lastElectionID = Convert.ToInt32(reader("election_id"))
                End If
                reader.Close()

                ' Update the code_of_conduct_text column in the code_of_conduct table
                Dim updateCmd As New MySqlCommand("UPDATE code_of_conduct SET code_of_conduct_text = @codeOfConductText WHERE election_id = @electionID;", Con)
                updateCmd.Parameters.AddWithValue("@electionID", lastElectionID)
                updateCmd.Parameters.AddWithValue("@codeOfConductText", RichTextBox1.Text)
                Dim rowsAffected As Integer = updateCmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MessageBox.Show("Code of conduct text updated successfully.")
                Else
                    MessageBox.Show("Failed to update code of conduct text.")
                End If
            Else
                MessageBox.Show("Nominations have already started. Code of Conduct can't be updated now.")
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close the connection
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try



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
End Class