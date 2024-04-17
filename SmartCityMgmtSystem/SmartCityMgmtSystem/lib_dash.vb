Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class lib_dash
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"

    Private Structure Entry
        Public BookID As Integer
        Public Author As String
        Public Title As String
        Public DueDate As String
    End Structure

    Dim overdueBooks As New List(Of Entry)
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

    Private Sub lib_dash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label8.Text = uid
        LoadOverdueBooks()
        PopulateTable()
    End Sub

    Private Sub LoadOverdueBooks()
        overdueBooks.Clear()
        Dim query As String = "SELECT bb.book_ID, bb.dueDate, b.author, b.title " &
                          "FROM lib_borrowed_books bb " &
                          "INNER JOIN lib_books b ON bb.book_ID = b.book_ID " &
                          "WHERE bb.issuedTo = '" & uid & "' AND bb.dueDate < CURRENT_DATE"
        Dim Con = Globals.GetDBConnection()

        Try
            Con.Open()
            Dim cmd As New MySqlCommand(query, Con)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                overdueBooks.Add(New Entry With {
                .BookID = reader("book_ID").ToString(),
                .Author = reader("author").ToString(),
                .Title = reader("title").ToString(),
                .DueDate = reader("dueDate").ToString()
            })
            End While
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            Con.Close() ' Close the connection in the end
        End Try
    End Sub


    Private Sub PopulateTable()
        overdueBooksTablePanel.Controls.Clear()
        overdueBooksTablePanel.RowStyles.Clear()
        For rowIndex As Integer = 0 To overdueBooks.Count - 1
            overdueBooksTablePanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 50))
        Next

        If overdueBooks.Count > 0 Then
            For rowIndex As Integer = 0 To overdueBooks.Count - 1
                Dim entry As Entry = overdueBooks(rowIndex)
                ' Add book details
                Dim bookIdLabel As New Label()
                bookIdLabel.Text = entry.BookID.ToString()
                'bookIdLabel.Font = New Font(bookIdLabel.Font.FontFamily, 13) ' Set font size
                overdueBooksTablePanel.Controls.Add(bookIdLabel, 0, rowIndex)
                bookIdLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
                bookIdLabel.Anchor = AnchorStyles.None ' Set Anchor to None

                Dim authorLabel As New Label()
                authorLabel.Text = entry.Author
                authorLabel.Width = 200 ' Adjust width as needed
                'authorLabel.Font = New Font(authorLabel.Font.FontFamily, 13) ' Set font size
                overdueBooksTablePanel.Controls.Add(authorLabel, 1, rowIndex)
                authorLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
                authorLabel.Anchor = AnchorStyles.None ' Set Anchor to None

                Dim titleLabel As New Label()
                titleLabel.Text = entry.Title
                'titleLabel.AutoSize = False
                'titleLabel.Width = 200
                'titleLabel.Font = New Font(titleLabel.Font.FontFamily, 13) ' Set font size
                overdueBooksTablePanel.Controls.Add(titleLabel, 2, rowIndex)
                titleLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
                titleLabel.Anchor = AnchorStyles.None ' Set Anchor to None
                titleLabel.AutoSize = True

                Dim dueDateLabel As New Label()
                dueDateLabel.Text = entry.DueDate
                dueDateLabel.Width = 200
                'dueDateLabel.Font = New Font(dueDateLabel.Font.FontFamily, 13) ' Set font size
                overdueBooksTablePanel.Controls.Add(dueDateLabel, 3, rowIndex)
                dueDateLabel.TextAlign = ContentAlignment.MiddleCenter ' Center the label
                dueDateLabel.Anchor = AnchorStyles.None ' Set Anchor to None

            Next

            Dim adjustLabel2 As New Label()
            adjustLabel2.Text = ""
            overdueBooksTablePanel.Controls.Add(adjustLabel2, 1, overdueBooks.Count)
        End If

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
End Class