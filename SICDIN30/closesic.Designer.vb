<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class closesic
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.salirbut = New System.Windows.Forms.Button()
        Me.userbut = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Cancelb = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(15, 235)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(570, 16)
        Me.Panel4.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(15, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(570, 16)
        Me.Panel2.TabIndex = 12
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(15, 251)
        Me.Panel1.TabIndex = 11
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(585, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(15, 251)
        Me.Panel3.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(106, 38)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(448, 55)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "¿Qué desea hacer?"
        '
        'salirbut
        '
        Me.salirbut.BackgroundImage = Global.SICDIN30.My.Resources.Resources.Logout_37127
        Me.salirbut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.salirbut.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.salirbut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.salirbut.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.salirbut.Location = New System.Drawing.Point(225, 133)
        Me.salirbut.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.salirbut.Name = "salirbut"
        Me.salirbut.Size = New System.Drawing.Size(135, 72)
        Me.salirbut.TabIndex = 15
        Me.salirbut.Tag = "Salir"
        Me.salirbut.UseVisualStyleBackColor = True
        '
        'userbut
        '
        Me.userbut.BackgroundImage = Global.SICDIN30.My.Resources.Resources.founder_icon_icons_com_76996
        Me.userbut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.userbut.DialogResult = System.Windows.Forms.DialogResult.No
        Me.userbut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.userbut.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.userbut.Location = New System.Drawing.Point(58, 133)
        Me.userbut.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.userbut.Name = "userbut"
        Me.userbut.Size = New System.Drawing.Size(135, 72)
        Me.userbut.TabIndex = 16
        Me.userbut.Tag = "Cambiar usuario"
        Me.userbut.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SICDIN30.My.Resources.Resources.dudaemo
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.Location = New System.Drawing.Point(30, 38)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(85, 83)
        Me.PictureBox1.TabIndex = 17
        Me.PictureBox1.TabStop = False
        '
        'Cancelb
        '
        Me.Cancelb.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.Cancelb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Cancelb.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancelb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancelb.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancelb.Location = New System.Drawing.Point(389, 133)
        Me.Cancelb.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Cancelb.Name = "Cancelb"
        Me.Cancelb.Size = New System.Drawing.Size(135, 72)
        Me.Cancelb.TabIndex = 18
        Me.Cancelb.Tag = "Cancelar"
        Me.Cancelb.UseVisualStyleBackColor = True
        '
        'closesic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(600, 251)
        Me.Controls.Add(Me.Cancelb)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.userbut)
        Me.Controls.Add(Me.salirbut)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "closesic"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Salida de SICDIN"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents salirbut As Button
    Friend WithEvents userbut As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Cancelb As Button
End Class
