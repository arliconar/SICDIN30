Imports System

Imports System.IO
Imports System.Threading



Public Class main
    Dim copyy As copiador

    Dim cat As Form
    Dim conexion As tcpconnexion


    'Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
    '    ' Cree una nueva instancia del formulario secundario.
    '    Dim ChildForm As New Global.System.Windows.Forms.Form
    '    ' Conviértalo en un elemento secundario de este formulario MDI antes de mostrarlo.
    '    ChildForm.MdiParent = Me

    '    m_ChildFormNumber += 1
    '    ChildForm.Text = "Ventana " & m_ChildFormNumber

    '    ChildForm.Show()
    'End Sub

    'Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim OpenFileDialog As New OpenFileDialog
    '    OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    '    OpenFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*"
    '    If (OpenFileDialog.ShowDialog(Me) = Global.System.Windows.Forms.DialogResult.OK) Then
    '        Dim FileName As String = CType(OpenFileDialog.FileName, System.String)
    '        ' TODO: agregue código aquí para abrir el archivo.
    '    End If
    'End Sub

    'Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim SaveFileDialog As New SaveFileDialog
    '    SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    '    SaveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*"

    '    If (SaveFileDialog.ShowDialog(Me) = Global.System.Windows.Forms.DialogResult.OK) Then
    '        Dim FileName As String = CType(SaveFileDialog.FileName, System.String)
    '        ' TODO: agregue código aquí para guardar el contenido actual del formulario en un archivo.
    '    End If
    'End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Utilice My.Computer.Clipboard para insertar el texto o las imágenes seleccionadas en el Portapapeles
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Utilice My.Computer.Clipboard para insertar el texto o las imágenes seleccionadas en el Portapapeles
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Utilice My.Computer.Clipboard.GetText() o My.Computer.Clipboard.GetData para recuperar la información del Portapapeles.
    End Sub

    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ''     Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ''     Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ''     Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ''      Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Cierre todos los formularios secundarios del principal.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer
    Public Sub existenciasfront()
        If ppos.Controls.IndexOf(existencias.panelexis) Then
            ppos.Controls.Add(existencias.panelexis)

        End If

        ppos.Controls.Item("panelexis").BringToFront()
        existencias.cargarfaltantes()
        existencias.cargarperdidas()

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles invb.Click
        existenciasfront()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles catbut.Click
        ppos.Controls.Item("panelcat").BringToFront()




    End Sub

    Private Sub FileSystemWatcher1_Changed(sender As Object, e As IO.FileSystemEventArgs)
        'lectorbasecloud()



    End Sub

    Private Sub cloudwatcher_Created(sender As Object, e As FileSystemEventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)





    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Select Case msg.WParam.ToInt32()
            Case CInt(Keys.Enter)
                SendKeys.SendWait("{TAB}")
                Return True
            Case CInt(Keys.F1)
                ppos.Controls.Item("panelcat").BringToFront()

                Dim ob As Object = ppos.Controls.Item("panelcat")
                catalogo.sbox.Focus()
                catalogo.sbox.SelectAll()

                Return True
            Case CInt(Keys.F2)
                newventawin()
                Return True
            Case CInt(Keys.F3)
                If tienda.tipo <> "Tienda solo" Then
                    newrepwin()
                End If

                Return True
            Case CInt(Keys.F4)
                libretaloader()
                Return True
            Case CInt(Keys.F5)
                opennotas()
                Return True

            Case CInt(Keys.F12)
                maintrans()
                Dim resultado As DialogResult = DialogResult.No
                Do While resultado <> DialogResult.OK
                    session.firttime = False
                    resultado = session.ShowDialog
                    session.Close()
                Loop
                mainnormal()

                Return True



            Case Else
                Return False


        End Select




    End Function


    Private Sub ppos_Paint_1(sender As Object, e As PaintEventArgs) Handles ppos.Paint

    End Sub


    Private Sub openclientes() Handles clntbutton.Click
        If Not clientes.panelclientes.Visible Then
            ppos.Controls.Add(clientes.panelclientes)
        End If
        clientes.loader()
        If user.admin Then
            clientes.delclnt.Visible = True
            clientes.modclnt.Visible = True
        Else
            clientes.delclnt.Visible = False
            clientes.modclnt.Visible = False
        End If
        ppos.Controls.Item("panelclientes").BringToFront()
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs)




    End Sub

    Public Sub opennotas() Handles notasbutton.Click
        If Not opennota.mopen Then
            ppos.Controls.Add(opennota.pnotas)
            opennota.loader()
            opennota.acoplarresize()
        End If
        ppos.Controls.Item("pnotas").BringToFront()


    End Sub
    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles sqlb.Click

        maintrans()
        sql.ShowDialog()
        mainnormal()



    End Sub

    Private Sub Button7_MouseEnter(sender As Object, e As EventArgs) Handles notasbutton.MouseEnter, Reports.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub Button7_MouseLeave(sender As Object, e As EventArgs) Handles notasbutton.MouseLeave, Reports.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub Button5_MouseEnter(sender As Object, e As EventArgs) Handles clntbutton.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub Button5_MouseLeave(sender As Object, e As EventArgs) Handles clntbutton.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub Button2_MouseEnter(sender As Object, e As EventArgs) Handles catbut.MouseEnter
        shownamebutton(sender)

    End Sub

    Private Sub Button2_MouseLeave(sender As Object, e As EventArgs) Handles catbut.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub Button3_MouseEnter(sender As Object, e As EventArgs) Handles sqlb.MouseEnter
        sqlb.Text = "SQL"
    End Sub

    Private Sub Button3_MouseLeave(sender As Object, e As EventArgs) Handles sqlb.MouseLeave
        sqlb.Text = ""
    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles invb.MouseEnter
        invb.Text = "Inventario"
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles invb.MouseLeave
        invb.Text = ""

    End Sub

    Private Sub panelbutton_Paint(sender As Object, e As PaintEventArgs) Handles panelbutton.Paint

    End Sub


    Public Sub libretaloader() Handles libretabutton.Click
        If Not libreta.panellibreta.Visible Then
            ppos.Controls.Add(libreta.panellibreta)
        End If
        libreta.loader()

        ppos.Controls.Item("panellibreta").BringToFront()
    End Sub
    Private Sub libretabutton_MouseEnter(sender As Object, e As EventArgs) Handles libretabutton.MouseEnter, Venta.MouseEnter, Reparacion.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub libretabutton_MouseLeave(sender As Object, e As EventArgs) Handles libretabutton.MouseLeave, Reparacion.MouseLeave, Venta.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub loadusuarios() Handles usuarios.Click

        If Not Usuariosform.userpanel.Visible Then
            ppos.Controls.Add(Usuariosform.userpanel)
            Usuariosform.loadtrabajadores()
            Usuariosform.loadusers()

        End If
        If tienda.tipo = "Tienda solo" Then
            Usuariosform.onlytienda()

        End If

        ppos.Controls.Item("userpanel").BringToFront()

    End Sub

    Private Sub usuarios_MouseEnter(sender As Object, e As EventArgs) Handles usuarios.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub usuarios_MouseLeave(sender As Object, e As EventArgs) Handles usuarios.MouseLeave
        hidenamebutton(sender)
    End Sub

    Public Sub onlytienda()
        If tienda.tipo = "Tienda solo" Then
            Reparacion.Hide()

        End If


    End Sub
    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        maintrans()

        Select Case closesic.ShowDialog
            Case DialogResult.Yes
                e.Cancel = True
                If Not blockexit Then
                    Dim hilo As Thread = New Thread(New ThreadStart(AddressOf backinup))
                    hilo.Start()

                Else
                    wait.loader()


                End If
            Case DialogResult.Cancel
                e.Cancel = True
            Case DialogResult.No
                e.Cancel = True
                Dim resultado As DialogResult = DialogResult.No
                Do While resultado <> DialogResult.OK
                    session.firttime = False
                    resultado = session.ShowDialog
                    session.Close()
                Loop

        End Select
        mainnormal()

    End Sub

    Private Sub Setup_Click(sender As Object, e As EventArgs) Handles Setupb.Click
        setup.loader()


    End Sub
    Public Sub loadadmin()
        mainnormal()

        If user.admin Then
            usuarios.Show()
            sqlb.Show()
            invb.Show()
            catalogo.AltaArtículoToolStripMenuItem.Visible = True
            catalogo.modicarcontext.Visible = True
            catalogo.EliminarArtículoToolStripMenuItem.Visible = True
            catalogo.ToolStripSeparator1.Visible = True

            Reports.Show()


        Else
            usuarios.Hide()
            sqlb.Hide()
            invb.Hide()
            catalogo.AltaArtículoToolStripMenuItem.Visible = False
            catalogo.modicarcontext.Visible = False
            catalogo.EliminarArtículoToolStripMenuItem.Visible = False
            catalogo.ToolStripSeparator1.Visible = False
            Reports.Hide()
        End If
    End Sub
    Public Sub mainloadfirts()

        Me.Icon = icono
        loadadmin()
        Me.Show()
        Me.Text = "SICDIN 3.0: " & user.nombre & " en " & tienda.nombre
        lector.Close()

        Me.ppos.Controls.Add(catalogo.panelcat)
        catalogo.reloadcat()

        Me.ppos.Controls.Item("panelcat").BringToFront()
        ' Dim hilo2 As Thread = New Thread(New ThreadStart(AddressOf lectorbasecloud))
        ' hilo2.Start()
        notifier = New notificacion
        catbut.PerformClick()
        onlytienda()
    End Sub
    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles convbutton.Click
        Calculadora.ShowDialog()

    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs)

    End Sub



    Private Sub Cloudupdated_Tick(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)

    End Sub

    Private Sub Venta_Click(sender As Object, e As EventArgs) Handles Venta.Click
        newventawin()

    End Sub

    Private Sub Reparacion_Click(sender As Object, e As EventArgs) Handles Reparacion.Click
        newrepwin()

    End Sub

    Private Sub UPDATECLOUD_Tick(sender As Object, e As EventArgs) Handles UPDATECLOUD.Tick
        'Dim hilo As Thread = New Thread(New ThreadStart(AddressOf duplicadorbase))
        ' hilo.Start()

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles whatsbut.Click
        maintrans()
        Enviarwhats.Close()
        Enviarwhats.numero.Focus()

        If Enviarwhats.ShowDialog = DialogResult.OK Then
            sendtowhats(Enviarwhats.numero.Text)
        End If
        mainnormal()

    End Sub

    Private Sub CHECKCLOUD_Tick(sender As Object, e As EventArgs) Handles CHECKCLOUD.Tick
        Dim hilo2 As Thread = New Thread(New ThreadStart(AddressOf lectorbasecloud))
        hilo2.Start()
    End Sub

    Private Sub updatet_ButtonClick(sender As Object, e As EventArgs) Handles updatet.ButtonClick
        'Dim hilo As Thread = New Thread(New ThreadStart(AddressOf duplicadorbase))
        'hilo.Start()
        Dim hilo2 As Thread = New Thread(New ThreadStart(AddressOf lectorbasecloud))
        hilo2.Start()
    End Sub

    Private Sub ToolStripStatusLabel1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub loadusuarios(sender As Object, e As EventArgs) Handles usuarios.Click

    End Sub

    Private Sub openclientes(sender As Object, e As EventArgs) Handles clntbutton.Click

    End Sub

    Private Sub opennotas(sender As Object, e As EventArgs) Handles notasbutton.Click


    End Sub

    Private Sub notify_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub libretaloader(sender As Object, e As EventArgs) Handles libretabutton.Click

    End Sub

    Private Sub Reports_Click(sender As Object, e As EventArgs) Handles Reports.Click
        If ppos.Controls.IndexOf(Reportes.Reportpanel) Then
            Reportes.loader()

            ppos.Controls.Add(Reportes.Reportpanel)

        End If

        ppos.Controls.Item("Reportpanel").BringToFront()

    End Sub

    Private Sub estadomain_Click(sender As Object, e As EventArgs) Handles estadomain.Click

    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles Button1.Click


        helpcontext.Show(MousePosition)

    End Sub

    Private Sub BuscarNotaDeVentaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuscarNotaDeVentaToolStripMenuItem.Click
        opennotas()
        opennota.tabvg.SelectedIndex = 3
        opennota.tabbuscar.SelectedIndex = 0
    End Sub

    Private Sub BuscarNotaDeReparaciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuscarNotaDeReparaciónToolStripMenuItem.Click
        opennotas()
        opennota.tabvg.SelectedIndex = 3
        opennota.tabbuscar.SelectedIndex = 1
    End Sub

    Private Sub BuscarApartadosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuscarApartadosToolStripMenuItem.Click
        opennotas()
        opennota.tabvg.SelectedIndex = 2
    End Sub

    Private Sub VentasGuardadasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VentasGuardadasToolStripMenuItem.Click
        opennotas()
        opennota.tabvg.SelectedIndex = 1
    End Sub
    Private Sub itemcopuier(sender As Object, e As EventArgs)
        copyy.copy(sender.text)

    End Sub
    Private Sub copy_Click(sender As Object, e As EventArgs) Handles copy.Click
        copier.Items.Clear()
        copyy = New copiador
        For i = 0 To copyy.i - 1
            copier.Items.Add(copyy.descripcion.Item(i))
            AddHandler copier.Items(i).Click, AddressOf itemcopuier
        Next
        copier.Show(MousePosition)

    End Sub
End Class
