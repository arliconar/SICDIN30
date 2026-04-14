<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class loadscreen
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
        Me.components = New System.ComponentModel.Container()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.loadtext = New System.Windows.Forms.Label()
        Me.pbar = New System.Windows.Forms.ProgressBar()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'loadtext
        '
        Me.loadtext.AutoSize = True
        Me.loadtext.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.loadtext.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.loadtext.Location = New System.Drawing.Point(0, 59)
        Me.loadtext.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.loadtext.Name = "loadtext"
        Me.loadtext.Size = New System.Drawing.Size(96, 26)
        Me.loadtext.TabIndex = 0
        Me.loadtext.Text = "Cargando:"
        '
        'pbar
        '
        Me.pbar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbar.Location = New System.Drawing.Point(0, 0)
        Me.pbar.Margin = New System.Windows.Forms.Padding(2)
        Me.pbar.Name = "pbar"
        Me.pbar.Size = New System.Drawing.Size(753, 59)
        Me.pbar.TabIndex = 1
        '
        'loadscreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(753, 85)
        Me.Controls.Add(Me.pbar)
        Me.Controls.Add(Me.loadtext)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "loadscreen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "loadscreen"
        Me.TopMost = True
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents loadtext As Label
    Friend WithEvents pbar As ProgressBar
End Class
