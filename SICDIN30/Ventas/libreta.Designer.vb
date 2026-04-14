<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class libreta
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.panellibreta = New System.Windows.Forms.Panel()
        Me.paneldg = New System.Windows.Forms.Panel()
        Me.dglv = New System.Windows.Forms.DataGridView()
        Me.Cancelb = New System.Windows.Forms.DataGridViewImageColumn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditarPagoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConvertirANotaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.panelcomando = New System.Windows.Forms.Panel()
        Me.totalefe = New System.Windows.Forms.LinkLabel()
        Me.totall = New System.Windows.Forms.LinkLabel()
        Me.tiendascombo = New System.Windows.Forms.ComboBox()
        Me.Verp = New System.Windows.Forms.Button()
        Me.Retiro = New System.Windows.Forms.Button()
        Me.Cerrardia = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.hoy = New System.Windows.Forms.Button()
        Me.calendario = New System.Windows.Forms.DateTimePicker()
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.panellibreta.SuspendLayout()
        Me.paneldg.SuspendLayout()
        CType(Me.dglv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.panelcomando.SuspendLayout()
        Me.SuspendLayout()
        '
        'panellibreta
        '
        Me.panellibreta.Controls.Add(Me.paneldg)
        Me.panellibreta.Controls.Add(Me.panelcomando)
        Me.panellibreta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panellibreta.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panellibreta.Location = New System.Drawing.Point(0, 0)
        Me.panellibreta.Margin = New System.Windows.Forms.Padding(2)
        Me.panellibreta.Name = "panellibreta"
        Me.panellibreta.Size = New System.Drawing.Size(918, 366)
        Me.panellibreta.TabIndex = 0
        '
        'paneldg
        '
        Me.paneldg.Controls.Add(Me.dglv)
        Me.paneldg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.paneldg.Location = New System.Drawing.Point(0, 36)
        Me.paneldg.Margin = New System.Windows.Forms.Padding(2)
        Me.paneldg.Name = "paneldg"
        Me.paneldg.Size = New System.Drawing.Size(918, 330)
        Me.paneldg.TabIndex = 0
        '
        'dglv
        '
        Me.dglv.AllowUserToAddRows = False
        Me.dglv.AllowUserToDeleteRows = False
        Me.dglv.BackgroundColor = System.Drawing.SystemColors.InactiveCaption
        Me.dglv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.dglv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dglv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Cancelb})
        Me.dglv.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dglv.DefaultCellStyle = DataGridViewCellStyle1
        Me.dglv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dglv.Location = New System.Drawing.Point(0, 0)
        Me.dglv.Margin = New System.Windows.Forms.Padding(2)
        Me.dglv.Name = "dglv"
        Me.dglv.ReadOnly = True
        Me.dglv.RowHeadersWidth = 51
        Me.dglv.RowTemplate.Height = 24
        Me.dglv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dglv.Size = New System.Drawing.Size(918, 330)
        Me.dglv.TabIndex = 0
        '
        'Cancelb
        '
        Me.Cancelb.HeaderText = "Cancel"
        Me.Cancelb.Image = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.Cancelb.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.Cancelb.MinimumWidth = 6
        Me.Cancelb.Name = "Cancelb"
        Me.Cancelb.ReadOnly = True
        Me.Cancelb.Width = 125
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditarPagoToolStripMenuItem, Me.AbrirToolStripMenuItem, Me.ConvertirANotaToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(164, 82)
        '
        'EditarPagoToolStripMenuItem
        '
        Me.EditarPagoToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.pagar
        Me.EditarPagoToolStripMenuItem.Name = "EditarPagoToolStripMenuItem"
        Me.EditarPagoToolStripMenuItem.Size = New System.Drawing.Size(163, 26)
        Me.EditarPagoToolStripMenuItem.Text = "Editar pago"
        '
        'AbrirToolStripMenuItem
        '
        Me.AbrirToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.folderorangeopen_93000
        Me.AbrirToolStripMenuItem.Name = "AbrirToolStripMenuItem"
        Me.AbrirToolStripMenuItem.Size = New System.Drawing.Size(163, 26)
        Me.AbrirToolStripMenuItem.Text = "Abrir"
        '
        'ConvertirANotaToolStripMenuItem
        '
        Me.ConvertirANotaToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.flechader
        Me.ConvertirANotaToolStripMenuItem.Name = "ConvertirANotaToolStripMenuItem"
        Me.ConvertirANotaToolStripMenuItem.Size = New System.Drawing.Size(163, 26)
        Me.ConvertirANotaToolStripMenuItem.Text = "Convertir a nota"
        '
        'panelcomando
        '
        Me.panelcomando.Controls.Add(Me.totalefe)
        Me.panelcomando.Controls.Add(Me.totall)
        Me.panelcomando.Controls.Add(Me.tiendascombo)
        Me.panelcomando.Controls.Add(Me.Verp)
        Me.panelcomando.Controls.Add(Me.Retiro)
        Me.panelcomando.Controls.Add(Me.Cerrardia)
        Me.panelcomando.Controls.Add(Me.Button1)
        Me.panelcomando.Controls.Add(Me.hoy)
        Me.panelcomando.Controls.Add(Me.calendario)
        Me.panelcomando.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelcomando.Location = New System.Drawing.Point(0, 0)
        Me.panelcomando.Margin = New System.Windows.Forms.Padding(2)
        Me.panelcomando.Name = "panelcomando"
        Me.panelcomando.Size = New System.Drawing.Size(918, 36)
        Me.panelcomando.TabIndex = 1
        '
        'totalefe
        '
        Me.totalefe.AutoSize = True
        Me.totalefe.Dock = System.Windows.Forms.DockStyle.Left
        Me.totalefe.Location = New System.Drawing.Point(566, 0)
        Me.totalefe.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.totalefe.Name = "totalefe"
        Me.totalefe.Size = New System.Drawing.Size(97, 23)
        Me.totalefe.TabIndex = 6
        Me.totalefe.TabStop = True
        Me.totalefe.Text = "Efectivo:$0.0"
        '
        'totall
        '
        Me.totall.AutoSize = True
        Me.totall.Dock = System.Windows.Forms.DockStyle.Left
        Me.totall.Location = New System.Drawing.Point(469, 0)
        Me.totall.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.totall.Name = "totall"
        Me.totall.Size = New System.Drawing.Size(97, 23)
        Me.totall.TabIndex = 2
        Me.totall.TabStop = True
        Me.totall.Text = "V. Total:$0.0"
        '
        'tiendascombo
        '
        Me.tiendascombo.Dock = System.Windows.Forms.DockStyle.Left
        Me.tiendascombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tiendascombo.FormattingEnabled = True
        Me.tiendascombo.Location = New System.Drawing.Point(348, 0)
        Me.tiendascombo.Name = "tiendascombo"
        Me.tiendascombo.Size = New System.Drawing.Size(121, 30)
        Me.tiendascombo.TabIndex = 8
        Me.tiendascombo.Visible = False
        '
        'Verp
        '
        Me.Verp.AutoSize = True
        Me.Verp.BackgroundImage = Global.SICDIN30.My.Resources.Resources.zenmap_104119
        Me.Verp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Verp.Dock = System.Windows.Forms.DockStyle.Right
        Me.Verp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Verp.Location = New System.Drawing.Point(694, 0)
        Me.Verp.Margin = New System.Windows.Forms.Padding(2)
        Me.Verp.Name = "Verp"
        Me.Verp.Size = New System.Drawing.Size(56, 36)
        Me.Verp.TabIndex = 7
        Me.Verp.Tag = "Ver pagos"
        Me.Verp.UseVisualStyleBackColor = True
        '
        'Retiro
        '
        Me.Retiro.AutoSize = True
        Me.Retiro.BackgroundImage = Global.SICDIN30.My.Resources.Resources.business_color_handout_icon_icons_com_53455
        Me.Retiro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Retiro.Dock = System.Windows.Forms.DockStyle.Right
        Me.Retiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Retiro.Location = New System.Drawing.Point(750, 0)
        Me.Retiro.Margin = New System.Windows.Forms.Padding(2)
        Me.Retiro.Name = "Retiro"
        Me.Retiro.Size = New System.Drawing.Size(56, 36)
        Me.Retiro.TabIndex = 5
        Me.Retiro.Tag = "Retiro efectivo"
        Me.Retiro.UseVisualStyleBackColor = True
        '
        'Cerrardia
        '
        Me.Cerrardia.AutoSize = True
        Me.Cerrardia.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cajar
        Me.Cerrardia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Cerrardia.Dock = System.Windows.Forms.DockStyle.Right
        Me.Cerrardia.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cerrardia.Location = New System.Drawing.Point(806, 0)
        Me.Cerrardia.Margin = New System.Windows.Forms.Padding(2)
        Me.Cerrardia.Name = "Cerrardia"
        Me.Cerrardia.Size = New System.Drawing.Size(56, 36)
        Me.Cerrardia.TabIndex = 3
        Me.Cerrardia.Tag = "Cerrar día"
        Me.Cerrardia.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.SICDIN30.My.Resources.Resources.Logout_37127
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(862, 0)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(56, 36)
        Me.Button1.TabIndex = 1
        Me.Button1.Tag = "Salir"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'hoy
        '
        Me.hoy.AutoSize = True
        Me.hoy.BackgroundImage = Global.SICDIN30.My.Resources.Resources.hoy
        Me.hoy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.hoy.Dock = System.Windows.Forms.DockStyle.Left
        Me.hoy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.hoy.Location = New System.Drawing.Point(314, 0)
        Me.hoy.Margin = New System.Windows.Forms.Padding(2)
        Me.hoy.Name = "hoy"
        Me.hoy.Size = New System.Drawing.Size(34, 36)
        Me.hoy.TabIndex = 4
        Me.hoy.Tag = "Hoy"
        Me.hoy.UseVisualStyleBackColor = True
        '
        'calendario
        '
        Me.calendario.Dock = System.Windows.Forms.DockStyle.Left
        Me.calendario.Location = New System.Drawing.Point(0, 0)
        Me.calendario.Margin = New System.Windows.Forms.Padding(2)
        Me.calendario.Name = "calendario"
        Me.calendario.Size = New System.Drawing.Size(314, 29)
        Me.calendario.TabIndex = 0
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.HeaderText = "Cancel"
        Me.DataGridViewImageColumn1.Image = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.DataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn1.MinimumWidth = 6
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ReadOnly = True
        Me.DataGridViewImageColumn1.Width = 125
        '
        'libreta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(918, 366)
        Me.Controls.Add(Me.panellibreta)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "libreta"
        Me.Text = "libreta"
        Me.panellibreta.ResumeLayout(False)
        Me.paneldg.ResumeLayout(False)
        CType(Me.dglv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.panelcomando.ResumeLayout(False)
        Me.panelcomando.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelcomando As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents calendario As DateTimePicker
    Friend WithEvents dglv As DataGridView
    Public WithEvents panellibreta As Panel
    Public WithEvents paneldg As Panel
    Friend WithEvents totall As LinkLabel
    Friend WithEvents Cancelb As DataGridViewImageColumn
    Friend WithEvents Cerrardia As Button
    Friend WithEvents hoy As Button
    Friend WithEvents DataGridViewImageColumn1 As DataGridViewImageColumn
    Friend WithEvents Retiro As Button
    Friend WithEvents totalefe As LinkLabel
    Friend WithEvents Verp As Button
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents EditarPagoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AbrirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tiendascombo As ComboBox
    Friend WithEvents ConvertirANotaToolStripMenuItem As ToolStripMenuItem
End Class
