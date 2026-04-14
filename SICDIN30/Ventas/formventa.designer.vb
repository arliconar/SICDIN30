<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class formvent
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditarArtículoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AgregarExistenciasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopiarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PegarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.foliolabel = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Ventaopen = New System.Windows.Forms.Panel()
        Me.panelb = New System.Windows.Forms.Panel()
        Me.menuext = New System.Windows.Forms.Button()
        Me.Guardarv = New System.Windows.Forms.Button()
        Me.okv = New System.Windows.Forms.Button()
        Me.Cancelarv = New System.Windows.Forms.Button()
        Me.pagoll = New System.Windows.Forms.GroupBox()
        Me.pagol = New System.Windows.Forms.Label()
        Me.gbtotal = New System.Windows.Forms.GroupBox()
        Me.totallabel = New System.Windows.Forms.Label()
        Me.gbadeudo = New System.Windows.Forms.GroupBox()
        Me.adeudol = New System.Windows.Forms.Label()
        Me.gbsub = New System.Windows.Forms.GroupBox()
        Me.subtotallabel = New System.Windows.Forms.Label()
        Me.Panelai = New System.Windows.Forms.Panel()
        Me.bclint = New System.Windows.Forms.Panel()
        Me.verc = New System.Windows.Forms.Button()
        Me.addv = New System.Windows.Forms.Button()
        Me.clngb = New System.Windows.Forms.GroupBox()
        Me.comboclnt = New System.Windows.Forms.ComboBox()
        Me.dg1 = New System.Windows.Forms.DataGridView()
        Me.can = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cancel = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.des = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pq = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tpv = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.impv = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.del = New System.Windows.Forms.DataGridViewImageColumn()
        Me.tra = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.einf = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.exis = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.found = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.clave = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.notas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.panelventa = New System.Windows.Forms.Panel()
        Me.panelfloat = New System.Windows.Forms.Panel()
        Me.Cotizacion = New System.Windows.Forms.Button()
        Me.Apartado = New System.Windows.Forms.Button()
        Me.block = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.estatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.paneldia = New System.Windows.Forms.Panel()
        Me.imgen = New System.Windows.Forms.Button()
        Me.print = New System.Windows.Forms.Button()
        Me.acepdi = New System.Windows.Forms.Button()
        Me.printd = New System.Windows.Forms.PrintDialog()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Ventaopen.SuspendLayout()
        Me.panelb.SuspendLayout()
        Me.pagoll.SuspendLayout()
        Me.gbtotal.SuspendLayout()
        Me.gbadeudo.SuspendLayout()
        Me.gbsub.SuspendLayout()
        Me.Panelai.SuspendLayout()
        Me.bclint.SuspendLayout()
        Me.clngb.SuspendLayout()
        CType(Me.dg1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelventa.SuspendLayout()
        Me.panelfloat.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.paneldia.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditarArtículoToolStripMenuItem, Me.AgregarExistenciasToolStripMenuItem, Me.CopiarToolStripMenuItem, Me.PegarToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(212, 108)
        '
        'EditarArtículoToolStripMenuItem
        '
        Me.EditarArtículoToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.edit
        Me.EditarArtículoToolStripMenuItem.Name = "EditarArtículoToolStripMenuItem"
        Me.EditarArtículoToolStripMenuItem.Size = New System.Drawing.Size(211, 26)
        Me.EditarArtículoToolStripMenuItem.Text = "Editar Artículo"
        '
        'AgregarExistenciasToolStripMenuItem
        '
        Me.AgregarExistenciasToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.add_icon_icons_com_74429
        Me.AgregarExistenciasToolStripMenuItem.Name = "AgregarExistenciasToolStripMenuItem"
        Me.AgregarExistenciasToolStripMenuItem.Size = New System.Drawing.Size(211, 26)
        Me.AgregarExistenciasToolStripMenuItem.Text = "Agregar existencias"
        '
        'CopiarToolStripMenuItem
        '
        Me.CopiarToolStripMenuItem.Name = "CopiarToolStripMenuItem"
        Me.CopiarToolStripMenuItem.Size = New System.Drawing.Size(211, 26)
        Me.CopiarToolStripMenuItem.Text = "Copiar"
        '
        'PegarToolStripMenuItem
        '
        Me.PegarToolStripMenuItem.Name = "PegarToolStripMenuItem"
        Me.PegarToolStripMenuItem.Size = New System.Drawing.Size(211, 26)
        Me.PegarToolStripMenuItem.Text = "Pegar"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 2000
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Panel1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(1259, 25)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(20, 495)
        Me.Panel3.TabIndex = 7
        Me.Panel3.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(20, 495)
        Me.Panel1.TabIndex = 7
        Me.Panel1.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 25)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(20, 495)
        Me.Panel2.TabIndex = 8
        Me.Panel2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Controls.Add(Me.foliolabel)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1279, 25)
        Me.Panel4.TabIndex = 9
        Me.Panel4.Visible = False
        '
        'foliolabel
        '
        Me.foliolabel.AutoSize = True
        Me.foliolabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.foliolabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.foliolabel.Location = New System.Drawing.Point(0, 0)
        Me.foliolabel.Name = "foliolabel"
        Me.foliolabel.Size = New System.Drawing.Size(45, 16)
        Me.foliolabel.TabIndex = 0
        Me.foliolabel.Text = "Venta:"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(0, 520)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1279, 25)
        Me.Panel5.TabIndex = 10
        Me.Panel5.Visible = False
        '
        'Ventaopen
        '
        Me.Ventaopen.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Ventaopen.Controls.Add(Me.panelb)
        Me.Ventaopen.Controls.Add(Me.pagoll)
        Me.Ventaopen.Controls.Add(Me.gbtotal)
        Me.Ventaopen.Controls.Add(Me.gbadeudo)
        Me.Ventaopen.Controls.Add(Me.gbsub)
        Me.Ventaopen.Controls.Add(Me.Panelai)
        Me.Ventaopen.Controls.Add(Me.clngb)
        Me.Ventaopen.Dock = System.Windows.Forms.DockStyle.Top
        Me.Ventaopen.Location = New System.Drawing.Point(0, 0)
        Me.Ventaopen.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Ventaopen.Name = "Ventaopen"
        Me.Ventaopen.Size = New System.Drawing.Size(1239, 87)
        Me.Ventaopen.TabIndex = 8
        '
        'panelb
        '
        Me.panelb.Controls.Add(Me.menuext)
        Me.panelb.Controls.Add(Me.Guardarv)
        Me.panelb.Controls.Add(Me.okv)
        Me.panelb.Controls.Add(Me.Cancelarv)
        Me.panelb.Dock = System.Windows.Forms.DockStyle.Right
        Me.panelb.Location = New System.Drawing.Point(839, 0)
        Me.panelb.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.panelb.Name = "panelb"
        Me.panelb.Size = New System.Drawing.Size(400, 87)
        Me.panelb.TabIndex = 11
        '
        'menuext
        '
        Me.menuext.AutoSize = True
        Me.menuext.BackgroundImage = Global.SICDIN30.My.Resources.Resources.dots
        Me.menuext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.menuext.Dock = System.Windows.Forms.DockStyle.Right
        Me.menuext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.menuext.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.menuext.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.menuext.Location = New System.Drawing.Point(113, 0)
        Me.menuext.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.menuext.Name = "menuext"
        Me.menuext.Size = New System.Drawing.Size(37, 87)
        Me.menuext.TabIndex = 10
        Me.menuext.Tag = "Bloquear"
        Me.menuext.UseVisualStyleBackColor = True
        '
        'Guardarv
        '
        Me.Guardarv.BackgroundImage = Global.SICDIN30.My.Resources.Resources.Save_as_37111
        Me.Guardarv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Guardarv.Dock = System.Windows.Forms.DockStyle.Right
        Me.Guardarv.FlatAppearance.BorderSize = 0
        Me.Guardarv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Guardarv.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Guardarv.ForeColor = System.Drawing.Color.Black
        Me.Guardarv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Guardarv.Location = New System.Drawing.Point(150, 0)
        Me.Guardarv.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Guardarv.Name = "Guardarv"
        Me.Guardarv.Size = New System.Drawing.Size(87, 87)
        Me.Guardarv.TabIndex = 7
        Me.Guardarv.Tag = "Guardar"
        Me.Guardarv.UseVisualStyleBackColor = True
        '
        'okv
        '
        Me.okv.AutoSize = True
        Me.okv.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.okv.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.okv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.okv.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.okv.Dock = System.Windows.Forms.DockStyle.Right
        Me.okv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.okv.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.okv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.okv.Location = New System.Drawing.Point(237, 0)
        Me.okv.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.okv.Name = "okv"
        Me.okv.Size = New System.Drawing.Size(88, 87)
        Me.okv.TabIndex = 8
        Me.okv.Tag = "Aceptar"
        Me.okv.UseVisualStyleBackColor = False
        '
        'Cancelarv
        '
        Me.Cancelarv.AutoSize = True
        Me.Cancelarv.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Cancelarv.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.Cancelarv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Cancelarv.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancelarv.Dock = System.Windows.Forms.DockStyle.Right
        Me.Cancelarv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancelarv.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancelarv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Cancelarv.Location = New System.Drawing.Point(325, 0)
        Me.Cancelarv.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Cancelarv.Name = "Cancelarv"
        Me.Cancelarv.Size = New System.Drawing.Size(75, 87)
        Me.Cancelarv.TabIndex = 6
        Me.Cancelarv.Tag = "Cancelar"
        Me.Cancelarv.UseVisualStyleBackColor = False
        '
        'pagoll
        '
        Me.pagoll.AutoSize = True
        Me.pagoll.Controls.Add(Me.pagol)
        Me.pagoll.Dock = System.Windows.Forms.DockStyle.Left
        Me.pagoll.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!)
        Me.pagoll.Location = New System.Drawing.Point(1112, 0)
        Me.pagoll.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.pagoll.Name = "pagoll"
        Me.pagoll.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.pagoll.Size = New System.Drawing.Size(114, 87)
        Me.pagoll.TabIndex = 17
        Me.pagoll.TabStop = False
        Me.pagoll.Text = "Pago:"
        Me.pagoll.Visible = False
        '
        'pagol
        '
        Me.pagol.AutoSize = True
        Me.pagol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pagol.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!)
        Me.pagol.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pagol.Location = New System.Drawing.Point(3, 33)
        Me.pagol.Name = "pagol"
        Me.pagol.Size = New System.Drawing.Size(108, 46)
        Me.pagol.TabIndex = 3
        Me.pagol.Text = "$ 0.0"
        Me.pagol.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'gbtotal
        '
        Me.gbtotal.AutoSize = True
        Me.gbtotal.Controls.Add(Me.totallabel)
        Me.gbtotal.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!)
        Me.gbtotal.Location = New System.Drawing.Point(998, 0)
        Me.gbtotal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbtotal.Name = "gbtotal"
        Me.gbtotal.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbtotal.Size = New System.Drawing.Size(114, 87)
        Me.gbtotal.TabIndex = 12
        Me.gbtotal.TabStop = False
        Me.gbtotal.Text = "Total:"
        '
        'totallabel
        '
        Me.totallabel.AutoSize = True
        Me.totallabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.totallabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!)
        Me.totallabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.totallabel.Location = New System.Drawing.Point(3, 33)
        Me.totallabel.Name = "totallabel"
        Me.totallabel.Size = New System.Drawing.Size(108, 46)
        Me.totallabel.TabIndex = 3
        Me.totallabel.Text = "$ 0.0"
        Me.totallabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'gbadeudo
        '
        Me.gbadeudo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbadeudo.Controls.Add(Me.adeudol)
        Me.gbadeudo.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbadeudo.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!)
        Me.gbadeudo.Location = New System.Drawing.Point(859, 0)
        Me.gbadeudo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbadeudo.Name = "gbadeudo"
        Me.gbadeudo.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbadeudo.Size = New System.Drawing.Size(139, 87)
        Me.gbadeudo.TabIndex = 13
        Me.gbadeudo.TabStop = False
        Me.gbadeudo.Text = "Adeudo:"
        '
        'adeudol
        '
        Me.adeudol.AutoSize = True
        Me.adeudol.Dock = System.Windows.Forms.DockStyle.Left
        Me.adeudol.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!)
        Me.adeudol.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.adeudol.Location = New System.Drawing.Point(3, 33)
        Me.adeudol.Name = "adeudol"
        Me.adeudol.Size = New System.Drawing.Size(108, 46)
        Me.adeudol.TabIndex = 4
        Me.adeudol.Text = "$ 0.0"
        Me.adeudol.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'gbsub
        '
        Me.gbsub.Controls.Add(Me.subtotallabel)
        Me.gbsub.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbsub.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!)
        Me.gbsub.Location = New System.Drawing.Point(716, 0)
        Me.gbsub.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbsub.Name = "gbsub"
        Me.gbsub.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbsub.Size = New System.Drawing.Size(143, 87)
        Me.gbsub.TabIndex = 14
        Me.gbsub.TabStop = False
        Me.gbsub.Text = "Subtotal:"
        '
        'subtotallabel
        '
        Me.subtotallabel.AutoSize = True
        Me.subtotallabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.subtotallabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!)
        Me.subtotallabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.subtotallabel.Location = New System.Drawing.Point(3, 33)
        Me.subtotallabel.Name = "subtotallabel"
        Me.subtotallabel.Size = New System.Drawing.Size(108, 46)
        Me.subtotallabel.TabIndex = 6
        Me.subtotallabel.Text = "$ 0.0"
        Me.subtotallabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Panelai
        '
        Me.Panelai.Controls.Add(Me.bclint)
        Me.Panelai.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panelai.Location = New System.Drawing.Point(572, 0)
        Me.Panelai.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panelai.Name = "Panelai"
        Me.Panelai.Size = New System.Drawing.Size(144, 87)
        Me.Panelai.TabIndex = 16
        '
        'bclint
        '
        Me.bclint.Controls.Add(Me.verc)
        Me.bclint.Controls.Add(Me.addv)
        Me.bclint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.bclint.Location = New System.Drawing.Point(0, 0)
        Me.bclint.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.bclint.Name = "bclint"
        Me.bclint.Size = New System.Drawing.Size(144, 87)
        Me.bclint.TabIndex = 1
        '
        'verc
        '
        Me.verc.BackgroundImage = Global.SICDIN30.My.Resources.Resources.zenmap_104119
        Me.verc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.verc.Dock = System.Windows.Forms.DockStyle.Top
        Me.verc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.verc.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!)
        Me.verc.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.verc.Location = New System.Drawing.Point(0, 47)
        Me.verc.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.verc.Name = "verc"
        Me.verc.Size = New System.Drawing.Size(144, 39)
        Me.verc.TabIndex = 4
        Me.verc.Tag = "Ver"
        Me.verc.UseVisualStyleBackColor = True
        '
        'addv
        '
        Me.addv.BackgroundImage = Global.SICDIN30.My.Resources.Resources.business_application_addmale_useradd_insert_add_user_client_2312
        Me.addv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.addv.Dock = System.Windows.Forms.DockStyle.Top
        Me.addv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.addv.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!)
        Me.addv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.addv.Location = New System.Drawing.Point(0, 0)
        Me.addv.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.addv.Name = "addv"
        Me.addv.Size = New System.Drawing.Size(144, 47)
        Me.addv.TabIndex = 3
        Me.addv.Tag = "Cliente no registrado"
        Me.addv.UseVisualStyleBackColor = True
        '
        'clngb
        '
        Me.clngb.Controls.Add(Me.comboclnt)
        Me.clngb.Dock = System.Windows.Forms.DockStyle.Left
        Me.clngb.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!)
        Me.clngb.Location = New System.Drawing.Point(0, 0)
        Me.clngb.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.clngb.Name = "clngb"
        Me.clngb.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.clngb.Size = New System.Drawing.Size(572, 87)
        Me.clngb.TabIndex = 15
        Me.clngb.TabStop = False
        Me.clngb.Text = "Cliente:"
        '
        'comboclnt
        '
        Me.comboclnt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.comboclnt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.comboclnt.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.comboclnt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.comboclnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.comboclnt.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Bold)
        Me.comboclnt.FormattingEnabled = True
        Me.comboclnt.Location = New System.Drawing.Point(3, 33)
        Me.comboclnt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.comboclnt.Name = "comboclnt"
        Me.comboclnt.Size = New System.Drawing.Size(566, 39)
        Me.comboclnt.TabIndex = 4
        Me.comboclnt.Text = "1:Mostrador"
        '
        'dg1
        '
        Me.dg1.AllowUserToAddRows = False
        Me.dg1.AllowUserToDeleteRows = False
        Me.dg1.AllowUserToOrderColumns = True
        Me.dg1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption
        Me.dg1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dg1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(159, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dg1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.can, Me.cancel, Me.des, Me.pq, Me.Tpv, Me.impv, Me.del, Me.tra, Me.einf, Me.exis, Me.found, Me.clave, Me.notas})
        Me.dg1.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dg1.DefaultCellStyle = DataGridViewCellStyle2
        Me.dg1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dg1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dg1.Location = New System.Drawing.Point(0, 87)
        Me.dg1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dg1.Name = "dg1"
        Me.dg1.RowHeadersWidth = 51
        Me.dg1.RowTemplate.Height = 40
        Me.dg1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dg1.Size = New System.Drawing.Size(1239, 354)
        Me.dg1.TabIndex = 7
        '
        'can
        '
        Me.can.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.can.Frozen = True
        Me.can.HeaderText = "Cantidad"
        Me.can.MinimumWidth = 6
        Me.can.Name = "can"
        Me.can.Width = 158
        '
        'cancel
        '
        Me.cancel.HeaderText = "cancel"
        Me.cancel.MinimumWidth = 6
        Me.cancel.Name = "cancel"
        Me.cancel.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cancel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.cancel.Visible = False
        Me.cancel.Width = 125
        '
        'des
        '
        Me.des.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.des.HeaderText = "Descripción"
        Me.des.MinimumWidth = 6
        Me.des.Name = "des"
        '
        'pq
        '
        Me.pq.HeaderText = "Paquete"
        Me.pq.MinimumWidth = 6
        Me.pq.Name = "pq"
        Me.pq.ReadOnly = True
        Me.pq.Width = 125
        '
        'Tpv
        '
        Me.Tpv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Tpv.HeaderText = "Precio"
        Me.Tpv.MinimumWidth = 6
        Me.Tpv.Name = "Tpv"
        Me.Tpv.Width = 101
        '
        'impv
        '
        Me.impv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.impv.HeaderText = "Importe"
        Me.impv.MinimumWidth = 6
        Me.impv.Name = "impv"
        Me.impv.ReadOnly = True
        Me.impv.Width = 138
        '
        'del
        '
        Me.del.HeaderText = "Del."
        Me.del.Image = Global.SICDIN30.My.Resources.Resources._1486504830_delete_dustbin_empty_recycle_recycling_remove_trash_81361
        Me.del.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.del.MinimumWidth = 6
        Me.del.Name = "del"
        Me.del.Width = 125
        '
        'tra
        '
        Me.tra.HeaderText = "trabajo"
        Me.tra.MinimumWidth = 6
        Me.tra.Name = "tra"
        Me.tra.Visible = False
        Me.tra.Width = 125
        '
        'einf
        '
        Me.einf.HeaderText = "einf"
        Me.einf.MinimumWidth = 6
        Me.einf.Name = "einf"
        Me.einf.Visible = False
        Me.einf.Width = 125
        '
        'exis
        '
        Me.exis.HeaderText = "exis"
        Me.exis.MinimumWidth = 6
        Me.exis.Name = "exis"
        Me.exis.Visible = False
        Me.exis.Width = 125
        '
        'found
        '
        Me.found.HeaderText = "found"
        Me.found.MinimumWidth = 6
        Me.found.Name = "found"
        Me.found.Visible = False
        Me.found.Width = 125
        '
        'clave
        '
        Me.clave.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.clave.HeaderText = "clave"
        Me.clave.MinimumWidth = 6
        Me.clave.Name = "clave"
        Me.clave.Visible = False
        Me.clave.Width = 125
        '
        'notas
        '
        Me.notas.HeaderText = "notas"
        Me.notas.MinimumWidth = 6
        Me.notas.Name = "notas"
        Me.notas.Visible = False
        Me.notas.Width = 125
        '
        'panelventa
        '
        Me.panelventa.Controls.Add(Me.panelfloat)
        Me.panelventa.Controls.Add(Me.StatusStrip1)
        Me.panelventa.Controls.Add(Me.dg1)
        Me.panelventa.Controls.Add(Me.Ventaopen)
        Me.panelventa.Controls.Add(Me.paneldia)
        Me.panelventa.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelventa.Location = New System.Drawing.Point(20, 25)
        Me.panelventa.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.panelventa.Name = "panelventa"
        Me.panelventa.Size = New System.Drawing.Size(1239, 495)
        Me.panelventa.TabIndex = 0
        '
        'panelfloat
        '
        Me.panelfloat.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.panelfloat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panelfloat.Controls.Add(Me.Cotizacion)
        Me.panelfloat.Controls.Add(Me.Apartado)
        Me.panelfloat.Controls.Add(Me.block)
        Me.panelfloat.Location = New System.Drawing.Point(759, 161)
        Me.panelfloat.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.panelfloat.Name = "panelfloat"
        Me.panelfloat.Size = New System.Drawing.Size(316, 89)
        Me.panelfloat.TabIndex = 11
        Me.panelfloat.Visible = False
        '
        'Cotizacion
        '
        Me.Cotizacion.AutoSize = True
        Me.Cotizacion.BackgroundImage = Global.SICDIN30.My.Resources.Resources._1485476036_artboard_1_78544
        Me.Cotizacion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Cotizacion.Dock = System.Windows.Forms.DockStyle.Left
        Me.Cotizacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cotizacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cotizacion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Cotizacion.Location = New System.Drawing.Point(208, 0)
        Me.Cotizacion.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Cotizacion.Name = "Cotizacion"
        Me.Cotizacion.Size = New System.Drawing.Size(97, 85)
        Me.Cotizacion.TabIndex = 11
        Me.Cotizacion.Tag = "Cotización"
        Me.Cotizacion.UseVisualStyleBackColor = True
        '
        'Apartado
        '
        Me.Apartado.AutoSize = True
        Me.Apartado.BackgroundImage = Global.SICDIN30.My.Resources.Resources.canasta
        Me.Apartado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Apartado.Dock = System.Windows.Forms.DockStyle.Left
        Me.Apartado.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Apartado.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Apartado.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Apartado.Location = New System.Drawing.Point(111, 0)
        Me.Apartado.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Apartado.Name = "Apartado"
        Me.Apartado.Size = New System.Drawing.Size(97, 85)
        Me.Apartado.TabIndex = 10
        Me.Apartado.Tag = "Apartar"
        Me.Apartado.UseVisualStyleBackColor = True
        '
        'block
        '
        Me.block.AutoSize = True
        Me.block.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.block.BackgroundImage = Global.SICDIN30.My.Resources.Resources._1458264596_authorisation_lock_padlock_safe_password_privacy_security_icon_icons_com_55333
        Me.block.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.block.Dock = System.Windows.Forms.DockStyle.Left
        Me.block.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.block.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.block.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.block.Location = New System.Drawing.Point(0, 0)
        Me.block.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.block.Name = "block"
        Me.block.Size = New System.Drawing.Size(111, 85)
        Me.block.TabIndex = 9
        Me.block.Tag = "Bloquear"
        Me.block.UseVisualStyleBackColor = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.estatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 415)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 13, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1239, 26)
        Me.StatusStrip1.TabIndex = 9
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'estatus
        '
        Me.estatus.Name = "estatus"
        Me.estatus.Size = New System.Drawing.Size(13, 20)
        Me.estatus.Text = " "
        '
        'paneldia
        '
        Me.paneldia.Controls.Add(Me.imgen)
        Me.paneldia.Controls.Add(Me.print)
        Me.paneldia.Controls.Add(Me.acepdi)
        Me.paneldia.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.paneldia.Location = New System.Drawing.Point(0, 441)
        Me.paneldia.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.paneldia.Name = "paneldia"
        Me.paneldia.Size = New System.Drawing.Size(1239, 54)
        Me.paneldia.TabIndex = 10
        Me.paneldia.Visible = False
        '
        'imgen
        '
        Me.imgen.BackgroundImage = Global.SICDIN30.My.Resources.Resources.image
        Me.imgen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.imgen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.imgen.Location = New System.Drawing.Point(947, 0)
        Me.imgen.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.imgen.Name = "imgen"
        Me.imgen.Size = New System.Drawing.Size(292, 54)
        Me.imgen.TabIndex = 2
        Me.imgen.Tag = "Imprimir"
        Me.imgen.UseVisualStyleBackColor = True
        Me.imgen.Visible = False
        '
        'print
        '
        Me.print.BackgroundImage = Global.SICDIN30.My.Resources.Resources.printer_icon_icons_com_52926
        Me.print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.print.Dock = System.Windows.Forms.DockStyle.Left
        Me.print.Location = New System.Drawing.Point(323, 0)
        Me.print.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.print.Name = "print"
        Me.print.Size = New System.Drawing.Size(624, 54)
        Me.print.TabIndex = 1
        Me.print.Tag = "Imprimir"
        Me.print.UseVisualStyleBackColor = True
        Me.print.Visible = False
        '
        'acepdi
        '
        Me.acepdi.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.acepdi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.acepdi.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.acepdi.Dock = System.Windows.Forms.DockStyle.Left
        Me.acepdi.Location = New System.Drawing.Point(0, 0)
        Me.acepdi.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.acepdi.Name = "acepdi"
        Me.acepdi.Size = New System.Drawing.Size(323, 54)
        Me.acepdi.TabIndex = 0
        Me.acepdi.Tag = "Salir"
        Me.acepdi.UseVisualStyleBackColor = True
        Me.acepdi.Visible = False
        '
        'printd
        '
        Me.printd.UseEXDialog = True
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.HeaderText = "Column1"
        Me.DataGridViewImageColumn1.Image = Global.SICDIN30.My.Resources.Resources._1486504830_delete_dustbin_empty_recycle_recycling_remove_trash_81361
        Me.DataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn1.MinimumWidth = 6
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.Width = 125
        '
        'formvent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1279, 545)
        Me.ControlBox = False
        Me.Controls.Add(Me.panelventa)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "formvent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "9"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Ventaopen.ResumeLayout(False)
        Me.Ventaopen.PerformLayout()
        Me.panelb.ResumeLayout(False)
        Me.panelb.PerformLayout()
        Me.pagoll.ResumeLayout(False)
        Me.pagoll.PerformLayout()
        Me.gbtotal.ResumeLayout(False)
        Me.gbtotal.PerformLayout()
        Me.gbadeudo.ResumeLayout(False)
        Me.gbadeudo.PerformLayout()
        Me.gbsub.ResumeLayout(False)
        Me.gbsub.PerformLayout()
        Me.Panelai.ResumeLayout(False)
        Me.bclint.ResumeLayout(False)
        Me.clngb.ResumeLayout(False)
        CType(Me.dg1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelventa.ResumeLayout(False)
        Me.panelventa.PerformLayout()
        Me.panelfloat.ResumeLayout(False)
        Me.panelfloat.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.paneldia.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewImageColumn1 As DataGridViewImageColumn
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents EditarArtículoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AgregarExistenciasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents foliolabel As Label
    Friend WithEvents Panel5 As Panel
    Public WithEvents Ventaopen As Panel
    Friend WithEvents pagoll As GroupBox
    Friend WithEvents pagol As Label
    Friend WithEvents gbtotal As GroupBox
    Friend WithEvents totallabel As Label
    Friend WithEvents panelb As Panel
    Friend WithEvents block As Button
    Friend WithEvents okv As Button
    Friend WithEvents Guardarv As Button
    Friend WithEvents Apartado As Button
    Friend WithEvents Cancelarv As Button
    Friend WithEvents gbadeudo As GroupBox
    Friend WithEvents adeudol As Label
    Friend WithEvents gbsub As GroupBox
    Friend WithEvents subtotallabel As Label
    Public WithEvents Panelai As Panel
    Friend WithEvents bclint As Panel
    Friend WithEvents verc As Button
    Friend WithEvents addv As Button
    Public WithEvents clngb As GroupBox
    Public WithEvents comboclnt As ComboBox
    Public WithEvents dg1 As DataGridView
    Public WithEvents panelventa As Panel
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents estatus As ToolStripStatusLabel
    Friend WithEvents paneldia As Panel
    Friend WithEvents print As Button
    Friend WithEvents acepdi As Button
    Friend WithEvents printd As PrintDialog
    Friend WithEvents imgen As Button
    Friend WithEvents can As DataGridViewTextBoxColumn
    Friend WithEvents cancel As DataGridViewCheckBoxColumn
    Friend WithEvents des As DataGridViewTextBoxColumn
    Friend WithEvents pq As DataGridViewTextBoxColumn
    Friend WithEvents Tpv As DataGridViewComboBoxColumn
    Friend WithEvents impv As DataGridViewTextBoxColumn
    Friend WithEvents del As DataGridViewImageColumn
    Friend WithEvents tra As DataGridViewCheckBoxColumn
    Friend WithEvents einf As DataGridViewCheckBoxColumn
    Friend WithEvents exis As DataGridViewTextBoxColumn
    Friend WithEvents found As DataGridViewCheckBoxColumn
    Friend WithEvents clave As DataGridViewTextBoxColumn
    Friend WithEvents notas As DataGridViewTextBoxColumn
    Friend WithEvents Cotizacion As Button
    Friend WithEvents panelfloat As Panel
    Friend WithEvents menuext As Button
    Friend WithEvents Timer2 As Timer
    Friend WithEvents CopiarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PegarToolStripMenuItem As ToolStripMenuItem
End Class
