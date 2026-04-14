<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Usuariosform
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
        Me.userpanel = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Trabajadores = New System.Windows.Forms.TabPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.totallabel = New System.Windows.Forms.Label()
        Me.dgtrabajo = New System.Windows.Forms.DataGridView()
        Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Hora = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Folio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Clave = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Des = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Importe = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fechar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.fechaa = New System.Windows.Forms.DateTimePicker()
        Me.fechade = New System.Windows.Forms.DateTimePicker()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pagar = New System.Windows.Forms.Button()
        Me.destajo = New System.Windows.Forms.CheckBox()
        Me.activo = New System.Windows.Forms.CheckBox()
        Me.nuevomec = New System.Windows.Forms.Button()
        Me.combotra = New System.Windows.Forms.ComboBox()
        Me.favomec = New System.Windows.Forms.Button()
        Me.altabaja = New System.Windows.Forms.Button()
        Me.Usu = New System.Windows.Forms.TabPage()
        Me.dgusers = New System.Windows.Forms.DataGridView()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Block = New System.Windows.Forms.Button()
        Me.modpass = New System.Windows.Forms.Button()
        Me.adduser = New System.Windows.Forms.Button()
        Me.DirectorySearcher1 = New System.DirectoryServices.DirectorySearcher()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.userpanel.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.Trabajadores.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.dgtrabajo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Usu.SuspendLayout()
        CType(Me.dgusers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'userpanel
        '
        Me.userpanel.Controls.Add(Me.TabControl1)
        Me.userpanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.userpanel.Location = New System.Drawing.Point(0, 0)
        Me.userpanel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.userpanel.Name = "userpanel"
        Me.userpanel.Size = New System.Drawing.Size(1001, 450)
        Me.userpanel.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Trabajadores)
        Me.TabControl1.Controls.Add(Me.Usu)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1001, 450)
        Me.TabControl1.TabIndex = 0
        '
        'Trabajadores
        '
        Me.Trabajadores.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Trabajadores.Controls.Add(Me.Panel4)
        Me.Trabajadores.Controls.Add(Me.dgtrabajo)
        Me.Trabajadores.Controls.Add(Me.Panel2)
        Me.Trabajadores.Controls.Add(Me.Panel3)
        Me.Trabajadores.Controls.Add(Me.Panel1)
        Me.Trabajadores.Location = New System.Drawing.Point(4, 36)
        Me.Trabajadores.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Trabajadores.Name = "Trabajadores"
        Me.Trabajadores.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Trabajadores.Size = New System.Drawing.Size(993, 410)
        Me.Trabajadores.TabIndex = 1
        Me.Trabajadores.Text = "Trabajadores"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.totallabel)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(3, 367)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(987, 41)
        Me.Panel4.TabIndex = 4
        '
        'totallabel
        '
        Me.totallabel.AutoSize = True
        Me.totallabel.Dock = System.Windows.Forms.DockStyle.Right
        Me.totallabel.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.totallabel.Location = New System.Drawing.Point(919, 0)
        Me.totallabel.Name = "totallabel"
        Me.totallabel.Size = New System.Drawing.Size(68, 27)
        Me.totallabel.TabIndex = 0
        Me.totallabel.Text = "Total:$"
        '
        'dgtrabajo
        '
        Me.dgtrabajo.AllowUserToAddRows = False
        Me.dgtrabajo.AllowUserToDeleteRows = False
        Me.dgtrabajo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgtrabajo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Fecha, Me.Hora, Me.Folio, Me.Cantidad, Me.Clave, Me.Des, Me.Importe, Me.Tot, Me.Fechar})
        Me.dgtrabajo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgtrabajo.Location = New System.Drawing.Point(3, 170)
        Me.dgtrabajo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgtrabajo.Name = "dgtrabajo"
        Me.dgtrabajo.ReadOnly = True
        Me.dgtrabajo.RowHeadersWidth = 51
        Me.dgtrabajo.RowTemplate.Height = 24
        Me.dgtrabajo.Size = New System.Drawing.Size(987, 238)
        Me.dgtrabajo.TabIndex = 3
        '
        'Fecha
        '
        Me.Fecha.HeaderText = "Fecha"
        Me.Fecha.MinimumWidth = 6
        Me.Fecha.Name = "Fecha"
        Me.Fecha.ReadOnly = True
        Me.Fecha.Width = 125
        '
        'Hora
        '
        Me.Hora.HeaderText = "Hora"
        Me.Hora.MinimumWidth = 6
        Me.Hora.Name = "Hora"
        Me.Hora.ReadOnly = True
        Me.Hora.Width = 125
        '
        'Folio
        '
        Me.Folio.HeaderText = "Folio"
        Me.Folio.MinimumWidth = 6
        Me.Folio.Name = "Folio"
        Me.Folio.ReadOnly = True
        Me.Folio.Width = 125
        '
        'Cantidad
        '
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.MinimumWidth = 6
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        Me.Cantidad.Width = 125
        '
        'Clave
        '
        Me.Clave.HeaderText = "Clave"
        Me.Clave.MinimumWidth = 6
        Me.Clave.Name = "Clave"
        Me.Clave.ReadOnly = True
        Me.Clave.Width = 125
        '
        'Des
        '
        Me.Des.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Des.HeaderText = "Descripción"
        Me.Des.MinimumWidth = 6
        Me.Des.Name = "Des"
        Me.Des.ReadOnly = True
        Me.Des.Width = 136
        '
        'Importe
        '
        Me.Importe.HeaderText = "Importe"
        Me.Importe.MinimumWidth = 6
        Me.Importe.Name = "Importe"
        Me.Importe.ReadOnly = True
        Me.Importe.Width = 125
        '
        'Tot
        '
        Me.Tot.HeaderText = "Total"
        Me.Tot.MinimumWidth = 6
        Me.Tot.Name = "Tot"
        Me.Tot.ReadOnly = True
        Me.Tot.Width = 125
        '
        'Fechar
        '
        Me.Fechar.HeaderText = "Fechar"
        Me.Fechar.MinimumWidth = 6
        Me.Fechar.Name = "Fechar"
        Me.Fechar.ReadOnly = True
        Me.Fechar.Visible = False
        Me.Fechar.Width = 125
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.fechaa)
        Me.Panel2.Controls.Add(Me.fechade)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(3, 113)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(987, 57)
        Me.Panel2.TabIndex = 1
        '
        'fechaa
        '
        Me.fechaa.Dock = System.Windows.Forms.DockStyle.Right
        Me.fechaa.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fechaa.Location = New System.Drawing.Point(583, 0)
        Me.fechaa.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.fechaa.Name = "fechaa"
        Me.fechaa.Size = New System.Drawing.Size(404, 34)
        Me.fechaa.TabIndex = 1
        '
        'fechade
        '
        Me.fechade.Dock = System.Windows.Forms.DockStyle.Left
        Me.fechade.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fechade.Location = New System.Drawing.Point(0, 0)
        Me.fechade.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.fechade.Name = "fechade"
        Me.fechade.Size = New System.Drawing.Size(367, 34)
        Me.fechade.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 72)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(987, 41)
        Me.Panel3.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(957, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 27)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "A:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 27)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "De:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pagar)
        Me.Panel1.Controls.Add(Me.destajo)
        Me.Panel1.Controls.Add(Me.activo)
        Me.Panel1.Controls.Add(Me.nuevomec)
        Me.Panel1.Controls.Add(Me.combotra)
        Me.Panel1.Controls.Add(Me.favomec)
        Me.Panel1.Controls.Add(Me.altabaja)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 2)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(987, 70)
        Me.Panel1.TabIndex = 0
        '
        'pagar
        '
        Me.pagar.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.pagar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.pagar1
        Me.pagar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pagar.Dock = System.Windows.Forms.DockStyle.Right
        Me.pagar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.pagar.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pagar.Location = New System.Drawing.Point(613, 0)
        Me.pagar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.pagar.Name = "pagar"
        Me.pagar.Size = New System.Drawing.Size(91, 70)
        Me.pagar.TabIndex = 8
        Me.pagar.Tag = "Pagar"
        Me.pagar.UseVisualStyleBackColor = False
        '
        'destajo
        '
        Me.destajo.AutoSize = True
        Me.destajo.Checked = True
        Me.destajo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.destajo.Dock = System.Windows.Forms.DockStyle.Left
        Me.destajo.Location = New System.Drawing.Point(403, 0)
        Me.destajo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.destajo.Name = "destajo"
        Me.destajo.Size = New System.Drawing.Size(96, 70)
        Me.destajo.TabIndex = 5
        Me.destajo.Text = "Destajo"
        Me.destajo.UseVisualStyleBackColor = True
        '
        'activo
        '
        Me.activo.AutoSize = True
        Me.activo.Checked = True
        Me.activo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.activo.Dock = System.Windows.Forms.DockStyle.Left
        Me.activo.Location = New System.Drawing.Point(319, 0)
        Me.activo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.activo.Name = "activo"
        Me.activo.Size = New System.Drawing.Size(84, 70)
        Me.activo.TabIndex = 4
        Me.activo.Text = "Activo"
        Me.activo.UseVisualStyleBackColor = True
        '
        'nuevomec
        '
        Me.nuevomec.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.nuevomec.BackgroundImage = Global.SICDIN30.My.Resources.Resources.mecanico
        Me.nuevomec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.nuevomec.Dock = System.Windows.Forms.DockStyle.Right
        Me.nuevomec.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.nuevomec.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nuevomec.Location = New System.Drawing.Point(704, 0)
        Me.nuevomec.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.nuevomec.Name = "nuevomec"
        Me.nuevomec.Size = New System.Drawing.Size(101, 70)
        Me.nuevomec.TabIndex = 2
        Me.nuevomec.Tag = "Nuevo mecánico"
        Me.nuevomec.UseVisualStyleBackColor = False
        '
        'combotra
        '
        Me.combotra.Dock = System.Windows.Forms.DockStyle.Left
        Me.combotra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.combotra.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.combotra.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combotra.FormattingEnabled = True
        Me.combotra.Location = New System.Drawing.Point(0, 0)
        Me.combotra.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.combotra.Name = "combotra"
        Me.combotra.Size = New System.Drawing.Size(319, 35)
        Me.combotra.TabIndex = 3
        '
        'favomec
        '
        Me.favomec.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.favomec.BackgroundImage = Global.SICDIN30.My.Resources.Resources.favorito
        Me.favomec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.favomec.Dock = System.Windows.Forms.DockStyle.Right
        Me.favomec.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.favomec.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.favomec.Location = New System.Drawing.Point(805, 0)
        Me.favomec.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.favomec.Name = "favomec"
        Me.favomec.Size = New System.Drawing.Size(91, 70)
        Me.favomec.TabIndex = 6
        Me.favomec.Tag = "Favorito"
        Me.favomec.UseVisualStyleBackColor = False
        '
        'altabaja
        '
        Me.altabaja.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.altabaja.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.altabaja.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.altabaja.Dock = System.Windows.Forms.DockStyle.Right
        Me.altabaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.altabaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.altabaja.Location = New System.Drawing.Point(896, 0)
        Me.altabaja.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.altabaja.Name = "altabaja"
        Me.altabaja.Size = New System.Drawing.Size(91, 70)
        Me.altabaja.TabIndex = 7
        Me.altabaja.Tag = "Dar de baja"
        Me.altabaja.UseVisualStyleBackColor = False
        '
        'Usu
        '
        Me.Usu.Controls.Add(Me.dgusers)
        Me.Usu.Controls.Add(Me.Panel5)
        Me.Usu.Location = New System.Drawing.Point(4, 36)
        Me.Usu.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Usu.Name = "Usu"
        Me.Usu.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Usu.Size = New System.Drawing.Size(993, 410)
        Me.Usu.TabIndex = 0
        Me.Usu.Text = "Usuarios"
        Me.Usu.UseVisualStyleBackColor = True
        '
        'dgusers
        '
        Me.dgusers.AllowUserToAddRows = False
        Me.dgusers.AllowUserToDeleteRows = False
        Me.dgusers.BackgroundColor = System.Drawing.SystemColors.InactiveCaption
        Me.dgusers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgusers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgusers.Location = New System.Drawing.Point(3, 102)
        Me.dgusers.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgusers.Name = "dgusers"
        Me.dgusers.ReadOnly = True
        Me.dgusers.RowHeadersWidth = 51
        Me.dgusers.RowTemplate.Height = 24
        Me.dgusers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgusers.Size = New System.Drawing.Size(987, 306)
        Me.dgusers.TabIndex = 1
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Panel5.Controls.Add(Me.Block)
        Me.Panel5.Controls.Add(Me.modpass)
        Me.Panel5.Controls.Add(Me.adduser)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(3, 2)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(987, 100)
        Me.Panel5.TabIndex = 0
        '
        'Block
        '
        Me.Block.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Block.BackgroundImage = Global.SICDIN30.My.Resources.Resources.delete_delete_deleteusers_delete_male_user_maleclient_23481
        Me.Block.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Block.Dock = System.Windows.Forms.DockStyle.Left
        Me.Block.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Block.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Block.Location = New System.Drawing.Point(182, 0)
        Me.Block.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Block.Name = "Block"
        Me.Block.Size = New System.Drawing.Size(91, 100)
        Me.Block.TabIndex = 10
        Me.Block.Tag = "Pagar"
        Me.Block.UseVisualStyleBackColor = False
        '
        'modpass
        '
        Me.modpass.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.modpass.BackgroundImage = Global.SICDIN30.My.Resources.Resources.businessapplication_edit_male_user_thepencil_theclient_negocio_2321
        Me.modpass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.modpass.Dock = System.Windows.Forms.DockStyle.Left
        Me.modpass.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.modpass.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modpass.Location = New System.Drawing.Point(91, 0)
        Me.modpass.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.modpass.Name = "modpass"
        Me.modpass.Size = New System.Drawing.Size(91, 100)
        Me.modpass.TabIndex = 11
        Me.modpass.Tag = "Pagar"
        Me.modpass.UseVisualStyleBackColor = False
        '
        'adduser
        '
        Me.adduser.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.adduser.BackgroundImage = Global.SICDIN30.My.Resources.Resources.business_application_addmale_useradd_insert_add_user_client_2312
        Me.adduser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.adduser.Dock = System.Windows.Forms.DockStyle.Left
        Me.adduser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.adduser.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adduser.Location = New System.Drawing.Point(0, 0)
        Me.adduser.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.adduser.Name = "adduser"
        Me.adduser.Size = New System.Drawing.Size(91, 100)
        Me.adduser.TabIndex = 9
        Me.adduser.Tag = "Pagar"
        Me.adduser.UseVisualStyleBackColor = False
        '
        'DirectorySearcher1
        '
        Me.DirectorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01")
        Me.DirectorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01")
        Me.DirectorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01")
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Usuariosform
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1001, 450)
        Me.Controls.Add(Me.userpanel)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Usuariosform"
        Me.Text = "Form1"
        Me.userpanel.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.Trabajadores.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.dgtrabajo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Usu.ResumeLayout(False)
        CType(Me.dgusers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents userpanel As Panel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents Trabajadores As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Usu As TabPage
    Friend WithEvents nuevomec As Button
    Friend WithEvents combotra As ComboBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents fechaa As DateTimePicker
    Friend WithEvents fechade As DateTimePicker
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents totallabel As Label
    Friend WithEvents dgtrabajo As DataGridView
    Friend WithEvents destajo As CheckBox
    Friend WithEvents activo As CheckBox
    Friend WithEvents favomec As Button
    Friend WithEvents altabaja As Button
    Friend WithEvents pagar As Button
    Friend WithEvents dgusers As DataGridView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents adduser As Button
    Friend WithEvents Block As Button
    Friend WithEvents modpass As Button
    Friend WithEvents DirectorySearcher1 As DirectoryServices.DirectorySearcher
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Fecha As DataGridViewTextBoxColumn
    Friend WithEvents Hora As DataGridViewTextBoxColumn
    Friend WithEvents Folio As DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As DataGridViewTextBoxColumn
    Friend WithEvents Clave As DataGridViewTextBoxColumn
    Friend WithEvents Des As DataGridViewTextBoxColumn
    Friend WithEvents Importe As DataGridViewTextBoxColumn
    Friend WithEvents Tot As DataGridViewTextBoxColumn
    Friend WithEvents Fechar As DataGridViewTextBoxColumn
End Class
