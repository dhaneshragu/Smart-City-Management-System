Imports System.Data.SqlClient
Public Class TransportTGEnter
    Public Property uid As Integer = 11
    Public Property u_name As String = "Dhanesh"
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub LoadandBindDataGridView()
        'Get connection String from globals
        Dim conString = Globals.getdbConnectionString()
        Dim Con = New SqlConnection(conString)

        'Query for SQL table
        Dim query = "enter your query"
        Con.Open()

        Dim cmd As New SqlCommand(query, Con)
        Dim adapter As New SqlDataAdapter(cmd)

        ' Create a DataTable to store the data
        Dim dataTable As New DataTable()

        'Fill the DataTable with data from the SQL table
        adapter.Fill(dataTable)

        'IMP: Specify the Column Mappings from DataGridView to SQL Table

        ' Bind the data to DataGridView

    End Sub

    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' Dummy Data, Change it to LoadandBindDataGridView() 
        For i As Integer = 1 To 8
            ' Add an empty row to the DataGridView
            Dim row As New DataGridViewRow()


            ' Set values for the first three columns in the current row

        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        MessageBox.Show("Your Fastag ID is authentic.Payment request will be sent.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class