<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class pagof
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.nombrec = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgpago = New System.Windows.Forms.DataGridView()
        Me.cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tipo = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.dgpanel = New System.Windows.Forms.Panel()
        Me.dgl = New System.Windows.Forms.DataGridView()
        Me.Can = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Des = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.printd = New System.Windows.Forms.PrintDialog()
        Me.printticket = New System.Drawing.Printing.PrintDocument()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewImageColumn2 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.imagen = New System.Windows.Forms.Button()
        Me.iva = New System.Windows.Forms.Button()
        Me.bticket = New System.Windows.Forms.Button()
        Me.bnota = New System.Windows.Forms.Button()
        Me.asc = New System.Windows.Forms.Button()
        Me.agregar = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Del = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.Aceptar = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.dgpago, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dgpanel.SuspendLayout()
        CType(Me.dgl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(80, 34)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 26)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Subtotal:$0.0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(85, 70)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 26)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Adeudo:$0.0"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Controls.Add(Me.nombrec)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(907, 32)
        Me.Panel1.TabIndex = 6
        '
        'nombrec
        '
        Me.nombrec.AutoSize = True
        Me.nombrec.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nombrec.ForeColor = System.Drawing.Color.White
        Me.nombrec.Location = New System.Drawing.Point(113, 6)
        Me.nombrec.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.nombrec.Name = "nombrec"
        Me.nombrec.Size = New System.Drawing.Size(86, 26)
        Me.nombrec.TabIndex = 0
        Me.nombrec.Text = "Cliente "
        Me.nombrec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(888, 32)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(19, 431)
        Me.Panel2.TabIndex = 7
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 463)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(907, 20)
        Me.Panel4.TabIndex = 8
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 32)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 431)
        Me.Panel3.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(80, 154)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 26)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Pagos"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(111, 136)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 26)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Total:$0.0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(80, 344)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(117, 26)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Resta:$0.0"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'dgpago
        '
        Me.dgpago.AllowUserToAddRows = False
        Me.dgpago.BackgroundColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(159, Byte), Integer))
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgpago.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.dgpago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgpago.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cantidad, Me.Tipo, Me.agregar, Me.Del})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgpago.DefaultCellStyle = DataGridViewCellStyle10
        Me.dgpago.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgpago.Location = New System.Drawing.Point(41, 183)
        Me.dgpago.Margin = New System.Windows.Forms.Padding(2)
        Me.dgpago.Name = "dgpago"
        Me.dgpago.RowHeadersVisible = False
        Me.dgpago.RowHeadersWidth = 51
        Me.dgpago.RowTemplate.Height = 24
        Me.dgpago.Size = New System.Drawing.Size(372, 156)
        Me.dgpago.TabIndex = 14
        '
        'cantidad
        '
        Me.cantidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.cantidad.HeaderText = "Cantidad"
        Me.cantidad.MinimumWidth = 6
        Me.cantidad.Name = "cantidad"
        Me.cantidad.Width = 98
        '
        'Tipo
        '
        Me.Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Tipo.HeaderText = "Tipo"
        Me.Tipo.Items.AddRange(New Object() {"1.-Efectivo", "2.-Tarjeta", "3.-Transferencia", "4.-Descuento"})
        Me.Tipo.MinimumWidth = 6
        Me.Tipo.Name = "Tipo"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'dgpanel
        '
        Me.dgpanel.Controls.Add(Me.dgl)
        Me.dgpanel.Location = New System.Drawing.Point(433, 32)
        Me.dgpanel.Margin = New System.Windows.Forms.Padding(2)
        Me.dgpanel.Name = "dgpanel"
        Me.dgpanel.Size = New System.Drawing.Size(687, 395)
        Me.dgpanel.TabIndex = 15
        '
        'dgl
        '
        Me.dgl.AllowUserToAddRows = False
        Me.dgl.AllowUserToDeleteRows = False
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgl.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgl.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Can, Me.Des})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgl.DefaultCellStyle = DataGridViewCellStyle12
        Me.dgl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgl.Location = New System.Drawing.Point(0, 0)
        Me.dgl.Margin = New System.Windows.Forms.Padding(2)
        Me.dgl.Name = "dgl"
        Me.dgl.ReadOnly = True
        Me.dgl.RowHeadersWidth = 51
        Me.dgl.RowTemplate.Height = 24
        Me.dgl.Size = New System.Drawing.Size(606, 383)
        Me.dgl.TabIndex = 0
        '
        'Can
        '
        Me.Can.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.Can.HeaderText = "Cantidad"
        Me.Can.MinimumWidth = 6
        Me.Can.Name = "Can"
        Me.Can.ReadOnly = True
        Me.Can.Width = 124
        '
        'Des
        '
        Me.Des.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Des.HeaderText = "Descripción"
        Me.Des.MinimumWidth = 6
        Me.Des.Name = "Des"
        Me.Des.ReadOnly = True
        '
        'printd
        '
        Me.printd.UseEXDialog = True
        '
        'printticket
        '
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(57, 104)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(164, 26)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Descuento:$0.0"
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridViewImageColumn1.HeaderText = "Agregar"
        Me.DataGridViewImageColumn1.Image = Global.SICDIN30.My.Resources.Resources.add_icon_icons_com_74429
        Me.DataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn1.MinimumWidth = 6
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.Width = 72
        '
        'DataGridViewImageColumn2
        '
        Me.DataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridViewImageColumn2.HeaderText = "Eliminar"
        Me.DataGridViewImageColumn2.Image = Global.SICDIN30.My.Resources.Resources._1486504830_delete_dustbin_empty_recycle_recycling_remove_trash_81361
        Me.DataGridViewImageColumn2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn2.MinimumWidth = 6
        Me.DataGridViewImageColumn2.Name = "DataGridViewImageColumn2"
        Me.DataGridViewImageColumn2.Width = 71
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Button1.BackgroundImage = Global.SICDIN30.My.Resources.Resources.help
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(328, 140)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(85, 40)
        Me.Button1.TabIndex = 22
        Me.Button1.Tag = ""
        Me.ToolTip1.SetToolTip(Me.Button1, "Ayuda en pagos")
        Me.Button1.UseVisualStyleBackColor = False
        '
        'imagen
        'imagen
        '
        Me.imagen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.imagen.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.imagen.BackgroundImage = Global.SICDIN30.My.Resources.Resources.image
        Me.imagen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.imagen.FlatAppearance.BorderSize = 0
        Me.imagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.imagen.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.imagen.ForeColor = System.Drawing.Color.Black
        Me.imagen.Location = New System.Drawing.Point(1010, 445)
        Me.imagen.Margin = New System.Windows.Forms.Padding(2)
        Me.imagen.Name = "imagen"
        Me.imagen.Size = New System.Drawing.Size(85, 40)
        Me.imagen.TabIndex = 20
        Me.imagen.Tag = "Crear imagen"
        Me.imagen.UseVisualStyleBackColor = False
        '
        'iva
        '
        Me.iva.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.iva.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.iva.BackgroundImage = Global.SICDIN30.My.Resources.Resources.ivono
        Me.iva.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.iva.FlatAppearance.BorderSize = 0
        Me.iva.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.iva.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.iva.ForeColor = System.Drawing.Color.Black
        Me.iva.Location = New System.Drawing.Point(780, 445)
        Me.iva.Margin = New System.Windows.Forms.Padding(2)
        Me.iva.Name = "iva"
        Me.iva.Size = New System.Drawing.Size(85, 40)
        Me.iva.TabIndex = 19
        Me.iva.Tag = "I.V.A."
        Me.iva.UseVisualStyleBackColor = False
        '
        'bticket
        '
        Me.bticket.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bticket.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.bticket.BackgroundImage = Global.SICDIN30.My.Resources.Resources.receipt_106581
        Me.bticket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bticket.FlatAppearance.BorderSize = 0
        Me.bticket.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bticket.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bticket.ForeColor = System.Drawing.Color.Black
        Me.bticket.Location = New System.Drawing.Point(895, 445)
        Me.bticket.Margin = New System.Windows.Forms.Padding(2)
        Me.bticket.Name = "bticket"
        Me.bticket.Size = New System.Drawing.Size(85, 40)
        Me.bticket.TabIndex = 18
        Me.bticket.Tag = "Imprimir ticket"
        Me.bticket.UseVisualStyleBackColor = False
        '
        'bnota
        '
        Me.bnota.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bnota.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.bnota.BackgroundImage = Global.SICDIN30.My.Resources.Resources.printer_icon_icons_com_52926
        Me.bnota.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bnota.FlatAppearance.BorderSize = 0
        Me.bnota.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bnota.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bnota.ForeColor = System.Drawing.Color.Black
        Me.bnota.Location = New System.Drawing.Point(665, 445)
        Me.bnota.Margin = New System.Windows.Forms.Padding(2)
        Me.bnota.Name = "bnota"
        Me.bnota.Size = New System.Drawing.Size(85, 40)
        Me.bnota.TabIndex = 17
        Me.bnota.Tag = "Imprimir nota"
        Me.bnota.UseVisualStyleBackColor = False
        '
        'asc
        '
        Me.asc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.asc.AutoSize = False
        Me.asc.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.asc.BackgroundImage = Global.SICDIN30.My.Resources.Resources.uparrow_arriba_1538
        Me.asc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.asc.FlatAppearance.BorderSize = 0
        Me.asc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.asc.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.asc.ForeColor = System.Drawing.Color.Black
        Me.asc.Location = New System.Drawing.Point(550, 445)
        Me.asc.Margin = New System.Windows.Forms.Padding(2)
        Me.asc.Name = "asc"
        Me.asc.Size = New System.Drawing.Size(85, 40)
        Me.asc.TabIndex = 16
        Me.asc.Tag = "Ascendente"
        Me.asc.UseVisualStyleBackColor = False
        '
        'agregar
        '
        Me.agregar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.agregar.HeaderText = "Agregar"
        Me.agregar.Image = Global.SICDIN30.My.Resources.Resources.add_icon_icons_com_74429
        Me.agregar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.agregar.MinimumWidth = 6
        Me.agregar.Name = "agregar"
        Me.agregar.Width = 72
        '
        'Del
        '
        Me.Del.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.Del.HeaderText = "Eliminar"
        Me.Del.Image = Global.SICDIN30.My.Resources.Resources._1486504830_delete_dustbin_empty_recycle_recycling_remove_trash_81361
        Me.Del.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.Del.MinimumWidth = 6
        Me.Del.Name = "Del"
        Me.Del.Width = 71
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Cancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Cancel.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.FlatAppearance.BorderSize = 0
        Me.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel.ForeColor = System.Drawing.Color.Black
        Me.Cancel.Location = New System.Drawing.Point(235, 445)
        Me.Cancel.Margin = New System.Windows.Forms.Padding(2)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(85, 40)
        Me.Cancel.TabIndex = 1
        Me.Cancel.Tag = "Cancelar"
        Me.Cancel.UseVisualStyleBackColor = False
        '
        'Aceptar
        '
        Me.Aceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Aceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Aceptar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.Aceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Aceptar.FlatAppearance.BorderSize = 0
        Me.Aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Aceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Aceptar.ForeColor = System.Drawing.Color.Black
        Me.Aceptar.Location = New System.Drawing.Point(84, 445)
        Me.Aceptar.Margin = New System.Windows.Forms.Padding(2)
        Me.Aceptar.Name = "Aceptar"
        Me.Aceptar.Size = New System.Drawing.Size(85, 40)
        Me.Aceptar.TabIndex = 0
        Me.Aceptar.Tag = "Aceptar"
        Me.Aceptar.UseVisualStyleBackColor = False
        '
        'pagof
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(1150, 520)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.imagen)
        Me.Controls.Add(Me.iva)
        Me.Controls.Add(Me.bticket)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.bnota)
        Me.Controls.Add(Me.asc)
        Me.Controls.Add(Me.dgpanel)
        Me.Controls.Add(Me.dgpago)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Aceptar)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.Name = "pagof"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgpago, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dgpanel.ResumeLayout(False)
        CType(Me.dgl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Aceptar As Button
    Friend WithEvents Cancel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents dgpago As DataGridView
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents nombrec As Label
    Friend WithEvents dgpanel As Panel
    Friend WithEvents asc As Button
    Friend WithEvents bnota As Button
    Friend WithEvents printd As PrintDialog
    Friend WithEvents printticket As Printing.PrintDocument
    Friend WithEvents Can As DataGridViewTextBoxColumn
    Friend WithEvents Des As DataGridViewTextBoxColumn
    Public WithEvents dgl As DataGridView
    Friend WithEvents DataGridViewImageColumn1 As DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn2 As DataGridViewImageColumn
    Friend WithEvents iva As Button
    Friend WithEvents imagen As Button
    Friend WithEvents Label6 As Label
    Public WithEvents bticket As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents cantidad As DataGridViewTextBoxColumn
    Friend WithEvents Tipo As DataGridViewComboBoxColumn
    Friend WithEvents agregar As DataGridViewImageColumn
    Friend WithEvents Del As DataGridViewImageColumn
End Class
