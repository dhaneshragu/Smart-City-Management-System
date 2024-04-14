Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Org.BouncyCastle.Asn1.Anssi
Public Class TransportTGEnter
    Public Property uid As Integer = 11
    Public Property u_name As String = "Dhanesh"
    Public Property vehicle_type As Integer = -1

    'Purchase ID is input to this function
    Public Function CompareTypes(id As Integer) As Boolean
        Dim ans As Boolean = False

        ' Selected vehicle ID from ComboBox
        Dim selectedVehicleId As String = ComboBox1.SelectedItem.ToString()

        ' Query to find type_1 from vehicle_reg table
        Dim queryType1 As String = "SELECT vehicle_type AS type_1 FROM vehicle_reg WHERE vehicle_id = @selectedVehicleId"

        ' Query to find type_2 by joining fastag_purchases and fastag_plans tables
        Dim queryType2 As String = "SELECT fp.vehicle_type AS type_2 FROM fastag_plans AS fp JOIN fastag_purchases AS fpl ON fp.id = fpl.ft_id WHERE fpl.purchase_id = @id AND DATE_ADD(fpl.bought_on, INTERVAL fp.validity_months MONTH) >= '" & DateTime.Today.ToString("yyyy-MM-dd") & "'"

        ' Create a MySqlConnection object
        Using conn As New MySqlConnection(Globals.getdbConnectionString())
            ' Open the connection
            conn.Open()

            ' Execute query to find type_1
            Using cmdType1 As New MySqlCommand(queryType1, conn)
                cmdType1.Parameters.AddWithValue("@selectedVehicleId", selectedVehicleId)

                ' Execute the query and retrieve the result into a DataTable
                Dim type1Result As New DataTable()
                Using adapter As New MySqlDataAdapter(cmdType1)
                    adapter.Fill(type1Result)
                End Using

                ' Execute query to find type_2
                Using cmdType2 As New MySqlCommand(queryType2, conn)
                    cmdType2.Parameters.AddWithValue("@id", id)

                    ' Execute the query and retrieve the result into a DataTable
                    Dim type2Result As New DataTable()
                    Using adapter As New MySqlDataAdapter(cmdType2)
                        adapter.Fill(type2Result)
                    End Using

                    ' Check if type_1 and type_2 exist and compare their values
                    If type1Result.Rows.Count > 0 AndAlso type2Result.Rows.Count > 0 Then
                        Dim type1 As String = type1Result.Rows(0)("type_1").ToString()
                        Dim type2 As String = type2Result.Rows(0)("type_2").ToString()

                        If type1 = type2 Then
                            ans = True
                            'Convert to integer and store as vehicle_type property
                            vehicle_type = Integer.Parse(type1)
                        Else
                            MessageBox.Show("Incompatible Fastag type for the vehicle", "Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    Else
                        MessageBox.Show("Invaid Fastag Purchase ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        End Using
        Return ans
    End Function
    Private Sub GetVehicleNumberComboBox()
        ' Clear existing items from ComboBox
        ComboBox1.Items.Clear()

        ' Query to retrieve approved vehicle numbers
        Dim query As String = "SELECT vehicle_id FROM vehicle_reg WHERE status = 'approved' AND uid=" & uid

        Using con As New MySqlConnection(Globals.getdbConnectionString())
            Dim command As New MySqlCommand(query, con)
            con.Open()

            ' Execute the query and read the results
            Using reader As MySqlDataReader = command.ExecuteReader()
                While reader.Read()
                    ' Add each vehicle number to the ComboBox
                    ComboBox1.Items.Add(reader.GetString("vehicle_id"))
                End While
            End Using

            con.Close()
        End Using
    End Sub
    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Get the vehicle numbers for the user in the combobox using this Subroutine
        GetVehicleNumberComboBox()
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Check if Textbox for Fastag ID is empty / is a number
        If TextBox1.Text = "" Or Not IsNumeric(TextBox1.Text) Then
            MessageBox.Show("Please enter a valid Fastag ID", "Invalid Fastag ID", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        'Check if vehicle number is selected
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Please select a vehicle number", "Invalid Vehicle Number", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If CompareTypes(TextBox1.Text.Trim()) And vehicle_type <> -1 Then
            'Vehicle Type matches , fetch a single toll ID randomly corresponding to the vehicle type from toll_booth db
            Dim query As String = "SELECT lane_id FROM tollboothdb WHERE allowed_vehicle_types = '" & TransportGlobals.GetVehicleType(vehicle_type) & "' ORDER BY RAND() LIMIT 1"
            Using connection As New MySqlConnection(Globals.getdbConnectionString())
                Using command As New MySqlCommand(query, connection)
                    Try
                        connection.Open()
                        Dim toll_id As Integer = command.ExecuteScalar()
                        'Upddate the toll lane in the form and make the enter button visible
                        Label8.Text = toll_id
                        'Unhide the Panel1 for entering the tollgate
                        Panel1.Visible = True
                    Catch ex As Exception
                        MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            End Using


        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Check if there is enough balance in the fastag_purchases, then decremenet the value corresponding to fare_per_vehicle in tollboothdb table
        Dim query As String = "SELECT amt_left FROM fastag_purchases WHERE purchase_id = @purchase_id"
        Using connection As New MySqlConnection(Globals.getdbConnectionString())
            Try
                connection.Open()

                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@purchase_id", TextBox1.Text.Trim())
                    Dim amt_left As Integer = command.ExecuteScalar()

                    'Get the fare_per_vehicle from tollboothdb corresponding to the lane number allotted
                    Dim queryToll As String = "SELECT fare_per_vehicle FROM tollboothdb WHERE lane_id = @lane_id"
                    Using commandToll As New MySqlCommand(queryToll, connection)
                        commandToll.Parameters.AddWithValue("@lane_id", Label8.Text.Trim())
                        Dim fare_per_vehicle As Integer = commandToll.ExecuteScalar()

                        If amt_left >= fare_per_vehicle Then
                            'Update the amount left in the fastag_purchases table
                            Dim updateQuery As String = "UPDATE fastag_purchases SET amt_left = amt_left - @fare_per_vehicle WHERE purchase_id = @purchase_id"
                            Using updateCommand As New MySqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@purchase_id", TextBox1.Text.Trim())
                                updateCommand.Parameters.AddWithValue("@fare_per_vehicle", fare_per_vehicle)
                                updateCommand.ExecuteNonQuery()
                            End Using

                            ' Insert into toll booth
                            ' Enter the tollgate - insert into toll_entries vehicle_id, ft_id, lane_id, time_stamp
                            Dim insertQuery As String = "INSERT INTO toll_entries (vehicle_id, ft_id, lane_id, timestamp) VALUES (@vehicle_id, @ft_id, @lane_id, @time_stamp)"
                            Using insertCommand As New MySqlCommand(insertQuery, connection)
                                insertCommand.Parameters.AddWithValue("@vehicle_id", ComboBox1.SelectedItem.ToString())
                                insertCommand.Parameters.AddWithValue("@ft_id", TextBox1.Text.Trim())
                                insertCommand.Parameters.AddWithValue("@lane_id", Label8.Text.Trim())
                                insertCommand.Parameters.AddWithValue("@time_stamp", DateTime.Now)
                                insertCommand.ExecuteNonQuery()
                            End Using

                            MessageBox.Show("Tollgate entry successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Label7.Visible = True
                        Else
                            MessageBox.Show("Insufficient balance in Fastag, Entry denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    End Using ' End of commandToll
                End Using ' End of command
            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                connection.Close()
            End Try
        End Using ' End of connection


    End Sub
End Class