<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class visorpago
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
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lista = New System.Windows.Forms.ListBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.idlt = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.foliol = New System.Windows.Forms.Label()
        Me.fechal = New System.Windows.Forms.Label()
        Me.idl = New System.Windows.Forms.Label()
        Me.userl = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(20, 410)
        Me.Panel4.TabIndex = 7
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(457, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(20, 410)
        Me.Panel3.TabIndex = 6
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 430)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(477, 20)
        Me.Panel2.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(477, 20)
        Me.Panel1.TabIndex = 4
        '
        'lista
        '
        Me.lista.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lista.FormattingEnabled = True
        Me.lista.ItemHeight = 25
        Me.lista.Location = New System.Drawing.Point(20, 100)
        Me.lista.Name = "lista"
        Me.lista.Size = New System.Drawing.Size(431, 279)
        Me.lista.TabIndex = 8
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.SICDIN30.My.Resources.Resources.vcsnormal_93488
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button1.Location = New System.Drawing.Point(20, 381)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(437, 49)
        Me.Button1.TabIndex = 9
        Me.Button1.Tag = "Aceptar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(51, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 25)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Folio:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(38, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 25)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Fecha:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'idlt
        '
        Me.idlt.AutoSize = True
        Me.idlt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.idlt.Location = New System.Drawing.Point(286, 23)
        Me.idlt.Name = "idlt"
        Me.idlt.Size = New System.Drawing.Size(34, 25)
        Me.idlt.TabIndex = 12
        Me.idlt.Text = "Id:"
        Me.idlt.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(26, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 25)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Usuario:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'foliol
        '
        Me.foliol.AutoSize = True
        Me.foliol.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.foliol.Location = New System.Drawing.Point(106, 23)
        Me.foliol.Name = "foliol"
        Me.foliol.Size = New System.Drawing.Size(60, 25)
        Me.foliol.TabIndex = 14
        Me.foliol.Text = "Folio:"
        Me.foliol.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'fechal
        '
        Me.fechal.AutoSize = True
        Me.fechal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fechal.Location = New System.Drawing.Point(106, 48)
        Me.fechal.Name = "fechal"
        Me.fechal.Size = New System.Drawing.Size(60, 25)
        Me.fechal.TabIndex = 15
        Me.fechal.Text = "Folio:"
        Me.fechal.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'idl
        '
        Me.idl.AutoSize = True
        Me.idl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.idl.Location = New System.Drawing.Point(320, 23)
        Me.idl.Name = "idl"
        Me.idl.Size = New System.Drawing.Size(60, 25)
        Me.idl.TabIndex = 16
        Me.idl.Text = "Folio:"
        Me.idl.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'userl
        '
        Me.userl.AutoSize = True
        Me.userl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.userl.Location = New System.Drawing.Point(106, 72)
        Me.userl.Name = "userl"
        Me.userl.Size = New System.Drawing.Size(60, 25)
        Me.userl.TabIndex = 17
        Me.userl.Text = "Folio:"
        Me.userl.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'visorpago
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(477, 450)
        Me.Controls.Add(Me.userl)
        Me.Controls.Add(Me.idl)
        Me.Controls.Add(Me.fechal)
        Me.Controls.Add(Me.foliol)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.idlt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lista)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "visorpago"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "visorpago"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lista As ListBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents idlt As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents foliol As Label
    Friend WithEvents fechal As Label
    Friend WithEvents idl As Label
    Friend WithEvents userl As Label
End Class
