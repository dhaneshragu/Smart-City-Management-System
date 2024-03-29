Public Class UserProfile
    Private uid As Integer

    Public Sub New(uid As Integer)
        InitializeComponent()
        Me.uid = uid ' Store the passed email ID in a private field
    End Sub
    Private Sub TransportationDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        'View the TransportationAdminHome screen by default - first argument, name of the panel in the parent panel, second - name of the child form
        Globals.viewChildForm(childformPanel, TransportationAdminHome)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        'Dim home = New HomePageDashboard(uid)
        'home.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        OpenFileDialog1.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Get the selected file path
            Dim imagePath As String = OpenFileDialog1.FileName
            ' Display the selected image in a PictureBox control
            PictureBox2.Image = Image.FromFile(imagePath)
        End If
    End Sub
End Class
