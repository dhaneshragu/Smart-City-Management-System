Imports System.Security.Cryptography
Imports MySql.Data.MySqlClient

Public Class complaint_details_users
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"
    Public Property pid As Integer = -1

    Private Sub Load_data()
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand
        Dim reader As MySqlDataReader

        Try
            ' Open the connection
            Con.Open()

            ' Create a MySqlCommand object with the query and connection
            cmd = New MySqlCommand("SELECT complaintid, priority, timestamp, ComplaintTitle, NatureOfcomplaint, Department_Name, ComplaintText, status,Remarks FROM complaints WHERE ComplaintID = @userId", Con)
            cmd.Parameters.AddWithValue("@userId", pid)
            reader = cmd.ExecuteReader()

            ' Loop through the result set
            While reader.Read()
                ' Assign values to specific RichTextBox controls based on column
                RichTextBox1.Text = reader("complaintid").ToString()
                RichTextBox2.Text = reader("priority").ToString()
                RichTextBox3.Text = reader("timestamp").ToString()
                RichTextBox4.Text = reader("ComplaintTitle").ToString()
                RichTextBox5.Text = reader("NatureOfcomplaint").ToString()
                RichTextBox6.Text = reader("Department_Name").ToString()
                RichTextBox9.Text = reader("ComplaintText").ToString()
                RichTextBox7.Text = reader("status").ToString()
                RichTextBox8.Text = reader("Remarks").ToString()


                ' Break the loop after the first row if you only want to display one record
                Exit While
            End While

        Catch ex As Exception
            ' Handle exceptions
            MessageBox.Show("Error: " & ex.Message)

        Finally
            ' Close the reader and connection
            If reader IsNot Nothing Then
                reader.Close()
            End If
            Con.Close()
        End Try



    End Sub
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label9.Text = u_name
        Load_data()
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

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim HomePageDashboard = New HomePageDashboard() With {
            .uid = uid
            }
        HomePageDashboard.Show()
        Me.Close()
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub
End Class
