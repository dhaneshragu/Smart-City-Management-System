Imports System.Data.SqlClient
Imports System.IO
Imports Google.Protobuf.WellKnownTypes
Imports Org.BouncyCastle.Cmp
Public Class Ed_CertificateList
    Public EC_Insti As String
    Public uploadedFileBytes() As Byte
    Private Sub Ed_CertificateList_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Label1.Text = "Marksheets"
        Label2.Text = "Extra-Curricular Certificates"
        Label3.Text = "Entrance Exam Results"

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
            Add_Buttons(FlowLayoutPanel1, 100)
        ElseIf EC_Insti = "Institute" Then
            ' Configure FlowLayoutPanel1
            ConfigureFlowLayoutPanel(FlowLayoutPanel1)

            ' Configure FlowLayoutPanel2
            ConfigureFlowLayoutPanel(FlowLayoutPanel2)

            ' Configure FlowLayoutPanel3
            ConfigureFlowLayoutPanel(FlowLayoutPanel3)
            Add_Buttons(FlowLayoutPanel1, 20)
            Add_Buttons(FlowLayoutPanel2, 20)
            Add_Buttons(FlowLayoutPanel3, 20)
        End If

    End Sub

    Private Sub Add_Buttons(panel As FlowLayoutPanel, buttonCount As Integer)
        Dim button1 As New Button()
        button1.Text = "Upload"
        button1.Size = New Size(120, 120)
        AddHandler button1.Click, AddressOf Upload_Certificate
        panel.Controls.Add(button1)
        For i As Integer = 1 To buttonCount
            Dim button As New Button()
            button.Text = $"Button {i}: Button Text Here" ' Set text for each button individually
            button.Size = New Size(120, 120)
            AddHandler button.Click, AddressOf Button_Click
            panel.Controls.Add(button)
        Next
    End Sub
    Private Sub ConfigureFlowLayoutPanel(panel As FlowLayoutPanel)
        ' Set AutoScroll, WrapContents, and AutoSize properties
        panel.AutoScroll = True
        panel.WrapContents = False
        panel.AutoSize = False
        panel.HorizontalScroll.Visible = True
    End Sub
    Private Sub Button_Click(sender As Object, e As EventArgs)
        Dim pdf As Byte() = If(uploadedFileBytes IsNot Nothing AndAlso uploadedFileBytes.Length > 0, uploadedFileBytes, New Byte() {})
        Using tmp As New FileStream("file.pdf", FileMode.Create)
            tmp.Write(pdf, 0, pdf.Length)
        End Using
        Process.Start("file.pdf")

    End Sub
    Private Sub Upload_Certificate(sender As Object, e As EventArgs)
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.Filter = "PDF Files|*.pdf"
        openFileDialog1.Title = "Select a PDF File"

        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = openFileDialog1.FileName

            ' Read the file and convert it to byte array
            Try
                Using fileStream As FileStream = File.OpenRead(filePath)
                    ReDim uploadedFileBytes(fileStream.Length - 1)
                    fileStream.Read(uploadedFileBytes, 0, uploadedFileBytes.Length)
                End Using

                MessageBox.Show("PDF file uploaded successfully and converted to bytes.")
            Catch ex As Exception
                MessageBox.Show("Error reading file: " & ex.Message)
            End Try
        End If
    End Sub



End Class