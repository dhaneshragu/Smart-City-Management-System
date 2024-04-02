Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class Ed_EntranceInnerScreen
    Private WithEvents marqueeTimer As New Timer()
    Private currentIndex As Integer = 0
    Public labelText As String = ""
    Public examID As Integer
    Private marqueeText As String = "Applications for the upcoming exam are now open! Apply now to secure your spot!                                                                            "
    Private Sub Ed_EntranceInnerScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Con = Globals.GetDBConnection()
        Try
            Con.Open()

            ' Check if an entry corresponding to the userID exists in the ee_details table
            Dim query As String = "SELECT Syllabus, About FROM ee_details WHERE Exam_ID = @examID"
            Dim cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@examID", examID)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                ' Populate syllabus and about from the database query result
                If Not reader.IsDBNull(reader.GetOrdinal("Syllabus")) Then
                    Dim syllabus As String = reader.GetString(reader.GetOrdinal("Syllabus"))
                    RichTextBox1.Rtf = syllabus
                End If

                If Not reader.IsDBNull(reader.GetOrdinal("About")) Then
                    Dim about As String = reader.GetString(reader.GetOrdinal("About"))
                    RichTextBox2.Rtf = about
                End If
            End If

            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try


        Label1.Text = labelText
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

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

End Class
