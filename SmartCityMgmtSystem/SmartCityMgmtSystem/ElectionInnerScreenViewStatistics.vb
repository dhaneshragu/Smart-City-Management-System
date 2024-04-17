Imports System.Data.SqlClient
Imports System.IO
Imports FastReport.DataVisualization.Charting
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreenViewStatistics

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"

    Public Property innerPanel As Panel
    Dim electionInnerScreen1 As ElectionInnerScreen1 = Nothing
    Dim electionInnerScreenViewStatisticsMinistry As ElectionInnerScreenViewStatisticsMinistry = Nothing
    Dim electionInnerScreenViewStatisticsMinistry2 As ElectionInnerScreenViewStatisticsMinistry2 = Nothing
    Dim electionInnerScreenViewStatisticsMinistry3 As ElectionInnerScreenViewStatisticsMinistry3 = Nothing
    Dim electionInnerScreenViewStatisticsMinistry4 As ElectionInnerScreenViewStatisticsMinistry4 = Nothing
    Dim electionInnerScreenViewStatisticsMinistry5 As ElectionInnerScreenViewStatisticsMinistry5 = Nothing
    Dim electionInnerScreenViewStatisticsMinistry6 As ElectionInnerScreenViewStatisticsMinistry6 = Nothing
    Dim electionInnerScreenViewStatisticsMinistry7 As ElectionInnerScreenViewStatisticsMinistry7 = Nothing
    Dim electionInnerScreenViewStatisticsMinistry8 As ElectionInnerScreenViewStatisticsMinistry8 = Nothing
    Dim electionInnerScreenViewStatisticsMinistry9 As ElectionInnerScreenViewStatisticsMinistry9 = Nothing


    Public Property lastElectionID As Integer
    Public Property totalVoted As Integer = 0
    Dim turnoutPercentage As Double
    Dim Male As Integer
    Private Sub Chart_Init()
        ' Clear existing series and chart areas
        Chart1.Series.Clear()
        Chart1.ChartAreas.Clear()

        ' Create a new chart area
        Dim chartArea As New ChartArea("MainChartArea")
        Chart1.ChartAreas.Add(chartArea)

        ' Create a new series
        Dim series As New Series("DataSeries")
        series.ChartType = SeriesChartType.Pie
        series.ChartArea = "MainChartArea"
        Chart1.Series.Add(series)

        ' Add data points for each day of the week manually
        Chart1.Series("DataSeries").Points.AddXY("Male", Male)
        Chart1.Series("DataSeries").Points.AddXY("Female", totalVoted - Male)
    End Sub

    Private Sub LoadData()
        Dim Con As MySqlConnection = Globals.GetDBConnection()
        Dim cmd As MySqlCommand
        Dim reader As MySqlDataReader

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        cmd = New MySqlCommand("SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            lastElectionID = Convert.ToInt32(reader("election_id"))
        End If
        reader.Close()


        ' Execute the first SQL query to count the number of rows where the voter column is 1
        cmd = New MySqlCommand("SELECT COUNT(*) AS total_voters FROM users WHERE voter = 1", Con)
        reader = cmd.ExecuteReader()

        Dim totalVoters As Integer = 0

        If reader.Read() Then
            totalVoters = Convert.ToInt32(reader("total_voters"))
        End If

        reader.Close()

        ' Execute the second SQL query to count the number of rows where the voted column is 1
        cmd = New MySqlCommand("SELECT COUNT(*) AS total_voted FROM users WHERE voted = 1", Con)
        reader = cmd.ExecuteReader()


        If reader.Read() Then
            totalVoted = Convert.ToInt32(reader("total_voted"))
        End If

        reader.Close()

        ' Execute the second SQL query to count the number of rows where the voted column is 1
        cmd = New MySqlCommand("SELECT COUNT(*) AS male 
                                FROM users 
                                WHERE voted = 1 AND gender = 'Male'", Con)
        reader = cmd.ExecuteReader()


        If reader.Read() Then
            Male = Convert.ToInt32(reader("male"))
        End If

        reader.Close()
        Label20.Text = ""
        Label6.Text = ""
        Label7.Text = ""

        ' Calculate the turnout only if totalVoters is not zero to avoid division by zero error
        If totalVoters <> 0 Then
            turnoutPercentage = (totalVoted / totalVoters) * 100

            ' Display the turnout in Panel 2 with up to 2 decimal places
            Label20.Text = turnoutPercentage.ToString("0.##") & "%"
        Else
            Label20.Text = "No voters"
        End If


        ' Query to find the ministry with the maximum votes_received
        cmd = New MySqlCommand("SELECT ministries.ministry_name FROM ministries INNER JOIN turnout ON ministries.ministry_id = turnout.ministry_id WHERE turnout.election_id = @electionId ORDER BY turnout.votes_received DESC LIMIT 1", Con)
        cmd.Parameters.AddWithValue("@electionId", lastElectionID)
        reader = cmd.ExecuteReader()

        If reader.Read() Then
            Dim ministryName As String = reader("ministry_name").ToString()
            Label6.Text = ministryName.Replace("Ministry of ", "")
            'Label6.Text = totalVoted
        Else
            Label6.Text = "No data found"
        End If

        reader.Close()

        ' Query to find the ministry with the minimum votes_received
        cmd = New MySqlCommand("SELECT ministries.ministry_name FROM ministries INNER JOIN turnout ON ministries.ministry_id = turnout.ministry_id WHERE turnout.election_id = @electionId ORDER BY turnout.votes_received ASC LIMIT 1", Con)
        cmd.Parameters.AddWithValue("@electionId", lastElectionID)
        reader = cmd.ExecuteReader()

        If reader.Read() Then
            Dim ministryName As String = reader("ministry_name").ToString()
            Label7.Text = ministryName.Replace("Ministry of ", "")
            'Label7.Text = totalVoters
        Else
            Label7.Text = "No data found"
        End If

        reader.Close()
        Con.Close()
    End Sub


    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
        Chart_Init()
    End Sub

    Private Sub Panel7_Click(sender As Object, e As EventArgs) Handles Panel7.Click
        electionInnerScreenViewStatisticsMinistry2?.Dispose()
        electionInnerScreenViewStatisticsMinistry2 = New ElectionInnerScreenViewStatisticsMinistry2 With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name,
            .lastElectionID = lastElectionID,
            .totalVoted = totalVoted
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenViewStatisticsMinistry2)
        'Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry2)

    End Sub

    Private Sub Panel8_Click(sender As Object, e As EventArgs) Handles Panel8.Click
        electionInnerScreenViewStatisticsMinistry?.Dispose()
        electionInnerScreenViewStatisticsMinistry = New ElectionInnerScreenViewStatisticsMinistry With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name,
            .lastElectionID = lastElectionID,
                        .totalVoted = totalVoted
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenViewStatisticsMinistry)
        'Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry)
    End Sub

    Private Sub Panel9_Click(sender As Object, e As EventArgs) Handles Panel9.Click
        electionInnerScreenViewStatisticsMinistry5?.Dispose()
        electionInnerScreenViewStatisticsMinistry5 = New ElectionInnerScreenViewStatisticsMinistry5 With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name,
            .lastElectionID = lastElectionID,
                        .totalVoted = totalVoted
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenViewStatisticsMinistry5)

        'Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry5)
    End Sub

    Private Sub Panel10_Click(sender As Object, e As EventArgs) Handles Panel10.Click
        electionInnerScreenViewStatisticsMinistry7?.Dispose()
        electionInnerScreenViewStatisticsMinistry7 = New ElectionInnerScreenViewStatisticsMinistry7 With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name,
            .lastElectionID = lastElectionID
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenViewStatisticsMinistry7)
        'Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry7)
    End Sub

    Private Sub Panel11_Click(sender As Object, e As EventArgs) Handles Panel11.Click
        electionInnerScreenViewStatisticsMinistry4?.Dispose()
        electionInnerScreenViewStatisticsMinistry4 = New ElectionInnerScreenViewStatisticsMinistry4 With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name,
            .lastElectionID = lastElectionID,
            .totalVoted = totalVoted
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenViewStatisticsMinistry4)
        'Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry4)
    End Sub

    Private Sub Panel12_Click(sender As Object, e As EventArgs) Handles Panel12.Click
        electionInnerScreenViewStatisticsMinistry6?.Dispose()
        electionInnerScreenViewStatisticsMinistry6 = New ElectionInnerScreenViewStatisticsMinistry6 With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name,
            .lastElectionID = lastElectionID,
            .totalVoted = totalVoted
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenViewStatisticsMinistry6)
        'Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry6)
    End Sub

    Private Sub Panel13_Click(sender As Object, e As EventArgs) Handles Panel13.Click
        electionInnerScreenViewStatisticsMinistry3?.Dispose()
        electionInnerScreenViewStatisticsMinistry3 = New ElectionInnerScreenViewStatisticsMinistry3 With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name,
            .lastElectionID = lastElectionID,
            .totalVoted = totalVoted
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenViewStatisticsMinistry3)
        'Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry3)
    End Sub

    Private Sub Panel14_Click(sender As Object, e As EventArgs) Handles Panel14.Click
        electionInnerScreenViewStatisticsMinistry8?.Dispose()
        electionInnerScreenViewStatisticsMinistry8 = New ElectionInnerScreenViewStatisticsMinistry8 With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name,
            .lastElectionID = lastElectionID,
            .totalVoted = totalVoted
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenViewStatisticsMinistry8)
        'Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry8)
    End Sub

    Private Sub Panel15_Click(sender As Object, e As EventArgs) Handles Panel15.Click
        electionInnerScreenViewStatisticsMinistry9?.Dispose()
        electionInnerScreenViewStatisticsMinistry9 = New ElectionInnerScreenViewStatisticsMinistry9 With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name,
            .lastElectionID = lastElectionID,
            .totalVoted = totalVoted
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenViewStatisticsMinistry9)
        'Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry9)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        electionInnerScreen1?.Dispose()
        electionInnerScreen1 = New ElectionInnerScreen1 With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
           }
        Globals.viewChildForm(innerPanel, electionInnerScreen1)
        'Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreen1)
    End Sub

End Class