Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports System.IO
Public Class TransportTollPlaza
    Public Property uid As Integer = 11
    Public Property u_name As String = "Dhanesh"

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

    'Get the list of the vehicles from vehicleTypeMap
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

    Private Sub LoadPosts(Optional filter_type As Integer = -1)
        ' Clear existing posts
        PostsPanel.Controls.Clear()
        Dim query As String = "SELECT * from fastag_plans"
        If filter_type <> -1 Then
            query = "SELECT * from fastag_plans WHERE vehicle_type = " & filter_type
        End If
        Using connection As New MySqlConnection(Globals.getdbConnectionString())
            Using command As New MySqlCommand(query, connection)
                Try
                    connection.Open()

                    ' Execute the command
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        ' Read each row
                        While reader.Read()
                            ' Access columns by name or index
                            Dim Id As Integer = Convert.ToInt32(reader("id"))
                            Dim veh_type As Integer = Convert.ToInt32(reader("vehicle_type"))
                            Dim validity As Integer = Convert.ToInt32(reader("validity_months"))
                            Dim fee As Integer = Convert.ToInt32(reader("fee_amt"))

                            'Add to PostsPanel
                            AddPostPlan(Id, TransportGlobals.GetVehicleType(veh_type), "Valid for " & validity & " months", fee)
                        End While
                    End Using
                Catch ex As Exception
                    ' Handle the exception by showing a message box
                    MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using


    End Sub

    Private Sub AddPostPurchase(postNum As Integer, vehnum As String,
                           Optional drvlicensenum As String = "12345",
                          Optional dt As String = "21st March 2024",
                           Optional fare As Integer = 0)
        Dim RidePost As FastagPurchase
        RidePost = New FastagPurchase()
        With RidePost
            .Name = "post_" & postNum
        End With
        RidePost.SetDetails(vehnum, drvlicensenum, dt, fare)
        PostsPanel2.Controls.Add(RidePost)
    End Sub
    Private Sub AddPostPlan(postNum As Integer, vehtype As String,
                           Optional validity As String = "Valid for 3 months",
                           Optional fare As Integer = 0)
        Dim RidePost As FastagPlanItem
        RidePost = New FastagPlanItem()

        With RidePost
            .Name = "post_" & postNum
            .uid = uid
        End With
        RidePost.SetDetails(postNum, vehtype, validity, fare)
        PostsPanel.Controls.Add(RidePost)
    End Sub
    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Load the places from database and populate the combobox
        Dim vehicles As List(Of Vehicle) = GetVehicles()
        Dim viewAllItem As New Vehicle(0, "View All")
        vehicles.Insert(0, viewAllItem)

        'To bind the places as their names in the combobox and their IDs as values
        ComboBox1.DataSource = vehicles.ToList()
        ComboBox1.DisplayMember = "Name"
        ComboBox1.ValueMember = "ID"

        ComboBox1.SelectedIndex = 0

        LoadPosts()


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        'Load posts based on a vehicle Type
        Dim vehicleType As Integer = Convert.ToInt32(ComboBox1.SelectedValue)
        LoadPosts(vehicleType)
    End Sub
End Class