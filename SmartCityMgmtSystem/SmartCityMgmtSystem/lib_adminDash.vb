Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class lib_adminDash

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim lib_admin_eBooks = New lib_admin_eBooks()
        lib_admin_eBooks.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim lib_adminBM = New lib_adminBM()
        lib_adminBM.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim lib_adminMT = New lib_adminMT()
        lib_adminMT.Show()
        Me.Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim lib_adminReq = New lib_adminReq()
        lib_adminReq.Show()
        Me.Close()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim lib_adminEBM = New lib_adminEBM()
        lib_adminEBM.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim lib_adminSearch = New lib_adminSearch()
        lib_adminSearch.Show()
        Me.Close()
    End Sub

    Private Sub lib_adminDash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        total_books()
        total_ebooks()
        borrowed_books()
        overdue_books()
        fine_due()
        fine_collected()
    End Sub

    Private Sub total_books()
        Dim query As String = "SELECT COALESCE(COUNT(*), 0) FROM lib_books"
        Dim Con = Globals.GetDBConnection()

        Try
            Con.Open()
            Dim cmd As New MySqlCommand(query, Con)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            tbooks.Text = count.ToString()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            Con.Close() ' Close the connection in the end
        End Try
    End Sub

    Private Sub total_ebooks()
        Dim query As String = "SELECT COALESCE(COUNT(*), 0) FROM lib_ebooks"
        Dim Con = Globals.GetDBConnection()

        Try
            Con.Open()
            Dim cmd As New MySqlCommand(query, Con)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            tebooks.Text = count.ToString()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            Con.Close() ' Close the connection in the end
        End Try
    End Sub

    Private Sub borrowed_books()
        Dim query As String = "SELECT COALESCE(COUNT(*), 0) FROM lib_borrowed_books"
        Dim Con = Globals.GetDBConnection()

        Try
            Con.Open()
            Dim cmd As New MySqlCommand(query, Con)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            bbooks.Text = count.ToString()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            Con.Close() ' Close the connection in the end
        End Try
    End Sub

    Private Sub overdue_books()
        Dim query As String = "SELECT COALESCE(COUNT(*), 0) AS TotalOverdueBooks FROM lib_borrowed_books WHERE dueDate < CURDATE();"
        Dim Con = Globals.GetDBConnection()

        Try
            Con.Open()
            Dim cmd As New MySqlCommand(query, Con)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            obooks.Text = count.ToString()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            Con.Close() ' Close the connection in the end
        End Try
    End Sub

    Private Sub fine_due()
        Dim query As String = "SELECT COALESCE(SUM(fine), 0) AS total_fine FROM lib_fine;"
        Dim Con = Globals.GetDBConnection()

        Try
            Con.Open()
            Dim cmd As New MySqlCommand(query, Con)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            fdue.Text = count.ToString()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            Con.Close() ' Close the connection in the end
        End Try
    End Sub

    Private Sub fine_collected()
        Dim query As String = "SELECT COALESCE(SUM(fine), 0) FROM lib_fine_collected;"
        Dim Con = Globals.GetDBConnection()

        Try
            Con.Open()
            Dim cmd As New MySqlCommand(query, Con)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            fcollected.Text = count.ToString()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            Con.Close() ' Close the connection in the end
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim HomePageDashboard = New HomePageDashboard()
        HomePageDashboard.Show()
        Me.Close()
    End Sub

    Private Sub childformPanel_Paint(sender As Object, e As PaintEventArgs) Handles childformPanel.Paint

    End Sub
End Class
