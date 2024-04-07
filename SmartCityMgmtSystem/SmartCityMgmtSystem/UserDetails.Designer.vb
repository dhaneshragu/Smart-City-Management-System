<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserDetails
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
        Me.childformPanel = New System.Windows.Forms.Panel()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.childformPanel.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'childformPanel
        '
        Me.childformPanel.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.childformPanel.Controls.Add(Me.ComboBox1)
        Me.childformPanel.Controls.Add(Me.Button9)
        Me.childformPanel.Controls.Add(Me.Button3)
        Me.childformPanel.Controls.Add(Me.PictureBox2)
        Me.childformPanel.Controls.Add(Me.Label10)
        Me.childformPanel.Controls.Add(Me.DateTimePicker1)
        Me.childformPanel.Controls.Add(Me.Label9)
        Me.childformPanel.Controls.Add(Me.TextBox7)
        Me.childformPanel.Controls.Add(Me.Label8)
        Me.childformPanel.Controls.Add(Me.TextBox5)
        Me.childformPanel.Controls.Add(Me.Label6)
        Me.childformPanel.Controls.Add(Me.TextBox4)
        Me.childformPanel.Controls.Add(Me.Label5)
        Me.childformPanel.Controls.Add(Me.Label4)
        Me.childformPanel.Controls.Add(Me.TextBox2)
        Me.childformPanel.Controls.Add(Me.Label2)
        Me.childformPanel.Controls.Add(Me.TextBox1)
        Me.childformPanel.Controls.Add(Me.Label1)
        Me.childformPanel.Font = New System.Drawing.Font("Trebuchet MS", 9.762712!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.childformPanel.Location = New System.Drawing.Point(-1, 131)
        Me.childformPanel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.childformPanel.Name = "childformPanel"
        Me.childformPanel.Size = New System.Drawing.Size(1555, 651)
        Me.childformPanel.TabIndex = 1
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.Color.SeaShell
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Male", "Female", "Other"})
        Me.ComboBox1.Location = New System.Drawing.Point(366, 216)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(352, 30)
        Me.ComboBox1.TabIndex = 25
        '
        'Button9
        '
        Me.Button9.BackColor = System.Drawing.Color.Goldenrod
        Me.Button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button9.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button9.Location = New System.Drawing.Point(715, 570)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(178, 40)
        Me.Button9.TabIndex = 24
        Me.Button9.Text = "Save"
        Me.Button9.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.Maroon
        Me.Button3.Location = New System.Drawing.Point(1088, 363)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(189, 45)
        Me.Button3.TabIndex = 18
        Me.Button3.Text = "Upload Picture"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.student_default
        Me.PictureBox2.Location = New System.Drawing.Point(998, 30)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(380, 327)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 17
        Me.PictureBox2.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Trebuchet MS", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(823, 36)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(155, 28)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Profile Photo"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CalendarMonthBackground = System.Drawing.Color.Linen
        Me.DateTimePicker1.CalendarTitleForeColor = System.Drawing.Color.AntiqueWhite
        Me.DateTimePicker1.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Location = New System.Drawing.Point(366, 94)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(352, 30)
        Me.DateTimePicker1.TabIndex = 15
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Trebuchet MS", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(163, 96)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(168, 28)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Date of Birth *"
        '
        'TextBox7
        '
        Me.TextBox7.BackColor = System.Drawing.Color.SeaShell
        Me.TextBox7.Location = New System.Drawing.Point(366, 450)
        Me.TextBox7.Multiline = True
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(352, 34)
        Me.TextBox7.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Trebuchet MS", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(163, 456)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(134, 28)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "Occupation"
        '
        'TextBox5
        '
        Me.TextBox5.BackColor = System.Drawing.Color.SeaShell
        Me.TextBox5.Location = New System.Drawing.Point(366, 336)
        Me.TextBox5.Multiline = True
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(352, 88)
        Me.TextBox5.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Trebuchet MS", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(163, 336)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(113, 28)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Address *"
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.SeaShell
        Me.TextBox4.Location = New System.Drawing.Point(366, 270)
        Me.TextBox4.Multiline = True
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(352, 34)
        Me.TextBox4.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Trebuchet MS", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(163, 276)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(189, 28)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Phone Number *"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Trebuchet MS", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(163, 216)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 28)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Gender *"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.SeaShell
        Me.TextBox2.Location = New System.Drawing.Point(366, 150)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(352, 34)
        Me.TextBox2.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Trebuchet MS", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(163, 156)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 28)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Age"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.SeaShell
        Me.TextBox1.Location = New System.Drawing.Point(366, 36)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(352, 34)
        Me.TextBox1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Trebuchet MS", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(163, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Full Name *"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.CausesValidation = False
        Me.Label3.Font = New System.Drawing.Font("Agency FB", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Ivory
        Me.Label3.Location = New System.Drawing.Point(707, 9)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(155, 39)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Enter Details"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.SMART_CITY___GUWAHATI
        Me.PictureBox1.Location = New System.Drawing.Point(-1, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(282, 132)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'UserDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkGoldenrod
        Me.ClientSize = New System.Drawing.Size(1552, 779)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.childformPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.Name = "UserDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transportation"
        Me.childformPanel.ResumeLayout(False)
        Me.childformPanel.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents childformPanel As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label10 As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Button3 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents TextBox2 As TextBox
End Class
