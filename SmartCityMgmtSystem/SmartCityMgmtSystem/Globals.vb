Imports System.Configuration
Imports System.IO
Imports MySql.Data.MySqlClient

'To get the global variables/declarations to be used all over the project
Public Class Globals
    'Deployed Database
    Private Shared dbConnectionString As String = "server=mysql9001.site4now.net;user id=aa69bc_sghy;password=swelab123;database=db_aa69bc_sghy;"


    'To get the dbString
    Public Shared Function getdbConnectionString() As String
        Return dbConnectionString
    End Function

    'To get the dbConnection
    Public Shared Function GetDBConnection() As MySqlConnection
        Dim dbConnectionString As String = getdbConnectionString()
        Dim conn = New MySqlConnection(dbConnectionString)
        Return conn
    End Function

    'To execute a Delete Query - returns True if succesful
    Public Shared Function ExecuteDeleteQuery(query As String) As Boolean

        Using conn As MySqlConnection = GetDBConnection()
            Try
                conn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                    'MessageBox.Show("Deleted successfully.", "Delete Entry", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return True
                End Using
            Catch ex As Exception
                MessageBox.Show("Error executing query: " & ex.Message, "Query Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Using
        Return False
    End Function
    'To execute a Update Query returns True if succesful
    Public Shared Function ExecuteUpdateQuery(query As String) As Boolean

        Using conn As MySqlConnection = GetDBConnection()
            Try
                conn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                    'MessageBox.Show("Updated successfully.", "Update Entry", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return True
                End Using
            Catch ex As Exception
                MessageBox.Show("Error executing query: " & ex.Message, "Query Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Using
        Return False
    End Function

    'To execute a Insert Query returns True if succesful
    Public Shared Function ExecuteInsertQuery(query As String) As Boolean

        Using conn As MySqlConnection = GetDBConnection()
            Try
                conn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                    'MessageBox.Show("Inserted successfully.", "Insert Entry", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return True
                End Using
            Catch ex As Exception
                MessageBox.Show("Error executing query: " & ex.Message, "Query Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Using
        Return False
    End Function
    'Returns the ID of the last inserted row
    Public Shared Function ExecuteInsertQueryAndReturnID(query As String) As Integer

        Using conn As MySqlConnection = GetDBConnection()
            Try
                conn.Open()
                Using cmd As MySqlCommand = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                    'Retrieve the last inserted ID
                    cmd.CommandText = "SELECT LAST_INSERT_ID();"
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            Catch ex As Exception
                MessageBox.Show("Error executing query: " & ex.Message, "Query Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return -1
            End Try
        End Using
        Return False
    End Function
    'To view the child form in the same window inside a parentPanel
    'Eg. to view InnerScreen(childform) in Dashboard Screen
    Public Shared Sub viewChildForm(ByVal parentpanel As Panel, ByVal childform As Form)
        parentpanel.Controls.Clear()
        childform.TopLevel = False
        childform.FormBorderStyle = FormBorderStyle.None
        childform.Dock = DockStyle.Fill
        childform.BringToFront()
        parentpanel.Controls.Add(childform)
        childform.Show() 'Add to panel and show the child form
    End Sub

    Public Shared Sub viewChildFormAndClose(ByVal parentpanel As Panel, ByRef childform As Form, ByRef parentForm As Form)
        parentpanel.Controls.Clear()
        childform.TopLevel = False
        childform.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        childform.Dock = DockStyle.Fill
        childform.BringToFront()
        parentpanel.Controls.Add(childform)
        childform.Show() ' Add to panel and show the child form
        ' Pass reference of the parent form to the child form
        If TypeOf childform Is HomePage Then
            CType(childform, HomePage).SetMainForm(parentForm)
        End If
    End Sub
    'To send notifications to the users, give uid as -1 if u want to send to all
    'refer to your ministry id from ministries table in db
    Public Shared Sub SendNotifications(ministry_id As Integer, to_uid As Integer, notif_title As String, notifmsg As String)
        Dim currentTimeStamp As DateTime = DateTime.Now
        Dim formattedTimeStamp As String = currentTimeStamp.ToString("yyyy-MM-dd HH:mm:ss")
        Dim query As String = "INSERT INTO notifications (ministry_id, to_id, title, message, time_stamp) VALUES (@ministry_id, @to_uid, @notif_title, @notifmsg, @dt)"

        Using connection As New MySqlConnection(getdbConnectionString())
            Using command As New MySqlCommand(query, connection)
                ' Add parameters
                command.Parameters.AddWithValue("@ministry_id", ministry_id)
                command.Parameters.AddWithValue("@to_uid", to_uid)
                command.Parameters.AddWithValue("@notif_title", notif_title)
                command.Parameters.AddWithValue("@notifmsg", notifmsg)
                command.Parameters.AddWithValue("@dt", formattedTimeStamp)
                Try
                    connection.Open()
                    If command.ExecuteNonQuery() > 0 Then
                        'MessageBox.Show("Notification sent successfully", "Notification Sent", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error sending notification: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    'Fetches the Picture from DB, given the query and field name
    Public Shared Function GetPicture(query As String, fieldName As String) As Image
        Dim profileImage As Image = Nothing

        Try
            Using connection As New MySqlConnection(getdbConnectionString())
                connection.Open()
                Using command As New MySqlCommand(query, connection)

                    Using reader As MySqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            ' Check if the photo data is not null
                            If Not reader.IsDBNull(0) Then
                                ' Retrieve the photo data as a byte array
                                Dim photoData As Byte() = DirectCast(reader(fieldName), Byte())

                                ' Load the photo data into a MemoryStream
                                Using ms As New MemoryStream(photoData)
                                    ' Create an Image object from the MemoryStream
                                    profileImage = Image.FromStream(ms)
                                End Using
                            End If
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions
            MessageBox.Show("Error fetching profile photo: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return profileImage
    End Function


End Class


