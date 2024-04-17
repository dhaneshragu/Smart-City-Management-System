Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography

Public Class Complaint_User_Info
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"

    Dim userId As Integer

    Private Sub Load_data()
        Dim Con = Globals.GetDBConnection()

        Try
            ' Open the connection
            Con.Open()

            ' Create and execute the command to retrieve user_id

            Using cmd As New MySqlCommand("SELECT user_id FROM complaints WHERE ComplaintID = @userId", Con)
                cmd.Parameters.AddWithValue("@userId", uid)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                ' If there's a result, retrieve user_id and store it in userId
                If reader.Read() Then userId = Convert.ToInt32(reader("user_id"))

                ' Close the reader
                reader.Close()
            End Using

        Catch ex As Exception
            ' Handle exceptions
            MessageBox.Show("Error: " & ex.Message)

        Finally
            ' Close the connection
            If Con.State = ConnectionState.Open Then Con.Close()
        End Try

    End Sub
    Private Sub filldata()
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand
        Dim reader As MySqlDataReader

        Try
            ' Open the connection
            Con.Open()

            ' Create a MySqlCommand object with the query and connection
            cmd = New MySqlCommand("SELECT name, email, phone_number,address,occupation FROM users WHERE user_id = @userId", Con)
            cmd.Parameters.AddWithValue("@userId", userId)
            reader = cmd.ExecuteReader()

            ' Loop through the result set
            While reader.Read()
                ' Assign values to specific RichTextBox controls based on column
                RichTextBox1.Text = reader("name").ToString()
                RichTextBox2.Text = reader("email").ToString()
                RichTextBox3.Text = userId.ToString()
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
        Load_data()
        filldata()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) 
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, TransportationAdminHome)
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

    Private Sub RichTextBox3_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox3.TextChanged

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub
End Class
