Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient

Public Class lib_search

    Dim allBooks As New List(Of Entry)
    Structure Entry
        Public BookID As Integer
        Public Author As String
        Public Title As String
        Public DueDate As String
        Public Rating As String
        Public Status As String
        Public Genre As String
        Public RadioButton As RadioButton ' Added RadioButton field
    End Structure

    ' comment
    Dim selectedSearchMode As String = "Empty"
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim lib_dash = New lib_dash() With {
            .uid = uid,
            .u_name = u_name
        }
        lib_dash.Show()
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim lib_borrowed = New lib_borrowed() With {
            .uid = uid,
            .u_name = u_name
        }
        lib_borrowed.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim lib_request = New lib_request() With {
            .uid = uid,
            .u_name = u_name
        }
        lib_request.Show()
        Me.Close()
    End Sub

    Private Sub lib_search_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label9.Text = uid
        LoadAllBooks()
        PopulateTable()

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If selectedSearchMode = "Empty" Then
            MessageBox.Show("Select a search mode first")
            Return
        ElseIf selectedSearchMode = "Book ID" Then

            Dim bookQuery = "SELECT * FROM lib_books WHERE book_ID LIKE '%" & queryBook.Text & "%'"

            Dim Con = Globals.GetDBConnection()
            Dim reader As MySqlDataReader
            Dim cmd As MySqlCommand

            Try
                Con.Open()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            cmd = New MySqlCommand(bookQuery, Con)
            reader = cmd.ExecuteReader

            ' Create a list to store the books
            'Dim allBooks As New List(Of Entry)
            allBooks = New List(Of Entry)

            ' Read the data from the reader
            While reader.Read()
                Dim status As String
                If reader("issued").ToString() = "False" Then
                    status = "Available"
                Else
                    status = "Not Available"
                End If

                allBooks.Add(New Entry With {
                    .BookID = reader("book_ID").ToString(),
                    .Author = reader("author").ToString(),
                    .Title = reader("title").ToString(),
                    .Genre = reader("genre").ToString(),
                    .Rating = reader("rating").ToString(),
                    .Status = status,
                    .RadioButton = New RadioButton()
                })
            End While

            reader.Close()
            Con.Close()

        ElseIf selectedSearchMode = "Author" Then
            'Dim bookQuery = "SELECT * FROM books WHERE (NOT isIssued AND NOT isReserved AND authorName = '" & queryBook.Text & "')"
            Dim bookQuery = "SELECT * FROM lib_books WHERE author LIKE '%" & queryBook.Text & "%'"
            Dim Con = Globals.GetDBConnection()
            Dim reader As MySqlDataReader
            Dim cmd As MySqlCommand

            Try
                Con.Open()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            cmd = New MySqlCommand(bookQuery, Con)
            reader = cmd.ExecuteReader

            ' Create a list to store the books
            'Dim allBooks As New List(Of Entry)
            allBooks = New List(Of Entry)

            ' Read the data from the reader
            While reader.Read()
                Dim status As String
                If reader("issued").ToString() = "False" Then
                    status = "Available"
                Else
                    status = "Not Available"
                End If

                allBooks.Add(New Entry With {
                    .BookID = reader("book_ID").ToString(),
                    .Author = reader("author").ToString(),
                    .Title = reader("title").ToString(),
                    .Genre = reader("genre").ToString(),
                    .Rating = reader("rating").ToString(),
                    .Status = status,
                    .RadioButton = New RadioButton()
                })
            End While

            reader.Close()
            Con.Close()
        ElseIf selectedSearchMode = "Title" Then
            Dim bookQuery = "SELECT * FROM lib_books WHERE title LIKE '%" & queryBook.Text & "%'"
            Dim Con = Globals.GetDBConnection()
            Dim reader As MySqlDataReader
            Dim cmd As MySqlCommand

            Try
                Con.Open()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            cmd = New MySqlCommand(bookQuery, Con)
            reader = cmd.ExecuteReader

            ' Create a list to store the books
            'Dim allBooks As New List(Of Entry)
            allBooks = New List(Of Entry)

            ' Read the data from the reader
            While reader.Read()
                Dim status As String
                If reader("issued").ToString() = "False" Then
                    status = "Available"
                Else
                    status = "Not Available"
                End If

                allBooks.Add(New Entry With {
                    .BookID = reader("book_ID").ToString(),
                    .Author = reader("author").ToString(),
                    .Title = reader("title").ToString(),
                    .Genre = reader("genre").ToString(),
                    .Rating = reader("rating").ToString(),
                    .Status = status,
                    .RadioButton = New RadioButton()
                })
            End While

            reader.Close()
            Con.Close()
        ElseIf selectedSearchMode = "Category" Then
            'Dim bookQuery = "SELECT * FROM books WHERE (NOT isIssued AND NOT isReserved AND Subject like '%" & queryBook.Text & "%')"
            Dim bookQuery = "SELECT * FROM lib_books WHERE genre LIKE '%" & queryBook.Text & "%'"
            Dim Con = Globals.GetDBConnection()
            Dim reader As MySqlDataReader
            Dim cmd As MySqlCommand

            Try
                Con.Open()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            cmd = New MySqlCommand(bookQuery, Con)
            reader = cmd.ExecuteReader

            ' Create a list to store the books
            'Dim allBooks As New List(Of Entry)
            allBooks = New List(Of Entry)

            ' Read the data from the reader
            While reader.Read()
                Dim status As String
                If reader("issued").ToString() = "False" Then
                    status = "Available"
                Else
                    status = "Not Available"
                End If

                allBooks.Add(New Entry With {
                    .BookID = reader("book_ID").ToString(),
                    .Author = reader("author").ToString(),
                    .Title = reader("title").ToString(),
                    .Genre = reader("genre").ToString(),
                    .Rating = reader("rating").ToString(),
                    .Status = status,
                    .RadioButton = New RadioButton()
                })
            End While

            reader.Close()
            Con.Close()
        End If
        PopulateTable()
    End Sub

    Private Sub PopulateTable()

        searchBooksTablePanel.Controls.Clear()
        searchBooksTablePanel.RowStyles.Clear()
        'searchBooksTablePanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 50)) ' Set the height to 50 pixels for the first row
        ' Set the height to 50 pixels for all rows
        For rowIndex As Integer = 0 To allBooks.Count - 1
            searchBooksTablePanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 50))
        Next

        ' Add allBooks to the table
        For rowIndex As Integer = 0 To allBooks.Count - 1
            Dim entry As Entry = allBooks(rowIndex)

            ' Add book details
            Dim bookIdLabel As New Label()
            bookIdLabel.Text = entry.BookID.ToString()
            searchBooksTablePanel.Controls.Add(bookIdLabel, 0, rowIndex)
            bookIdLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
            bookIdLabel.Anchor = AnchorStyles.None ' Set Anchor to None

            Dim authorLabel As New Label()
            authorLabel.Text = entry.Author
            searchBooksTablePanel.Controls.Add(authorLabel, 1, rowIndex)
            authorLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
            authorLabel.Anchor = AnchorStyles.None ' Set Anchor to None
            authorLabel.AutoSize = True

            Dim titleLabel As New Label()
            titleLabel.Text = entry.Title
            searchBooksTablePanel.Controls.Add(titleLabel, 2, rowIndex)
            titleLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
            titleLabel.Anchor = AnchorStyles.None ' Set Anchor to None
            titleLabel.AutoSize = True

            Dim genreLabel As New Label()
            genreLabel.Text = entry.Genre
            searchBooksTablePanel.Controls.Add(genreLabel, 3, rowIndex)
            genreLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
            genreLabel.Anchor = AnchorStyles.None ' Set Anchor to None
            genreLabel.AutoSize = True



            Dim statusLabel As New Label()
            statusLabel.Text = entry.Status
            searchBooksTablePanel.Controls.Add(statusLabel, 4, rowIndex)
            statusLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
            statusLabel.Anchor = AnchorStyles.None ' Set Anchor to None
            statusLabel.AutoSize = True

            ' Set color based on status
            If entry.Status = "Available" Then
                statusLabel.ForeColor = Color.Green
            Else
                statusLabel.ForeColor = Color.Red
            End If

            Dim ratingPanel As New Panel()
            ratingPanel.AutoSize = True
            ratingPanel.Anchor = AnchorStyles.None ' Ensure the panel is not anchored to any side
            ratingPanel.Location = New Point((searchBooksTablePanel.Width - ratingPanel.Width) / 2, (searchBooksTablePanel.Height - ratingPanel.Height) / 2)
            searchBooksTablePanel.Controls.Add(ratingPanel, 5, rowIndex)

            Dim ratingPictureBox As New PictureBox()
            ratingPictureBox.Image = SmartCityMgmtSystem.My.Resources.Resources.icons8_star_48
            ratingPictureBox.SizeMode = PictureBoxSizeMode.Zoom
            ratingPictureBox.Size = New Size(30, 30)  ' Set the size of the PictureBox
            ratingPictureBox.Margin = New Padding(0, 0, 0, 0) ' Set margin to 0 to ensure the image is not pushed away
            ratingPanel.Controls.Add(ratingPictureBox)

            Dim ratingLabel As New Label()
            ratingLabel.Text = entry.Rating & "/5"
            ratingLabel.AutoSize = True
            ratingLabel.Padding = New Padding(35, (ratingPanel.Height - ratingLabel.Height) / 2, 0, 0) ' Vertically center the text
            ratingPanel.Controls.Add(ratingLabel)









            'Dim ratingLabel As New Label()
            'ratingLabel.Text = entry.Rating
            'searchBooksTablePanel.Controls.Add(ratingLabel, 5, rowIndex)
            'ratingLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
            'ratingLabel.Anchor = AnchorStyles.None ' Set Anchor to None


            ' Add radio button for options
            'searchBooksTablePanel.Controls.Add(allBooks(rowIndex).RadioButton, 5, rowIndex)
            'entry.RadioButton.TextAlign = ContentAlignment.MiddleCenter ' center the radio button
            'entry.RadioButton.Anchor = AnchorStyles.None ' set anchor to none
            'entry.RadioButton.Size = New Size(16, 16) ' set the size of the radio button

        Next

        Dim adjustLabel3 As New Label()
        adjustLabel3.Text = ""
        searchBooksTablePanel.Controls.Add(adjustLabel3, 1, allBooks.Count + 1)

    End Sub

    Private Sub LoadAllBooks()
        allBooks.Clear()
        Dim bookQuery = "SELECT * FROM lib_books"
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand(bookQuery, Con)
        reader = cmd.ExecuteReader

        ' Create a list to store the books
        'Dim allBooks As New List(Of Entry)
        allBooks = New List(Of Entry)

        ' Read the data from the reader
        While reader.Read()
            Dim status As String
            If reader("issued").ToString() = "False" Then
                status = "Available"
            Else
                status = "Not Available"
            End If

            allBooks.Add(New Entry With {
                    .BookID = reader("book_ID").ToString(),
                    .Author = reader("author").ToString(),
                    .Title = reader("title").ToString(),
                    .Rating = reader("rating").ToString(),
                    .Genre = reader("genre").ToString(),
                    .Status = status,
                    .RadioButton = New RadioButton()
                })
        End While

        reader.Close()
        Con.Close()
    End Sub

    Private Sub srchSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles srchSelect.SelectedIndexChanged
        selectedSearchMode = srchSelect.SelectedItem.ToString()
    End Sub

    Private Sub queryBook_TextChanged(sender As Object, e As EventArgs) Handles queryBook.TextChanged

    End Sub

    Private Sub queryBook_Click(sender As Object, e As EventArgs) Handles queryBook.Click
        queryBook.Text = ""
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim HomePageDashboard = New HomePageDashboard() With {
            .uid = uid
        }
        HomePageDashboard.Show()
        Me.Close()
    End Sub

    Private Sub searchBooksTablePanel_Paint(sender As Object, e As PaintEventArgs) Handles searchBooksTablePanel.Paint

    End Sub

    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub
End Class