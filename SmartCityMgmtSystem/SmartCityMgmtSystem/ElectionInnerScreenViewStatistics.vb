Imports System.Data.SqlClient
Imports System.IO
Imports FastReport.DataVisualization.Charting
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreenViewStatistics
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
        Chart1.Series("DataSeries").Points.AddXY("Voted", 60)
        Chart1.Series("DataSeries").Points.AddXY("Not Voted", 40)
    End Sub

    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Chart_Init()
    End Sub

    Private Sub Panel7_Click(sender As Object, e As EventArgs) Handles Panel7.Click
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry)
    End Sub

    Private Sub Panel8_Click(sender As Object, e As EventArgs) Handles Panel8.Click
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry)
    End Sub

    Private Sub Panel9_Click(sender As Object, e As EventArgs) Handles Panel9.Click
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreenViewStatisticsMinistry)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Globals.viewChildForm(ElectionDashboard.childformPanel, ElectionInnerScreen1)
    End Sub
End Class