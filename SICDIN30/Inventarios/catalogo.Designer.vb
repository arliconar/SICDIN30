<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class catalogo
    Inherits Global.System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <Global.System.Diagnostics.DebuggerNonUserCode()>
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
    Private components As Global.System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <Global.System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(catalogo))
        Me.panelcat = New System.Windows.Forms.Panel()
        Me.dgcat = New System.Windows.Forms.DataGridView()
        Me.Verc = New System.Windows.Forms.DataGridViewImageColumn()
        Me.vrap = New System.Windows.Forms.DataGridViewImageColumn()
        Me.AE = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Editar = New System.Windows.Forms.DataGridViewImageColumn()
        Me.menusecundario = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.VentaRapiidaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AVentaNuevaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopiarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DescripciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClaveDescripciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EtiquetaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VerImagenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VerificarExistenciasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PedidaDevoluciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.AltaArtículoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.modicarcontext = New System.Windows.Forms.ToolStripMenuItem()
        Me.EliminarArtículoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.aee = New System.Windows.Forms.CheckBox()
        Me.fecha = New System.Windows.Forms.CheckBox()
        Me.may = New System.Windows.Forms.CheckBox()
        Me.Trabajo = New System.Windows.Forms.CheckBox()
        Me.etiquetas = New System.Windows.Forms.CheckBox()
        Me.existentes = New System.Windows.Forms.CheckBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.sbox = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.busqueda = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewImageColumn2 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewImageColumn3 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewImageColumn4 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panelcat.SuspendLayout()
        CType(Me.dgcat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.menusecundario.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelcat
        '
        Me.panelcat.Controls.Add(Me.dgcat)
        Me.panelcat.Controls.Add(Me.Panel5)
        Me.panelcat.Controls.Add(Me.Panel4)
        Me.panelcat.Controls.Add(Me.Panel2)
        Me.panelcat.Controls.Add(Me.Panel1)
        Me.panelcat.Controls.Add(Me.Panel3)
        Me.panelcat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelcat.Location = New System.Drawing.Point(0, 0)
        Me.panelcat.Margin = New System.Windows.Forms.Padding(2)
        Me.panelcat.Name = "panelcat"
        Me.panelcat.Size = New System.Drawing.Size(830, 457)
        Me.panelcat.TabIndex = 7
        '
        'dgcat
        '
        Me.dgcat.AllowUserToAddRows = False
        Me.dgcat.AllowUserToDeleteRows = False
        Me.dgcat.BackgroundColor = System.Drawing.SystemColors.InactiveCaption
        Me.dgcat.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgcat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgcat.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Verc, Me.vrap, Me.AE, Me.Editar})
        Me.dgcat.ContextMenuStrip = Me.menusecundario
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgcat.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgcat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgcat.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgcat.EnableHeadersVisualStyles = False
        Me.dgcat.Location = New System.Drawing.Point(15, 76)
        Me.dgcat.Margin = New System.Windows.Forms.Padding(2)
        Me.dgcat.Name = "dgcat"
        Me.dgcat.ReadOnly = True
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgcat.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgcat.RowHeadersVisible = False
        Me.dgcat.RowHeadersWidth = 51
        Me.dgcat.RowTemplate.Height = 24
        Me.dgcat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgcat.Size = New System.Drawing.Size(800, 365)
        Me.dgcat.TabIndex = 4
        '
        'Verc
        '
        Me.Verc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.Verc.HeaderText = "Ver"
        Me.Verc.Image = Global.SICDIN30.My.Resources.Resources.zenmap_104119
        Me.Verc.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.Verc.MinimumWidth = 6
        Me.Verc.Name = "Verc"
        Me.Verc.ReadOnly = True
        Me.Verc.Width = 29
        '
        'vrap
        '
        Me.vrap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.vrap.HeaderText = "V.Rap."
        Me.vrap.Image = Global.SICDIN30.My.Resources.Resources.Bagitems_icon_icons_com_51211
        Me.vrap.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.vrap.MinimumWidth = 6
        Me.vrap.Name = "vrap"
        Me.vrap.ReadOnly = True
        Me.vrap.Width = 46
        '
        'AE
        '
        Me.AE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.AE.HeaderText = "A.E."
        Me.AE.Image = Global.SICDIN30.My.Resources.Resources.add_icon_icons_com_74429
        Me.AE.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.AE.MinimumWidth = 6
        Me.AE.Name = "AE"
        Me.AE.ReadOnly = True
        Me.AE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AE.Visible = False
        '
        'Editar
        '
        Me.Editar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.Editar.HeaderText = "Editar"
        Me.Editar.Image = Global.SICDIN30.My.Resources.Resources.edit
        Me.Editar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.Editar.MinimumWidth = 6
        Me.Editar.Name = "Editar"
        Me.Editar.ReadOnly = True
        Me.Editar.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Editar.Visible = False
        '
        'menusecundario
        '
        Me.menusecundario.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.menusecundario.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.menusecundario.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VentaRapiidaToolStripMenuItem, Me.AVentaNuevaToolStripMenuItem, Me.CopiarToolStripMenuItem, Me.VerImagenToolStripMenuItem, Me.VerificarExistenciasToolStripMenuItem, Me.PedidaDevoluciónToolStripMenuItem, Me.ToolStripSeparator1, Me.AltaArtículoToolStripMenuItem, Me.modicarcontext, Me.EliminarArtículoToolStripMenuItem})
        Me.menusecundario.Name = "ContextMenuStrip1"
        Me.menusecundario.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.menusecundario.Size = New System.Drawing.Size(221, 244)
        '
        'VentaRapiidaToolStripMenuItem
        '
        Me.VentaRapiidaToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.Bagitems_icon_icons_com_51211
        Me.VentaRapiidaToolStripMenuItem.Name = "VentaRapiidaToolStripMenuItem"
        Me.VentaRapiidaToolStripMenuItem.Size = New System.Drawing.Size(220, 26)
        Me.VentaRapiidaToolStripMenuItem.Text = "Venta rápida"
        Me.VentaRapiidaToolStripMenuItem.ToolTipText = "Venta rápida de un artículo (Solo menudeo)"
        '
        'AVentaNuevaToolStripMenuItem
        '
        Me.AVentaNuevaToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.ventanueva
        Me.AVentaNuevaToolStripMenuItem.Name = "AVentaNuevaToolStripMenuItem"
        Me.AVentaNuevaToolStripMenuItem.Size = New System.Drawing.Size(220, 26)
        Me.AVentaNuevaToolStripMenuItem.Text = "A venta nueva"
        '
        'CopiarToolStripMenuItem
        '
        Me.CopiarToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClaveToolStripMenuItem, Me.DescripciónToolStripMenuItem, Me.ClaveDescripciónToolStripMenuItem, Me.EtiquetaToolStripMenuItem})
        Me.CopiarToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.pegar
        Me.CopiarToolStripMenuItem.Name = "CopiarToolStripMenuItem"
        Me.CopiarToolStripMenuItem.Size = New System.Drawing.Size(220, 26)
        Me.CopiarToolStripMenuItem.Text = "Copiar"
        '
        'ClaveToolStripMenuItem
        '
        Me.ClaveToolStripMenuItem.Name = "ClaveToolStripMenuItem"
        Me.ClaveToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.ClaveToolStripMenuItem.Text = "Clave"
        '
        'DescripciónToolStripMenuItem
        '
        Me.DescripciónToolStripMenuItem.Name = "DescripciónToolStripMenuItem"
        Me.DescripciónToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.DescripciónToolStripMenuItem.Text = "Descripción"
        '
        'ClaveDescripciónToolStripMenuItem
        '
        Me.ClaveDescripciónToolStripMenuItem.Name = "ClaveDescripciónToolStripMenuItem"
        Me.ClaveDescripciónToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.ClaveDescripciónToolStripMenuItem.Text = "Clave+Descripción"
        '
        'EtiquetaToolStripMenuItem
        '
        Me.EtiquetaToolStripMenuItem.Name = "EtiquetaToolStripMenuItem"
        Me.EtiquetaToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.EtiquetaToolStripMenuItem.Text = "Etiqueta"
        '
        'VerImagenToolStripMenuItem
        '
        Me.VerImagenToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.zenmap_104119
        Me.VerImagenToolStripMenuItem.Name = "VerImagenToolStripMenuItem"
        Me.VerImagenToolStripMenuItem.Size = New System.Drawing.Size(220, 26)
        Me.VerImagenToolStripMenuItem.Text = "Ver imagen"
        '
        'VerificarExistenciasToolStripMenuItem
        '
        Me.VerificarExistenciasToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources._1485476036_artboard_1_78544
        Me.VerificarExistenciasToolStripMenuItem.Name = "VerificarExistenciasToolStripMenuItem"
        Me.VerificarExistenciasToolStripMenuItem.Size = New System.Drawing.Size(220, 26)
        Me.VerificarExistenciasToolStripMenuItem.Text = "Existencias y transferencias"
        '
        'PedidaDevoluciónToolStripMenuItem
        '
        Me.PedidaDevoluciónToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.roto
        Me.PedidaDevoluciónToolStripMenuItem.Name = "PedidaDevoluciónToolStripMenuItem"
        Me.PedidaDevoluciónToolStripMenuItem.Size = New System.Drawing.Size(220, 26)
        Me.PedidaDevoluciónToolStripMenuItem.Text = "Perdida"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(217, 6)
        '
        'AltaArtículoToolStripMenuItem
        '
        Me.AltaArtículoToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources._1486504366_check_clipboard_select_list_success_ok_tasks_81314
        Me.AltaArtículoToolStripMenuItem.Name = "AltaArtículoToolStripMenuItem"
        Me.AltaArtículoToolStripMenuItem.Size = New System.Drawing.Size(220, 26)
        Me.AltaArtículoToolStripMenuItem.Text = "Alta artículo"
        '
        'modicarcontext
        '
        Me.modicarcontext.Image = Global.SICDIN30.My.Resources.Resources.edit
        Me.modicarcontext.Name = "modicarcontext"
        Me.modicarcontext.Size = New System.Drawing.Size(220, 26)
        Me.modicarcontext.Text = "Modificar artículo"
        '
        'EliminarArtículoToolStripMenuItem
        '
        Me.EliminarArtículoToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources._1486504830_delete_dustbin_empty_recycle_recycling_remove_trash_81361
        Me.EliminarArtículoToolStripMenuItem.Name = "EliminarArtículoToolStripMenuItem"
        Me.EliminarArtículoToolStripMenuItem.Size = New System.Drawing.Size(220, 26)
        Me.EliminarArtículoToolStripMenuItem.Text = "Eliminar artículo"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Controls.Add(Me.busqueda)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(15, 16)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(800, 60)
        Me.Panel5.TabIndex = 10
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.aee)
        Me.Panel6.Controls.Add(Me.fecha)
        Me.Panel6.Controls.Add(Me.may)
        Me.Panel6.Controls.Add(Me.Trabajo)
        Me.Panel6.Controls.Add(Me.etiquetas)
        Me.Panel6.Controls.Add(Me.existentes)
        Me.Panel6.Controls.Add(Me.Panel7)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(56, 0)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(744, 60)
        Me.Panel6.TabIndex = 8
        '
        'aee
        '
        Me.aee.AutoSize = True
        Me.aee.Dock = System.Windows.Forms.DockStyle.Left
        Me.aee.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aee.Location = New System.Drawing.Point(500, 32)
        Me.aee.Margin = New System.Windows.Forms.Padding(2)
        Me.aee.Name = "aee"
        Me.aee.Size = New System.Drawing.Size(110, 28)
        Me.aee.TabIndex = 6
        Me.aee.Text = "Inventario"
        Me.aee.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.aee.UseVisualStyleBackColor = True
        '
        'fecha
        '
        Me.fecha.AutoSize = True
        Me.fecha.Dock = System.Windows.Forms.DockStyle.Left
        Me.fecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fecha.Location = New System.Drawing.Point(417, 32)
        Me.fecha.Margin = New System.Windows.Forms.Padding(2)
        Me.fecha.Name = "fecha"
        Me.fecha.Size = New System.Drawing.Size(83, 28)
        Me.fecha.TabIndex = 9
        Me.fecha.Text = "Fecha"
        Me.fecha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.fecha.UseVisualStyleBackColor = True
        '
        'may
        '
        Me.may.AutoSize = True
        Me.may.Dock = System.Windows.Forms.DockStyle.Left
        Me.may.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.may.Location = New System.Drawing.Point(314, 32)
        Me.may.Margin = New System.Windows.Forms.Padding(2)
        Me.may.Name = "may"
        Me.may.Size = New System.Drawing.Size(103, 28)
        Me.may.TabIndex = 5
        Me.may.Text = "Mayoreo"
        Me.may.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.may.UseVisualStyleBackColor = True
        '
        'Trabajo
        '
        Me.Trabajo.AutoSize = True
        Me.Trabajo.Dock = System.Windows.Forms.DockStyle.Left
        Me.Trabajo.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Trabajo.Location = New System.Drawing.Point(221, 32)
        Me.Trabajo.Margin = New System.Windows.Forms.Padding(2)
        Me.Trabajo.Name = "Trabajo"
        Me.Trabajo.Size = New System.Drawing.Size(93, 28)
        Me.Trabajo.TabIndex = 4
        Me.Trabajo.Text = "Trabajo"
        Me.Trabajo.UseVisualStyleBackColor = True
        '
        'etiquetas
        '
        Me.etiquetas.AutoSize = True
        Me.etiquetas.Dock = System.Windows.Forms.DockStyle.Left
        Me.etiquetas.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.etiquetas.Location = New System.Drawing.Point(115, 32)
        Me.etiquetas.Margin = New System.Windows.Forms.Padding(2)
        Me.etiquetas.Name = "etiquetas"
        Me.etiquetas.Size = New System.Drawing.Size(106, 28)
        Me.etiquetas.TabIndex = 3
        Me.etiquetas.Text = "Etiquetas"
        Me.etiquetas.UseVisualStyleBackColor = True
        '
        'existentes
        '
        Me.existentes.AutoSize = True
        Me.existentes.Checked = True
        Me.existentes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.existentes.Dock = System.Windows.Forms.DockStyle.Left
        Me.existentes.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.existentes.Location = New System.Drawing.Point(0, 32)
        Me.existentes.Margin = New System.Windows.Forms.Padding(2)
        Me.existentes.Name = "existentes"
        Me.existentes.Size = New System.Drawing.Size(115, 28)
        Me.existentes.TabIndex = 2
        Me.existentes.Text = "Existentes"
        Me.existentes.UseVisualStyleBackColor = True
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.sbox)
        Me.Panel7.Controls.Add(Me.Button1)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(744, 32)
        Me.Panel7.TabIndex = 8
        '
        'sbox
        '
        Me.sbox.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.sbox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sbox.Font = New System.Drawing.Font("Arial Black", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sbox.Location = New System.Drawing.Point(0, 0)
        Me.sbox.Margin = New System.Windows.Forms.Padding(2)
        Me.sbox.Name = "sbox"
        Me.sbox.Size = New System.Drawing.Size(712, 33)
        Me.sbox.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.SICDIN30.My.Resources.Resources.formula
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Button1.Location = New System.Drawing.Point(712, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(32, 32)
        Me.Button1.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.Button1, "Fórmulas")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'busqueda
        '
        Me.busqueda.BackgroundImage = CType(resources.GetObject("busqueda.BackgroundImage"), System.Drawing.Image)
        Me.busqueda.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.busqueda.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.busqueda.Dock = System.Windows.Forms.DockStyle.Left
        Me.busqueda.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.busqueda.Location = New System.Drawing.Point(0, 0)
        Me.busqueda.Margin = New System.Windows.Forms.Padding(2)
        Me.busqueda.Name = "busqueda"
        Me.busqueda.Size = New System.Drawing.Size(56, 60)
        Me.busqueda.TabIndex = 1
        Me.busqueda.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.busqueda.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(15, 441)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(800, 16)
        Me.Panel4.TabIndex = 9
        Me.Panel4.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(15, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(800, 16)
        Me.Panel2.TabIndex = 8
        Me.Panel2.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Busqueda de artículo"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(15, 457)
        Me.Panel1.TabIndex = 7
        Me.Panel1.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(815, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(15, 457)
        Me.Panel3.TabIndex = 6
        Me.Panel3.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridViewImageColumn1.HeaderText = "Ver"
        Me.DataGridViewImageColumn1.Image = Global.SICDIN30.My.Resources.Resources.zenmap_104119
        Me.DataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn1.MinimumWidth = 6
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ReadOnly = True
        '
        'DataGridViewImageColumn2
        '
        Me.DataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridViewImageColumn2.HeaderText = "A.E."
        Me.DataGridViewImageColumn2.Image = Global.SICDIN30.My.Resources.Resources.add_icon_icons_com_74429
        Me.DataGridViewImageColumn2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn2.MinimumWidth = 6
        Me.DataGridViewImageColumn2.Name = "DataGridViewImageColumn2"
        Me.DataGridViewImageColumn2.ReadOnly = True
        Me.DataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewImageColumn2.Visible = False
        '
        'DataGridViewImageColumn3
        '
        Me.DataGridViewImageColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridViewImageColumn3.HeaderText = "Editar"
        Me.DataGridViewImageColumn3.Image = Global.SICDIN30.My.Resources.Resources.edit
        Me.DataGridViewImageColumn3.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn3.MinimumWidth = 6
        Me.DataGridViewImageColumn3.Name = "DataGridViewImageColumn3"
        Me.DataGridViewImageColumn3.ReadOnly = True
        Me.DataGridViewImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewImageColumn3.Visible = False
        '
        'DataGridViewImageColumn4
        '
        Me.DataGridViewImageColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridViewImageColumn4.HeaderText = "Editar"
        Me.DataGridViewImageColumn4.Image = Global.SICDIN30.My.Resources.Resources.edit
        Me.DataGridViewImageColumn4.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn4.MinimumWidth = 6
        Me.DataGridViewImageColumn4.Name = "DataGridViewImageColumn4"
        Me.DataGridViewImageColumn4.ReadOnly = True
        Me.DataGridViewImageColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewImageColumn4.Visible = False
        '
        'catalogo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(830, 457)
        Me.ControlBox = False
        Me.Controls.Add(Me.panelcat)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "catalogo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "catalogo"
        Me.panelcat.ResumeLayout(False)
        CType(Me.dgcat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.menusecundario.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelcat As Panel
    Friend WithEvents dgcat As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents busqueda As Button
    Friend WithEvents existentes As CheckBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents etiquetas As CheckBox
    Friend WithEvents Trabajo As CheckBox
    Friend WithEvents DataGridViewImageColumn1 As DataGridViewImageColumn
    Friend WithEvents may As CheckBox
    Public WithEvents aee As CheckBox
    Public WithEvents fecha As CheckBox
    Friend WithEvents DataGridViewImageColumn2 As DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn3 As DataGridViewImageColumn
    Friend WithEvents Verc As DataGridViewImageColumn
    Friend WithEvents vrap As DataGridViewImageColumn
    Friend WithEvents AE As DataGridViewImageColumn
    Friend WithEvents Editar As DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn4 As DataGridViewImageColumn
    Public WithEvents sbox As TextBox
    Public WithEvents menusecundario As ContextMenuStrip
    Friend WithEvents VentaRapiidaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AVentaNuevaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopiarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DescripciónToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClaveDescripciónToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EtiquetaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents VerImagenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents VerificarExistenciasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PedidaDevoluciónToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents AltaArtículoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents modicarcontext As ToolStripMenuItem
    Friend WithEvents EliminarArtículoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button1 As Button
    Friend WithEvents ToolTip1 As ToolTip
End Class
