<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class sql
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.sqlorder = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.sys = New System.Windows.Forms.RadioButton()
        Me.usu = New System.Windows.Forms.RadioButton()
        Me.rep = New System.Windows.Forms.RadioButton()
        Me.ven = New System.Windows.Forms.RadioButton()
        Me.cli = New System.Windows.Forms.RadioButton()
        Me.art = New System.Windows.Forms.RadioButton()
        Me.ejec = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(22, 258)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(603, 25)
        Me.Panel4.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(22, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(603, 25)
        Me.Panel2.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.Control
        Me.Label2.Location = New System.Drawing.Point(6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(149, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Ejecutar orden SQL"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(22, 283)
        Me.Panel1.TabIndex = 11
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(625, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(22, 283)
        Me.Panel3.TabIndex = 10
        '
        'sqlorder
        '
        Me.sqlorder.Location = New System.Drawing.Point(45, 65)
        Me.sqlorder.Name = "sqlorder"
        Me.sqlorder.Size = New System.Drawing.Size(468, 26)
        Me.sqlorder.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(41, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 20)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "ORDEN:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.sys)
        Me.GroupBox1.Controls.Add(Me.usu)
        Me.GroupBox1.Controls.Add(Me.rep)
        Me.GroupBox1.Controls.Add(Me.ven)
        Me.GroupBox1.Controls.Add(Me.cli)
        Me.GroupBox1.Controls.Add(Me.art)
        Me.GroupBox1.Location = New System.Drawing.Point(45, 109)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(559, 100)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Base de datos"
        '
        'sys
        '
        Me.sys.AutoSize = True
        Me.sys.Dock = System.Windows.Forms.DockStyle.Left
        Me.sys.Location = New System.Drawing.Point(452, 22)
        Me.sys.Name = "sys"
        Me.sys.Size = New System.Drawing.Size(85, 75)
        Me.sys.TabIndex = 5
        Me.sys.Text = "Sistema"
        Me.sys.UseVisualStyleBackColor = True
        '
        'usu
        '
        Me.usu.AutoSize = True
        Me.usu.Dock = System.Windows.Forms.DockStyle.Left
        Me.usu.Location = New System.Drawing.Point(362, 22)
        Me.usu.Name = "usu"
        Me.usu.Size = New System.Drawing.Size(90, 75)
        Me.usu.TabIndex = 4
        Me.usu.Text = "Usuarios"
        Me.usu.UseVisualStyleBackColor = True
        '
        'rep
        '
        Me.rep.AutoSize = True
        Me.rep.Dock = System.Windows.Forms.DockStyle.Left
        Me.rep.Location = New System.Drawing.Point(253, 22)
        Me.rep.Name = "rep"
        Me.rep.Size = New System.Drawing.Size(109, 75)
        Me.rep.TabIndex = 3
        Me.rep.Text = "Reparación"
        Me.rep.UseVisualStyleBackColor = True
        '
        'ven
        '
        Me.ven.AutoSize = True
        Me.ven.Dock = System.Windows.Forms.DockStyle.Left
        Me.ven.Location = New System.Drawing.Point(175, 22)
        Me.ven.Name = "ven"
        Me.ven.Size = New System.Drawing.Size(78, 75)
        Me.ven.TabIndex = 2
        Me.ven.Text = "Ventas"
        Me.ven.UseVisualStyleBackColor = True
        '
        'cli
        '
        Me.cli.AutoSize = True
        Me.cli.Dock = System.Windows.Forms.DockStyle.Left
        Me.cli.Location = New System.Drawing.Point(91, 22)
        Me.cli.Name = "cli"
        Me.cli.Size = New System.Drawing.Size(84, 75)
        Me.cli.TabIndex = 1
        Me.cli.Text = "Clientes"
        Me.cli.UseVisualStyleBackColor = True
        '
        'art
        '
        Me.art.AutoSize = True
        Me.art.Checked = True
        Me.art.Dock = System.Windows.Forms.DockStyle.Left
        Me.art.Location = New System.Drawing.Point(3, 22)
        Me.art.Name = "art"
        Me.art.Size = New System.Drawing.Size(88, 75)
        Me.art.TabIndex = 0
        Me.art.TabStop = True
        Me.art.Text = "Artículos"
        Me.art.UseVisualStyleBackColor = True
        '
        'ejec
        '
        Me.ejec.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.ejec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ejec.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ejec.Location = New System.Drawing.Point(519, 59)
        Me.ejec.Name = "ejec"
        Me.ejec.Size = New System.Drawing.Size(39, 38)
        Me.ejec.TabIndex = 17
        Me.ejec.Tag = "Ejecutar"
        Me.ejec.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Button2
        '
        Me.Button2.BackgroundImage = Global.SICDIN30.My.Resources.Resources.Logout_37127
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(22, 220)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(603, 38)
        Me.Button2.TabIndex = 18
        Me.Button2.Tag = "Ejecutar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'sql
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(647, 283)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ejec)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.sqlorder)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "sql"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "sql"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents sqlorder As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rep As RadioButton
    Friend WithEvents ven As RadioButton
    Friend WithEvents cli As RadioButton
    Friend WithEvents art As RadioButton
    Friend WithEvents sys As RadioButton
    Friend WithEvents usu As RadioButton
    Friend WithEvents ejec As Button
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents Button2 As Button
    Friend WithEvents Label2 As Label
End Class
