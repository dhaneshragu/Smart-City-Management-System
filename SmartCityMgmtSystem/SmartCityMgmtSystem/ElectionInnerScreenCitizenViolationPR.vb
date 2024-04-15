Imports System.Data.SqlClient
Imports System.Management.Instrumentation
Imports MySql.Data.MySqlClient
Imports SmartCityMgmtSystem.ElectionInnerScreenCitizenViolation
Public Class ElectionInnerScreenCitizenViolationPR
    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Public Property innerPanel As Panel

    Dim electionInnerScreenCitizenViolation As ElectionInnerScreenCitizenViolation = Nothing
    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        LoadandBindDataGridView()
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

        cmd = New MySqlCommand("SELECT vr.report_id, u.name, vr.violation_text, vr.response
                                FROM violations_reported vr
                                JOIN users u ON vr.candidate_uid = u.user_id
                                WHERE vr.reporter_uid = " & ElectionDashboard.uid & " AND vr.election_id = " & ElectionInnerScreenCitizenViolation.LastElectionIDpass & ";", Con)
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
        DataGridView1.Columns(1).DataPropertyName = "name"
        DataGridView1.Columns(2).DataPropertyName = "violation_text"
        DataGridView1.Columns(3).DataPropertyName = "response"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        electionInnerScreenCitizenViolation?.Dispose()
        electionInnerScreenCitizenViolation = New ElectionInnerScreenCitizenViolation With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenCitizenViolation)
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class