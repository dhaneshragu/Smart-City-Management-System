Imports MySql.Data.MySqlClient

Public Class Complaint_User_Details
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"

    Private Sub filldata()
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand
        Dim reader As MySqlDataReader

        Try
            ' Open the connection
            Con.Open()

            ' Create a MySqlCommand object with the query and connection
            cmd = New MySqlCommand("SELECT name, email, phone_number,address,occupation FROM users WHERE user_id = @userId", Con)
            cmd.Parameters.AddWithValue("@userId", uid)
            reader = cmd.ExecuteReader()

            ' Loop through the result set
            While reader.Read()
                ' Assign values to specific RichTextBox controls based on column
                RichTextBox1.Text = reader("name").ToString()
                RichTextBox2.Text = reader("email").ToString()
                RichTextBox3.Text = uid
                RichTextBox4.Text = reader("phone_number").ToString()
                RichTextBox5.Text = reader("address").ToString()
                RichTextBox6.Text = reader("occupation").ToString()

                ' Break the loop after the first row if you only want to display one record
                Exit While
            End While

            ' Close the reader
            reader.Close()

        Catch ex As Exception
            ' Handle exceptions
            MessageBox.Show("Error: " & ex.Message)

        Finally
            ' Close the connection
            Con.Close()
        End Try

    End Sub
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label9.Text = u_name
        filldata()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, TransportationAdminHome)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim Complaint_User_Details = New Complaint_User_Details() With {
            .uid = uid,
            .u_name = u_name
        }
        Complaint_User_Details.Show()
        Me.Close()


    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim Complaint_User_Dashboard = New Complaint_User_Dashboard() With {
            .uid = uid,
            .u_name = u_name
        }
        Complaint_User_Dashboard.Show()

        Me.Close()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim Complaints_Lodge_Complaint = New Complaints_Lodge_Complaint() With {
            .uid = uid,
            .u_name = u_name
        }
        Complaints_Lodge_Complaint.Show()

        Me.Close()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Complaints_User_Com_History = New Complaints_User_Com_History() With {
            .uid = uid,
            .u_name = u_name
        }
        Complaints_User_Com_History.Show()
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim HomePageDashboard = New HomePageDashboard() With {
            .uid = uid
            }
        HomePageDashboard.Show()
        Me.Close()
    End Sub

    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub
End Class
