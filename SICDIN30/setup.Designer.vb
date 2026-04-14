<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class setup
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.FontDialog = New System.Windows.Forms.FontDialog()
        Me.panelsetup = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.printempty = New System.Windows.Forms.Button()
        Me.printselect = New System.Windows.Forms.ComboBox()
        Me.nubecalendar = New System.Windows.Forms.DateTimePicker()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.Desde = New System.Windows.Forms.RadioButton()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.syncpr = New System.Windows.Forms.ProgressBar()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.com = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.fuentelabel = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.fontb = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.panelsetup.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelsetup
        '
        Me.panelsetup.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.panelsetup.Controls.Add(Me.GroupBox1)
        Me.panelsetup.Controls.Add(Me.nubecalendar)
        Me.panelsetup.Controls.Add(Me.RadioButton2)
        Me.panelsetup.Controls.Add(Me.Desde)
        Me.panelsetup.Controls.Add(Me.ComboBox1)
        Me.panelsetup.Controls.Add(Me.syncpr)
        Me.panelsetup.Controls.Add(Me.Button3)
        Me.panelsetup.Controls.Add(Me.com)
        Me.panelsetup.Controls.Add(Me.Button2)
        Me.panelsetup.Controls.Add(Me.Button1)
        Me.panelsetup.Controls.Add(Me.fuentelabel)
        Me.panelsetup.Controls.Add(Me.Panel4)
        Me.panelsetup.Controls.Add(Me.Panel2)
        Me.panelsetup.Controls.Add(Me.Panel1)
        Me.panelsetup.Controls.Add(Me.Panel3)
        Me.panelsetup.Controls.Add(Me.fontb)
        Me.panelsetup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelsetup.Location = New System.Drawing.Point(0, 0)
        Me.panelsetup.Name = "panelsetup"
        Me.panelsetup.Size = New System.Drawing.Size(932, 358)
        Me.panelsetup.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.printempty)
        Me.GroupBox1.Controls.Add(Me.printselect)
        Me.GroupBox1.Location = New System.Drawing.Point(30, 161)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(234, 85)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Imprimir notas vacias"
        '
        'printempty
        '
        Me.printempty.BackgroundImage = Global.SICDIN30.My.Resources.Resources._1486504845_device_electronic_paper_education_business_office_print_printer_81370
        Me.printempty.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.printempty.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.printempty.Location = New System.Drawing.Point(19, 28)
        Me.printempty.Name = "printempty"
        Me.printempty.Size = New System.Drawing.Size(52, 45)
        Me.printempty.TabIndex = 24
        Me.printempty.Tag = "Imprimir"
        Me.printempty.UseVisualStyleBackColor = True
        '
        'printselect
        '
        Me.printselect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.printselect.FormattingEnabled = True
        Me.printselect.Items.AddRange(New Object() {"Venta", "Reparación"})
        Me.printselect.Location = New System.Drawing.Point(77, 28)
        Me.printselect.Name = "printselect"
        Me.printselect.Size = New System.Drawing.Size(141, 30)
        Me.printselect.TabIndex = 25
        '
        'nubecalendar
        '
        Me.nubecalendar.Enabled = False
        Me.nubecalendar.Location = New System.Drawing.Point(436, 127)
        Me.nubecalendar.Name = "nubecalendar"
        Me.nubecalendar.Size = New System.Drawing.Size(267, 29)
        Me.nubecalendar.TabIndex = 23
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(513, 94)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(64, 27)
        Me.RadioButton2.TabIndex = 22
        Me.RadioButton2.Text = "Todo"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'Desde
        '
        Me.Desde.AutoSize = True
        Me.Desde.Checked = True
        Me.Desde.Location = New System.Drawing.Point(436, 94)
        Me.Desde.Name = "Desde"
        Me.Desde.Size = New System.Drawing.Size(74, 27)
        Me.Desde.TabIndex = 21
        Me.Desde.TabStop = True
        Me.Desde.Text = "Desde"
        Me.Desde.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(436, 58)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(141, 30)
        Me.ComboBox1.TabIndex = 20
        '
        'syncpr
        '
        Me.syncpr.Enabled = False
        Me.syncpr.Location = New System.Drawing.Point(318, 139)
        Me.syncpr.Name = "syncpr"
        Me.syncpr.Size = New System.Drawing.Size(112, 23)
        Me.syncpr.TabIndex = 19
        Me.syncpr.Visible = False
        '
        'Button3
        '
        Me.Button3.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cloudsync
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Location = New System.Drawing.Point(318, 58)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(112, 75)
        Me.Button3.TabIndex = 18
        Me.Button3.Tag = "Sincronización total"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'com
        '
        Me.com.AutoSize = True
        Me.com.Location = New System.Drawing.Point(26, 249)
        Me.com.Name = "com"
        Me.com.Size = New System.Drawing.Size(100, 23)
        Me.com.TabIndex = 17
        Me.com.Text = "Compilación:"
        '
        'Button2
        '
        Me.Button2.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(222, 58)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(69, 75)
        Me.Button2.TabIndex = 16
        Me.Button2.Tag = "Borrar fuente"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Button1.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button1.Location = New System.Drawing.Point(20, 281)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(892, 57)
        Me.Button1.TabIndex = 15
        Me.Button1.UseVisualStyleBackColor = False
        '
        'fuentelabel
        '
        Me.fuentelabel.AutoSize = True
        Me.fuentelabel.Location = New System.Drawing.Point(26, 136)
        Me.fuentelabel.Name = "fuentelabel"
        Me.fuentelabel.Size = New System.Drawing.Size(63, 23)
        Me.fuentelabel.TabIndex = 14
        Me.fuentelabel.Text = "Fuente:"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(20, 338)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(892, 20)
        Me.Panel4.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(20, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(892, 30)
        Me.Panel2.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Configuración"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(20, 358)
        Me.Panel1.TabIndex = 11
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(912, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(20, 358)
        Me.Panel3.TabIndex = 10
        '
        'fontb
        '
        Me.fontb.BackgroundImage = Global.SICDIN30.My.Resources.Resources.barcode
        Me.fontb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.fontb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.fontb.Location = New System.Drawing.Point(26, 58)
        Me.fontb.Name = "fontb"
        Me.fontb.Size = New System.Drawing.Size(190, 75)
        Me.fontb.TabIndex = 0
        Me.fontb.Tag = "Fuente para codigo de barras"
        Me.fontb.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "art"
        Me.OpenFileDialog1.Filter = " SICDIN Database|*.dat"
        '
        'setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(932, 358)
        Me.Controls.Add(Me.panelsetup)
        Me.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "setup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "setup"
        Me.panelsetup.ResumeLayout(False)
        Me.panelsetup.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FontDialog As FontDialog
    Friend WithEvents panelsetup As Panel
    Friend WithEvents fontb As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents fuentelabel As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents com As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents syncpr As ProgressBar
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents nubecalendar As DateTimePicker
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents Desde As RadioButton
    Friend WithEvents printempty As Button
    Friend WithEvents printselect As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
End Class
