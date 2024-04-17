Imports System.IO
Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class ViewBankTransactions
    Public Property accn As Integer = 1
    Public Property u_name As String = "Me"
    Private Sub LoadandBindDataGridView()
        ' Get connection from globals
        Dim Con As MySqlConnection = Globals.GetDBConnection()

        Try
            Con.Open()

            ' Create a MySqlCommand
            Dim cmd As MySqlCommand = Con.CreateCommand()

            ' Set the SQL query to fetch transactions where sender or receiver account is equal to accn
            cmd.CommandText = "SELECT * FROM transactions WHERE sender_account = @Accn OR receiver_account = @Accn"
            cmd.Parameters.AddWithValue("@Accn", accn)
            'cmd.Parameters.AddWithValue("@Accn", accn)

            ' Execute the SQL query and fetch the data
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            ' Create a DataTable to store the data
            Dim dataTable As New DataTable()

            ' Load the data into the DataTable
            dataTable.Load(reader)

            ' Clear existing columns and data from DataGridView
            DataGridView1.Columns.Clear()
            DataGridView1.DataSource = Nothing

            ' Bind the data to DataGridView
            DataGridView1.DataSource = dataTable

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close the connection
            Con.Close()
        End Try
    End Sub

    Private Sub ViewBankTransactions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
        saveFileDialog.FilterIndex = 1
        saveFileDialog.RestoreDirectory = True

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = saveFileDialog.FileName

            Dim doc As New Document()

            Try
                Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(filePath, FileMode.Create))
                doc.Open()

                ' Add heading "Passbook"
                Dim fontTitle As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK)
                Dim title As New Paragraph("Passbook", fontTitle)
                title.Alignment = Element.ALIGN_CENTER
                doc.Add(title)

                ' Add account details
                Dim fontDetails As Font = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK)
                Dim details As New Paragraph("Account Name: " & u_name, fontDetails)
                details.Alignment = Element.ALIGN_LEFT
                doc.Add(details)

                details = New Paragraph("Account Number: " & accn, fontDetails)
                details.Alignment = Element.ALIGN_LEFT
                doc.Add(details)

                ' Add spacing after details
                doc.Add(New Paragraph(" "))

                ' Create PDF table
                Dim pdfTable As New PdfPTable(DataGridView1.ColumnCount)
                For Each column As DataGridViewColumn In DataGridView1.Columns
                    pdfTable.AddCell(column.HeaderText)
                Next

                For Each row As DataGridViewRow In DataGridView1.Rows
                    For Each cell As DataGridViewCell In row.Cells
                        pdfTable.AddCell(cell.Value.ToString())
                    Next
                Next

                ' Add PDF table to document
                doc.Add(pdfTable)

                ' Close document
                doc.Close()
                writer.Close()

                MessageBox.Show("PDF downloaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

End Class
