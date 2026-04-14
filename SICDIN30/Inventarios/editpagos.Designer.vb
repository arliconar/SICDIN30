<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class editpagos
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
        Me.efectivo = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Creditol = New System.Windows.Forms.Label()
        Me.Credito = New System.Windows.Forms.TextBox()
        Me.Debitol = New System.Windows.Forms.Label()
        Me.Debito = New System.Windows.Forms.TextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.cancel = New System.Windows.Forms.Button()
        Me.aceptar = New System.Windows.Forms.Button()
        Me.totall = New System.Windows.Forms.Label()
        Me.adv = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'efectivo
        '
        Me.efectivo.Location = New System.Drawing.Point(46, 142)
        Me.efectivo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.efectivo.Name = "efectivo"
        Me.efectivo.Size = New System.Drawing.Size(136, 29)
        Me.efectivo.TabIndex = 0
        Me.efectivo.Text = "0"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(20, 340)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(566, 20)
        Me.Panel4.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(20, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(566, 34)
        Me.Panel2.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Editar pago"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(20, 360)
        Me.Panel1.TabIndex = 11
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(586, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(20, 360)
        Me.Panel3.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(41, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 23)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Efectivo:"
        '
        'Creditol
        '
        Me.Creditol.AutoSize = True
        Me.Creditol.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Creditol.Location = New System.Drawing.Point(226, 108)
        Me.Creditol.Name = "Creditol"
        Me.Creditol.Size = New System.Drawing.Size(57, 23)
        Me.Creditol.TabIndex = 15
        Me.Creditol.Text = "Tarjeta"
        '
        'Credito
        '
        Me.Credito.Location = New System.Drawing.Point(231, 142)
        Me.Credito.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Credito.Name = "Credito"
        Me.Credito.Size = New System.Drawing.Size(136, 29)
        Me.Credito.TabIndex = 16
        Me.Credito.Text = "0"
        '
        'Debitol
        '
        Me.Debitol.AutoSize = True
        Me.Debitol.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Debitol.Location = New System.Drawing.Point(415, 108)
        Me.Debitol.Name = "Debitol"
        Me.Debitol.Size = New System.Drawing.Size(105, 23)
        Me.Debitol.TabIndex = 17
        Me.Debitol.Text = "Transferencia"
        '
        'Debito
        '
        Me.Debito.Location = New System.Drawing.Point(420, 142)
        Me.Debito.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Debito.Name = "Debito"
        Me.Debito.Size = New System.Drawing.Size(136, 29)
        Me.Debito.TabIndex = 18
        Me.Debito.Text = "0"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.cancel)
        Me.Panel5.Controls.Add(Me.aceptar)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(20, 240)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(566, 100)
        Me.Panel5.TabIndex = 19
        '
        'cancel
        '
        Me.cancel.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cancel.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancel.Location = New System.Drawing.Point(273, 0)
        Me.cancel.Name = "cancel"
        Me.cancel.Size = New System.Drawing.Size(293, 100)
        Me.cancel.TabIndex = 3
        Me.cancel.Tag = "Cancelar"
        Me.cancel.UseVisualStyleBackColor = True
        '
        'aceptar
        '
        Me.aceptar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.aceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.aceptar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.aceptar.Dock = System.Windows.Forms.DockStyle.Left
        Me.aceptar.Enabled = False
        Me.aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.aceptar.Font = New System.Drawing.Font("Arial Black", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aceptar.Location = New System.Drawing.Point(0, 0)
        Me.aceptar.Name = "aceptar"
        Me.aceptar.Size = New System.Drawing.Size(273, 100)
        Me.aceptar.TabIndex = 2
        Me.aceptar.Tag = "Aceptar"
        Me.aceptar.UseVisualStyleBackColor = True
        '
        'totall
        '
        Me.totall.AutoSize = True
        Me.totall.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.totall.Location = New System.Drawing.Point(230, 37)
        Me.totall.Name = "totall"
        Me.totall.Size = New System.Drawing.Size(78, 23)
        Me.totall.TabIndex = 20
        Me.totall.Text = "Total:$0.0"
        '
        'adv
        '
        Me.adv.AutoSize = True
        Me.adv.ForeColor = System.Drawing.Color.Red
        Me.adv.Location = New System.Drawing.Point(102, 66)
        Me.adv.Name = "adv"
        Me.adv.Size = New System.Drawing.Size(313, 23)
        Me.adv.TabIndex = 21
        Me.adv.Text = "La edición de pagos no coincide con el total"
        Me.adv.Visible = False
        '
        'editpagos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(606, 360)
        Me.Controls.Add(Me.adv)
        Me.Controls.Add(Me.totall)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Debito)
        Me.Controls.Add(Me.Debitol)
        Me.Controls.Add(Me.Credito)
        Me.Controls.Add(Me.Creditol)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.efectivo)
        Me.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "editpagos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "editpagos"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Creditol As Label
    Friend WithEvents Debitol As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents totall As Label
    Friend WithEvents cancel As Button
    Friend WithEvents aceptar As Button
    Public WithEvents efectivo As TextBox
    Public WithEvents Credito As TextBox
    Public WithEvents Debito As TextBox
    Friend WithEvents adv As Label
End Class
