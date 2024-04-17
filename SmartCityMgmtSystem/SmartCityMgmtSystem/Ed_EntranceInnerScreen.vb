Imports System.Data.SqlClient
Imports System.Management
Imports System.Web.UI.WebControls
Imports MySql.Data.MySqlClient

Public Class Ed_EntranceInnerScreen
    Private WithEvents marqueeTimer As New Timer()
    Private currentIndex As Integer = 0
    Public labelText As String = ""
    Public ProfilePhoto As Byte()
    Public examID As Integer
    Private details As Ed_EntranceExam_Handler.EEDetails
    Private CurrentAdmit As Ed_EntranceExam_Handler.EEStudentAdmit
    Private CompletedAdmit As Ed_EntranceExam_Handler.EEStudentAdmit
    Private Venue As Ed_EntranceExam_Handler.EEVenue
    Private UnsedVenue As Ed_EntranceExam_Handler.EEVenue

    Private entranceExamHandler As New Ed_EntranceExam_Handler()
    Private ImpNotice As String = "Applications for the upcoming exam are now open! Apply now to secure your spot!"
    Private marqueeText As String
    Private spaces As String = "                                                                          "
    Private Sub Ed_EntranceInnerScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        details = entranceExamHandler.get_ee_details(examID)
        CurrentAdmit = entranceExamHandler.get_ee_studentadmit(Ed_GlobalDashboard.Ed_Profile.Ed_User_ID, examID, DateTime.Now.Year)
        CompletedAdmit = entranceExamHandler.get_ee_completed_studentadmit(Ed_GlobalDashboard.Ed_Profile.Ed_User_ID, examID)
        Venue = entranceExamHandler.get_venue_by_id(CurrentAdmit.Venue_ID)
        ProfilePhoto = entranceExamHandler.get_user_picture(Ed_GlobalDashboard.Ed_Profile.Ed_User_ID)
        Label1.Text = details.Name
        RichTextBox2.Rtf = details.About
        RichTextBox1.Rtf = details.Syllabus
        ImpNotice = details.Notice_Message
        marqueeText = ImpNotice + spaces
        Label5.Text = details.Contact_email
        Label6.Text = details.Contact_Info

        If (details.Exam_application = "Open") Then
            Label10.Text = "Apply For The Exam Here"
            If (CurrentAdmit.Year <> 0) Then
                If (CurrentAdmit.ApprvStatus = "pending") Then
                    Button2.Text = "Application Sent"
                    Button2.BackColor = Color.DarkGoldenrod
                End If
                If (CurrentAdmit.ApprvStatus = "approved") Then
                    If (CurrentAdmit.fee_Status) Then
                        Button2.Text = "View Admit Card"
                    Else
                        Button2.Text = "Pay Fee"
                        Button2.BackColor = Color.SeaGreen
                    End If
                End If
                If (CurrentAdmit.ApprvStatus = "rejected") Then
                    Button2.Hide()
                    Label11.Text = "Your application has been rejected. Come back next year !"
                    Button1.Hide()
                End If
            End If
        End If

        If (details.Exam_application = "Close") Then
            Label10.Text = "Applications for the Exam are currently unavailable"
            Button2.Hide()
        End If

        If (details.Exam_results = "Released") Then

            Label11.Text = "The exam results have been released!"
        Else
            Label11.Text = "No information about the Results yet"
            Button1.Hide()
        End If


        If (CompletedAdmit.Year <> 0) Then
            Label10.Text = "You have already applied for this exam in year : " + CompletedAdmit.Year.ToString
            Button2.Text = "View Admit Card"
            Label11.Text = "View Your Result Here "
        End If



        ' Configure the Timer
        marqueeTimer.Interval = 100 ' Adjust the interval as needed for desired scrolling speed
        marqueeTimer.Start()

        ' Set initial text
        UpdateMarqueeText()

        ' Start the marquee
        StartMarquee()
    End Sub

    Private Sub marqueeTimer_Tick(sender As Object, e As EventArgs) Handles marqueeTimer.Tick
        ' Move to the next character in the marquee text
        currentIndex += 1

        ' Check if we've reached the end of the text
        If currentIndex >= marqueeText.Length Then
            currentIndex = 0
        End If

        ' Update the label with the new text
        UpdateMarqueeText()
    End Sub

    Private Sub StartMarquee()
        ' Start the marquee by enabling the Timer
        marqueeTimer.Enabled = True
    End Sub

    Private Sub StopMarquee()
        ' Stop the marquee by disabling the Timer
        marqueeTimer.Enabled = False
    End Sub

    Private Sub UpdateMarqueeText()
        ' Display a portion of the marquee text starting from currentIndex
        Dim displayedText As String = marqueeText.Substring(currentIndex) & marqueeText.Substring(0, currentIndex)

        ' Smooth transition using opacity
        Dim opacityValue As Double = Math.Abs(Math.Sin(currentIndex / 10)) ' Adjust the divisor for smoother or faster fading
        Label2.Text = displayedText
        Label2.ForeColor = Color.FromArgb(CInt(opacityValue * 255), Label2.ForeColor)
    End Sub

    Private Sub Ed_EntranceInnerScreen_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Ensure the Timer is stopped when the form is closing to prevent memory leaks
        StopMarquee()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Use a Select Case statement for cleaner logic
        Select Case Button2.Text
            Case "Apply"
                entranceExamHandler.AddStudentAdmit(Ed_GlobalDashboard.Ed_Profile.Ed_User_ID, examID, DateTime.Now.Year)
                Button2.Text = "Application Sent"
                Button2.BackColor = Color.DarkGoldenrod
            Case "Pay Fee"
                'Dim feePopUp As New Ed_FeePopup()
                'feePopUp.UserID = Ed_GlobalDashboard.Ed_Profile.Ed_User_ID
                'feePopUp.UserName = Ed_GlobalDashboard.Ed_Profile.Ed_Name
                'feePopUp.PayingTo = "Education Minister"
                'feePopUp.PurposeOfPayment = "Exam Entrance Fee"
                'feePopUp.Amount = "200"
                'feePopUp.ShowDialog()
                'If (feePopUp.DialogResult = DialogResult.OK) Then
                'entranceExamHandler.UpdateFeeStatus(Ed_GlobalDashboard.Ed_Profile.Ed_User_ID, examID, DateTime.Now.Year)
                'Button2.Text = "View Admit Card"
                'Button2.BackColor = Color.FromArgb(40, 68, 114)
                'CurrentAdmit.fee_Status = 1
                'End If
                Dim pay = New PaymentGateway() With {
                    .uid = Ed_GlobalDashboard.Ed_Profile.Ed_User_ID
                }
                pay.readonly_prop = True
                pay.TextBox2.Text = 200
                pay.TextBox3.Text = "Entrance Exam Fee"
                pay.TextBox1.Text = 3
                If (pay.ShowDialog() = DialogResult.OK) Then
                    entranceExamHandler.UpdateFeeStatus(Ed_GlobalDashboard.Ed_Profile.Ed_User_ID, examID, DateTime.Now.Year)
                    UnsedVenue = entranceExamHandler.GetLowestUnusedVenueAndUpdateCapacity()
                    entranceExamHandler.UpdateExamVenueID(Ed_GlobalDashboard.Ed_Profile.Ed_User_ID, examID, DateTime.Now.Year, UnsedVenue.EE_Venue_ID)
                    Button2.Text = "View Admit Card"
                    Button2.BackColor = Color.FromArgb(40, 68, 114)
                    CurrentAdmit.fee_Status = 1
                    Venue = UnsedVenue
                End If

            Case "View Admit Card"
                Dim admitCard As New Ed_ExamAdmitCard()
                admitCard.UserID = Ed_GlobalDashboard.Ed_Profile.Ed_User_ID
                admitCard.UserName = Ed_GlobalDashboard.Ed_Profile.Ed_Name
                admitCard.ExamName = details.Name
                admitCard.Venue = Venue.Address
                admitCard.Date_Time = details.Date_Time
                admitCard.ProfilePhoto = ProfilePhoto
                admitCard.ShowDialog()
        End Select

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim resultCard As New Ed_ExamResultCard()
        resultCard.UserID = Ed_GlobalDashboard.Ed_Profile.Ed_User_ID
        resultCard.UserName = Ed_GlobalDashboard.Ed_Profile.Ed_Name
        resultCard.ExamName = details.Name
        resultCard.Rank = CompletedAdmit.Result_Rank
        resultCard.Date_Time = details.Date_Time
        resultCard.ProfilePhoto = ProfilePhoto
        resultCard.ShowDialog()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
