Imports MySql.Data.MySqlClient
Public Class FastagPlanItem
    Public Property fastag_id As Integer = 1
    Public Property uid As Integer = 11
    Public Property fare_ As Integer = 20
    Public Sub SetDetails(fastagid As Integer, vehtype As String,
                           Optional validity As String = "Valid for 3 months",
                           Optional fare As Integer = 0)
        ' Set the labels
        lblvehtype.Text = "       " & vehtype
        lblvalidity.Text = "       " & validity
        lblfare.Text = "       ₹" & fare
        fastag_id = fastagid
        fare_ = fare
    End Sub
    Private Function getDLID() As Integer
        'Get the driving license ID given the UID
        Dim dl_id As Integer = 0
        Dim query As String = "SELECT dl_id FROM dl_entries WHERE uid = @uid AND vehicle_type = @veh_typ AND test_status='pass'"
        Using connection As New MySqlConnection(Globals.getdbConnectionString())
            Using command As New MySqlCommand(query, connection)
                ' Add parameters
                command.Parameters.AddWithValue("@uid", uid)
                command.Parameters.AddWithValue("@veh_typ", TransportGlobals.GetVehicleTypeID(lblvehtype.Text.Trim()))
                Try
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        dl_id = reader.GetInt32("dl_id")
                    Else
                        dl_id = -1
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error getting DL ID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
        Return dl_id
    End Function

    Private Function getVehicleNum() As String
        Dim query As String = "SELECT vehicle_id FROM vehicles WHERE uid = @uid AND vehicle_type = @veh_typ"
        Dim vehicle_id As String = ""
        Using connection As New MySqlConnection(Globals.getdbConnectionString())
            Using command As New MySqlCommand(query, connection)
                ' Add parameters
                command.Parameters.AddWithValue("@uid", uid)
                command.Parameters.AddWithValue("@veh_typ", TransportGlobals.GetVehicleTypeID(lblvehtype.Text.Trim()))
                Try
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        vehicle_id = reader.GetString("vehicle_id")
                    Else
                    vehicle_id = ""
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error getting Vehicle ID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Function
    Private Sub btnview_Click(sender As Object, e As EventArgs) Handles btnview.Click
        'Add to fastag purchases
        Dim dl_id As Integer = getDLID()
        If dl_id = -1 Then
            MessageBox.Show("You do not have a valid driving license for this vehicle type: " & lblvehtype.Text.Trim(), "Cant purchase Fastag", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        'Call the payment gateway
        Dim paymentGateway As New PaymentGateway() With
           {
            .uid = uid,
            .readonly_prop = True
        }
        paymentGateway.TextBox1.Text = 5 'Transport minister UID
        paymentGateway.TextBox2.Text = fare_ 'Amount to purchase fastag
        paymentGateway.TextBox3.Text = "Fastag Payment for vehicle type: " & lblvehtype.Text.Trim()
        'If payment is successful, add to fastag purchases
        If paymentGateway.ShowDialog() = DialogResult.OK Then
            Dim currentTimeStamp As DateTime = DateTime.Now
            Dim formattedTimeStamp As String = currentTimeStamp.ToString("yyyy-MM-dd HH:mm:ss")
            Dim query As String = "INSERT INTO fastag_purchases (ft_id, uid, dl_id, bought_on, amt_left) VALUES (@ft, @uid, @dl_id,@dt,@amt)"
            Using connection As New MySqlConnection(Globals.getdbConnectionString())
                Using command As New MySqlCommand(query, connection)
                    ' Add parameters
                    command.Parameters.AddWithValue("@ft", fastag_id)
                    command.Parameters.AddWithValue("@uid", uid)
                    command.Parameters.AddWithValue("@dl_id", dl_id)
                    command.Parameters.AddWithValue("@dt", formattedTimeStamp)
                    command.Parameters.AddWithValue("@amt", fare_)
                    Try
                        connection.Open()
                        If command.ExecuteNonQuery() > 0 Then
                            MessageBox.Show("Fastag purchased successfully", "Fastag Purchased", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Globals.ExecuteUpdateQuery("UPDATE admin_records set toll_revenue = toll_revenue + " & fare_)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error purchasing fastag: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        connection.Close()
                    End Try
                End Using
            End Using
        End If


    End Sub
End Class
