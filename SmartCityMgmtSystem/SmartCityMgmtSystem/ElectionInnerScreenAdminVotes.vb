Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreenAdminVotes

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Dim electionInnerScreenAdmin As ElectionInnerScreenAdmin = Nothing
    Public Property innerPanel As Panel
    Dim electionInnerScreenAdminVotesTurnout As ElectionInnerScreenAdminVotesTurnout = Nothing
    ' Create a new dictionary with Integer keys and String values
    Dim ministryToId As New Dictionary(Of String, Integer)

    Private Sub LoadandBindDataGridView()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
            cmd = New MySqlCommand("SELECT election_id, candidate_uid, name, ministry_name,votes
                                FROM candidate_register
                                JOIN users ON users.user_id = candidate_register.candidate_uid
                                JOIN ministries ON ministries.ministry_id = candidate_register.ministry_id
                                WHERE status = ""Approved"";", Con)
            reader = cmd.ExecuteReader
            ' Create a DataTable to store the data
            Dim dataTable As New DataTable()

            'Fill the DataTable with data from the SQL table
            dataTable.Load(reader)
            reader.Close()
            Con.Close()

            'IMP: Specify the Column Mappings from DataGridView to SQL Table
            DataGridView1.AutoGenerateColumns = False
            DataGridView1.Columns(0).DataPropertyName = "election_id"
            DataGridView1.Columns(1).DataPropertyName = "candidate_uid"
            DataGridView1.Columns(2).DataPropertyName = "name"
            DataGridView1.Columns(3).DataPropertyName = "ministry_name"
            DataGridView1.Columns(4).DataPropertyName = "votes"

            ' Bind the data to DataGridView
            DataGridView1.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        electionInnerScreenAdmin?.Dispose()
        electionInnerScreenAdmin = New ElectionInnerScreenAdmin With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenAdmin)
    End Sub


    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        electionInnerScreenAdminVotesTurnout?.Dispose()
        electionInnerScreenAdminVotesTurnout = New ElectionInnerScreenAdminVotesTurnout With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenAdminVotesTurnout)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim selectedValue1 As Object = ComboBox1.SelectedItem

        If selectedValue1 IsNot Nothing Then
            Dim position As String = selectedValue1.ToString()
            'Get connection from globals
            Dim Con = Globals.GetDBConnection()
            Dim reader As MySqlDataReader
            Dim cmd As MySqlCommand

            Try
                Con.Open()
                cmd = New MySqlCommand("SELECT election_id, candidate_uid, name, ministry_name, agenda, status 
                                    FROM candidate_register
                                    JOIN users ON users.user_id = candidate_register.candidate_uid
                                    JOIN ministries ON ministries.ministry_id = candidate_register.ministry_id
                                    WHERE status = ""Approved"" and ministries.ministry_id = " & ministryToId(position) & ";", Con)
                reader = cmd.ExecuteReader
                ' Create a DataTable to store the data
                Dim dataTable As New DataTable()

                'Fill the DataTable with data from the SQL table
                dataTable.Load(reader)
                reader.Close()
                Con.Close()

                'IMP: Specify the Column Mappings from DataGridView to SQL Table
                DataGridView1.AutoGenerateColumns = False
                DataGridView1.Columns(0).DataPropertyName = "election_id"
                DataGridView1.Columns(1).DataPropertyName = "candidate_uid"
                DataGridView1.Columns(2).DataPropertyName = "name"
                DataGridView1.Columns(3).DataPropertyName = "ministry_name"
                DataGridView1.Columns(4).DataPropertyName = "votes"

                ' Bind the data to DataGridView
                DataGridView1.DataSource = dataTable
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try


        End If
    End Sub
End Class