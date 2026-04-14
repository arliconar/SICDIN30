<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class userform
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
        Me.cancelar = New System.Windows.Forms.Button()
        Me.aceptar = New System.Windows.Forms.Button()
        Me.nombre = New System.Windows.Forms.TextBox()
        Me.contra1 = New System.Windows.Forms.TextBox()
        Me.contra2 = New System.Windows.Forms.TextBox()
        Me.admin = New System.Windows.Forms.CheckBox()
        Me.telefono = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Idlabel = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.foto = New System.Windows.Forms.PictureBox()
        Me.buscar = New System.Windows.Forms.OpenFileDialog()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.foto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(20, 536)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(576, 20)
        Me.Panel4.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(20, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(576, 35)
        Me.Panel2.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Alta de usuario"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(20, 556)
        Me.Panel1.TabIndex = 11
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(596, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(20, 556)
        Me.Panel3.TabIndex = 10
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.cancelar)
        Me.Panel5.Controls.Add(Me.aceptar)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(20, 484)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(576, 52)
        Me.Panel5.TabIndex = 14
        '
        'cancelar
        '
        Me.cancelar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cancelar.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancelar.Location = New System.Drawing.Point(265, 0)
        Me.cancelar.Name = "cancelar"
        Me.cancelar.Size = New System.Drawing.Size(311, 52)
        Me.cancelar.TabIndex = 19
        Me.cancelar.Tag = "Cancelar"
        Me.cancelar.UseVisualStyleBackColor = True
        '
        'aceptar
        '
        Me.aceptar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.aceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.aceptar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.aceptar.Dock = System.Windows.Forms.DockStyle.Left
        Me.aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.aceptar.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aceptar.Location = New System.Drawing.Point(0, 0)
        Me.aceptar.Name = "aceptar"
        Me.aceptar.Size = New System.Drawing.Size(265, 52)
        Me.aceptar.TabIndex = 18
        Me.aceptar.Tag = "Aceptar"
        Me.aceptar.UseVisualStyleBackColor = True
        '
        'nombre
        '
        Me.nombre.Location = New System.Drawing.Point(231, 94)
        Me.nombre.Name = "nombre"
        Me.nombre.Size = New System.Drawing.Size(272, 29)
        Me.nombre.TabIndex = 15
        '
        'contra1
        '
        Me.contra1.Location = New System.Drawing.Point(231, 150)
        Me.contra1.Name = "contra1"
        Me.contra1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.contra1.Size = New System.Drawing.Size(272, 29)
        Me.contra1.TabIndex = 16
        '
        'contra2
        '
        Me.contra2.Location = New System.Drawing.Point(231, 205)
        Me.contra2.Name = "contra2"
        Me.contra2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.contra2.Size = New System.Drawing.Size(272, 29)
        Me.contra2.TabIndex = 17
        '
        'admin
        '
        Me.admin.AutoSize = True
        Me.admin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.admin.Location = New System.Drawing.Point(231, 261)
        Me.admin.Name = "admin"
        Me.admin.Size = New System.Drawing.Size(124, 27)
        Me.admin.TabIndex = 18
        Me.admin.Text = "Administrador"
        Me.admin.UseVisualStyleBackColor = True
        '
        'telefono
        '
        Me.telefono.Location = New System.Drawing.Point(231, 300)
        Me.telefono.Name = "telefono"
        Me.telefono.Size = New System.Drawing.Size(272, 29)
        Me.telefono.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(142, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 23)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Nombre:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(110, 150)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 23)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Contraseña:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(49, 205)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(143, 23)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Repita Contraseña:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(133, 305)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 23)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Telefono:"
        '
        'Idlabel
        '
        Me.Idlabel.AutoSize = True
        Me.Idlabel.Location = New System.Drawing.Point(188, 55)
        Me.Idlabel.Name = "Idlabel"
        Me.Idlabel.Size = New System.Drawing.Size(26, 23)
        Me.Idlabel.TabIndex = 26
        Me.Idlabel.Text = "Id:"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Buscar", "Hombre", "Mujer", "Bicicleta", "Carro", "Flor"})
        Me.ComboBox1.Location = New System.Drawing.Point(352, 351)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(151, 30)
        Me.ComboBox1.TabIndex = 28
        '
        'foto
        '
        Me.foto.BackgroundImage = Global.SICDIN30.My.Resources.Resources.hombre
        Me.foto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.foto.Location = New System.Drawing.Point(138, 351)
        Me.foto.Name = "foto"
        Me.foto.Size = New System.Drawing.Size(208, 127)
        Me.foto.TabIndex = 27
        Me.foto.TabStop = False
        '
        'buscar
        '
        Me.buscar.Filter = "Archivos de imagen |*.jpg;*.bmp;*.png"
        '
        'userform
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(616, 556)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.foto)
        Me.Controls.Add(Me.Idlabel)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.telefono)
        Me.Controls.Add(Me.admin)
        Me.Controls.Add(Me.contra2)
        Me.Controls.Add(Me.contra1)
        Me.Controls.Add(Me.nombre)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "userform"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "userform"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.foto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents cancelar As Button
    Friend WithEvents aceptar As Button
    Friend WithEvents nombre As TextBox
    Friend WithEvents contra1 As TextBox
    Friend WithEvents contra2 As TextBox
    Friend WithEvents admin As CheckBox
    Friend WithEvents telefono As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Idlabel As Label
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents foto As PictureBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents buscar As OpenFileDialog
End Class
