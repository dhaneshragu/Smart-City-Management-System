Imports System.Data.SqlClient
Imports System.IO
Imports MySql.Data.MySqlClient
Public Class TransportAdminRSReq
    Private Property uid As Integer
    Private Property u_name As String
    Private postNum As Integer
    Public filter_type As String
    Private Sub AddPost(reqid As Integer,
                       dname As String,
                          Optional vehid As String = "",
                           Optional datetime As String = "",
                           Optional fromPlace As String = "",
                           Optional toPlace As String = "",
                           Optional dlid As String = "",
                            Optional fare_per_person As Integer = 0,
                           Optional capacity As Integer = 0,
                           Optional postNum As Integer = 0,
                         Optional Status As String = "",
                          Optional image As Image = Nothing)
        Dim RidePost As TransportRSReqPost
        RidePost = New TransportRSReqPost()
        With RidePost
            .Name = "post_" & postNum
            .Width = 800
            .Height = 220
        End With
        RidePost.SetDetails(reqid, dname, vehid, datetime, fromPlace, toPlace, dlid, fare_per_person, capacity, Status, image, postNum)
        PostsPanel.Controls.Add(RidePost)
    End Sub

    Public Sub LoadPosts(Optional filter_type As String = "")
        ' Clear existing posts
        PostsPanel.Controls.Clear()
        Dim query As String = "SELECT rs.uid, rs.req_id, users.name, users.profile_photo, rs.vehicle_id, rs.capacity, rs.fare_per_person, COALESCE(MAX(dl.dl_id), 'NONE') as dl_id,
                                CONCAT(DATE_FORMAT(rs.start_datetime, '%d-%m-%Y'), ', ', DATE_FORMAT(rs.start_datetime, '%h:%i %p')) AS start_datetime, 
                                src.place_name AS src, dest.place_name AS dest , rs.status
                                FROM 
                                    ride_sharing_entries rs 
                                JOIN 
                                    placedb src ON rs.src_id = src.id 
                                JOIN 
                                    placedb dest ON rs.dest_id = dest.id 
                                JOIN 
                                    users ON users.user_id = rs.uid 
                                LEFT JOIN
                                     dl_entries dl ON dl.uid = rs.uid
                                GROUP BY rs.uid, rs.req_id, users.name, users.profile_photo, rs.vehicle_id, rs.capacity, rs.fare_per_person,
                                    rs.start_datetime, src.place_name, dest.place_name, rs.status
                                ORDER BY 
                                    rs.start_datetime ASC;"
        If filter_type <> "All" Then
            query = "SELECT rs.uid, rs.req_id, users.name, users.profile_photo, rs.vehicle_id, rs.capacity, rs.fare_per_person, COALESCE(MAX(dl.dl_id), 'NONE') as dl_id,
                                CONCAT(DATE_FORMAT(rs.start_datetime, '%d-%m-%Y'), ', ', DATE_FORMAT(rs.start_datetime, '%h:%i %p')) AS start_datetime, 
                                src.place_name AS src, dest.place_name AS dest , rs.status
                                FROM 
                                    ride_sharing_entries rs 
                                JOIN 
                                    placedb src ON rs.src_id = src.id 
                                JOIN 
                                    placedb dest ON rs.dest_id = dest.id 
                                JOIN 
                                    users ON users.user_id = rs.uid 
                                LEFT JOIN
                                     dl_entries dl ON dl.uid = rs.uid
                                where 
                                     rs.status = @Status
                                GROUP BY rs.uid, rs.req_id, users.name, users.profile_photo, rs.vehicle_id, rs.capacity, rs.fare_per_person,
                                    rs.start_datetime, src.place_name, dest.place_name, rs.status
                                ORDER BY 
                                    rs.start_datetime ASC;"
        End If
        Using connection As New MySqlConnection(Globals.getdbConnectionString())
            Using command As New MySqlCommand(query, connection)
                connection.Open()
                If filter_type <> "All" Then
                    command.Parameters.AddWithValue("@Status", filter_type)
                End If
                postNum = 0
                ' Execute the command
                Using reader As MySqlDataReader = command.ExecuteReader()
                    ' Read each row
                    Dim row As Integer = 0
                    While reader.Read()
                        ' Access columns by name or index
                        row += 1
                        Dim req_id As Integer = Convert.ToInt32(reader("req_id"))
                        Dim dl_id As String = If(Not IsDBNull(reader("dl_id")), Convert.ToString(reader("dl_id")), "NONE")
                        Dim userName As String = reader("name").ToString()
                        Dim vehicleId As String = reader("vehicle_id")
                        Dim capacity As Integer = Convert.ToInt32(reader("capacity"))
                        Dim farePerPerson As Integer = Convert.ToInt32(reader("fare_per_person"))
                        Dim startDatetime As String = reader("start_datetime").ToString()
                        Dim src As String = reader("src").ToString()
                        Dim dest As String = reader("dest").ToString()
                        Dim Status As String = reader("status").ToString()
                        Dim picture As Image = Nothing
                        If Not reader.IsDBNull(reader.GetOrdinal("profile_photo")) Then
                            Dim photoData As Byte() = DirectCast(reader("profile_photo"), Byte())
                            Using ms As New MemoryStream(photoData)
                                picture = Image.FromStream(ms)
                            End Using
                        End If
                        postNum += 1
                        'Add to PostsPanel
                        AddPost(req_id, userName, vehicleId, startDatetime, src, dest, dl_id, farePerPerson, capacity, postNum, Status, picture)
                    End While
                    'MessageBox.Show("Number of Posts :" & row, "", MessageBoxButtons.OK)

                End Using
                connection.Close()
            End Using
        End Using

    End Sub
    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("All")
        ComboBox1.Items.Add("requested")
        ComboBox1.Items.Add("rejected")
        ComboBox1.Items.Add("approved")
        ComboBox1.SelectedValue = "All"
        filter_type = "All"
        LoadPosts(filter_type)

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs)
        If ComboBox1.SelectedItem IsNot Nothing AndAlso ComboBox1.SelectedItem.ToString() <> "" Then
            filter_type = ComboBox1.SelectedItem.ToString()
            LoadPosts(filter_type)
        Else
            ' Handle case when nothing is selected in ComboBox1
            filter_type = "All"
            LoadPosts(filter_type)
        End If
    End Sub


End Class