Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports SmartCityMgmtSystem.TransportGlobals
Public Class TransportationManageFastagAdmin
    Private primaryKeyEdit As String ' Define a class-level variable to hold the primary key of the row being edited

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
        Label3.Text = "Update Fastag Plan"
        Button3.Text = "Update"
        TextBox1.Text = txt1
        ' Iterate through the items in the ComboBox
        For Each item As Object In ComboBox1.Items
            ' Assuming your display member property is "DisplayMember"
            ' You might need to replace it with the actual property name
            Dim displayMemberValue As String = item.GetType().GetProperty("Name").GetValue(item).ToString()

            ' Check if the display member value matches the desired value
            If displayMemberValue = txt2 Then
                ' Set the item as the selected item
                ComboBox1.SelectedItem = item
                Exit For ' Exit the loop once the value is found
            End If
        Next
        TextBox2.Text = txt3
        TextBox3.Text = txt4
    End Sub

    Private Function GetVehicles() As List(Of Vehicle)
        Dim vehicles As New List(Of Vehicle)()
        For i As Integer = 1 To 7
            Dim id As Integer = i
            Dim name As String = GetVehicleType(i)
            Dim vehicle As New Vehicle(id, name)
            vehicles.Add(vehicle)
        Next
        Return vehicles
    End Function

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is in the "EditBut" column and not a header cell
        If e.ColumnIndex = DataGridView1.Columns("EditBut").Index AndAlso e.RowIndex >= 0 Then
            ' Change this to DB logic later
            'Show Edit Option
            primaryKeyEdit = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            ShowEditOption(DataGridView1.Rows(e.RowIndex).Cells(0).Value, DataGridView1.Rows(e.RowIndex).Cells(1).Value, DataGridView1.Rows(e.RowIndex).Cells(2).Value, DataGridView1.Rows(e.RowIndex).Cells(3).Value)
            ' Check if the clicked cell is in the "DeleteBut" column and not a header cell
        ElseIf e.ColumnIndex = DataGridView1.Columns("DeleteBut").Index AndAlso e.RowIndex >= 0 Then
            ' Perform the action for the "DeleteButton" column
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this entry?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then

                ' Call a method to delete the row from the database using the primary key
                Dim success As Boolean = Globals.ExecuteDeleteQuery("DELETE FROM fastag_plans where id = " & DataGridView1.Rows(e.RowIndex).Cells(0).Value)

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

        cmd = New MySqlCommand("SELECT * FROM fastag_plans", Con)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()
        DataGridView1.Rows.Clear()

        Dim i As Int16 = 0
        While (reader.Read())
            Dim row As New DataGridViewRow()
            DataGridView1.Rows.Add(row)
            DataGridView1.Rows(i).Cells("Column1").Value = reader.GetInt16("id")
            DataGridView1.Rows(i).Cells("Column2").Value = GetVehicleType(reader.GetInt16("vehicle_type"))
            DataGridView1.Rows(i).Cells("Column3").Value = reader.GetInt16("fee_amt")
            DataGridView1.Rows(i).Cells("Column4").Value = reader.GetInt16("validity_months")
            i = i + 1
        End While

        'Fill the DataTable with data from the SQL table
        reader.Close()
        Con.Close()
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
        Label3.Text = "Add Fastag Plan"
        Button3.Text = "Add"
        TextBox1.Clear()
        ComboBox1.SelectedIndex = -1
        TextBox2.Clear()
        TextBox3.Clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim t1, t2, t3, t4 As Int16
        t1 = 0
        t2 = 1
        t3 = 0
        t4 = 0
        If Not String.IsNullOrWhiteSpace(TextBox1.Text) Then
            Dim userInput As String = TextBox1.Text
            Dim intValue As Integer

            If Integer.TryParse(userInput, intValue) Then
                ' The TextBox contains an integer value
                t1 = 1
            Else
                ' The TextBox does not contain an integer value
                MessageBox.Show("The Plan Number TextBox does not contain an integer value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Label3.Text = "Add Fastag Plan"
                Button3.Text = "Add"
                TextBox1.Clear()
                ComboBox1.SelectedIndex = -1
                TextBox2.Clear()
                TextBox3.Clear()
                Return
            End If
        Else
            ' The TextBox is empty
            MessageBox.Show("Please enter some input in the Plan Number textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Label3.Text = "Add Fastag Plan"
            Button3.Text = "Add"
            TextBox1.Clear()
            ComboBox1.SelectedIndex = -1
            TextBox2.Clear()
            TextBox3.Clear()
            Return
        End If
        If ComboBox1.SelectedIndex = -1 Then
            t2 = 0
            MessageBox.Show("Please enter some thing into comboBox", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Label3.Text = "Add Fastag Plan"
            Button3.Text = "Add"
            TextBox1.Clear()
            ComboBox1.SelectedIndex = -1
            TextBox2.Clear()
            TextBox3.Clear()
            Return
        End If
        If Not String.IsNullOrWhiteSpace(TextBox2.Text) Then
            Dim userInput As String = TextBox2.Text
            Dim intValue As Integer

            If Integer.TryParse(userInput, intValue) Then
                ' The TextBox contains an integer value
                t3 = 1
            Else
                ' The TextBox does not contain an integer value
                MessageBox.Show("The Fee amount TextBox does not contain an integer value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Label3.Text = "Add Fastag Plan"
                Button3.Text = "Add"
                TextBox1.Clear()
                ComboBox1.SelectedIndex = -1
                TextBox2.Clear()
                TextBox3.Clear()
                Return
            End If
        Else
            ' The TextBox is empty
            MessageBox.Show("Please enter some input in the Fee amount textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Label3.Text = "Add Fastag Plan"
            Button3.Text = "Add"
            TextBox1.Clear()
            ComboBox1.SelectedIndex = -1
            TextBox2.Clear()
            TextBox3.Clear()
            Return
        End If
        If Not String.IsNullOrWhiteSpace(TextBox3.Text) Then
            Dim userInput As String = TextBox3.Text
            Dim intValue As Integer

            If Integer.TryParse(userInput, intValue) Then
                ' The TextBox contains an integer value
                t4 = 1
            Else
                MessageBox.Show("The Validity TextBox does not contain an integer value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Label3.Text = "Add Fastag Plan"
                Button3.Text = "Add"
                TextBox1.Clear()
                ComboBox1.SelectedIndex = -1
                TextBox2.Clear()
                TextBox3.Clear()
                Return
            End If
        Else
            ' The TextBox is empty
            MessageBox.Show("Please enter some input in the Validity textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Label3.Text = "Add Fastag Plan"
            Button3.Text = "Add"
            TextBox1.Clear()
            ComboBox1.SelectedIndex = -1
            TextBox2.Clear()
            TextBox3.Clear()
            Return
        End If
        If Button3.Text = "Update" Then
            If t1 = 1 And t2 = 1 And t3 = 1 And t4 = 1 Then
                Dim cmd As String
                cmd = "UPDATE fastag_plans SET id = " & Convert.ToInt32(TextBox1.Text) & " , vehicle_type = " & ComboBox1.SelectedValue & " , fee_amt =" & Convert.ToInt32(TextBox2.Text) & " , validity_months = " & Convert.ToInt32(TextBox3.Text) & " WHERE id =" & primaryKeyEdit
                Dim success As Boolean = Globals.ExecuteUpdateQuery(cmd)
                If success Then
                    LoadandBindDataGridView()

                End If
                Dim veh As String = ComboBox1.SelectedItem.GetType().GetProperty("Name").GetValue(ComboBox1.SelectedItem).ToString()
                Globals.SendNotifications(4, -1, "Fastag Plan Updated", "An existing fastag plan with id " & TextBox1.Text & " has been updated by the admin with new vehicle type " & veh & ", fee as " & TextBox2.Text & " rupees and validity for " & TextBox3.Text & " months.")
                Label3.Text = "Add Fastag Plan"
                Button3.Text = "Add"
                TextBox1.Clear()
                ComboBox1.SelectedIndex = -1
                TextBox2.Clear()
                TextBox3.Clear()
            End If
        Else
            If t1 = 1 And t2 = 1 And t3 = 1 And t4 = 1 Then
                Dim cmd As String
                cmd = "INSERT into fastag_plans(id,vehicle_type,fee_amt,validity_months) VALUES (" & Convert.ToInt32(TextBox1.Text) & "," & ComboBox1.SelectedValue & "," & Convert.ToInt32(TextBox2.Text) & "," & Convert.ToInt32(TextBox3.Text) & ")"
                Dim success As Boolean = Globals.ExecuteInsertQuery(cmd)
                If success Then
                    LoadandBindDataGridView()
                End If
                Dim veh As String = ComboBox1.SelectedItem.GetType().GetProperty("Name").GetValue(ComboBox1.SelectedItem).ToString()
                Globals.SendNotifications(4, -1, "Fastag Plan Added", "A new fastag plan with id " & TextBox1.Text & " has been added by the admin with vehicle type " & veh & ", fee as " & TextBox2.Text & " rupees and validity for " & TextBox3.Text & " months.")
                TextBox1.Clear()
                ComboBox1.SelectedIndex = -1
                TextBox2.Clear()
                TextBox3.Clear()
            End If
        End If
    End Sub
End Class