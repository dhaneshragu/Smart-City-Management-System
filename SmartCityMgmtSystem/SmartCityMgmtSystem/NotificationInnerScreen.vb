
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports SmartCityMgmtSystem.NotificationInnerScreen
Imports SmartCityMgmtSystem.RideSharingMain
Public Class NotificationInnerScreen
    Public Property uid As Integer = 11
    Public Property u_name As String = "Dhanesh"
    'To hold all the ministry names

    Public Class Ministry
        Public Property ID As String
        Public Property Name As String
    End Class

    ' Create a list to hold the ministries
    Dim ministries As New List(Of Ministry)()

    Private Sub AddPost(postNum As Integer,
                        Optional ByVal title As String = "Notification",
                        Optional ByVal timestamp As String = "28 Mar 24,05:30 PM",
                        Optional ByVal msg As String = "This is a test Notification",
                        Optional ByVal ministry_id As Integer = 1,
                        Optional ByVal ministry_name As String = "Ministry of Labour and Employment")
        Dim Notif As NotificationBubble
        Notif = New NotificationBubble()
        With Notif
            .Name = "post_" & postNum
        End With
        Notif.SetDetails(title, timestamp, msg, ministry_id, ministry_name)
        PostsPanel.Controls.Add(Notif)
    End Sub


    Private Sub PopulateComboBox()
        Dim connection As New MySqlConnection(Globals.getdbConnectionString())
        Try
            ' Open the connection
            connection.Open()

            ' Query to retrieve ministry_id and ministry_name
            Dim query As String = "SELECT ministry_id, ministry_name FROM ministries"
            Dim command As New MySqlCommand(query, connection)

            ' Execute the query and read the results
            Dim reader As MySqlDataReader = command.ExecuteReader()



            While reader.Read()
                ' Create a new Ministry object with ministry_id and ministry_name
                Dim ministry As New Ministry With {
                    .ID = reader("ministry_id").ToString(),
                    .Name = reader("ministry_name").ToString()
                }
                ' Add the ministry to the list
                ministries.Add(ministry)
            End While

            ' Close the reader
            reader.Close()

            ' Add "View All" option with id -1
            Dim viewAll As New Ministry()
            viewAll.ID = "-1"
            viewAll.Name = "View All"
            ministries.Insert(0, viewAll)

            ' Bind ComboBox1 to the list of ministries
            ComboBox1.DataSource = ministries
            ComboBox1.DisplayMember = "Name"
            ComboBox1.ValueMember = "ID"

            ' Set default selection to "View All"
            ComboBox1.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show("Error while populating ComboBox: " & ex.Message)
        Finally
            ' Close the connection
            connection.Close()
        End Try
    End Sub
    Private Sub FilterNotifications(Optional ministryId As Integer = -1)
        PostsPanel.Controls.Clear()
        Try
            Using connection As New MySqlConnection(Globals.getdbConnectionString())
                connection.Open()
                Dim commandText As String = "SELECT * FROM notifications"
                If ministryId = -1 Then
                    ' If ministryId is -1, get all notifications for this UID or as broadcast
                    commandText &= " WHERE to_id = @uid OR to_id = -1"
                Else
                    ' If ministryId is not -1, get notifications for that ministry
                    commandText &= " WHERE (to_id = @uid OR to_id = -1) AND ministry_id = @ministryId"
                End If
                Dim command As New MySqlCommand(commandText, connection)
                command.Parameters.AddWithValue("@uid", uid)
                If ministryId <> -1 Then
                    command.Parameters.AddWithValue("@ministryId", ministryId)
                End If
                Dim reader As MySqlDataReader = command.ExecuteReader()

                While reader.Read()
                    Dim notificationId As Integer = Convert.ToInt32(reader("notif_id"))
                    Dim notif_ministry_id As Integer = Convert.ToInt32(reader("ministry_id"))
                    Dim ministryName As String = ministries.FirstOrDefault(Function(m) m.ID = notif_ministry_id.ToString())?.Name
                    Dim msg As String = reader("message").ToString()
                    Dim timestamp As String = Convert.ToDateTime(reader("time_stamp")).ToString("dd MMM yy, hh:mm tt")
                    Dim title As String = reader("title").ToString()
                    ' Add post based on notification data
                    AddPost(notificationId, title, timestamp, msg, notif_ministry_id, ministryName)
                End While

                reader.Close()
            End Using ' Closes the connection automatically
        Catch ex As Exception
            ' Handle the exception (e.g., log the error, display a message to the user)
            MessageBox.Show("An error occurred while filtering notifications: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem IsNot Nothing Then
            ' Get the associated ministry_id
            Dim selectedMinistry As Ministry = DirectCast(ComboBox1.SelectedItem, Ministry)
            'Filter the flow layout panel accordingly
            FilterNotifications(selectedMinistry.ID)
        End If
    End Sub
    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Load the places from database and populate the combobox
        PopulateComboBox()
        FilterNotifications(-1)
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        PostsPanel.Controls.Clear()
        Try
            Using connection As New MySqlConnection(Globals.getdbConnectionString())
                connection.Open()
                Dim commandText As String = "SELECT * FROM notifications WHERE (to_id = @uid OR to_id = -1) AND time_stamp >= @selectedDate"
                Dim command As New MySqlCommand(commandText, connection)
                command.Parameters.AddWithValue("@uid", uid)
                command.Parameters.AddWithValue("@selectedDate", DateTimePicker1.Value)
                Dim reader As MySqlDataReader = command.ExecuteReader()

                While reader.Read()
                    Dim notificationId As Integer = Convert.ToInt32(reader("notif_id"))
                    Dim notif_ministry_id As Integer = Convert.ToInt32(reader("ministry_id"))
                    Dim ministryName As String = ministries.FirstOrDefault(Function(m) m.ID = notif_ministry_id.ToString())?.Name
                    Dim msg As String = reader("message").ToString()
                    Dim timestamp As String = Convert.ToDateTime(reader("time_stamp")).ToString("dd MMM yy, hh:mm tt")
                    Dim title As String = reader("title").ToString()
                    ' Add post based on notification data
                    AddPost(notificationId, title, timestamp, msg, notif_ministry_id, ministryName)
                End While

                reader.Close()
            End Using ' Closes the connection automatically
        Catch ex As Exception
            ' Handle the exception (e.g., log the error, display a message to the user)
            MessageBox.Show("An error occurred while filtering notifications: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class