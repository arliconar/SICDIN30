<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class loader
    Inherits Global.System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <Global.System.Diagnostics.DebuggerNonUserCode()> _
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
    Private components As Global.System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <Global.System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ProgressBar1 = New Global.System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = Global.System.Windows.Forms.DockStyle.Fill
        Me.ProgressBar1.Location = New Global.System.Drawing.Point(0, 0)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New Global.System.Drawing.Size(796, 86)
        Me.ProgressBar1.TabIndex = 0
        '
        'loader
        '
        Me.AutoScaleDimensions = New Global.System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = Global.System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New Global.System.Drawing.Size(796, 86)
        Me.Controls.Add(Me.ProgressBar1)
        Me.FormBorderStyle = Global.System.Windows.Forms.FormBorderStyle.None
        Me.Name = "loader"
        Me.StartPosition = Global.System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ProgressBar1 As ProgressBar
End Class
