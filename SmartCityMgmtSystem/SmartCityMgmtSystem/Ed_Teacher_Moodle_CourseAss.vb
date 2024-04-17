Imports System.Data.SqlClient
Imports System.IO
Imports SmartCityMgmtSystem.Ed_Coursera_Handler
Imports SmartCityMgmtSystem.Ed_Moodle_Handler
Public Class Ed_Teacher_Moodle_CourseAss

    Public Property RoomID As Integer
    Public Property Seq_no As Integer
    Public Property ResourceName As String
    Public Property VideoLink As String
    Public Property TextContent As String

    Private callingPanel As Panel


    Public Property course As Ed_Moodle_Handler.MoodleCourse
    Public Property content As Ed_Moodle_Handler.RoomContent

    'A property which is an array of studentAssRecord'
    Public Property Ass_list As New List(Of StudentAssRecord)()


    Dim handler As New Ed_Moodle_Handler()

    Public Sub New(panel As Panel)
        InitializeComponent()
        callingPanel = panel

    End Sub

    Private Sub Load_Assignments()
        'clear datagridveiw1'
        DataGridView1.Rows.Clear()
        Ass_list.Clear()

        Dim assmarks As StudentAssRecord() = handler.GetAssignmentsSubmitted(RoomID, content.SeqNo)
        For Each record As StudentAssRecord In assmarks
            Ass_list.Add(record)
            Dim marksValue As String = If(record.Marks <> -1, record.Marks.ToString(), "N/A")

            ' Check if submit time is available
            Dim submitTimeValue As String = If(record.Submit_Time <> DateTime.MinValue, record.Submit_Time.ToString(), "N/A")

            ' Add a new row to the DataGridView and populate it with data from each StudentAssRecord'
            Dim newRow As New DataGridViewRow()
            newRow.Height = 40

            ' Add checkbox column
            Dim checkboxCell As New DataGridViewCheckBoxCell()
            checkboxCell.Value = False ' Set initial value
            newRow.Cells.Add(checkboxCell)


            Dim textBoxCell As New DataGridViewTextBoxCell()
            textBoxCell.Value = record.Student_ID
            newRow.Cells.Add(textBoxCell)

            ' Add text column'
            Dim textCell As New DataGridViewTextBoxCell()
            textCell.Value = submitTimeValue
            newRow.Cells.Add(textCell)


            ' Add button column
            Dim buttonCell As New DataGridViewButtonCell()
            buttonCell.Value = "Download" ' Set button text
            newRow.Cells.Add(buttonCell)

            'Add text column'
            Dim textCell2 As New DataGridViewTextBoxCell()
            textCell2.Value = marksValue
            newRow.Cells.Add(textCell2)



            ' Add the row to the DataGridView
            DataGridView1.Rows.Add(newRow)
        Next

    End Sub

    Private Sub Ed_Teacher_Moodle_CourseAss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AutoScroll = True
        Label2.Text = content.ContentName

        course = handler.LoadCourseDetails(RoomID)

        'TextContent = "Problem Statement: Implementing Binary Search" & vbCrLf & vbCrLf & "Description:" & vbCrLf & vbCrLf & "You are tasked with implementing the binary search algorithm in a programming language of your choice. Binary search is a fast and efficient searching algorithm used to find an element within a sorted array by repeatedly dividing the search interval in half. Your implementation should handle both sorted arrays of integers and floating-point numbers."
        TextContent = content.Content
        RichTextBox1.Rtf = TextContent
        Load_Assignments()


    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        course = handler.LoadCourseDetails(RoomID)
        Dim form As New Ed_Teacher_Moodle_CourseContent(callingPanel)
        form.CourseContent = course
        Globals.viewChildForm(callingPanel, form)

        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim edit_assgn As New Ed_Teacher_EditAssgn(content)
        edit_assgn.callingPanel = callingPanel
        edit_assgn.CourseItem = course
        edit_assgn.ShowDialog() ' Show as dialog if needed
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Delete resource'

        handler.DeleteCourseContent(content.RoomID, content.SeqNo)
        Dim form As New Ed_Teacher_Moodle_CourseContent(callingPanel)
        form.CourseContent = course
        Globals.viewChildForm(callingPanel, form)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'read marks from textbox'
        Dim marks As Integer = 0
        If Not String.IsNullOrEmpty(TextBox1.Text) Then
            If Integer.TryParse(TextBox1.Text, marks) Then
                For Each row As DataGridViewRow In DataGridView1.Rows
                    ' Check if the checkbox cell is checked
                    Dim checkboxCell As DataGridViewCheckBoxCell = row.Cells("Column5")
                    If checkboxCell.Value IsNot Nothing AndAlso Convert.ToBoolean(checkboxCell.Value) Then
                        ' Perform your update action for the selected row
                        ' Access row data using cell indexes or column names
                        ' For example:
                        Dim studentID As Integer = Convert.ToInt32(row.Cells("Column1").Value)
                        ' Dim newGrade As Integer = Convert.ToInt32(txtNewGrade.Text)
                        handler.UpdateMarks(marks, content.RoomID, studentID, content.SeqNo)
                    End If
                Next

                Load_Assignments()
                MsgBox("Marks updated successfully.")

            Else
                MsgBox("Invalid input for marks.")
            End If
        Else
            MsgBox("Marks has not been entered.")
        End If

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is a button cell and the button column index
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = DataGridView1.Columns("Column4").Index Then
            ' Handle the button click event for the specific row
            ' You can access the row data using DataGridView1.Rows(e.RowIndex).Cells(ColumnIndex).Value
            ' For example:
            'Dim studentID As String = DataGridView1.Rows(e.RowIndex).Cells("StudentIDColumn").Value.ToString()
            ' Perform actions based on the row data...
            ' Example: MsgBox("Download clicked for Student ID: " & studentID)

            Dim assgnData As Byte() = DirectCast(Ass_list(e.RowIndex).FileData, Byte())
            Dim filePath As String = "file.pdf"
            File.WriteAllBytes(filePath, assgnData)
            Using tmp As New FileStream(filePath, FileMode.Create)
                tmp.Write(assgnData, 0, assgnData.Length)
            End Using
            Process.Start(filePath)

        End If
    End Sub

End Class