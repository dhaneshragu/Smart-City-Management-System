Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class lib_adminMT

    'Public Property uid As Integer = -1
    'Public Property u_name As String = "User/Admin"

    Dim Con = Globals.GetDBConnection()
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim lib_adminSearch = New lib_adminSearch()
        lib_adminSearch.Show()
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

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim lib_adminReq = New lib_adminReq()
        lib_adminReq.Show()
        Me.Close()
    End Sub

    'Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
    '    Dim lib_adminEBM = New lib_adminEBM()
    '    lib_adminEBM.Show()
    '    Me.Close()
    'End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim lib_adminEBM = New lib_adminEBM()
        lib_adminSearch.Show()
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim HomePageDashboard = New HomePageDashboard()
        HomePageDashboard.Show()
        Me.Close()
    End Sub
    
    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub

    Private Sub lib_adminMT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Label2.Text = u_name
        Panel9.Visible = False
        'Panel6.Visible = False
        'LoadBorrowedBooks()
        'PopulateTable()
    End Sub

    Private Sub Issue_button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Issue_button.Click


        If StudentID_tb.Text = "" Or BookID_tb2.Text = "" Then
            MsgBox("Missing Information", 0 + 0, "Error")
        Else
            '
            '
            ' Issue the Book....to particular user....
            '
            '

            Dim isStudent As Boolean = False
            'Dim isFaculty As Boolean = False
            'Dim isReserved As Boolean = False



            ' Check whether user ID is valid
            Dim userQuery = "SELECT * FROM users where user_id='" & StudentID_tb.Text & "'"
            'Using connection As New MySqlConnection(connectionString)
            'Con.Open()
            Using command As New MySqlCommand(userQuery, Con)
                Try
                    'connection.Open()
                    Con.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    Dim count As Integer
                    count = 0
                    While reader.Read
                        count = count + 1
                    End While
                    If count > 0 Then
                        isStudent = True
                        'MessageBox.Show("student with given id does not exist.")
                        'Return
                    End If
                    Con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
                'Con.Close()
            End Using
            'End Using

            If isStudent = False Then
                MessageBox.Show("User with given id does not exist.")
                Return
            End If

            ' Check whether book ID is valid
            userQuery = "SELECT * FROM lib_books where book_ID='" & BookID_tb2.Text & "'"
            'Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(userQuery, Con)
                Try
                    Con.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    Dim count As Integer
                    count = 0
                    While reader.Read
                        count = count + 1
                    End While
                    If count = 0 Then
                        MessageBox.Show("Book with given ID does not exist.")
                        Con.Close()
                        Return
                    End If
                    Con.Close()

                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
                'Con.Close()
            End Using
            'End Using
            'If isStudent = True Then
            '    userQuery = "SELECT * FROM books where (BINARY ID='" & BookID_tb2.Text & "' AND NOT isReserved)"
            '    Using connection As New MySqlConnection(connectionString)
            '        Using command As New MySqlCommand(userQuery, connection)
            '            Try
            '                connection.Open()
            '                Dim reader As MySqlDataReader = command.ExecuteReader()
            '                Dim count As Integer
            '                count = 0
            '                While reader.Read
            '                    count = count + 1
            '                End While
            '                If count = 0 Then
            '                    MessageBox.Show("Book with given ID is reserved for faculty.")
            '                    Return
            '                End If
            '            Catch ex As Exception
            '                MessageBox.Show("Error: " & ex.Message)
            '            End Try
            '        End Using
            '    End Using
            'End If

            ' Check whether book is already issued
            userQuery = "SELECT * FROM lib_books where (book_ID='" & BookID_tb2.Text & "' AND NOT issued)"
            'Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(userQuery, Con)
                Try
                    Con.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    Dim count As Integer
                    count = 0
                    While reader.Read
                        count = count + 1
                    End While
                    If count = 0 Then
                        MessageBox.Show("Book with given ID is already issued.")
                        Con.Close()
                        Return
                    End If
                    Con.Close()

                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
                'Con.Close()
            End Using
            'End Using

            ' Issuing the book
            Dim quotaExceed As Integer = 0

            ' Check if issuing the book crosses issue limit
            userQuery = "SELECT * FROM lib_borrowed_books WHERE issuedTo = '" & StudentID_tb.Text & "'"
            'Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(userQuery, Con)
                Try
                    Con.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    Dim count As Integer = 0
                    While reader.Read()
                        count = count + 1
                    End While
                    If count >= 7 Then
                        quotaExceed = 1
                    End If
                    Con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
                'Con.Close()
            End Using
            'End Using
            If quotaExceed = 1 Then
                MessageBox.Show("Your quota to issue books is exhausted, return some books to issue new books.")
                Return
            Else
                'Dim currentDate As DateTime = DateTime.Now.Date
                Dim currentDate As DateTime = DateTimePicker3.Value
                Dim futureDate As DateTime = DateAdd("d", 7, currentDate)
                Dim updateQueryInBooks = "UPDATE lib_books SET issued = '1', dueDate = '" & futureDate.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', issuedTo = '" & StudentID_tb.Text & "' WHERE book_ID = '" & BookID_tb2.Text & "'"
                Dim updateQueryInBorrowed_Books = "INSERT INTO lib_borrowed_books (book_ID,  issueDate, dueDate, issuedTo) VALUES ('" & BookID_tb2.Text & "','" & currentDate.Date.ToString("yyyy-MM-dd HH:mm:ss") & "','" & futureDate.Date.ToString("yyyy-MM-dd HH:mm:ss") & "', '" & StudentID_tb.Text & "')"
                'Using newConnection As New MySqlConnection(connectionString)
                Using newCommand As New MySqlCommand(updateQueryInBooks, Con)
                    Try
                        Con.Open()
                        newCommand.ExecuteNonQuery()
                        MessageBox.Show("Your book with BookID: " + BookID_tb2.Text.ToString + " has been issued to " + StudentID_tb.Text + "till: " + futureDate.Date.ToString)
                        Con.Close()
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message)
                    End Try
                    'Con.Close()
                End Using
                'End Using
                'Using newConnection As New MySqlConnection(connectionString)
                Using newCommand As New MySqlCommand(updateQueryInBorrowed_Books, Con)
                    Try
                        Con.Open()
                        newCommand.ExecuteNonQuery()
                        Con.Close()
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message)
                    End Try
                    'Con.Close()
                End Using
                'End Using
                'Dim addTransactionToAdmin = "INSERT INTO transactions (transaction) VALUES (' " & StudentID_tb.Text & " has issued the book with book ID " & BookID_tb2.Text & ", till " & futureDate.Date.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                'Using newNewConnection As New MySqlConnection(connectionString)
                '    Using newNewCommand As New MySqlCommand(addTransactionToAdmin, newNewConnection)
                '        Try
                '            newNewConnection.Open()
                '            newNewCommand.ExecuteNonQuery()
                '        Catch ex As Exception
                '            MessageBox.Show("Error: " & ex.Message)
                '        End Try
                '    End Using
                'End Using
                '' Populate the table with the borrowedBooks
                'LoadAllBooks()
                'PopulateTable()
                MsgBox("Book Issued Successfully")

            End If

            ' After issuing the book, clear the inputs and show the msg box that it is issued....
            BookID_tb2.Text = ""
            addBalance_tb.Text = ""
            StudentID_tb.Text = ""
            Fine_tb.Text = ""

        End If
    End Sub


    Private Sub Button888_Click(sender As Object, e As EventArgs) Handles Button888.Click
        If StudentID_tb.Text = "" Or BookID_tb2.Text = "" Then
            MsgBox("Missing Information", 0 + 0, "Error")
        Else
            '
            '
            ' Implement the return function.....
            '
            '

            Dim isStudent As Boolean = False
            'Dim isFaculty As Boolean = False

            ' Check whether user ID is valid
            Dim userQuery = "SELECT * FROM users where user_id='" & StudentID_tb.Text & "'"
            'Using connection As New MySqlConnection(connectionString)
            'Con.Open()
            Using command As New MySqlCommand(userQuery, Con)
                Try
                    'connection.Open()
                    Con.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    Dim count As Integer
                    count = 0
                    While reader.Read
                        count = count + 1
                    End While
                    If count > 0 Then
                        isStudent = True
                        'MessageBox.Show("student with given id does not exist.")
                        'Return
                    End If
                    Con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
                'Con.Close()
            End Using
            'userQuery = "SELECT * FROM faculty where BINARY ID='" & StudentID_tb.Text & "'"
            'Using connection As New MySqlConnection(connectionString)
            '    Using command As New MySqlCommand(userQuery, connection)
            '        Try
            '            connection.Open()
            '            Dim reader As MySqlDataReader = command.ExecuteReader()
            '            Dim count As Integer
            '            count = 0
            '            While reader.Read
            '                count = count + 1
            '            End While
            '            If count > 0 Then
            '                isFaculty = True
            '                'MessageBox.Show("student with given id does not exist.")
            '                'Return
            '            End If
            '        Catch ex As Exception
            '            MessageBox.Show("Error: " & ex.Message)
            '        End Try
            '    End Using
            'End Using
            If isStudent = False Then
                MessageBox.Show("User with given id does not exist.")
                Return
            End If

            'Check whether the book is issued to the user
            userQuery = "SELECT * FROM lib_borrowed_books WHERE (book_ID = '" & BookID_tb2.Text & "' AND issuedTo = '" & StudentID_tb.Text & "')"
            'Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(userQuery, Con)
                Try
                    Con.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    Dim count As Integer
                    count = 0
                    While reader.Read
                        count = count + 1
                    End While
                    If count = 0 Then
                        MessageBox.Show("This book is not issued to the user.")
                        Con.Close()
                        Return
                    End If
                    Con.Close()

                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
                'Con.Close()
            End Using
            'End Using

            Dim query = "SELECT * FROM lib_borrowed_books WHERE (book_ID = '" & BookID_tb2.Text & "')"
            'Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, Con)
                Try
                    Con.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    Dim currentDate As DateTime = DateTimePicker4.Value
                    Dim NewCon = Globals.GetDBConnection()
                    While reader.Read()
                        Dim updateQueryInBooks = "UPDATE lib_books SET issued = 0, issuedTo = NULL, dueDate = '0000-00-00 00:00:00' WHERE book_ID = '" & BookID_tb2.Text & "'"
                        Dim updateQueryInBorrowed_Books = "DELETE FROM lib_borrowed_books WHERE book_ID = '" & BookID_tb2.Text & "'"
                        'Using newConnection As New MySqlConnection(connectionString)

                        Using newCommand As New MySqlCommand(updateQueryInBooks, NewCon)
                            Try
                                NewCon.Open()
                                newCommand.ExecuteNonQuery()
                                NewCon.Close()
                                If reader("dueDate") < currentDate Then
                                    Dim fine As Integer = DateDiff(DateInterval.Day, reader("dueDate"), currentDate)
                                    Dim fineUpdateQuery As String
                                    'If isStudent Then
                                    '    fineUpdateQuery = "SELECT * FROM students WHERE ID = '" & StudentID_tb.Text & "'"
                                    'Else
                                    '    fineUpdateQuery = "SELECT * FROM faculty WHERE ID = '" & StudentID_tb.Text & "'"
                                    'End If
                                    fineUpdateQuery = "SELECT * FROM lib_fine WHERE uid = '" & StudentID_tb.Text & "'"
                                    'Using newNewConnection As New MySqlConnection(connectionString)
                                    'NewCon.Open()
                                    Using newNewCommand As New MySqlCommand(fineUpdateQuery, NewCon)
                                        Try
                                            'Con.Open()
                                            NewCon.Open()
                                            Dim newNewReader As MySqlDataReader = newNewCommand.ExecuteReader
                                            While newNewReader.Read()
                                                Dim value As Integer
                                                Integer.TryParse(newNewReader("fine").ToString, value)
                                                fine = fine + value
                                            End While
                                            NewCon.Close()
                                        Catch ex As Exception
                                            MessageBox.Show("Error: " & ex.Message)
                                        End Try
                                        'Con.CLose()
                                    End Using
                                    'End Using
                                    'If isStudent Then
                                    '    fineUpdateQuery = "UPDATE students SET Fine = '" & fine & "' WHERE ID = '" & StudentID_tb.Text & "'"
                                    'Else
                                    '    fineUpdateQuery = "UPDATE faculty SET Fine = '" & fine & "' WHERE ID = '" & StudentID_tb.Text & "'"
                                    'End If


                                    ' CHECK   ERROR     ERROR       ERROR


                                    fineUpdateQuery = "UPDATE lib_fine SET fine = '" & fine & "' WHERE uid = '" & StudentID_tb.Text & "'"
                                    'Using newNewConnection As New MySqlConnection(Con)
                                    Using newNewCommand As New MySqlCommand(fineUpdateQuery, NewCon)
                                        Try
                                            'Con.Open()
                                            NewCon.Open()
                                            newNewCommand.ExecuteNonQuery()
                                            'MessageBox.Show("Fine updated to " + fine.ToString)
                                            'Con.Close()
                                            NewCon.Close()
                                        Catch ex As Exception
                                            MessageBox.Show("Error: " & ex.Message)
                                        End Try
                                    End Using
                                    'End Using
                                    'Dim addTransactionToAdmin = "INSERT INTO transactions (transaction) VALUES (' " & StudentID_tb.Text & " returned the book with book ID " & addBalance_tb.Text & ", and incurred a fine of " & fine.ToString & "')"
                                    'Using newNewConnection As New MySqlConnection(connectionString)
                                    '    Using newNewCommand As New MySqlCommand(addTransactionToAdmin, newNewConnection)
                                    '        Try
                                    '            newNewConnection.Open()
                                    '            newNewCommand.ExecuteNonQuery()
                                    '        Catch ex As Exception
                                    '            MessageBox.Show("Error: " & ex.Message)
                                    '        End Try
                                    '    End Using
                                    'End Using
                                    MessageBox.Show("Since you are returning the book late, some fine has been added to your account. New fine is " + fine.ToString)
                                Else
                                    'Dim addTransactionToAdmin = "INSERT INTO transactions (transaction) VALUES (' " & StudentID_tb.Text & " returned the book with book ID " & BookID_tb2.Text & "')"
                                    'Using newNewConnection As New MySqlConnection(connectionString)
                                    '    Using newNewCommand As New MySqlCommand(addTransactionToAdmin, newNewConnection)
                                    '        Try
                                    '            newNewConnection.Open()
                                    '            newNewCommand.ExecuteNonQuery()
                                    '        Catch ex As Exception
                                    '            MessageBox.Show("Error: " & ex.Message)
                                    '        End Try
                                    '    End Using
                                    'End Using
                                End If
                                'Con.Close()
                                MessageBox.Show("Your book with BookID: " + BookID_tb2.Text.ToString + " has been returned to the library.")
                            Catch ex As Exception
                                MessageBox.Show("Error: " & ex.Message)
                            End Try
                            'Con.Close()
                        End Using
                        'End Using
                        'Using newConnection As New MySqlConnection(connectionString)
                        Using newCommand As New MySqlCommand(updateQueryInBorrowed_Books, NewCon)
                            Try
                                NewCon.Open()
                                newCommand.ExecuteNonQuery()
                                NewCon.Close()
                            Catch ex As Exception
                                MessageBox.Show("Error: " & ex.Message)
                            End Try
                            'Con.Close()
                        End Using
                        'End Using
                    End While
                    Con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
            'End Using

            ' Populate the table with all Books
            'LoadAllBooks()
            'PopulateTable()

            ' After returning the book, clear the inputs and show the msg box that it is Returned....
            MsgBox("Book Returned Successfully")
            'LoadAllBooks()
            BookID_tb2.Text = ""
            addBalance_tb.Text = ""
            StudentID_tb.Text = ""
            Fine_tb.Text = ""
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If StudentID_tb.Text = "" Or BookID_tb2.Text = "" Then
            MsgBox("Missing Information", 0 + 0, "Error")
        Else
            '
            '
            ' Implement the renew function.....
            'Fine_tb.Text = "20" ' just for checking the working of pay button.....
            '

            'Check whether the book is issued to the user
            Dim userQuery = "SELECT * FROM lib_borrowed_books WHERE (book_ID = '" & BookID_tb2.Text & "' AND issuedTo = '" & StudentID_tb.Text & "')"
            'Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(userQuery, Con)
                Try
                    Con.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    Dim count As Integer
                    count = 0
                    While reader.Read
                        count = count + 1
                    End While
                    If count = 0 Then
                        MessageBox.Show("This book is not issued to the user.")
                        Con.Close()
                        Return
                    End If
                    Con.Close()

                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
                'Con.Close()
            End Using
            'End Using

            Dim query = "SELECT * FROM lib_borrowed_books WHERE (book_ID = '" & BookID_tb2.Text & "')"
            'Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, Con)
                Try
                    Con.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    Dim currentDate As DateTime = DateTimePicker5.Value
                    Dim futureDate As DateTime = DateAdd("d", 14, currentDate)
                    While reader.Read()
                        If reader("dueDate") < currentDate Then

                            MessageBox.Show("Book can't be renewed as it is past it's due date. Please pay fine and re-issue.")
                            reader.Close()
                            Con.Close()
                            Return
                        Else
                            Dim NewCon = Globals.GetDBConnection()
                            Dim updateQueryInBooks = "UPDATE lib_books SET dueDate = '" & futureDate.Date.ToString("yyyy-MM-dd HH:mm:ss") & "' WHERE book_ID = '" & BookID_tb2.Text & "'"
                            Dim updateQueryInBorrowed_Books = "UPDATE lib_borrowed_books SET dueDate = '" & futureDate.Date.ToString("yyyy-MM-dd HH:mm:ss") & "' WHERE book_ID = '" & BookID_tb2.Text & "'"

                            'Using newConnection As New MySqlConnection(connectionString)
                            Using newCommand As New MySqlCommand(updateQueryInBooks, NewCon)
                                Try
                                    'Con.Open()
                                    NewCon.Open()
                                    newCommand.ExecuteNonQuery()
                                    NewCon.Close()
                                Catch ex As Exception
                                    MessageBox.Show("Error: " & ex.Message)
                                End Try
                                'Con.Close()
                            End Using
                            'End Using
                            'Using newConnection As New MySqlConnection(connectionString)
                            Using newCommand As New MySqlCommand(updateQueryInBorrowed_Books, NewCon)
                                Try
                                    'Con.Open()
                                    NewCon.Open()
                                    newCommand.ExecuteNonQuery()
                                    NewCon.Close()
                                Catch ex As Exception
                                    MessageBox.Show("Error: " & ex.Message)
                                End Try
                                'Con.Close()
                            End Using
                            'End Using
                            'Dim addTransactionToAdmin = "INSERT INTO transactions (transaction) VALUES (' " & StudentID_tb.Text & " renewed the book with book ID " & BookID_tb2.Text & " till " & futureDate.Date.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                            'Using newConnection As New MySqlConnection(connectionString)
                            '    Using newCommand As New MySqlCommand(addTransactionToAdmin, newConnection)
                            '        Try
                            '            newConnection.Open()
                            '            newCommand.ExecuteNonQuery()
                            '        Catch ex As Exception
                            '            MessageBox.Show("Error: " & ex.Message)
                            '        End Try
                            '    End Using
                            'End Using
                            MessageBox.Show("You have successfully renewed the book with book ID " + BookID_tb2.Text.ToString + " till " & futureDate.Date.ToString("yyyy-MM-dd HH:mm:ss"))

                        End If
                    End While
                    reader.Close()
                    Con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
                'Con.Close()
            End Using
            'End Using
            ' Populate the table with the borrowedBooks
            'PopulateTable()


            ' After renewing the book, clear the inputs and show the msg box that it is Renewed....
            MsgBox("Book Renewed Successfully")
            BookID_tb2.Text = ""
            addBalance_tb.Text = ""
            StudentID_tb.Text = ""
            Fine_tb.Text = ""

        End If
    End Sub

    Private Sub loadFineButton_Click(sender As Object, e As EventArgs) Handles loadFineButton.Click
        If StudentID_tb.Text = "" Then
            MsgBox("Missing Information", 0 + 0, "Error")
        Else
            Dim isStudent As Boolean = False
            'Dim isFaculty As Boolean = False

            ' Check whether user ID is valid
            Dim userQuery = "SELECT * FROM lib_fine where uid='" & StudentID_tb.Text & "'"
            'Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(userQuery, Con)
                Try
                    Con.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    Dim count As Integer
                    count = 0
                    While reader.Read
                        count = count + 1
                    End While
                    If count > 0 Then
                        isStudent = True
                        'MessageBox.Show("student with given id does not exist.")
                        'Return
                    End If
                    Con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
            'End Using
            'userQuery = "SELECT * FROM faculty where BINARY ID='" & StudentID_tb.Text & "'"
            'Using connection As New MySqlConnection(connectionString)
            '    Using command As New MySqlCommand(userQuery, connection)
            '        Try
            '            connection.Open()
            '            Dim reader As MySqlDataReader = command.ExecuteReader()
            '            Dim count As Integer
            '            count = 0
            '            While reader.Read
            '                count = count + 1
            '            End While
            '            If count > 0 Then
            '                isFaculty = True
            '                'MessageBox.Show("student with given id does not exist.")
            '                'Return
            '            End If
            '        Catch ex As Exception
            '            MessageBox.Show("Error: " & ex.Message)
            '        End Try
            '    End Using
            'End Using
            If isStudent = False Then
                MessageBox.Show("User with given id does not exist.")
                Return
            End If

            Dim searchQuery As String
            'If isStudent = True Then
            '    searchQuery = "SELECT * FROM students WHERE ID = '" & StudentID_tb.Text & "'"
            'Else
            '    searchQuery = "SELECT * FROM faculty WHERE ID = '" & StudentID_tb.Text & "'"
            'End If
            searchQuery = "SELECT * FROM lib_fine WHERE uid = '" & StudentID_tb.Text & "'"
            'Using newConnection As New MySqlConnection(connectionString)
            Using newCommand As New MySqlCommand(searchQuery, Con)
                Try
                    Con.Open()
                    Dim newReader As MySqlDataReader = newCommand.ExecuteReader
                    Dim fine As Integer
                    While newReader.Read()
                        fine = newReader("fine")
                    End While

                    Fine_tb.Text = fine
                    Con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
            'End Using
        End If
    End Sub

    Private Sub addBalanceButton_Click(sender As Object, e As EventArgs) Handles addBalanceButton.Click
        If StudentID_tb.Text = "" Or addBalance_tb.Text = "" Then
            MsgBox("Missing Information", 0 + 0, "Error")
        Else
            Dim isStudent As Boolean = False
            'Dim isFaculty As Boolean = False

            ' Check whether user ID is valid
            Dim userQuery = "SELECT * FROM lib_fine where uid='" & StudentID_tb.Text & "'"
            'Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(userQuery, Con)
                Try
                    Con.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    Dim count As Integer
                    count = 0
                    While reader.Read
                        count = count + 1
                    End While
                    If count > 0 Then
                        isStudent = True
                        'MessageBox.Show("student with given id does not exist.")
                        'Return
                    End If
                    Con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
            'End Using
            'userQuery = "SELECT * FROM faculty where BINARY ID='" & StudentID_tb.Text & "'"
            'Using connection As New MySqlConnection(connectionString)
            '    Using command As New MySqlCommand(userQuery, connection)
            '        Try
            '            connection.Open()
            '            Dim reader As MySqlDataReader = command.ExecuteReader()
            '            Dim count As Integer
            '            count = 0
            '            While reader.Read
            '                count = count + 1
            '            End While
            '            If count > 0 Then
            '                isFaculty = True
            '                'MessageBox.Show("student with given id does not exist.")
            '                'Return
            '            End If
            '        Catch ex As Exception
            '            MessageBox.Show("Error: " & ex.Message)
            '        End Try
            '    End Using
            'End Using
            If isStudent = False Then
                MessageBox.Show("User with given id does not exist.")
                Return
            End If

            Dim searchQuery As String
            Dim successful As Boolean
            successful = False
            'If isStudent = True Then
            '    searchQuery = "SELECT * FROM students WHERE ID = '" & StudentID_tb.Text & "'"
            'Else
            '    searchQuery = "SELECT * FROM faculty WHERE ID = '" & StudentID_tb.Text & "'"
            'End If
            searchQuery = "SELECT * FROM lib_fine WHERE uid = '" & StudentID_tb.Text & "'"
            'Using newConnection As New MySqlConnection(connectionString)
            Using newCommand As New MySqlCommand(searchQuery, Con)
                Try
                    Con.Open()
                    Dim newReader As MySqlDataReader = newCommand.ExecuteReader
                    Dim balance As Integer
                    While newReader.Read()
                        balance = newReader("balance")
                    End While

                    newReader.Close()

                    If Convert.ToInt32(addBalance_tb.Text) <= 0 Then
                        MessageBox.Show("Add positive amount.")
                        Con.Close()
                        Return
                    ElseIf balance + Convert.ToInt32(addBalance_tb.Text) > 1000 Then
                        MessageBox.Show("You can't have more than Rs. 1000 in your account.")
                        Con.Close()
                        Return
                    Else
                        balance = balance + Convert.ToInt32(addBalance_tb.Text)
                        successful = True
                    End If

                    Dim balanceUpdateQuery As String
                    'If isStudent = True Then
                    '    balanceUpdateQuery = "UPDATE students SET Balance = '" & balance & "' WHERE ID = '" & StudentID_tb.Text & "'"
                    'Else
                    '    balanceUpdateQuery = "UPDATE faculty SET Balance = '" & balance & "' WHERE ID = '" & StudentID_tb.Text & "'"
                    'End If

                    balanceUpdateQuery = "UPDATE lib_fine SET balance = '" & balance & "' WHERE uid = '" & StudentID_tb.Text & "'"

                    'Dim addTransactionToAdmin = "INSERT INTO transactions (transaction) VALUES (' " & StudentID_tb.Text & " has updated is balance to Rs. " & balance.ToString & "')"

                    Using balanceUpdateCommand As New MySqlCommand(balanceUpdateQuery, Con)
                        balanceUpdateCommand.ExecuteNonQuery()
                    End Using

                    'Using addTransactionToAdminCommand As New MySqlCommand(addTransactionToAdmin, newConnection)
                    '    addTransactionToAdminCommand.ExecuteNonQuery()
                    'End Using
                    Con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try

            End Using
            'End Using
            If successful Then
                MessageBox.Show(addBalance_tb.Text + " successfully added to balance!")
            End If
        End If
        addBalance_tb.Text = ""
    End Sub

    Private Sub Pay_button_Click(sender As Object, e As EventArgs) Handles Pay_button.Click


        If StudentID_tb.Text = "" Then
            MsgBox("Missing Information", 0 + 0, "Error")
        Else
            Dim isStudent As Boolean = False
            'Dim isFaculty As Boolean = False

            ' Check whether user ID is valid
            Dim userQuery = "SELECT * FROM lib_fine where uid='" & StudentID_tb.Text & "'"
            'Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(userQuery, Con)
                Try
                    Con.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()
                    Dim count As Integer
                    count = 0
                    While reader.Read
                        count = count + 1
                    End While
                    If count > 0 Then
                        isStudent = True
                        'MessageBox.Show("student with given id does not exist.")
                        'Return
                    End If
                    Con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using

            If isStudent = False Then
                MessageBox.Show("User with given id does not exist.")
                Return
            End If

            Panel2.Visible = False
            Panel3.Visible = False
            Panel7.Visible = False
            Panel9.Visible = True

        End If





    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim userFine As String = TextBox2.Text
        Dim integerValue As Integer

        Dim successful As Boolean
        successful = False

        If Integer.TryParse(userFine, integerValue) AndAlso integerValue > 0 Then
            ' Text entered is a valid positive integer
            ' Store it in a variable or perform any other action
            MessageBox.Show("The entered value is: " & integerValue.ToString())
        Else
            ' Text entered is not a valid positive integer
            MessageBox.Show("Please enter a valid positive integer.")
        End If

        '' Create and show the input prompt form
        'Dim inputPromptForm As New lib_fineForm()
        'If inputPromptForm.ShowDialog() = DialogResult.OK Then
        '    ' Retrieve the input value
        '    If Integer.TryParse(inputPromptForm.InputValue, userFine) Then
        '        ' Input value is valid
        '    Else
        '        ' Input value is not a valid integer
        '        MessageBox.Show("Invalid input. Please enter a valid integer.")
        '    End If
        'End If

        Dim searchQuery As String
        'If isStudent = True Then
        '    searchQuery = "SELECT * FROM students WHERE ID = '" & StudentID_tb.Text & "'"
        'Else
        '    searchQuery = "SELECT * FROM faculty WHERE ID = '" & StudentID_tb.Text & "'"
        'End If
        searchQuery = "SELECT * FROM lib_fine WHERE uid = '" & StudentID_tb.Text & "'"
        'Using newConnection As New MySqlConnection(connectionString)
        Using newCommand As New MySqlCommand(searchQuery, Con)
            Try
                Con.Open()
                Dim newReader As MySqlDataReader = newCommand.ExecuteReader
                Dim fine As Integer
                Dim balance As Integer
                While newReader.Read()
                    fine = newReader("fine")
                    balance = newReader("balance")
                End While

                ' Close the DataReader before executing UPDATE queries
                newReader.Close()
                If userFine <= 0 Then
                    MessageBox.Show("Enter a positive integer!")
                ElseIf userFine > fine Then
                    MessageBox.Show("Don't pay more than the fine!.")
                ElseIf userFine > balance Then
                    MessageBox.Show("Insufficient Balance!.")
                Else
                    fine = fine - userFine
                    balance = balance - userFine
                    'fineCollected = fineCollected + iFine
                    successful = True
                End If

                Dim fineUpdateQuery As String
                Dim balanceUpdateQuery As String
                'If isStudent = True Then
                '    fineUpdateQuery = "UPDATE students SET Fine = '" & fine & "' WHERE ID = '" & StudentID_tb.Text & "'"
                '    balanceUpdateQuery = "UPDATE students SET Balance = '" & balance & "' WHERE ID = '" & StudentID_tb.Text & "'"
                'Else
                '    fineUpdateQuery = "UPDATE faculty SET Fine = '" & fine & "' WHERE ID = '" & StudentID_tb.Text & "'"
                '    balanceUpdateQuery = "UPDATE faculty SET Balance = '" & balance & "' WHERE ID = '" & StudentID_tb.Text & "'"
                'End If

                fineUpdateQuery = "UPDATE lib_fine SET fine = '" & fine & "' WHERE uid = '" & StudentID_tb.Text & "'"
                balanceUpdateQuery = "UPDATE lib_fine SET balance = '" & balance & "' WHERE uid = '" & StudentID_tb.Text & "'"

                'Dim addTransactionToAdmin = "INSERT INTO transactions (transaction) VALUES (' " & StudentID_tb.Text & " has paid a fine of Rs. " & iFine.ToString & "')"
                'Dim updateFineCollected = "UPDATE admin SET fineCollected = '" & fineCollected & "' WHERE username = 'admin'"

                ' Execute the UPDATE queries
                Using fineUpdateCommand As New MySqlCommand(fineUpdateQuery, Con)
                    fineUpdateCommand.ExecuteNonQuery()
                End Using

                Using balanceUpdateCommand As New MySqlCommand(balanceUpdateQuery, Con)
                    balanceUpdateCommand.ExecuteNonQuery()
                End Using

                'Using addTransactionToAdminCommand As New MySqlCommand(addTransactionToAdmin, newConnection)
                '    addTransactionToAdminCommand.ExecuteNonQuery()
                'End Using

                'Using updateFineCollectedCommand As New MySqlCommand(updateFineCollected, newConnection)
                '    updateFineCollectedCommand.ExecuteNonQuery()
                'End Using

                Con.Close()


            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
        'End Using
        If successful Then
            MessageBox.Show("Fine payment of Rs." + userFine.ToString + " successful!")
        End If

        ' After paying the fine, clear the inputs and show the msg box that it is Returned....
        BookID_tb2.Text = ""
        addBalance_tb.Text = ""
        StudentID_tb.Text = ""
        Fine_tb.Text = ""
        Panel9.Visible = False
        Panel2.Visible = True
        Panel3.Visible = True
        Panel7.Visible = True

        Con.Close()


    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Panel9.Visible = False
        Panel2.Visible = True
        Panel3.Visible = True
        Panel7.Visible = True
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
        TextBox2.Text = ""
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim lib_adminDash = New lib_adminDash()
        lib_adminDash.Show()
        Me.Close()
    End Sub
End Class