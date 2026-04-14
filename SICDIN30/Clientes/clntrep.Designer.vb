<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class clntre
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
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.dg = New System.Windows.Forms.DataGridView()
        Me.nclnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Telcel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Telcasa = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RFC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dir = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Base = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.caja = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.cancelar = New System.Windows.Forms.Button()
        Me.aceptar = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Venta = New System.Windows.Forms.CheckBox()
        Me.Reparacion = New System.Windows.Forms.CheckBox()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(10, 484)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(883, 16)
        Me.Panel4.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(883, 30)
        Me.Panel2.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(183, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Busqueda de cliente"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(10, 500)
        Me.Panel1.TabIndex = 11
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(893, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(13, 500)
        Me.Panel3.TabIndex = 10
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.dg)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(10, 104)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(883, 280)
        Me.Panel5.TabIndex = 14
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.AllowUserToDeleteRows = False
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.nclnt, Me.Telcel, Me.Telcasa, Me.RFC, Me.Dir, Me.Base})
        Me.dg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dg.Location = New System.Drawing.Point(0, 0)
        Me.dg.Margin = New System.Windows.Forms.Padding(5)
        Me.dg.Name = "dg"
        Me.dg.ReadOnly = True
        Me.dg.RowHeadersWidth = 51
        Me.dg.RowTemplate.Height = 24
        Me.dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dg.Size = New System.Drawing.Size(883, 280)
        Me.dg.TabIndex = 0
        '
        'nclnt
        '
        Me.nclnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.nclnt.HeaderText = "Nombre"
        Me.nclnt.Name = "nclnt"
        Me.nclnt.ReadOnly = True
        Me.nclnt.Width = 104
        '
        'Telcel
        '
        Me.Telcel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Telcel.HeaderText = "Tel.Cel"
        Me.Telcel.Name = "Telcel"
        Me.Telcel.ReadOnly = True
        Me.Telcel.Width = 95
        '
        'Telcasa
        '
        Me.Telcasa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Telcasa.HeaderText = "Tel.Casa"
        Me.Telcasa.Name = "Telcasa"
        Me.Telcasa.ReadOnly = True
        Me.Telcasa.Width = 109
        '
        'RFC
        '
        Me.RFC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.RFC.HeaderText = "RFC"
        Me.RFC.Name = "RFC"
        Me.RFC.ReadOnly = True
        Me.RFC.Width = 73
        '
        'Dir
        '
        Me.Dir.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Dir.HeaderText = "Dirección"
        Me.Dir.Name = "Dir"
        Me.Dir.ReadOnly = True
        '
        'Base
        '
        Me.Base.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Base.HeaderText = "Base"
        Me.Base.Name = "Base"
        Me.Base.ReadOnly = True
        Me.Base.Width = 77
        '
        'caja
        '
        Me.caja.Location = New System.Drawing.Point(257, 53)
        Me.caja.Name = "caja"
        Me.caja.Size = New System.Drawing.Size(314, 28)
        Me.caja.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 24)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Buscar por:"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.cancelar)
        Me.Panel6.Controls.Add(Me.aceptar)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(10, 384)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(883, 100)
        Me.Panel6.TabIndex = 20
        '
        'cancelar
        '
        Me.cancelar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cancelar.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancelar.Location = New System.Drawing.Point(447, 0)
        Me.cancelar.Name = "cancelar"
        Me.cancelar.Size = New System.Drawing.Size(436, 100)
        Me.cancelar.TabIndex = 23
        Me.cancelar.Tag = "Cancelar"
        Me.cancelar.UseVisualStyleBackColor = True
        '
        'aceptar
        '
        Me.aceptar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.aceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.aceptar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.aceptar.Dock = System.Windows.Forms.DockStyle.Left
        Me.aceptar.Enabled = False
        Me.aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.aceptar.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aceptar.Location = New System.Drawing.Point(0, 0)
        Me.aceptar.Name = "aceptar"
        Me.aceptar.Size = New System.Drawing.Size(447, 100)
        Me.aceptar.TabIndex = 22
        Me.aceptar.Tag = "Aceptar"
        Me.aceptar.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.SICDIN30.My.Resources.Resources.loupe_78956
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(805, 35)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 58)
        Me.Button1.TabIndex = 19
        Me.Button1.Tag = "Buscar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Nombre", "Telefono", "RFC"})
        Me.ComboBox1.Location = New System.Drawing.Point(126, 53)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(125, 30)
        Me.ComboBox1.TabIndex = 21
        '
        'Venta
        '
        Me.Venta.AutoSize = True
        Me.Venta.Checked = True
        Me.Venta.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Venta.Location = New System.Drawing.Point(577, 55)
        Me.Venta.Name = "Venta"
        Me.Venta.Size = New System.Drawing.Size(78, 28)
        Me.Venta.TabIndex = 22
        Me.Venta.Text = "Venta"
        Me.Venta.UseVisualStyleBackColor = True
        '
        'Reparacion
        '
        Me.Reparacion.AutoSize = True
        Me.Reparacion.Checked = True
        Me.Reparacion.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Reparacion.Location = New System.Drawing.Point(661, 55)
        Me.Reparacion.Name = "Reparacion"
        Me.Reparacion.Size = New System.Drawing.Size(126, 28)
        Me.Reparacion.TabIndex = 23
        Me.Reparacion.Text = "Reparación"
        Me.Reparacion.UseVisualStyleBackColor = True
        '
        'clntre
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(906, 500)
        Me.Controls.Add(Me.Reparacion)
        Me.Controls.Add(Me.Venta)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.caja)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label2)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "clntre"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "clntrep"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents dg As DataGridView
    Friend WithEvents caja As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel6 As Panel
    Friend WithEvents cancelar As Button
    Friend WithEvents aceptar As Button
    Friend WithEvents nclnt As DataGridViewTextBoxColumn
    Friend WithEvents Telcel As DataGridViewTextBoxColumn
    Friend WithEvents Telcasa As DataGridViewTextBoxColumn
    Friend WithEvents RFC As DataGridViewTextBoxColumn
    Friend WithEvents Dir As DataGridViewTextBoxColumn
    Friend WithEvents Base As DataGridViewTextBoxColumn
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Venta As CheckBox
    Friend WithEvents Reparacion As CheckBox
End Class
