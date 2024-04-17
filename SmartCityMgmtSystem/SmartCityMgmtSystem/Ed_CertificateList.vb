Imports System.Data.SqlClient
Imports System.IO
Imports Google.Protobuf.WellKnownTypes
Imports Org.BouncyCastle.Cmp
Imports SmartCityMgmtSystem.Ed_GlobalDashboard
Public Class Ed_CertificateList
    Public EC_Insti As String
    Public uploadedFileBytes() As Byte
    Dim coursera_handler As New Ed_Coursera_Handler()
    Dim insitute_handler As New Ed_Institute_Handler()
    Private Sub Ed_CertificateList_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Label1.Text = "Marksheets"
        Label3.Text = "Extra-Curricular Certificates"
        Label2.Text = "Entrance Exam Results"

        ' Adjust layout if EC_Insti is Coursera
        If EC_Insti = "Coursera" Then
            FlowLayoutPanel2.Visible = False
            Label2.Visible = False
            FlowLayoutPanel3.Visible = False
            Label3.Visible = False
            FlowLayoutPanel1.Height *= 4
            Label1.Text = "Coursera"
            FlowLayoutPanel1.AutoScroll = True
            FlowLayoutPanel1.WrapContents = True
            FlowLayoutPanel1.AutoSize = False
            FlowLayoutPanel1.VerticalScroll.Enabled = True
            FlowLayoutPanel1.HorizontalScroll.Enabled = False
            Add_Coursera_Buttons(FlowLayoutPanel1)
        ElseIf EC_Insti = "Institute" Then
            ' Configure FlowLayoutPanel1
            ConfigureFlowLayoutPanel(FlowLayoutPanel1)

            ' Configure FlowLayoutPanel2
            ConfigureFlowLayoutPanel(FlowLayoutPanel2)

            ' Configure FlowLayoutPanel3
            ConfigureFlowLayoutPanel(FlowLayoutPanel3)
            Add_ButtonsByType(FlowLayoutPanel1, "Marksheet")
            Add_ButtonsByType(FlowLayoutPanel2, "Entrance-Result")
            Add_ButtonsByType(FlowLayoutPanel3, "Extra-Curricular")

        End If

    End Sub

    Public Sub Add_Coursera_Buttons(panel As FlowLayoutPanel)
        Dim certificates As List(Of CertificateData) = coursera_handler.GetCertificates(Ed_GlobalDashboard.userID)
        panel.Controls.Clear()
        For Each certificate As CertificateData In certificates
            Dim button As New Ed_CertButton()
            button.certData = certificate
            panel.Controls.Add(button)
        Next
    End Sub


    Private Sub Add_ButtonsByType(panel As FlowLayoutPanel, TypeofCert As String)
        Dim certificates As List(Of CertificateData) = insitute_handler.GetCertificatesByType(Ed_GlobalDashboard.userID, TypeofCert)
        For Each certificate As CertificateData In certificates
            Dim button As New Ed_CertButton()
            button.certData = certificate
            panel.Controls.Add(button)
        Next
    End Sub
    Private Sub ConfigureFlowLayoutPanel(panel As FlowLayoutPanel)
        panel.Controls.Clear()
        ' Set AutoScroll, WrapContents, and AutoSize properties
        panel.AutoScroll = True
        panel.WrapContents = False
        panel.AutoSize = False
        panel.HorizontalScroll.Visible = True
        Dim button1 As New Button()
        button1.Text = "Upload"
        button1.Size = New Size(120, 120)
        AddHandler button1.Click, AddressOf Upload_Certificate
        panel.Controls.Add(button1)
    End Sub

    Private Sub Upload_Certificate(sender As Object, e As EventArgs)
        Dim form As New Ed_UploadCert()
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, form)

    End Sub



End Class