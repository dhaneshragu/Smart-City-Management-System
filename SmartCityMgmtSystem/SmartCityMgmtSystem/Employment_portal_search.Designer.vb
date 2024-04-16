<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Employment_portal_search
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
        Me.childformPanel = New System.Windows.Forms.Panel()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Dept = New System.Windows.Forms.ComboBox()
        Me.Job_Desc = New System.Windows.Forms.ComboBox()
        Me.RichTextBox10 = New System.Windows.Forms.RichTextBox()
        Me.RichTextBox9 = New System.Windows.Forms.RichTextBox()
        Me.Qual = New System.Windows.Forms.ComboBox()
        Me.RichTextBox8 = New System.Windows.Forms.RichTextBox()
        Me.RichTextBox7 = New System.Windows.Forms.RichTextBox()
        Me.RichTextBox6 = New System.Windows.Forms.RichTextBox()
        Me.Job_ID = New System.Windows.Forms.RichTextBox()
        Me.Salary = New System.Windows.Forms.RichTextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.childformPanel.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'childformPanel
        '
        Me.childformPanel.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.childformPanel.Controls.Add(Me.RichTextBox1)
        Me.childformPanel.Controls.Add(Me.Dept)
        Me.childformPanel.Controls.Add(Me.Job_Desc)
        Me.childformPanel.Controls.Add(Me.RichTextBox10)
        Me.childformPanel.Controls.Add(Me.RichTextBox9)
        Me.childformPanel.Controls.Add(Me.Qual)
        Me.childformPanel.Controls.Add(Me.RichTextBox8)
        Me.childformPanel.Controls.Add(Me.RichTextBox7)
        Me.childformPanel.Controls.Add(Me.RichTextBox6)
        Me.childformPanel.Controls.Add(Me.Job_ID)
        Me.childformPanel.Controls.Add(Me.Salary)
        Me.childformPanel.Controls.Add(Me.PictureBox1)
        Me.childformPanel.Controls.Add(Me.Label4)
        Me.childformPanel.Controls.Add(Me.DataGridView1)
        Me.childformPanel.Controls.Add(Me.btnSearch)
        Me.childformPanel.Font = New System.Drawing.Font("Trebuchet MS", 9.762712!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.childformPanel.Location = New System.Drawing.Point(1, 59)
        Me.childformPanel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.childformPanel.Name = "childformPanel"
        Me.childformPanel.Size = New System.Drawing.Size(1313, 721)
        Me.childformPanel.TabIndex = 1
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Location = New System.Drawing.Point(279, 65)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.RichTextBox1.Size = New System.Drawing.Size(752, 43)
        Me.RichTextBox1.TabIndex = 34
        Me.RichTextBox1.Text = "Welcome to our Job Search Portal. If you wish to search all possible job openings" &
    ", then just press the Search Button. Enter your preferences and then Search for " &
    "using the advanced filter."
        '
        'Dept
        '
        Me.Dept.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Dept.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Dept.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.Dept.BackColor = System.Drawing.Color.DarkGoldenrod
        Me.Dept.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Dept.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.Dept.ForeColor = System.Drawing.Color.White
        Me.Dept.FormattingEnabled = True
        Me.Dept.Location = New System.Drawing.Point(130, 318)
        Me.Dept.Margin = New System.Windows.Forms.Padding(0)
        Me.Dept.Name = "Dept"
        Me.Dept.Size = New System.Drawing.Size(221, 39)
        Me.Dept.TabIndex = 33
        '
        'Job_Desc
        '
        Me.Job_Desc.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Job_Desc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Job_Desc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.Job_Desc.BackColor = System.Drawing.Color.DarkGoldenrod
        Me.Job_Desc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Job_Desc.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.Job_Desc.ForeColor = System.Drawing.Color.White
        Me.Job_Desc.FormattingEnabled = True
        Me.Job_Desc.Location = New System.Drawing.Point(130, 235)
        Me.Job_Desc.Margin = New System.Windows.Forms.Padding(0)
        Me.Job_Desc.Name = "Job_Desc"
        Me.Job_Desc.Size = New System.Drawing.Size(221, 39)
        Me.Job_Desc.TabIndex = 32
        '
        'RichTextBox10
        '
        Me.RichTextBox10.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.RichTextBox10.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox10.Location = New System.Drawing.Point(11, 160)
        Me.RichTextBox10.Name = "RichTextBox10"
        Me.RichTextBox10.Size = New System.Drawing.Size(75, 39)
        Me.RichTextBox10.TabIndex = 31
        Me.RichTextBox10.Text = "Job ID:"
        '
        'RichTextBox9
        '
        Me.RichTextBox9.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.RichTextBox9.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox9.Location = New System.Drawing.Point(11, 237)
        Me.RichTextBox9.Name = "RichTextBox9"
        Me.RichTextBox9.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.RichTextBox9.Size = New System.Drawing.Size(98, 37)
        Me.RichTextBox9.TabIndex = 30
        Me.RichTextBox9.Text = "Job Desc:"
        '
        'Qual
        '
        Me.Qual.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Qual.BackColor = System.Drawing.Color.DarkGoldenrod
        Me.Qual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Qual.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Qual.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.Qual.ForeColor = System.Drawing.Color.White
        Me.Qual.FormattingEnabled = True
        Me.Qual.Items.AddRange(New Object() {"10th Pass", "12th Pass", "UG", "PG", "0-2 yrs Experience", "2+ yrs Experience"})
        Me.Qual.Location = New System.Drawing.Point(130, 460)
        Me.Qual.Margin = New System.Windows.Forms.Padding(0)
        Me.Qual.Name = "Qual"
        Me.Qual.Size = New System.Drawing.Size(221, 39)
        Me.Qual.TabIndex = 29
        '
        'RichTextBox8
        '
        Me.RichTextBox8.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.RichTextBox8.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox8.Location = New System.Drawing.Point(11, 318)
        Me.RichTextBox8.Name = "RichTextBox8"
        Me.RichTextBox8.Size = New System.Drawing.Size(116, 39)
        Me.RichTextBox8.TabIndex = 28
        Me.RichTextBox8.Text = "Department:"
        '
        'RichTextBox7
        '
        Me.RichTextBox7.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.RichTextBox7.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox7.Location = New System.Drawing.Point(11, 391)
        Me.RichTextBox7.Name = "RichTextBox7"
        Me.RichTextBox7.Size = New System.Drawing.Size(75, 39)
        Me.RichTextBox7.TabIndex = 27
        Me.RichTextBox7.Text = "Salary:"
        '
        'RichTextBox6
        '
        Me.RichTextBox6.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.RichTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox6.Location = New System.Drawing.Point(11, 460)
        Me.RichTextBox6.Name = "RichTextBox6"
        Me.RichTextBox6.Size = New System.Drawing.Size(116, 39)
        Me.RichTextBox6.TabIndex = 26
        Me.RichTextBox6.Text = "Qualification:"
        '
        'Job_ID
        '
        Me.Job_ID.BackColor = System.Drawing.Color.DarkGoldenrod
        Me.Job_ID.Location = New System.Drawing.Point(130, 160)
        Me.Job_ID.Name = "Job_ID"
        Me.Job_ID.Size = New System.Drawing.Size(221, 39)
        Me.Job_ID.TabIndex = 21
        Me.Job_ID.Text = ""
        '
        'Salary
        '
        Me.Salary.BackColor = System.Drawing.Color.DarkGoldenrod
        Me.Salary.Location = New System.Drawing.Point(130, 388)
        Me.Salary.Name = "Salary"
        Me.Salary.Size = New System.Drawing.Size(221, 39)
        Me.Salary.TabIndex = 19
        Me.Salary.Text = ""
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_job_48
        Me.PictureBox1.Location = New System.Drawing.Point(450, 11)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(50, 48)
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(521, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(290, 48)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Search Jobs"
        '
        'DataGridView1
        '
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.BlanchedAlmond
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column4, Me.Column5, Me.Column6, Me.Column3})
        Me.DataGridView1.Location = New System.Drawing.Point(381, 148)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataGridView1.Size = New System.Drawing.Size(918, 560)
        Me.DataGridView1.TabIndex = 14
        '
        'Column1
        '
        Me.Column1.HeaderText = "JobID"
        Me.Column1.MinimumWidth = 6
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 125
        '
        'Column2
        '
        Me.Column2.HeaderText = "Job Description"
        Me.Column2.MinimumWidth = 6
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 180
        '
        'Column4
        '
        Me.Column4.HeaderText = "Department"
        Me.Column4.MinimumWidth = 6
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 135
        '
        'Column5
        '
        Me.Column5.HeaderText = "Annual Salary"
        Me.Column5.MinimumWidth = 6
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 175
        '
        'Column6
        '
        Me.Column6.HeaderText = "Application Deadline"
        Me.Column6.MinimumWidth = 6
        Me.Column6.Name = "Column6"
        Me.Column6.Width = 170
        '
        'Column3
        '
        Me.Column3.HeaderText = "Qualification"
        Me.Column3.MinimumWidth = 6
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 140
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.DarkGoldenrod
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(130, 553)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(0)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(225, 38)
        Me.btnSearch.TabIndex = 12
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.CausesValidation = False
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0339!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Ivory
        Me.Label2.Location = New System.Drawing.Point(152, 13)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 29)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "{Name}"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.CausesValidation = False
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0339!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Ivory
        Me.Label3.Location = New System.Drawing.Point(803, 13)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(163, 29)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "{Aadhar No.}"
        '
        'Button5
        '
        Me.Button5.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_exit_50
        Me.Button5.Location = New System.Drawing.Point(1217, 9)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(52, 44)
        Me.Button5.TabIndex = 15
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.CausesValidation = False
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0339!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.Ivory
        Me.Button6.Image = Global.SmartCityMgmtSystem.My.Resources.Resources.icons8_back_arrow_25
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button6.Location = New System.Drawing.Point(1388, 0)
        Me.Button6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button6.Name = "Button6"
        Me.Button6.Padding = New System.Windows.Forms.Padding(12, 0, 0, 0)
        Me.Button6.Size = New System.Drawing.Size(163, 39)
        Me.Button6.TabIndex = 7
        Me.Button6.Text = "      Home Page"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.CausesValidation = False
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0339!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Ivory
        Me.Label1.Location = New System.Drawing.Point(725, 13)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 29)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "UID: "
        '
        'Employment_portal_search
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkGoldenrod
        Me.ClientSize = New System.Drawing.Size(1312, 779)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.childformPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.Name = "Employment_portal_search"
        Me.Text = "Transportation"
        Me.childformPanel.ResumeLayout(False)
        Me.childformPanel.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents childformPanel As System.Windows.Forms.Panel
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button5 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Qual As ComboBox
    Friend WithEvents RichTextBox8 As RichTextBox
    Friend WithEvents RichTextBox7 As RichTextBox
    Friend WithEvents RichTextBox6 As RichTextBox
    Friend WithEvents Job_ID As RichTextBox
    Friend WithEvents Salary As RichTextBox
    Friend WithEvents RichTextBox10 As RichTextBox
    Friend WithEvents RichTextBox9 As RichTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Job_Desc As ComboBox
    Friend WithEvents Dept As ComboBox
    Friend WithEvents RichTextBox1 As RichTextBox
End Class
