Imports System.Data.SqlClient
Imports System.Runtime.Remoting
Imports MySql.Data.MySqlClient
Public Class Service_Withdraw
    Public Property uid As Integer = 1
    Public Property u_name As String = "Ashish Bharti"
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Service_Withdraw_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT user_id AS ID,
                                    department AS dept, 
                                    service_desc AS description, 
                                    service_charge AS charge
                                    FROM service_desc 
                                    WHERE user_id = @serviceID;"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@serviceID", uid) ' Replace userIdValue with the actual user ID

                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)


                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "ID"
        DataGridView1.Columns(1).DataPropertyName = "description"
        DataGridView1.Columns(2).DataPropertyName = "dept"
        DataGridView1.Columns(3).DataPropertyName = "charge"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()
                Dim rowsToRemove As New List(Of DataGridViewRow)
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If CBool(row.Cells("Column6").Value) Then

                        Dim sql As String = "DELETE FROM service_desc
                                             WHERE user_id = @userID;"


                        Using cmd As New MySqlCommand(sql, con)
                            cmd.Parameters.AddWithValue("@userID", uid)

                            cmd.ExecuteNonQuery()

                        End Using
                        rowsToRemove.Add(row)
                    End If
                Next
                For Each row As DataGridViewRow In rowsToRemove
                    DataGridView1.Rows.Remove(row)
                Next
                MessageBox.Show("Service Withdrawn Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
End Class