Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports SmartCityMgmtSystem.Ed_Coursera_Handler
Imports SmartCityMgmtSystem.Ed_GlobalDashboard
Public Class Ed_Certificate
    Public Property certData As CertificateData
    Dim handler As New Ed_Coursera_Handler()

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim form As New Ed_CertificateList()
        form.EC_Insti = "Coursera"
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, form)
        Me.Close()
    End Sub

    Private Sub Ed_Certificate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim result As Tuple(Of String, String) = handler.GetInstituteAndCourseName(certData.Course_ID)
        Label1.Text = result.Item1
        Label5.Text = result.Item2
        Label2.Text = Ed_GlobalDashboard.userName
        Label6.Text = certData.Year.ToString()
    End Sub

    Private WithEvents printDocument As New PrintDocument()

    ' Handle the PrintPage event to specify what should be printed
    Private Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDocument.PrintPage
        ' Draw the form on the printer
        Dim bmp As New Bitmap(Me.Width, Me.Height)
        Me.DrawToBitmap(bmp, New Rectangle(0, 0, Me.Width, Me.Height))
        e.Graphics.DrawImage(bmp, 0, 0)
    End Sub

    ' Handle the button click event to trigger printing
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Show print dialog and print the document
        Dim printDialog As New PrintDialog()
        printDialog.Document = printDocument
        If printDialog.ShowDialog() = DialogResult.OK Then
            printDocument.Print()
        End If
    End Sub
End Class