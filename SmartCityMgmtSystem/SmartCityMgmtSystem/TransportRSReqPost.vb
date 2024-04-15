Imports MySql.Data.MySqlClient

Public Class TransportRSReqPost
    Private postnum As Integer
    Private req_id As Integer
    Private acceptclick As Boolean
    Private rejectclick As Boolean
    Private post_status As String
    Public Sub SetDetails(id As Integer,
                         dname As String,
                          Optional vehid As String = "",
                           Optional datetime As String = "",
                           Optional fromPlace As String = "",
                           Optional toPlace As String = "",
                           Optional dlid As String = "",
                          Optional fare_per_person As Integer = 0,
                           Optional capacity As Integer = 0,
                          Optional Status As String = "",
                          Optional image As Image = Nothing,
                          Optional post_no As Integer = 0)
        ' Set the labels
        lbldname.Text = "       " & dname
        lblvid.Text = "        " & vehid
        lbldt.Text = "       " & datetime
        lblfrom.Text = "      " & fromPlace
        lblto.Text = "      " & toPlace
        lbldlid.Text = "        " & dlid
        lblcapacity.Text = "       " & capacity
        lblfpp.Text = "        " & fare_per_person
        postnum = post_no
        req_id = id
        post_status = Status
        If post_status = "approved" Then
            Button1.BringToFront()
        ElseIf post_status = "rejected" Then
            Button2.BringToFront()
        End If
        ' Set the picture box
        If image IsNot Nothing Then
            picbox.Image = image
        End If
    End Sub

    Private Sub LoadandBindData()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand
        Dim reader As MySqlDataReader

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        If acceptclick Then

            Dim query As String = "update ride_sharing_entries set status = @Status where req_id = @req and status = @status1"
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@Status", "approved")
            cmd.Parameters.AddWithValue("@status1", "requested")
            cmd.Parameters.AddWithValue("@req", req_id)
            cmd.ExecuteNonQuery()

            query = "select rs.uid, src.place_name as src, dest.place_name as dest, rs.start_datetime as DT  from ride_sharing_entries rs JOIN placedb src ON rs.src_id = src.id JOIN placedb dest ON rs.dest_id = dest.id where req_id = @r"
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@r", req_id)
            reader = cmd.ExecuteReader()
            'Dim datatable As New DataTable
            'DataTable.Load(reader)
            While reader.Read()
                Dim uid As Integer = Convert.ToInt32(reader("uid"))
                Dim src As String = reader("src").ToString
                Dim dest As String = reader("dest").ToString
                Dim DT As String = reader("DT").ToString
                Globals.SendNotifications(4, uid, "Your ride sharing request is Approved", $"Your ride sharing request with request id {req_id} from {src} to {dest} at {DT} is approved ")
            End While
            reader.Close()
            acceptclick = False
        End If

        If rejectclick Then

            Dim query As String = "update ride_sharing_entries set status = @Status where req_id = @req and status = @status1"
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@Status", "rejected")
            cmd.Parameters.AddWithValue("@status1", "requested")
            cmd.Parameters.AddWithValue("@req", req_id)
            cmd.ExecuteNonQuery()

            query = "select rs.uid, src.place_name as src, dest.place_name as dest, rs.start_datetime as DT  from ride_sharing_entries rs JOIN placedb src ON rs.src_id = src.id JOIN placedb dest ON rs.dest_id = dest.id where req_id = @r"
            cmd = New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@r", req_id)
            reader = cmd.ExecuteReader()
            'Dim datatable As New DataTable
            'DataTable.Load(reader)
            While reader.Read()
                Dim uid As Integer = Convert.ToInt32(reader("uid"))
            Dim src As String = reader("src").ToString
            Dim dest As String = reader("dest").ToString
            Dim DT As String = reader("DT").ToString
                Globals.SendNotifications(4, uid, "Your ride sharing request is Rejected", $"Your ride sharing request with request id {req_id} from {src} to {dest} at {DT} is rejected ")
            End While
            reader.Close()
            rejectclick = False
        End If

        Con.Close()


    End Sub

    Public Sub Btnactionaccept_Click(sender As Object, e As EventArgs) Handles btnactionaccept.Click
        acceptclick = True
        LoadandBindData()
        Button1.BringToFront()
        Dim adminrsreq As TransportAdminRSReq
        adminrsreq = New TransportAdminRSReq()
        adminrsreq.PostsPanel.Controls.Clear()
        Dim filter_type As String = adminrsreq.filter_type
        adminrsreq.LoadPosts(filter_type)
        'MessageBox.Show("ride sharing request will be approved", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Sub Btnactionreject_Click(sender As Object, e As EventArgs) Handles btnactionreject.Click
        rejectclick = True
        LoadandBindData()
        Button2.BringToFront()
        Dim adminrsreq As TransportAdminRSReq
        adminrsreq = New TransportAdminRSReq()
        adminrsreq.PostsPanel.Controls.Clear()
        Dim filter_type As String = adminrsreq.filter_type
        adminrsreq.LoadPosts(filter_type)
        'MessageBox.Show("ride sharing request will be rejected", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
