Imports System.Data.SqlClient
Imports System.IO
Imports Google.Protobuf.WellKnownTypes
Imports SmartCityMgmtSystem.Ed_GlobalDashboard
Public Class Ed_UploadCert
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim form As New Ed_CertificateList()
        form.EC_Insti = "Institute"
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, form)
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.Filter = "PDF Files|*.pdf"
        openFileDialog1.Title = "Select a PDF File"
        Dim uploadedFileBytes As Byte() = New Byte() {}

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
        Dim certData As CertificateData = CreateCertificateFromInputs(TextBox1, Ed_GlobalDashboard.userID, ComboBox1, TextBox2, TextBox3, TextBox4, uploadedFileBytes, TextBox5)
        Dim institute_handler As New Ed_Institute_Handler()
        ' Use the connection in the InsertCertificate function
        institute_handler.InsertCertificate(certData)
        Button6_Click(sender, e)
    End Sub
    Public Function CreateCertificateFromInputs(instIdTextBox As TextBox, studentId As Integer, typeComboBox As ComboBox, classTextBox As TextBox, semTextBox As TextBox, yearTextBox As TextBox, uploadedFileBytes As Byte(), certNameTextBox As TextBox) As CertificateData
        ' Create a new CertificateData object
        Dim certData As New CertificateData()

        ' Convert textbox values to appropriate types and assign to CertificateData object
        certData.Inst_ID = If(String.IsNullOrWhiteSpace(instIdTextBox.Text), 0, Convert.ToInt32(instIdTextBox.Text))
        certData.Student_ID = studentId
        certData.Type = typeComboBox.SelectedItem.ToString()
        certData.sClass = If(String.IsNullOrWhiteSpace(classTextBox.Text), 0, Convert.ToInt32(classTextBox.Text))
        certData.sSem = If(String.IsNullOrWhiteSpace(semTextBox.Text), 0, Convert.ToInt32(semTextBox.Text))
        certData.Year = If(String.IsNullOrWhiteSpace(yearTextBox.Text), 0, Convert.ToInt32(yearTextBox.Text))
        certData.CertName = If(String.IsNullOrWhiteSpace(yearTextBox.Text), "NO NAME", Convert.ToString(certNameTextBox.Text))
        certData.Certificate = uploadedFileBytes


        ' Return the CertificateData object
        Return certData
    End Function

    Private Sub Ed_UploadCert_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class