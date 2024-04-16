Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports SmartCityMgmtSystem.Ed_GlobalDashboard
Public Class Ed_Certificate
    Public Property certData As CertificateData

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim form As New Ed_CertificateList()
        form.EC_Insti = "Coursera"
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, form)
        Me.Close()
    End Sub

    Private Sub Ed_Certificate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = certData.Inst_ID.ToString()
        Label2.Text = certData.Student_ID.ToString()
        Label3.Text = certData.Type
        Label4.Text = certData.sClass.ToString()
        Label5.Text = certData.sSem.ToString()
        Label6.Text = certData.Year.ToString()
    End Sub
End Class