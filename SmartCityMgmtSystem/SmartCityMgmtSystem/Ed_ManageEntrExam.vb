Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Ed_ManageEntrExam
    Private examID As Integer
    Private entranceExamHandler As New Ed_EntranceExam_Handler()
    Private details As Ed_EntranceExam_Handler.EEDetails
    Private venues As List(Of Ed_EntranceExam_Handler.EEVenue)
    Private pendingAdmits As List(Of Ed_EntranceExam_Handler.EEStudentAdmit)
    Private About As String
    Private Syllabus As String
    Private updatedSyllabus As String
    Private updatedAbout As String
    Private newVenueName As String
    Private newVenueAddress As String
    Private newVenueCapacity As Integer

    ' Parameterized constructor to set the value for examID
    Public Sub New(examID As Integer)
        InitializeComponent()
        Me.examID = examID
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Globals.viewChildForm(Ed_GlobalDashboard.innerpanel, New Ed_ManageEntrHome())
        Me.Close()
    End Sub

    Private Sub Ed_ManageEntrExam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        details = entranceExamHandler.get_ee_details(examID)
        venues = entranceExamHandler.get_all_venues()
        pendingAdmits = entranceExamHandler.extract_pending_student_admits(examID)
        Label11.Text = details.Name
        ComboBox1.Text = details.Exam_application
        ComboBox2.Text = details.Exam_results
        RichTextBox1.Text = details.Notice_Message
        RichTextBox2.Text = details.Contact_email
        RichTextBox3.Text = details.Contact_Info
        About = details.About
        Syllabus = details.Syllabus
        updatedSyllabus = Syllabus
        updatedAbout = About
        DateTimePicker1.Value = details.Date_Time


        For Each pending As Ed_EntranceExam_Handler.EEStudentAdmit In pendingAdmits
            Dim pendingItem As New Ed_ExamApprvItem()
            pendingItem.examID = examID
            pendingItem.studentID = pending.Student_ID
            pendingItem.Label2.Text = "Name : " + pending.User_Name
            pendingItem.Label1.Text = "Contact : " + pending.Phone_Number
            pendingItem.Label3.Text = "UID : " + pending.Student_ID.ToString()
            ' Set properties of venueItem using venue data
            FlowLayoutPanel1.Controls.Add(pendingItem)
        Next

        For Each venue As Ed_EntranceExam_Handler.EEVenue In venues
            Dim venueItem As New Ed_VenueItem()
            venueItem.Label1.Text = venue.Capacity.ToString()
            venueItem.Label2.Text = venue.Name
            venueItem.Label3.Text = venue.Address
            ' Set properties of venueItem using venue data
            FlowLayoutPanel2.Controls.Add(venueItem)
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim updateForm As New Ed_UpdateForm()
        updateForm.original = updatedAbout
        updateForm.ShowDialog()

        ' After the update form is closed, update the values if the DialogResult is OK
        If updateForm.DialogResult = DialogResult.OK Then
            updatedAbout = updateForm.updated
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim updateForm As New Ed_UpdateForm()
        updateForm.original = updatedSyllabus
        updateForm.ShowDialog()

        ' After the update form is closed, update the values if the DialogResult is OK
        If updateForm.DialogResult = DialogResult.OK Then
            updatedSyllabus = updateForm.updated
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim addnewVenue As New Ed_newVenue()
        addnewVenue.ShowDialog()

        If addnewVenue.DialogResult = DialogResult.OK Then
            newVenueAddress = addnewVenue.venueAddress
            newVenueCapacity = addnewVenue.venueCapacity
            newVenueName = addnewVenue.venueName
            entranceExamHandler.add_new_venue(newVenueName, newVenueAddress, newVenueCapacity)
            FlowLayoutPanel2.Controls.Clear()
            venues = entranceExamHandler.get_all_venues()
            For Each venue As Ed_EntranceExam_Handler.EEVenue In venues
                Dim venueItem As New Ed_VenueItem()
                venueItem.Label1.Text = venue.Capacity.ToString()
                venueItem.Label2.Text = venue.Name
                venueItem.Label3.Text = venue.Address
                ' Set properties of venueItem using venue data
                FlowLayoutPanel2.Controls.Add(venueItem)
            Next
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim _details As Ed_EntranceExam_Handler.EEDetails
        _details.About = updatedAbout
        _details.Syllabus = updatedSyllabus
        _details.Name = details.Name
        If (CheckBox1.Checked) Then
            _details.Date_Time = DateTimePicker1.Value
        Else
            _details.Date_Time = details.Date_Time
        End If
        _details.Venue_List = details.Venue_List
        _details.Exam_application = ComboBox1.Text
        _details.Exam_results = ComboBox2.Text
        _details.Notice_Message = RichTextBox1.Text
        _details.Contact_email = RichTextBox2.Text
        _details.Contact_Info = RichTextBox3.Text
        entranceExamHandler.update_ee_details(examID, _details)
    End Sub


End Class