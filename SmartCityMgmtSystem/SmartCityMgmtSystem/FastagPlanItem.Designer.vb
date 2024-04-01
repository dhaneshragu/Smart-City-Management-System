<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FastagPlanItem
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnview = New System.Windows.Forms.Button()
        Me.lblvalidity = New System.Windows.Forms.Label()
        Me.lblfare = New System.Windows.Forms.Label()
        Me.lblvehtype = New System.Windows.Forms.Label()
        Me.picbox = New System.Windows.Forms.PictureBox()
        CType(Me.picbox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Comic Sans MS", 12.20339!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.DarkRed
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(159, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(169, 35)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Buy Fastag Plan"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnview
        '
        Me.btnview.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnview.BackColor = System.Drawing.Color.SaddleBrown
        Me.btnview.BackgroundImage = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_pay_301
        Me.btnview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnview.FlatAppearance.BorderSize = 0
        Me.btnview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnview.Location = New System.Drawing.Point(512, 0)
        Me.btnview.Name = "btnview"
        Me.btnview.Size = New System.Drawing.Size(60, 104)
        Me.btnview.TabIndex = 9
        Me.btnview.UseVisualStyleBackColor = False
        '
        'lblvalidity
        '
        Me.lblvalidity.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblvalidity.Font = New System.Drawing.Font("Trebuchet MS", 10.98305!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvalidity.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_calender_301
        Me.lblvalidity.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblvalidity.Location = New System.Drawing.Point(160, 67)
        Me.lblvalidity.Name = "lblvalidity"
        Me.lblvalidity.Size = New System.Drawing.Size(208, 33)
        Me.lblvalidity.TabIndex = 7
        Me.lblvalidity.Text = "        3 Months Validity"
        Me.lblvalidity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblfare
        '
        Me.lblfare.Font = New System.Drawing.Font("Trebuchet MS", 10.98305!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfare.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_cash_30
        Me.lblfare.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblfare.Location = New System.Drawing.Point(383, 67)
        Me.lblfare.Name = "lblfare"
        Me.lblfare.Size = New System.Drawing.Size(123, 33)
        Me.lblfare.TabIndex = 5
        Me.lblfare.Text = "       ₹150"
        Me.lblfare.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblvehtype
        '
        Me.lblvehtype.Font = New System.Drawing.Font("Trebuchet MS", 10.98305!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvehtype.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_steering_wheel_301
        Me.lblvehtype.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblvehtype.Location = New System.Drawing.Point(159, 32)
        Me.lblvehtype.Name = "lblvehtype"
        Me.lblvehtype.Size = New System.Drawing.Size(244, 33)
        Me.lblvehtype.TabIndex = 1
        Me.lblvehtype.Text = "       Light Four Wheelers"
        Me.lblvehtype.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picbox
        '
        Me.picbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.picbox.BackColor = System.Drawing.Color.Transparent
        Me.picbox.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.Fatstag
        Me.picbox.Location = New System.Drawing.Point(-35, 0)
        Me.picbox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picbox.Name = "picbox"
        Me.picbox.Size = New System.Drawing.Size(203, 104)
        Me.picbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picbox.TabIndex = 0
        Me.picbox.TabStop = False
        '
        'FastagPlanItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SeaShell
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnview)
        Me.Controls.Add(Me.lblvalidity)
        Me.Controls.Add(Me.lblfare)
        Me.Controls.Add(Me.lblvehtype)
        Me.Controls.Add(Me.picbox)
        Me.Font = New System.Drawing.Font("Trebuchet MS", 9.762712!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FastagPlanItem"
        Me.Size = New System.Drawing.Size(572, 104)
        CType(Me.picbox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents picbox As PictureBox
    Friend WithEvents lblvehtype As Label
    Friend WithEvents lblfare As Label
    Friend WithEvents btnview As Button
    Friend WithEvents lblvalidity As Label
    Friend WithEvents Label1 As Label
End Class
