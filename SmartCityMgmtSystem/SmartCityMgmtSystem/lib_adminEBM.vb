Imports System.Data.SqlClient
Imports FastReport.DataVisualization.Charting
Imports System.Net
Imports MySql.Data.MySqlClient
Public Class lib_adminEBM

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim lib_adminDash = New lib_adminDash()
        lib_adminDash.Show()
        Me.Close()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim lib_admin_eBooks = New lib_admin_eBooks()
        lib_admin_eBooks.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim lib_adminBM = New lib_adminBM()
        lib_adminBM.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim lib_adminMT = New lib_adminMT()
        lib_adminMT.Show()
        Me.Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim lib_adminReq = New lib_adminReq()
        lib_adminReq.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim lib_adminSearch = New lib_adminSearch()
        lib_adminSearch.Show()
        Me.Close()
    End Sub

    Private Sub Update_button_Click(sender As Object, e As EventArgs) Handles Update_button.Click
        If ubook_id.Text = "" Then
            MsgBox("Missing Information", 0 + 0, "Error")
        Else
            Dim query As String = "SELECT book_ID, title, author, genre, link FROM lib_ebooks WHERE book_ID='" & ubook_id.Text & "'"
            Dim Con = Globals.GetDBConnection()

            Try
                Con.Open()
                Dim cmd As New MySqlCommand(query, Con)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                If reader.HasRows Then
                    While reader.Read()
                        Dim bookID As String = reader("book_ID").ToString()
                        Dim author As String = reader("author").ToString()
                        Dim title As String = reader("title").ToString()
                        Dim genre As String = reader("genre").ToString()
                        Dim link As String = reader("link").ToString()

                        bdid.Text = bookID
                        bdauthor.Text = author
                        bdtitle.Text = title
                        bdgenre.Text = genre
                        bdlink.Text = link
                    End While
                Else
                    MessageBox.Show("No records found for the given book ID.", "Information")
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            Finally
                Con.Close() ' Close the connection in the end
            End Try
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim valid As Boolean
        If rbookid.Text = "" Then
            MsgBox("Missing Information", 0 + 0, "Error")
        Else
            Dim query As String = "SELECT * FROM lib_ebooks WHERE book_ID='" & rbookid.Text & "'"
            Dim Con = Globals.GetDBConnection()

            Try
                Con.Open()
                Dim cmd As New MySqlCommand(query, Con)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                If reader.HasRows Then
                    valid = True
                Else
                    MessageBox.Show("No records found for the given book ID.", "Information")
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            Finally
                Con.Close() ' Close the connection in the end
            End Try
        End If

        If valid Then
            Dim query As String = "DELETE FROM lib_ebooks WHERE book_ID = '" & rbookid.Text & "' "
            Dim Con = Globals.GetDBConnection()

            Try
                Con.Open()
                Dim cmd As New MySqlCommand(query, Con)
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MessageBox.Show("Book with ID: " & rbookid.Text & " has been successfully removed.")
                    ' Additional actions if needed after deletion
                    bdid.Text = ""
                    bdauthor.Text = ""
                    bdtitle.Text = ""
                    bdgenre.Text = ""
                    bdlink.Text = ""
                    ubook_id.Text = ""
                    rbookid.Text = ""
                Else
                    MessageBox.Show("No book found with ID: " & rbookid.Text)
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            Finally
                Con.Close() ' Close the connection in the end
            End Try
        End If

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If bdid.Text = "" Or bdauthor.Text = "" Or bdtitle.Text = "" Or bdgenre.Text = "" Or bdlink.Text = "" Then
            MsgBox("Missing Information", 0 + 0, "Error")
        Else
            Dim bookID As Integer
            If Not Integer.TryParse(bdid.Text, bookID) Then
                MessageBox.Show("Invalid Book ID. Please enter a valid integer.")
                Return
            End If

            Dim author As String = bdauthor.Text
            Dim title As String = bdtitle.Text
            Dim genre As String = bdgenre.Text
            Dim link As String = bdlink.Text

            Dim checkQuery As String = "SELECT * FROM lib_ebooks WHERE book_ID='" & bookID & "'"
            Dim bookExists As Boolean = False
            Dim query As String = "INSERT INTO lib_ebooks (book_ID,link, author, title, genre) VALUES ('" & bookID & "','" & link & "','" & author & "','" & title & "','" & genre & "')"

            Dim Con = Globals.GetDBConnection()

            Try
                Con.Open()
                Dim cmdCheck As New MySqlCommand(checkQuery, Con)
                Dim reader As MySqlDataReader = cmdCheck.ExecuteReader()

                If reader.HasRows Then
                    bookExists = True
                    MessageBox.Show("Book with given ID already exists.", "Information")
                End If
                reader.Close()

                If Not bookExists Then
                    Dim cmdInsert As New MySqlCommand(query, Con)
                    Dim rowsAffected As Integer = cmdInsert.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Book details added successfully.")
                        ' Additional actions if needed after update
                    Else
                        MessageBox.Show("Error encountered while adding book with ID: " & bookID)
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            Finally
                Con.Close() ' Close the connection in the end
            End Try
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        bdid.Text = ""
        bdauthor.Text = ""
        bdtitle.Text = ""
        bdgenre.Text = ""
        bdlink.Text = ""
        ubook_id.Text = ""
        rbookid.Text = ""
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If bdid.Text = "" Or bdauthor.Text = "" Or bdtitle.Text = "" Or bdgenre.Text = "" Or bdlink.Text = "" Then
            MsgBox("Missing Information", 0 + 0, "Error")
        Else
            Dim bookID As Integer
            Integer.TryParse(bdid.Text, bookID)
            Dim author As String = bdauthor.Text
            Dim title As String = bdtitle.Text
            Dim genre As String = bdgenre.Text
            Dim link As String = bdlink.Text

            Dim query As String = "UPDATE lib_ebooks SET author='" & author & "',title='" & title & "',genre='" & genre & "',link='" & link & "' WHERE book_ID='" & bookID & "'"
            Dim Con = Globals.GetDBConnection()

            Try
                Con.Open()
                Dim cmd As New MySqlCommand(query, Con)
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MessageBox.Show("Book details updated successfully.")
                    ' Additional actions if needed after update
                Else
                    MessageBox.Show("No book found with ID: " & bookID)
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            Finally
                Con.Close() ' Close the connection in the end
            End Try
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim HomePageDashboard = New HomePageDashboard()
        HomePageDashboard.Show()
        Me.Close()
    End Sub
End Class