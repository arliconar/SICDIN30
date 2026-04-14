<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class modver
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
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Aceptar = New System.Windows.Forms.Button()
        Me.Clientever = New System.Windows.Forms.TextBox()
        Me.Notaver = New System.Windows.Forms.TextBox()
        Me.des = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(800, 25)
        Me.Panel3.TabIndex = 19
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 425)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 25)
        Me.Panel1.TabIndex = 20
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 25)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(25, 400)
        Me.Panel2.TabIndex = 21
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel4.Location = New System.Drawing.Point(775, 25)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(25, 400)
        Me.Panel4.TabIndex = 22
        '
        'Aceptar
        '
        Me.Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Aceptar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Aceptar.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Aceptar.Location = New System.Drawing.Point(25, 366)
        Me.Aceptar.Name = "Aceptar"
        Me.Aceptar.Size = New System.Drawing.Size(750, 59)
        Me.Aceptar.TabIndex = 23
        Me.Aceptar.Text = "Aceptar"
        Me.Aceptar.UseVisualStyleBackColor = True
        '
        'Clientever
        '
        Me.Clientever.Location = New System.Drawing.Point(24, 61)
        Me.Clientever.Name = "Clientever"
        Me.Clientever.Size = New System.Drawing.Size(751, 38)
        Me.Clientever.TabIndex = 24
        '
        'Notaver
        '
        Me.Notaver.Location = New System.Drawing.Point(25, 135)
        Me.Notaver.Multiline = True
        Me.Notaver.Name = "Notaver"
        Me.Notaver.Size = New System.Drawing.Size(750, 61)
        Me.Notaver.TabIndex = 25
        '
        'des
        '
        Me.des.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.des.Location = New System.Drawing.Point(25, 227)
        Me.des.Multiline = True
        Me.des.Name = "des"
        Me.des.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.des.Size = New System.Drawing.Size(750, 139)
        Me.des.TabIndex = 26
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 33)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Autorizó:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 33)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Nota:"
        '
        'modver
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.des)
        Me.Controls.Add(Me.Notaver)
        Me.Controls.Add(Me.Clientever)
        Me.Controls.Add(Me.Aceptar)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "modver"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "modver"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Aceptar As Button
    Friend WithEvents Clientever As TextBox
    Friend WithEvents Notaver As TextBox
    Friend WithEvents des As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
