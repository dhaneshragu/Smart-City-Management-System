Imports MySql.Data.MySqlClient

Public Class Service_search_results
    Public Property uid As Integer = 1
    Public Property u_name As String = "Ashish Bharti"
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label3.Text = "Unique Identification No: " & uid
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim selectedOption As String = ComboBox2.SelectedItem.ToString()

        Dim sortBy As String = ""
        Select Case selectedOption
            Case "By Price"
                sortBy = "service_charge"
            Case "By Date/Time"
                sortBy = "start_time"
            Case Else
                ' Handle unexpected selection
                Exit Sub
        End Select

        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = $"SELECT user_id AS ID,
                   service_desc AS Description,
                   department AS Department,
                   service_offered AS ServiceOffered,
                   service_charge AS Charge,
                   start_time AS StartTime,
                   end_time AS EndTime
                    FROM service_desc
                    WHERE LOWER(department) = LOWER(@DepartmentParam)
                    OR LOWER(service_desc) LIKE LOWER(CONCAT('%', @DescriptionParam, '%'))
                    ORDER BY {sortBy};"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@DescriptionParam", If(String.IsNullOrEmpty(TextBox1.Text), DBNull.Value, TextBox1.Text))
                    cmd.Parameters.AddWithValue("@DepartmentParam", ComboBox1.Text.ToLower())

                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        ' Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "ID"
        DataGridView1.Columns(1).DataPropertyName = "ServiceOffered"
        DataGridView1.Columns(2).DataPropertyName = "Department"
        DataGridView1.Columns(3).DataPropertyName = "Charge"
        DataGridView1.Columns(4).DataPropertyName = "StartTime"
        DataGridView1.Columns(5).DataPropertyName = "EndTime"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub


    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim servicePortal = New Service_Portal() With {
            .uid = uid,
            .u_name = u_name
        }
        Me.Hide()
        servicePortal.Show()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dataTable As New DataTable()
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT user_id AS ID,
                   service_desc AS Description,
                   department AS Department,
                   service_offered AS ServiceOffered,
                   service_charge AS Charge,
                   start_time AS StartTime,
                   end_time AS EndTime
                    FROM service_desc
                    WHERE LOWER(department) = LOWER(@DepartmentParam)
                    OR LOWER(service_desc) LIKE LOWER(CONCAT('%', @DescriptionParam, '%'));"


                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@DescriptionParam", If(String.IsNullOrEmpty(TextBox1.Text), DBNull.Value, TextBox1.Text))
                    cmd.Parameters.AddWithValue("@DepartmentParam", ComboBox1.Text.ToLower())

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
        DataGridView1.Columns(1).DataPropertyName = "ServiceOffered"
        DataGridView1.Columns(2).DataPropertyName = "Department"
        DataGridView1.Columns(3).DataPropertyName = "Charge"
        DataGridView1.Columns(4).DataPropertyName = "StartTime"
        DataGridView1.Columns(5).DataPropertyName = "EndTime"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
        'DataGridView1.Visible = True
    End Sub
End Class
