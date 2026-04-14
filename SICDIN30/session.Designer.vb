<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class session
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(session))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.imagen = New System.Windows.Forms.PictureBox()
        Me.Fechalabel = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.panelusu = New System.Windows.Forms.Panel()
        Me.panelbuttons = New System.Windows.Forms.Panel()
        Me.cancel = New System.Windows.Forms.Button()
        Me.aceptar = New System.Windows.Forms.Button()
        Me.passbox = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panelri = New System.Windows.Forms.Panel()
        Me.Panelleft = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.ver = New System.Windows.Forms.Label()
        CType(Me.imagen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.panelusu.SuspendLayout()
        Me.panelbuttons.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial Narrow", 72.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(118, 38)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(447, 110)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SICDIN 3.0"
        '
        'imagen
        '
        Me.imagen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.imagen.Location = New System.Drawing.Point(20, 52)
        Me.imagen.Margin = New System.Windows.Forms.Padding(2)
        Me.imagen.Name = "imagen"
        Me.imagen.Size = New System.Drawing.Size(86, 76)
        Me.imagen.TabIndex = 1
        Me.imagen.TabStop = False
        '
        'Fechalabel
        '
        Me.Fechalabel.AutoSize = True
        Me.Fechalabel.Font = New System.Drawing.Font("Arial Narrow", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Fechalabel.Location = New System.Drawing.Point(218, 141)
        Me.Fechalabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Fechalabel.Name = "Fechalabel"
        Me.Fechalabel.Size = New System.Drawing.Size(91, 37)
        Me.Fechalabel.TabIndex = 2
        Me.Fechalabel.Text = "Fecha"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Red
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Transparent
        Me.Button1.Location = New System.Drawing.Point(567, 0)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(37, 31)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "X"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(15, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(604, 31)
        Me.Panel1.TabIndex = 4
        '
        'panelusu
        '
        Me.panelusu.Controls.Add(Me.panelbuttons)
        Me.panelusu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelusu.Location = New System.Drawing.Point(150, 0)
        Me.panelusu.Margin = New System.Windows.Forms.Padding(2)
        Me.panelusu.Name = "panelusu"
        Me.panelusu.Size = New System.Drawing.Size(304, 101)
        Me.panelusu.TabIndex = 5
        '
        'panelbuttons
        '
        Me.panelbuttons.Controls.Add(Me.cancel)
        Me.panelbuttons.Controls.Add(Me.aceptar)
        Me.panelbuttons.Controls.Add(Me.passbox)
        Me.panelbuttons.Location = New System.Drawing.Point(89, 28)
        Me.panelbuttons.Margin = New System.Windows.Forms.Padding(2)
        Me.panelbuttons.Name = "panelbuttons"
        Me.panelbuttons.Size = New System.Drawing.Size(190, 63)
        Me.panelbuttons.TabIndex = 3
        Me.panelbuttons.Visible = False
        '
        'cancel
        '
        Me.cancel.BackgroundImage = Global.SICDIN30.My.Resources.Resources.cancel_77947
        Me.cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cancel.Location = New System.Drawing.Point(91, 26)
        Me.cancel.Margin = New System.Windows.Forms.Padding(2)
        Me.cancel.Name = "cancel"
        Me.cancel.Size = New System.Drawing.Size(99, 37)
        Me.cancel.TabIndex = 2
        Me.cancel.UseVisualStyleBackColor = True
        '
        'aceptar
        '
        Me.aceptar.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.aceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.aceptar.Dock = System.Windows.Forms.DockStyle.Left
        Me.aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.aceptar.Location = New System.Drawing.Point(0, 26)
        Me.aceptar.Margin = New System.Windows.Forms.Padding(2)
        Me.aceptar.Name = "aceptar"
        Me.aceptar.Size = New System.Drawing.Size(91, 37)
        Me.aceptar.TabIndex = 1
        Me.aceptar.UseVisualStyleBackColor = True
        '
        'passbox
        '
        Me.passbox.Dock = System.Windows.Forms.DockStyle.Top
        Me.passbox.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.passbox.Location = New System.Drawing.Point(0, 0)
        Me.passbox.Margin = New System.Windows.Forms.Padding(2)
        Me.passbox.Name = "passbox"
        Me.passbox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.passbox.Size = New System.Drawing.Size(190, 26)
        Me.passbox.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.panelusu)
        Me.Panel2.Controls.Add(Me.Panelri)
        Me.Panel2.Controls.Add(Me.Panelleft)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(15, 190)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(604, 101)
        Me.Panel2.TabIndex = 6
        '
        'Panelri
        '
        Me.Panelri.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panelri.Location = New System.Drawing.Point(454, 0)
        Me.Panelri.Margin = New System.Windows.Forms.Padding(2)
        Me.Panelri.Name = "Panelri"
        Me.Panelri.Size = New System.Drawing.Size(150, 101)
        Me.Panelri.TabIndex = 7
        '
        'Panelleft
        '
        Me.Panelleft.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panelleft.Location = New System.Drawing.Point(0, 0)
        Me.Panelleft.Margin = New System.Windows.Forms.Padding(2)
        Me.Panelleft.Name = "Panelleft"
        Me.Panelleft.Size = New System.Drawing.Size(150, 101)
        Me.Panelleft.TabIndex = 6
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(15, 291)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(604, 21)
        Me.Panel3.TabIndex = 7
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(15, 312)
        Me.Panel6.TabIndex = 11
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel7.Location = New System.Drawing.Point(619, 0)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(15, 312)
        Me.Panel7.TabIndex = 10
        '
        'ver
        '
        Me.ver.AutoSize = True
        Me.ver.Location = New System.Drawing.Point(574, 33)
        Me.ver.Name = "ver"
        Me.ver.Size = New System.Drawing.Size(45, 13)
        Me.ver.TabIndex = 12
        Me.ver.Text = "Versión:"
        Me.ver.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'session
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(634, 312)
        Me.Controls.Add(Me.ver)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Fechalabel)
        Me.Controls.Add(Me.imagen)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel7)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "session"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SICDIN 3.0:Inicio de sesión"
        CType(Me.imagen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.panelusu.ResumeLayout(False)
        Me.panelbuttons.ResumeLayout(False)
        Me.panelbuttons.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents imagen As PictureBox
    Friend WithEvents Fechalabel As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents panelusu As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panelri As Panel
    Friend WithEvents Panelleft As Panel
    Friend WithEvents passbox As TextBox
    Friend WithEvents cancel As Button
    Friend WithEvents aceptar As Button
    Friend WithEvents panelbuttons As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents ver As Label
End Class
