<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Visor
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
        Me.copiar = New System.Windows.Forms.Button()
        Me.okv = New System.Windows.Forms.Button()
        Me.foto = New System.Windows.Forms.PictureBox()
        Me.siguiente = New System.Windows.Forms.Button()
        Me.anterior = New System.Windows.Forms.Button()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.foto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Cursor = System.Windows.Forms.Cursors.SizeNS
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(15, 350)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(570, 16)
        Me.Panel4.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Cursor = System.Windows.Forms.Cursors.SizeNS
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(15, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(570, 16)
        Me.Panel2.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Visor de imagenes"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Cursor = System.Windows.Forms.Cursors.SizeWE
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(15, 366)
        Me.Panel1.TabIndex = 11
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Cursor = System.Windows.Forms.Cursors.SizeWE
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(585, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(15, 366)
        Me.Panel3.TabIndex = 10
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Panel5.Controls.Add(Me.copiar)
        Me.Panel5.Controls.Add(Me.okv)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(15, 269)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(570, 81)
        Me.Panel5.TabIndex = 14
        '
        'copiar
        '
        Me.copiar.AutoSize = True
        Me.copiar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.copiar
        Me.copiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.copiar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.copiar.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copiar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.copiar.Location = New System.Drawing.Point(272, 0)
        Me.copiar.Margin = New System.Windows.Forms.Padding(2)
        Me.copiar.Name = "copiar"
        Me.copiar.Size = New System.Drawing.Size(298, 81)
        Me.copiar.TabIndex = 10
        Me.copiar.Tag = "Copiar"
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
        Me.okv.Margin = New System.Windows.Forms.Padding(2)
        Me.okv.Name = "okv"
        Me.okv.Size = New System.Drawing.Size(272, 81)
        Me.okv.TabIndex = 9
        Me.okv.Tag = "Aceptar"
        Me.okv.UseVisualStyleBackColor = True
        '
        'foto
        '
        Me.foto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.foto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.foto.Location = New System.Drawing.Point(15, 16)
        Me.foto.Margin = New System.Windows.Forms.Padding(2)
        Me.foto.Name = "foto"
        Me.foto.Size = New System.Drawing.Size(570, 253)
        Me.foto.TabIndex = 15
        Me.foto.TabStop = False
        '
        'siguiente
        '
        Me.siguiente.Dock = System.Windows.Forms.DockStyle.Right
        Me.siguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.siguiente.Location = New System.Drawing.Point(563, 16)
        Me.siguiente.Name = "siguiente"
        Me.siguiente.Size = New System.Drawing.Size(22, 253)
        Me.siguiente.TabIndex = 16
        Me.siguiente.Text = ">"
        Me.siguiente.UseVisualStyleBackColor = True
        Me.siguiente.Visible = False
        '
        'anterior
        '
        Me.anterior.Dock = System.Windows.Forms.DockStyle.Left
        Me.anterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.anterior.Location = New System.Drawing.Point(15, 16)
        Me.anterior.Name = "anterior"
        Me.anterior.Size = New System.Drawing.Size(22, 253)
        Me.anterior.TabIndex = 17
        Me.anterior.Text = "<"
        Me.anterior.UseVisualStyleBackColor = True
        Me.anterior.Visible = False
        '
        'Visor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(600, 366)
        Me.ControlBox = False
        Me.Controls.Add(Me.anterior)
        Me.Controls.Add(Me.siguiente)
        Me.Controls.Add(Me.foto)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Visor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SICDIN 3.0: Visor de imagenes"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.foto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents foto As PictureBox
    Friend WithEvents okv As Button
    Friend WithEvents copiar As Button
    Friend WithEvents siguiente As Button
    Friend WithEvents anterior As Button
End Class
