Imports Google.Protobuf.WellKnownTypes
Imports MySql.Data.MySqlClient

Public Class medicine_buy

    Public Property medicine_Id As Integer = 1
    Public Property pharmacy_Id As Integer = 1
    Public Property uid As Integer = 4
    Public Property patient_id As Integer = 130


    Private Sub Mbuy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT pharmacydb.pharmacy_id, pharmacydb.pharmacy_name, medicine.price, medicine.medicine_name FROM pharmacydb INNER JOIN medicine ON pharmacydb.pharmacy_id=medicine.pharmacy_id WHERE medicine.medicine_id = @Value; ", Con)
        cmd.Parameters.AddWithValue("@Value", medicine_Id)
        reader = cmd.ExecuteReader()
        Dim i As Integer = 0
        'Fill the DataTable with data from the SQL tables
        If reader.HasRows Then
            While reader.Read()
                pharmacy_Id = reader("pharmacy_id").ToString()
                p_name.Text = reader("pharmacy_name").ToString()
                m_name.Text = reader("medicine_name").ToString()
                cost.Text = reader("price").ToString()

            End While
        End If
        Con.Close()

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT * FROM patient WHERE patient_id = @uid", Con)
        cmd.Parameters.AddWithValue("@uid", uid)
        reader = cmd.ExecuteReader()

        'Fill the DataTable with data from the SQL table
        If reader.HasRows Then
            While reader.Read()
                Dim Value As Integer = reader("uid").ToString()
                patient_id = Value
            End While
        End If
        Con.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Buy_Click(sender As Object, e As EventArgs) Handles Buy.Click
        Dim pay = New PaymentGateway() With {
            .uid = patient_id,
            .readonly_prop = True
        }
        pay.TextBox1.Text = 4
        pay.TextBox2.Text = total_cost.Text
        pay.TextBox3.Text = "medicine cost"
        If (pay.ShowDialog() = DialogResult.OK) Then
            Dim cmd As New MySqlCommand
            Using con As MySqlConnection = New MySqlConnection(Globals.getdbConnectionString())

                Dim apply_date As DateTime = DateTime.Now

                Dim stmnt As String = "INSERT INTO pharmacybill VALUES (NULL, @pharmacy_id, @medicine_id , @price, @time, @patient_id, @quantity)"

                cmd = New MySqlCommand(stmnt, con)
                cmd.Parameters.AddWithValue("@pharmacy_id", pharmacy_Id)
                cmd.Parameters.AddWithValue("@patient_ID", uid)
                cmd.Parameters.AddWithValue("@medicine_id", medicine_Id)
                cmd.Parameters.AddWithValue("@price", total_cost.Text)
                cmd.Parameters.AddWithValue("@quantity", TextBox1.Text)
                cmd.Parameters.AddWithValue("@time", Now())

                Try
                    con.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
                con.Close()
            End Using
            MessageBox.Show("Payment successful!")
            Me.Close()
        Else
            MessageBox.Show("Payment failed.")
            Me.Close()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim number As Integer
        Dim cost1 As Integer = cost.Text
        If Integer.TryParse(TextBox1.Text, number) AndAlso TextBox1.Text > 0 Then
            total_cost.Text = cost1 * number
        Else
            total_cost.Text = 0
        End If
    End Sub
End Class