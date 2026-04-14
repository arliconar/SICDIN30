<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class existencias
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.panelexis = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.falt = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.perd = New System.Windows.Forms.DataGridView()
        Me.Ver = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Borrar = New System.Windows.Forms.DataGridViewImageColumn()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.dglog = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dtlog = New System.Windows.Forms.DateTimePicker()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.del = New System.Windows.Forms.CheckBox()
        Me.agregar = New System.Windows.Forms.CheckBox()
        Me.ext = New System.Windows.Forms.CheckBox()
        Me.cambio = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Copiarp = New System.Windows.Forms.Button()
        Me.recargar = New System.Windows.Forms.Button()
        Me.dardealta = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SeleccionaTodoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.panelexis.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.falt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.perd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.dglog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelexis
        '
        Me.panelexis.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.panelexis.Controls.Add(Me.TabControl1)
        Me.panelexis.Controls.Add(Me.Panel2)
        Me.panelexis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelexis.Location = New System.Drawing.Point(0, 0)
        Me.panelexis.Margin = New System.Windows.Forms.Padding(2)
        Me.panelexis.Name = "panelexis"
        Me.panelexis.Size = New System.Drawing.Size(600, 366)
        Me.panelexis.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 63)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(600, 303)
        Me.TabControl1.TabIndex = 6
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.falt)
        Me.TabPage1.Location = New System.Drawing.Point(4, 31)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage1.Size = New System.Drawing.Size(592, 268)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Faltantes"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'falt
        '
        Me.falt.AllowUserToAddRows = False
        Me.falt.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.falt.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.falt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.falt.DefaultCellStyle = DataGridViewCellStyle2
        Me.falt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.falt.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.falt.Location = New System.Drawing.Point(2, 2)
        Me.falt.Margin = New System.Windows.Forms.Padding(2)
        Me.falt.Name = "falt"
        Me.falt.ReadOnly = True
        Me.falt.RowHeadersWidth = 51
        Me.falt.RowTemplate.Height = 24
        Me.falt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.falt.Size = New System.Drawing.Size(588, 264)
        Me.falt.TabIndex = 5
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.perd)
        Me.TabPage2.Location = New System.Drawing.Point(4, 31)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage2.Size = New System.Drawing.Size(592, 268)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Perdidas"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'perd
        '
        Me.perd.AllowUserToAddRows = False
        Me.perd.AllowUserToDeleteRows = False
        Me.perd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.perd.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Ver, Me.Borrar})
        Me.perd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.perd.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.perd.Location = New System.Drawing.Point(2, 2)
        Me.perd.Margin = New System.Windows.Forms.Padding(2)
        Me.perd.Name = "perd"
        Me.perd.ReadOnly = True
        Me.perd.RowHeadersWidth = 51
        Me.perd.RowTemplate.Height = 24
        Me.perd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.perd.Size = New System.Drawing.Size(588, 264)
        Me.perd.TabIndex = 6
        '
        'Ver
        '
        Me.Ver.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.Ver.HeaderText = "Ver"
        Me.Ver.Image = Global.SICDIN30.My.Resources.Resources.zenmap_104119
        Me.Ver.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.Ver.MinimumWidth = 6
        Me.Ver.Name = "Ver"
        Me.Ver.ReadOnly = True
        Me.Ver.Width = 41
        '
        'Borrar
        '
        Me.Borrar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.Borrar.HeaderText = "Borrar"
        Me.Borrar.Image = Global.SICDIN30.My.Resources.Resources._1486504830_delete_dustbin_empty_recycle_recycling_remove_trash_81361
        Me.Borrar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.Borrar.MinimumWidth = 6
        Me.Borrar.Name = "Borrar"
        Me.Borrar.ReadOnly = True
        Me.Borrar.Width = 59
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.TabPage3.Controls.Add(Me.dglog)
        Me.TabPage3.Controls.Add(Me.Panel1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 31)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(592, 268)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Nube"
        '
        'dglog
        '
        Me.dglog.BackgroundColor = System.Drawing.SystemColors.InactiveCaption
        Me.dglog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dglog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dglog.Location = New System.Drawing.Point(3, 124)
        Me.dglog.Name = "dglog"
        Me.dglog.Size = New System.Drawing.Size(586, 141)
        Me.dglog.TabIndex = 8
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.dtlog)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.del)
        Me.Panel1.Controls.Add(Me.agregar)
        Me.Panel1.Controls.Add(Me.ext)
        Me.Panel1.Controls.Add(Me.cambio)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(586, 121)
        Me.Panel1.TabIndex = 7
        '
        'dtlog
        '
        Me.dtlog.Location = New System.Drawing.Point(7, 13)
        Me.dtlog.Name = "dtlog"
        Me.dtlog.Size = New System.Drawing.Size(281, 29)
        Me.dtlog.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.SICDIN30.My.Resources.Resources.loupe_78956
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(394, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(127, 61)
        Me.Button1.TabIndex = 6
        Me.Button1.Tag = "Buscar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Fecha:"
        '
        'del
        '
        Me.del.AutoSize = True
        Me.del.Checked = True
        Me.del.CheckState = System.Windows.Forms.CheckState.Checked
        Me.del.Location = New System.Drawing.Point(297, 83)
        Me.del.Name = "del"
        Me.del.Size = New System.Drawing.Size(85, 27)
        Me.del.TabIndex = 5
        Me.del.Text = "Borrado"
        Me.del.UseVisualStyleBackColor = True
        '
        'agregar
        '
        Me.agregar.AutoSize = True
        Me.agregar.Checked = True
        Me.agregar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.agregar.Location = New System.Drawing.Point(13, 83)
        Me.agregar.Name = "agregar"
        Me.agregar.Size = New System.Drawing.Size(85, 27)
        Me.agregar.TabIndex = 2
        Me.agregar.Text = "Agregar"
        Me.agregar.UseVisualStyleBackColor = True
        '
        'ext
        '
        Me.ext.AutoSize = True
        Me.ext.Checked = True
        Me.ext.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ext.Location = New System.Drawing.Point(193, 83)
        Me.ext.Name = "ext"
        Me.ext.Size = New System.Drawing.Size(107, 27)
        Me.ext.TabIndex = 4
        Me.ext.Text = "Existencias"
        Me.ext.UseVisualStyleBackColor = True
        '
        'cambio
        '
        Me.cambio.AutoSize = True
        Me.cambio.Checked = True
        Me.cambio.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cambio.Location = New System.Drawing.Point(104, 83)
        Me.cambio.Name = "cambio"
        Me.cambio.Size = New System.Drawing.Size(83, 27)
        Me.cambio.TabIndex = 3
        Me.cambio.Text = "Cambio"
        Me.cambio.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Panel2.Controls.Add(Me.Copiarp)
        Me.Panel2.Controls.Add(Me.recargar)
        Me.Panel2.Controls.Add(Me.dardealta)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(600, 63)
        Me.Panel2.TabIndex = 3
        '
        'Copiarp
        '
        Me.Copiarp.BackgroundImage = Global.SICDIN30.My.Resources.Resources.copiar
        Me.Copiarp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Copiarp.Dock = System.Windows.Forms.DockStyle.Right
        Me.Copiarp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Copiarp.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Copiarp.Location = New System.Drawing.Point(438, 0)
        Me.Copiarp.Margin = New System.Windows.Forms.Padding(2)
        Me.Copiarp.Name = "Copiarp"
        Me.Copiarp.Size = New System.Drawing.Size(81, 63)
        Me.Copiarp.TabIndex = 4
        Me.Copiarp.Tag = "Copiar"
        Me.Copiarp.UseVisualStyleBackColor = True
        '
        'recargar
        '
        Me.recargar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.recargar
        Me.recargar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.recargar.Dock = System.Windows.Forms.DockStyle.Right
        Me.recargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.recargar.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.recargar.Location = New System.Drawing.Point(519, 0)
        Me.recargar.Margin = New System.Windows.Forms.Padding(2)
        Me.recargar.Name = "recargar"
        Me.recargar.Size = New System.Drawing.Size(81, 63)
        Me.recargar.TabIndex = 1
        Me.recargar.Tag = "Recargar faltantes"
        Me.recargar.UseVisualStyleBackColor = True
        '
        'dardealta
        '
        Me.dardealta.BackgroundImage = Global.SICDIN30.My.Resources.Resources._1486504366_check_clipboard_select_list_success_ok_tasks_81314
        Me.dardealta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.dardealta.Dock = System.Windows.Forms.DockStyle.Left
        Me.dardealta.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.dardealta.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dardealta.Location = New System.Drawing.Point(0, 0)
        Me.dardealta.Margin = New System.Windows.Forms.Padding(2)
        Me.dardealta.Name = "dardealta"
        Me.dardealta.Size = New System.Drawing.Size(91, 63)
        Me.dardealta.TabIndex = 2
        Me.dardealta.Tag = "Alta artículo nuevo"
        Me.dardealta.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SeleccionaTodoToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(159, 26)
        '
        'SeleccionaTodoToolStripMenuItem
        '
        Me.SeleccionaTodoToolStripMenuItem.Name = "SeleccionaTodoToolStripMenuItem"
        Me.SeleccionaTodoToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.SeleccionaTodoToolStripMenuItem.Text = "Selecciona todo"
        '
        'existencias
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 366)
        Me.ControlBox = False
        Me.Controls.Add(Me.panelexis)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "existencias"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.panelexis.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.falt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.perd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.dglog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelexis As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents dardealta As Button
    Friend WithEvents recargar As Button
    Friend WithEvents Copiarp As Button
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SeleccionaTodoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents falt As DataGridView
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents perd As DataGridView
    Friend WithEvents Ver As DataGridViewImageColumn
    Friend WithEvents Borrar As DataGridViewImageColumn
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents dtlog As DateTimePicker
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents dglog As DataGridView
    Public WithEvents TabControl1 As TabControl
    Public WithEvents del As CheckBox
    Public WithEvents ext As CheckBox
    Public WithEvents cambio As CheckBox
    Public WithEvents agregar As CheckBox
End Class
