Imports System.IO
Imports SmartCityMgmtSystem.Ed_GlobalDashboard

Public Class Ed_CertButton
    Public Property certData As CertificateData

    Private Sub Ed_CertButton_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim certificateName As String = $"{certData.Year}_{certData.Inst_ID}}_{certData.Course_ID}_{certData.sClass}"

        Button1.Text = certificateName
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If certData.Type = "E-Course" Then
            Dim form As New Ed_Certificate()
            form.certData = certData
            Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, form)
        Else
            Dim button As Button = DirectCast(sender, Button)

            ' Get the certificate data from the button tag
            Dim certificateData As Byte() = DirectCast(certData.Certificate, Byte())
            Dim filePath As String = "file.pdf"
            File.WriteAllBytes(filePath, certificateData)
            Using tmp As New FileStream(filePath, FileMode.Create)
                tmp.Write(certificateData, 0, certificateData.Length)
            End Using
            Process.Start(filePath)
        End If
    End Sub
End Class
