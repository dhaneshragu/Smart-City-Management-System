Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class HealthcareMedicineInventoryAdmin

    Private primaryKeyEdit As String ' Define a class-level variable to hold the primary key of the row being edited

    Private Sub ShowEditOption(ByVal txt6 As String, ByVal txt3 As String, ByVal txt2 As String, ByVal txt1 As String, ByVal txt7 As String, ByVal txt5 As String, ByVal txt4 As String)
        Label3.Text = "Update Medicine Details"
        Button1.Text = "Update"
        TextBox6.Text = txt6
        TextBox3.Text = txt3
        TextBox2.Text = txt2
        TextBox1.Text = txt1
        TextBox7.Text = txt7
        DateTimePicker1.Text = txt5
        DateTimePicker2.Text = txt4

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is in the "EditBut" column and not a header cell
        If e.ColumnIndex = DataGridView1.Columns("EditBut").Index AndAlso e.RowIndex >= 0 Then
            ' Change this to DB logic later
            'Show Edit Option
            primaryKeyEdit = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            ShowEditOption(DataGridView1.Rows(e.RowIndex).Cells(0).Value, DataGridView1.Rows(e.RowIndex).Cells(1).Value, DataGridView1.Rows(e.RowIndex).Cells(2).Value, DataGridView1.Rows(e.RowIndex).Cells(3).Value, DataGridView1.Rows(e.RowIndex).Cells(4).Value, DataGridView1.Rows(e.RowIndex).Cells(5).Value, DataGridView1.Rows(e.RowIndex).Cells(6).Value)
            ' Check if the clicked cell is in the "DeleteBut" column and not a header cell
        ElseIf e.ColumnIndex = DataGridView1.Columns("DeleteBut").Index AndAlso e.RowIndex >= 0 Then
            ' Perform the action for the "DeleteButton" column
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this entry?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then

                ' Call a method to delete the row from the database using the primary key
                Dim success As Boolean = Globals.ExecuteDeleteQuery("DELETE FROM medicine where medicine_id = " & DataGridView1.Rows(e.RowIndex).Cells(0).Value)

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

        cmd = New MySqlCommand("SELECT * FROM medicine", Con)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "medicine_id"
        DataGridView1.Columns(1).DataPropertyName = "pharmacy_id"
        DataGridView1.Columns(2).DataPropertyName = "medicine_name"
        DataGridView1.Columns(3).DataPropertyName = "quantity"
        DataGridView1.Columns(4).DataPropertyName = "price"
        DataGridView1.Columns(5).DataPropertyName = "manufactured_date"
        DataGridView1.Columns(6).DataPropertyName = "expiry_date"


        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label3.Text = "Add Medicine Details"
        Button1.Text = "Add"
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox7.Clear()
        TextBox6.Clear()
        TextBox1.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If String.IsNullOrWhiteSpace(TextBox6.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrWhiteSpace(TextBox7.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Button1.Text = "Update" Then
            Dim cmd As String
            cmd = "UPDATE medicine SET medicine_id = " & Convert.ToInt32(TextBox6.Text) & " , pharmacy_id =" & Convert.ToInt32(TextBox3.Text) & " , medicine_name = '" & TextBox2.Text & "', quantity = " & Convert.ToInt32(TextBox1.Text) & ", price = " & Convert.ToInt32(TextBox7.Text) & ", manufactured_date = '" & DateTimePicker1.Value.Date.ToString("yyyyMMdd") & "', expiry_date = '" & DateTimePicker2.Value.Date.ToString("yyyyMMdd") & "' WHERE medicine_id =" & primaryKeyEdit
            Dim success As Boolean = Globals.ExecuteUpdateQuery(cmd)
            If success Then
                LoadandBindDataGridView()

            End If
            Label3.Text = "Add Medicine Details"
            Button1.Text = "Add"
            TextBox6.Clear()
            TextBox3.Clear()
            TextBox2.Clear()
            TextBox1.Clear()
            TextBox7.Clear()
        Else
            Dim cmd As String
            cmd = "INSERT into medicine(medicine_id,pharmacy_id,medicine_name,quantity,price,manufactured_date,expiry_date) VALUES (" & Convert.ToInt32(TextBox6.Text) & "," & Convert.ToInt32(TextBox3.Text) & ",'" & TextBox2.Text & "'," & Convert.ToInt32(TextBox1.Text) & "," & Convert.ToInt32(TextBox7.Text) & ",'" & DateTimePicker1.Value.Date.ToString("yyyyMMdd") & "','" & DateTimePicker2.Value.Date.ToString("yyyyMMdd") & "')"
            Dim success As Boolean = Globals.ExecuteInsertQuery(cmd)
            If success Then
                LoadandBindDataGridView()
            End If
            TextBox6.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox1.Clear()
            TextBox7.Clear()
        End If
    End Sub

End Class