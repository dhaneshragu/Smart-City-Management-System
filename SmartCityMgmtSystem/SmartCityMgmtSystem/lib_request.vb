Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports Ubiety.Dns.Core

Public Class lib_request
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"

    Private Structure Entry
        Public Title As String
        Public Author As String
        Public RDate As String
        Public Status As String
    End Structure

    Dim requestBooks As New List(Of Entry)
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

    Private Sub lib_request_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = u_name
        Label9.Text = uid
        LoadrequestBooks()
        PopulateTable()
    End Sub

    Private Sub LoadrequestBooks()
        requestBooks.Clear()
        Dim query As String = "SELECT title, author, date, status " &
                          "FROM lib_book_request br " &
                          "WHERE br.uid = '" & uid & "'"
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
                        statusString = "Rejected"
                    Case 0
                        statusString = "Pending"
                    Case 1
                        statusString = "Approved"
                    Case Else
                        statusString = "Unknown"
                End Select

                requestBooks.Add(New Entry With {
                .RDate = reader("date").ToString(),
                .Author = reader("author").ToString(),
                .Title = reader("title").ToString(),
                .Status = statusString ' Set status based on the mapping
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

            Next

            Dim adjustLabel2 As New Label()
            adjustLabel2.Text = ""
            requestBooksTablePanel.Controls.Add(adjustLabel2, 1, requestBooks.Count)
        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim HomePageDashboard = New HomePageDashboard() With {
            .uid = uid
        }
        HomePageDashboard.Show()
        Me.Close()
    End Sub

    Protected Sub btnAddRequest_Click(sender As Object, e As EventArgs) Handles btnAddRequest.Click
        Dim lib_NewRequest = New lib_NewRequest() With {
            .uid = uid
        }
        lib_NewRequest.Show()
        Me.Close()
    End Sub

    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub
End Class