Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class lib_NewRequest
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim lib_search = New lib_search() With {
            .uid = uid,
            .u_name = u_name
        }
        lib_search.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim lib_ebooks = New lib_ebooks() With {
            .uid = uid,
            .u_name = u_name
        }
        lib_ebooks.Show()
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim lib_dash = New lib_dash() With {
            .uid = uid,
            .u_name = u_name
        }
        lib_dash.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim lib_borrowed = New lib_borrowed() With {
            .uid = uid,
            .u_name = u_name
        }
        lib_borrowed.Show()
        Me.Close()
    End Sub

    Private Sub lib_NewRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label9.Text = uid
    End Sub



    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim HomePageDashboard = New HomePageDashboard() With {
            .uid = uid
        }
        HomePageDashboard.Show()
        Me.Close()
    End Sub

    Private Sub btnAddBalance_Click(sender As Object, e As EventArgs) Handles btnAddBalance.Click
        Dim success As Boolean
        ' Define SQL query to insert data into the database
        Dim query As String = "INSERT INTO lib_book_request (title, author, date, status, uid) VALUES (@title, @author, @date, @status, @uid)"

        Dim Con = Globals.GetDBConnection()

        Try
            Con.Open()
            Dim cmd As New MySqlCommand(query, Con)
            ' Dim reader As MySqlDataReader = cmd.ExecuteReader()

            ' Add parameters to the command
            cmd.Parameters.AddWithValue("@title", TextBox2.Text)
            cmd.Parameters.AddWithValue("@author", TextBox1.Text)
            cmd.Parameters.AddWithValue("@date", DateTime.Now)
            cmd.Parameters.AddWithValue("@status", 0) ' Default status to 0
            cmd.Parameters.AddWithValue("@uid", uid)

            ' Execute the command
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            ' Check if any rows were affected
            If rowsAffected > 0 Then
                MessageBox.Show("Request added successfully!")
                success = True
            Else
                MessageBox.Show("No rows were affected. Request not added.")
                success = False
            End If


            ' Clear input fields after successful insertion
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close() ' Close the connection in the finally block to ensure it's closed even if an exception occurs
            End If
        End Try

        If success Then
            Dim lib_request = New lib_request() With {
            .uid = uid,
            .u_name = u_name
        }
            lib_request.Show()
            Me.Close()
        End If
    End Sub
End Class