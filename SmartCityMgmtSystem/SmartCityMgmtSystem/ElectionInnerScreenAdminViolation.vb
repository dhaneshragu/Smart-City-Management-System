Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreenAdminViolation

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Dim electionInnerScreenAdmin As ElectionInnerScreenAdmin = Nothing

    Public Property innerPanel As Panel

    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()

        DataGridView1.Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(4).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(5).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(6).DefaultCellStyle.WrapMode = DataGridViewTriState.True
    End Sub

    Private Sub LoadandBindDataGridView()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()

            ' Execute query to count rows in election_time table
            cmd = New MySqlCommand("SELECT COUNT(*) FROM election_time;", Con)
            Dim electionTimeRowCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            'MessageBox.Show("election time row count " & electionTimeRowCount)

            If electionTimeRowCount = 0 Then
                MessageBox.Show("No elections have been scheduled yet.")
                Exit Sub
            End If

            ' Retrieve the value of the election_id column from the last row of the election_time table
            Dim lastElectionID As Integer = electionTimeRowCount ' Default value in case there are no rows in election_time

            cmd = New MySqlCommand("SELECT v.report_id, v.reporter_uid, reporter.name AS reporter_name, v.candidate_uid, candidate.name AS candidate_name, v.violation_text, v.response
                                    FROM violations_reported v
                                    JOIN users reporter ON v.reporter_uid = reporter.user_id
                                    JOIN users candidate ON v.candidate_uid = candidate.user_id
                                    WHERE election_id = @lastElectionID;", Con)
            cmd.Parameters.AddWithValue("@lastElectionID", lastElectionID)
            reader = cmd.ExecuteReader()

            ' Create a DataTable to store the data
            Dim dataTable As New DataTable()

            'Fill the DataTable with data from the SQL table
            dataTable.Load(reader)
            reader.Close()
            Con.Close()

            'IMP: Specify the Column Mappings from DataGridView to SQL Table
            DataGridView1.AutoGenerateColumns = False
            DataGridView1.Columns(0).DataPropertyName = "report_id"
            DataGridView1.Columns(1).DataPropertyName = "reporter_uid"
            DataGridView1.Columns(2).DataPropertyName = "reporter_name"
            DataGridView1.Columns(3).DataPropertyName = "candidate_uid"
            DataGridView1.Columns(4).DataPropertyName = "candidate_name"
            DataGridView1.Columns(5).DataPropertyName = "violation_text"
            DataGridView1.Columns(6).DataPropertyName = "response"

            ' Bind the data to DataGridView
            DataGridView1.DataSource = dataTable

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        ' Check if the value changed in the ComboBox column and it's not a header row
        'If e.ColumnIndex = 5 AndAlso e.RowIndex <> -1 Then
        '    Dim selectedValue As String = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
        '    ' You can perform any other actions here based on the changed value
        '    Dim Con = Globals.GetDBConnection()
        '    Dim reader As MySqlDataReader
        '    Dim cmd As MySqlCommand

        '    Try
        '        Con.Open()
        '    Catch ex As Exception
        '        MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End Try

        '    cmd = New MySqlCommand("UPDATE rti_queries_table SET status = " + """" + selectedValue + """" + " WHERE query_id = " & DataGridView1.Rows(e.RowIndex).Cells(0).Value & ";", Con)
        '    reader = cmd.ExecuteReader

        'End If

        If e.ColumnIndex = 6 AndAlso e.RowIndex <> -1 Then
            Dim selectedValue As String = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
            ' You can perform any other actions here based on the changed value
            Dim Con = Globals.GetDBConnection()
            Dim reader As MySqlDataReader
            Dim cmd As MySqlCommand

            Try
                Con.Open()
                cmd = New MySqlCommand("UPDATE violations_reported SET response = " + """" + selectedValue + """" + " WHERE report_id = " & DataGridView1.Rows(e.RowIndex).Cells(0).Value & ";", Con)
                reader = cmd.ExecuteReader
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try



        End If

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
End Class