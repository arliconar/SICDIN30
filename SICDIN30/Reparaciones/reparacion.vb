Public Class reparacion
    Public idventa As String

    Public fechacr As Date = Now
    Public nameautorizo As String
    Public notaatorizo As String
    Public stmod As String
    Public modificar As Boolean = False
    Public tpagado As Decimal = 0
    Public folio As String = ""
    Public repopen As Boolean = False
    Public tresta As Decimal = 0
    Dim clntfound As Boolean = False
    Dim mayoreo As Boolean = False
    Public adeudo As Decimal = 0
    Public subtotalrep As Double = 0
    Public totalrep As Double = 0
    Dim clntid As Integer = 0

    Public rep As repbic = New repbic
    Dim especial As Boolean = False
    Dim horaes As Date

    Public Sub loader() ''carga una reparacion nueva
        rep = New repbic
        main.ppos.Controls.Add(Me.panelrep)
        horaes = Now.AddHours(1)
        addnewpage()
        loadclnt()
        main.ppos.Controls.Item("panelrep").BringToFront()
        folio = ""
        panelrep.BringToFront()

    End Sub
    Public Sub modertextbici(sender As Object, e As EventArgs)
        ''Pinta los cuadros de texto cuando se mofidica algo
        If modificar Then
            sender.BackColor = Drawing.Color.Blue
            sender.ForeColor = Drawing.Color.White
        End If
    End Sub
    Public Sub loadervent(folio As String, Optional id As Integer = 0) ''carga una venta
        'Carga una venta
        rep = New repbic
        rep.loadrep(folio)
        Dim taber As TabPage = tabe.TabPages.Item(0)
        tabe.TabPages.Clear()
        tabe.TabPages.Add(taber)

        Dim orden As OleDb.OleDbCommand
        Dim lector As OleDb.OleDbDataReader
        Dim orden3 As OleDb.OleDbCommand
        Dim lector3 As OleDb.OleDbDataReader
        loadscreen.loader(3, 1)
        loadscreen.newtext("Cliente")
        loadscreen.stepp()
        setfolio(folio)
        Dim st As String = "SELECT Cliente,Direccion,RFC,telcasa,telcel,mail,Fcreacion FROM nota WHERE Folio='" & folio & "'"
        orden = New OleDb.OleDbCommand(st, conbicrep)
        Me.folio = folio
        lector = orden.ExecuteReader ''se carga la nota
        While lector.Read
            fechacr = lector("Fcreacion")
            comboclnt.Text = lector("Cliente")
            ''clnt.loadclntst(comboclnt.Text)
            direccion.Text = delvoiddb(lector("Direccion"))
            rfc.Text = delvoiddb(lector("RFC"))
            telcel.Text = delvoiddb(lector("telcel"))
            telcasa.Text = delvoiddb(lector("telcasa"))
            mail.Text = delvoiddb(lector("mail"))
        End While
        lector.Close()
        loadscreen.newtext("Bicicletas")
        loadscreen.stepp()
        searclnt()
        st = "SELECT Id,Marca,Color,Rodada,Nota,Fhentrega,bFinalizada,bCancelada,bentregada,Entregada,Finalizada,horae,Realizo,Entrego FROM biciconsulta WHERE Folio='" & folio & "' ORDER BY Id"
        orden = New OleDb.OleDbCommand(st, conbicrep)
        lector = orden.ExecuteReader
        Dim i As Integer = 1
        repopen = True
        While lector.Read ''se cargan las bicis
            Dim sthora As Date = lector("horae")
            horaes = sthora
            addnewpage()
            With tabe.TabPages.Item(i).Controls("pbicicar").Controls
                .Item("rodada").Text = lector("Rodada")
                .Item("rodada").Enabled = False
                .Item("marca").Text = lector("Marca")
                .Item("marca").Enabled = False
                .Item("color").Text = lector("Color")
                .Item("color").Enabled = False
                .Item("Notas").Text = lector("Nota")
                .Item("Notas").Enabled = False
                .Item("Panel5").Controls.Item("deltab").Visible = False
                .Item("Panel5").Controls.Item("addtab").Visible = False
                Dim ch As CheckBox = .Item("Finalizada")
                ch.Checked = lector("bFinalizada")
                If ch.Checked Then
                    ch.Text = "Finalizada por " & lector("Realizo") & " el " & lector("Finalizada")
                End If
                ch.Enabled = Not (lector("bEntregada") Or lector("bCancelada"))
                ch = .Item("Entregada")
                ch.Checked = lector("bEntregada")
                If ch.Checked Then
                    ch.Text = "Entregada por " & lector("Entrego") & " el " & lector("Entregada")
                End If
                ch.Enabled = Not lector("bCancelada")
                ch = .Item("cancelbic")
                ch.Checked = lector("bCancelada")
                If ch.Checked Then
                    tabe.TabPages.Item(i).Enabled = False
                End If
                Dim d As DateTimePicker = .Item("fe")
                Dim fechahora As Date = lector("Fhentrega")
                d.Value = fechahora.ToShortDateString

                .Item("hora").Text = sthora.ToString("hh")
                .Item("minutos").Text = sthora.ToString("mm")

                .Item("formato").Text = sthora.ToString("tt")
            End With
            Dim st2 As String = "SELECT cantidad,clave,tp,cancel,colocado,Paquete FROM art WHERE Folio='" & folio & "' AND Id=" & lector("id")
            orden3 = New OleDb.OleDbCommand(st2, conbicrep) ''se cargan los articulos

            lector3 = orden3.ExecuteReader
            Dim rindex As Integer = 0
            Dim dg1 As DataGridView = tabe.TabPages.Item(i).Controls("pventa").Controls.Item("dg1")
            While lector3.Read
                dg1.Rows.Add()
                dg1.ReadOnly = True
                dg1("can", rindex).Value = lector3("cantidad")
                If Microsoft.VisualBasic.Left(lector3("Clave"), 6) = "TRNORE" Or Microsoft.VisualBasic.Left(lector3("Clave"), 6) = "ARNORE" Then ''para articulos y trabajos no registrados
                    dg1("des", rindex).Value = lector3("Clave")
                    dg1("des", rindex).ToolTipText = Microsoft.VisualBasic.Left(lector3("Clave"), 6)
                    dg1("Clave", rindex).Value = Microsoft.VisualBasic.Left(lector3("Clave"), 6)
                    Dim a As DataGridViewComboBoxCell = dg1("tpv", rindex)

                    a.Value = "" 'Inicializacion obligatoria para evitar mensajes de error cuando el combo se borra. Probable error de .NET
                    a.Items.Clear()
                    a.Items.Add(lector3("Tp"))
                    a.Value = lector3("Tp")
                    dg1("pq", rindex).Value = lector3("Paquete")
                Else
                    dg1("des", rindex).Value = lector3("clave")
                    searartdg(dg1, rindex, mayoreo, especial, False, True)
                    dg1("clave", rindex).Value = lector3("clave")

                End If
                multiimp(dg1, rindex)
                dg1("cancel", rindex).Value = lector3("cancel")
                If lector3("cancel") = True Then

                    dg1.Rows.Item(rindex).DefaultCellStyle.BackColor = Drawing.Color.Red
                    dg1.Rows.Item(rindex).DefaultCellStyle.Font = ftachado
                Else
                    If lector3("colocado") = True Then
                        dg1.Rows.Item(rindex).DefaultCellStyle.BackColor = Drawing.Color.Green
                    Else
                        dg1.Rows.Item(rindex).DefaultCellStyle.BackColor = Drawing.Color.Yellow
                    End If
                End If
                rindex += 1
            End While
            dg1.EditMode = DataGridViewEditMode.EditProgrammatically

            lector3.Close()
            Dim subtotal As Double = dgsubtotal(dg1)
            tabe.TabPages.Item(i).Tag.subtotal = subtotal
            i += 1
        End While
        lector.Close()
        loadscreen.newtext("Pagos")
        loadscreen.stepp()
        loadpagos()

        If id > 0 Then
            tabe.SelectedIndex = id
            tabe.SelectedTab.Focus()
        End If
        modificarb.Show()
        showmodes()
        comboclnt.Enabled = False
        showfinencan()
        copydg()
        loadmod(folio)
        loadscreen.unloadme()

        printnota.Visible = True

    End Sub
    Private Sub loadpagos()
        Dim st As String = "SELECT Id,Fecha,Cantidad, Efectivo, Transferencia, Tarjeta, Apartado, Usuario FROM pagos WHERE Folio='" & folio & "'"
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, conbicrep)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        Dim i As Integer = 0
        dgpagos.Rows.Clear()
        While lector.Read
            dgpagos.Rows.Add()
            dgpagos("Idp", i).Value = lector("Id")
            dgpagos("Fechap", i).Value = lector("Fecha")
            dgpagos("Canp", i).Value = Math.Round(lector("Efectivo") + lector("Tarjeta") + lector("Transferencia") + lector("Apartado"), 2)
            dgpagos("usup", i).Value = lector("Usuario")
            i += 1
        End While
        subtotalesum()
    End Sub
    Private Sub subtotalesum()
        ''sumatotodos los subtotales
        subtotalrep = 0
        For Each a As TabPage In tabe.Controls
            If a.Text <> "Cliente" Then
                Dim ob As Object = a
                subtotalrep += Math.Round(a.Tag.subtotal, 2)
                a.Controls.Item("pbicicar").Controls.Item("subsubtotal").Text = a.Tag.subtotal
                copydg()
            End If
        Next
        subtotallabel.Text = "$" & subtotalrep
        totalrep = subtotalrep
        sumapagos()
    End Sub
    Public Sub allowmodificar() Handles modificarb.Click
        ''Permite moficar la nota
        If Not modificar Then
            dialogs.Close()

            If dialogs.pregunta("¿Desea modificar esta nota?") = DialogResult.Yes Then
                If Not autorizacion.loader(comboclnt.Text) Then
                    Exit Sub
                End If
                If autorizacion.cliente.Text = "" Then
                    nameautorizo = user.idnameusu
                Else
                    nameautorizo = autorizacion.cliente.Text & " " & autorizacion.medioaut.Text
                End If
                notaatorizo = autorizacion.notas.Text
                modificar = True
                showmodes()
            End If
        End If


    End Sub
    Public Sub showmodes()
        ''Alterna controles entre modificar o no

        If modificar Then
            okv.Show()
            okvc.Show()
            modificarb.Hide()
            Dim i As Integer

            For i = 1 To tabe.TabCount - 1
                Dim dg1 As DataGridView = tabe.TabPages.Item(i).Controls("pventa").Controls.Item("dg1")
                dg1.ReadOnly = True

                With tabe.TabPages.Item(i).Controls("pbicicar").Controls
                    .Item("pcolor").Visible = True
                    .Item("notas").Enabled = True
                    .Item("hora").Enabled = True
                    .Item("minutos").Enabled = True
                    .Item("formato").Enabled = True
                    .Item("fe").Enabled = True
                End With
                dg1.ReadOnly = False
            Next
        Else
            okv.Hide()
            okvc.Hide()
            modificarb.Show()


            Dim i As Integer
            For i = 1 To tabe.TabCount - 1
                Dim dg1 As DataGridView = tabe.TabPages.Item(i).Controls("pventa").Controls.Item("dg1")
                With tabe.TabPages.Item(i).Controls("pbicicar").Controls
                    .Item("Panel5").Controls.Item("addtab").Visible = False
                    .Item("Panel5").Controls.Item("deltab").Visible = False
                    .Item("pcolor").Visible = False
                    .Item("rodada").Enabled = False
                    .Item("marca").Enabled = False
                    .Item("color").Enabled = False
                    .Item("notas").Enabled = False
                    .Item("hora").Enabled = False
                    .Item("minutos").Enabled = False
                    .Item("formato").Enabled = False
                    .Item("fe").Enabled = False
                    dg1.ReadOnly = True
                End With
            Next
            comboclnt.Enabled = False
        End If
        rfc.Enabled = modificar
        direccion.Enabled = modificar
        telcel.Enabled = modificar
        telcasa.Enabled = modificar
        mail.Enabled = modificar

    End Sub

    Public Sub addnewpage()
        ''clona la pagina de tabclone

        Dim nfdummy As Object = New tabclone
        nfdummy.repopen = repopen

        Dim nlabelfolio As Label = nfdummy.foliol
        nlabelfolio.Text = folio

        Dim ndummybutton As Button = nfdummy.addtab
        Dim ndummybutton2 As Button = nfdummy.deltab

        AddHandler ndummybutton.Click, AddressOf addnewpage
        AddHandler ndummybutton2.Click, AddressOf pagerecont

        Dim ndummytextb As TextBox = nfdummy.rodada
        AddHandler ndummytextb.LostFocus, AddressOf textlostf
        ndummytextb = nfdummy.marca
        AddHandler ndummytextb.LostFocus, AddressOf textlostf
        ndummytextb = nfdummy.color
        AddHandler ndummytextb.LostFocus, AddressOf textlostf

        Dim ndummynotas As TextBox = nfdummy.notas
        AddHandler ndummynotas.TextChanged, AddressOf modertextbici


        Dim ndummyhora As ComboBox = nfdummy.hora
        Dim ndummyminutos As ComboBox = nfdummy.minutos
        Dim ndummyformato As ComboBox = nfdummy.formato
        Dim i As Integer

        For i = 0 To 59
            ndummyminutos.Items.Add(Format(i, "0#"))
        Next
        Select Case Format(horaes, "tt").ToString
            Case "a. m."
                ndummyformato.SelectedIndex = 0
            Case "p. m."
                ndummyformato.SelectedIndex = 1
        End Select
        ndummyhora.Text = Format(Val(horaes.Hour), "0#")


        If Val(ndummyhora.Text) > 13 Then
            ndummyhora.Text = (Val(ndummyhora.Text) - 12).ToString
            ndummyformato.Text = "PM"
        ElseIf Val(ndummyhora.Text) = 0 Then
            ndummyhora.Text = "12"
        End If
        ndummyminutos.Text = Format(Val(horaes.Minute), "0#")
        AddHandler ndummyhora.KeyPress, AddressOf textnumonly
        AddHandler ndummyminutos.KeyPress, AddressOf textnumonly
        AddHandler ndummyhora.TextChanged, AddressOf modertextbici
        AddHandler ndummyminutos.TextChanged, AddressOf modertextbici


        Dim ndummycheck As CheckBox = nfdummy.Finalizada
        AddHandler ndummycheck.Click, AddressOf textlostf
        ndummycheck = nfdummy.Entregada
        AddHandler ndummycheck.Click, AddressOf textlostf

        Dim nfdummy2 As Object = New formvent
        nfdummy.tc1.tag = nfdummy2
        Dim ndummydg1 As DataGridView = nfdummy2.dg1
        AddHandler ndummydg1.CellValidated, AddressOf subtotalesum
        nfdummy.pventa.Controls.Add(nfdummy2.dg1)
        nfdummy2.dg1.Rows.Add()

        tabe.TabPages.Add(nfdummy.tc1)
        tabe.TabPages.Item(tabe.TabPages.Count - 1).ImageIndex = 1
        ' nfdummy.Dispose()
        If tabe.TabPages.Count > 2 Then
            tabe.SelectedTab = nfdummy.tc1
        End If

        pagerecont()
        actmay()
    End Sub


    Public Sub showfinencan()
        'Muestra los controles de finalizar, entregar y cancelar
        Dim i As Integer
        For i = 1 To tabe.TabPages.Count - 1
            With tabe.TabPages.Item(i).Controls("pbicicar").Controls
                .Item("Finalizada").Show()
                .Item("Entregada").Show()
                .Item("cancelbic").Show()
            End With
        Next


    End Sub
    Public Function saverep() As Boolean
        ''Guarda la reparación
        saverep = False

        If comboclnt.Text = "1:Mostrador" Then
            dialogs.Close()
            dialogs.advertencia("El cliente no puede ser Mostrador" & Chr(13) & "Agregue un cliente diferente")
            Exit Function
        ElseIf comboclnt.Text = "" Or comboclnt.Text = " " Then
            dialogs.Close()
            dialogs.advertencia("Debe colocar al menos el nombre del cliente")
        Else
            If (telcasa.Text = "" And telcel.Text = "" And mail.Text = "") And Not modificar Then
                dialogs.Close()

                If dialogs.pregunta("Se sugiere colocar al menos un medio de contacto" & Chr(13) & "¿Desea continuar?") = DialogResult.No Then
                    tabe.SelectedIndex = 0
                    tabecontrol.SelectedIndex = 0
                    telcasa.Focus()
                    Exit Function
                End If
            End If
            Dim i As Integer
            For i = 1 To tabe.TabPages.Count - 1
                With tabe.TabPages.Item(i).Controls.Item("pbicicar").Controls
                    If .Item("rodada").Text = "" And .Item("marca").Text = "" And .Item("color").Text = "" Then
                        dialogs.Close()

                        dialogs.advertencia("Debe colocar al menos un dato de la Bicicleta " & i)
                        tabe.SelectedTab = tabe.TabPages.Item(i)
                        .Item("rodada").Focus()
                        Exit Function
                    End If

                End With
            Next

            Dim bicestatus(tabe.TabPages.Count - 1) As bicicleta
            For i = 1 To tabe.TabPages.Count - 1
                With tabe.TabPages.Item(i).Controls.Item("pbicicar").Controls
                    Dim cheker As CheckBox = .Item("Finalizada")
                    bicestatus(i) = New bicicleta
                    bicestatus(i).bfinalizada = cheker.Checked
                    cheker = .Item("Entregada")
                    bicestatus(i).bentregada = cheker.Checked
                    cheker = .Item("Cancelada")


                End With
            Next

            If modificar Then

                delrepbic(folio)
            Else
                dialogs.Close()

                If dialogs.pregunta("Una vez que se haya guardado ya no podra editar al cliente" & vbNewLine & "ni agregar o quitar bicicletas" & vbNewLine & "¿Desea continuar?") = DialogResult.No Then
                    Exit Function
                End If
            End If

            folio = saverepbic(comboclnt.Text, totalrep, tabe, direccion.Text, rfc.Text, telcasa.Text, telcel.Text, mail.Text, fechacr, folio, nameautorizo, notaatorizo)
            setfolio(folio)
            saverep = True
            modificar = False
            showmodes()
            showfinencan()
            loadmod(folio)
            printnota.Visible = True
            loadervent(folio)
            copydg(True)
        End If
    End Function
    Public Sub setfolio(folio As String)
        ''Coloca el folio a las pestañas de las bicis
        foliolabel.Text = folio
        Dim i As Integer
        For i = 1 To tabe.TabPages.Count - 1
            With tabe.TabPages.Item(i).Controls.Item("pbicicar").Controls
                .Item("foliol").Text = folio
            End With
        Next
        canceln.Show()

    End Sub
    Public Sub loadmod(folio As String)
        'Carga la tabla de moficaciones
        Dim st As String = "SELECT Fecha,Descripcion,Autorizo,Nota,Usuario FROM Mod WHERE Folio='" & folio & "' ORDER BY Fecha"
        Dim ds As BindingSource = New BindingSource
        orden = New OleDb.OleDbCommand(st, conbicrep)
        lector = orden.ExecuteReader
        Ver.Visible = lector.HasRows
        ds.DataSource = lector
        If lector.HasRows Then
            dgmod.DataSource = ds
            dgmod.Columns.Item("Descripcion").HeaderText = "Descripción"
            dgmod.Columns.Item("Fecha").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgmod.Columns.Item("Autorizo").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgmod.Columns.Item("Nota").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgmod.Columns.Item("Usuario").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgmod.Columns.Item("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        End If

        lector.Close()

    End Sub

    Private Sub okv_Click(sender As Object, e As EventArgs) Handles okv.Click
        saverep()


    End Sub
    Private Sub loadclnt()
        rep.clnt = New Cliente
        rep.clnt.clear()
        comboclnt.Items.Clear()
        orden = New OleDb.OleDbCommand("SELECT id, Nombre FROM client", conclnt)
        lector = orden.ExecuteReader
        While lector.Read
            comboclnt.Items.Add(lector("id") & ":" & lector("Nombre"))
        End While
        lector.Close()
        comboclnt.AutoCompleteCustomSource = aclnt

    End Sub
    Private Sub blockclientefound()
        rfc.Enabled = False
        direccion.Enabled = False
        telcasa.Enabled = False
        telcel.Enabled = False
        mail.Enabled = False

    End Sub
    Private Sub unlockclientefound()
        rfc.Enabled = True
        direccion.Enabled = True
        telcasa.Enabled = True
        telcel.Enabled = True
        mail.Enabled = True

    End Sub
    Public Function searclnt() As Boolean

        rep.clnt.loadclntst(comboclnt.Text)

        If rep.clnt.found Then
            loadfromclnt(rep.clnt)

            If mayoreo Then
                estatus.Text = "El cliente es mayoreo"

            End If
            If especial Then
                estatus.Text &= " y tiene derecho a precio especial"

            End If
        Else
            rep.clnt.Nombre = comboclnt.Text
            rep.clnt.mayoreo = False
            rep.clnt.especial = False
            rep.clnt.id = -1
            estatus.Text = "Cliente no registrado"
        End If
        sumtotal()
        actmay()
        Return rep.clnt.found

    End Function
    Public Sub loadfromclnt(clientea As Cliente)
        rep.clnt = clientea
        With rep.clnt
            comboclnt.Text = .id & ":" & .Nombre
            direccion.Text = .Direccion
            rfc.Text = .RFC
            telcel.Text = .telcel
            telcasa.Text = .telcasa
            mail.Text = .mail
            mayoreo = .mayoreo
            especial = .especial

        End With
    End Sub
    Public Sub actmay() ''actualiza los mayoreso en cada una de las ventaform
        For Each a As TabPage In tabe.Controls
            If a.Text <> "Cliente" Then
                a.Tag.mayoreo = mayoreo
            End If
        Next
    End Sub
    Public Sub sumtotal()

    End Sub
    Private Sub reparacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub addv_Click(sender As Object, e As EventArgs)
        Dim st As String = comboclnt.Text
        If st = "Mostrador" Then
            st = ""
        End If
        If Not clntfound Then
            comboclnt.Text = clienteform.addclnt(st)
        Else
            comboclnt.Text = clienteform.addclnt("")
        End If
        comboclnt.Focus()
        SendKeys.Send(Chr(13))
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        If comboclnt.Text <> "1:Mostrador" Then
            Dim sep = comboclnt.Text.Split(":")
            Dim num As String = sep(0)
            '    clienteform.ver(num)
        Else
            dialogs.Close()

            dialogs.advertencia("No se pueden ver los detalles de Mostrador")
        End If
    End Sub
    Public Sub repclose()
        main.ppos.Controls.Remove(panelrep)
        panelrep.Dispose()
        Me.Close()
    End Sub
    Public Sub cerrar() Handles cerrarv.Click
        If folio = "" Or modificar Then
            dialogs.Close()

            If dialogs.pregunta("¿Quiere cerrar la edición de esta reparación?" & Chr(13) & "Los datos no guardados se perderan") = DialogResult.No Then
                Exit Sub
            End If
        End If
        delbutton(idventa)
        repclose()
        opennota.busquedabicis()
    End Sub

    Private Sub Cancelarv_Click(sender As Object, e As EventArgs) Handles cerrarv.Click


    End Sub

    Private Sub clngb_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub comboclnt_LostFocus(sender As Object, e As EventArgs) Handles comboclnt.LostFocus


    End Sub
    Private Function searclntrep(Optional nombre As String = "", Optional tel As String = "") As Boolean ''busca clientes en las notas de reparaciones
        searclntrep = False
        If nombre = "" Or tel = "" Then
            Exit Function
        End If
        Dim st As String = "SELECT * FROM nota WHERE "
        If nombre <> "" Then
            st &= "Cliente LIKE '%" & nombre & "%'"
            'If tel <> "" Then
            '    st &= " OR Telcel='" & tel & "' OR Telcasa='" & tel & "'"
            'End If
        Else
            'If tel <> "" Then
            '    st &= "Telcel='" & tel & "' OR Telcasa='" & tel & "'"
            'End If
        End If
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, conbicrep)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        While lector.Read

        End While
        searclntrep = lector.HasRows
        lector.Close()

    End Function
    Private Sub textlostf()
        copydg()
    End Sub
    Private Sub copydg(Optional saveme As Boolean = False)
        If saveme Then
            dgbicis.Rows.Clear()
            For i = 0 To tabe.TabPages.Count - 1
                dgbicis.Rows.Add()
            Next

        End If
        For i = 1 To tabe.TabPages.Count - 1
            dgbicis("Idb", i - 1).Value = i
            dgbicis("Rod", i - 1).Value = tabe.TabPages.Item(i).Controls.Item("pbicicar").Controls.Item("rodada").Text
            dgbicis("marca", i - 1).Value = tabe.TabPages.Item(i).Controls.Item("pbicicar").Controls.Item("marca").Text
            dgbicis("color", i - 1).Value = tabe.TabPages.Item(i).Controls.Item("pbicicar").Controls.Item("color").Text
            dgbicis("Subtotal", i - 1).Value = tabe.TabPages.Item(i).Controls.Item("pbicicar").Controls.Item("subsubtotal").Text
            Dim ob As CheckBox = tabe.TabPages.Item(i).Controls.Item("pbicicar").Controls.Item("Finalizada")
            Dim st1 As String
            If ob.Checked Then
                st1 = tabe.TabPages.Item(i).Controls.Item("pbicicar").Controls.Item("Finalizada").Text
                st1 = Microsoft.VisualBasic.Mid(st1, 15)
                dgbicis("Finali", i - 1).Value = st1

            End If
            ob = tabe.TabPages.Item(i).Controls.Item("pbicicar").Controls.Item("Entregada")
            If ob.Checked Then
                st1 = tabe.TabPages.Item(i).Controls.Item("pbicicar").Controls.Item("Entregada").Text
                st1 = Microsoft.VisualBasic.Mid(st1, 14)
                dgbicis("entregada", i - 1).Value = st1
            End If

        Next
    End Sub
    Public Sub pagerecont()
        Dim n As Integer = tabe.TabPages.Count
        dgbicis.Rows.Clear()
        For i = 1 To n
            If tabe.TabPages.Item(i - 1).Text <> "Cliente" Then
                tabe.TabPages.Item(i - 1).Text = "Bicicleta " & i - 1
                dgbicis.Rows.Add()
            End If

        Next
        copydg()
        dgbicis.SelectAll()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        maintrans()
        If folio = "" Then
            dialogs.Close()

            If dialogs.pregunta("Antes de agregar pagos debe guardar" & Chr(13) & "¿Desea guardar?") = DialogResult.No Then
                Exit Sub
            Else
                If Not saverep() Then
                    Exit Sub
                End If
            End If
        End If
        If tresta = 0 Then
            dialogs.Close()

            dialogs.advertencia("No puede agregar más pagos")
            Exit Sub
        End If
        pagof.Close()

        Dim pago As pagos = pagof.pagorep(folio, tresta)
        tpagado += pago.pagado
        tresta -= pago.pagado
        pagof.Close()
        If pago.pagado > 0 Then
            dgpagos.Rows.Add()
            Dim rindex As Integer = dgpagos.Rows.Count - 1
            dgpagos("Idp", rindex).Value = rindex + 1
            dgpagos("Fechap", rindex).Value = Now
            dgpagos("Canp", rindex).Value = pago.pagado
            dgpagos("usup", rindex).Value = user.idnameusu
            addpago(folio, rep.clnt.Nombre, rindex + 1, pago)

        End If

        sumapagos()

        mainnormal()

    End Sub
    Private Sub sumapagos()
        Dim i As Integer
        tpagado = 0
        For i = 0 To dgpagos.Rows.Count - 1
            tpagado += Math.Round(dgpagos("Canp", i).Value, 2)
        Next
        tpagos.Text = "Total:$" & tpagado
        tresta = Math.Round(subtotalrep - tpagado, 2)
        trestal.Text = "Resta:$" & tresta
        If tpagado > 0 Then
            pagadog.Show()
            restag.Show()
            pagadolabel.Text = "$" & tpagado
            restalabel.Text = "$" & tresta
        Else
            pagadog.Hide()
            restag.Hide()
        End If
    End Sub

    Private Sub dgpagos_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgpagos.CellContentClick
        If e.ColumnIndex = dgpagos.Columns.Item("delp").Index And dgpagos.Rows.Count > 0 And e.RowIndex >= 0 Then
            dialogs.Close()

            If dialogs.pregunta("¿Desea eliminar este pago ?") = DialogResult.Yes Then
                dgpagos.Rows.RemoveAt(e.RowIndex)
                sumapagos()

            End If
        End If
        If e.ColumnIndex = dgpagos.Columns.Item("Verp").Index Then
            Dim pagotem As pagos = New pagos
            pagotem.loadpago(folio, dgpagos("Idp", e.RowIndex).Value)
            visorpago.loaderpago(folio, dgpagos("usup", e.RowIndex).Value, pagotem, dgpagos("Idp", e.RowIndex).Value)
        End If
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub tabe_DrawItem(sender As Object, e As DrawItemEventArgs) Handles tabe.DrawItem

    End Sub

    Private Sub tabe_ControlRemoved(sender As Object, e As ControlEventArgs) Handles tabe.ControlRemoved
        subtotalesum()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)
        dgbicis.SelectAll()

    End Sub

    Private Sub arrob_Click(sender As Object, e As EventArgs) Handles arrob.Click
        mail.Text = mail.Text & "@"
        mail.Focus()
        mail.SelectionLength = 0
        mail.SelectionStart = Len(mail.Text)
    End Sub

    Private Sub mail_TextChanged(sender As Object, e As EventArgs) Handles mail.TextChanged
        If Not mail.Text.Contains("@") And mail.Text <> "" Then
            arrob.Visible = True
        Else
            arrob.Visible = False
        End If
        rep.clnt.mail = mail.Text
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles canceln.Click
        dialogs.Close()

        If dialogs.pregunta("¿Esta seguro que desea cancelar esta nota?" & Chr(13) & "La nota sera descontada y no se podra modificar") = DialogResult.Yes Then
            cancelarbici(folio, 0)
            delbutton(idventa)
            repclose()
        End If
    End Sub

    Private Sub okvc_Click(sender As Object, e As EventArgs) Handles okvc.Click
        If saverep() Then
            delbutton(idventa)
            repclose()
            opennota.busquedabicis()
        End If


    End Sub

    Private Sub comboclnt_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles comboclnt.SelectedIndexChanged

    End Sub

    Private Sub panelsup_Paint(sender As Object, e As PaintEventArgs) Handles panelsup.Paint

    End Sub
    Private Sub changeclientdat(sender As Object, e As EventArgs) Handles direccion.TextChanged, rfc.TextChanged, telcel.TextChanged, telcasa.TextChanged, mail.TextChanged
        If modificar Then
            sender.BackColor = Drawing.Color.Blue
            sender.ForeColor = Drawing.Color.White
        End If

    End Sub
    Private Sub rfc_Textchanged(sender As Object, e As EventArgs) Handles rfc.TextChanged
        rep.clnt.RFC = rfc.Text
    End Sub
    Private Sub telcasa_textchange(sender As Object, e As EventArgs) Handles telcasa.TextChanged
        rep.clnt.telcasa = telcasa.Text
    End Sub
    Private Sub telcel_Textchanged(sender As Object, e As EventArgs) Handles telcel.TextChanged
        rep.clnt.telcel = telcel.Text
    End Sub
    Private Sub direccion_TextChanged(sender As Object, e As EventArgs) Handles direccion.TextChanged
        rep.clnt.Direccion = direccion.Text
    End Sub

    Private Sub dgmod_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgmod.CellContentClick

    End Sub

    Private Sub dgmod_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgmod.CellClick
        If Ver.Visible Then
            If e.ColumnIndex = Ver.Index Then
                modver.loader(dgmod("Autorizo", e.RowIndex).Value, dgmod("Nota", e.RowIndex).Value, dgmod("Descripcion", e.RowIndex).Value)
            End If
        End If
    End Sub

    Private Sub dgbicis_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgbicis.CellContentClick
        If e.ColumnIndex = mcancel.Index And e.RowIndex >= 0 Then
            loadrazon(folio, dgbicis("Idb", e.RowIndex).Value)
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs)
        adderrepbic()
        imprimirrep(rep)

    End Sub

    Private Sub allowmodificar(sender As Object, e As EventArgs) Handles modificarb.Click

    End Sub

    Private Sub modificarb_MouseEnter(sender As Object, e As EventArgs) Handles modificarb.MouseEnter, cerrarv.MouseEnter, okv.MouseEnter, okvc.MouseEnter, canceln.MouseEnter

        shownamebutton(sender)

    End Sub

    Private Sub modificarb_MouseLeave(sender As Object, e As EventArgs) Handles modificarb.MouseLeave, cerrarv.MouseLeave, okv.MouseLeave, okvc.MouseLeave, canceln.MouseLeave

        hidenamebutton(sender)

    End Sub

    Private Sub datoscl_Click(sender As Object, e As EventArgs) Handles datoscl.Click

    End Sub

    Private Sub rfc_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles rfc.MaskInputRejected

    End Sub
    Public Sub copytoclnt()
        With rep.clnt
            .Nombre = comboclnt.Text
            .Direccion = direccion.Text
            .mail = mail.Text
            .RFC = rfc.Text
            .telcasa = telcasa.Text
            .telcel = telcel.Text
        End With


    End Sub
    Public Sub adderrepbic()
        copytoclnt()
        rep = New repbic
        rep.folio = folio
        rep.fcreacion = fechacr
        Dim i As Integer
        Dim j As Integer
        Dim rindex As Integer
        With rep.clnt
            .Nombre = comboclnt.Text
            .RFC = rfc.Text
            .Direccion = direccion.Text
            .telcasa = telcasa.Text
            .telcel = telcel.Text
            .mail = mail.Text
        End With

        For i = 1 To tabe.TabPages.Count - 1
            Dim printer As Boolean = True
            If allone.Checked = False Then ''verificar si esta seleccionado
                If dgbicis.Rows(i - 1).Selected = False Then
                    printer = False
                End If
            End If
            If printer Then
                With tabe.TabPages.Item(i).Controls("pbicicar").Controls
                    Dim bicir As bicicleta = New bicicleta
                    Dim stformat As String = .Item("formato").Text
                    If stformat = "24hrs" Then
                        stformat = ""
                    End If
                    bicir.addnew(i, .Item("rodada").Text, .Item("marca").Text, .Item("color").Text, .Item("notas").Text, fdateselhour(.Item("fe"), .Item("hora").Text & ":" & .Item("minutos").Text & ":00" & " " & stformat), tabe.TabPages.Item(i).Controls("pventa").Controls.Item("dg1"))

                    rep.addbici(bicir)

                End With
            End If
        Next
        For j = 1 To dgpagos.Rows.Count - 1
            rep.addpago(dgpagos("Canp", j).Value, dgpagos("Fechap", rindex).Value)
        Next
        rep.tpagado = tpagado
        rep.total = totalrep
        rep.resta = tpagado - totalrep
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs)
        adderrepbic()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles allone.CheckedChanged
        If allone.Checked Then
            allone.Text = "Todas"
        Else
            allone.Text = "Seleccionadas"

        End If
    End Sub

    Private Sub comboclnt_TabStopChanged(sender As Object, e As EventArgs) Handles comboclnt.TabStopChanged

    End Sub

    Private Sub buscarclnt_Click(sender As Object, e As EventArgs) Handles buscarclnt.Click
        searcliente()
    End Sub
    Private Sub searcliente()
        Dim clnt As Cliente = New Cliente
        clntre.Close()
        clnt = clntre.searclnt(2)
        If clnt.found Then
            With clnt
                comboclnt.Text = .Nombre
                direccion.Text = .Direccion
                telcasa.Text = .telcasa
                telcel.Text = .telcel
                rfc.Text = .RFC
            End With

        End If
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Clipboard.SetText(comboclnt.Text)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Clipboard.SetText(rfc.Text)

    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        Clipboard.SetText(direccion.Text)

    End Sub

    Private Sub Button2_Click_2(sender As Object, e As EventArgs) Handles Button2.Click
        Clipboard.SetText(telcel.Text)

    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        Clipboard.SetText(telcasa.Text)

    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        Clipboard.SetText(mail.Text)

    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If folio = "" Then
            dialogs.advertencia("No puede eliminar un pago si la venta no se ha guardado")
            Exit Sub
        End If
        If dgpagos.Rows.Count = 0 Then
            Exit Sub
        End If
        Dim rindex As Integer = dgpagos.CurrentCell.RowIndex
        Dim id As Integer = dgpagos("Idp", rindex).Value
        Dim fechas As Date = dgpagos("fechap", rindex).Value
        If dgpagos.Rows.Count > 0 And id > 0 Then

            If dialogs.pregunta("¿Desea eliminar el pago con ID=" & id & vbNewLine & "Esta acción es irreversible") = DialogResult.Yes Then
                delpago(folio, id)
                loadpagos()
                cancelvenr(fechas)

            End If

        End If
    End Sub

    Private Sub Button3_MouseEnter(sender As Object, e As EventArgs) Handles Button3.MouseEnter, Button9.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub Button9_MouseLeave(sender As Object, e As EventArgs) Handles Button3.MouseLeave, Button9.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        sendtowhats(telcel.Text)
    End Sub

    Private Sub Button8_Click_2(sender As Object, e As EventArgs) Handles printnota.Click
        If folio = "" Then
            dialogs.Close()
            If dialogs.pregunta("Antes de imprimir debe guardar" & Chr(13) & "¿Desea guardar?") = DialogResult.No Then
                Exit Sub
            Else
                If Not saverep() Then
                    Exit Sub
                End If
            End If
        End If

        adderrepbic()
        Dim prep As imprimir = New imprimir
        prep.printreparacion(rep)

    End Sub

    Private Sub acuses_Click(sender As Object, e As EventArgs) Handles acuses.Click

    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter, printnota.MouseEnter, bmpr.MouseEnter
        shownamebutton(sender)

    End Sub

    Private Sub printnota_MouseLeave(sender As Object, e As EventArgs) Handles printnota.MouseLeave, bmpr.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub bmpr_Click(sender As Object, e As EventArgs) Handles bmpr.Click
        If folio = "" Then
            dialogs.Close()
            If dialogs.pregunta("Antes de crear nota virtual debe guardar" & Chr(13) & "¿Desea guardar?") = DialogResult.No Then
                Exit Sub
            Else
                If Not saverep() Then
                    Exit Sub
                End If
            End If
        End If

        adderrepbic()
        Dim prep As imprimir = New imprimir
        prep.printbmprep(rep)
        If rep.clnt.telcel <> "" Then
            sendtowhats(rep.clnt.telcel, "Bicicletas " & tienda.nombre & " agradece su preferencia y confianza. Anexamos a este mensaje su nota en formato digital respecto al servicio que solicito.")
            Threading.Thread.Sleep(2000)
            SendKeys.Send("^v")
        End If
    End Sub
End Class