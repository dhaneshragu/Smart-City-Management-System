Imports System.Data.SqlClient
Public Class TransportVehicleRegReq
    Public Property uid = 11
    Public Property u_name = "Dhanesh"
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is in the "Column3" column and not a header cell
        If e.ColumnIndex = DataGridView1.Columns("Column3").Index AndAlso e.RowIndex >= 0 Then
            ' Change this to DB logic later
            MessageBox.Show("Invoice pdf will be fetched " & e.RowIndex.ToString(), "Invoice PDF", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub



    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' Dummy Data, Change it to LoadandBindDataGridView() 
        For i As Integer = 1 To 8
            ' Add an empty row to the DataGridView
            Dim row As New DataGridViewRow()
            DataGridView1.Rows.Add(row)

            ' Set values for the first three columns in the current row
            DataGridView1.Rows(i - 1).Cells("Column1").Value = "DummyVal"
            DataGridView1.Rows(i - 1).Cells("Column2").Value = "DummyVal"

        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        MessageBox.Show("Payment request will be sent. ", "Registration fee Payment", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MessageBox.Show("Registration will be cancelled ", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        MessageBox.Show("Select Invoice PDF ", "browse", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Vehicle_picbtn.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.gif;*.png)|*.bmp;*.jpg;*.jpeg;*.gif;*.png"
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Dim imageFilePath As String = openFileDialog.FileName
            Dim image As Image = Image.FromFile(imageFilePath)
            PictureBox1.Image = image
        End If
    End Sub
End Class