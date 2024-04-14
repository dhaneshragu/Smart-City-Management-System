Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class HealthcareManageStaffAdmn

    Private primaryKeyEdit As String ' Define a class-level variable to hold the primary key of the row being edited

    Private Sub ShowEditOption(ByVal txt6 As String, ByVal txt3 As String, ByVal txt2 As String, ByVal txt1 As String, ByVal txt7 As String, ByVal txt5 As String, ByVal txt4 As String)
        Label3.Text = "Update Staff Information"
        Button1.Text = "Update"
        TextBox6.Text = txt6
        TextBox3.Text = txt3
        TextBox2.Text = txt2
        TextBox1.Text = txt1
        TextBox7.Text = txt7
        TextBox5.Text = txt5
        TextBox4.Text = txt4
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
                Dim success As Boolean = Globals.ExecuteDeleteQuery("DELETE FROM hc_staff where staff_id = " & DataGridView1.Rows(e.RowIndex).Cells(0).Value)

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

        cmd = New MySqlCommand("SELECT * FROM hc_staff", Con)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "staff_id"
        DataGridView1.Columns(1).DataPropertyName = "hospital_id"
        DataGridView1.Columns(2).DataPropertyName = "department_id"
        DataGridView1.Columns(3).DataPropertyName = "sname"
        DataGridView1.Columns(4).DataPropertyName = "phone_number"
        DataGridView1.Columns(5).DataPropertyName = "age"
        DataGridView1.Columns(6).DataPropertyName = "gender"


        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label3.Text = "Add Staff Information"
        Button1.Text = "Add"
        TextBox2.Clear()
        TextBox1.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrWhiteSpace(TextBox4.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrWhiteSpace(TextBox5.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrWhiteSpace(TextBox6.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrWhiteSpace(TextBox7.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If Button1.Text = "Update" Then
            Dim cmd As String
            cmd = "UPDATE hc_staff SET staff_id = " & Convert.ToInt32(TextBox6.Text) & " , hospital_id =" & Convert.ToInt32(TextBox3.Text) & " , department_id = " & Convert.ToInt32(TextBox2.Text) & ", sname = '" & TextBox1.Text & "', phone_number = '" & TextBox7.Text & "', age = " & Convert.ToInt32(TextBox5.Text) & ", gender = '" & TextBox4.Text & "' WHERE staff_id =" & primaryKeyEdit
            Dim success As Boolean = Globals.ExecuteUpdateQuery(cmd)
            If success Then
                LoadandBindDataGridView()

            End If
            Label3.Text = "Add Staff Information"
            Button1.Text = "Add"
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
        Else
            Dim cmd As String
            cmd = "INSERT into hc_staff(staff_id,hospital_id,department_id,sname,phone_number,age,gender) VALUES (" & Convert.ToInt32(TextBox6.Text) & "," & Convert.ToInt32(TextBox3.Text) & "," & Convert.ToInt32(TextBox2.Text) & ",'" & TextBox1.Text & "','" & TextBox7.Text & "'," & Convert.ToInt32(TextBox5.Text) & ",'" & TextBox4.Text & "')"
            Dim success As Boolean = Globals.ExecuteInsertQuery(cmd)
            If success Then
                LoadandBindDataGridView()
            End If
            TextBox2.Clear()
            TextBox1.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
        End If
    End Sub



End Class