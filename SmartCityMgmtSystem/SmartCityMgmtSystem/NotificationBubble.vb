Public Class NotificationBubble
    Public Sub SetDetails(Optional ByVal title As String = "Notification",
                          Optional ByVal timestamp As String = "28 Mar 24,05:30 PM",
                          Optional ByVal msg As String = "",
                          Optional ByVal ministry_id As Integer = 1,
                          Optional ByVal ministry_name As String = "Ministry of Labour and Employment")
        ' Set the labels
        Label1.Text = title
        Label2.Text = timestamp
        RichTextBox1.Text = msg

        ' Set the ministry image
        Select Case ministry_id
            Case 1
                'PictureBox1.Image = My.Resources.icons8_money_48
                PictureBox1.Image = My.Resources.icons8_helping_hand_100
            Case 2
                PictureBox1.Image = My.Resources.icons8_books_64
                'PictureBox1.Image = My.Resources.icons8_politician_96
            Case 3
                'PictureBox1.Image = My.Resources.indian_wedding
                PictureBox1.Image = My.Resources.icons8_medical_doctor_50
            Case 4
                PictureBox1.Image = My.Resources.icons8_driving_license_64
            Case 5
                PictureBox1.Image = My.Resources.indian_wedding
                'PictureBox1.Image = My.Resources.icons8_medicine_96
            Case 6
                'PictureBox1.Image = My.Resources.icons8_books_64
                PictureBox1.Image = My.Resources.icons8_politician_96
            Case 7
                PictureBox1.Image = My.Resources.icons8_medical_doctor_50
            Case 8
                PictureBox1.Image = My.Resources.icons8_staff_96
            Case Else
                PictureBox1.Image = My.Resources.icons8_politician_96
                Exit Select
        End Select

        Label3.Text = "Issued By: " & ministry_name
    End Sub



End Class
