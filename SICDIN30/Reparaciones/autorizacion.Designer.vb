<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class autorizacion
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.panelnota = New System.Windows.Forms.Panel()
        Me.notas = New System.Windows.Forms.TextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.cancelar = New System.Windows.Forms.Button()
        Me.aceptar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cliente = New System.Windows.Forms.TextBox()
        Me.medioaut = New System.Windows.Forms.ComboBox()
        Me.panelnota.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(800, 25)
        Me.Panel2.TabIndex = 15
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 425)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 25)
        Me.Panel1.TabIndex = 16
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(775, 25)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(25, 400)
        Me.Panel3.TabIndex = 17
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 25)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(25, 400)
        Me.Panel4.TabIndex = 18
        '
        'panelnota
        '
        Me.panelnota.Controls.Add(Me.notas)
        Me.panelnota.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelnota.Location = New System.Drawing.Point(25, 209)
        Me.panelnota.Name = "panelnota"
        Me.panelnota.Size = New System.Drawing.Size(750, 139)
        Me.panelnota.TabIndex = 19
        '
        'notas
        '
        Me.notas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.notas.Location = New System.Drawing.Point(0, 0)
        Me.notas.Multiline = True
        Me.notas.Name = "notas"
        Me.notas.Size = New System.Drawing.Size(750, 139)
        Me.notas.TabIndex = 3
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.cancelar)
        Me.Panel5.Controls.Add(Me.aceptar)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(25, 348)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(750, 77)
        Me.Panel5.TabIndex = 20
        '
        'cancelar
        '
        Me.cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cancelar.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancelar.Location = New System.Drawing.Point(368, 0)
        Me.cancelar.Name = "cancelar"
        Me.cancelar.Size = New System.Drawing.Size(382, 77)
        Me.cancelar.TabIndex = 5
        Me.cancelar.Text = "Cancelar"
        Me.cancelar.UseVisualStyleBackColor = True
        '
        'aceptar
        '
        Me.aceptar.Dock = System.Windows.Forms.DockStyle.Left
        Me.aceptar.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aceptar.Location = New System.Drawing.Point(0, 0)
        Me.aceptar.Name = "aceptar"
        Me.aceptar.Size = New System.Drawing.Size(368, 77)
        Me.aceptar.TabIndex = 4
        Me.aceptar.Text = "Aceptar"
        Me.aceptar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(28, 173)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 33)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Nota:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 33)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Autorizó:"
        '
        'cliente
        '
        Me.cliente.Location = New System.Drawing.Point(31, 61)
        Me.cliente.Name = "cliente"
        Me.cliente.Size = New System.Drawing.Size(738, 38)
        Me.cliente.TabIndex = 1
        '
        'medioaut
        '
        Me.medioaut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.medioaut.FormattingEnabled = True
        Me.medioaut.Items.AddRange(New Object() {"En persona", "Por teléfono", "Por mensaje", "Por mail"})
        Me.medioaut.Location = New System.Drawing.Point(31, 117)
        Me.medioaut.Name = "medioaut"
        Me.medioaut.Size = New System.Drawing.Size(738, 39)
        Me.medioaut.TabIndex = 2
        '
        'autorizacion
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.medioaut)
        Me.Controls.Add(Me.cliente)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.panelnota)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "autorizacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.panelnota.ResumeLayout(False)
        Me.panelnota.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents panelnota As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents cancelar As Button
    Friend WithEvents aceptar As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Public WithEvents notas As TextBox
    Public WithEvents cliente As TextBox
    Public WithEvents medioaut As ComboBox
End Class
