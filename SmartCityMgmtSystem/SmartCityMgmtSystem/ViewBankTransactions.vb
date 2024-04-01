Imports MySql.Data.MySqlClient

Public Class ViewBankTransactions
    Public Property accn As Integer = 1

    Private Sub LoadandBindDataGridView()
        ' Get connection from globals
        Dim Con As MySqlConnection = Globals.GetDBConnection()

        Try
            Con.Open()

            ' Create a MySqlCommand
            Dim cmd As MySqlCommand = Con.CreateCommand()

            ' Set the SQL query to fetch transactions where sender or receiver account is equal to accn
            cmd.CommandText = "SELECT * FROM transactions WHERE sender_account = @Accn OR receiver_account = @Accn"
            cmd.Parameters.AddWithValue("@Accn", accn)

            ' Execute the SQL query and fetch the data
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            ' Create a DataTable to store the data
            Dim dataTable As New DataTable()

            ' Load the data into the DataTable
            dataTable.Load(reader)

            ' Clear existing columns and data from DataGridView
            DataGridView1.Columns.Clear()
            DataGridView1.DataSource = Nothing

            ' Bind the data to DataGridView
            DataGridView1.DataSource = dataTable

            ' Optionally, you can define specific column properties here
            ' For example:
            ' DataGridView1.Columns("sender_account").HeaderText = "Sender Account"
            ' DataGridView1.Columns("amount").DefaultCellStyle.Format = "C2" ' Format currency

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close the connection
            Con.Close()
        End Try
    End Sub

    Private Sub ViewBankTransactions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
    End Sub
End Class
