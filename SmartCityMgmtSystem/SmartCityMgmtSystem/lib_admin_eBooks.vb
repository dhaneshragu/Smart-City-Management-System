Imports MySql.Data.MySqlClient

Public Class lib_admin_eBooks


    Dim alleBooks As New List(Of Entry)
    Structure Entry
        Public BookID As Integer
        Public Author As String
        Public Title As String
        Public Rating As String
        Public Link As String
        Public Genre As String
        'Public RadioButton As RadioButton ' Added RadioButton field
    End Structure

    Dim selectedSearchMode As String = "Empty"
    'Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
    '    Dim lib_adminDash = New lib_adminDash()
    '    lib_adminDash.Show()
    '    Me.Close()
    'End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim lib_adminSearch = New lib_adminSearch()
        lib_adminSearch.Show()
        Me.Close()
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Dim lib_adminBM = New lib_adminBM()
        lib_adminBM.Show()
        Me.Close()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim lib_adminEBM = New lib_adminEBM()
        lib_adminEBM.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Dim lib_adminBM = New lib_adminBM()
        lib_adminBM.Show()
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim lib_adminDash = New lib_adminDash()
        lib_adminDash.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim lib_adminSearch = New lib_adminSearch()
        lib_adminSearch.Show()
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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim HomePageDashboard = New HomePageDashboard()
        HomePageDashboard.Show()
        Me.Close()
    End Sub
    
    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub lib_admin_eBooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Label2.Text = u_name
        'Panel9.Visible = False
        LoadAlleBooks()
        PopulateTable()
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
                    .Link = reader("link").ToString()
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
                    .Link = reader("link").ToString()
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
                    .Link = reader("link").ToString()
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
                .Link = reader("link").ToString()
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
            linkLabel.TextAlign = ContentAlignment.BottomCenter ' Center the label both horizontally and vertically
            linkLabel.Padding = New Padding(9, 3, 0, -5) ' Adjust the padding to center the text vertically
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
            'eBooksTablePanel.Controls.Add(alleBooks(rowIndex).RadioButton, 6, rowIndex)
            'entry.RadioButton.TextAlign = ContentAlignment.MiddleCenter ' center the radio button
            'entry.RadioButton.Anchor = AnchorStyles.None ' set anchor to none
            'entry.RadioButton.Size = New Size(16, 16) ' set the size of the radio button

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
                    .Link = reader("link").ToString()
                })
        End While

        reader.Close()
        Con.Close()
    End Sub

    Private Sub queryBook_Click(sender As Object, e As EventArgs) Handles queryBook.Click
        queryBook.Text = ""
    End Sub

    Private Sub srchSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles srchSelect.SelectedIndexChanged
        selectedSearchMode = srchSelect.SelectedItem.ToString()
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub
End Class