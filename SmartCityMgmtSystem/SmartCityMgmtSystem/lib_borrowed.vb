Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports SmartCityMgmtSystem.lib_dash
Public Class lib_borrowed
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"

    Private Structure Entry
        Public BookID As Integer
        Public Author As String
        Public Title As String
        Public DueDate As String
        Public RadioButton As RadioButton ' Added RadioButton field
    End Structure

    Dim borrowedBooks As New List(Of Entry)
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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim lib_request = New lib_request() With {
            .uid = uid,
            .u_name = u_name
        }
        lib_request.Show()
        Me.Close()
    End Sub

    Private Sub lib_borrowed_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label9.Text = uid
        Panel6.Visible = False
        LoadBorrowedBooks()
        PopulateTable()
    End Sub

    Private Sub LoadBorrowedBooks()
        borrowedBooks.Clear()
        Dim query As String = "SELECT bb.book_ID, b.author, b.title, bb.dueDate FROM lib_borrowed_books bb INNER JOIN lib_books  b ON bb.book_ID = b.book_ID WHERE bb.issuedTo = '" & uid & "'"

        Dim Con = Globals.GetDBConnection()

        Try
            Con.Open()
            Dim cmd As New MySqlCommand(query, Con)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                borrowedBooks.Add(New Entry With {
                .BookID = reader("book_ID").ToString(),
                .Author = reader("author").ToString(),
                .Title = reader("title").ToString(),
                .DueDate = reader("dueDate").ToString(),
                .RadioButton = New RadioButton()
            })
            End While

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close() ' Close the connection in the finally block to ensure it's closed even if an exception occurs
            End If
        End Try
    End Sub

    Private Sub PopulateTable()
        borrowedBooksTablePanel.Controls.Clear()
        borrowedBooksTablePanel.RowStyles.Clear()
        For rowIndex As Integer = 0 To borrowedBooks.Count - 1
            borrowedBooksTablePanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 50))
        Next

        If borrowedBooks.Count > 0 Then
            For rowIndex As Integer = 0 To borrowedBooks.Count - 1
                Dim entry As Entry = borrowedBooks(rowIndex)


                ' Add book details
                Dim bookIdLabel As New Label()
                bookIdLabel.Text = entry.BookID.ToString()
                'bookIdLabel.Font = New Font(bookIdLabel.Font.FontFamily, 13) ' Set font size
                bookIdLabel.ForeColor = Color.Black ' Set font color to black
                borrowedBooksTablePanel.Controls.Add(bookIdLabel, 0, rowIndex)
                bookIdLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
                bookIdLabel.Anchor = AnchorStyles.None ' Set Anchor to None

                Dim authorLabel As New Label()
                authorLabel.Text = entry.Author
                authorLabel.Width = 200 ' Adjust width as needed
                'authorLabel.Font = New Font(authorLabel.Font.FontFamily, 13) ' Set font size
                authorLabel.ForeColor = Color.Black ' Set font color to black
                borrowedBooksTablePanel.Controls.Add(authorLabel, 1, rowIndex)
                authorLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
                authorLabel.Anchor = AnchorStyles.None ' Set Anchor to None

                Dim titleLabel As New Label()
                titleLabel.Text = entry.Title
                titleLabel.AutoSize = True
                'titleLabel.Font = New Font(titleLabel.Font.FontFamily, 13) ' Set font size
                titleLabel.ForeColor = Color.Black ' Set font color to black
                borrowedBooksTablePanel.Controls.Add(titleLabel, 2, rowIndex)
                titleLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
                titleLabel.Anchor = AnchorStyles.None ' Set Anchor to None

                Dim dueDateLabel As New Label()
                dueDateLabel.Text = entry.DueDate
                dueDateLabel.Width = 200
                'dueDateLabel.Font = New Font(dueDateLabel.Font.FontFamily, 13) ' Set font size
                dueDateLabel.ForeColor = Color.Black ' Set font color to black
                borrowedBooksTablePanel.Controls.Add(dueDateLabel, 3, rowIndex)
                dueDateLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
                dueDateLabel.Anchor = AnchorStyles.None ' Set Anchor to None

                ' Add radio button for options
                borrowedBooksTablePanel.Controls.Add(entry.RadioButton, 4, rowIndex)
                entry.RadioButton.TextAlign = ContentAlignment.MiddleCenter ' Center the radio button
                entry.RadioButton.Anchor = AnchorStyles.None ' Set Anchor to None
                entry.RadioButton.Size = New Size(16, 16) ' Set the size of the radio button

            Next

            Dim adjustLabel2 As New Label()
            adjustLabel2.Text = ""
            borrowedBooksTablePanel.Controls.Add(adjustLabel2, 1, borrowedBooks.Count)
        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim HomePageDashboard = New HomePageDashboard() With {
            .uid = uid
        }
        HomePageDashboard.Show()
        Me.Close()
    End Sub

    Dim rateID As Integer
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim count As Integer = 0
        For Each entry As Entry In borrowedBooks
            If entry.RadioButton.Checked Then
                count = count + 1
                rateID = entry.BookID

            End If
        Next
        If count = 0 Then
            MessageBox.Show("No book selected.")
            rateID = -1
        Else
            Panel6.Visible = True
        End If

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim ratingInput As Integer
        If Integer.TryParse(TextBox2.Text, ratingInput) AndAlso ratingInput >= 0 AndAlso ratingInput <= 5 Then
            Dim queryCheck As String = "SELECT COUNT(*) FROM lib_book_ratings WHERE book_ID = @rateID and book_type = 0 and uid = @uid"
            Dim queryUpdate As String = "UPDATE lib_book_ratings SET rating = @ratingInput WHERE book_ID = @rateID and book_type = 0 and uid = @uid "
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
                    cmdInsert.Parameters.AddWithValue("@book_type", 0)
                    cmdInsert.ExecuteNonQuery()
                End If

                ' Calculate and update the average rating in the lib_books table
                Dim queryAvgRating As String = "UPDATE lib_books SET rating = (SELECT AVG(rating) FROM lib_book_ratings WHERE book_ID = @rateID AND book_type = 0) WHERE book_ID = @rateID"
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
            Panel6.Visible = False
            TextBox2.Text = "Enter your Rating"
        Else
            ' If TextBox2.Text is not a valid integer or not within the range, display an error message
            MessageBox.Show("Please enter a valid integer between 0 and 5")
            Return ' Exit the method if input is invalid
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

    Private Sub RenewButton_Click(sender As Object, e As EventArgs) Handles RenewButton.Click
        Dim count As Integer = 0
        Dim renewID As Integer
        For Each entry As Entry In borrowedBooks
            If entry.RadioButton.Checked Then
                count = count + 1
                renewID = entry.BookID

                Dim currentDate As DateTime = DateTime.Now.Date
                Dim futureDate As DateTime = DateAdd("d", 14, DateTime.Parse(entry.DueDate))

                If entry.DueDate < currentDate Then
                    MessageBox.Show("Book can't be renewed as it is past it's due date. Please pay fine and re-issue.")
                    Return
                Else

                    Dim updateQueryInBooks = "UPDATE lib_books SET dueDate = '" & futureDate.Date.ToString("yyyy-MM-dd HH:mm:ss") & "' WHERE book_Id = '" & renewID & "'"
                    Dim updateQueryInBorrowed_Books = "UPDATE lib_borrowed_books SET dueDate = '" & futureDate.Date.ToString("yyyy-MM-dd HH:mm:ss") & "' WHERE book_ID = '" & renewID & "'"

                    Dim Con = Globals.GetDBConnection()
                    Try
                        Con.Open()

                        Dim cmdUpdate As New MySqlCommand(updateQueryInBooks, Con)
                        cmdUpdate.ExecuteNonQuery()

                        Dim cmdUpdatebb As New MySqlCommand(updateQueryInBorrowed_Books, Con)
                        cmdUpdatebb.ExecuteNonQuery()

                        MessageBox.Show("Renewed Successfully.")

                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message)
                    Finally
                        If Con.State = ConnectionState.Open Then
                            Con.Close() ' Close the connection in the finally block to ensure it's closed even if an exception occurs
                        End If
                    End Try

                End If

            End If
        Next
        If count = 0 Then
            MessageBox.Show("No book selected.")
            Return
        Else
            Dim lib_borrowed = New lib_borrowed() With {
            .uid = uid,
            .u_name = u_name
        }
            lib_borrowed.Show()
            Me.Close()
        End If

    End Sub

    Private Sub heading_Click(sender As Object, e As EventArgs) Handles heading.Click

    End Sub

    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs) Handles Panel7.Paint

    End Sub
End Class