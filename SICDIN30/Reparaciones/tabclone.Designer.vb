<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class tabclone
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
        Me.tabe = New System.Windows.Forms.TabControl()
        Me.tc1 = New System.Windows.Forms.TabPage()
        Me.pventa = New System.Windows.Forms.Panel()
        Me.pbicicar = New System.Windows.Forms.Panel()
        Me.pcolor = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cancelbic = New System.Windows.Forms.CheckBox()
        Me.subsubtotal = New System.Windows.Forms.Label()
        Me.formato = New System.Windows.Forms.ComboBox()
        Me.minutos = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.hora = New System.Windows.Forms.ComboBox()
        Me.subsub = New System.Windows.Forms.Label()
        Me.Finalizada = New System.Windows.Forms.CheckBox()
        Me.foliol = New System.Windows.Forms.Label()
        Me.Entregada = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.fe = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.notas = New System.Windows.Forms.TextBox()
        Me.color = New System.Windows.Forms.TextBox()
        Me.marca = New System.Windows.Forms.TextBox()
        Me.rodada = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.addtab = New System.Windows.Forms.Button()
        Me.deltab = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.tabe.SuspendLayout()
        Me.tc1.SuspendLayout()
        Me.pbicicar.SuspendLayout()
        Me.pcolor.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabe
        '
        Me.tabe.Controls.Add(Me.tc1)
        Me.tabe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabe.Font = New System.Drawing.Font("Arial Narrow", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabe.Location = New System.Drawing.Point(0, 0)
        Me.tabe.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tabe.Name = "tabe"
        Me.tabe.SelectedIndex = 0
        Me.tabe.Size = New System.Drawing.Size(932, 400)
        Me.tabe.TabIndex = 1
        '
        'tc1
        '
        Me.tc1.Controls.Add(Me.pventa)
        Me.tc1.Controls.Add(Me.pbicicar)
        Me.tc1.Location = New System.Drawing.Point(4, 35)
        Me.tc1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tc1.Name = "tc1"
        Me.tc1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tc1.Size = New System.Drawing.Size(924, 361)
        Me.tc1.TabIndex = 0
        Me.tc1.Text = "Bicicleta 1"
        Me.tc1.UseVisualStyleBackColor = True
        '
        'pventa
        '
        Me.pventa.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pventa.Location = New System.Drawing.Point(2, 218)
        Me.pventa.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.pventa.Name = "pventa"
        Me.pventa.Size = New System.Drawing.Size(920, 141)
        Me.pventa.TabIndex = 5
        '
        'pbicicar
        '
        Me.pbicicar.Controls.Add(Me.pcolor)
        Me.pbicicar.Controls.Add(Me.cancelbic)
        Me.pbicicar.Controls.Add(Me.subsubtotal)
        Me.pbicicar.Controls.Add(Me.formato)
        Me.pbicicar.Controls.Add(Me.minutos)
        Me.pbicicar.Controls.Add(Me.Label8)
        Me.pbicicar.Controls.Add(Me.hora)
        Me.pbicicar.Controls.Add(Me.subsub)
        Me.pbicicar.Controls.Add(Me.Finalizada)
        Me.pbicicar.Controls.Add(Me.foliol)
        Me.pbicicar.Controls.Add(Me.Entregada)
        Me.pbicicar.Controls.Add(Me.Label6)
        Me.pbicicar.Controls.Add(Me.fe)
        Me.pbicicar.Controls.Add(Me.Label5)
        Me.pbicicar.Controls.Add(Me.notas)
        Me.pbicicar.Controls.Add(Me.color)
        Me.pbicicar.Controls.Add(Me.marca)
        Me.pbicicar.Controls.Add(Me.rodada)
        Me.pbicicar.Controls.Add(Me.Label4)
        Me.pbicicar.Controls.Add(Me.Label3)
        Me.pbicicar.Controls.Add(Me.Label2)
        Me.pbicicar.Controls.Add(Me.Label1)
        Me.pbicicar.Controls.Add(Me.Panel5)
        Me.pbicicar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pbicicar.Location = New System.Drawing.Point(2, 2)
        Me.pbicicar.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.pbicicar.Name = "pbicicar"
        Me.pbicicar.Size = New System.Drawing.Size(920, 216)
        Me.pbicicar.TabIndex = 4
        '
        'pcolor
        '
        Me.pcolor.BackColor = System.Drawing.Color.Transparent
        Me.pcolor.Controls.Add(Me.Label12)
        Me.pcolor.Controls.Add(Me.Panel4)
        Me.pcolor.Controls.Add(Me.Label11)
        Me.pcolor.Controls.Add(Me.Panel3)
        Me.pcolor.Controls.Add(Me.Label10)
        Me.pcolor.Controls.Add(Me.Panel1)
        Me.pcolor.Controls.Add(Me.Label9)
        Me.pcolor.Controls.Add(Me.Panel2)
        Me.pcolor.Controls.Add(Me.Label7)
        Me.pcolor.Location = New System.Drawing.Point(608, 110)
        Me.pcolor.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.pcolor.Name = "pcolor"
        Me.pcolor.Size = New System.Drawing.Size(309, 100)
        Me.pcolor.TabIndex = 26
        Me.pcolor.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(155, 72)
        Me.Label12.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(113, 26)
        Me.Label12.TabIndex = 32
        Me.Label12.Text = "Por cancelar"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Pink
        Me.Panel4.Location = New System.Drawing.Point(128, 73)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(23, 24)
        Me.Panel4.TabIndex = 29
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(155, 32)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(98, 26)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "Cancelado"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(128, 32)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(23, 24)
        Me.Panel3.TabIndex = 28
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(34, 72)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 26)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "Colocado"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Green
        Me.Panel1.Location = New System.Drawing.Point(7, 73)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(23, 24)
        Me.Panel1.TabIndex = 29
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(34, 30)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 26)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Registrado"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Yellow
        Me.Panel2.Location = New System.Drawing.Point(7, 30)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(23, 24)
        Me.Panel2.TabIndex = 27
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(2, 0)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(157, 26)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "Codigo de colores"
        '
        'cancelbic
        '
        Me.cancelbic.AutoSize = True
        Me.cancelbic.ForeColor = System.Drawing.Color.Red
        Me.cancelbic.Location = New System.Drawing.Point(431, 185)
        Me.cancelbic.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cancelbic.Name = "cancelbic"
        Me.cancelbic.Size = New System.Drawing.Size(103, 30)
        Me.cancelbic.TabIndex = 24
        Me.cancelbic.Text = "Cancelar"
        Me.cancelbic.UseVisualStyleBackColor = True
        Me.cancelbic.Visible = False
        '
        'subsubtotal
        '
        Me.subsubtotal.AutoSize = True
        Me.subsubtotal.Location = New System.Drawing.Point(568, 110)
        Me.subsubtotal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.subsubtotal.Name = "subsubtotal"
        Me.subsubtotal.Size = New System.Drawing.Size(37, 26)
        Me.subsubtotal.TabIndex = 23
        Me.subsubtotal.Text = "0.0"
        '
        'formato
        '
        Me.formato.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.formato.FormattingEnabled = True
        Me.formato.Items.AddRange(New Object() {"AM", "PM"})
        Me.formato.Location = New System.Drawing.Point(692, 74)
        Me.formato.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.formato.Name = "formato"
        Me.formato.Size = New System.Drawing.Size(79, 34)
        Me.formato.TabIndex = 22
        '
        'minutos
        '
        Me.minutos.FormattingEnabled = True
        Me.minutos.Location = New System.Drawing.Point(629, 74)
        Me.minutos.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.minutos.Name = "minutos"
        Me.minutos.Size = New System.Drawing.Size(51, 34)
        Me.minutos.TabIndex = 21
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(616, 76)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(17, 26)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = ":"
        '
        'hora
        '
        Me.hora.FormattingEnabled = True
        Me.hora.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"})
        Me.hora.Location = New System.Drawing.Point(564, 74)
        Me.hora.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.hora.Name = "hora"
        Me.hora.Size = New System.Drawing.Size(51, 34)
        Me.hora.TabIndex = 19
        '
        'subsub
        '
        Me.subsub.AutoSize = True
        Me.subsub.Location = New System.Drawing.Point(478, 110)
        Me.subsub.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.subsub.Name = "subsub"
        Me.subsub.Size = New System.Drawing.Size(93, 26)
        Me.subsub.TabIndex = 18
        Me.subsub.Text = "Subtotal:$"
        '
        'Finalizada
        '
        Me.Finalizada.AutoSize = True
        Me.Finalizada.Location = New System.Drawing.Point(431, 133)
        Me.Finalizada.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Finalizada.Name = "Finalizada"
        Me.Finalizada.Size = New System.Drawing.Size(113, 30)
        Me.Finalizada.TabIndex = 17
        Me.Finalizada.Text = "Finalizada"
        Me.Finalizada.UseVisualStyleBackColor = True
        Me.Finalizada.Visible = False
        '
        'foliol
        '
        Me.foliol.AutoSize = True
        Me.foliol.Location = New System.Drawing.Point(332, 133)
        Me.foliol.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.foliol.Name = "foliol"
        Me.foliol.Size = New System.Drawing.Size(71, 26)
        Me.foliol.TabIndex = 25
        Me.foliol.Text = "dummy"
        Me.foliol.Visible = False
        '
        'Entregada
        '
        Me.Entregada.AutoSize = True
        Me.Entregada.Location = New System.Drawing.Point(431, 159)
        Me.Entregada.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Entregada.Name = "Entregada"
        Me.Entregada.Size = New System.Drawing.Size(114, 30)
        Me.Entregada.TabIndex = 16
        Me.Entregada.Text = "Entregada"
        Me.Entregada.UseVisualStyleBackColor = True
        Me.Entregada.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(416, 76)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(147, 26)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Hora de entrega:"
        '
        'fe
        '
        Me.fe.Location = New System.Drawing.Point(570, 39)
        Me.fe.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.fe.Name = "fe"
        Me.fe.Size = New System.Drawing.Size(318, 32)
        Me.fe.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(427, 39)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(135, 26)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Día de entrega:"
        '
        'notas
        '
        Me.notas.Location = New System.Drawing.Point(109, 137)
        Me.notas.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.notas.Multiline = True
        Me.notas.Name = "notas"
        Me.notas.Size = New System.Drawing.Size(172, 75)
        Me.notas.TabIndex = 9
        '
        'color
        '
        Me.color.Location = New System.Drawing.Point(109, 95)
        Me.color.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.color.Name = "color"
        Me.color.Size = New System.Drawing.Size(172, 32)
        Me.color.TabIndex = 8
        '
        'marca
        '
        Me.marca.Location = New System.Drawing.Point(109, 59)
        Me.marca.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.marca.Name = "marca"
        Me.marca.Size = New System.Drawing.Size(172, 32)
        Me.marca.TabIndex = 7
        '
        'rodada
        '
        Me.rodada.Location = New System.Drawing.Point(109, 24)
        Me.rodada.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.rodada.Name = "rodada"
        Me.rodada.Size = New System.Drawing.Size(64, 32)
        Me.rodada.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(40, 137)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 26)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Notas:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(43, 99)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 26)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Color:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(38, 63)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 26)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Marca:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 24)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 26)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Rodada:"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.addtab)
        Me.Panel5.Controls.Add(Me.deltab)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(920, 34)
        Me.Panel5.TabIndex = 15
        '
        'addtab
        '
        Me.addtab.AutoSize = True
        Me.addtab.BackgroundImage = Global.SICDIN30.My.Resources.Resources.add_icon_icons_com_74429
        Me.addtab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.addtab.Dock = System.Windows.Forms.DockStyle.Right
        Me.addtab.Location = New System.Drawing.Point(741, 0)
        Me.addtab.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.addtab.Name = "addtab"
        Me.addtab.Size = New System.Drawing.Size(100, 34)
        Me.addtab.TabIndex = 0
        Me.addtab.Tag = "Agregar"
        Me.addtab.UseVisualStyleBackColor = True
        '
        'deltab
        '
        Me.deltab.AutoSize = True
        Me.deltab.BackgroundImage = Global.SICDIN30.My.Resources.Resources._1486504830_delete_dustbin_empty_recycle_recycling_remove_trash_81361
        Me.deltab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.deltab.Dock = System.Windows.Forms.DockStyle.Right
        Me.deltab.Location = New System.Drawing.Point(841, 0)
        Me.deltab.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.deltab.Name = "deltab"
        Me.deltab.Size = New System.Drawing.Size(79, 34)
        Me.deltab.TabIndex = 1
        Me.deltab.Tag = "Eliminar"
        Me.deltab.UseVisualStyleBackColor = True
        '
        'tabclone
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(932, 400)
        Me.Controls.Add(Me.tabe)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "tabclone"
        Me.Text = "tabclone"
        Me.tabe.ResumeLayout(False)
        Me.tc1.ResumeLayout(False)
        Me.pbicicar.ResumeLayout(False)
        Me.pbicicar.PerformLayout()
        Me.pcolor.ResumeLayout(False)
        Me.pcolor.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents tabe As TabControl
    Public WithEvents tc1 As TabPage
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel5 As Panel
    Public WithEvents pventa As Panel
    Public WithEvents addtab As Button
    Public WithEvents deltab As Button
    Public WithEvents notas As TextBox
    Public WithEvents color As TextBox
    Public WithEvents marca As TextBox
    Public WithEvents rodada As TextBox
    Public WithEvents Entregada As CheckBox
    Public WithEvents Finalizada As CheckBox
    Friend WithEvents Label8 As Label
    Public WithEvents minutos As ComboBox
    Public WithEvents hora As ComboBox
    Public WithEvents formato As ComboBox
    Public WithEvents subsub As Label
    Public WithEvents subsubtotal As Label
    Public WithEvents cancelbic As CheckBox
    Public WithEvents pbicicar As Panel
    Public WithEvents foliol As Label
    Public WithEvents fe As DateTimePicker
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label9 As Label
    Public WithEvents pcolor As Panel
End Class
