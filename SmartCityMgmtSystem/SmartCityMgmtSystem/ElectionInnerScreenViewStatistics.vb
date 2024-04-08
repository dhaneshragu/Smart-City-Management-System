Imports System.Data.SqlClient
Imports System.IO
Imports FastReport.DataVisualization.Charting
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreenViewStatistics
    Public Property SelectedMinistry As Integer
    Dim turnoutPercentage As Double
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
        Chart1.Series("DataSeries").Points.AddXY("Voted", turnoutPercentage)
        Chart1.Series("DataSeries").Points.AddXY("Not Voted", 100-turnoutPercentage)
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


        ' Retrieve the value of the election_id column from the last row of the election_time table
        Dim lastElectionID As Integer = -1 ' Default value in case there are no rows in election_time
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

        Dim totalVoted As Integer = 0

        If reader.Read() Then
            totalVoted = Convert.ToInt32(reader("total_voted"))
        End If

        reader.Close()
        Label20.Text = ""
        Label6.Text = ""
        Label7.Text = ""

        ' Calculate the turnout only if totalVoters is not zero to avoid division by zero error
        If totalVoters <> 0 Then
            turnoutPercentage = (totalVoted / totalVoters) * 100

            ' Display the turnout in Panel 2
            Label20.Text = turnoutPercentage & "%"
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
        SelectedMinistry = 2
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry)
    End Sub

    Private Sub Panel8_Click(sender As Object, e As EventArgs) Handles Panel8.Click
        SelectedMinistry = 1
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry)
    End Sub

    Private Sub Panel9_Click(sender As Object, e As EventArgs) Handles Panel9.Click
        SelectedMinistry = 5
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry)
    End Sub

    Private Sub Panel10_Click(sender As Object, e As EventArgs) Handles Panel10.Click
        SelectedMinistry = 7
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry)
    End Sub

    Private Sub Panel11_Click(sender As Object, e As EventArgs) Handles Panel11.Click
        SelectedMinistry = 4
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry)
    End Sub

    Private Sub Panel12_Click(sender As Object, e As EventArgs) Handles Panel12.Click
        SelectedMinistry = 6
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry)
    End Sub

    Private Sub Panel13_Click(sender As Object, e As EventArgs) Handles Panel13.Click
        SelectedMinistry = 3
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry)
    End Sub

    Private Sub Panel14_Click(sender As Object, e As EventArgs) Handles Panel14.Click
        SelectedMinistry = 12
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry)
    End Sub

    Private Sub Panel15_Click(sender As Object, e As EventArgs) Handles Panel15.Click
        SelectedMinistry = 11
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreen1)
    End Sub

End Class