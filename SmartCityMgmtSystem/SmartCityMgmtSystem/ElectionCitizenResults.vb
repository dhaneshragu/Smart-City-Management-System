Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class ElectionCitizenResults

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Public Property innerPanel As Panel

    Dim electionInnerScreen1 As ElectionInnerScreen1 = Nothing
    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim row0 As New DataGridViewRow()
        'DataGridView1.Rows.Add(row0)

        '' Set values for the first three columns in the current row
        'DataGridView1.Rows(0).Cells("Column1").Value = "Employment"
        'DataGridView1.Rows(0).Cells("Column2").Value = "Neha Sharma"

        'Dim row1 As New DataGridViewRow()
        'DataGridView1.Rows.Add(row1)

        '' Set values for the first three columns in the current row
        'DataGridView1.Rows(1).Cells("Column1").Value = "Education"
        'DataGridView1.Rows(1).Cells("Column2").Value = "Rajesh Patel"

        'Dim row2 As New DataGridViewRow()
        'DataGridView1.Rows.Add(row2)

        '' Set values for the first three columns in the current row
        'DataGridView1.Rows(2).Cells("Column1").Value = "Health"
        'DataGridView1.Rows(2).Cells("Column2").Value = "Priya Singh"

        'Dim row3 As New DataGridViewRow()
        'DataGridView1.Rows.Add(row3)

        '' Set values for the first three columns in the current row
        'DataGridView1.Rows(3).Cells("Column1").Value = "Transport"
        'DataGridView1.Rows(3).Cells("Column2").Value = "Arjun Kumar"

        'Dim row4 As New DataGridViewRow()
        'DataGridView1.Rows.Add(row4)

        '' Set values for the first three columns in the current row
        'DataGridView1.Rows(4).Cells("Column1").Value = "Culture"
        'DataGridView1.Rows(4).Cells("Column2").Value = "Pooja Mehta"

        'Dim row5 As New DataGridViewRow()
        'DataGridView1.Rows.Add(row5)

        '' Set values for the first three columns in the current row
        'DataGridView1.Rows(5).Cells("Column1").Value = "Power"
        'DataGridView1.Rows(5).Cells("Column2").Value = "Sanjay Gupta"

        'Dim row6 As New DataGridViewRow()
        'DataGridView1.Rows.Add(row6)

        '' Set values for the first three columns in the current row
        'DataGridView1.Rows(6).Cells("Column1").Value = "Finance"
        'DataGridView1.Rows(6).Cells("Column2").Value = "Anjali Dubey"

        'Dim row7 As New DataGridViewRow()
        'DataGridView1.Rows.Add(row7)

        '' Set values for the first three columns in the current row
        'DataGridView1.Rows(7).Cells("Column1").Value = "Broadcasting"
        'DataGridView1.Rows(7).Cells("Column2").Value = "Vikram Chauhan"

        'Dim row8 As New DataGridViewRow()
        'DataGridView1.Rows.Add(row8)

        '' Set values for the first three columns in the current row
        'DataGridView1.Rows(8).Cells("Column1").Value = "IT"
        'DataGridView1.Rows(8).Cells("Column2").Value = "Ritu Verma
        '
        LoadandBindDataGridView()
    End Sub

    Private Sub LoadandBindDataGridView()
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



        Dim updateCmd As New MySqlCommand("SELECT users.name, m.ministry_name
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

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
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