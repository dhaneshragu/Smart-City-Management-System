Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports System.Diagnostics.Eventing
Imports System.IO

Public Class TransportAdminVRReq
    Private uid As Integer
    Private Accept_click As Boolean
    Private Reject_click As Boolean
    Private row1 As Integer = 0
    Private vType As String
    Private vTypeId As Integer
    Private id As String
    Private Function ByteArrayToImage(byteArray As Byte()) As Image
        ' Create a MemoryStream from the byte array
        Using ms As New MemoryStream(byteArray)
            ' Create an Image from the MemoryStream
            Return Image.FromStream(ms)
        End Using
    End Function

    Private Function ResizeImage(originalImage As Image, width As Integer, height As Integer) As Image
        ' Create a new bitmap with the desired width and height
        Dim resizedImage As New Bitmap(width, height)

        ' Create a Graphics object from the resized bitmap
        Using g As Graphics = Graphics.FromImage(resizedImage)
            ' Set interpolation mode to high quality bicubic to ensure the best quality
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic

            ' Draw the original image onto the resized bitmap using DrawImage method
            g.DrawImage(originalImage, 0, 0, width, height)
        End Using

        ' Return the resized image
        Return resizedImage
    End Function

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is in the "Column4" column and not a header cell
        If e.ColumnIndex = DataGridView1.Columns("Column4").Index AndAlso e.RowIndex >= 0 Then
            row1 = e.RowIndex
            Dim cellValue As Object = DataGridView1.Rows(row1).Cells(0).Value
            id = cellValue
            Dim image As Image = Globals.GetPicture($"SELECT vehicle_pic FROM vehicle_reg WHERE vehicle_id ='{id}'", "vehicle_pic")
            If image IsNot Nothing Then
                Dim resizedImage As Image = ResizeImage(image, 100, 100)
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = resizedImage
            Else
                MessageBox.Show("No image found in the database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        ElseIf e.ColumnIndex = DataGridView1.Columns("Column5").Index AndAlso e.RowIndex >= 0 Then
            row1 = e.RowIndex
            Dim cellValue As Object = DataGridView1.Rows(row1).Cells(0).Value
            id = cellValue
            'Retrieve the PDF bytes from the database
            Using Con As MySqlConnection = Globals.GetDBConnection()
                Con.Open()
                Dim query As String = "SELECT inv_pdf FROM vehicle_reg WHERE vehicle_id = @a"
                Using command As New MySqlCommand(query, Con)
                    command.Parameters.AddWithValue("@a", id)
                    ' Execute the query and read the PDF data
                    Dim pdfData As Byte() = Nothing
                    Dim result As Object = command.ExecuteScalar()

                    If result IsNot DBNull.Value Then
                        pdfData = DirectCast(result, Byte())
                    End If
                    ' Check if the PDF data is not null
                    If pdfData IsNot Nothing Then
                        ' Save the PDF data to a temporary file with .tmp extension
                        Dim tempFilePath As String = Path.GetTempFileName()

                        ' Rename the file with .pdf extension
                        Dim pdfFilePath As String = Path.ChangeExtension(tempFilePath, ".pdf")
                        File.WriteAllBytes(pdfFilePath, pdfData)

                        ' Open the PDF file using the default web browser
                        Process.Start(pdfFilePath)
                    Else
                        ' If no PDF data is found in the database
                        MessageBox.Show("No PDF found in the database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            End Using
        ElseIf e.ColumnIndex = DataGridView1.Columns("Column6").Index AndAlso e.RowIndex >= 0 Then
            ' Change this to DB logic later
            Accept_click = True
            row1 = e.RowIndex
            Dim cellValue As Object = DataGridView1.Rows(row1).Cells(1).Value
            Integer.TryParse(cellValue.ToString(), uid)
            vType = DataGridView1.Rows(row1).Cells(2).Value
            vTypeId = TransportGlobals.GetVehicleTypeID(vType)
            LoadandBindDataGridView()

            ' Check if the clicked cell is in the "Column7" column and not a header cell
        ElseIf e.ColumnIndex = DataGridView1.Columns("Column7").Index AndAlso e.RowIndex >= 0 Then
            ' Perform the action for the "Column8" column
            Reject_click = True
            row1 = e.RowIndex
            Dim cellValue As Object = DataGridView1.Rows(row1).Cells(1).Value
            Integer.TryParse(cellValue.ToString(), uid)
            vType = DataGridView1.Rows(row1).Cells(2).Value
            vTypeId = TransportGlobals.GetVehicleTypeID(vType)
            LoadandBindDataGridView()

        End If
    End Sub

    Private Sub LoadandBindDataGridView()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        cmd = New MySqlCommand("SELECT uid, vehicle_type as vehicle_type_ID, vehicle_id, vehicle_pic, inv_pdf FROM vehicle_reg where status = @a ", Con)
        cmd.Parameters.AddWithValue("@a", "requested")
        reader = cmd.ExecuteReader
        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        dataTable.Load(reader)
        reader.Close()

        Dim NewColumn As New DataColumn("vehicle_type", GetType(String))

        ' Add the new column to the DataTable
        dataTable.Columns.Add(NewColumn)
        If dataTable.Rows.Count > 0 Then
            For Each row As DataRow In dataTable.Rows
                Dim id As Integer = If(Not IsDBNull(row("vehicle_type_ID")), Convert.ToInt32(row("vehicle_type_ID")), "")
                Dim name As String = (TransportGlobals.GetVehicleType(id)).ToString()
                row("vehicle_type") = name
            Next
            ' Iterate through the rows to handle empty image data
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim imageCell As DataGridViewImageCell = row.Cells("Column4")

                ' Check if the image cell value is DBNull or Nothing
                If IsDBNull(imageCell.Value) OrElse imageCell.Value Is Nothing Then
                    ' Set a placeholder image or leave the cell blank
                    ' For example, you can set a blank image
                    Dim blankImage As New Bitmap(1, 1)
                    imageCell.Value = blankImage
                End If
            Next
        End If

        'IMP: Specify the Column Mappings from DataGridView to SQL Table
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns(0).DataPropertyName = "vehicle_id"
        DataGridView1.Columns(1).DataPropertyName = "uid"
        DataGridView1.Columns(2).DataPropertyName = "vehicle_type"

        ' Bind the data to DataGridView
        DataGridView1.DataSource = dataTable

        Using command As New MySqlCommand("update vehicle_reg set status = @c where uid = @a and vehicle_type = @b and status = @d ", Con)

            command.Parameters.AddWithValue("@a", uid)
            command.Parameters.AddWithValue("@b", vTypeId)
            command.Parameters.AddWithValue("@d", "requested")
            If Accept_click Then

                command.Parameters.AddWithValue("@c", "approved")

                Accept_click = False
                ' Execute the command (Update statement)
                command.ExecuteNonQuery()
                Globals.SendNotifications(4, uid, "Vehicle Registration Request Approved", "Your Request for Vehicle Registration is approved for vehicle Type " & vType & ". You can view your Vehicle ID in your Vehicle Registration Page.")
            ElseIf Reject_click Then

                command.Parameters.AddWithValue("@c", "rejected")
                Reject_click = False
                ' Execute the command (Update statement)
                command.ExecuteNonQuery()
                Globals.SendNotifications(4, uid, "Vehicle Registration Request Rejected", "Your Request for Vehicle Registration is rejected for vehicle Type " & vType & ". Better luck next time!")
            End If

        End Using
        Con.Close()

    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadandBindDataGridView()
    End Sub

End Class