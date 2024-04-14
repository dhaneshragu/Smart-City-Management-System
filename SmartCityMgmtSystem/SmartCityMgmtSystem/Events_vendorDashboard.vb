Public Class Events_vendorDashboard

    

    Dim dt As New DataTable

    Private Sub Events_vendorDashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Add columns to the DataTable
        dt.Columns.Add("SL No", GetType(Integer))
        dt.Columns.Add("Timestamp", GetType(DateTime))
        dt.Columns.Add("VendorID", GetType(Integer))
        dt.Columns.Add("CustomerID", GetType(Integer))
        dt.Columns.Add("TransactionID", GetType(Integer))
        dt.Columns.Add("Invoice", GetType(String))
        ' Populate the DataTable with 5 random examples
        For i As Integer = 1 To 5
            dt.Rows.Add(i, DateTime.Now, GetRandomID(), GetRandomID(), GetRandomID())
        Next
        Dim buttonColumn As New DataGridViewButtonColumn()
        buttonColumn.HeaderText = "Invoice"
        buttonColumn.Text = "Generate"
        buttonColumn.UseColumnTextForButtonValue = True
        vendorIDDashboard.Columns.Add(buttonColumn)
        ' Bind the DataTable to the DataGridView
        vendorIDDashboard.DataSource = dt
    End Sub

    Private Function GetRandomID() As Integer
        ' Generate a random ID for testing purposes
        Dim random As New Random()
        Return random.Next(1, 1000)
    End Function

    
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click

    End Sub
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub
    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
    Private Sub childformPanel_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles childformPanel.Paint

    End Sub
End Class