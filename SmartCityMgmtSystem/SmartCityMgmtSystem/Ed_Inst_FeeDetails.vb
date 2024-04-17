Imports System.Data.SqlClient
Public Class Ed_Inst_FeeDetails
    Dim inst_handler As New Ed_Institute_Handler()
    Public Property ToPay As Ed_Institute_Handler.InstituteFeePaid
    Private Sub Ed_Stud_Coursera_Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Rows.Clear()
        Dim feeRecs As Ed_Institute_Handler.InstituteFeePaid() = inst_handler.GetInstituteFeesRecords(Ed_GlobalDashboard.userID)


        For Each fee As Ed_Institute_Handler.InstituteFeePaid In feeRecs

            Dim ClassSem As String
            If fee.Semester = 0 Then
                ' If semester is 0, set ClassSem to "Class {class number}"
                ClassSem = "Class " & fee.ClassNum.ToString()
            Else
                ' If semester is not 0, set ClassSem to "Semester {semester}"
                ClassSem = "Semester " & fee.Semester.ToString()
            End If
            DataGridView1.Rows.Add(ClassSem, fee.Year, fee.Fee, fee.PaidOn.Date.ToString("yyyy-MM-dd"), "View")
        Next
        Dim currentYear As Integer = DateTime.Now.Year
        Dim paycheckas As Boolean = feeRecs.Any(Function(fee) fee.ClassNum = Ed_GlobalDashboard.Ed_Profile.Ed_Class) AndAlso feeRecs.Any(Function(fee) fee.Semester = Ed_GlobalDashboard.Ed_Profile.Ed_Sem)
        If Not paycheckas Then
            Dim fee As Ed_Institute_Handler.InstituteFeePaid = inst_handler.GetCurrPayDetails(Ed_GlobalDashboard.userID)

            Dim ClassSem As String
            If fee.Semester = 0 Then
                ' If semester is 0, set ClassSem to "Class {class number}"
                ClassSem = "Class " & fee.ClassNum.ToString()
            Else
                ' If semester is not 0, set ClassSem to "Semester {semester}"
                ClassSem = "Semester " & fee.Semester.ToString()
            End If
            DataGridView1.Rows.Add(ClassSem, fee.Year, fee.Fee, "NA", "Pay")
            ToPay = fee
        End If


    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Check if the clicked cell is valid and is a button cell
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 AndAlso TypeOf DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex) Is DataGridViewButtonCell Then
            ' Check if the column index corresponds to the "Action" column
            Dim columnIndex As Integer = DataGridView1.Columns("Column6").Index
            If columnIndex >= 0 AndAlso e.ColumnIndex = columnIndex Then
                ' Get the value of the clicked cell
                Dim action As String = DataGridView1.Rows(e.RowIndex).Cells(columnIndex).Value.ToString()

                ' Open respective forms based on the action value
                If action = "Pay" Then
                    ' Open the form for Pay action
                    Dim pay = New PaymentGateway() With {
                    .uid = Ed_GlobalDashboard.Ed_Profile.Ed_User_ID
                }
                    pay.readonly_prop = True
                    pay.TextBox2.Text = ToPay.Fee
                    pay.TextBox3.Text = "Yearly Fees"
                    pay.TextBox1.Text = "3"
                    If (pay.ShowDialog() = DialogResult.OK) Then
                        Dim currentDate As Date = Date.Now

                        ' Call the InsertStudentCourseRecord function
                        ToPay.PaidOn = DateTime.Now.Date
                        ToPay.Year = DateTime.Now.Year
                        inst_handler.PayFee(ToPay)
                        Ed_Stud_Coursera_Home_Load(sender, e)

                    End If
                ElseIf action = "View" Then
                    ' Open the form for View action
                    Dim viewForm As New Ed_ViewPaymentPopup()
                    viewForm.TextBox1.Text = Ed_GlobalDashboard.userID.ToString()
                    viewForm.TextBox2.Text = Ed_GlobalDashboard.userName.ToString()
                    viewForm.TextBox3.Text = "Education Ministry"
                    viewForm.TextBox6.Text = "Fee Payment"
                    viewForm.TextBox7.Text = DataGridView1.Rows(e.RowIndex).Cells(DataGridView1.Columns("Column3").Index).Value.ToString()
                    viewForm.TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(DataGridView1.Columns("Column4").Index).Value.ToString()
                    viewForm.ShowDialog() ' Show as dialog if needed
                End If
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Label6_Click_1(sender As Object, e As EventArgs)

    End Sub

End Class