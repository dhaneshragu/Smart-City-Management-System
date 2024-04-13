Public Class EventInvoice
    Public Property uid As Integer
    Private Property customerID As Integer
    Private Property vendorID As Integer
    Private Property transactionID As String
    Private Property cost As Integer
    Private Property duration As Integer
    Private Property vendorName As String
    Private Property customerName As String

    Public Sub New(ByVal customerID As Integer, ByVal vendorID As Integer, ByVal transactionID As String, ByVal cost As Integer, ByVal duration As Integer, ByVal vendorName As String, ByVal customerName As String)
        InitializeComponent()
        Me.customerID = customerID
        Me.vendorID = vendorID
        Me.transactionID = transactionID
        Me.cost = cost
        Me.duration = duration
        Me.vendorName = vendorName
        Me.customerName = customerName
    End Sub


    Private Sub Invoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim row As New DataGridViewRow()
        DataGridView1.Rows.Add(row)

        DataGridView1.Rows(0).Cells("TRANSACTION_ID").Value = transactionID
        DataGridView1.Rows(0).Cells("RATE").Value = cost
        DataGridView1.Rows(0).Cells("DAYS").Value = duration
        DataGridView1.Rows(0).Cells("AMOUNT").Value = cost * duration
        total.Text = cost * duration
        customerDescription.Text = customerName
        vendorAddress.Text = vendorName
    End Sub


End Class
