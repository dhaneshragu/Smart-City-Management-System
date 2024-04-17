Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports System.Collections.Generic
Imports Google.Protobuf.WellKnownTypes
Imports System.Text.RegularExpressions
Imports System.Diagnostics.Eventing
Imports System.Threading


Public Class ElectionInnerScreenAdminNomination

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"

    Public Property innerPanel As Panel
    Dim electionInnerScreenAdmin As ElectionInnerScreenAdmin = Nothing
    ' Create a new dictionary with Integer keys and String values
    Dim ministryToId As New Dictionary(Of String, Integer)

    ' Add items to the dictionary

    Private Sub LoadandBindDataGridView()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()


            ' Retrieve the value of the election_id column from the last row of the election_time table
            Dim lastElectionID As Integer = -1 ' Default value in case there are no rows in election_time
            cmd = New MySqlCommand("SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
            reader = cmd.ExecuteReader()
            If reader.Read() Then
                lastElectionID = Convert.ToInt32(reader("election_id"))
            End If
            reader.Close()

            ' Use the last election_id value to filter rows in the candidate_register table
            cmd = New MySqlCommand("SELECT election_id, candidate_uid, name, ministry_name, agenda, status 
                        FROM candidate_register
                        JOIN users ON users.user_id = candidate_register.candidate_uid
                        JOIN ministries ON ministries.ministry_id = candidate_register.ministry_id
                        WHERE election_id = @electionID;", Con)
            cmd.Parameters.AddWithValue("@electionID", lastElectionID)
            reader = cmd.ExecuteReader()

            ' Create a DataTable to store the data
            Dim dataTable As New DataTable()
            ' Fill the DataTable with data from the SQL table
            dataTable.Load(reader)
            reader.Close()
            Con.Close()

            ' Specify the Column Mappings from DataGridView to SQL Table
            DataGridView1.AutoGenerateColumns = False
            DataGridView1.Columns(0).DataPropertyName = "election_id"
            DataGridView1.Columns(1).DataPropertyName = "candidate_uid"
            DataGridView1.Columns(2).DataPropertyName = "name"
            DataGridView1.Columns(3).DataPropertyName = "ministry_name"
            DataGridView1.Columns(4).DataPropertyName = "agenda"
            DataGridView1.Columns(5).DataPropertyName = "status"

            ' Bind the data to DataGridView
            DataGridView1.DataSource = dataTable

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try



    End Sub

    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'For Each column As DataGridViewColumn In DataGridView1.Columns
        '    column.Frozen = False
        'Next

        'DataGridView1.RowTemplate.Height = 300
        'DataGridView1.ScrollBars = ScrollBars.Both

        'Dim rowIndex As Integer = DataGridView1.Rows.Add()
        'DataGridView1.Rows(rowIndex).Cells(0).Value = "1"
        'DataGridView1.Rows(rowIndex).Cells(1).Value = "121"
        'DataGridView1.Rows(rowIndex).Cells(2).Value = "Neha Sharma"
        'DataGridView1.Rows(rowIndex).Cells(3).Value = "Power Minister"
        'DataGridView1.Rows(rowIndex).Cells(4).Value = "Implementing renewable energy initiatives to reduce reliance on fossil fuels and promote sustainability. Modernizing and upgrading existing power infrastructure to ensure reliable and efficient electricity supply for all citizens."
        'DataGridView1.Rows(rowIndex).Cells(5).Value = "No"
        'DataGridView1.Rows(rowIndex).Cells(6).Value = "Pending"

        'rowIndex = DataGridView1.Rows.Add()
        'DataGridView1.Rows(rowIndex).Cells(0).Value = "2"
        'DataGridView1.Rows(rowIndex).Cells(1).Value = "256"
        'DataGridView1.Rows(rowIndex).Cells(2).Value = "Rajesh Patel"
        'DataGridView1.Rows(rowIndex).Cells(3).Value = "Finance Minister"
        'DataGridView1.Rows(rowIndex).Cells(4).Value = "Implementing fiscal policies to stimulate economic growth and job creation while maintaining fiscal discipline. Enhancing financial inclusion and stability through measures to promote investment, manage public debt, and ensure equitable distribution of resources."
        'DataGridView1.Rows(rowIndex).Cells(5).Value = "Yes"
        'DataGridView1.Rows(rowIndex).Cells(6).Value = "Approved"

        'Employment
        'Education
        'Health
        'Transport
        'Culture
        'Power
        'Finance
        'Broadcasting
        'IT

        ministryToId.Add("Employment", 1)
        ministryToId.Add("Education", 2)
        ministryToId.Add("Health", 3)
        ministryToId.Add("Transport", 4)
        ministryToId.Add("Culture", 5)
        ministryToId.Add("Power", 6)
        ministryToId.Add("Finance", 7)
        ministryToId.Add("Broadcasting", 8)
        ministryToId.Add("IT", 9)

        LoadandBindDataGridView()

    End Sub


    Private Function AreDatesIncreasing(dates() As String) As Boolean
        ' Iterate through the array starting from the second element
        For i As Integer = 1 To dates.Length - 1
            ' Convert the current and previous dates to Date objects for comparison
            Dim currentDate As Date
            Dim previousDate As Date
            If Date.TryParse(dates(i), currentDate) AndAlso Date.TryParse(dates(i - 1), previousDate) Then
                ' Check if the current date is less than or equal to the previous date
                If currentDate <= previousDate Then
                    ' If not, return false
                    Return False
                End If
            Else
                ' If any date parsing fails, return false
                Return False
            End If
        Next

        ' If all dates are in increasing order, return true
        Return True
    End Function

    Private Function isItPastElection()

        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand

        Try
            Con.Open()

            ' You can access the selected date and time like this:
            'Dim dt As DateTime = DateTime.Now
            'Dim year As Integer = dt.Year
            'Dim month As Integer = dt.Month
            'Dim day As Integer = dt.Day
            'Dim current_date As String = year.ToString + "-" + month.ToString + "-" + day.ToString
            Dim current_date As String = "2024-03-04"

            Dim resultAnnouncementDate As DateTime = DateTime.MinValue

            ' Retrieve the value of nomination_start from the last row of the election_time table
            Dim selectQuery As String = "SELECT campaigning_start FROM election_time ORDER BY election_id DESC LIMIT 1;"
            cmd = New MySqlCommand(selectQuery, Con)
            Dim result As Object = cmd.ExecuteScalar()

            ' Check if the result is not null and assign it to nominationStartDate
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                resultAnnouncementDate = Convert.ToDateTime(result)
            End If

            Dim dates() As String = {current_date, resultAnnouncementDate}

            Dim canwe As Boolean = AreDatesIncreasing(dates)

            Return canwe

            MessageBox.Show(canwe)
            'If canwe Then
            'Else
            '    If DataGridView1.Columns.Contains("Status") Then
            '        DataGridView1.Columns("Status").ReadOnly = True
            '    End If
            'End If


        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Function

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        ' Check if the value changed in the ComboBox column and it's not a header row
        If e.ColumnIndex = 5 AndAlso e.RowIndex <> -1 Then

            Dim canwe As Boolean = isItPastElection()

            If canwe Then
                Dim selectedValue As String = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
                ' You can perform any other actions here based on the changed value
                Dim Con = Globals.GetDBConnection()
                Dim reader As MySqlDataReader
                Dim cmd As MySqlCommand

                Try
                    Con.Open()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                cmd = New MySqlCommand("UPDATE candidate_register SET status = " + """" + selectedValue + """" + " WHERE election_id = " & DataGridView1.Rows(e.RowIndex).Cells(0).Value & " AND candidate_uid = " & DataGridView1.Rows(e.RowIndex).Cells(1).Value & ";", Con)
                reader = cmd.ExecuteReader

            Else
                MessageBox.Show("It's past the nomination time. Any changes will not be saved.")
            End If


        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        electionInnerScreenAdmin?.Dispose()
        electionInnerScreenAdmin = New ElectionInnerScreenAdmin With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenAdmin)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim selectedValue1 As Object = ComboBox1.SelectedItem
        Dim selectedValue2 As Object = ComboBox2.SelectedItem

        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()

            ' Retrieve the value of the election_id column from the last row of the election_time table
            Dim lastElectionID As Integer = -1 ' Default value in case there are no rows in election_time
            cmd = New MySqlCommand("SELECT election_id FROM election_time ORDER BY election_id DESC LIMIT 1;", Con)
            reader = cmd.ExecuteReader()
            If reader.Read() Then
                lastElectionID = Convert.ToInt32(reader("election_id"))
            End If
            reader.Close()

            If selectedValue1 Is Nothing And selectedValue2 IsNot Nothing Then
                Dim status As String = selectedValue2.ToString()
                'Get connection from globals
                'Dim Con = Globals.GetDBConnection()
                'Dim reader As MySqlDataReader
                'Dim cmd As MySqlCommand

                'Try
                '    Con.Open()
                'Catch ex As Exception
                '    MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'End Try

                cmd = New MySqlCommand("SELECT election_id, candidate_uid, name, ministry_name, agenda, status 
                                    FROM candidate_register
                                    JOIN users ON users.user_id = candidate_register.candidate_uid
                                    JOIN ministries ON ministries.ministry_id = candidate_register.ministry_id
                                    WHERE status = """ + status + " and election_id = @electionID;"";", Con)
                cmd.Parameters.AddWithValue("@electionID", lastElectionID)
                reader = cmd.ExecuteReader
                ' Create a DataTable to store the data
                Dim dataTable As New DataTable()

                'Fill the DataTable with data from the SQL table
                dataTable.Load(reader)
                reader.Close()
                Con.Close()

                'IMP: Specify the Column Mappings from DataGridView to SQL Table
                DataGridView1.AutoGenerateColumns = False
                DataGridView1.Columns(0).DataPropertyName = "election_id"
                DataGridView1.Columns(1).DataPropertyName = "candidate_uid"
                DataGridView1.Columns(2).DataPropertyName = "name"
                DataGridView1.Columns(3).DataPropertyName = "ministry_name"
                DataGridView1.Columns(4).DataPropertyName = "agenda"
                DataGridView1.Columns(5).DataPropertyName = "status"

                ' Bind the data to DataGridView
                DataGridView1.DataSource = dataTable
            ElseIf selectedValue1 IsNot Nothing And selectedValue2 Is Nothing Then
                Dim position As String = selectedValue1.ToString()
                ''Get connection from globals
                'Dim Con = Globals.GetDBConnection()
                'Dim reader As MySqlDataReader
                'Dim cmd As MySqlCommand

                'Try
                '    Con.Open()
                'Catch ex As Exception
                '    MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'End Try

                cmd = New MySqlCommand("SELECT election_id, candidate_uid, name, ministry_name, agenda, status 
                                    FROM candidate_register
                                    JOIN users ON users.user_id = candidate_register.candidate_uid
                                    JOIN ministries ON ministries.ministry_id = candidate_register.ministry_id
                                    WHERE ministries.ministry_id = " & ministryToId(position) & " AND election_id = @electionID;", Con)
                cmd.Parameters.AddWithValue("@electionID", lastElectionID)
                reader = cmd.ExecuteReader
                ' Create a DataTable to store the data
                Dim dataTable As New DataTable()

                'Fill the DataTable with data from the SQL table
                dataTable.Load(reader)
                reader.Close()
                Con.Close()

                'IMP: Specify the Column Mappings from DataGridView to SQL Table
                DataGridView1.AutoGenerateColumns = False
                DataGridView1.Columns(0).DataPropertyName = "election_id"
                DataGridView1.Columns(1).DataPropertyName = "candidate_uid"
                DataGridView1.Columns(2).DataPropertyName = "name"
                DataGridView1.Columns(3).DataPropertyName = "ministry_name"
                DataGridView1.Columns(4).DataPropertyName = "agenda"
                DataGridView1.Columns(5).DataPropertyName = "status"

                ' Bind the data to DataGridView
                DataGridView1.DataSource = dataTable
            ElseIf selectedValue1 IsNot Nothing And selectedValue2 IsNot Nothing Then
                Dim status As String = selectedValue2.ToString()
                Dim position As String = selectedValue1.ToString()

                ''Get connection from globals
                'Dim Con = Globals.GetDBConnection()
                'Dim reader As MySqlDataReader
                'Dim cmd As MySqlCommand

                'Try
                '    Con.Open()
                'Catch ex As Exception
                '    MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'End Try

                cmd = New MySqlCommand("SELECT election_id, candidate_uid, name, ministry_name, agenda, status 
                                    FROM candidate_register
                                    JOIN users ON users.user_id = candidate_register.candidate_uid
                                    JOIN ministries ON ministries.ministry_id = candidate_register.ministry_id
                                    WHERE status = """ + status + """ AND " + "ministries.ministry_id = " & ministryToId(position) & " AND election_id = @electionID;", Con)
                cmd.Parameters.AddWithValue("@electionID", lastElectionID)
                reader = cmd.ExecuteReader
                ' Create a DataTable to store the data
                Dim dataTable As New DataTable()

                'Fill the DataTable with data from the SQL table
                dataTable.Load(reader)
                reader.Close()
                Con.Close()

                'IMP: Specify the Column Mappings from DataGridView to SQL Table
                DataGridView1.AutoGenerateColumns = False
                DataGridView1.Columns(0).DataPropertyName = "election_id"
                DataGridView1.Columns(1).DataPropertyName = "candidate_uid"
                DataGridView1.Columns(2).DataPropertyName = "name"
                DataGridView1.Columns(3).DataPropertyName = "ministry_name"
                DataGridView1.Columns(4).DataPropertyName = "agenda"
                DataGridView1.Columns(5).DataPropertyName = "status"

                ' Bind the data to DataGridView
                DataGridView1.DataSource = dataTable
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
End Class