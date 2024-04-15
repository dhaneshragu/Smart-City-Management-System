Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class ElectionCitizenTimeline

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Public Property innerPanel As Panel

    Dim electionInnerScreen1 As ElectionInnerScreen1 = Nothing

    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim row1 As New DataGridViewRow()
        DataGridView1.Rows.Add(row1)
        DataGridView1.Rows(0).Cells("Column1").Value = "Nomination Start Date"
        'DataGridView1.Rows(0).Cells("Column2").Value = "18.03.2024"

        Dim row2 As New DataGridViewRow()
        DataGridView1.Rows.Add(row2)
        DataGridView1.Rows(1).Cells("Column1").Value = "Nomination End Date"
        'DataGridView1.Rows(1).Cells("Column2").Value = "20.03.2024"

        Dim row3 As New DataGridViewRow()
        DataGridView1.Rows.Add(row3)
        DataGridView1.Rows(2).Cells("Column1").Value = "Campaigning Start Date"
        'DataGridView1.Rows(2).Cells("Column2").Value = "22.03.2024"

        Dim row4 As New DataGridViewRow()
        DataGridView1.Rows.Add(row4)
        DataGridView1.Rows(3).Cells("Column1").Value = "Campaigning End Date"
        'DataGridView1'.Rows(3).Cells("Column2").Value = "25.03.2024"

        Dim row5 As New DataGridViewRow()
        DataGridView1.Rows.Add(row5)
        DataGridView1.Rows(4).Cells("Column1").Value = "Election Date"
        'DataGridView1.Rows(4).Cells("Column2").Value = "27.03.2024"

        Dim row6 As New DataGridViewRow()
        DataGridView1.Rows.Add(row6)
        DataGridView1.Rows(5).Cells("Column1").Value = "Results Announcement Date"
        'DataGridView1.Rows(5).Cells("Column2").Value = "30.03.2024"
        LoadDates()

    End Sub

    Private Sub LoadDates()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Retrieve the value of the election_id column from the last row of the election_time table
        Dim nominations_start As String ' Default value in case there are no rows in election_time
        cmd = New MySqlCommand("SELECT nomination_start FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            nominations_start = reader("nomination_start").ToString.Substring(0, 10)
        End If
        reader.Close()
        DataGridView1.Rows(0).Cells("Column2").Value = nominations_start

        ' Retrieve the value of the election_id column from the last row of the election_time table
        Dim nomination_end As String ' Default value in case there are no rows in election_time
        cmd = New MySqlCommand("SELECT nomination_end FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            nomination_end = reader("nomination_end").ToString.Substring(0, 10)
        End If
        reader.Close()
        DataGridView1.Rows(1).Cells("Column2").Value = nomination_end

        ' Retrieve the value of the election_id column from the last row of the election_time table
        Dim campaigning_start As String ' Default value in case there are no rows in election_time
        cmd = New MySqlCommand("SELECT campaigning_start FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            campaigning_start = reader("campaigning_start").ToString.Substring(0, 10)
        End If
        reader.Close()
        DataGridView1.Rows(2).Cells("Column2").Value = campaigning_start

        ' Retrieve the value of the election_id column from the last row of the election_time table
        Dim campaigning_end As String ' Default value in case there are no rows in election_time
        cmd = New MySqlCommand("SELECT campaigning_end FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            campaigning_end = reader("campaigning_end").ToString.Substring(0, 10)
        End If
        reader.Close()
        DataGridView1.Rows(3).Cells("Column2").Value = campaigning_end


        ' Retrieve the value of the election_id column from the last row of the election_time table
        Dim election As String ' Default value in case there are no rows in election_time
        cmd = New MySqlCommand("SELECT election FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            election = reader("election").ToString.Substring(0, 10)
        End If
        reader.Close()
        DataGridView1.Rows(4).Cells("Column2").Value = election

        ' Retrieve the value of the election_id column from the last row of the election_time table
        Dim results_announcement As String ' Default value in case there are no rows in election_time
        cmd = New MySqlCommand("SELECT results_announcement FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            results_announcement = reader("results_announcement").ToString.Substring(0, 10)
        End If
        reader.Close()
        DataGridView1.Rows(5).Cells("Column2").Value = results_announcement


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
End Class