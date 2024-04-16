Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Service_Leave
    Public Property uid As Integer = 1
    Public Property u_name As String = "Ashish Bharti"
    Private Sub TransportationInnerScreen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim userID As Integer
        Dim department As String
        Dim description As String
        Dim charge As Double

        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                Dim sql As String = "SELECT user_id AS ID,
                                    department AS dept, 
                                    service_desc AS description, 
                                    service_charge AS charge
                                    FROM service_desc 
                                    WHERE user_id = @serviceID;"

                Using cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@serviceID", uid)

                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)

                    If dataTable.Rows.Count > 0 Then
                        ' Assuming only one row will be returned, you can access the first row
                        Dim row As DataRow = dataTable.Rows(0)

                        ' Assign values to variables
                        userID = Convert.ToInt32(row("ID"))
                        department = Convert.ToString(row("dept"))
                        description = Convert.ToString(row("description"))
                        charge = Convert.ToDouble(row("charge"))

                        Dim currentDate As Date = Date.Now
                        Dim sevenDaysFromNow As Date = Date.Now.AddDays(7)

                        While currentDate < sevenDaysFromNow
                            Dim dayOfWeek As String = currentDate.ToString("dddd") ' Get the day of the week
                            DataGridView1.Rows.Add(userID, description, department, charge, dayOfWeek)
                            currentDate = currentDate.AddDays(1)
                        End While

                    Else
                        MessageBox.Show("No records found for user ID: " & uid)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
        DataGridView1.AllowUserToAddRows = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())
                con.Open()

                ' List to store the days that have been selected
                Dim selectedDays As New List(Of String)

                ' Loop through DataGridView1 to check if the checkbox is checked and store selected days
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If Convert.ToBoolean(row.Cells(5).Value) Then ' Check if the checkbox is checked
                        Dim selectedDayOfWeek As String = Convert.ToString(row.Cells(4).Value) ' Get the day of the week
                        selectedDays.Add(selectedDayOfWeek)
                        Dim updateSql As String = $"UPDATE service_leave SET {selectedDayOfWeek} = TRUE WHERE user_id = @selectedUserID"

                        Using cmd As New MySqlCommand(updateSql, con)
                            cmd.Parameters.AddWithValue("@selectedUserID", uid)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If
                Next

                ' If no days selected, notify the user
                If selectedDays.Count = 0 Then
                    MessageBox.Show("Please select at least one day.")
                Else
                    ' Update the service_leave table for the selected days
                    Dim allDaysOfWeek As New List(Of String) From {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
                    Dim nonSelectedDays = allDaysOfWeek.Except(selectedDays)

                    For Each nonSelectedDay As String In nonSelectedDays
                        Dim updateSql As String = $"UPDATE service_leave SET {nonSelectedDay} = FALSE WHERE user_id = @selectedUserID"

                        Using cmd As New MySqlCommand(updateSql, con)
                            cmd.Parameters.AddWithValue("@selectedUserID", uid)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        row.Cells(5).Value = False
                    Next
                    MessageBox.Show("Service leave days updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub
End Class