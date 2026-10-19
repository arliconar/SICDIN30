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
        Me.LabelStatus = New Global.System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = Global.System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New Global.System.Drawing.Point(0, 46)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New Global.System.Drawing.Size(796, 40)
        Me.ProgressBar1.TabIndex = 0
        '
        'LabelStatus
        '
        Me.LabelStatus.BackColor = Global.System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.LabelStatus.Dock = Global.System.Windows.Forms.DockStyle.Top
        Me.LabelStatus.Font = New Global.System.Drawing.Font("Segoe UI", 11.0!, Global.System.Drawing.FontStyle.Regular, Global.System.Drawing.GraphicsUnit.Point)
        Me.LabelStatus.ForeColor = Global.System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.LabelStatus.Location = New Global.System.Drawing.Point(0, 0)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Padding = New Global.System.Windows.Forms.Padding(12, 0, 0, 0)
        Me.LabelStatus.Size = New Global.System.Drawing.Size(796, 46)
        Me.LabelStatus.TabIndex = 1
        Me.LabelStatus.Text = "Iniciando SICDIN 3.0..."
        Me.LabelStatus.TextAlign = Global.System.Drawing.ContentAlignment.MiddleLeft
        '
        'loader
        '
        Me.AutoScaleDimensions = New Global.System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = Global.System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New Global.System.Drawing.Size(796, 86)
        Me.Controls.Add(Me.LabelStatus)
        Me.Controls.Add(Me.ProgressBar1)
        Me.FormBorderStyle = Global.System.Windows.Forms.FormBorderStyle.None
        Me.Name = "loader"
        Me.StartPosition = Global.System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cargando SICDIN 3.0"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents LabelStatus As Label
End Class
