Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreenPastElections

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Public Property innerPanel As Panel
    Dim electionInnerScreen1 As ElectionInnerScreen1 = Nothing
    ' Create a new dictionary with Integer keys and String values
    Dim ministryToId As New Dictionary(Of String, Integer)
    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ministryToId.Add("Employment", 1)
        ministryToId.Add("Education", 2)
        ministryToId.Add("Health", 3)
        ministryToId.Add("Transport", 4)
        ministryToId.Add("Culture", 5)
        ministryToId.Add("Power", 6)
        ministryToId.Add("Finance", 7)
        ministryToId.Add("Broadcasting", 8)
        ministryToId.Add("IT", 9)

        LoadandBindDataGridView()
        helpOptionsAdd()
    End Sub

    Private Sub helpOptionsAdd()
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Retrieve the value of the election_id column from the last row of the election_time table
        Dim lastElectionID As Integer = -1 ' Default value in case there are no rows in election_time
        cmd = New MySqlCommand("SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            lastElectionID = Convert.ToInt32(reader("election_id"))
        End If
        reader.Close()

        ComboBox1.Items.Clear()

        For index As Integer = 1 To lastElectionID
            populateComboBox(index)
        Next

        MessageBox.Show("You can now search for previous elections data.")

    End Sub

    Private Sub populateComboBox(ByVal electionId As Integer)
        ' Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand
        Dim announced As Integer

        Try
            Con.Open()
            Dim query As String = "SELECT announced
                                FROM election_time 
                                WHERE election_id = @electionId;"
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@electionId", electionId)
            reader = cmd.ExecuteReader()
            If reader.Read() Then
                announced = Convert.ToInt32(reader("announced"))
            End If
            reader.Close()

            If announced = 1 Then
                Dim id As String = electionId.ToString() ' Get user_id
                ComboBox1.Items.Add(id)
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

    Private Sub LoadandBindDataGridView()
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
        Dim lastElectionID As Integer = -1 ' Default value in case there are no rows in election_time
        cmd = New MySqlCommand("SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            lastElectionID = Convert.ToInt32(reader("election_id"))
        End If
        reader.Close()

        ' Use the last election_id value to filter rows in the candidate_register table
        ''cmd = New MySqlCommand("SELECT election_id, ministry_name, candidate_uid, name, votes, votes_received
        ''                        FROM candidate_register
        ''                        JOIN ministries ON ministries.ministry_id = candidate_register.ministry_id
        ''                        JOIN turnout ON turnout.election_id = candidate_register.election_id AND turnout.ministry_id = candidate_register.ministry_id
        ''                        JOIN users ON users.user_id = candidate_register.candidate_uid
        ''                        WHERE candidate_register.election_id = @electionID;", Con)
        ''cmd.Parameters.AddWithValue("@electionID", lastElectionID)
        ''reader = cmd.ExecuteReader()
        ''
        cmd = New MySqlCommand("SELECT candidate_register.election_id, ministry_name, candidate_uid, name, votes, votes_received
                                FROM candidate_register
                                JOIN ministries ON ministries.ministry_id = candidate_register.ministry_id
                                JOIN turnout ON turnout.election_id = candidate_register.election_id AND turnout.ministry_id = candidate_register.ministry_id
                                JOIN users ON users.user_id = candidate_register.candidate_uid
                                JOIN election_time ON election_time.election_id = candidate_register.election_id
                                WHERE election_time.announced = 1 AND candidate_register.status = 'Approved';", Con)
        reader = cmd.ExecuteReader()

        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()
        ' Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()

        ' Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "election_id"
        DataGridView1.Columns(1).DataPropertyName = "ministry_name"
        DataGridView1.Columns(2).DataPropertyName = "candidate_uid"
        DataGridView1.Columns(3).DataPropertyName = "name"
        DataGridView1.Columns(4).DataPropertyName = "votes"
        DataGridView1.Columns(5).DataPropertyName = "votes_received"

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim selectedValue1 As Object = ComboBox1.SelectedItem

        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If selectedValue1 Is Nothing Then
            MessageBox.Show("Please choose a Position to filter results.")
        Else
            Dim position As String = selectedValue1.ToString()

            cmd = New MySqlCommand("SELECT candidate_register.election_id, ministry_name, candidate_uid, name, votes, votes_received
                                    FROM candidate_register
                                    JOIN ministries ON ministries.ministry_id = candidate_register.ministry_id
                                    JOIN turnout ON turnout.election_id = candidate_register.election_id AND turnout.ministry_id = candidate_register.ministry_id
                                    JOIN users ON users.user_id = candidate_register.candidate_uid
                                    JOIN election_time ON election_time.election_id = candidate_register.election_id
                                    WHERE election_time.announced = 1 AND candidate_register.status = 'Approved' AND election_time.election_id = @electionID;", Con)
            cmd.Parameters.AddWithValue("@electionID", position)
            reader = cmd.ExecuteReader()
            ' Create a DataTable to store the data
            Dim dataTable As New DataTable()

            'Fill the DataTable with data from the SQL table
            dataTable.Load(reader)
            reader.Close()
            Con.Close()

            ' Specify the Column Mappings from DataGridView to SQL Table
            DataGridView1.AutoGenerateColumns = False
            DataGridView1.Columns(0).DataPropertyName = "election_id"
            DataGridView1.Columns(1).DataPropertyName = "ministry_name"
            DataGridView1.Columns(2).DataPropertyName = "candidate_uid"
            DataGridView1.Columns(3).DataPropertyName = "name"
            DataGridView1.Columns(4).DataPropertyName = "votes"
            DataGridView1.Columns(5).DataPropertyName = "votes_received"

            ' Bind the data to DataGridView
            DataGridView1.DataSource = dataTable
        End If

    End Sub
End Class