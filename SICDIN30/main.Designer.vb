<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(main))
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.estadomain = New System.Windows.Forms.ToolStripStatusLabel()
        Me.updatet = New System.Windows.Forms.ToolStripSplitButton()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.copy = New System.Windows.Forms.Button()
        Me.panelbutton = New System.Windows.Forms.Panel()
        Me.whatsbut = New System.Windows.Forms.Button()
        Me.convbutton = New System.Windows.Forms.Button()
        Me.Setupb = New System.Windows.Forms.Button()
        Me.invb = New System.Windows.Forms.Button()
        Me.sqlb = New System.Windows.Forms.Button()
        Me.panelventbut = New System.Windows.Forms.Panel()
        Me.Reports = New System.Windows.Forms.Button()
        Me.clntbutton = New System.Windows.Forms.Button()
        Me.usuarios = New System.Windows.Forms.Button()
        Me.notasbutton = New System.Windows.Forms.Button()
        Me.libretabutton = New System.Windows.Forms.Button()
        Me.Reparacion = New System.Windows.Forms.Button()
        Me.Venta = New System.Windows.Forms.Button()
        Me.catbut = New System.Windows.Forms.Button()
        Me.ppos = New System.Windows.Forms.Panel()
        Me.UPDATECLOUD = New System.Windows.Forms.Timer(Me.components)
        Me.CHECKCLOUD = New System.Windows.Forms.Timer(Me.components)
        Me.helpcontext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BuscarNotaDeVentaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarNotaDeReparaciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarApartadosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VentasGuardadasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.copier = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MaskedTextBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.StatusStrip.SuspendLayout()
        Me.panelbutton.SuspendLayout()
        Me.ppos.SuspendLayout()
        Me.helpcontext.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        Me.StatusStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.estadomain, Me.updatet, Me.ToolStripStatusLabel1})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 427)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1151, 26)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'estadomain
        '
        Me.estadomain.Name = "estadomain"
        Me.estadomain.Size = New System.Drawing.Size(42, 21)
        Me.estadomain.Text = "Estado"
        '
        'updatet
        '
        Me.updatet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.updatet.Image = Global.SICDIN30.My.Resources.Resources.recargar
        Me.updatet.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.updatet.Name = "updatet"
        Me.updatet.Size = New System.Drawing.Size(36, 24)
        Me.updatet.Text = "ToolStripSplitButton1"
        Me.updatet.ToolTipText = "Forzar actualización de nube"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(1058, 21)
        Me.ToolStripStatusLabel1.Spring = True
        '
        'Button1
        '
        Me.Button1.AutoSize = True
        Me.Button1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Button1.BackgroundImage = Global.SICDIN30.My.Resources.Resources.help
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(949, 0)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(68, 58)
        Me.Button1.TabIndex = 17
        Me.Button1.Tag = "Ayuda"
        Me.ToolTip.SetToolTip(Me.Button1, "Ayuda")
        Me.Button1.UseVisualStyleBackColor = False
        '
        'copy
        '
        Me.copy.AutoSize = True
        Me.copy.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.copy.BackgroundImage = Global.SICDIN30.My.Resources.Resources.copiar
        Me.copy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.copy.Dock = System.Windows.Forms.DockStyle.Left
        Me.copy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.copy.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copy.Location = New System.Drawing.Point(881, 0)
        Me.copy.Margin = New System.Windows.Forms.Padding(2)
        Me.copy.Name = "copy"
        Me.copy.Size = New System.Drawing.Size(68, 58)
        Me.copy.TabIndex = 18
        Me.copy.Tag = "Ayuda"
        Me.ToolTip.SetToolTip(Me.copy, "Ayuda")
        Me.copy.UseVisualStyleBackColor = False
        '
        'panelbutton
        '
        Me.panelbutton.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.panelbutton.Controls.Add(Me.Button1)
        Me.panelbutton.Controls.Add(Me.copy)
        Me.panelbutton.Controls.Add(Me.whatsbut)
        Me.panelbutton.Controls.Add(Me.convbutton)
        Me.panelbutton.Controls.Add(Me.Setupb)
        Me.panelbutton.Controls.Add(Me.invb)
        Me.panelbutton.Controls.Add(Me.sqlb)
        Me.panelbutton.Controls.Add(Me.panelventbut)
        Me.panelbutton.Controls.Add(Me.Reports)
        Me.panelbutton.Controls.Add(Me.clntbutton)
        Me.panelbutton.Controls.Add(Me.usuarios)
        Me.panelbutton.Controls.Add(Me.notasbutton)
        Me.panelbutton.Controls.Add(Me.libretabutton)
        Me.panelbutton.Controls.Add(Me.Reparacion)
        Me.panelbutton.Controls.Add(Me.Venta)
        Me.panelbutton.Controls.Add(Me.catbut)
        Me.panelbutton.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelbutton.Location = New System.Drawing.Point(0, 0)
        Me.panelbutton.Margin = New System.Windows.Forms.Padding(2)
        Me.panelbutton.Name = "panelbutton"
        Me.panelbutton.Size = New System.Drawing.Size(1151, 58)
        Me.panelbutton.TabIndex = 10
        '
        'whatsbut
        '
        Me.whatsbut.AutoSize = True
        Me.whatsbut.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.whatsbut.BackgroundImage = Global.SICDIN30.My.Resources.Resources.Whatsapp
        Me.whatsbut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.whatsbut.Dock = System.Windows.Forms.DockStyle.Left
        Me.whatsbut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.whatsbut.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.whatsbut.Location = New System.Drawing.Point(813, 0)
        Me.whatsbut.Margin = New System.Windows.Forms.Padding(2)
        Me.whatsbut.Name = "whatsbut"
        Me.whatsbut.Size = New System.Drawing.Size(68, 58)
        Me.whatsbut.TabIndex = 15
        Me.whatsbut.Tag = "Configuration"
        Me.whatsbut.UseVisualStyleBackColor = False
        '
        'convbutton
        '
        Me.convbutton.AutoSize = True
        Me.convbutton.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.convbutton.BackgroundImage = Global.SICDIN30.My.Resources.Resources.calc
        Me.convbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.convbutton.Dock = System.Windows.Forms.DockStyle.Left
        Me.convbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.convbutton.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.convbutton.Location = New System.Drawing.Point(745, 0)
        Me.convbutton.Margin = New System.Windows.Forms.Padding(2)
        Me.convbutton.Name = "convbutton"
        Me.convbutton.Size = New System.Drawing.Size(68, 58)
        Me.convbutton.TabIndex = 12
        Me.convbutton.Tag = "Configuration"
        Me.convbutton.UseVisualStyleBackColor = False
        '
        'Setupb
        '
        Me.Setupb.AutoSize = True
        Me.Setupb.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Setupb.BackgroundImage = Global.SICDIN30.My.Resources.Resources.setup
        Me.Setupb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Setupb.Dock = System.Windows.Forms.DockStyle.Left
        Me.Setupb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Setupb.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Setupb.Location = New System.Drawing.Point(677, 0)
        Me.Setupb.Margin = New System.Windows.Forms.Padding(2)
        Me.Setupb.Name = "Setupb"
        Me.Setupb.Size = New System.Drawing.Size(68, 58)
        Me.Setupb.TabIndex = 11
        Me.Setupb.Tag = "Configuration"
        Me.Setupb.UseVisualStyleBackColor = False
        '
        'invb
        '
        Me.invb.AutoSize = True
        Me.invb.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.invb.BackgroundImage = Global.SICDIN30.My.Resources.Resources.Inventory_maintenance_25374
        Me.invb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.invb.Dock = System.Windows.Forms.DockStyle.Left
        Me.invb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.invb.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.invb.Location = New System.Drawing.Point(609, 0)
        Me.invb.Margin = New System.Windows.Forms.Padding(2)
        Me.invb.Name = "invb"
        Me.invb.Size = New System.Drawing.Size(68, 58)
        Me.invb.TabIndex = 0
        Me.invb.UseVisualStyleBackColor = False
        '
        'sqlb
        '
        Me.sqlb.AutoSize = True
        Me.sqlb.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.sqlb.BackgroundImage = Global.SICDIN30.My.Resources.Resources.Sql_runner_35757
        Me.sqlb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.sqlb.Dock = System.Windows.Forms.DockStyle.Left
        Me.sqlb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sqlb.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sqlb.Location = New System.Drawing.Point(541, 0)
        Me.sqlb.Margin = New System.Windows.Forms.Padding(2)
        Me.sqlb.Name = "sqlb"
        Me.sqlb.Size = New System.Drawing.Size(68, 58)
        Me.sqlb.TabIndex = 8
        Me.sqlb.UseVisualStyleBackColor = False
        '
        'panelventbut
        '
        Me.panelventbut.AutoSize = True
        Me.panelventbut.Dock = System.Windows.Forms.DockStyle.Right
        Me.panelventbut.Location = New System.Drawing.Point(1151, 0)
        Me.panelventbut.Margin = New System.Windows.Forms.Padding(2)
        Me.panelventbut.Name = "panelventbut"
        Me.panelventbut.Size = New System.Drawing.Size(0, 58)
        Me.panelventbut.TabIndex = 5
        '
        'Reports
        '
        Me.Reports.AutoSize = True
        Me.Reports.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Reports.BackgroundImage = Global.SICDIN30.My.Resources.Resources.reportes
        Me.Reports.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Reports.Dock = System.Windows.Forms.DockStyle.Left
        Me.Reports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Reports.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Reports.Location = New System.Drawing.Point(473, 0)
        Me.Reports.Margin = New System.Windows.Forms.Padding(2)
        Me.Reports.Name = "Reports"
        Me.Reports.Size = New System.Drawing.Size(68, 58)
        Me.Reports.TabIndex = 16
        Me.Reports.Tag = "Reportes"
        Me.Reports.UseVisualStyleBackColor = False
        '
        'clntbutton
        '
        Me.clntbutton.AutoSize = True
        Me.clntbutton.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.clntbutton.BackgroundImage = Global.SICDIN30.My.Resources.Resources.founder_icon_icons_com_76996
        Me.clntbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.clntbutton.Dock = System.Windows.Forms.DockStyle.Left
        Me.clntbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.clntbutton.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clntbutton.Location = New System.Drawing.Point(405, 0)
        Me.clntbutton.Margin = New System.Windows.Forms.Padding(2)
        Me.clntbutton.Name = "clntbutton"
        Me.clntbutton.Size = New System.Drawing.Size(68, 58)
        Me.clntbutton.TabIndex = 4
        Me.clntbutton.Tag = "Clientes"
        Me.clntbutton.UseVisualStyleBackColor = False
        '
        'usuarios
        '
        Me.usuarios.AutoSize = True
        Me.usuarios.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.usuarios.BackgroundImage = Global.SICDIN30.My.Resources.Resources.usuarios
        Me.usuarios.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.usuarios.Dock = System.Windows.Forms.DockStyle.Left
        Me.usuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.usuarios.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.usuarios.Location = New System.Drawing.Point(337, 0)
        Me.usuarios.Margin = New System.Windows.Forms.Padding(2)
        Me.usuarios.Name = "usuarios"
        Me.usuarios.Size = New System.Drawing.Size(68, 58)
        Me.usuarios.TabIndex = 10
        Me.usuarios.Tag = "Usuarios"
        Me.usuarios.UseVisualStyleBackColor = False
        '
        'notasbutton
        '
        Me.notasbutton.AutoSize = True
        Me.notasbutton.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.notasbutton.BackgroundImage = Global.SICDIN30.My.Resources.Resources.folderorangeopen_93000
        Me.notasbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.notasbutton.Dock = System.Windows.Forms.DockStyle.Left
        Me.notasbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.notasbutton.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.notasbutton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.notasbutton.Location = New System.Drawing.Point(269, 0)
        Me.notasbutton.Margin = New System.Windows.Forms.Padding(2)
        Me.notasbutton.Name = "notasbutton"
        Me.notasbutton.Size = New System.Drawing.Size(68, 58)
        Me.notasbutton.TabIndex = 7
        Me.notasbutton.Tag = "Notas"
        Me.notasbutton.Text = "F5"
        Me.notasbutton.UseCompatibleTextRendering = True
        Me.notasbutton.UseVisualStyleBackColor = False
        '
        'libretabutton
        '
        Me.libretabutton.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.libretabutton.BackgroundImage = Global.SICDIN30.My.Resources.Resources.libreta
        Me.libretabutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.libretabutton.Dock = System.Windows.Forms.DockStyle.Left
        Me.libretabutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.libretabutton.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.libretabutton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.libretabutton.Location = New System.Drawing.Point(201, 0)
        Me.libretabutton.Margin = New System.Windows.Forms.Padding(2)
        Me.libretabutton.Name = "libretabutton"
        Me.libretabutton.Size = New System.Drawing.Size(68, 58)
        Me.libretabutton.TabIndex = 9
        Me.libretabutton.Tag = "Libreta de ventas"
        Me.libretabutton.Text = "F4"
        Me.libretabutton.UseCompatibleTextRendering = True
        Me.libretabutton.UseVisualStyleBackColor = False
        '
        'Reparacion
        '
        Me.Reparacion.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Reparacion.BackgroundImage = Global.SICDIN30.My.Resources.Resources.technical_support_2_icon_icons_com_52811
        Me.Reparacion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Reparacion.Dock = System.Windows.Forms.DockStyle.Left
        Me.Reparacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Reparacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Reparacion.Location = New System.Drawing.Point(136, 0)
        Me.Reparacion.Margin = New System.Windows.Forms.Padding(2)
        Me.Reparacion.Name = "Reparacion"
        Me.Reparacion.Size = New System.Drawing.Size(65, 58)
        Me.Reparacion.TabIndex = 14
        Me.Reparacion.Tag = "Reparación"
        Me.Reparacion.Text = "F3"
        Me.Reparacion.UseVisualStyleBackColor = False
        '
        'Venta
        '
        Me.Venta.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Venta.BackgroundImage = Global.SICDIN30.My.Resources.Resources.ic_shopping_cart_128_28698
        Me.Venta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Venta.Dock = System.Windows.Forms.DockStyle.Left
        Me.Venta.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Venta.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Venta.Location = New System.Drawing.Point(68, 0)
        Me.Venta.Margin = New System.Windows.Forms.Padding(2)
        Me.Venta.Name = "Venta"
        Me.Venta.Size = New System.Drawing.Size(68, 58)
        Me.Venta.TabIndex = 13
        Me.Venta.Tag = "Ventas"
        Me.Venta.Text = "F2"
        Me.Venta.UseVisualStyleBackColor = False
        '
        'catbut
        '
        Me.catbut.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.catbut.BackgroundImage = Global.SICDIN30.My.Resources.Resources.catalog256_24878
        Me.catbut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.catbut.Dock = System.Windows.Forms.DockStyle.Left
        Me.catbut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.catbut.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.catbut.Location = New System.Drawing.Point(0, 0)
        Me.catbut.Margin = New System.Windows.Forms.Padding(2)
        Me.catbut.Name = "catbut"
        Me.catbut.Size = New System.Drawing.Size(68, 58)
        Me.catbut.TabIndex = 1
        Me.catbut.Tag = "Catálogo"
        Me.catbut.Text = "F1"
        Me.catbut.UseVisualStyleBackColor = False
        '
        'ppos
        '
        Me.ppos.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ppos.Controls.Add(Me.MaskedTextBox1)
        Me.ppos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ppos.Location = New System.Drawing.Point(0, 58)
        Me.ppos.Margin = New System.Windows.Forms.Padding(2)
        Me.ppos.Name = "ppos"
        Me.ppos.Size = New System.Drawing.Size(1151, 369)
        Me.ppos.TabIndex = 14
        '
        'UPDATECLOUD
        '
        Me.UPDATECLOUD.Interval = 3600000
        '
        'CHECKCLOUD
        '
        Me.CHECKCLOUD.Enabled = True
        Me.CHECKCLOUD.Interval = 60000
        '
        'helpcontext
        '
        Me.helpcontext.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.helpcontext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BuscarNotaDeVentaToolStripMenuItem, Me.BuscarNotaDeReparaciónToolStripMenuItem, Me.BuscarApartadosToolStripMenuItem, Me.VentasGuardadasToolStripMenuItem})
        Me.helpcontext.Name = "helpcontext"
        Me.helpcontext.Size = New System.Drawing.Size(323, 140)
        '
        'BuscarNotaDeVentaToolStripMenuItem
        '
        Me.BuscarNotaDeVentaToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BuscarNotaDeVentaToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.ic_shopping_cart_128_28698
        Me.BuscarNotaDeVentaToolStripMenuItem.Name = "BuscarNotaDeVentaToolStripMenuItem"
        Me.BuscarNotaDeVentaToolStripMenuItem.Size = New System.Drawing.Size(322, 34)
        Me.BuscarNotaDeVentaToolStripMenuItem.Text = "Buscar nota de venta"
        '
        'BuscarNotaDeReparaciónToolStripMenuItem
        '
        Me.BuscarNotaDeReparaciónToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BuscarNotaDeReparaciónToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.technical_support_2_icon_icons_com_52811
        Me.BuscarNotaDeReparaciónToolStripMenuItem.Name = "BuscarNotaDeReparaciónToolStripMenuItem"
        Me.BuscarNotaDeReparaciónToolStripMenuItem.Size = New System.Drawing.Size(322, 34)
        Me.BuscarNotaDeReparaciónToolStripMenuItem.Text = "Buscar nota de reparación"
        '
        'BuscarApartadosToolStripMenuItem
        '
        Me.BuscarApartadosToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BuscarApartadosToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.hoy
        Me.BuscarApartadosToolStripMenuItem.Name = "BuscarApartadosToolStripMenuItem"
        Me.BuscarApartadosToolStripMenuItem.Size = New System.Drawing.Size(322, 34)
        Me.BuscarApartadosToolStripMenuItem.Text = "Buscar apartados"
        '
        'VentasGuardadasToolStripMenuItem
        '
        Me.VentasGuardadasToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VentasGuardadasToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.ventanueva
        Me.VentasGuardadasToolStripMenuItem.Name = "VentasGuardadasToolStripMenuItem"
        Me.VentasGuardadasToolStripMenuItem.Size = New System.Drawing.Size(322, 34)
        Me.VentasGuardadasToolStripMenuItem.Text = "Ventas guardadas"
        '
        'copier
        '
        Me.copier.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.copier.Name = "copier"
        Me.copier.Size = New System.Drawing.Size(61, 4)
        '
        'MaskedTextBox1
        '
        Me.MaskedTextBox1.Location = New System.Drawing.Point(923, 229)
        Me.MaskedTextBox1.Name = "MaskedTextBox1"
        Me.MaskedTextBox1.Size = New System.Drawing.Size(100, 20)
        Me.MaskedTextBox1.TabIndex = 0
        '
        'main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1151, 453)
        Me.Controls.Add(Me.ppos)
        Me.Controls.Add(Me.panelbutton)
        Me.Controls.Add(Me.StatusStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "main"
        Me.Text = "SICDIN 3.1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.panelbutton.ResumeLayout(False)
        Me.panelbutton.PerformLayout()
        Me.ppos.ResumeLayout(False)
        Me.ppos.PerformLayout()
        Me.helpcontext.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As Global.System.Windows.Forms.ToolTip
    Friend WithEvents panelbutton As Panel
    Friend WithEvents invb As Button
    Friend WithEvents catbut As Button
    Friend WithEvents ppos As Panel
    Friend WithEvents clntbutton As Button
    Friend WithEvents panelventbut As Panel
    Friend WithEvents notasbutton As Button
    Friend WithEvents sqlb As Button
    Friend WithEvents libretabutton As Button
    Friend WithEvents usuarios As Button
    Friend WithEvents Setupb As Button
    Friend WithEvents convbutton As Button
    Public WithEvents estadomain As ToolStripStatusLabel
    Friend WithEvents updatet As ToolStripSplitButton
    Friend WithEvents Venta As Button
    Friend WithEvents Reparacion As Button
    Friend WithEvents UPDATECLOUD As Timer
    Friend WithEvents whatsbut As Button
    Friend WithEvents CHECKCLOUD As Timer
    Public WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Public WithEvents StatusStrip As StatusStrip
    Friend WithEvents Reports As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents helpcontext As ContextMenuStrip
    Friend WithEvents BuscarNotaDeVentaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BuscarNotaDeReparaciónToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BuscarApartadosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents VentasGuardadasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents copy As Button
    Friend WithEvents copier As ContextMenuStrip
    Friend WithEvents MaskedTextBox1 As MaskedTextBox
End Class
