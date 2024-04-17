Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar
Imports MySql.Data.MySqlClient


Public Class lib_adminReq
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim lib_adminDash = New lib_adminDash()
        lib_adminDash.Show()
        Me.Close()
    End Sub

    Private Structure Entry
        Public Title As String
        Public Author As String
        Public RDate As String
        Public Status As String
        Public RadioButton As RadioButton ' Added RadioButton field
        Public UID As String
    End Structure

    Dim requestBooks As New List(Of Entry)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim lib_adminMT = New lib_adminMT()
        lib_adminMT.Show()
        Me.Close()
    End Sub

    Private Sub lib_adminReq_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadrequestBooks()
        PopulateTable()
    End Sub

    Private Sub LoadrequestBooks()
        requestBooks.Clear()
        Dim query As String = "SELECT title, author, date, status, uid " &
                          "FROM lib_book_request "
        Dim Con = Globals.GetDBConnection()

        Try
            Con.Open()
            Dim cmd As New MySqlCommand(query, Con)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim status As Integer = Convert.ToInt32(reader("status"))

                ' Map status to corresponding string
                Dim statusString As String = ""
                Select Case status
                    Case -1
                        Continue While
                        statusString = "Rejected"
                    Case 0
                        statusString = "Pending"
                    Case 1
                        statusString = "Approved"
                        Continue While
                    Case Else
                        statusString = "Unknown"
                        Continue While
                End Select

                requestBooks.Add(New Entry With {
                .RDate = reader("date").ToString(),
                .Author = reader("author").ToString(),
                .Title = reader("title").ToString(),
                .Status = statusString, ' Set status based on the mapping
                .RadioButton = New RadioButton(),
                .UID = reader("uid").ToString()
            })
            End While
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            Con.Close() ' Close the connection in the end
        End Try
    End Sub



    Private Sub PopulateTable()
        requestBooksTablePanel.Controls.Clear()
        requestBooksTablePanel.RowStyles.Clear()
        For rowIndex As Integer = 0 To requestBooks.Count - 1
            requestBooksTablePanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 50))
        Next

        If requestBooks.Count > 0 Then
            For rowIndex As Integer = 0 To requestBooks.Count - 1
                Dim entry As Entry = requestBooks(rowIndex)


                ' Add book details
                Dim statusLabel As New Label()
                statusLabel.Text = entry.Status.ToString()
                requestBooksTablePanel.Controls.Add(statusLabel, 3, rowIndex)
                statusLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
                statusLabel.Anchor = AnchorStyles.None ' Set Anchor to None


                Select Case entry.Status
                    Case "Rejected"
                        statusLabel.ForeColor = Color.Red
                    Case "Pending"
                        statusLabel.ForeColor = Color.Navy
                    Case "Approved"
                        statusLabel.ForeColor = Color.Green
                    Case Else
                        statusLabel.ForeColor = Color.Black
                End Select

                Dim authorLabel As New Label()
                authorLabel.Text = entry.Author
                authorLabel.Width = 200 ' Adjust width as needed
                requestBooksTablePanel.Controls.Add(authorLabel, 1, rowIndex)
                authorLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
                authorLabel.Anchor = AnchorStyles.None ' Set Anchor to None

                Dim titleLabel As New Label()
                titleLabel.Text = entry.Title
                titleLabel.AutoSize = True
                requestBooksTablePanel.Controls.Add(titleLabel, 0, rowIndex)
                titleLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
                titleLabel.Anchor = AnchorStyles.None ' Set Anchor to None

                Dim RDateLabel As New Label()
                RDateLabel.Text = entry.RDate
                RDateLabel.Width = 200
                requestBooksTablePanel.Controls.Add(RDateLabel, 2, rowIndex)
                RDateLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
                RDateLabel.Anchor = AnchorStyles.None ' Set Anchor to None

                requestBooksTablePanel.Controls.Add(requestBooks(rowIndex).RadioButton, 6, rowIndex)
                entry.RadioButton.TextAlign = ContentAlignment.MiddleCenter ' center the radio button
                entry.RadioButton.Anchor = AnchorStyles.None ' set anchor to none
                entry.RadioButton.Size = New Size(16, 16) ' set the size of the radio button

            Next

            Dim adjustLabel2 As New Label()
            adjustLabel2.Text = ""
            requestBooksTablePanel.Controls.Add(adjustLabel2, 1, requestBooks.Count)
        End If

    End Sub

    Private Sub btnAddBalance_Click(sender As Object, e As EventArgs) Handles btnAddBalance.Click
        Dim success As Boolean
        For Each entry As Entry In requestBooks
            If entry.RadioButton.Checked Then
                If entry.Status = "Approved" Then
                    MessageBox.Show("Book already approved .")
                    Return
                End If
                Dim query As String = "UPDATE lib_book_request SET status = '1' WHERE title='" & entry.Title & "' AND uid=' " & entry.UID & "'"
                    Dim Con = Globals.GetDBConnection()

                    Try
                        Con.Open()
                        Dim cmd As New MySqlCommand(query, Con)
                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                        If rowsAffected > 0 Then
                            MessageBox.Show("Book approved successfully.")
                            ' Additional actions if needed after update
                            success = True
                        Else
                            MessageBox.Show("No book found ")
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message)
                    Finally
                        Con.Close() ' Close the connection in the end
                    End Try
                End If
        Next

        If success Then
            Dim lib_adminReq = New lib_adminReq()
            lib_adminReq.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim HomePageDashboard = New HomePageDashboard()
        HomePageDashboard.Show()
        Me.Close()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim success As Boolean
        For Each entry As Entry In requestBooks
            If entry.RadioButton.Checked Then
                If entry.Status = "Rejected" Then
                    MessageBox.Show("Book already rejected .")
                    Return
                End If
                Dim query As String = "UPDATE lib_book_request SET status = '-1' WHERE title='" & entry.Title & "' AND uid=' " & entry.UID & "'"
                Dim Con = Globals.GetDBConnection()

                Try
                    Con.Open()
                    Dim cmd As New MySqlCommand(query, Con)
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Book rejected successfully.")
                        ' Additional actions if needed after update
                        success = True
                    Else
                        MessageBox.Show("No book found ")
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                Finally
                    Con.Close() ' Close the connection in the end
                End Try
            End If
        Next

        If success Then
            Dim lib_adminReq = New lib_adminReq()
            lib_adminReq.Show()
            Me.Close()
        End If
    End Sub
End Class