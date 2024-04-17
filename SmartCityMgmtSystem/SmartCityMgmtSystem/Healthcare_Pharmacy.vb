Imports System.Data.SqlClient
Imports Google.Protobuf.WellKnownTypes
Imports MySql.Data.MySqlClient
Public Class Healthcare_Pharmacy

    Public Property uid As Integer = 130
    Private Sub LoadandBindDataGridView()
        'Get connection from globals
        Dim Con = Globals.GetDBConnection()
        Dim reader As MySqlDataReader
        Dim cmd As MySqlCommand

        Try
            Con.Open()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmd = New MySqlCommand("SELECT pharmacydb.pharmacy_name, pharmacydb.location, pharmacydb.pharmacy_id, medicine.medicine_id FROM pharmacydb INNER JOIN medicine ON pharmacydb.pharmacy_id=medicine.pharmacy_id WHERE medicine.medicine_name LIKE CONCAT('%', @Value, '%'); ", Con)
        cmd.Parameters.AddWithValue("@Value", RichTextBox1.Text)
        reader = cmd.ExecuteReader()
        Dim i As Integer = 0
        'Fill the DataTable with data from the SQL tables
        If reader.HasRows Then
            While reader.Read()
                Dim Value As String = reader("pharmacy_name").ToString()
                Dim Value2 As String = reader("pharmacy_id").ToString()
                Dim Value1 As String = reader("location").ToString()
                Dim Value3 As String = reader("medicine_id").ToString()
                Dim label As New Windows.Forms.Label()
                label.BackColor = Color.LightSkyBlue
                label.Width = 700
                label.Height = 100
                label.Location = New Point(230, 50 + i)

                ' Create a label for the hospital name
                Dim lblHospital As New Windows.Forms.Label()
                lblHospital.Text = Value
                lblHospital.Size = New Size(400, 80)
                lblHospital.Font = New Font("Arial", 24, FontStyle.Bold)
                lblHospital.Location = New Point(10, 30)
                label.Controls.Add(lblHospital)

                ' Create a label for the location
                Dim lblLocation As New Windows.Forms.Label()
                lblLocation.Text = Value1
                lblLocation.Size = New Size(300, 80)
                lblLocation.Font = New Font("Times New Roman", 16, FontStyle.Regular)
                lblLocation.Location = New Point(410, 40)
                label.Controls.Add(lblLocation)

                Dim lblBuy As New Windows.Forms.Button()

                lblBuy.Text = "BUY"
                lblBuy.BackColor = Color.FromArgb(0, 180, 0)
                lblBuy.Width = 100
                lblBuy.Height = 100
                lblBuy.Location = New Point(940, 50 + i)
                lblBuy.Name = Value2 & "_" & Value3
                AddHandler lblBuy.Click, AddressOf Buy_Click
                i += 120
                Panel1.Controls.Add(lblBuy)
                ' Add the button to the form
                Panel1.Controls.Add(label)
                'button.Text = Value & Environment.NewLine & Value1

            End While
        Else
            MessageBox.Show(RichTextBox1.Text & " not available")
        End If
        Con.Close()
    End Sub

    Private Sub Buy_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim pharmacy_Button As Button = DirectCast(sender, Button)
        Dim parts() As String = pharmacy_Button.Name.Split("_"c)
        Dim medicine_Id As Integer = Integer.Parse(parts(1))
        Dim pharmacy_Id As Integer = Integer.Parse(parts(0))

        Dim medicine_buy = New medicine_buy() With {
            .medicine_Id = medicine_Id,
            .uid = uid
        }
        If (medicine_buy.ShowDialog() = DialogResult.OK) Then
            MessageBox.Show("opened")
            Me.Close()
        Else

        End If

    End Sub

    Private Sub Healthcare_Pharmacy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
    Sub ClearPanel(panel As Panel)
        panel.Controls.Clear()
    End Sub
    Private Sub d1_Click(sender As Object, e As EventArgs) Handles d1.Click
        ClearPanel(Panel1)
        LoadandBindDataGridView()
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class