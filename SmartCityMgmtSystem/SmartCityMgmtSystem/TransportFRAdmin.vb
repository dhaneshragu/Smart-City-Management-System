Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports SmartCityMgmtSystem.RideSharingMain
Public Class TransportFRAdmin
    Private Sub TransportFRAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using conn As New MySqlConnection(Globals.getdbConnectionString())
            conn.Open()
            'label6 accno,label7 toll,label9 dl, label8 bus
            Dim query As String = "SELECT * FROM admin_records"
            Using command As New MySqlCommand(query, conn)
                Using reader As MySqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Label6.Text = reader.GetInt32("account_no")
                        Label7.Text = "Rs. " & reader.GetInt32("toll_revenue")
                        Label9.Text = "Rs. " & reader.GetInt32("reg_dl_revenue")
                        Label8.Text = "Rs. " & reader.GetInt32("bus_revenue")
                    End While
                End Using
            End Using
        End Using
    End Sub
End Class