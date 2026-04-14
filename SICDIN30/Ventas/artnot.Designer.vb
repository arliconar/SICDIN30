<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class artnot
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
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.des = New System.Windows.Forms.TextBox()
        Me.precio = New System.Windows.Forms.TextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.cancelar = New System.Windows.Forms.Button()
        Me.aceptar = New System.Windows.Forms.Button()
        Me.titulo = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.paquete = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel5.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(25, 318)
        Me.Panel3.TabIndex = 10
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(843, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(25, 318)
        Me.Panel1.TabIndex = 11
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(25, 293)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(818, 25)
        Me.Panel4.TabIndex = 12
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(25, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(818, 25)
        Me.Panel2.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(31, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(139, 33)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Descripción:"
        '
        'des
        '
        Me.des.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.des.Location = New System.Drawing.Point(31, 107)
        Me.des.Name = "des"
        Me.des.Size = New System.Drawing.Size(496, 38)
        Me.des.TabIndex = 15
        '
        'precio
        '
        Me.precio.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.precio.Location = New System.Drawing.Point(586, 107)
        Me.precio.Name = "precio"
        Me.precio.Size = New System.Drawing.Size(141, 38)
        Me.precio.TabIndex = 17
        Me.precio.Text = "0"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.cancelar)
        Me.Panel5.Controls.Add(Me.aceptar)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(25, 193)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(818, 100)
        Me.Panel5.TabIndex = 18
        '
        'cancelar
        '
        Me.cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cancelar.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancelar.Location = New System.Drawing.Point(309, 0)
        Me.cancelar.Name = "cancelar"
        Me.cancelar.Size = New System.Drawing.Size(509, 100)
        Me.cancelar.TabIndex = 1
        Me.cancelar.Text = "Cancelar"
        Me.cancelar.UseVisualStyleBackColor = True
        '
        'aceptar
        '
        Me.aceptar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.aceptar.Dock = System.Windows.Forms.DockStyle.Left
        Me.aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.aceptar.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aceptar.Location = New System.Drawing.Point(0, 0)
        Me.aceptar.Name = "aceptar"
        Me.aceptar.Size = New System.Drawing.Size(309, 100)
        Me.aceptar.TabIndex = 0
        Me.aceptar.Text = "Aceptar"
        Me.aceptar.UseVisualStyleBackColor = True
        '
        'titulo
        '
        Me.titulo.AutoSize = True
        Me.titulo.Dock = System.Windows.Forms.DockStyle.Left
        Me.titulo.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.titulo.Location = New System.Drawing.Point(25, 25)
        Me.titulo.Name = "titulo"
        Me.titulo.Size = New System.Drawing.Size(361, 33)
        Me.titulo.TabIndex = 19
        Me.titulo.Text = "Introduzca el artículo no registrado"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'paquete
        '
        Me.paquete.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.paquete.Location = New System.Drawing.Point(733, 107)
        Me.paquete.Mask = "&&&&"
        Me.paquete.Name = "paquete"
        Me.paquete.Size = New System.Drawing.Size(100, 38)
        Me.paquete.TabIndex = 20
        Me.paquete.Text = "PZA"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(580, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 33)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Importe:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(727, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(108, 33)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Embalaje"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'artnot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(868, 318)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.paquete)
        Me.Controls.Add(Me.titulo)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.precio)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.des)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "artnot"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "artnot"
        Me.Panel5.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents des As TextBox
    Friend WithEvents precio As TextBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents aceptar As Button
    Friend WithEvents cancelar As Button
    Friend WithEvents titulo As Label
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents Label3 As Label
    Friend WithEvents paquete As MaskedTextBox
    Friend WithEvents Label2 As Label
End Class
