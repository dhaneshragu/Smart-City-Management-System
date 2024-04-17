Imports MySql.Data.MySqlClient

Public Class lib_ebooks

    Dim alleBooks As New List(Of Entry)
    Structure Entry
        Public BookID As Integer
        Public Author As String
        Public Title As String
        Public Rating As String
        Public Link As String
        Public Genre As String
        Public RadioButton As RadioButton ' Added RadioButton field
    End Structure

    Dim selectedSearchMode As String = "Empty"
    ' comment

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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim lib_request = New lib_request() With {
            .uid = uid,
            .u_name = u_name
        }
        lib_request.Show()
        Me.Close()
    End Sub

    Private Sub lib_ebooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label12.Text = uid
        Panel9.Visible = False
        LoadAlleBooks()
        PopulateTable()
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If selectedSearchMode = "Empty" Then
            MessageBox.Show("Select a search mode first")
            Return
        ElseIf selectedSearchMode = "Book ID" Then

            Dim bookQuery = "SELECT * FROM lib_ebooks WHERE book_ID LIKE '%" & queryBook.Text & "%'"

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
            alleBooks = New List(Of Entry)

            ' Read the data from the reader
            While reader.Read()
                'Dim status As String
                'If reader("issued").ToString() = "False" Then
                'status = "Available"
                'Else
                'status = "Not Available"
                'End If

                alleBooks.Add(New Entry With {
                    .BookID = reader("book_ID").ToString(),
                    .Author = reader("author").ToString(),
                    .Title = reader("title").ToString(),
                    .Genre = reader("genre").ToString(),
                    .Rating = reader("rating").ToString(),
                    .Link = reader("link").ToString(),
                    .RadioButton = New RadioButton()
                })
            End While

            reader.Close()
            Con.Close()

        ElseIf selectedSearchMode = "Author" Then
            'Dim bookQuery = "SELECT * FROM books WHERE (NOT isIssued AND NOT isReserved AND authorName = '" & queryBook.Text & "')"
            Dim bookQuery = "SELECT * FROM lib_ebooks WHERE author LIKE '%" & queryBook.Text & "%'"
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
            alleBooks = New List(Of Entry)

            ' Read the data from the reader
            While reader.Read()
                'Dim status As String
                'If reader("issued").ToString() = "False" Then
                'status = "Available"
                'Else
                'status = "Not Available"
                'End If

                alleBooks.Add(New Entry With {
                    .BookID = reader("book_ID").ToString(),
                    .Author = reader("author").ToString(),
                    .Title = reader("title").ToString(),
                    .Genre = reader("genre").ToString(),
                    .Rating = reader("rating").ToString(),
                    .Link = reader("link").ToString(),
                    .RadioButton = New RadioButton()
                })
            End While

            reader.Close()
            Con.Close()
        ElseIf selectedSearchMode = "Title" Then
            Dim bookQuery = "SELECT * FROM lib_ebooks WHERE title LIKE '%" & queryBook.Text & "%'"
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
            alleBooks = New List(Of Entry)

            ' Read the data from the reader
            While reader.Read()
                'Dim status As String
                'If reader("issued").ToString() = "False" Then
                'status = "Available"
                'Else
                'status = "Not Available"
                'End If

                alleBooks.Add(New Entry With {
                    .BookID = reader("book_ID").ToString(),
                    .Author = reader("author").ToString(),
                    .Title = reader("title").ToString(),
                    .Genre = reader("genre").ToString(),
                    .Rating = reader("rating").ToString(),
                    .Link = reader("link").ToString(),
                    .RadioButton = New RadioButton()
                })
            End While

            reader.Close()
            Con.Close()
        ElseIf selectedSearchMode = "Category" Then
            'Dim bookQuery = "SELECT * FROM books WHERE (NOT isIssued AND NOT isReserved AND Subject like '%" & queryBook.Text & "%')"
            Dim bookQuery = "SELECT * FROM lib_ebooks WHERE genre LIKE '%" & queryBook.Text & "%'"
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
            alleBooks = New List(Of Entry)

            ' Read the data from the reader
            While reader.Read()
                'Dim status As String
                'If reader("issued").ToString() = "False" Then
                'status = "Available"
                'Else
                'status = "Not Available"
                'End If

                alleBooks.Add(New Entry With {
                    .BookID = reader("book_ID").ToString(),
                    .Author = reader("author").ToString(),
                    .Title = reader("title").ToString(),
                    .Genre = reader("genre").ToString(),
                    .Rating = reader("rating").ToString(),
                    .Link = reader("link").ToString(),
                    .RadioButton = New RadioButton()
                })
            End While

            reader.Close()
            Con.Close()
        End If
        PopulateTable()
    End Sub

    Private Sub PopulateTable()

        eBooksTablePanel.Controls.Clear()
        eBooksTablePanel.RowStyles.Clear()
        'eBooksTablePanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 50)) ' Set the height to 50 pixels for the first row
        ' Set the height to 50 pixels for all rows
        For rowIndex As Integer = 0 To alleBooks.Count - 1
            eBooksTablePanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 50))
        Next

        ' Add alleBooks to the table
        For rowIndex As Integer = 0 To alleBooks.Count - 1
            Dim entry As Entry = alleBooks(rowIndex)

            ' Add book details
            Dim bookIdLabel As New Label()
            bookIdLabel.Text = entry.BookID.ToString()
            eBooksTablePanel.Controls.Add(bookIdLabel, 0, rowIndex)
            bookIdLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
            bookIdLabel.Anchor = AnchorStyles.None ' Set Anchor to None

            Dim authorLabel As New Label()
            authorLabel.Text = entry.Author
            eBooksTablePanel.Controls.Add(authorLabel, 1, rowIndex)
            authorLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
            authorLabel.Anchor = AnchorStyles.None ' Set Anchor to None
            authorLabel.AutoSize = True

            Dim titleLabel As New Label()
            titleLabel.Text = entry.Title
            eBooksTablePanel.Controls.Add(titleLabel, 2, rowIndex)
            titleLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
            titleLabel.Anchor = AnchorStyles.None ' Set Anchor to None
            titleLabel.AutoSize = True

            Dim genreLabel As New Label()
            genreLabel.Text = entry.Genre
            eBooksTablePanel.Controls.Add(genreLabel, 3, rowIndex)
            genreLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
            genreLabel.Anchor = AnchorStyles.None ' Set Anchor to None
            genreLabel.AutoSize = True


            Dim ratingPanel As New Panel()
            ratingPanel.AutoSize = True
            ratingPanel.Anchor = AnchorStyles.None ' Ensure the panel is not anchored to any side
            ratingPanel.Location = New Point((eBooksTablePanel.Width - ratingPanel.Width) / 2, (eBooksTablePanel.Height - ratingPanel.Height) / 2)
            eBooksTablePanel.Controls.Add(ratingPanel, 4, rowIndex)

            Dim ratingPictureBox As New PictureBox()
            ratingPictureBox.Image = SmartCityMgmtSystem.My.Resources.Resources.icons8_star_48
            ratingPictureBox.SizeMode = PictureBoxSizeMode.Zoom
            ratingPictureBox.Size = New Size(30, 30)  ' Set the size of the PictureBox
            ratingPictureBox.Margin = New Padding(0, 0, 0, 0) ' Set margin to 0 to ensure the image is not pushed away
            ratingPanel.Controls.Add(ratingPictureBox)

            Dim ratingLabel As New Label()
            ratingLabel.Text = entry.Rating & "/5"
            ratingLabel.AutoSize = True
            ratingLabel.Padding = New Padding(30, (ratingPanel.Height - ratingLabel.Height) / 2, 0, 0) ' Vertically center the text
            ratingPanel.Controls.Add(ratingLabel)

            'Dim ratingLabel As New Label()
            'eBooksTablePanel.Controls.Add(ratingLabel, 4, rowIndex)
            'ratingLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
            'ratingLabel.Anchor = AnchorStyles.None ' Set Anchor to None
            'ratingLabel.AutoSize = True
            'ratingLabel.Text = entry.Status

            ' Set color based on status
            'If entry.Status = "Available" Then
            '   statusLabel.ForeColor = Color.Green
            'Else
            '   statusLabel.ForeColor = Color.Red
            'End If

            Dim linkLabel As New LinkLabel()
            linkLabel.Text = "Click here"
            linkLabel.LinkBehavior = LinkBehavior.HoverUnderline
            linkLabel.LinkColor = Color.Blue
            linkLabel.Cursor = Cursors.Hand
            linkLabel.Margin = New Padding(0)
            linkLabel.Font = New Font(linkLabel.Font.FontFamily, 10, linkLabel.Font.Style) ' Set the font size to 12
            linkLabel.Padding = New Padding(0, 3, 0, 0) ' Adjust the padding to center the text vertically
            linkLabel.TextAlign = ContentAlignment.BottomCenter ' Center the label both horizontally and vertically
            linkLabel.AutoSize = True
            AddHandler linkLabel.LinkClicked, Sub(sender As Object, e As LinkLabelLinkClickedEventArgs)
                                                  Process.Start(entry.Link)
                                              End Sub
            eBooksTablePanel.Controls.Add(linkLabel, 5, rowIndex)

            'Dim linkLabel As New LinkLabel()
            'linkLabel.Text = "Click here"
            'linkLabel.LinkBehavior = LinkBehavior.HoverUnderline
            'linkLabel.LinkColor = Color.Blue
            'linkLabel.Cursor = Cursors.Hand
            'linkLabel.TextAlign = ContentAlignment.BottomCenter ' Center the label both horizontally and vertically

            '' Create a TableLayoutPanel with a single cell and add the linkLabel to it
            'Dim linkPanel As New TableLayoutPanel()
            'linkPanel.RowCount = 1
            'linkPanel.ColumnCount = 1
            'linkPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
            'linkPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 100))
            'linkPanel.Controls.Add(linkLabel, 0, 0)

            'AddHandler linkLabel.LinkClicked, Sub(sender As Object, e As LinkLabelLinkClickedEventArgs)
            '                                      Process.Start(entry.Link)
            '                                  End Sub

            'eBooksTablePanel.Controls.Add(linkPanel, 5, rowIndex)




            ' Add radio button for options
            eBooksTablePanel.Controls.Add(alleBooks(rowIndex).RadioButton, 6, rowIndex)
            entry.RadioButton.TextAlign = ContentAlignment.MiddleCenter ' center the radio button
            entry.RadioButton.Anchor = AnchorStyles.None ' set anchor to none
            entry.RadioButton.Size = New Size(16, 16) ' set the size of the radio button

        Next

        Dim adjustLabel3 As New Label()
        adjustLabel3.Text = ""
        eBooksTablePanel.Controls.Add(adjustLabel3, 1, alleBooks.Count + 1)

    End Sub

    Private Sub LoadAlleBooks()
        alleBooks.Clear()
        Dim bookQuery = "SELECT * FROM lib_ebooks"
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
        alleBooks = New List(Of Entry)

        ' Read the data from the reader
        While reader.Read()
            'Dim status As String
            'If reader("issued").ToString() = "False" Then
            '   status = "Available"
            'Else
            '   status = "Not Available"
            'End If

            alleBooks.Add(New Entry With {
                    .BookID = reader("book_ID").ToString(),
                    .Author = reader("author").ToString(),
                    .Title = reader("title").ToString(),
                    .Genre = reader("genre").ToString(),
                    .Rating = reader("rating").ToString(),
                    .Link = reader("link").ToString(),
                    .RadioButton = New RadioButton()
                })
        End While

        reader.Close()
        Con.Close()
    End Sub

    Private Sub queryBook_TextChanged(sender As Object, e As EventArgs) Handles queryBook.TextChanged

    End Sub

    Private Sub queryBook_Click(sender As Object, e As EventArgs) Handles queryBook.Click
        queryBook.Text = ""
    End Sub

    Private Sub srchSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles srchSelect.SelectedIndexChanged
        selectedSearchMode = srchSelect.SelectedItem.ToString()
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

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim ratingInput As Integer
        If Integer.TryParse(TextBox2.Text, ratingInput) AndAlso ratingInput >= 0 AndAlso ratingInput <= 5 Then
            Dim queryCheck As String = "SELECT COUNT(*) FROM lib_book_ratings WHERE book_ID = @rateID and book_type = 1 and uid = @uid"
            Dim queryUpdate As String = "UPDATE lib_book_ratings SET rating = @ratingInput WHERE book_ID = @rateID and book_type = 1 and uid = @uid"
            Dim queryInsert As String = "INSERT INTO lib_book_ratings (book_ID, uid, book_type, rating) VALUES (@rateID, @uid, @book_type, @ratingInput)"

            Dim Con = Globals.GetDBConnection()
            Try
                Con.Open()
                Dim cmdCheck As New MySqlCommand(queryCheck, Con)
                cmdCheck.Parameters.AddWithValue("@rateID", rateID)
                cmdCheck.Parameters.AddWithValue("@uid", uid)

                Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

                If count > 0 Then
                    ' If a row exists with the given book_ID, update the rating
                    Dim cmdUpdate As New MySqlCommand(queryUpdate, Con)
                    cmdUpdate.Parameters.AddWithValue("@rateID", rateID)
                    cmdUpdate.Parameters.AddWithValue("@ratingInput", ratingInput)
                    cmdUpdate.Parameters.AddWithValue("@uid", uid)
                    cmdUpdate.ExecuteNonQuery()
                Else
                    ' If no row exists with the given book_ID, insert a new row
                    Dim cmdInsert As New MySqlCommand(queryInsert, Con)
                    cmdInsert.Parameters.AddWithValue("@rateID", rateID)
                    cmdInsert.Parameters.AddWithValue("@ratingInput", ratingInput)
                    cmdInsert.Parameters.AddWithValue("@uid", uid)
                    cmdInsert.Parameters.AddWithValue("@book_type", 1)
                    cmdInsert.ExecuteNonQuery()
                End If

                ' Calculate and update the average rating in the lib_books table
                Dim queryAvgRating As String = "UPDATE lib_ebooks SET rating = (SELECT AVG(rating) FROM lib_book_ratings WHERE book_ID = @rateID AND book_type = 1) WHERE book_ID = @rateID"
                Dim cmdAvgRating As New MySqlCommand(queryAvgRating, Con)
                cmdAvgRating.Parameters.AddWithValue("@rateID", rateID)
                cmdAvgRating.ExecuteNonQuery()

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            Finally
                If Con.State = ConnectionState.Open Then
                    Con.Close() ' Close the connection in the finally block to ensure it's closed even if an exception occurs
                End If
                MessageBox.Show("Rating Successful")
            End Try
            Panel9.Visible = False
            TextBox2.Text = "Enter your Rating"
        Else
            ' If TextBox2.Text is not a valid integer or not within the range, display an error message
            MessageBox.Show("Please enter a valid integer between 0 and 5")
            Return ' Exit the method if input is invalid
        End If

        LoadAlleBooks()
        PopulateTable()


    End Sub

    Dim rateID As Integer

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim count As Integer = 0
        For Each entry As Entry In alleBooks
            If entry.RadioButton.Checked Then
                count = count + 1
                rateID = entry.BookID

            End If
        Next
        If count = 0 Then
            MessageBox.Show("No book selected.")
            rateID = -1
        Else
            Panel9.Visible = True
        End If
    End Sub

    Private Sub TextBox2_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox2.GotFocus
        ' When the textbox gains focus, clear the placeholder text if it's present
        If TextBox2.Text = "Enter your Rating" Then
            TextBox2.Text = ""
            TextBox2.ForeColor = Color.Black ' Set text color back to black
        End If
    End Sub

    Private Sub TextBox2_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox2.LostFocus
        ' When the textbox loses focus and it's empty, display the placeholder text
        If TextBox2.Text = "" Then
            TextBox2.Text = "Enter your Rating"
            TextBox2.ForeColor = Color.Gray ' Set text color to gray for placeholder text
        End If
    End Sub
End Class