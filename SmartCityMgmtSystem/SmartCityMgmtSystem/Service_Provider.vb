Imports MySql.Data.MySqlClient

Public Class Service_Provider
    Public Property uid As Integer = 1
    Public Property u_name As String = "Ashish Bharti"
    Private servicePost As Service_Post = Nothing
    Private serviceWithdraw As Service_Withdraw = Nothing
    Private serviceOffered As Service_View_Offered = Nothing
    Private scheduledServices As Service_Scheduled = Nothing
    Private serviceleave As Service_Leave = Nothing
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label3.Text = "Unique Identification No: " & uid
        Dim todaydate As DateTime = DateTime.Now.Date
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT
                                    requester_id AS userID,
                                    service_charge AS charge,
                                    (SELECT service_desc FROM service_desc sd WHERE sd.user_id = ss.provider_id) AS descr,
                                    Department AS depat,
                                    (SELECT name FROM users WHERE user_id = ss.requester_id) AS requester,
                                    schedule_date AS scheduleDate,
                                    start_time AS startTime
                                    From services_scheduled ss
                                    WHERE provider_id = @serviceID
                                    AND schedule_date >= @cur_date
                                    ORDER BY schedule_date ASC,
                                    start_time ASC;"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@serviceID", uid)
                    cmd.Parameters.AddWithValue("@cur_date", todaydate)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            ' Get the first row
                            reader.Read()
                            Dim firstDate As DateTime = reader.GetDateTime("scheduleDate")
                            Dim firstUserID As Integer = reader.GetInt32("userID")
                            Dim dept As String = reader.GetString("depat")
                            Dim requester As String = reader.GetString("requester")

                            ' Print or use the first date and user ID
                            RichTextBox9.Text = dept
                            RichTextBox7.Text = requester
                            DateTimePicker2.Value = firstDate

                            ' Check if there's a second row
                            If reader.Read() Then
                                Dim secondDate As DateTime = reader.GetDateTime("scheduleDate")
                                Dim secondUserID As Integer = reader.GetInt32("userID")
                                Dim dept2 As String = reader.GetString("depat")
                                Dim requester2 As String = reader.GetString("requester")

                                RichTextBox12.Text = dept2
                                RichTextBox14.Text = requester2
                                DateTimePicker3.Value = secondDate
                            End If
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim servicePortal = New Service_Portal() With {
            .uid = uid,
            .u_name = u_name
        }
        Me.Hide()
        servicePortal.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        servicePost?.Dispose()
        servicePost = New Service_Post() With {
            .uid = uid,
            .u_name = u_name
        }
        'Me.Hide()
        'servicePost.Show()
        Globals.viewChildForm(childformPanel, servicePost)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        serviceWithdraw?.Dispose()
        serviceWithdraw = New Service_Withdraw() With {
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, serviceWithdraw)
        'Me.Hide()
        'serviceWithdraw.Show()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        serviceOffered?.Dispose()
        serviceOffered = New Service_View_Offered() With {
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, serviceOffered)
        'Me.Hide()
        'serviceOffered.Show()

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        scheduledServices?.Dispose()
        scheduledServices = New Service_Scheduled() With {
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, scheduledServices)
        'Me.Hide()
        'scheduledServices.Show()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        serviceleave?.Dispose()
        serviceleave = New Service_Leave() With {
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(childformPanel, serviceleave)
        'Me.Hide()
        'scheduledServices.Show()
    End Sub
End Class
