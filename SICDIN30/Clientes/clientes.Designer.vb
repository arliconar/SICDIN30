<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class clientes
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.panelclientes = New System.Windows.Forms.Panel()
        Me.clntmov = New System.Windows.Forms.DataGridView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PagarAdeudoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.filtros = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dp2 = New System.Windows.Forms.DateTimePicker()
        Me.dp1 = New System.Windows.Forms.DateTimePicker()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.nmov = New System.Windows.Forms.TextBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.dg = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.sbox = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pagaradeudo = New System.Windows.Forms.Button()
        Me.modadeudo = New System.Windows.Forms.Button()
        Me.delclnt = New System.Windows.Forms.Button()
        Me.modclnt = New System.Windows.Forms.Button()
        Me.Agregar = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Abrirmov = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.panelclientes.SuspendLayout()
        CType(Me.clntmov, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelclientes
        '
        Me.panelclientes.Controls.Add(Me.clntmov)
        Me.panelclientes.Controls.Add(Me.Panel4)
        Me.panelclientes.Controls.Add(Me.dg)
        Me.panelclientes.Controls.Add(Me.Panel2)
        Me.panelclientes.Controls.Add(Me.Panel1)
        Me.panelclientes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelclientes.Location = New System.Drawing.Point(0, 0)
        Me.panelclientes.Margin = New System.Windows.Forms.Padding(2)
        Me.panelclientes.Name = "panelclientes"
        Me.panelclientes.Size = New System.Drawing.Size(731, 366)
        Me.panelclientes.TabIndex = 0
        '
        'clntmov
        '
        Me.clntmov.AllowUserToAddRows = False
        Me.clntmov.AllowUserToDeleteRows = False
        Me.clntmov.BackgroundColor = System.Drawing.SystemColors.InactiveCaption
        Me.clntmov.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.clntmov.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Abrirmov})
        Me.clntmov.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.clntmov.DefaultCellStyle = DataGridViewCellStyle5
        Me.clntmov.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clntmov.Location = New System.Drawing.Point(0, 303)
        Me.clntmov.Margin = New System.Windows.Forms.Padding(2)
        Me.clntmov.MultiSelect = False
        Me.clntmov.Name = "clntmov"
        Me.clntmov.ReadOnly = True
        Me.clntmov.RowHeadersWidth = 51
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.clntmov.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.clntmov.RowTemplate.Height = 24
        Me.clntmov.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.clntmov.Size = New System.Drawing.Size(731, 63)
        Me.clntmov.TabIndex = 6
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PagarAdeudoToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(152, 30)
        '
        'PagarAdeudoToolStripMenuItem
        '
        Me.PagarAdeudoToolStripMenuItem.Image = Global.SICDIN30.My.Resources.Resources.pagar1
        Me.PagarAdeudoToolStripMenuItem.Name = "PagarAdeudoToolStripMenuItem"
        Me.PagarAdeudoToolStripMenuItem.Size = New System.Drawing.Size(151, 26)
        Me.PagarAdeudoToolStripMenuItem.Text = "Pagar adeudo"
        '
        'Panel4
        '
        Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel4.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.filtros)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.dp2)
        Me.Panel4.Controls.Add(Me.dp1)
        Me.Panel4.Controls.Add(Me.RadioButton3)
        Me.Panel4.Controls.Add(Me.RadioButton2)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.nmov)
        Me.Panel4.Controls.Add(Me.RadioButton1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 262)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(731, 41)
        Me.Panel4.TabIndex = 5
        '
        'filtros
        '
        Me.filtros.BackgroundImage = Global.SICDIN30.My.Resources.Resources.loupe_78956
        Me.filtros.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.filtros.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.filtros.Location = New System.Drawing.Point(664, 18)
        Me.filtros.Name = "filtros"
        Me.filtros.Size = New System.Drawing.Size(43, 23)
        Me.filtros.TabIndex = 14
        Me.filtros.Tag = "Buscar"
        Me.filtros.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(431, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(13, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "a"
        '
        'dp2
        '
        Me.dp2.Enabled = False
        Me.dp2.Location = New System.Drawing.Point(450, 16)
        Me.dp2.Name = "dp2"
        Me.dp2.Size = New System.Drawing.Size(147, 20)
        Me.dp2.TabIndex = 12
        '
        'dp1
        '
        Me.dp1.Enabled = False
        Me.dp1.Location = New System.Drawing.Point(278, 16)
        Me.dp1.Name = "dp1"
        Me.dp1.Size = New System.Drawing.Size(147, 20)
        Me.dp1.TabIndex = 11
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(603, 18)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(55, 17)
        Me.RadioButton3.TabIndex = 10
        Me.RadioButton3.Text = "Todos"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(195, 18)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(77, 17)
        Me.RadioButton2.TabIndex = 9
        Me.RadioButton2.Text = "Fecha: Del"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(124, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "movimientos"
        '
        'nmov
        '
        Me.nmov.Location = New System.Drawing.Point(67, 16)
        Me.nmov.Name = "nmov"
        Me.nmov.Size = New System.Drawing.Size(51, 20)
        Me.nmov.TabIndex = 7
        Me.nmov.Text = "10"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(10, 18)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(62, 17)
        Me.RadioButton1.TabIndex = 6
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Últimos:"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.AllowUserToDeleteRows = False
        Me.dg.BackgroundColor = System.Drawing.SystemColors.InactiveCaption
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dg.DefaultCellStyle = DataGridViewCellStyle1
        Me.dg.Dock = System.Windows.Forms.DockStyle.Top
        Me.dg.Location = New System.Drawing.Point(0, 95)
        Me.dg.Margin = New System.Windows.Forms.Padding(2)
        Me.dg.MultiSelect = False
        Me.dg.Name = "dg"
        Me.dg.ReadOnly = True
        Me.dg.RowHeadersWidth = 51
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.dg.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dg.RowTemplate.Height = 24
        Me.dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dg.Size = New System.Drawing.Size(731, 167)
        Me.dg.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.sbox)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 63)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(731, 32)
        Me.Panel2.TabIndex = 3
        '
        'sbox
        '
        Me.sbox.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.sbox.Dock = System.Windows.Forms.DockStyle.Top
        Me.sbox.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sbox.Location = New System.Drawing.Point(43, 0)
        Me.sbox.Margin = New System.Windows.Forms.Padding(2)
        Me.sbox.Name = "sbox"
        Me.sbox.Size = New System.Drawing.Size(688, 32)
        Me.sbox.TabIndex = 4
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.PictureBox1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(43, 32)
        Me.Panel3.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.PictureBox1.BackgroundImage = Global.SICDIN30.My.Resources.Resources.loupe_78956
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(43, 32)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Panel1.Controls.Add(Me.pagaradeudo)
        Me.Panel1.Controls.Add(Me.modadeudo)
        Me.Panel1.Controls.Add(Me.delclnt)
        Me.Panel1.Controls.Add(Me.modclnt)
        Me.Panel1.Controls.Add(Me.Agregar)
        Me.Panel1.Controls.Add(Me.Button4)
        Me.Panel1.Controls.Add(Me.Button5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(731, 63)
        Me.Panel1.TabIndex = 0
        '
        'pagaradeudo
        '
        Me.pagaradeudo.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.pagaradeudo.BackgroundImage = Global.SICDIN30.My.Resources.Resources.pagar
        Me.pagaradeudo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pagaradeudo.Dock = System.Windows.Forms.DockStyle.Right
        Me.pagaradeudo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.pagaradeudo.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pagaradeudo.Location = New System.Drawing.Point(491, 0)
        Me.pagaradeudo.Margin = New System.Windows.Forms.Padding(2)
        Me.pagaradeudo.Name = "pagaradeudo"
        Me.pagaradeudo.Size = New System.Drawing.Size(80, 63)
        Me.pagaradeudo.TabIndex = 6
        Me.pagaradeudo.Tag = "Pagar adeudo"
        Me.pagaradeudo.UseVisualStyleBackColor = False
        '
        'modadeudo
        '
        Me.modadeudo.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.modadeudo.BackgroundImage = Global.SICDIN30.My.Resources.Resources.business_color_handout_icon_icons_com_53455
        Me.modadeudo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.modadeudo.Dock = System.Windows.Forms.DockStyle.Right
        Me.modadeudo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.modadeudo.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modadeudo.Location = New System.Drawing.Point(571, 0)
        Me.modadeudo.Margin = New System.Windows.Forms.Padding(2)
        Me.modadeudo.Name = "modadeudo"
        Me.modadeudo.Size = New System.Drawing.Size(80, 63)
        Me.modadeudo.TabIndex = 3
        Me.modadeudo.Tag = "Modificar adeudo"
        Me.modadeudo.UseVisualStyleBackColor = False
        '
        'delclnt
        '
        Me.delclnt.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.delclnt.BackgroundImage = Global.SICDIN30.My.Resources.Resources.delete_delete_deleteusers_delete_male_user_maleclient_2348
        Me.delclnt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.delclnt.Dock = System.Windows.Forms.DockStyle.Left
        Me.delclnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.delclnt.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.delclnt.Location = New System.Drawing.Point(240, 0)
        Me.delclnt.Margin = New System.Windows.Forms.Padding(2)
        Me.delclnt.Name = "delclnt"
        Me.delclnt.Size = New System.Drawing.Size(80, 63)
        Me.delclnt.TabIndex = 2
        Me.delclnt.Tag = "Eliminar"
        Me.delclnt.UseVisualStyleBackColor = False
        '
        'modclnt
        '
        Me.modclnt.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.modclnt.BackgroundImage = Global.SICDIN30.My.Resources.Resources.businessapplication_edit_male_user_thepencil_theclient_negocio_2321
        Me.modclnt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.modclnt.Dock = System.Windows.Forms.DockStyle.Left
        Me.modclnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.modclnt.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modclnt.Location = New System.Drawing.Point(160, 0)
        Me.modclnt.Margin = New System.Windows.Forms.Padding(2)
        Me.modclnt.Name = "modclnt"
        Me.modclnt.Size = New System.Drawing.Size(80, 63)
        Me.modclnt.TabIndex = 1
        Me.modclnt.Tag = "Modificar"
        Me.modclnt.UseVisualStyleBackColor = False
        '
        'Agregar
        '
        Me.Agregar.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Agregar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.business_application_addmale_useradd_insert_add_user_client_2312
        Me.Agregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Agregar.Dock = System.Windows.Forms.DockStyle.Left
        Me.Agregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Agregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Agregar.Location = New System.Drawing.Point(80, 0)
        Me.Agregar.Margin = New System.Windows.Forms.Padding(2)
        Me.Agregar.Name = "Agregar"
        Me.Agregar.Size = New System.Drawing.Size(80, 63)
        Me.Agregar.TabIndex = 0
        Me.Agregar.Tag = "Agregar"
        Me.Agregar.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Button4.BackgroundImage = Global.SICDIN30.My.Resources.Resources.zenmap_104119
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(0, 0)
        Me.Button4.Margin = New System.Windows.Forms.Padding(2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(80, 63)
        Me.Button4.TabIndex = 4
        Me.Button4.Tag = "Ver"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Button5.BackgroundImage = Global.SICDIN30.My.Resources.Resources.Logout_37127
        Me.Button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Location = New System.Drawing.Point(651, 0)
        Me.Button5.Margin = New System.Windows.Forms.Padding(2)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(80, 63)
        Me.Button5.TabIndex = 5
        Me.Button5.Tag = "Cerrar"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Abrirmov
        '
        Me.Abrirmov.HeaderText = "Abrir"
        Me.Abrirmov.Image = Global.SICDIN30.My.Resources.Resources.folderorangeopen_93000
        Me.Abrirmov.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.Abrirmov.Name = "Abrirmov"
        Me.Abrirmov.ReadOnly = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Movimientos:"
        '
        'clientes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(731, 366)
        Me.Controls.Add(Me.panelclientes)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "clientes"
        Me.Text = "clientes"
        Me.panelclientes.ResumeLayout(False)
        CType(Me.clntmov, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelclientes As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Agregar As Button
    Friend WithEvents modadeudo As Button
    Friend WithEvents delclnt As Button
    Friend WithEvents modclnt As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents sbox As TextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents pagaradeudo As Button
    Friend WithEvents PagarAdeudoToolStripMenuItem As ToolStripMenuItem
    Public WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents dg As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents nmov As TextBox
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents dp2 As DateTimePicker
    Friend WithEvents dp1 As DateTimePicker
    Friend WithEvents clntmov As DataGridView
    Friend WithEvents filtros As Button
    Friend WithEvents Abrirmov As DataGridViewImageColumn
    Friend WithEvents Label3 As Label
End Class
