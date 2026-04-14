<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class perdida
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.clavelabel = New System.Windows.Forms.Label()
        Me.Descripcion = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.cancel = New System.Windows.Forms.Button()
        Me.okv = New System.Windows.Forms.Button()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(20, 378)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(545, 20)
        Me.Panel4.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(20, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(545, 20)
        Me.Panel2.TabIndex = 12
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(20, 398)
        Me.Panel1.TabIndex = 11
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(565, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(20, 398)
        Me.Panel3.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(215, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(159, 29)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Declarar perdida"
        '
        'clavelabel
        '
        Me.clavelabel.AutoSize = True
        Me.clavelabel.Location = New System.Drawing.Point(80, 69)
        Me.clavelabel.Name = "clavelabel"
        Me.clavelabel.Size = New System.Drawing.Size(67, 29)
        Me.clavelabel.TabIndex = 15
        Me.clavelabel.Text = "Clave:"
        '
        'Descripcion
        '
        Me.Descripcion.AutoSize = True
        Me.Descripcion.Location = New System.Drawing.Point(26, 107)
        Me.Descripcion.Name = "Descripcion"
        Me.Descripcion.Size = New System.Drawing.Size(121, 29)
        Me.Descripcion.TabIndex = 16
        Me.Descripcion.Text = "Descripción:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(51, 148)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 29)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Cantidad:"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(153, 146)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(120, 34)
        Me.NumericUpDown1.TabIndex = 18
        Me.NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 177)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 29)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Motivo"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.cancel)
        Me.Panel5.Controls.Add(Me.okv)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(20, 316)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(545, 62)
        Me.Panel5.TabIndex = 20
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.TextBox1)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(20, 210)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(545, 106)
        Me.Panel6.TabIndex = 21
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(545, 106)
        Me.TextBox1.TabIndex = 0
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'cancel
        '
        Me.cancel.AutoSize = True
        Me.cancel.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cancel.Location = New System.Drawing.Point(270, 0)
        Me.cancel.Name = "cancel"
        Me.cancel.Size = New System.Drawing.Size(275, 62)
        Me.cancel.TabIndex = 11
        Me.cancel.Tag = "Aceptar"
        Me.cancel.UseVisualStyleBackColor = True
        '
        'okv
        '
        Me.okv.AutoSize = True
        Me.okv.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.okv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.okv.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.okv.Dock = System.Windows.Forms.DockStyle.Left
        Me.okv.Enabled = False
        Me.okv.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.okv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.okv.Location = New System.Drawing.Point(0, 0)
        Me.okv.Name = "okv"
        Me.okv.Size = New System.Drawing.Size(270, 62)
        Me.okv.TabIndex = 10
        Me.okv.Tag = "Aceptar"
        Me.okv.UseVisualStyleBackColor = True
        '
        'perdida
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 27.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(585, 398)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Descripcion)
        Me.Controls.Add(Me.clavelabel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "perdida"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "t"
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents clavelabel As Label
    Friend WithEvents Descripcion As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents okv As Button
    Friend WithEvents cancel As Button
    Friend WithEvents ErrorProvider1 As ErrorProvider
End Class
