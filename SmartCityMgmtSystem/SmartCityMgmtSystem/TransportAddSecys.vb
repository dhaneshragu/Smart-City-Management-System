Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class TransportAddSecys
    Private previousPosition As String = "" 'To store previous position
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is in the "EditBut" column and not a header cell
        If e.ColumnIndex = DataGridView1.Columns("EditBut").Index AndAlso e.RowIndex >= 0 Then
            TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            NumericUpDown1.Value = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            NumericUpDown1.ReadOnly = True
            NumericUpDown1.Increment = 0
            Label3.Text = "Update Officers"
            Button1.Text = "Update"
            ComboBox1.SelectedItem = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            previousPosition = DataGridView1.Rows(e.RowIndex).Cells(2).Value

            ' Check if the clicked cell is in the "DeleteBut" column and not a header cell
        ElseIf e.ColumnIndex = DataGridView1.Columns("DeleteBut").Index AndAlso e.RowIndex >= 0 Then
            ' Perform the action for the "DeleteButton" column
            ' Confirm deletion with the user
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to revoke this position?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then

                ' Call a method to delete the row from the database using the primary key
                Dim query As String = "DELETE FROM admin_officers WHERE uid = " & DataGridView1.Rows(e.RowIndex).Cells(0).Value & " AND position = '" & DataGridView1.Rows(e.RowIndex).Cells(2).Value & "';"
                Dim success As Boolean = Globals.ExecuteDeleteQuery(query)
                If success Then
                    ' If deletion is successful, then refresh the datagridview
                    LoadandBindDataGridView()
                End If

            End If

        End If
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

        cmd = New MySqlCommand("SELECT ad.uid, users.name , ad.position FROM admin_officers ad JOIN users ON ad.uid = users.user_id", Con)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "uid"
        DataGridView1.Columns(1).DataPropertyName = "name"
        DataGridView1.Columns(2).DataPropertyName = "position"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
        TextBox1.Text = "User Not Found"
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        TextBox1.Text = ""
        Try
            Using connection As New MySqlConnection(Globals.getdbConnectionString())
                connection.Open()
                Dim query As String = "SELECT name FROM users WHERE user_id = @Uid"
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Uid", NumericUpDown1.Value)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            TextBox1.Text = reader.GetString("name")
                        Else
                            TextBox1.Text = "User Not Found"
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            TextBox1.Text = "User Not Found"
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex = -1 Then
            ' No item is selected
            MessageBox.Show("No Role is Selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Label3.Text = "Add Officers"
            Button1.Text = "Add"
            NumericUpDown1.Value = 0
            ComboBox1.SelectedIndex = -1
            Return
        End If

        If TextBox1.Text = "User Not Found" Then
            ' No item is selected
            MessageBox.Show("Enter a valid UID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Label3.Text = "Add Officers"
            Button1.Text = "Add"
            NumericUpDown1.Value = 0
            ComboBox1.SelectedIndex = -1
            Return
        End If

        If Button1.Text = "Update" Then
            Dim cmd As String
            cmd = "UPDATE admin_officers SET position = '" & ComboBox1.SelectedItem.ToString & "' WHERE uid = " & NumericUpDown1.Value & " AND position = '" & previousPosition & "';"
            Dim success As Boolean = Globals.ExecuteUpdateQuery(cmd)
            If success Then
                LoadandBindDataGridView()
            End If
            Label2.Text = "Add Officers"
            Button1.Text = "Add"
            NumericUpDown1.Value = 0
            NumericUpDown1.ReadOnly = False
            NumericUpDown1.Increment = 1
            ComboBox1.SelectedIndex = -1
        Else
            Dim cmd As String
            cmd = "INSERT into admin_officers (uid, position) VALUES (" & NumericUpDown1.Value & ",'" & ComboBox1.SelectedItem.ToString & "')"
            Dim success As Boolean = Globals.ExecuteInsertQuery(cmd)
            If success Then
                LoadandBindDataGridView()
            End If
            ComboBox1.SelectedIndex = -1
            NumericUpDown1.Value = 0
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label3.Text = "Add Officers"
        Button1.Text = "Add"
        NumericUpDown1.Value = 0
        ComboBox1.SelectedIndex = -1
        TextBox1.Text = "User Not Found"
    End Sub
End Class