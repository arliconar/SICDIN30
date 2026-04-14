<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class trabajadorform
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.nombrec = New System.Windows.Forms.Label()
        Me.idlabel = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.destajo = New System.Windows.Forms.CheckBox()
        Me.deslabel = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.destajotext = New System.Windows.Forms.NumericUpDown()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.cancelar = New System.Windows.Forms.Button()
        Me.aceptar = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.destajotext, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(436, 34)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(19, 443)
        Me.Panel2.TabIndex = 11
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 34)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(17, 443)
        Me.Panel3.TabIndex = 13
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 477)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(455, 32)
        Me.Panel4.TabIndex = 12
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Controls.Add(Me.nombrec)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(455, 34)
        Me.Panel1.TabIndex = 10
        '
        'nombrec
        '
        Me.nombrec.AutoSize = True
        Me.nombrec.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nombrec.ForeColor = System.Drawing.Color.White
        Me.nombrec.Location = New System.Drawing.Point(159, 2)
        Me.nombrec.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.nombrec.Name = "nombrec"
        Me.nombrec.Size = New System.Drawing.Size(153, 32)
        Me.nombrec.TabIndex = 0
        Me.nombrec.Text = "Trabajador"
        Me.nombrec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'idlabel
        '
        Me.idlabel.AutoSize = True
        Me.idlabel.Location = New System.Drawing.Point(151, 39)
        Me.idlabel.Name = "idlabel"
        Me.idlabel.Size = New System.Drawing.Size(33, 29)
        Me.idlabel.TabIndex = 14
        Me.idlabel.Text = "Id:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(96, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 29)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Nombre:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(84, 120)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 29)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Dirección:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(42, 213)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(142, 29)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Telefono casa:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 258)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(158, 29)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Telefono celular:"
        '
        'destajo
        '
        Me.destajo.AutoSize = True
        Me.destajo.Location = New System.Drawing.Point(83, 306)
        Me.destajo.Name = "destajo"
        Me.destajo.Size = New System.Drawing.Size(101, 33)
        Me.destajo.TabIndex = 4
        Me.destajo.Text = "Destajo"
        Me.destajo.UseVisualStyleBackColor = True
        '
        'deslabel
        '
        Me.deslabel.AutoSize = True
        Me.deslabel.Location = New System.Drawing.Point(190, 305)
        Me.deslabel.Name = "deslabel"
        Me.deslabel.Size = New System.Drawing.Size(108, 29)
        Me.deslabel.TabIndex = 20
        Me.deslabel.Text = "% Destajo:"
        Me.deslabel.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(96, 350)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 29)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Salario:$"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(178, 83)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(190, 34)
        Me.TextBox1.TabIndex = 0
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(178, 123)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(190, 87)
        Me.TextBox2.TabIndex = 1
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(178, 216)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(190, 34)
        Me.TextBox3.TabIndex = 2
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(178, 255)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(190, 34)
        Me.TextBox4.TabIndex = 3
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(178, 353)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(190, 34)
        Me.TextBox5.TabIndex = 5
        Me.TextBox5.Text = "0"
        '
        'destajotext
        '
        Me.destajotext.Location = New System.Drawing.Point(291, 305)
        Me.destajotext.Name = "destajotext"
        Me.destajotext.Size = New System.Drawing.Size(120, 34)
        Me.destajotext.TabIndex = 5
        Me.destajotext.Value = New Decimal(New Integer() {50, 0, 0, 0})
        Me.destajotext.Visible = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.cancelar)
        Me.Panel5.Controls.Add(Me.aceptar)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(17, 424)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(419, 53)
        Me.Panel5.TabIndex = 30
        '
        'cancelar
        '
        Me.cancelar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cancelar.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancelar.Location = New System.Drawing.Point(182, 0)
        Me.cancelar.Name = "cancelar"
        Me.cancelar.Size = New System.Drawing.Size(237, 53)
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
        Me.aceptar.Enabled = False
        Me.aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.aceptar.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aceptar.Location = New System.Drawing.Point(0, 0)
        Me.aceptar.Name = "aceptar"
        Me.aceptar.Size = New System.Drawing.Size(182, 53)
        Me.aceptar.TabIndex = 18
        Me.aceptar.Tag = "Aceptar"
        Me.aceptar.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'trabajadorform
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 27.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(455, 509)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.destajotext)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.deslabel)
        Me.Controls.Add(Me.destajo)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.idlabel)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial Narrow", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "trabajadorform"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.destajotext, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents nombrec As Label
    Friend WithEvents idlabel As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents destajo As CheckBox
    Friend WithEvents deslabel As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents destajotext As NumericUpDown
    Friend WithEvents Panel5 As Panel
    Friend WithEvents cancelar As Button
    Friend WithEvents aceptar As Button
    Friend WithEvents ErrorProvider1 As ErrorProvider
End Class
