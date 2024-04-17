Imports System.Data.SqlClient
Imports System.Management.Instrumentation
Imports FastReport.DataVisualization.Charting
Imports MySql.Data.MySqlClient
Imports SmartCityMgmtSystem.ElectionInnerScreenCitizenRTI
Public Class ElectionInnerScreenViewStatisticsMinistry9
    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Public Property innerPanel As Panel
    Public Property lastElectionID As Integer
    Public Property totalVoted As Integer
    Dim electionInnerScreenViewStatistics As ElectionInnerScreenViewStatistics = Nothing
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
        ' Execute the SQL query to get the votes received for the specified election and ministry
        cmd = New MySqlCommand("SELECT votes_received FROM turnout WHERE election_id = @election_id AND ministry_id = @ministry_id", Con)

        ' Add parameters to the command
        cmd.Parameters.AddWithValue("@election_id", lastElectionID)
        cmd.Parameters.AddWithValue("@ministry_id", 9)

        reader = cmd.ExecuteReader()

        Dim votesReceived As Integer = 0

        If reader.Read() Then
            votesReceived = Convert.ToInt32(reader("votes_received"))
        End If

        Label2.Text = "Total Votes : " & votesReceived
        ' Calculate the turnout only if totalVoters is not zero to avoid division by zero error
        If totalVoted <> 0 Then
            Dim turnoutPercentage As Double = (votesReceived / totalVoted) * 100
            Label3.Text = "Turnout : " & turnoutPercentage.ToString("0.##") & "%"
            ' Label3.Text = "Turnout : " & (votesReceived / totalVoted) * 100 & "%"
        Else
            Label3.Text = "No voters"
        End If

        reader.Close()

        ' Execute the SQL query to retrieve candidate name, votes, and calculate vote percentage
        cmd = New MySqlCommand("SELECT users.name AS candidate_name, candidate_register.votes
                                FROM candidate_register
                                JOIN users ON candidate_register.candidate_uid = users.user_id
                                WHERE election_id = @election_id AND ministry_id = @ministry_id
                                ORDER BY candidate_register.votes DESC;", Con)

        cmd.Parameters.AddWithValue("@election_id", lastElectionID)
        cmd.Parameters.AddWithValue("@ministry_id", 9)
        reader = cmd.ExecuteReader()


        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        ' Add columns for candidate name, votes, and vote percentage to the DataTable
        dataTable.Columns.Add("Candidate Name")
        dataTable.Columns.Add("Votes")
        dataTable.Columns.Add("Vote Percent")
        ' Fill the DataTable with data from the SQL query
        While reader.Read()
            Dim candidateName As String = reader("candidate_name").ToString()
            Dim votes As Integer = Convert.ToInt32(reader("votes"))


            ' Calculate vote percentage
            Dim votePercent As Double = (votes / votesReceived) * 100

            ' Add row to the DataTable
            dataTable.Rows.Add(candidateName, votes, votePercent.ToString("0.##") & "%")
        End While
        ' Specify the Column Mappings from DataTable to DataGridView
        DataGridView1.Columns(0).DataPropertyName = "Candidate Name"
        DataGridView1.Columns(1).DataPropertyName = "Votes"
        DataGridView1.Columns(2).DataPropertyName = "Vote Percent"
        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable

        'chart
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


        ' Loop through the dataTable and add data points to the series
        For Each row As DataRow In dataTable.Rows
            Dim candidateName As String = row("Candidate Name").ToString()
            Dim votes As Integer = Convert.ToInt32(row("Votes"))
            series.Points.AddXY(candidateName, votes)
        Next

        reader.Close()
        Con.Close()
    End Sub
    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        LoadData()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        electionInnerScreenViewStatistics?.Dispose()
        electionInnerScreenViewStatistics = New ElectionInnerScreenViewStatistics With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenViewStatistics)
        'Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatistics)
    End Sub

End Class