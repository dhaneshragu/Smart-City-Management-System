Imports System.Data.SqlClient
Imports FastReport.DataVisualization.Charting
Imports MySql.Data.MySqlClient
Imports SmartCityMgmtSystem.ElectionInnerScreenCitizenRTI
Public Class ElectionInnerScreenViewStatisticsMinistry8

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
        cmd.Parameters.AddWithValue("@election_id", ElectionInnerScreenViewStatistics.lastElectionID)
        cmd.Parameters.AddWithValue("@ministry_id", 8)

        reader = cmd.ExecuteReader()

        Dim votesReceived As Integer = 0

        If reader.Read() Then
            votesReceived = Convert.ToInt32(reader("votes_received"))
        End If

        Label2.Text = "Total Votes : " & votesReceived
        ' Calculate the turnout only if totalVoters is not zero to avoid division by zero error
        If ElectionInnerScreenViewStatistics.totalVoted <> 0 Then
            Label3.Text = "Ministry Turnout : " & (votesReceived / ElectionInnerScreenViewStatistics.totalVoted) * 100 & "%"
        Else
            Label3.Text = "No voters"
        End If

        reader.Close()
        Con.Close()
    End Sub
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
        Chart1.Series("DataSeries").Points.AddXY("John doe", 20)
        Chart1.Series("DataSeries").Points.AddXY("Carlos", 10)
    End Sub
    Private Sub ElectionInnerScreenViewStatisticsMinistry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Chart_Init()
        DataGridView1.Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        ' Fill two rows with dummy data
        Dim row1 As String() = {"John Doe", "20", "66"}
        Dim row2 As String() = {"Carlos", "10", "33"}
        DataGridView1.Rows.Add(row1)
        DataGridView1.Rows.Add(row2)
        LoadData()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatistics)
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class