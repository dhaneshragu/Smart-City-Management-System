Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient


Public Class ElectionInnerScreenAdminRTI

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Dim electionInnerScreenAdmin As ElectionInnerScreenAdmin = Nothing

    Public Property innerPanel As Panel

    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()

        ' Assuming dgv is your DataGridView and columnIndex is the index of the column containing the text
        DataGridView1.Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(4).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        'DataGridView1.Columns(5).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(6).DefaultCellStyle.WrapMode = DataGridViewTriState.True




    End Sub

    Private Sub LoadandBindDataGridView()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()

        Try
            Con.Open()
            Dim cmd As New MySqlCommand("SELECT query_id, citizen_uid, name, ministry_name, query, status, response 
                                    FROM rti_queries_table
                                    JOIN ministries ON ministries.ministry_id = rti_queries_table.ministry
                                    JOIN users ON users.user_id = rti_queries_table.citizen_uid;", Con)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            ' Create a DataTable to store the data
            Dim dataTable As New DataTable()

            'Fill the DataTable with data from the SQL table
            dataTable.Load(reader)
            reader.Close()
            Con.Close()

            'IMP: Specify the Column Mappings from DataGridView to SQL Table
            DataGridView1.AutoGenerateColumns = False
            DataGridView1.Columns(0).DataPropertyName = "query_id"
            DataGridView1.Columns(1).DataPropertyName = "citizen_uid"
            DataGridView1.Columns(2).DataPropertyName = "name"
            DataGridView1.Columns(3).DataPropertyName = "ministry_name"
            DataGridView1.Columns(4).DataPropertyName = "query"
            DataGridView1.Columns(5).DataPropertyName = "status"
            DataGridView1.Columns(6).DataPropertyName = "response"

            ' Bind the data to DataGridView
            DataGridView1.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


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

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        ' Check if the value changed in the ComboBox column and it's not a header row
        If e.ColumnIndex = 5 AndAlso e.RowIndex <> -1 Then
            Dim selectedValue As String = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
            ' You can perform any other actions here based on the changed value
            Dim Con = Globals.GetDBConnection()
            Dim reader As MySqlDataReader
            Dim cmd As MySqlCommand

            Try
                Con.Open()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            cmd = New MySqlCommand("UPDATE rti_queries_table SET status = " + """" + selectedValue + """" + " WHERE query_id = " & DataGridView1.Rows(e.RowIndex).Cells(0).Value & ";", Con)
            reader = cmd.ExecuteReader

        End If

        If e.ColumnIndex = 6 AndAlso e.RowIndex <> -1 Then
            Dim selectedValue As String = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
            ' You can perform any other actions here based on the changed value
            Dim Con = Globals.GetDBConnection()
            Dim reader As MySqlDataReader
            Dim cmd As MySqlCommand

            Try
                Con.Open()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            cmd = New MySqlCommand("UPDATE rti_queries_table SET response = " + """" + selectedValue + """" + " WHERE query_id = " & DataGridView1.Rows(e.RowIndex).Cells(0).Value & ";", Con)
            reader = cmd.ExecuteReader

        End If

    End Sub


End Class