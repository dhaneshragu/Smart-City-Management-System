Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreenCitizenCoC

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"
    Public Property innerPanel As Panel
    Dim electionInnerScreen1 As ElectionInnerScreen1 = Nothing
    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetCoC()
    End Sub

    Private Sub GetCoC()
        Dim Con = Globals.GetDBConnection()
        Dim cmd As MySqlCommand
        Dim COC As String ' Variable to store the number of rows in election_time table
        Dim electionId As Integer = 0

        Try
            Con.Open()
            ' Execute query to count rows in election_time table
            cmd = New MySqlCommand("SELECT COUNT(*) FROM election_time;", Con)
            electionId = Convert.ToInt32(cmd.ExecuteScalar())


            If electionId = 0 Then
                MessageBox.Show("No elections have been scheduled yet.")
                RichTextBox1.Text = "Code of Conduct will be updated when an election is scheduled."
                Exit Sub
            End If

            cmd = New MySqlCommand("SELECT code_of_conduct_text FROM code_of_conduct where election_id = @election_id;", Con)
            cmd.Parameters.AddWithValue("@election_id", electionId)
            COC = cmd.ExecuteScalar().ToString
            RichTextBox1.Text = COC
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close the connection
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        electionInnerScreen1?.Dispose()
        electionInnerScreen1 = New ElectionInnerScreen1 With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreen1)
    End Sub
End Class