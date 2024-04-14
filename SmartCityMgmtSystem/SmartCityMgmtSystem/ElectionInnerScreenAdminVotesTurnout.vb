Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreenAdminVotesTurnout

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Public Property innerPanel As Panel

    Dim electionInnerScreenAdminVotes As ElectionInnerScreenAdminVotes = Nothing
    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
    End Sub

    Private Sub LoadandBindDataGridView()
        ''Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Retrieve the value of the election_id column from the last row of the election_time table
        Dim lastElectionID As Integer = 0 ' Default value in case there are no rows in election_time
        cmd = New MySqlCommand("SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            lastElectionID = Convert.ToInt32(reader("election_id"))
        End If
        reader.Close()

        ' Use the last election_id value to filter rows in the candidate_register table
        cmd = New MySqlCommand("SELECT ministries.ministry_name, votes_received 
                                FROM turnout
                                JOIN ministries ON ministries.ministry_id = turnout.ministry_id
                                WHERE election_id = @electionID;", Con)
        cmd.Parameters.AddWithValue("@electionID", lastElectionID)
        reader = cmd.ExecuteReader()

        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()
        ' Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()

        ' Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "ministry_name"
        DataGridView1.Columns(1).DataPropertyName = "votes_received"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        electionInnerScreenAdminVotes?.Dispose()
        electionInnerScreenAdminVotes = New ElectionInnerScreenAdminVotes With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenAdminVotes)
    End Sub
End Class