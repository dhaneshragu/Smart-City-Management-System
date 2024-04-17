Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Utilities
Public Class Complaints_Admin_Take_action
    Public Property uid As Integer = -1
    Public Property pid As Integer = -1
    Public Property u_name As String = "Hello"


    Private Sub Complaints_Admin_Take_action_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Text = pid.ToString()



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Assuming you have three RichTextBox controls named richTextBoxCount1, richTextBoxCount2, and richTextBoxCount3
        ' Assuming you have three RichTextBox controls named richTextBoxCount1, richTextBoxCount2, and richTextBoxCount3
        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("Please fill in all text fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            Dim Con = Globals.GetDBConnection()

            Try
                ' Open the connection
                Con.Open()

                Dim query As String = "UPDATE complaints SET Remarks = @newValue, Status = @newStatus WHERE ComplaintID = @userId"
                Dim cmd As New MySqlCommand(query, Con)

                ' Add parameters to the query
                cmd.Parameters.AddWithValue("@userId", pid)
                cmd.Parameters.AddWithValue("@newStatus", ComboBox2.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@newValue", TextBox1.Text)

                ' Execute the query
                cmd.ExecuteNonQuery()
                MessageBox.Show("Done Successfully ")



            Catch ex As Exception
                ' Handle exceptions
                MessageBox.Show("Error: " & ex.Message)

            Finally
                ' Close the connection
                Con.Close()
            End Try
            Me.Close()
        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class