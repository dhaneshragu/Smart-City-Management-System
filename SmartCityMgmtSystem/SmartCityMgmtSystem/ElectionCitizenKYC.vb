Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Public Class ElectionCitizenKYC

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Public Property innerPanel As Panel

    Dim electionInnerScreen1 As ElectionInnerScreen1 = Nothing

    Dim ministryToId As New Dictionary(Of String, Integer)
    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each column As DataGridViewColumn In DataGridView1.Columns
            column.Frozen = False
        Next

        'DataGridView1.RowTemplate.Height = 300
        DataGridView1.ScrollBars = ScrollBars.Both
        LoadandBindDataGridView()

        ministryToId.Add("Employment", 1)
        ministryToId.Add("Education", 2)
        ministryToId.Add("Health", 3)
        ministryToId.Add("Transport", 4)
        ministryToId.Add("Culture", 5)
        ministryToId.Add("Power", 6)
        ministryToId.Add("Finance", 7)
        ministryToId.Add("Broadcasting", 8)
        ministryToId.Add("IT", 9)
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
        cmd = New MySqlCommand("SELECT election_id, candidate_uid, name, ministry_name, agenda, status 
                        FROM candidate_register
                        JOIN users ON users.user_id = candidate_register.candidate_uid
                        JOIN ministries ON ministries.ministry_id = candidate_register.ministry_id
                        WHERE election_id = @electionID AND status = ""Approved"";", Con)
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
        DataGridView1.Columns(0).DataPropertyName = "candidate_uid"
        DataGridView1.Columns(1).DataPropertyName = "name"
        DataGridView1.Columns(2).DataPropertyName = "ministry_name"
        DataGridView1.Columns(3).DataPropertyName = "agenda"

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

        ' Retrieve the value of the election_id column from the last row of the election_time table
        Dim lastElectionID As Integer = -1 ' Default value in case there are no rows in election_time
        cmd = New MySqlCommand("SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            lastElectionID = Convert.ToInt32(reader("election_id"))
        End If
        reader.Close()

        If selectedValue1 Is Nothing Then
            MessageBox.Show("Please choose a Position to filter results.")
        Else
            Dim position As String = selectedValue1.ToString()

            cmd = New MySqlCommand("SELECT candidate_uid, name, ministry_name, agenda
                                    FROM candidate_register
                                    JOIN users ON users.user_id = candidate_register.candidate_uid
                                    JOIN ministries ON ministries.ministry_id = candidate_register.ministry_id
                                    WHERE ministries.ministry_id = " & ministryToId(position) & " AND election_id = @electionID AND status = ""Approved"";", Con)
            cmd.Parameters.AddWithValue("@electionID", lastElectionID)
            reader = cmd.ExecuteReader
            ' Create a DataTable to store the data
            Dim dataTable As New DataTable()

            'Fill the DataTable with data from the SQL table
            dataTable.Load(reader)
            reader.Close()
            Con.Close()

            'IMP: Specify the Column Mappings from DataGridView to SQL Table
            DataGridView1.AutoGenerateColumns = False
            DataGridView1.Columns(0).DataPropertyName = "candidate_uid"
            DataGridView1.Columns(1).DataPropertyName = "name"
            DataGridView1.Columns(2).DataPropertyName = "ministry_name"
            DataGridView1.Columns(3).DataPropertyName = "agenda"

            ' Bind the data to DataGridView
            DataGridView1.DataSource = dataTable
        End If
    End Sub
End Class