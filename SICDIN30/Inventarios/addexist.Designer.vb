<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class addexist
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.clavelabel = New System.Windows.Forms.Label()
        Me.deslabel = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.cancelar = New System.Windows.Forms.Button()
        Me.aceptar = New System.Windows.Forms.Button()
        Me.existencias = New System.Windows.Forms.NumericUpDown()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.existencias, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(20, 234)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(607, 21)
        Me.Panel4.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(20, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(607, 31)
        Me.Panel2.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(157, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Busqueda de artículo"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(20, 255)
        Me.Panel1.TabIndex = 11
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(627, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(20, 255)
        Me.Panel3.TabIndex = 10
        '
        'clavelabel
        '
        Me.clavelabel.AutoSize = True
        Me.clavelabel.Location = New System.Drawing.Point(123, 72)
        Me.clavelabel.Name = "clavelabel"
        Me.clavelabel.Size = New System.Drawing.Size(53, 23)
        Me.clavelabel.TabIndex = 14
        Me.clavelabel.Text = "Clave:"
        '
        'deslabel
        '
        Me.deslabel.AutoSize = True
        Me.deslabel.Location = New System.Drawing.Point(69, 101)
        Me.deslabel.Name = "deslabel"
        Me.deslabel.Size = New System.Drawing.Size(96, 23)
        Me.deslabel.TabIndex = 15
        Me.deslabel.Text = "Descripción:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(69, 130)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 23)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Existencias:"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.cancelar)
        Me.Panel5.Controls.Add(Me.aceptar)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(20, 187)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(607, 47)
        Me.Panel5.TabIndex = 17
        '
        'cancelar
        '
        Me.cancelar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cancelar.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancelar.Location = New System.Drawing.Point(289, 0)
        Me.cancelar.Name = "cancelar"
        Me.cancelar.Size = New System.Drawing.Size(318, 47)
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
        Me.aceptar.Size = New System.Drawing.Size(289, 47)
        Me.aceptar.TabIndex = 18
        Me.aceptar.Tag = "Aceptar"
        Me.aceptar.UseVisualStyleBackColor = True
        '
        'existencias
        '
        Me.existencias.Location = New System.Drawing.Point(192, 130)
        Me.existencias.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.existencias.Name = "existencias"
        Me.existencias.Size = New System.Drawing.Size(159, 29)
        Me.existencias.TabIndex = 18
        '
        'addexist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(647, 255)
        Me.Controls.Add(Me.existencias)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.deslabel)
        Me.Controls.Add(Me.clavelabel)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "addexist"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "addexist"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        CType(Me.existencias, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents clavelabel As Label
    Friend WithEvents deslabel As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents cancelar As Button
    Friend WithEvents aceptar As Button
    Friend WithEvents existencias As NumericUpDown
End Class
