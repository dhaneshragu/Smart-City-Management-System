Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class TransportAdminRSReq
    Private Property uid As Integer
    Private Property u_name As String


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

        cmd = New MySqlCommand("SELECT uid as D_uid, vehicle_id, start_datetime, capacity, src_id, dest_id, status from ride_sharing_entries", Con)
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()
        Con.Close()




    End Sub

    Private Sub AddPost(dname As String,
                          Optional pname As String = "",
                          Optional vehid As String = "",
                           Optional datetime As String = "",
                           Optional fromPlace As String = "",
                           Optional toPlace As String = "",
                           Optional dlid As String = "",
                           Optional capacity As Integer = 0,
                          Optional postNum As Integer = 0,
                          Optional image As Image = Nothing)
        Dim RidePost As TransportRSReqPost
        RidePost = New TransportRSReqPost()
        With RidePost
            .Name = "post_" & postNum
            .Width = 595
            .Height = 210
        End With
        RidePost.SetDetails(dname, pname, vehid, datetime, fromPlace, toPlace, dlid, capacity, image)
        PostsPanel.Controls.Add(RidePost)
    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        AddPost("DummyVal", "DummyVal", "DummyVal", "21st March, 6:30PM", "IIT", "Airport", "DummyVal", 3, Nothing)
        AddPost("DummyVal", "DummyVal", "DummyVal", "21st March, 6:30PM", "IIT", "Airport", "DummyVal", 7, Nothing)
        AddPost("DummyVal", "DummyVal", "DummyVal", "21st March, 6:30PM", "IIT", "Airport", "DummyVal", 2, Nothing)
        AddPost("DummyVal", "DummyVal", "DummyVal", "21st March, 6:30PM", "IIT", "Airport", "DummyVal", 2, Nothing)

    End Sub


End Class