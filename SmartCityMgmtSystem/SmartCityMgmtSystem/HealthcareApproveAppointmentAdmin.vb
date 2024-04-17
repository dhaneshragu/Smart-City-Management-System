Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class HealthcareApproveAppointmentAdmin

    Private primaryKeyEdit As String ' Define a class-level variable to hold the primary key of the row being edited
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
        DataGridView1.Columns(0).DataPropertyName = "Appointment_id"
        DataGridView1.Columns(1).DataPropertyName = "status"


        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If


        'Dim cmd As String
        'Dim cbox As String = ComboBox1.SelectedValue
        'cmd = "UPDATE appointments SET Appointment_id = " & Convert.ToInt32(TextBox1.Text) & ", status = @cbox  WHERE Appointment_id =" & Convert.ToInt32(TextBox1.Text)
        'c
        'Dim success As Boolean = Globals.ExecuteUpdateQuery(cmd)
        'If success Then
        '        LoadandBindDataGridView()
        '    End If
        'ComboBox1.SelectedIndex = -1
        'TextBox1.Clear()

        Dim cmd As New MySqlCommand
        Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())


            Dim stmnt As String = "UPDATE appointments SET Appointment_id = @value1  , status = @value2  WHERE Appointment_id = @value1"

            cmd = New MySqlCommand(stmnt, con)
            cmd.Parameters.AddWithValue("@value1", TextBox1.Text)
            cmd.Parameters.AddWithValue("@value2", ComboBox1.SelectedItem.ToString)


            Try
                con.Open()
                cmd.ExecuteNonQuery()
                LoadandBindDataGridView()

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
            con.Close()

        End Using
        ComboBox1.SelectedIndex = -1
        TextBox1.Clear()
    End Sub


End Class