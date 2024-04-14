<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Ed_Teacher_AddSummary
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
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.btnFont = New System.Windows.Forms.Button()
        Me.btnFontColor = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(147, 208)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(997, 411)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = ""
        '
        'btnFont
        '
        Me.btnFont.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(114, Byte), Integer))
        Me.btnFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFont.Font = New System.Drawing.Font("Cascadia Mono", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFont.ForeColor = System.Drawing.Color.White
        Me.btnFont.Location = New System.Drawing.Point(397, 159)
        Me.btnFont.Name = "btnFont"
        Me.btnFont.Size = New System.Drawing.Size(173, 43)
        Me.btnFont.TabIndex = 47
        Me.btnFont.Text = "Select Font"
        Me.btnFont.UseVisualStyleBackColor = False
        '
        'btnFontColor
        '
        Me.btnFontColor.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(114, Byte), Integer))
        Me.btnFontColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFontColor.Font = New System.Drawing.Font("Cascadia Mono", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFontColor.ForeColor = System.Drawing.Color.White
        Me.btnFontColor.Location = New System.Drawing.Point(753, 159)
        Me.btnFontColor.Name = "btnFontColor"
        Me.btnFontColor.Size = New System.Drawing.Size(173, 43)
        Me.btnFontColor.TabIndex = 48
        Me.btnFontColor.Text = "Select Colour"
        Me.btnFontColor.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(114, Byte), Integer))
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Cascadia Mono", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(543, 655)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(173, 43)
        Me.Button2.TabIndex = 51
        Me.Button2.Text = "DONE"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(114, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(392, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(542, 43)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "Enter Syllabus"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Ed_Teacher_AddSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(13.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1308, 736)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnFontColor)
        Me.Controls.Add(Me.btnFont)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Font = New System.Drawing.Font("Verdana", 10.98305!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Ed_Teacher_AddSummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Enter Syllabus"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents btnFont As Button
    Friend WithEvents btnFontColor As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label1 As Label
End Class
