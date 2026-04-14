<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Formulas
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.pmen = New System.Windows.Forms.RadioButton()
        Me.pmay = New System.Windows.Forms.RadioButton()
        Me.exis = New System.Windows.Forms.RadioButton()
        Me.less = New System.Windows.Forms.RadioButton()
        Me.equal = New System.Windows.Forms.RadioButton()
        Me.greater = New System.Windows.Forms.RadioButton()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(593, 28)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(19, 228)
        Me.Panel2.TabIndex = 15
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 28)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 228)
        Me.Panel3.TabIndex = 17
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 256)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(612, 20)
        Me.Panel4.TabIndex = 16
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(612, 28)
        Me.Panel1.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(43, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(373, 29)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Seleccione la forma de busqueda"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.exis)
        Me.Panel5.Controls.Add(Me.pmay)
        Me.Panel5.Controls.Add(Me.pmen)
        Me.Panel5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(24, 72)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(525, 64)
        Me.Panel5.TabIndex = 19
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.greater)
        Me.Panel6.Controls.Add(Me.equal)
        Me.Panel6.Controls.Add(Me.less)
        Me.Panel6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel6.Location = New System.Drawing.Point(24, 142)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(525, 64)
        Me.Panel6.TabIndex = 20
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Button2)
        Me.Panel7.Controls.Add(Me.Button1)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel7.Location = New System.Drawing.Point(19, 212)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(574, 44)
        Me.Panel7.TabIndex = 22
        '
        'pmen
        '
        Me.pmen.AutoSize = True
        Me.pmen.Checked = True
        Me.pmen.Location = New System.Drawing.Point(14, 24)
        Me.pmen.Name = "pmen"
        Me.pmen.Size = New System.Drawing.Size(165, 24)
        Me.pmen.TabIndex = 0
        Me.pmen.TabStop = True
        Me.pmen.Text = "Precio Menudeo ($)"
        Me.pmen.UseVisualStyleBackColor = True
        '
        'pmay
        '
        Me.pmay.AutoSize = True
        Me.pmay.Location = New System.Drawing.Point(185, 24)
        Me.pmay.Name = "pmay"
        Me.pmay.Size = New System.Drawing.Size(168, 24)
        Me.pmay.TabIndex = 1
        Me.pmay.Text = "Precio Mayoreo ($$)"
        Me.pmay.UseVisualStyleBackColor = True
        '
        'exis
        '
        Me.exis.AutoSize = True
        Me.exis.Location = New System.Drawing.Point(359, 24)
        Me.exis.Name = "exis"
        Me.exis.Size = New System.Drawing.Size(130, 24)
        Me.exis.TabIndex = 2
        Me.exis.Text = "Existencias (#)"
        Me.exis.UseVisualStyleBackColor = True
        '
        'less
        '
        Me.less.AutoSize = True
        Me.less.Location = New System.Drawing.Point(12, 23)
        Me.less.Name = "less"
        Me.less.Size = New System.Drawing.Size(126, 24)
        Me.less.TabIndex = 0
        Me.less.Text = "Menor que (<)"
        Me.less.UseVisualStyleBackColor = True
        '
        'equal
        '
        Me.equal.AutoSize = True
        Me.equal.Checked = True
        Me.equal.Location = New System.Drawing.Point(185, 23)
        Me.equal.Name = "equal"
        Me.equal.Size = New System.Drawing.Size(116, 24)
        Me.equal.TabIndex = 1
        Me.equal.TabStop = True
        Me.equal.Text = "Igual que (=)"
        Me.equal.UseVisualStyleBackColor = True
        '
        'greater
        '
        Me.greater.AutoSize = True
        Me.greater.Location = New System.Drawing.Point(359, 24)
        Me.greater.Name = "greater"
        Me.greater.Size = New System.Drawing.Size(124, 24)
        Me.greater.TabIndex = 2
        Me.greater.Text = "Mayor que (>)"
        Me.greater.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Button2.Location = New System.Drawing.Point(204, 0)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(370, 44)
        Me.Button2.TabIndex = 2
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Button1.Location = New System.Drawing.Point(0, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(204, 44)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Formulas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(612, 276)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Formulas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Formulas"
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents exis As RadioButton
    Friend WithEvents pmay As RadioButton
    Friend WithEvents pmen As RadioButton
    Friend WithEvents greater As RadioButton
    Friend WithEvents equal As RadioButton
    Friend WithEvents less As RadioButton
End Class
