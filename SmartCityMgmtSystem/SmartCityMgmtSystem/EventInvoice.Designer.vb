<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EventInvoice
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TRANSACTION_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DAYS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AMOUNT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.customerDescription = New System.Windows.Forms.Label()
        Me.vendorAddress = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.bank = New System.Windows.Forms.Label()
        Me.account = New System.Windows.Forms.Label()
        Me.total = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 28.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(223, 54)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "INVOICE"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.Label2.Location = New System.Drawing.Point(66, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 29)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Billed To"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(66, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 29)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Pay To"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(66, 242)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(191, 29)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Account Number"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(66, 212)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 29)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Bank"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(202, 319)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(0, 16)
        Me.Label8.TabIndex = 7
        '
        'DataGridView1
        '
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TRANSACTION_ID, Me.RATE, Me.DAYS, Me.AMOUNT})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.Info
        Me.DataGridView1.Location = New System.Drawing.Point(39, 273)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView1.Size = New System.Drawing.Size(503, 110)
        Me.DataGridView1.TabIndex = 8
        '
        'TRANSACTION_ID
        '
        Me.TRANSACTION_ID.HeaderText = "TRANSACTION ID"
        Me.TRANSACTION_ID.MinimumWidth = 6
        Me.TRANSACTION_ID.Name = "TRANSACTION_ID"
        Me.TRANSACTION_ID.Width = 125
        '
        'RATE
        '
        Me.RATE.HeaderText = "RATE"
        Me.RATE.MinimumWidth = 6
        Me.RATE.Name = "RATE"
        Me.RATE.Width = 125
        '
        'DAYS
        '
        Me.DAYS.HeaderText = "DAYS"
        Me.DAYS.MinimumWidth = 6
        Me.DAYS.Name = "DAYS"
        Me.DAYS.Width = 125
        '
        'AMOUNT
        '
        Me.AMOUNT.HeaderText = "AMOUNT"
        Me.AMOUNT.MinimumWidth = 6
        Me.AMOUNT.Name = "AMOUNT"
        Me.AMOUNT.Width = 125
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(351, 385)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(125, 36)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "TOTAL "
        '
        'customerDescription
        '
        Me.customerDescription.AutoSize = True
        Me.customerDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.customerDescription.Location = New System.Drawing.Point(202, 72)
        Me.customerDescription.Name = "customerDescription"
        Me.customerDescription.Size = New System.Drawing.Size(190, 25)
        Me.customerDescription.TabIndex = 12
        Me.customerDescription.Text = "432790,Customer_1"
        '
        'vendorAddress
        '
        Me.vendorAddress.AutoSize = True
        Me.vendorAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vendorAddress.Location = New System.Drawing.Point(202, 110)
        Me.vendorAddress.Name = "vendorAddress"
        Me.vendorAddress.Size = New System.Drawing.Size(180, 75)
        Me.vendorAddress.TabIndex = 13
        Me.vendorAddress.Text = "1235890,Vendor_1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Bharalumukh," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Guwahati-781009"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(202, 150)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(0, 25)
        Me.Label12.TabIndex = 14
        '
        'bank
        '
        Me.bank.AutoSize = True
        Me.bank.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bank.Location = New System.Drawing.Point(202, 212)
        Me.bank.Name = "bank"
        Me.bank.Size = New System.Drawing.Size(118, 25)
        Me.bank.TabIndex = 15
        Me.bank.Text = "ABCD Bank"
        '
        'account
        '
        Me.account.AutoSize = True
        Me.account.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.account.Location = New System.Drawing.Point(202, 244)
        Me.account.Name = "account"
        Me.account.Size = New System.Drawing.Size(126, 25)
        Me.account.TabIndex = 16
        Me.account.Text = "xxx-xxx-xxxx"
        '
        'total
        '
        Me.total.AutoSize = True
        Me.total.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.total.Location = New System.Drawing.Point(470, 385)
        Me.total.Name = "total"
        Me.total.Size = New System.Drawing.Size(105, 36)
        Me.total.TabIndex = 19
        Me.total.Text = "₹4234"
        '
        'EventInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(582, 442)
        Me.Controls.Add(Me.total)
        Me.Controls.Add(Me.account)
        Me.Controls.Add(Me.bank)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.vendorAddress)
        Me.Controls.Add(Me.customerDescription)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "EventInvoice"
        Me.Text = "Invoice"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label10 As Label
    Friend WithEvents customerDescription As Label
    Friend WithEvents vendorAddress As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents bank As Label
    Friend WithEvents account As Label
    Friend WithEvents total As Label
    Friend WithEvents TRANSACTION_ID As DataGridViewTextBoxColumn
    Friend WithEvents RATE As DataGridViewTextBoxColumn
    Friend WithEvents DAYS As DataGridViewTextBoxColumn
    Friend WithEvents AMOUNT As DataGridViewTextBoxColumn
End Class
