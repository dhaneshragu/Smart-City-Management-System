Imports System.IO

Public Class ImageViewerForm
    Private imageData As Byte()

    Public Sub New(ByVal imageData As Byte())
        InitializeComponent()
        Me.imageData = imageData
    End Sub

    Private Sub ImageViewerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Convert byte stream data to image
        Dim image As Image = Nothing

        Using ms As New MemoryStream(imageData)
            image = Image.FromStream(ms)
        End Using

        ' Display the image in PictureBox1
        PictureBox1.Image = image
    End Sub
End Class
