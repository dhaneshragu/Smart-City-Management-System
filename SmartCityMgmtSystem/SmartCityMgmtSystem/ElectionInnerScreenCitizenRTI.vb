Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreenCitizenRTI

    Public Property uid As Integer = 8
    Public Property u_name As String = "admin"

    Public Property innerPanel As Panel
    Dim electionInnerScreenCitizenRTIPA As ElectionInnerScreenCitizenRTIPA = Nothing

    Dim ministryToId As New Dictionary(Of String, Integer)

    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ministryToId.Add("Employment", 1)
        ministryToId.Add("Education", 2)
        ministryToId.Add("Health", 3)
        ministryToId.Add("Transport", 4)
        ministryToId.Add("Culture", 5)
        ministryToId.Add("Power", 6)
        ministryToId.Add("Finance", 7)
        ministryToId.Add("Broadcasting", 8)
        ministryToId.Add("IT", 9)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        electionInnerScreenCitizenRTIPA?.Dispose()
        electionInnerScreenCitizenRTIPA = New ElectionInnerScreenCitizenRTIPA With {
            .innerPanel = innerPanel,
            .uid = uid,
            .u_name = u_name
        }
        Globals.viewChildForm(innerPanel, electionInnerScreenCitizenRTIPA)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim selectedValue As Object = ComboBox1.SelectedItem

        If selectedValue IsNot Nothing Then
            Dim selectedText As String = selectedValue.ToString()
        Else
            MessageBox.Show("Select the ministry and try again.")
            Exit Sub
        End If

        If TextBox1.Text = "" Then
            MessageBox.Show("Please write your query.")
            Exit Sub
        End If

        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        'Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
            ' Execute query to count rows in election_time table
            cmd = New MySqlCommand("SELECT COUNT(*) FROM rti_queries_table;", Con)
            Dim electionTimeRowCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Dim queryid = electionTimeRowCount + 1

            Dim insertQuery As String = "INSERT INTO rti_queries_table(query_id, citizen_uid, ministry, 
                                    query, status) VALUES(" & queryid & "," & uid & "," & ministryToId(selectedValue.ToString) & ", """ & TextBox1.Text & """, ""Pending"" )"

            Dim inserted As Boolean = Globals.ExecuteInsertQuery(insertQuery)

            If inserted Then
                MessageBox.Show("Successful")
            Else
                MessageBox.Show("Failed to insert a row")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try



    End Sub
End Class