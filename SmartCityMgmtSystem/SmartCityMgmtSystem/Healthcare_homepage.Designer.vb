<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Healthcare_homepage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Healthcare_homepage))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.hc_admin = New System.Windows.Forms.Button()
        Me.history = New System.Windows.Forms.Button()
        Me.emergency = New System.Windows.Forms.Button()
        Me.donate_blood = New System.Windows.Forms.Button()
        Me.pharmacy = New System.Windows.Forms.Button()
        Me.book_appointment = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.childformPanel = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(114, Byte), Integer))
        Me.Panel1.Controls.Add(Me.hc_admin)
        Me.Panel1.Controls.Add(Me.history)
        Me.Panel1.Controls.Add(Me.emergency)
        Me.Panel1.Controls.Add(Me.donate_blood)
        Me.Panel1.Controls.Add(Me.pharmacy)
        Me.Panel1.Controls.Add(Me.book_appointment)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 11.8209!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Panel1.Location = New System.Drawing.Point(1, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(282, 783)
        Me.Panel1.TabIndex = 8
        '
        'hc_admin
        '
        Me.hc_admin.BackColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.hc_admin.FlatAppearance.BorderSize = 0
        Me.hc_admin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.hc_admin.Font = New System.Drawing.Font("Tahoma", 12.81356!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hc_admin.ForeColor = System.Drawing.Color.White
        Me.hc_admin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.hc_admin.Location = New System.Drawing.Point(0, 644)
        Me.hc_admin.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.hc_admin.Name = "hc_admin"
        Me.hc_admin.Size = New System.Drawing.Size(282, 66)
        Me.hc_admin.TabIndex = 29
        Me.hc_admin.Text = "Admin Only"
        Me.hc_admin.UseVisualStyleBackColor = False
        Me.hc_admin.Visible = False
        '
        'history
        '
        Me.history.BackColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.history.FlatAppearance.BorderSize = 0
        Me.history.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.history.Font = New System.Drawing.Font("Tahoma", 12.81356!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.history.ForeColor = System.Drawing.Color.White
        Me.history.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.history.Location = New System.Drawing.Point(0, 570)
        Me.history.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.history.Name = "history"
        Me.history.Size = New System.Drawing.Size(282, 67)
        Me.history.TabIndex = 27
        Me.history.Text = "Appointment History"
        Me.history.UseVisualStyleBackColor = False
        '
        'emergency
        '
        Me.emergency.BackColor = System.Drawing.Color.Red
        Me.emergency.FlatAppearance.BorderSize = 0
        Me.emergency.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.emergency.Font = New System.Drawing.Font("Tahoma", 12.81356!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.emergency.ForeColor = System.Drawing.Color.White
        Me.emergency.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.emergency.Location = New System.Drawing.Point(0, 717)
        Me.emergency.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.emergency.Name = "emergency"
        Me.emergency.Size = New System.Drawing.Size(282, 67)
        Me.emergency.TabIndex = 28
        Me.emergency.Text = "Emergency"
        Me.emergency.UseVisualStyleBackColor = False
        '
        'donate_blood
        '
        Me.donate_blood.BackColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.donate_blood.FlatAppearance.BorderSize = 0
        Me.donate_blood.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.donate_blood.Font = New System.Drawing.Font("Tahoma", 12.81356!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.donate_blood.ForeColor = System.Drawing.Color.White
        Me.donate_blood.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.donate_blood.Location = New System.Drawing.Point(0, 496)
        Me.donate_blood.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.donate_blood.Name = "donate_blood"
        Me.donate_blood.Size = New System.Drawing.Size(282, 67)
        Me.donate_blood.TabIndex = 26
        Me.donate_blood.Text = "Donate Blood"
        Me.donate_blood.UseVisualStyleBackColor = False
        '
        'pharmacy
        '
        Me.pharmacy.BackColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.pharmacy.FlatAppearance.BorderSize = 0
        Me.pharmacy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.pharmacy.Font = New System.Drawing.Font("Tahoma", 12.81356!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pharmacy.ForeColor = System.Drawing.Color.White
        Me.pharmacy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.pharmacy.Location = New System.Drawing.Point(0, 422)
        Me.pharmacy.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pharmacy.Name = "pharmacy"
        Me.pharmacy.Size = New System.Drawing.Size(282, 67)
        Me.pharmacy.TabIndex = 25
        Me.pharmacy.Text = "Pharmacy"
        Me.pharmacy.UseVisualStyleBackColor = False
        '
        'book_appointment
        '
        Me.book_appointment.BackColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.book_appointment.FlatAppearance.BorderSize = 0
        Me.book_appointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.book_appointment.Font = New System.Drawing.Font("Tahoma", 12.81356!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.book_appointment.ForeColor = System.Drawing.Color.White
        Me.book_appointment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.book_appointment.Location = New System.Drawing.Point(0, 348)
        Me.book_appointment.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.book_appointment.Name = "book_appointment"
        Me.book_appointment.Size = New System.Drawing.Size(282, 67)
        Me.book_appointment.TabIndex = 24
        Me.book_appointment.Text = "Book Appointment"
        Me.book_appointment.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.final
        Me.PictureBox1.Location = New System.Drawing.Point(0, 3)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(282, 153)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(136, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(0, 159)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(282, 141)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SmartGhy" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "HealthCare" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Department"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.Label2.CausesValidation = False
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Ivory
        Me.Label2.Location = New System.Drawing.Point(291, 18)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 29)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "{Name}"
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.Button6.CausesValidation = False
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.Ivory
        Me.Button6.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_back_arrow_25
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button6.Location = New System.Drawing.Point(1389, 12)
        Me.Button6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button6.Name = "Button6"
        Me.Button6.Padding = New System.Windows.Forms.Padding(12, 0, 0, 0)
        Me.Button6.Size = New System.Drawing.Size(163, 39)
        Me.Button6.TabIndex = 11
        Me.Button6.Text = "   Home Page"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'childformPanel
        '
        Me.childformPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.childformPanel.BackgroundImage = CType(resources.GetObject("childformPanel.BackgroundImage"), System.Drawing.Image)
        Me.childformPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.childformPanel.Font = New System.Drawing.Font("Trebuchet MS", 9.762712!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.childformPanel.Location = New System.Drawing.Point(279, 66)
        Me.childformPanel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.childformPanel.Name = "childformPanel"
        Me.childformPanel.Size = New System.Drawing.Size(1276, 723)
        Me.childformPanel.TabIndex = 9
        '
        'Healthcare_homepage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1556, 784)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.childformPanel)
        Me.Name = "Healthcare_homepage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Healthcare_homepage"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button6 As Button
    Friend WithEvents childformPanel As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents hc_admin As Button
    Friend WithEvents history As Button
    Friend WithEvents emergency As Button
    Friend WithEvents donate_blood As Button
    Friend WithEvents pharmacy As Button
    Friend WithEvents book_appointment As Button
End Class
