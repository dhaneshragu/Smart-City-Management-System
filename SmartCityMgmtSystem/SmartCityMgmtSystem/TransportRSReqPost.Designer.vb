<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TransportRSReqPost
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components1 IsNot Nothing Then
                components1.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components1 As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblfpp = New System.Windows.Forms.Label()
        Me.btnactionreject = New System.Windows.Forms.Button()
        Me.btnactionaccept = New System.Windows.Forms.Button()
        Me.lbldlid = New System.Windows.Forms.Label()
        Me.lblvid = New System.Windows.Forms.Label()
        Me.lbldt = New System.Windows.Forms.Label()
        Me.lblcapacity = New System.Windows.Forms.Label()
        Me.lblto = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblfrom = New System.Windows.Forms.Label()
        Me.lbldname = New System.Windows.Forms.Label()
        Me.picbox = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.picbox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblfpp
        '
        Me.lblfpp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblfpp.Font = New System.Drawing.Font("Trebuchet MS", 10.98305!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfpp.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_rupee_30
        Me.lblfpp.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblfpp.Location = New System.Drawing.Point(362, 93)
        Me.lblfpp.Name = "lblfpp"
        Me.lblfpp.Size = New System.Drawing.Size(198, 33)
        Me.lblfpp.TabIndex = 15
        Me.lblfpp.Text = "     {fare per person}"
        Me.lblfpp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnactionreject
        '
        Me.btnactionreject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnactionreject.BackColor = System.Drawing.Color.OrangeRed
        Me.btnactionreject.BackgroundImage = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_cross_30
        Me.btnactionreject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnactionreject.FlatAppearance.BorderSize = 0
        Me.btnactionreject.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnactionreject.Location = New System.Drawing.Point(730, 0)
        Me.btnactionreject.Name = "btnactionreject"
        Me.btnactionreject.Size = New System.Drawing.Size(70, 220)
        Me.btnactionreject.TabIndex = 14
        Me.btnactionreject.UseVisualStyleBackColor = False
        '
        'btnactionaccept
        '
        Me.btnactionaccept.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnactionaccept.BackColor = System.Drawing.Color.Green
        Me.btnactionaccept.BackgroundImage = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_tick_30
        Me.btnactionaccept.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnactionaccept.FlatAppearance.BorderSize = 0
        Me.btnactionaccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnactionaccept.Location = New System.Drawing.Point(666, 0)
        Me.btnactionaccept.Name = "btnactionaccept"
        Me.btnactionaccept.Size = New System.Drawing.Size(70, 220)
        Me.btnactionaccept.TabIndex = 13
        Me.btnactionaccept.UseVisualStyleBackColor = False
        '
        'lbldlid
        '
        Me.lbldlid.Font = New System.Drawing.Font("Trebuchet MS", 10.98305!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldlid.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_driving_license_501
        Me.lbldlid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbldlid.Location = New System.Drawing.Point(117, 51)
        Me.lbldlid.Name = "lbldlid"
        Me.lbldlid.Size = New System.Drawing.Size(227, 33)
        Me.lbldlid.TabIndex = 12
        Me.lbldlid.Text = "           {DL ID} "
        Me.lbldlid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblvid
        '
        Me.lblvid.Font = New System.Drawing.Font("Trebuchet MS", 10.98305!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvid.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_vehicle_48
        Me.lblvid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblvid.Location = New System.Drawing.Point(362, 8)
        Me.lblvid.Name = "lblvid"
        Me.lblvid.Size = New System.Drawing.Size(230, 33)
        Me.lblvid.TabIndex = 11
        Me.lblvid.Text = "          {Vehicle ID} "
        Me.lblvid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbldt
        '
        Me.lbldt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbldt.Font = New System.Drawing.Font("Trebuchet MS", 10.98305!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldt.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_calender_301
        Me.lbldt.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lbldt.Location = New System.Drawing.Point(117, 93)
        Me.lbldt.Name = "lbldt"
        Me.lbldt.Size = New System.Drawing.Size(227, 33)
        Me.lbldt.TabIndex = 7
        Me.lbldt.Text = "        {Date and Time}"
        Me.lbldt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblcapacity
        '
        Me.lblcapacity.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblcapacity.Font = New System.Drawing.Font("Trebuchet MS", 10.98305!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcapacity.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_group_30
        Me.lblcapacity.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblcapacity.Location = New System.Drawing.Point(362, 51)
        Me.lblcapacity.Name = "lblcapacity"
        Me.lblcapacity.Size = New System.Drawing.Size(158, 33)
        Me.lblcapacity.TabIndex = 6
        Me.lblcapacity.Text = "       {Capacity}"
        Me.lblcapacity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblto
        '
        Me.lblto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblto.Font = New System.Drawing.Font("Trebuchet MS", 10.98305!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblto.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_location_30
        Me.lblto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblto.Location = New System.Drawing.Point(405, 135)
        Me.lblto.Name = "lblto"
        Me.lblto.Size = New System.Drawing.Size(246, 33)
        Me.lblto.TabIndex = 4
        Me.lblto.Text = "      {To Place} "
        Me.lblto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_arrow_30
        Me.Label3.Location = New System.Drawing.Point(350, 135)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 33)
        Me.Label3.TabIndex = 3
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'lblfrom
        '
        Me.lblfrom.Font = New System.Drawing.Font("Trebuchet MS", 10.98305!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfrom.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_location_30
        Me.lblfrom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblfrom.Location = New System.Drawing.Point(117, 135)
        Me.lblfrom.Name = "lblfrom"
        Me.lblfrom.Size = New System.Drawing.Size(227, 33)
        Me.lblfrom.TabIndex = 2
        Me.lblfrom.Text = "      {From Place} "
        Me.lblfrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbldname
        '
        Me.lbldname.Font = New System.Drawing.Font("Trebuchet MS", 10.98305!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldname.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_steering_wheel_30
        Me.lbldname.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbldname.Location = New System.Drawing.Point(117, 8)
        Me.lbldname.Name = "lbldname"
        Me.lbldname.Size = New System.Drawing.Size(227, 33)
        Me.lbldname.TabIndex = 1
        Me.lbldname.Text = "       {Driver Name} "
        Me.lbldname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picbox
        '
        Me.picbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.picbox.BackColor = System.Drawing.Color.Cornsilk
        Me.picbox.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_driver_48
        Me.picbox.Location = New System.Drawing.Point(9, 8)
        Me.picbox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picbox.Name = "picbox"
        Me.picbox.Size = New System.Drawing.Size(102, 203)
        Me.picbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picbox.TabIndex = 0
        Me.picbox.TabStop = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Trebuchet MS", 10.98305!)
        Me.Button1.Location = New System.Drawing.Point(666, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(134, 220)
        Me.Button1.TabIndex = 16
        Me.Button1.Text = "Request Approved"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.OrangeRed
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Trebuchet MS", 10.98305!)
        Me.Button2.Location = New System.Drawing.Point(666, -1)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(134, 220)
        Me.Button2.TabIndex = 17
        Me.Button2.Text = "Request Rejected"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'TransportRSReqPost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Controls.Add(Me.lblfpp)
        Me.Controls.Add(Me.lbldlid)
        Me.Controls.Add(Me.lblvid)
        Me.Controls.Add(Me.lbldt)
        Me.Controls.Add(Me.lblcapacity)
        Me.Controls.Add(Me.lblto)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblfrom)
        Me.Controls.Add(Me.lbldname)
        Me.Controls.Add(Me.picbox)
        Me.Controls.Add(Me.btnactionaccept)
        Me.Controls.Add(Me.btnactionreject)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Font = New System.Drawing.Font("Trebuchet MS", 9.762712!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "TransportRSReqPost"
        Me.Size = New System.Drawing.Size(800, 220)
        CType(Me.picbox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents picbox As PictureBox
    Friend WithEvents lbldname As Label
    Friend WithEvents lblfrom As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblto As Label
    Friend WithEvents lblcapacity As Label
    Friend WithEvents lbldt As Label
    Friend WithEvents lblvid As Label
    Friend WithEvents lbldlid As Label
    Friend WithEvents btnactionaccept As Button
    Friend WithEvents btnactionreject As Button
    Friend WithEvents lblfpp As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
