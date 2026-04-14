<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class clienteform
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.may = New System.Windows.Forms.CheckBox()
        Me.esp = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.title = New System.Windows.Forms.Label()
        Me.Nombre = New System.Windows.Forms.TextBox()
        Me.direccion = New System.Windows.Forms.TextBox()
        Me.tcasa = New System.Windows.Forms.TextBox()
        Me.tcel = New System.Windows.Forms.TextBox()
        Me.mail = New System.Windows.Forms.TextBox()
        Me.credito = New System.Windows.Forms.TextBox()
        Me.dc = New System.Windows.Forms.TextBox()
        Me.idlabel = New System.Windows.Forms.Label()
        Me.rfc = New System.Windows.Forms.MaskedTextBox()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.arrob = New System.Windows.Forms.Button()
        Me.aceptarblock = New System.Windows.Forms.Button()
        Me.cancelar = New System.Windows.Forms.Button()
        Me.aceptar = New System.Windows.Forms.Button()
        Me.Buscar = New System.Windows.Forms.Button()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(504, 16)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 577)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(504, 16)
        Me.Panel2.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(489, 16)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(15, 561)
        Me.Panel3.TabIndex = 2
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 16)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(15, 561)
        Me.Panel4.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(83, 90)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 26)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Nombre:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(69, 124)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 26)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Dirección:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(116, 205)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 26)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "RFC:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(68, 245)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(110, 26)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Tel. Casa:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(49, 285)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(128, 26)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Tel. Celular:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(100, 329)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 26)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "e-mail:"
        '
        'may
        '
        Me.may.AutoSize = True
        Me.may.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.may.Location = New System.Drawing.Point(104, 375)
        Me.may.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.may.Name = "may"
        Me.may.Size = New System.Drawing.Size(115, 30)
        Me.may.TabIndex = 6
        Me.may.Text = "Mayoreo"
        Me.may.UseVisualStyleBackColor = True
        '
        'esp
        '
        Me.esp.AutoSize = True
        Me.esp.Enabled = False
        Me.esp.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.esp.Location = New System.Drawing.Point(225, 375)
        Me.esp.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.esp.Name = "esp"
        Me.esp.Size = New System.Drawing.Size(114, 30)
        Me.esp.TabIndex = 7
        Me.esp.Text = "Especial"
        Me.esp.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(89, 421)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 26)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Crédito:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(16, 457)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(163, 26)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Días de crédito:"
        '
        'title
        '
        Me.title.AutoSize = True
        Me.title.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.title.Location = New System.Drawing.Point(137, 19)
        Me.title.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.title.Name = "title"
        Me.title.Size = New System.Drawing.Size(253, 26)
        Me.title.TabIndex = 14
        Me.title.Text = "Agregar un cliente nuevo"
        '
        'Nombre
        '
        Me.Nombre.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Nombre.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.Nombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Nombre.Location = New System.Drawing.Point(170, 91)
        Me.Nombre.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Nombre.Name = "Nombre"
        Me.Nombre.Size = New System.Drawing.Size(192, 32)
        Me.Nombre.TabIndex = 0
        '
        'direccion
        '
        Me.direccion.AcceptsReturn = True
        Me.direccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.direccion.Location = New System.Drawing.Point(170, 127)
        Me.direccion.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.direccion.Multiline = True
        Me.direccion.Name = "direccion"
        Me.direccion.Size = New System.Drawing.Size(192, 76)
        Me.direccion.TabIndex = 1
        '
        'tcasa
        '
        Me.tcasa.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcasa.Location = New System.Drawing.Point(170, 245)
        Me.tcasa.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tcasa.Name = "tcasa"
        Me.tcasa.Size = New System.Drawing.Size(192, 32)
        Me.tcasa.TabIndex = 3
        '
        'tcel
        '
        Me.tcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcel.Location = New System.Drawing.Point(170, 285)
        Me.tcel.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tcel.Name = "tcel"
        Me.tcel.Size = New System.Drawing.Size(192, 32)
        Me.tcel.TabIndex = 4
        '
        'mail
        '
        Me.mail.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mail.Location = New System.Drawing.Point(170, 329)
        Me.mail.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.mail.Name = "mail"
        Me.mail.Size = New System.Drawing.Size(192, 32)
        Me.mail.TabIndex = 5
        '
        'credito
        '
        Me.credito.Enabled = False
        Me.credito.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.credito.Location = New System.Drawing.Point(170, 416)
        Me.credito.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.credito.Name = "credito"
        Me.credito.Size = New System.Drawing.Size(192, 32)
        Me.credito.TabIndex = 8
        '
        'dc
        '
        Me.dc.Enabled = False
        Me.dc.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dc.Location = New System.Drawing.Point(170, 457)
        Me.dc.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.dc.Name = "dc"
        Me.dc.Size = New System.Drawing.Size(192, 32)
        Me.dc.TabIndex = 9
        '
        'idlabel
        '
        Me.idlabel.AutoSize = True
        Me.idlabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.idlabel.Location = New System.Drawing.Point(137, 50)
        Me.idlabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.idlabel.Name = "idlabel"
        Me.idlabel.Size = New System.Drawing.Size(48, 26)
        Me.idlabel.TabIndex = 25
        Me.idlabel.Text = "Id:0"
        '
        'rfc
        '
        Me.rfc.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rfc.Location = New System.Drawing.Point(170, 206)
        Me.rfc.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.rfc.Name = "rfc"
        Me.rfc.Size = New System.Drawing.Size(162, 32)
        Me.rfc.TabIndex = 2
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'arrob
        '
        Me.arrob.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.arrob.Location = New System.Drawing.Point(399, 329)
        Me.arrob.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.arrob.Name = "arrob"
        Me.arrob.Size = New System.Drawing.Size(68, 31)
        Me.arrob.TabIndex = 26
        Me.arrob.Text = "@"
        Me.arrob.UseVisualStyleBackColor = True
        Me.arrob.Visible = False
        '
        'aceptarblock
        '
        Me.aceptarblock.AutoSize = True
        Me.aceptarblock.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.aceptarblock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.aceptarblock.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.aceptarblock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.aceptarblock.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aceptarblock.Location = New System.Drawing.Point(202, 500)
        Me.aceptarblock.Margin = New System.Windows.Forms.Padding(2)
        Me.aceptarblock.Name = "aceptarblock"
        Me.aceptarblock.Size = New System.Drawing.Size(94, 44)
        Me.aceptarblock.TabIndex = 27
        Me.aceptarblock.Tag = "Aceptar"
        Me.aceptarblock.UseVisualStyleBackColor = True
        Me.aceptarblock.Visible = False
        '
        'cancelar
        '
        Me.cancelar.AutoSize = True
        Me.cancelar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancelar.Location = New System.Drawing.Point(315, 500)
        Me.cancelar.Margin = New System.Windows.Forms.Padding(2)
        Me.cancelar.Name = "cancelar"
        Me.cancelar.Size = New System.Drawing.Size(106, 44)
        Me.cancelar.TabIndex = 11
        Me.cancelar.Tag = "Cancelar"
        Me.cancelar.UseVisualStyleBackColor = True
        '
        'aceptar
        '
        Me.aceptar.AutoSize = True
        Me.aceptar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.aceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.aceptar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.aceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aceptar.Location = New System.Drawing.Point(82, 500)
        Me.aceptar.Margin = New System.Windows.Forms.Padding(2)
        Me.aceptar.Name = "aceptar"
        Me.aceptar.Size = New System.Drawing.Size(94, 44)
        Me.aceptar.TabIndex = 10
        Me.aceptar.Tag = "Aceptar"
        Me.aceptar.UseVisualStyleBackColor = True
        '
        'Buscar
        '
        Me.Buscar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.loupe_78956
        Me.Buscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Buscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Buscar.Location = New System.Drawing.Point(367, 90)
        Me.Buscar.Name = "Buscar"
        Me.Buscar.Size = New System.Drawing.Size(54, 34)
        Me.Buscar.TabIndex = 28
        Me.Buscar.Tag = "Buscar"
        Me.Buscar.UseVisualStyleBackColor = True
        Me.Buscar.Visible = False
        '
        'clienteform
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(504, 593)
        Me.Controls.Add(Me.Buscar)
        Me.Controls.Add(Me.aceptarblock)
        Me.Controls.Add(Me.arrob)
        Me.Controls.Add(Me.rfc)
        Me.Controls.Add(Me.idlabel)
        Me.Controls.Add(Me.cancelar)
        Me.Controls.Add(Me.aceptar)
        Me.Controls.Add(Me.dc)
        Me.Controls.Add(Me.credito)
        Me.Controls.Add(Me.mail)
        Me.Controls.Add(Me.tcel)
        Me.Controls.Add(Me.tcasa)
        Me.Controls.Add(Me.direccion)
        Me.Controls.Add(Me.Nombre)
        Me.Controls.Add(Me.title)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.esp)
        Me.Controls.Add(Me.may)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "clienteform"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents may As CheckBox
    Friend WithEvents esp As CheckBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents title As Label
    Friend WithEvents Nombre As TextBox
    Friend WithEvents direccion As TextBox
    Friend WithEvents tcasa As TextBox
    Friend WithEvents tcel As TextBox
    Friend WithEvents mail As TextBox
    Friend WithEvents credito As TextBox
    Friend WithEvents dc As TextBox
    Friend WithEvents aceptar As Button
    Friend WithEvents cancelar As Button
    Friend WithEvents idlabel As Label
    Friend WithEvents rfc As MaskedTextBox
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents arrob As Button
    Friend WithEvents aceptarblock As Button
    Friend WithEvents Buscar As Button
End Class
