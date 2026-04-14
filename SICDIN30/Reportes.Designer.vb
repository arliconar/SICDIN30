<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Reportes
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Reportpanel = New System.Windows.Forms.Panel()
        Me.dgventas = New System.Windows.Forms.DataGridView()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Ventatab = New System.Windows.Forms.TabPage()
        Me.psup = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.atp = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.detp = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tiendas = New System.Windows.Forms.ComboBox()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Reportpanel.SuspendLayout()
        CType(Me.dgventas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.Ventatab.SuspendLayout()
        Me.psup.SuspendLayout()
        Me.SuspendLayout()
        '
        'Reportpanel
        '
        Me.Reportpanel.Controls.Add(Me.dgventas)
        Me.Reportpanel.Controls.Add(Me.TabControl1)
        Me.Reportpanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Reportpanel.Location = New System.Drawing.Point(0, 0)
        Me.Reportpanel.Name = "Reportpanel"
        Me.Reportpanel.Size = New System.Drawing.Size(824, 373)
        Me.Reportpanel.TabIndex = 0
        '
        'dgventas
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgventas.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgventas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgventas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column7, Me.Column6})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgventas.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgventas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgventas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgventas.Location = New System.Drawing.Point(0, 129)
        Me.dgventas.Name = "dgventas"
        Me.dgventas.Size = New System.Drawing.Size(824, 244)
        Me.dgventas.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Ventatab)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(824, 129)
        Me.TabControl1.TabIndex = 0
        '
        'Ventatab
        '
        Me.Ventatab.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Ventatab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Ventatab.Controls.Add(Me.psup)
        Me.Ventatab.ImageKey = "(ninguno)"
        Me.Ventatab.Location = New System.Drawing.Point(4, 33)
        Me.Ventatab.Name = "Ventatab"
        Me.Ventatab.Padding = New System.Windows.Forms.Padding(3)
        Me.Ventatab.Size = New System.Drawing.Size(816, 92)
        Me.Ventatab.TabIndex = 0
        Me.Ventatab.Text = "Ventas"
        '
        'psup
        '
        Me.psup.Controls.Add(Me.Button1)
        Me.psup.Controls.Add(Me.atp)
        Me.psup.Controls.Add(Me.Label3)
        Me.psup.Controls.Add(Me.Label2)
        Me.psup.Controls.Add(Me.detp)
        Me.psup.Controls.Add(Me.Label1)
        Me.psup.Controls.Add(Me.tiendas)
        Me.psup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.psup.Location = New System.Drawing.Point(3, 3)
        Me.psup.Name = "psup"
        Me.psup.Size = New System.Drawing.Size(810, 86)
        Me.psup.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.SICDIN30.My.Resources.Resources.loupe_78956
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(679, 6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(91, 63)
        Me.Button1.TabIndex = 6
        Me.Button1.Tag = "Buscar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'atp
        '
        Me.atp.Location = New System.Drawing.Point(407, 40)
        Me.atp.Name = "atp"
        Me.atp.Size = New System.Drawing.Size(242, 29)
        Me.atp.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(369, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 24)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Al:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(358, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 24)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Del:"
        '
        'detp
        '
        Me.detp.Location = New System.Drawing.Point(407, 3)
        Me.detp.Name = "detp"
        Me.detp.Size = New System.Drawing.Size(242, 29)
        Me.detp.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Tienda:"
        '
        'tiendas
        '
        Me.tiendas.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.tiendas.FormattingEnabled = True
        Me.tiendas.Location = New System.Drawing.Point(83, 6)
        Me.tiendas.Name = "tiendas"
        Me.tiendas.Size = New System.Drawing.Size(258, 32)
        Me.tiendas.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        Me.Column1.Visible = False
        '
        'Column2
        '
        Me.Column2.HeaderText = "Column2"
        Me.Column2.Name = "Column2"
        Me.Column2.Visible = False
        '
        'Column3
        '
        Me.Column3.HeaderText = "Column3"
        Me.Column3.Name = "Column3"
        Me.Column3.Visible = False
        '
        'Column4
        '
        Me.Column4.HeaderText = "Column4"
        Me.Column4.Name = "Column4"
        Me.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column4.Visible = False
        '
        'Column5
        '
        Me.Column5.HeaderText = "Column5"
        Me.Column5.Name = "Column5"
        Me.Column5.Visible = False
        '
        'Column7
        '
        Me.Column7.HeaderText = "Column7"
        Me.Column7.Name = "Column7"
        '
        'Column6
        '
        Me.Column6.HeaderText = "Column6"
        Me.Column6.Name = "Column6"
        Me.Column6.Visible = False
        '
        'Reportes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(824, 373)
        Me.Controls.Add(Me.Reportpanel)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "Reportes"
        Me.Text = "Reportes"
        Me.Reportpanel.ResumeLayout(False)
        CType(Me.dgventas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.Ventatab.ResumeLayout(False)
        Me.psup.ResumeLayout(False)
        Me.psup.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents Ventatab As TabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents tiendas As ComboBox
    Friend WithEvents psup As Panel
    Friend WithEvents dgventas As DataGridView
    Friend WithEvents atp As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents detp As DateTimePicker
    Public WithEvents Reportpanel As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewImageColumn
End Class
