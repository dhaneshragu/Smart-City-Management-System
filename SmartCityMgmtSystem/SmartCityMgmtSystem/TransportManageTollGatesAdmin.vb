Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports SmartCityMgmtSystem.TransportGlobals
Public Class TransportManageTollGatesAdmin
    Private primaryKeyEdit As String

    Public Class Vehicle
        Public Property ID As Integer
        Public Property Name As String

        Public Sub New(ByVal id As Integer, ByVal name As String)
            Me.ID = id
            Me.Name = name
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Private Sub ShowEditOption(ByVal txt1 As String, ByVal txt2 As String, ByVal txt3 As String, ByVal txt4 As String)
        Label3.Text = "Update Toll Booth"
        Button3.Text = "Update"
        TextBox1.Text = txt1
        TextBox6.Text = txt2
        ' Iterate through the items in the ComboBox
        For Each item As Object In ComboBox1.Items
            ' Assuming your display member property is "DisplayMember"
            ' You might need to replace it with the actual property name
            Dim displayMemberValue As String = item.GetType().GetProperty("Name").GetValue(item).ToString()

            ' Check if the display member value matches the desired value
            If displayMemberValue = txt3 Then
                ' Set the item as the selected item
                ComboBox1.SelectedItem = item
                Exit For ' Exit the loop once the value is found
            End If
        Next
        TextBox3.Text = txt4
    End Sub

    Private Function GetVehicles() As List(Of Vehicle)
        Dim vehicles As New List(Of Vehicle)()
        For Each kvp As KeyValuePair(Of Integer, String) In TransportGlobals.vehicleTypeMap
            Dim id As Integer = kvp.Key
            Dim name As String = kvp.Value
            Dim vehicle As New Vehicle(id, name)
            vehicles.Add(vehicle)
        Next
        Return vehicles
    End Function


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is in the "EditBut" column and not a header cell
        If e.ColumnIndex = DataGridView1.Columns("EditBut").Index AndAlso e.RowIndex >= 0 Then
            'Show Edit Option
            primaryKeyEdit = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            ShowEditOption(DataGridView1.Rows(e.RowIndex).Cells(0).Value, DataGridView1.Rows(e.RowIndex).Cells(1).Value, DataGridView1.Rows(e.RowIndex).Cells(2).Value, DataGridView1.Rows(e.RowIndex).Cells(3).Value)
            ' Check if the clicked cell is in the "DeleteBut" column and not a header cell
        ElseIf e.ColumnIndex = DataGridView1.Columns("DeleteBut").Index AndAlso e.RowIndex >= 0 Then
            ' Perform the action for the "DeleteButton" column
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this entry?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then

                ' Call a method to delete the row from the database using the primary key
                Dim success As Boolean = Globals.ExecuteDeleteQuery("DELETE FROM tollboothdb where lane_id = " & DataGridView1.Rows(e.RowIndex).Cells(0).Value)

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

        cmd = New MySqlCommand("SELECT * FROM tollboothdb", Con)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "lane_id"
        DataGridView1.Columns(1).DataPropertyName = "description"
        DataGridView1.Columns(2).DataPropertyName = "allowed_vehicle_types"
        DataGridView1.Columns(3).DataPropertyName = "fare_per_vehicle"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Load the places from database and populate the combobox
        Dim vehicles As List(Of Vehicle) = GetVehicles()

        'To bind the places as their names in the combobox and their IDs as values
        ComboBox1.DataSource = vehicles.ToList()
        ComboBox1.DisplayMember = "Name"
        ComboBox1.ValueMember = "ID"

        ComboBox1.SelectedIndex = -1

        LoadandBindDataGridView()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label3.Text = "Add Toll Booth"
        Button3.Text = "Add"
        TextBox1.Clear()
        TextBox6.Clear()
        ComboBox1.SelectedIndex = -1
        TextBox3.Clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Not String.IsNullOrWhiteSpace(TextBox1.Text) Then
            Dim userInput As String = TextBox1.Text
            Dim intValue As Integer

            If Integer.TryParse(userInput, intValue) Then
                ' The TextBox contains an integer value
            Else
                ' The TextBox does not contain an integer value
                MessageBox.Show("The Lane ID TextBox does not contain an integer value")
                Label3.Text = "Add Toll Booth"
                Button3.Text = "Add"
                TextBox1.Clear()
                TextBox6.Clear()
                ComboBox1.SelectedIndex = -1
                TextBox3.Clear()
                Return
            End If
        Else
            ' The TextBox is empty
            MessageBox.Show("Please enter some input in the Lane ID textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Label3.Text = "Add Toll Booth"
            Button3.Text = "Add"
            TextBox1.Clear()
            TextBox6.Clear()
            ComboBox1.SelectedIndex = -1
            TextBox3.Clear()
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox6.Text) Then
            MessageBox.Show("Please enter some input in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Label3.Text = "Add Toll Booth"
            Button3.Text = "Add"
            TextBox1.Clear()
            TextBox6.Clear()
            ComboBox1.SelectedIndex = -1
            TextBox3.Clear()
            Return
        End If
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Please enter some input in the comboBox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Label3.Text = "Add Toll Booth"
            Button3.Text = "Add"
            TextBox1.Clear()
            TextBox6.Clear()
            ComboBox1.SelectedIndex = -1
            TextBox3.Clear()
            Return
        End If

        If Not String.IsNullOrWhiteSpace(TextBox3.Text) Then
            Dim userInput As String = TextBox3.Text
            Dim intValue As Integer

            If Integer.TryParse(userInput, intValue) Then
                ' The TextBox contains an integer value
            Else
                ' The TextBox does not contain an integer value
                MessageBox.Show("The Fare TextBox does not contain an integer value")
                Label3.Text = "Add Toll Booth"
                Button3.Text = "Add"
                TextBox1.Clear()
                TextBox6.Clear()
                ComboBox1.SelectedIndex = -1
                TextBox3.Clear()
                Return
            End If
        Else
            ' The TextBox is empty
            MessageBox.Show("Please enter some input in the Fare textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Label3.Text = "Add Toll Booth"
            Button3.Text = "Add"
            TextBox1.Clear()
            TextBox6.Clear()
            ComboBox1.SelectedIndex = -1
            TextBox3.Clear()
            Return
        End If

        If Button3.Text = "Update" Then
            Dim cmd As String
            cmd = "UPDATE tollboothdb SET lane_id = " & Convert.ToInt32(TextBox1.Text) & " , description = '" & TextBox6.Text & "' , allowed_vehicle_types ='" & ComboBox1.SelectedItem.Name & "' , fare_per_vehicle = '" & TextBox3.Text & "' WHERE lane_id =" & primaryKeyEdit
            Dim success As Boolean = Globals.ExecuteUpdateQuery(cmd)
            If success Then
                LoadandBindDataGridView()

            End If
            Globals.SendNotifications(4, -1, "Toll Booth Updated", "An existing toll booth with lane id " & TextBox1.Text & " is updated by admin with new description " & TextBox6.Text & ", allowed vehicles " & ComboBox1.SelectedItem.Name & " and fare " & TextBox3.Text & " rupees.")
            Label3.Text = "Add Toll Booth"
            Button3.Text = "Add"
            TextBox1.Clear()
            TextBox6.Clear()
            ComboBox1.SelectedIndex = -1
            TextBox3.Clear()
        Else
            Dim cmd As String
            cmd = "INSERT into tollboothdb VALUES (" & Convert.ToInt32(TextBox1.Text) & ",'" & TextBox6.Text & "','" & ComboBox1.SelectedItem.Name & "','" & TextBox3.Text & "')"
            Dim success As Boolean = Globals.ExecuteInsertQuery(cmd)
            If success Then
                LoadandBindDataGridView()
            End If
            Globals.SendNotifications(4, -1, "Toll Booth Added", "A new toll booth with lane id " & TextBox1.Text & " is added by admin with description " & TextBox6.Text & ", allowed vehicles " & ComboBox1.SelectedItem.Name & " and fare " & TextBox3.Text & " rupees.")
            TextBox1.Clear()
            TextBox6.Clear()
            ComboBox1.SelectedIndex = -1
            TextBox3.Clear()
        End If
    End Sub
End Class