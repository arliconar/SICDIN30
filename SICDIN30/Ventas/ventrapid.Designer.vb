<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Ventarapid
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
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cantidad = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.clave = New System.Windows.Forms.Label()
        Me.descripcion = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.cancelar = New System.Windows.Forms.Button()
        Me.aceptar = New System.Windows.Forms.Button()
        Me.Precio = New System.Windows.Forms.Label()
        Me.total = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Panel5.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 24)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(18, 271)
        Me.Panel4.TabIndex = 11
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(511, 24)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(18, 271)
        Me.Panel3.TabIndex = 10
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 295)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(529, 24)
        Me.Panel2.TabIndex = 9
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(529, 24)
        Me.Panel1.TabIndex = 8
        '
        'cantidad
        '
        Me.cantidad.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.cantidad.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cantidad.Location = New System.Drawing.Point(148, 113)
        Me.cantidad.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cantidad.Name = "cantidad"
        Me.cantidad.Size = New System.Drawing.Size(101, 34)
        Me.cantidad.TabIndex = 12
        Me.cantidad.Text = "1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(59, 113)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 29)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Cantidad:"
        '
        'clave
        '
        Me.clave.AutoSize = True
        Me.clave.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clave.Location = New System.Drawing.Point(88, 44)
        Me.clave.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.clave.Name = "clave"
        Me.clave.Size = New System.Drawing.Size(67, 29)
        Me.clave.TabIndex = 14
        Me.clave.Text = "Clave:"
        '
        'descripcion
        '
        Me.descripcion.AutoSize = True
        Me.descripcion.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.descripcion.Location = New System.Drawing.Point(34, 73)
        Me.descripcion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.descripcion.Name = "descripcion"
        Me.descripcion.Size = New System.Drawing.Size(121, 29)
        Me.descripcion.TabIndex = 15
        Me.descripcion.Text = "Descripción:"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.cancelar)
        Me.Panel5.Controls.Add(Me.aceptar)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(18, 246)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(493, 49)
        Me.Panel5.TabIndex = 16
        '
        'cancelar
        '
        Me.cancelar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cancelar.Location = New System.Drawing.Point(246, 0)
        Me.cancelar.Name = "cancelar"
        Me.cancelar.Size = New System.Drawing.Size(247, 49)
        Me.cancelar.TabIndex = 11
        Me.cancelar.Tag = "Aceptar"
        Me.cancelar.UseVisualStyleBackColor = True
        '
        'aceptar
        '
        Me.aceptar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.aceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.aceptar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.aceptar.Dock = System.Windows.Forms.DockStyle.Left
        Me.aceptar.Location = New System.Drawing.Point(0, 0)
        Me.aceptar.Name = "aceptar"
        Me.aceptar.Size = New System.Drawing.Size(246, 49)
        Me.aceptar.TabIndex = 10
        Me.aceptar.Tag = "Aceptar"
        Me.aceptar.UseVisualStyleBackColor = True
        '
        'Precio
        '
        Me.Precio.AutoSize = True
        Me.Precio.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Precio.Location = New System.Drawing.Point(75, 154)
        Me.Precio.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Precio.Name = "Precio"
        Me.Precio.Size = New System.Drawing.Size(112, 29)
        Me.Precio.TabIndex = 17
        Me.Precio.Text = "Precio:$0.0"
        '
        'total
        '
        Me.total.AutoSize = True
        Me.total.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.total.Location = New System.Drawing.Point(88, 195)
        Me.total.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.total.Name = "total"
        Me.total.Size = New System.Drawing.Size(99, 29)
        Me.total.TabIndex = 18
        Me.total.Text = "Total:$0.0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(202, 24)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(124, 29)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Venta rapida"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Ventarapid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 27.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(529, 319)
        Me.Controls.Add(Me.cantidad)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.total)
        Me.Controls.Add(Me.Precio)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.descripcion)
        Me.Controls.Add(Me.clave)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Ventarapid"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form2"
        Me.Panel5.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents cantidad As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents clave As Label
    Friend WithEvents descripcion As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents cancelar As Button
    Friend WithEvents aceptar As Button
    Friend WithEvents Precio As Label
    Friend WithEvents total As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents ErrorProvider1 As ErrorProvider
End Class
