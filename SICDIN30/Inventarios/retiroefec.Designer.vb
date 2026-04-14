<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class retiroefec
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
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cantidad = New System.Windows.Forms.TextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.copiar = New System.Windows.Forms.Button()
        Me.okv = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.razon = New System.Windows.Forms.TextBox()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(20, 269)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(610, 29)
        Me.Panel4.TabIndex = 17
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(20, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(610, 25)
        Me.Panel2.TabIndex = 16
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(20, 298)
        Me.Panel1.TabIndex = 15
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(630, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(20, 298)
        Me.Panel3.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(248, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(164, 29)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Retiro de efectivo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(140, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(107, 29)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Cantidad:$"
        '
        'cantidad
        '
        Me.cantidad.Location = New System.Drawing.Point(253, 83)
        Me.cantidad.Name = "cantidad"
        Me.cantidad.Size = New System.Drawing.Size(263, 34)
        Me.cantidad.TabIndex = 0
        Me.cantidad.Text = "0"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.copiar)
        Me.Panel5.Controls.Add(Me.okv)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(20, 188)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(610, 81)
        Me.Panel5.TabIndex = 21
        '
        'copiar
        '
        Me.copiar.AutoSize = True
        Me.copiar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.copiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.copiar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.copiar.Dock = System.Windows.Forms.DockStyle.Left
        Me.copiar.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copiar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.copiar.Location = New System.Drawing.Point(307, 0)
        Me.copiar.Name = "copiar"
        Me.copiar.Size = New System.Drawing.Size(303, 81)
        Me.copiar.TabIndex = 3
        Me.copiar.Tag = "Cancelar"
        Me.copiar.UseVisualStyleBackColor = True
        '
        'okv
        '
        Me.okv.AutoSize = True
        Me.okv.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.okv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.okv.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.okv.Dock = System.Windows.Forms.DockStyle.Left
        Me.okv.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.okv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.okv.Location = New System.Drawing.Point(0, 0)
        Me.okv.Name = "okv"
        Me.okv.Size = New System.Drawing.Size(307, 81)
        Me.okv.TabIndex = 2
        Me.okv.Tag = "Aceptar"
        Me.okv.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(172, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 29)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Razón:"
        '
        'razon
        '
        Me.razon.Location = New System.Drawing.Point(253, 133)
        Me.razon.Name = "razon"
        Me.razon.Size = New System.Drawing.Size(263, 34)
        Me.razon.TabIndex = 1
        '
        'retiroefec
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 27.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(650, 298)
        Me.Controls.Add(Me.razon)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.cantidad)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "retiroefec"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "5"
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents copiar As Button
    Friend WithEvents okv As Button
    Public WithEvents cantidad As TextBox
    Friend WithEvents Label3 As Label
    Public WithEvents razon As TextBox
End Class
