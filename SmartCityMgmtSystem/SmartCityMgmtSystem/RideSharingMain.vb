Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports MySql.Data.MySqlClient
Public Class RideSharingMain
    ' Public property to hold uid and name of the person
    Public Property uid As Integer = 1
    Public Property u_name As String = "Dhanesh"
    ' Define a class to hold place information
    Public Class Place
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

    ' Function to retrieve place data from the database
    Private Function GetPlacesFromDatabase() As List(Of Place)
        Dim places As New List(Of Place)()

        Using conn As New MySqlConnection(Globals.getdbConnectionString())
            conn.Open()

            Dim query As String = "SELECT * FROM placedb"
            Using command As New MySqlCommand(query, conn)
                Using reader As MySqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim id As Integer = reader.GetInt32("id")
                        Dim name As String = reader.GetString("place_name")
                        Dim place As New Place(id, name)
                        places.Add(place)
                    End While
                End Using
            End Using
            conn.Close()
        End Using

        Return places
    End Function
    ' To load the Track posts Datagridview
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
        cmd = New MySqlCommand("SELECT rs.req_id, rs.status,CONCAT(DATE_FORMAT(rs.start_datetime, '%d-%m-%Y'), ', ', DATE_FORMAT(rs.start_datetime, '%h:%i %p')) AS start_datetime, src.place_name AS src, dest.place_name AS dest FROM ride_sharing_entries rs JOIN placedb src ON rs.src_id = src.id JOIN placedb dest ON rs.dest_id = dest.id JOIN users ON users.user_id = rs.uid WHERE rs.uid = " & uid & ";", Con)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "req_id"
        DataGridView1.Columns(1).DataPropertyName = "src"
        DataGridView1.Columns(2).DataPropertyName = "dest"
        DataGridView1.Columns(3).DataPropertyName = "start_datetime"
        DataGridView1.Columns(4).DataPropertyName = "status"
        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub AddPost(name As String,
                        poster_uid As Integer,
                        vehicleNum As String,
                        note As String,
                        Optional datetime As String = "",
                        Optional fromPlace As String = "",
                        Optional toPlace As String = "",
                        Optional fare As Integer = 0,
                        Optional capacity As Integer = 0,
                        Optional postNum As Integer = 0,
                        Optional image As Image = Nothing)
        Dim RidePost As RideSharingPost
        RidePost = New RideSharingPost()
        With RidePost
            .Name = "post_" & postNum
            .Width = 595
        End With
        RidePost.SetDetails(name, datetime, fromPlace, toPlace, fare, capacity, image)
        RidePost.SetAuxillaryDetails(uid, poster_uid, u_name, postNum, vehicleNum, note)
        PostsPanel.Controls.Add(RidePost)
    End Sub

    Private Sub GetVehicleNumberComboBox()
        ' Clear existing items from ComboBox
        ComboBox3.Items.Clear()

        ' Query to retrieve approved vehicle numbers
        Dim query As String = "SELECT vehicle_id FROM vehicle_reg WHERE status = 'approved' AND uid=" & uid

        Using con As New MySqlConnection(Globals.getdbConnectionString())
            Dim command As New MySqlCommand(query, con)
            con.Open()

            ' Execute the query and read the results
            Using reader As MySqlDataReader = command.ExecuteReader()
                While reader.Read()
                    ' Add each vehicle number to the ComboBox
                    ComboBox3.Items.Add(reader.GetString("vehicle_id"))
                End While
            End Using

            con.Close()
        End Using
    End Sub


    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Load the places from database and populate the combobox
        Dim places As List(Of Place) = GetPlacesFromDatabase()

        'To bind the places as their names in the combobox and their IDs as values
        ComboBox1.DataSource = places.ToList()
        ComboBox1.DisplayMember = "Name"
        ComboBox1.ValueMember = "ID"

        ComboBox2.DataSource = places.ToList()
        ComboBox2.DisplayMember = "Name"
        ComboBox2.ValueMember = "ID"

        'Get the vehicle numbers in combobox
        GetVehicleNumberComboBox()

        'Load the posts
        LoadPosts()

        'Load the datagridview for tracking posts
        LoadandBindDataGridView()

    End Sub
    Private Sub ClearAddRidePost()
        DateTimePicker1.Value = DateTime.Today
        DateTimePicker2.Value = DateTime.Now
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1
        NumericUpDown1.Value = NumericUpDown1.Minimum
        NumericUpDown2.Value = NumericUpDown2.Minimum
        RichTextBox1.Clear() ' Clear the rich text box
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Add" Then
            Dim selectedDate As Date = DateTimePicker1.Value.Date
            Dim selectedTime As TimeSpan = DateTimePicker2.Value.TimeOfDay
            Dim combinedDateTime As DateTime = selectedDate.Add(selectedTime)
            Dim selectedVehicle As String = ComboBox3.SelectedItem

            ' Check if any of the required values is null or zero (if applicable)
            If selectedDate = Nothing OrElse selectedTime = Nothing OrElse ComboBox1.SelectedValue Is Nothing OrElse ComboBox1.SelectedValue = 0 OrElse ComboBox2.SelectedValue Is Nothing OrElse ComboBox2.SelectedValue = 0 OrElse selectedVehicle Is Nothing OrElse selectedVehicle = "" Then
                MessageBox.Show("Please fill in all required fields marked as *", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                ' Convert values to appropriate types
                Dim fromPlace As Integer = Convert.ToInt32(ComboBox1.SelectedValue)
                Dim toPlace As Integer = Convert.ToInt32(ComboBox2.SelectedValue)
                Dim fare As Integer = Convert.ToInt32(NumericUpDown1.Value)
                Dim capacity As Integer = Convert.ToInt32(NumericUpDown2.Value)
                Dim note As String = RichTextBox1.Text
                Dim selectedVehicleId As String = ComboBox3.SelectedItem.ToString()
                Dim insertQuery As String = "INSERT INTO ride_sharing_entries (uid, vehicle_id, src_id, dest_id, start_datetime, fare_per_person, capacity, note) VALUES (" & uid & ", '" & selectedVehicleId & "', " & fromPlace & ", " & toPlace & ", '" & combinedDateTime.ToString("yyyy-MM-dd HH:mm:ss") & "', " & fare & ", " & capacity & ", '" & note & "');"
                Dim req_ID As Integer = Globals.ExecuteInsertQueryAndReturnID(insertQuery)
                If req_ID > 0 Then
                    'Load the new posts and refresh the track posts datagridview
                    If Not RideSharingChats.CheckIfUidExists(uid, req_ID) Then
                        insertQuery = "INSERT into ridesharing_chats_users (req_id,uid,role,status,fee_paid) VALUES (" & req_ID & "," & uid & ",0,'added',1);"
                        If Globals.ExecuteInsertQuery(insertQuery) Then
                            MessageBox.Show("Your post has been added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ClearAddRidePost()
                        End If
                    End If
                    LoadPosts()
                    LoadandBindDataGridView()
                End If
            End If
        End If
    End Sub

    Private Sub LoadPosts()
        ' Clear existing posts
        PostsPanel.Controls.Clear()
        Dim query As String = "SELECT rs.req_id, rs.uid, users.name, users.profile_photo, rs.vehicle_id, rs.capacity, rs.note, rs.fare_per_person, CONCAT(DATE_FORMAT(rs.start_datetime, '%d-%m-%Y'), ', ', DATE_FORMAT(rs.start_datetime, '%h:%i %p')) AS start_datetime, src.place_name AS src, dest.place_name AS dest FROM ride_sharing_entries rs JOIN placedb src ON rs.src_id = src.id JOIN placedb dest ON rs.dest_id = dest.id JOIN users ON users.user_id = rs.uid WHERE rs.status='approved' ORDER BY rs.start_datetime ASC;"
        Using connection As New MySqlConnection(Globals.getdbConnectionString())
            Using command As New MySqlCommand(query, connection)
                connection.Open()

                ' Execute the command
                Using reader As MySqlDataReader = command.ExecuteReader()
                    ' Read each row
                    While reader.Read()
                        ' Access columns by name or index
                        Dim reqId As Integer = Convert.ToInt32(reader("req_id"))
                        Dim poster_uid As Integer = Convert.ToInt32(reader("uid"))
                        Dim userName As String = reader("name").ToString()
                        Dim vehicleId As String = reader("vehicle_id")
                        Dim capacity As Integer = Convert.ToInt32(reader("capacity"))
                        Dim farePerPerson As Integer = Convert.ToInt32(reader("fare_per_person"))
                        Dim startDatetime As String = reader("start_datetime").ToString()
                        Dim src As String = reader("src").ToString()
                        Dim note As String = reader("note").ToString()
                        Dim dest As String = reader("dest").ToString()
                        Dim picture As Image = Nothing
                        If Not reader.IsDBNull(reader.GetOrdinal("profile_photo")) Then
                            Dim photoData As Byte() = DirectCast(reader("profile_photo"), Byte())
                            Using ms As New MemoryStream(photoData)
                                picture = Image.FromStream(ms)
                            End Using
                        End If
                        'Add to PostsPanel
                        AddPost(userName, poster_uid, vehicleId, note, startDatetime, src, dest, farePerPerson, capacity, reqId, picture)
                    End While

                End Using
            End Using
        End Using

    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Prevent selecting the same place in both ComboBoxes
        Dim selectedPlace1 As Place = DirectCast(ComboBox1.SelectedItem, Place)
        Dim selectedPlace2 As Place = DirectCast(ComboBox2.SelectedItem, Place)

        If selectedPlace1 IsNot Nothing AndAlso selectedPlace2 IsNot Nothing AndAlso selectedPlace1.ID = selectedPlace2.ID Then
            ComboBox2.SelectedIndex = -1 ' Clear selection from ComboBox2
        End If
    End Sub


    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        ' Prevent selecting the same place in both ComboBoxes
        Dim selectedPlace1 As Place = DirectCast(ComboBox1.SelectedItem, Place)
        Dim selectedPlace2 As Place = DirectCast(ComboBox2.SelectedItem, Place)

        If selectedPlace1 IsNot Nothing AndAlso selectedPlace2 IsNot Nothing AndAlso selectedPlace1.ID = selectedPlace2.ID Then
            ComboBox1.SelectedIndex = -1 ' Clear selection from ComboBox1
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        LoadandBindDataGridView()
        LoadPosts()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked column is the "DeleteBut" column
        If e.ColumnIndex = DataGridView1.Columns("DeleteBut").Index AndAlso e.RowIndex >= 0 Then
            ' Show a confirmation dialog
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this post?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            ' If the user clicks Yes, proceed with the deletion
            If result = DialogResult.Yes Then
                ' Delete the entry from the database
                Dim req_id As Integer = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("Column5").Value)
                Dim query As String = "DELETE FROM ride_sharing_entries WHERE req_id = " & req_id & ";"

                If Globals.ExecuteDeleteQuery(query) Then
                    MessageBox.Show("Post deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadandBindDataGridView()
                    LoadPosts()
                End If
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ClearAddRidePost()
    End Sub
End Class