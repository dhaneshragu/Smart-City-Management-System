Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class BankAdminTransactions

    Private Sub LoadandBindDataGridView(query As String)
        'Get connection String from globals
        Dim conString = Globals.getdbConnectionString()
        Dim Con = New SqlConnection(conString)

        'Query for SQL table
        'Dim query = "SELECT * FROM transactions"
        'Dim connection As New MySqlConnection(Globals.getdbConnectionString())
        Using connection As New MySqlConnection(Globals.getdbConnectionString())
            connection.Open()

            Dim cmd As New MySqlCommand(query, connection)
            Dim adapter As New MySqlDataAdapter(cmd)

            ' Create a DataTable to store the data
            Dim dataTable As New DataTable()

            'Fill the DataTable with data from the SQL table
            adapter.Fill(dataTable)

            'IMP: Specify the Column Mappings from DataGridView to SQL Table
            DataGridView1.AutoGenerateColumns = False
            DataGridView1.Columns(0).DataPropertyName = "transaction_id"
            DataGridView1.Columns(1).DataPropertyName = "sender_account"
            DataGridView1.Columns(2).DataPropertyName = "receiver_account"
            DataGridView1.Columns(3).DataPropertyName = "time"
            DataGridView1.Columns(4).DataPropertyName = "amount"

            ' Bind the data to DataGridView
            DataGridView1.DataSource = dataTable
        End Using
    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' Dummy Data, Change it to LoadandBindDataGridView() 
        Dim query = "SELECT * FROM transactions"
        LoadandBindDataGridView(query)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim query = "SELECT * FROM transactions"
        TextBox1.Clear()
        TextBox2.Clear()
        LoadandBindDataGridView(query)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim query = "SELECT * FROM transactions"
        Dim id, amt As Integer
        If Not String.IsNullOrWhiteSpace(TextBox1.Text) AndAlso Integer.TryParse(TextBox1.Text, id) Then
            query &= " WHERE sender_account = " & id
            If Not String.IsNullOrWhiteSpace(TextBox2.Text) AndAlso Integer.TryParse(TextBox2.Text, amt) Then
                query &= " AND amount >= " & amt
            End If
        ElseIf Not String.IsNullOrWhiteSpace(TextBox2.Text) AndAlso Integer.TryParse(TextBox2.Text, amt) Then
            query &= " WHERE amount >= " & amt
        ElseIf Not String.IsNullOrWhiteSpace(TextBox1.Text) AndAlso Not Integer.TryParse(TextBox1.Text, id) Then
            MessageBox.Show("Invalid integer format. Please enter a valid Account ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf Not String.IsNullOrWhiteSpace(TextBox2.Text) AndAlso Not Integer.TryParse(TextBox2.Text, amt) Then
            MessageBox.Show("Invalid integer format. Please enter a valid amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        'MessageBox.Show(query)
        LoadandBindDataGridView(query)
    End Sub
End Class