Imports System.ComponentModel

Public Class formvent
    Public nota As notaventa
    Public idventa As String
    Dim controltex As Control
    Dim skipf1 As Boolean = False
    Dim ventg As Integer = 0
    Dim clntid As Integer = 1
    Public mayoreo As Boolean
    Dim clntfound As Boolean
    Public subtotal As Double
    Dim stdesprev As String ''cadena de respaldo para evitar el reentre de sart
    Dim kf1 As Boolean = False
    Dim total As Decimal
    Dim adedudo As Decimal
    Dim iamdialog As Boolean
    Public especial As Boolean
    Dim clientea As Cliente = New Cliente
    Dim ventanew As Boolean = False
    Public Sub loaderfin(ByVal notas As notaventa)
        nota = notas
        Panel1.Visible = True
        Panel2.Visible = True
        Panel3.Visible = True
        Panel4.Visible = True
        Panel5.Visible = True
        pagol.Text = "$" & nota.pago.pagado
        comboclnt.Text = notas.clnt.Nombre
        Guardarv.Hide()
        block.Hide()
        Cancelarv.Hide()
        comboclnt.Enabled = False
        addv.Hide()
        clientea = nota.clnt
        adeudo = clientea.adeudo
        pagol.Show()
        pagoll.Show()
        Apartado.Hide()
        paneldia.Show()
        acepdi.Show()
        print.Show()
        imgen.Show()
        acepdi.Size = New Size(paneldia.Size.Width / 3, acepdi.Size.Height)
        print.Size = New Size(paneldia.Size.Width / 3, print.Size.Height)

        okv.Hide()
        dg1.ReadOnly = True

        adeudol.Text = "$" & notas.clnt.adeudo
        dg1.Columns.Item("del").Visible = False

        Dim k As Integer = 0
        For i = 0 To nota.articulos.articulo.Count - 1
            With nota.articulos
                If .articulo(i).descripcion <> "" Then
                    dg1.Rows.Add()
                    dg1("can", k).Value = .articulo(i).cantidad
                    dgcopyfromart(.articulo(i), dg1, k)
                    Dim a As DataGridViewComboBoxCell = dg1("tpv", k)
                    a.Value = "" 'Inicializacion obligatoria para evitar mensajes de error cuando el combo se borra. Probable error de .NET
                    a.Items.Clear()
                    a.Items.Add(.articulo(i).tp)
                    a.Value = .articulo(i).tp
                    multiimp(k)
                    k += 1
                End If

            End With

        Next
        ''dg1.Enabled = False

        iamdialog = True
        maintrans()
        nota.subtotal = subtotal
        Me.ShowDialog()
        Me.Close()
        mainnormal()

    End Sub


    Public Sub loadervg(ByVal id As Integer, ByVal cliente As String)
        loadclnt()
        dg1.Rows.Add()
        Dim menv As Double = 0
        Dim mayv As Double = 0
        Dim espv As Double = 0
        Dim pesp As Double = 0
        Dim ecl As Double = 0
        Dim eab As Double = 0
        Dim st As String = "SELECT * FROM vgart WHERE Id=" & id
        Dim i As Integer = 0
        orden2 = New OleDb.OleDbCommand(st, convent)
        lector2 = orden2.ExecuteReader

        While lector2.Read
            dg1("can", i).Value = lector2("cant") ''verificar si no se esta cargando una articulo o trabajo guardado
            Dim a As DataGridViewComboBoxCell = dg1("tpv", i)
            If Microsoft.VisualBasic.Left(lector2("Clave"), 6) = "ARNORE" Or Microsoft.VisualBasic.Left(lector2("Clave"), 6) = "TRNORE" Then
                dg1("clave", i).Value = Microsoft.VisualBasic.Left(lector2("Clave"), 6)
                dg1("des", i).ToolTipText = dg1("clave", i).Value
                dg1("des", i).Value = lector2("clave")
                a.Value = "" 'Inicializacion obligatoria para evitar mensajes de error cuando el combo se borra. Probable error de .NET
                a.Items.Clear()
                a.Items.Add(lector2("Tp"))
                a.Value = lector2("Tp")
                dg1("pq", i).Value = lector2("Paquete")
            Else
                dg1("des", i).Value = lector2("clave")
                searartdg(False, True)
                a.Value = "" 'Inicializacion obligatoria para evitar mensajes de error cuando el combo se borra. Probable error de .NET
                a.Items.Clear()
                a.Items.Add(lector2("Tp"))
                a.Value = lector2("Tp")
                'a.Value = lector2("Tp")

            End If

            i += 1
            dg1.Rows.Add()
            dg1.CurrentCell = dg1("des", i)
        End While

        dg1.CurrentCell = dg1("can", i)
        dg1.Rows.RemoveAt(i)
        comboclnt.Text = cliente
        lector2.Close()
        searclnt()
        ventanew = False
        Guardarv.Text = "Actualizar"
        dg1.Focus()
        ventg = id
    End Sub

    Private Sub dg1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg1.CellClick

        Try
            If sender.Rows.Count > 1 Then
                If e.ColumnIndex = sender("del", e.RowIndex).columnindex And e.RowIndex < sender.Rows.Count - 1 Then 'Remueve filas, se asegura que se haya seleccionado el boton y que ademas no se este eliminando la última fila
                    If dg1.Rows.Item(e.RowIndex).DefaultCellStyle.BackColor <> Color.Red And dg1.Rows.Item(e.RowIndex).DefaultCellStyle.BackColor <> Color.Yellow And dg1.Rows.Item(e.RowIndex).DefaultCellStyle.BackColor <> Color.Green And dg1.Rows.Item(e.RowIndex).DefaultCellStyle.BackColor <> Color.Pink Then
                        sender.Rows.RemoveAt(e.RowIndex)
                    Else
                        If dg1.Rows.Item(e.RowIndex).DefaultCellStyle.BackColor = Color.Pink Then
                            dg1.Rows.Item(e.RowIndex).DefaultCellStyle.BackColor = Color.Yellow
                            dg1.Rows.Item(e.RowIndex).DefaultCellStyle.Font = fnottachado
                        Else
                            dg1.Rows.Item(e.RowIndex).DefaultCellStyle.BackColor = Color.Pink
                            dg1.Rows.Item(e.RowIndex).DefaultCellStyle.Font = ftachado
                        End If

                        multiimp()

                    End If

                End If
            End If
        Catch ex As Exception

        End Try
        multiimp()

    End Sub
    Private Sub sumtotal()
        total = adeudo + subtotal
        totallabel.Text = "$" & adapterprint(total)

    End Sub
    Private Sub multiimp(Optional rowp As Integer = -1)
        Dim rindex As Integer
        If rowp <> -1 Then
            rindex = rowp
        Else
            rindex = dg1.CurrentCell.RowIndex
        End If



        Dim a As DataGridViewComboBoxCell = dg1("tpv", rindex)
        If a.Value <> "" Then
            dg1("impv", rindex).Value = tpsplit(a.Value) * dg1("can", rindex).Value
        End If
        If Val(dg1("can", rindex).Value) > Val(dg1("exis", rindex).Value) And dg1("einf", rindex).Value = False And dg1("found", rindex).Value And dg1("tra", rindex).Value = False Then
            dg1("can", rindex).ErrorText = "No hay existencias suficientes" & Chr(13) & "Existencias en tienda:" & dg1("exis", rindex).Value
        Else
            dg1("can", rindex).ErrorText = ""
        End If
        Dim i As Integer = 0
        subtotal = 0
        For i = 0 To dg1.Rows.Count - 1
            subtotal += dg1("impv", i).Value


        Next
        subtotallabel.Text = "$" & subtotal
        sumtotal()
    End Sub
    Private Sub dg1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dg1.CellEndEdit


        multiimp()

        If e.ColumnIndex = sender("can", e.RowIndex).ColumnIndex Then
            sender("can", e.ColumnIndex).Value = Val(sender("can", e.ColumnIndex).Value)
            If Val(sender("can", e.ColumnIndex).Value) = 0 Then
                sender("can", e.ColumnIndex).Value = 1
            End If

        End If
        If dg1.CurrentCell.ColumnIndex = dg1.Columns.Item("des").Index And dg1("des", e.RowIndex).Value <> "" And dg1("des", e.RowIndex).Value <> stdesprev Then 'Busca articulos

            searartdg()



        End If

        If sender.CurrentCell.ColumnIndex = sender.Columns("can").Index Then 'remueve la limitante de solo escribir numeros
            RemoveHandler CType(controltex, TextBox).KeyPress, AddressOf textnumonly
        End If
        If Val(dg1("impv", e.RowIndex).Value) > 0 Then
            tpchanger(dg1("can", e.RowIndex).Value, dg1("tpv", e.RowIndex).Value, dg1("impv", e.RowIndex).Value)
        End If







    End Sub
    Public Sub dgcelkeyup(sender As Object, e As KeyEventArgs) ''permite capturar el f1

        Select Case e.KeyCode
            Case Keys.F1

                Dim rindex As Integer = dg1.CurrentCell.RowIndex
                If skipf1 = True Then ''colocado para evitar reentrar cuando se pulsa f1
                    Exit Sub
                Else
                    skipf1 = True
                End If
                Dim art As articuloventa = catar.loadcatadialog(dg1("Des", dg1.CurrentCell.RowIndex).Value)

                If art.found = True Then

                    dgcopyfromart(art, dg1, rindex)
                    multiimp()
                    SendKeys.SendWait("{TAB}")

                Else
                    dg1.BeginEdit(True)

                End If

        End Select
    End Sub
    Public Sub searartdg(Optional ByVal allow As Boolean = True, Optional exact As Boolean = False)
        If iamdialog Then
            Exit Sub
        End If
        Dim rindex As Integer = dg1.CurrentCell.RowIndex

        Dim art As articuloventa = sart(dg1("des", rindex).Value, exact)

        dgcopyfromart(art, dg1, rindex)
        Dim i As Integer = 0

        Dim a As DataGridViewComboBoxCell = dg1("tpv", rindex)
        a.Value = "" 'Inicializacion obligatoria para evitar mensajes de error cuando el combo se borra. Probable error de .NET
        a.Items.Clear()
        a.Items.Add("1.-m:" & art.menudeo)
        a.Items.Add("2.-M:" & art.mayoreo)
        a.Items.Add("3.-E:" & art.especial)
        If clientea.mayoreo Then
            If clientea.especial And art.pme > 0 And dg1("can", rindex).Value >= art.pme Then
                a.Value = "3.-E:" & art.especial
            Else
                a.Value = "2.-M:" & art.mayoreo
            End If
        Else
            a.Value = "1.-m:" & art.menudeo
        End If



        If dg1.CurrentCell.RowIndex = dg1.Rows.Count - 1 And art.found Then
            dg1.Rows.Add()
        End If



    End Sub
    Private Sub dg1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dg1.EditingControlShowing
        If dg1.CurrentCell.OwningColumn.Name = "des" Then
            If (dg1("des", dg1.CurrentCell.RowIndex).IsInEditMode) AndAlso TypeOf e.Control Is TextBox Then 'Permite la sugerencia de articulos
                AddHandler e.Control.KeyDown, AddressOf dgcelkeyup
                skipf1 = False
                With DirectCast(e.Control, TextBox)
                    .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    .AutoCompleteSource = AutoCompleteSource.CustomSource
                    .AutoCompleteCustomSource = asd
                End With
            End If
        ElseIf dg1.CurrentCell.OwningColumn.Name = "can" Then ''evita que el codigo de autosugerencia se meta en la columna de cantidad
            If (dg1("can", dg1.CurrentCell.RowIndex).IsInEditMode) AndAlso TypeOf e.Control Is TextBox Then

                With DirectCast(e.Control, TextBox)
                    .AutoCompleteMode = AutoCompleteMode.None
                    .AutoCompleteSource = AutoCompleteSource.None
                End With
            End If
        End If


        If sender.CurrentCell.ColumnIndex = sender("can", sender.CurrentCell.RowIndex).ColumnIndex Then 'Permite solo la utilización de numeros en cantidad
            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf textnumonly
            controltex = e.Control
        End If


    End Sub
    Public Sub loadervennew()
        ' main.ppos.Controls.Add(panelventa)
        dg1.Rows.Add()
        ventanew = True
        loadclnt()

    End Sub
    Private Sub dg1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dg1.RowsAdded
        If e.RowIndex > 0 Then 'Coloca las nuevas filas con los estandares de diseño para poner a la mayus bb
            sender.Rows.Item(e.RowIndex - 1).Height = 40 'coloca 40 besos en la frente de yoyo 
            sender("del", e.RowIndex - 1).Style.BackColor = Color.FromName("InactiveCaption")

        End If



    End Sub

    Private Sub dg1_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles dg1.CellValidated
        If e.RowIndex >= 0 Then
            If dg1("tpv", e.RowIndex).ColumnIndex = e.ColumnIndex And dg1("tpv", e.RowIndex).Value <> "" Then
                tpchanger(dg1("can", e.RowIndex).Value, dg1("tpv", e.RowIndex).Value, dg1("impv", e.RowIndex).Value)
            End If
        End If
        If e.ColumnIndex = dg1("can", e.RowIndex).ColumnIndex Then
            dg1("can", e.RowIndex).Value = Val(dg1("can", e.RowIndex).Value)
            If Val(dg1("can", e.RowIndex).Value) = 0 Then
                dg1("can", e.RowIndex).Value = 1
            End If
        End If

    End Sub

    Private Sub loadclnt()
        clientea = New Cliente()
        comboclnt.Items.Clear()
        orden = New OleDb.OleDbCommand("SELECT id, Nombre FROM client", conclnt)
        lector = orden.ExecuteReader
        While lector.Read
            comboclnt.Items.Add(lector("id") & ":" & lector("Nombre"))
        End While
        lector.Close()
        comboclnt.AutoCompleteCustomSource = aclnt
    End Sub
    Public Sub cancelar() Handles Cancelarv.Click
        If iamdialog Then
            Exit Sub
        End If
        dialogs.Close()

        If dialogs.pregunta("¿Desea cancelar esta venta?") = DialogResult.Yes Then
            main.ppos.Controls.Remove(panelventa)
            delbutton(idventa)



            Me.Close()
        End If


    End Sub


    Private Sub dg1_KeyUp(sender As Object, e As KeyEventArgs) Handles dg1.KeyUp, Me.KeyUp
        Select Case e.KeyCode
            Case Keys.Tab
                Dim stname As String = dg1.Columns.GetFirstColumn(DataGridViewElementStates.Visible).Name.ToString
                If dg1("found", dg1.CurrentCell.RowIndex).Value Then
                    dg1.CurrentCell = dg1(stname, dg1.Rows.Count - 1)
                Else
                    dg1.CurrentCell = dg1("des", dg1.CurrentCell.RowIndex)

                End If

        End Select


    End Sub


    Private Sub Guardarv_Click(sender As Object, e As EventArgs) Handles Guardarv.Click
        Dim data As Date
        delemptyrows()
        'If checkerventa(dg1) Then
        If calendar.loader(data) Then
                If ventanew Then
                    addventg(comboclnt.Text, subtotal, fecha, dg1, clntid)
                    main.ppos.Controls.Remove(panelventa)
                    delbutton(idventa)
                    Me.Close()
                Else
                    delventg(ventg)
                    addventg(comboclnt.Text, subtotal, data, dg1, clntid, ventg)
                    main.ppos.Controls.Remove(panelventa)
                    delbutton(idventa)
                    Me.Close()
                End If

            End If
        ' Else
        '  dg1.Rows.Add()
        '  End If

    End Sub
    Private Sub delemptyrows()
        Dim j As Integer = 0
        While j < dg1.Rows.Count 'Se usa while por que for deja el dato guardado como constante, no verifica el dato en cada iteración
            If dg1("des", j).Value = "" Then
                dg1.Rows.RemoveAt(j)
            End If
            j += 1
        End While

    End Sub

    Private Sub addv_Click(sender As Object, e As EventArgs) Handles addv.Click

        If clientea.found = True Then
            clientea.clear()
        End If
        clientea = clienteform.moredetailsventa(clientea)
        clientea.found = False
        verc.Enabled = False
        comboclnt.Text = clientea.Nombre
        comboclnt.Focus()
        SendKeys.Send(Chr(13))

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles verc.Click
        If comboclnt.Text <> "1:Mostrador" Then
            Dim sep = comboclnt.Text.Split(":")
            Dim num As String = sep(0)
            clienteform.ver(clientea)
        Else
            dialogs.Close()

            dialogs.advertencia("No se pueden ver los detalles de Mostrador")
        End If

    End Sub

    Private Sub comboclnt_LostFocus(sender As Object, e As EventArgs) Handles comboclnt.LostFocus
        searclnt()
    End Sub
    Public Sub searclnt()
        adeudo = 0
        clientea.loadclntst(comboclnt.Text)
        estatus.Text = ""
        adeudo = clientea.adeudo
        With clientea
            If .found Then
                comboclnt.Text = .id & ":" & .Nombre
                If .mayoreo Then
                    estatus.Text = "El cliente es mayoreo"

                End If
                If .especial Then
                    estatus.Text &= " y tiene derecho a precio especial"

                End If
                adeudol.Text = "$" & .adeudo
                verc.Enabled = True
            Else
                clientea.Nombre = comboclnt.Text
                clientea.id = -1
                estatus.Text = "Cliente no registrado"
                clientea.adeudo = 0
                verc.Enabled = False
            End If

        End With
        sumtotal()
    End Sub

    Private Sub finishvent(tipo As Integer)
        pagof.Close()
        nota = New notaventa
        nota.fecha = Now
        nota.subtotal = subtotal
        nota.total = total
        nota.folio = getventrepid(1, Now)
        nota.clnt = clientea
        nota.articulos.loadfromdg(dg1)
        Select Case tipo
            Case 1
                If checkerventa(dg1) = False Then
                    Exit Sub
                End If
            Case 2
                nota.cotizacion = True

        End Select

        If dg1("des", dg1.Rows.Count - 1).Value = "" Then
            dg1.Rows.RemoveAt(dg1.Rows.Count - 1)
        End If

        Dim pagores As DialogResult = pagof.loader(nota, subtotal)
        If pagof.printed Or pagores = DialogResult.OK Then
            If Not ventanew Then
                dialogs.Close()

                If dialogs.pregunta("Esta venta ya ha sido finalizada" & Chr(13) & "¿Desea borrarla?") = DialogResult.Yes Then
                    delventg(ventg)
                End If

            End If
            nota.pago.loadfromdg(pagof.dgpago, True, total)
            nota.addventa()

            main.ppos.Controls.Remove(panelventa)
            delbutton(idventa)
            pagof.Close()
            Me.Close()

        End If



    End Sub
    Private Sub okv_Click(sender As Object, e As EventArgs) Handles okv.Click
        If iamdialog Then
            Exit Sub
        End If
        finishvent(1)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles block.Click
        If block.Text = "Bloquear" Then
            block.Tag = "Desbloquear"
            block.Text = "Desbloquear"
            If dg1("des", dg1.Rows.Count - 1).Value = "" Then
                dg1.Rows.RemoveAt(dg1.Rows.Count - 1)
            End If
            dg1.EditMode = DataGridViewEditMode.EditProgrammatically
            dg1.Columns.Item("del").Visible = False

        Else
            block.Tag = "Bloquear"
            block.Text = "Bloquear"
            dg1.Rows.Add()
            dg1.EditMode = DataGridViewEditMode.EditOnEnter
            dg1.Columns.Item("del").Visible = True
        End If


    End Sub
    Private Sub dg1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles dg1.CellContentClick


    End Sub

    Private Sub StatusStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles StatusStrip1.ItemClicked

    End Sub

    Private Sub panelventa_Paint(sender As Object, e As PaintEventArgs) Handles panelventa.Paint

    End Sub

    Private Sub dg1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dg1.CellValueChanged
    End Sub

    Private Sub dg1_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles dg1.CellValidating
        multiimp()

    End Sub


    Private Sub comboclnt_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles comboclnt.SelectedIndexChanged

    End Sub

    Private Sub adeudol_Click(sender As Object, e As EventArgs) Handles adeudol.Click

    End Sub

    Private Sub adeudol_SizeChanged(sender As Object, e As EventArgs) Handles adeudol.SizeChanged
        adptalabel(adeudol, gbadeudo, 138)

    End Sub
    Private Sub adptalabel(ByVal textinside As Object, ByRef oadapt As Object, ByVal tamdef As Integer)

        Dim s As SizeF = textinside.CreateGraphics.MeasureString(textinside.Text, textinside.Font)
        If s.Width < oadapt.Width Then

            oadapt.Size = New Drawing.Size(tamdef, oadapt.Height)

        Else
            oadapt.Size = New Drawing.Size(textinside.Size.Width, oadapt.Height)

        End If

    End Sub

    Private Sub subtotallabel_SizeChanged(sender As Object, e As EventArgs) Handles subtotallabel.SizeChanged
        adptalabel(subtotallabel, gbsub, 143)

    End Sub

    Private Sub formvent_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp

    End Sub

    Private Sub dg1_TextChanged(sender As Object, e As EventArgs) Handles dg1.TextChanged

    End Sub

    Private Sub panelventa_KeyUp(sender As Object, e As KeyEventArgs) Handles panelventa.KeyUp

    End Sub

    Private Sub formvent_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub Ventaopen_Paint(sender As Object, e As PaintEventArgs) Handles Ventaopen.Paint

    End Sub

    Private Sub clngb_Enter(sender As Object, e As EventArgs) Handles clngb.Enter

    End Sub

    Private Sub dg1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dg1.CellEnter

        If dg1.Rows.Item(e.RowIndex).DefaultCellStyle.BackColor <> Drawing.Color.Yellow And dg1.Rows.Item(e.RowIndex).DefaultCellStyle.BackColor <> Drawing.Color.Green And dg1.Rows.Item(e.RowIndex).DefaultCellStyle.BackColor <> Drawing.Color.Red And dg1.Rows.Item(e.RowIndex).DefaultCellStyle.BackColor <> Drawing.Color.Pink Then
            dg1.EditMode = DataGridViewEditMode.EditOnEnter
        Else
            dg1.EditMode = DataGridViewEditMode.EditProgrammatically

        End If
    End Sub

    Private Sub okv_MouseEnter(sender As Object, e As EventArgs) Handles okv.MouseEnter, Guardarv.MouseEnter, Cancelarv.MouseEnter, block.MouseEnter, acepdi.MouseEnter, print.MouseEnter, Apartado.MouseEnter, Cotizacion.MouseEnter
        shownamebutton(sender)

    End Sub

    Private Sub okv_MouseLeave(sender As Object, e As EventArgs) Handles okv.MouseLeave, Guardarv.MouseLeave, Cancelarv.MouseLeave, block.MouseLeave, acepdi.MouseEnter, print.MouseEnter, Apartado.MouseLeave, Cotizacion.MouseLeave
        hidenamebutton(sender)

    End Sub

    Private Sub addv_MouseEnter(sender As Object, e As EventArgs) Handles addv.MouseEnter, verc.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub addv_MouseLeave(sender As Object, e As EventArgs) Handles addv.MouseLeave, verc.MouseLeave

        hidenamebutton(sender)
    End Sub

    Private Sub dg1_EditModeChanged(sender As Object, e As EventArgs) Handles dg1.EditModeChanged

    End Sub

    Private Sub totallabel_Click(sender As Object, e As EventArgs) Handles totallabel.Click

    End Sub

    Private Sub Apartado_Click(sender As Object, e As EventArgs) Handles Apartado.Click
        If clientea.id = 1 Or clientea.Nombre = "" Then
            dialogs.advertencia("Debe asignar un nombre al cliente")
            Exit Sub
        End If
        Dim fechar As Date = Now
        Dim nota = New notaventa
        nota.apartado = True

        If calendar.loader(nota.fechae, 60) = False Then
            Exit Sub
        End If
        If dg1("des", dg1.Rows.Count - 1).Value = "" Then
            dg1.Rows.RemoveAt(dg1.Rows.Count - 1)
        End If
        pagof.Close()

        nota.fecha = Now
        nota.subtotal = subtotal
        nota.total = total
        nota.folio = getventrepid(3, Now)
        nota.clnt = clientea
        nota.articulos.loadfromdg(dg1)
        pagof.allowanypersonadedudo = True
        pagof.bticket.Hide()
        pagof.imagen.Hide()


        Dim pagores As DialogResult = pagof.loader(nota, subtotal)

            If pagof.printed Or pagores = DialogResult.OK Then
                If Not ventanew Then
                    dialogs.Close()

                    If dialogs.pregunta("Esta venta ya ha sido finalizada" & Chr(13) & "¿Desea borrarla?") = DialogResult.Yes Then
                        delventg(ventg)
                    End If

                End If
                nota.pago.loadfromdg(pagof.dgpago, True, total)
            nota.addapartado()


            main.ppos.Controls.Remove(panelventa)
                delbutton(idventa)
                pagof.Close()
                Me.Close()

            End If



    End Sub

    Private Sub EditarArtículoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditarArtículoToolStripMenuItem.Click
        Dim rindex As Integer = dg1.CurrentCell.RowIndex
        If dg1("found", rindex).Value = True Then
            altaarticulo.loadermod(dg1("clave", rindex).Value)
            searartdg(False, True)
        End If

    End Sub

    Private Sub AgregarExistenciasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgregarExistenciasToolStripMenuItem.Click
        Dim rindex As Integer = dg1.CurrentCell.RowIndex
        If dg1("found", rindex).Value = True Then
            addexist.loader(dg1("clave", rindex).Value)
            searartdg(False, True)
        End If

    End Sub

    Private Sub acepdi_Click(sender As Object, e As EventArgs) Handles acepdi.Click

    End Sub

    Private Sub print_Click(sender As Object, e As EventArgs) Handles print.Click

        imprimirventa(nota, nota.subtotal, False, nota.pago.pagado, nota.pago.resta)

    End Sub

    Private Sub imgen_Click(sender As Object, e As EventArgs) Handles imgen.Click

        printencabezadobmp(nota)

    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip1.Opening
        If user.admin Then
            EditarArtículoToolStripMenuItem.Visible = True
            AgregarExistenciasToolStripMenuItem.Visible = True
        Else
            EditarArtículoToolStripMenuItem.Visible = False
            AgregarExistenciasToolStripMenuItem.Visible = False
        End If
    End Sub

    Private Sub Cotizacion_Click(sender As Object, e As EventArgs) Handles Cotizacion.Click

        finishvent(2)
    End Sub
    Private Sub panelfloathide() Handles Cotizacion.Click, Guardarv.Click, okv.Click, Cancelarv.Click, block.Click, Apartado.Click, dg1.Click, comboclnt.Click, addv.Click, verc.Click
        panelfloat.Visible = False
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles menuext.Click
        Dim np As Point = menuext.PointToScreen(menuext.Location)

        If panelfloat.Visible = False Then
            panelfloat.Visible = True
            panelfloat.Location = New Point(np.X - 100, np.Y - 10)
        Else
            panelfloat.Visible = False
        End If

    End Sub

    Private Sub panelfloat_Paint(sender As Object, e As PaintEventArgs) Handles panelfloat.Paint

    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub dg1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dg1.CellBeginEdit
        If dg1.Columns.Item("des").Index = e.ColumnIndex Then
            stdesprev = dg1("des", e.RowIndex).Value
        End If
    End Sub
End Class