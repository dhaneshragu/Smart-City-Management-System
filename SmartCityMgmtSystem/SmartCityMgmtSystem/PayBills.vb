Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class PayBills
    Public Property uid As Integer = 1
    Private Sub PayBills_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckLastPaymentMonth()
    End Sub

    Private Sub CheckLastPaymentMonth()
        ' Get the last payment months for each bill type from the database
        Dim electricityLastPaymentMonth As Integer = GetLastPaymentMonth("ElectricityLastPaymentMonth")
        Dim internetLastPaymentMonth As Integer = GetLastPaymentMonth("InternetLastPaymentMonth")
        Dim waterLastPaymentMonth As Integer = GetLastPaymentMonth("WaterLastPaymentMonth")
        Dim gasLastPaymentMonth As Integer = GetLastPaymentMonth("GasLastPaymentMonth")
        Dim tvLastPaymentMonth As Integer = GetLastPaymentMonth("TVLastPaymentMonth")

        ' Compare with current month for each bill type
        If electricityLastPaymentMonth = DateTime.Now.Month Then
            Button1.Enabled = False
            Button1.Text = "Paid"
        Else
            Button1.Enabled = True
            Button1.Text = "Pay"
        End If

        If internetLastPaymentMonth = DateTime.Now.Month Then
            Button2.Enabled = False
            Button2.Text = "Paid"
        Else
            Button2.Enabled = True
            Button2.Text = "Pay"
        End If

        If waterLastPaymentMonth = DateTime.Now.Month Then
            Button3.Enabled = False
            Button3.Text = "Paid"
        Else
            Button3.Enabled = True
            Button3.Text = "Pay"
        End If
        If gasLastPaymentMonth = DateTime.Now.Month Then
            Button4.Enabled = False
            Button4.Text = "Paid"
        Else
            Button4.Enabled = True
            Button4.Text = "Pay"
        End If

        If tvLastPaymentMonth = DateTime.Now.Month Then
            Button5.Enabled = False
            Button5.Text = "Paid"
        Else
            Button5.Enabled = True
            Button5.Text = "Pay"
        End If
    End Sub

    Private Function GetLastPaymentMonth(columnName As String) As Integer

        Dim query As String = $"SELECT {columnName} FROM housebills WHERE UserID = @UserID"

        Using connection As MySqlConnection = Globals.GetDBConnection()
            Using command As MySqlCommand = New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserID", uid)
                connection.Open()

                ' Execute the SQL query and get the last payment month
                Dim lastPaymentMonthObj As Object = command.ExecuteScalar()

                If lastPaymentMonthObj IsNot Nothing AndAlso Not DBNull.Value.Equals(lastPaymentMonthObj) Then
                    ' If last payment month is found, return it
                    Return Convert.ToInt32(lastPaymentMonthObj)
                Else
                    Return 0
                End If
            End Using
        End Using
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'electricity bill pay 
        Dim pay = New PaymentGateway() With {
            .uid = uid,
            .readonly_prop = True
        }
        pay.TextBox1.Text = 10
        pay.TextBox2.Text = 700
        pay.TextBox3.Text = "Electricity Bill Payment."
        If (pay.ShowDialog() = DialogResult.OK) Then
            MessageBox.Show("Payment successful!")

            ' Store the current month as the last payment month for electricity bills in the database
            StoreLastPaymentMonth("ElectricityLastPaymentMonth", DateTime.Now.Month)
            Button1.Enabled = False
            Button1.Text = "Paid"
        Else
            MessageBox.Show("Payment failed.")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'electricity bill pay 
        Dim pay = New PaymentGateway() With {
            .uid = uid,
            .readonly_prop = True
        }
        pay.TextBox1.Text = 10
        pay.TextBox2.Text = 500
        pay.TextBox3.Text = "Internet Bill Payment."
        If (pay.ShowDialog() = DialogResult.OK) Then
            MessageBox.Show("Payment successful!")

            ' Store the current month as the last payment month for electricity bills in the database
            StoreLastPaymentMonth("InternetLastPaymentMonth", DateTime.Now.Month)
            Button2.Enabled = False
            Button2.Text = "Paid"
        Else
            MessageBox.Show("Payment failed.")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'electricity bill pay 
        Dim pay = New PaymentGateway() With {
            .uid = uid,
            .readonly_prop = True
        }
        pay.TextBox1.Text = 10
        pay.TextBox2.Text = 100
        pay.TextBox3.Text = "Water Bill Payment."
        If (pay.ShowDialog() = DialogResult.OK) Then
            MessageBox.Show("Payment successful!")

            ' Store the current month as the last payment month for electricity bills in the database
            StoreLastPaymentMonth("WaterLastPaymentMonth", DateTime.Now.Month)
            Button3.Enabled = False
            Button3.Text = "Paid"
        Else
            MessageBox.Show("Payment failed.")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'electricity bill pay 
        Dim pay = New PaymentGateway() With {
            .uid = uid,
            .readonly_prop = True
        }
        pay.TextBox1.Text = 10
        pay.TextBox2.Text = 500
        pay.TextBox3.Text = "Electricity Bill Payment."
        If (pay.ShowDialog() = DialogResult.OK) Then
            MessageBox.Show("Payment successful!")

            ' Store the current month as the last payment month for electricity bills in the database
            StoreLastPaymentMonth("GasLastPaymentMonth", DateTime.Now.Month)
            Button4.Enabled = False
            Button4.Text = "Paid"
        Else
            MessageBox.Show("Payment failed.")
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'electricity bill pay 
        Dim pay = New PaymentGateway() With {
            .uid = uid,
            .readonly_prop = True
        }
        pay.TextBox1.Text = 10
        pay.TextBox2.Text = 250
        pay.TextBox3.Text = "TV Bill Payment."
        If (pay.ShowDialog() = DialogResult.OK) Then
            MessageBox.Show("Payment successful!")

            ' Store the current month as the last payment month for electricity bills in the database
            StoreLastPaymentMonth("TVLastPaymentMonth", DateTime.Now.Month)
            Button5.Enabled = False
            Button5.Text = "Paid"
        Else
            MessageBox.Show("Payment failed.")
        End If
    End Sub
    Private Sub StoreLastPaymentMonth(columnName As String, paymentMonth As Integer)
        Try
            Dim query As String = $"INSERT INTO housebills (UserID, {columnName}) VALUES (@UserID, @PaymentMonth) ON DUPLICATE KEY UPDATE {columnName} = @PaymentMonth"
            Using connection As MySqlConnection = Globals.GetDBConnection()
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@PaymentMonth", paymentMonth)
                    command.Parameters.AddWithValue("@UserID", uid)
                    connection.Open()

                    ' Execute the SQL query to insert or update the last payment month
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while updating the HouseBills table: " & ex.Message)
        End Try
    End Sub


End Class
