Imports MySql.Data.MySqlClient
Public Class Complaints_Lodge_Complaint
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label9.Text = u_name
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

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Con = Globals.GetDBConnection()

        Try
            ' Open the connection
            Con.Open()

            ' SQL query to insert data into columns
            Dim query As String = "INSERT INTO Complaints (user_id,Department_Name,ComplaintText,ComplaintTitle,NatureOfcomplaint,Priority) VALUES (@value1,@value2,@value3,@value4,@value5,@value6)"

            ' Create a MySqlCommand object with the query and connection
            Dim cmd As New MySqlCommand(query, Con)

            ' Add parameters to the query to prevent SQL injection
            cmd.Parameters.AddWithValue("@value1", uid)
            cmd.Parameters.AddWithValue("@value2", ComboBox1.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@value3", TextBox1.Text)
            cmd.Parameters.AddWithValue("@value4", TextBox2.Text)
            cmd.Parameters.AddWithValue("@value5", ComboBox3.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@value6", ComboBox2.SelectedItem.ToString())

            ' Execute the query
            cmd.ExecuteNonQuery()
            MessageBox.Show("Complaint submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Open new page
            Dim Complaint_User_Dashboard = New Complaint_User_Dashboard() With {
            .uid = uid,
            .u_name = u_name
        }
            Complaint_User_Dashboard.Show()

            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        HomePageDashboard.Show()
        Me.Close()
    End Sub
End Class
