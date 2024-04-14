Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Healthcare_History
    Public Property uid As Integer = 130
    Private Sub d1_Click(sender As Object, e As EventArgs) Handles d1.Click
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT * FROM appointments", Con)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "time"
        DataGridView1.Columns(1).DataPropertyName = "hospital_ID"
        DataGridView1.Columns(2).DataPropertyName = "doctor_ID"
        DataGridView1.Columns(3).DataPropertyName = "status"
        DataGridView1.Columns(4).DataPropertyName = "patient_id"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub d2_Click(sender As Object, e As EventArgs) Handles d2.Click

    End Sub
    Private Sub d3_Click(sender As Object, e As EventArgs) Handles d3.Click

    End Sub

End Class