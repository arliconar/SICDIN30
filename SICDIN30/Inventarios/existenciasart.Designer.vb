<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class existenciasart
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
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Descripciontext = New System.Windows.Forms.Label()
        Me.clavetext = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.okv = New System.Windows.Forms.Button()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.dg = New System.Windows.Forms.DataGridView()
        Me.tien = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cant = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Transfer = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cantidad = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.a = New System.Windows.Forms.ComboBox()
        Me.de = New System.Windows.Forms.ComboBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(15, 336)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(696, 16)
        Me.Panel4.TabIndex = 14
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(15, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(696, 16)
        Me.Panel2.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Existencias"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(15, 352)
        Me.Panel1.TabIndex = 12
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(711, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(15, 352)
        Me.Panel3.TabIndex = 11
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(15, 16)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(696, 79)
        Me.Panel5.TabIndex = 15
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Descripciontext)
        Me.Panel6.Controls.Add(Me.clavetext)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(696, 79)
        Me.Panel6.TabIndex = 16
        '
        'Descripciontext
        '
        Me.Descripciontext.AutoSize = True
        Me.Descripciontext.Location = New System.Drawing.Point(15, 37)
        Me.Descripciontext.Name = "Descripciontext"
        Me.Descripciontext.Size = New System.Drawing.Size(115, 24)
        Me.Descripciontext.TabIndex = 1
        Me.Descripciontext.Text = "Descripción:"
        '
        'clavetext
        '
        Me.clavetext.AutoSize = True
        Me.clavetext.Location = New System.Drawing.Point(68, 13)
        Me.clavetext.Name = "clavetext"
        Me.clavetext.Size = New System.Drawing.Size(62, 24)
        Me.clavetext.TabIndex = 0
        Me.clavetext.Text = "Clave:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 24)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Descripción:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(68, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 24)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Clave:"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.okv)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel7.Location = New System.Drawing.Point(15, 277)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(696, 59)
        Me.Panel7.TabIndex = 17
        '
        'okv
        '
        Me.okv.AutoSize = True
        Me.okv.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.okv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.okv.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.okv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.okv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.okv.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.okv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.okv.Location = New System.Drawing.Point(0, 0)
        Me.okv.Margin = New System.Windows.Forms.Padding(2)
        Me.okv.Name = "okv"
        Me.okv.Size = New System.Drawing.Size(696, 59)
        Me.okv.TabIndex = 10
        Me.okv.Tag = "Aceptar"
        Me.okv.UseVisualStyleBackColor = True
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.dg)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(15, 95)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(696, 109)
        Me.Panel8.TabIndex = 18
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.AllowUserToDeleteRows = False
        Me.dg.BackgroundColor = System.Drawing.SystemColors.InactiveCaption
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.tien, Me.Cant})
        Me.dg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dg.Location = New System.Drawing.Point(0, 0)
        Me.dg.Name = "dg"
        Me.dg.ReadOnly = True
        Me.dg.Size = New System.Drawing.Size(696, 109)
        Me.dg.TabIndex = 17
        '
        'tien
        '
        Me.tien.HeaderText = "Tienda"
        Me.tien.Name = "tien"
        Me.tien.ReadOnly = True
        '
        'Cant
        '
        Me.Cant.HeaderText = "Cantidad"
        Me.Cant.Name = "Cant"
        Me.Cant.ReadOnly = True
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.PictureBox1)
        Me.Panel9.Controls.Add(Me.Transfer)
        Me.Panel9.Controls.Add(Me.Label7)
        Me.Panel9.Controls.Add(Me.cantidad)
        Me.Panel9.Controls.Add(Me.Label6)
        Me.Panel9.Controls.Add(Me.Label5)
        Me.Panel9.Controls.Add(Me.a)
        Me.Panel9.Controls.Add(Me.de)
        Me.Panel9.Controls.Add(Me.CheckBox1)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel9.Location = New System.Drawing.Point(15, 204)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(696, 73)
        Me.Panel9.TabIndex = 19
        Me.Panel9.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SICDIN30.My.Resources.Resources.flechader
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.ErrorImage = Global.SICDIN30.My.Resources.Resources.flechader
        Me.PictureBox1.Location = New System.Drawing.Point(261, 39)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(33, 28)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'Transfer
        '
        Me.Transfer.BackgroundImage = Global.SICDIN30.My.Resources.Resources.transferir
        Me.Transfer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Transfer.Enabled = False
        Me.Transfer.Image = Global.SICDIN30.My.Resources.Resources.transferir
        Me.Transfer.Location = New System.Drawing.Point(573, 31)
        Me.Transfer.Name = "Transfer"
        Me.Transfer.Size = New System.Drawing.Size(95, 36)
        Me.Transfer.TabIndex = 6
        Me.Transfer.Tag = "Transferir"
        Me.Transfer.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(432, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 24)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Cantidad:"
        '
        'cantidad
        '
        Me.cantidad.Enabled = False
        Me.cantidad.Location = New System.Drawing.Point(436, 35)
        Me.cantidad.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.cantidad.Name = "cantidad"
        Me.cantidad.Size = New System.Drawing.Size(120, 29)
        Me.cantidad.TabIndex = 4
        Me.cantidad.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(296, 5)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 24)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "A:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(134, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 24)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "De:"
        '
        'a
        '
        Me.a.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.a.Enabled = False
        Me.a.FormattingEnabled = True
        Me.a.Location = New System.Drawing.Point(300, 37)
        Me.a.Name = "a"
        Me.a.Size = New System.Drawing.Size(121, 32)
        Me.a.TabIndex = 2
        '
        'de
        '
        Me.de.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.de.Enabled = False
        Me.de.FormattingEnabled = True
        Me.de.Location = New System.Drawing.Point(134, 37)
        Me.de.Name = "de"
        Me.de.Size = New System.Drawing.Size(121, 32)
        Me.de.TabIndex = 1
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(20, 28)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(108, 28)
        Me.CheckBox1.TabIndex = 0
        Me.CheckBox1.Text = "Transferir"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'existenciasart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(726, 352)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "existenciasart"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "existenciasart"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Descripciontext As Label
    Friend WithEvents clavetext As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents okv As Button
    Friend WithEvents Panel8 As Panel
    Friend WithEvents dg As DataGridView
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents a As ComboBox
    Friend WithEvents de As ComboBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents cantidad As NumericUpDown
    Friend WithEvents Label7 As Label
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents Transfer As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents tien As DataGridViewTextBoxColumn
    Friend WithEvents Cant As DataGridViewTextBoxColumn
End Class
