Public Class opennota
    Public modst As String = ""

    Public mopen As Boolean = False

    Public Sub acoplarresize()
        panelfolio.Size = New Size((tabvg.Size.Width / 3), panelfolio.Size.Height)
        panelclient.Size = New Size((tabvg.Size.Width / 3), panelclient.Size.Height)
        panelfecha.Size = New Size((tabvg.Size.Width / 3) - 200, panelclient.Size.Height)

        folioradio.Location = New Point(panelfolio.Location.X, folioradio.Location.Y)

        clienteradio.Location = New Point(panelclient.Location.X, folioradio.Location.Y)
        fecharadio.Location = New Point(panelfecha.Location.X, folioradio.Location.Y)

    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles buscar.Click
        searrep()
    End Sub
    Public Sub searrep()
        Dim foliot As String = ""
        Dim cliente As String = ""
        Dim fe As String = ""
        Dim fc As String = ""
        Dim b As Boolean = False


        If b Then
            searbici(dgsearch, foliot, cliente, fc, fe)
        Else
            dialogs.Close()

            dialogs.advertencia("Introduzca al menos un criterio de busqueda")
        End If
    End Sub
    Private Sub buscarrep_Paint(sender As Object, e As PaintEventArgs) Handles buscarrep.Paint

    End Sub

    Private Sub nametext_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub nametext_KeyUp(sender As Object, e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Enter
                searrep()
        End Select
    End Sub

    Private Sub fechacrerep_ValueChanged(sender As Object, e As EventArgs)
        searrep()

    End Sub

    Private Sub dg_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgsearch.CellContentClick
        Dim notas As notaventa = New notaventa
        If e.RowIndex < 0 Then
            Exit Sub
        End If
        Dim folio As String = dgsearch("folio", e.RowIndex).Value
        If dgsearch.Columns.Item("openr").Index = e.ColumnIndex Then
            Select Case folio(1)
                Case "V"
                    notas.loadventa(folio)
                    formvent.loaderfin(notas)
                Case "R"
                    Dim ob As Object = newrepwin()
                    If Not ob.Equals(main) Then
                        ob.loadervent(folio, 0)
                    End If
                    Exit Sub
            End Select
        Else

            If dgsearch("cancel", e.RowIndex).Value = False Then
                Select Case folio(1)
                    Case "V"
                        dialogs.Close()
                        If dialogs.pregunta("¿Desea cancelar esta venta?") = DialogResult.Yes Then
                            notas.loadventa(dgsearch("folio", e.RowIndex).Value)
                            notas.cancelme()
                            buscarnota()
                        End If
                End Select
            End If
        End If
    End Sub

    Private Sub dg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgsearch.CellClick

    End Sub

    Private Sub dgreps_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgreps.CellContentClick

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        newventawin()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabvg.SelectedIndexChanged

    End Sub


    Private Sub opennota_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
    Public Sub finalizarbiciclick()
        Dim folio As String = ""
        Dim id As Integer = 0
        dialogs.Close()
        Dim stpre As String = ""
        If dgreps.SelectedRows.Count = 1 Then
            stpre = "¿Desea finalizar esta bicicleta?"
        Else
            stpre = "¿Desea finalizar estas " & dgreps.SelectedRows.Count & " Bicicletas?"
        End If
        If dialogs.pregunta(stpre) = DialogResult.Yes Then
            Dim trasel As Integer = 0
            For i = 0 To dgreps.SelectedRows.Count - 1
                folio = dgreps("Folio", dgreps.SelectedRows.Item(i).Index).Value
                id = dgreps("Id", dgreps.SelectedRows.Item(i).Index).Value
                If dgreps.SelectedRows.Count > 1 Then
                    finalizarbici(folio, id,, True, trasel)

                Else
                    finalizarbici(folio, id)

                End If
            Next

            busquedabicis()

        End If
    End Sub
    Public Sub entregarbiciclick()
        dialogs.Close()
        Dim folio As String = ""
        Dim id As Integer = 0
        Dim stpre As String = ""
        If dgreps.SelectedRows.Count = 1 Then
            stpre = "¿Desea entregar esta bicicleta?"
        Else
            stpre = "¿Desea entregar esta " & dgreps.SelectedRows.Count & " Bicicletas?"
        End If
        If dialogs.pregunta(stpre) = DialogResult.Yes Then

            For i = 0 To dgreps.SelectedRows.Count - 1
                folio = dgreps("Folio", dgreps.SelectedRows.Item(i).Index).Value
                If orderc.SelectedIndex = 0 Then
                    id = dgreps("Id", dgreps.SelectedRows.Item(i).Index).Value
                Else
                    id = 0
                End If


                markentregada(folio, id)
            Next

            busquedabicis()

        End If
    End Sub
    Private Sub dgreps_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgreps.CellClick
        If e.RowIndex >= 0 Then
            Dim folio As String = dgreps("Folio", e.RowIndex).Value
            Dim id As Integer = 0
            If orderc.SelectedIndex = 0 Then
                id = dgreps("Id", e.RowIndex).Value
            End If
            Select Case e.ColumnIndex
                Case dgreps.Columns.Item("Entregar").Index
                    entregarbiciclick()

                Case dgreps.Columns.Item("finalires").Index
                    finalizarbiciclick()


                Case dgreps.Columns.Item("openres").Index
                    Dim ob As Object = newrepwin()
                    If Not ob.Equals(main) Then
                        ob.loadervent(folio, id)
                    End If
                    Exit Sub
                Case dgreps.Columns.Item("desal").Index

                    dialogs.Close()

                    If dialogs.pregunta("¿Desea desalmacenar esta bicicleta?") = DialogResult.Yes Then
                        almacenarbici(folio, "", id, False)
                        busquedabicis()
                    End If
                Case dgreps.Columns.Item("Whats").Index
                    Dim telcel As String = getwhatsrep(folio)
                    If telcel <> "" Then
                        sendtowhats(telcel)
                    End If

            End Select
        End If
    End Sub

    Public Sub loader() Handles tabvg.GotFocus

        Select Case idti
            Case 1
                tiendatab.SelectedIndex = 0

            Case 2
                tiendatab.SelectedIndex = 1
        End Select
        searchvg()
        orderc.SelectedIndex = 0
        reparacionc.SelectedIndex = 0
        Dim bnota As Boolean
        If orderc.SelectedIndex = 1 Then
            bnota = False
        Else
            bnota = True
        End If
        searresf(dgreps, bnota)
        onlytienda()
    End Sub
    Private Sub onlytienda()

        If tienda.tipo = "Tienda solo" Then
            tabvg.TabPages.Remove(rep1)
            tabbuscar.TabPages.Remove(TabPage2)
        End If


    End Sub


    Public Sub searchvg()
        dgvg.Rows.Clear()
        Dim tiendax As Integer = tiendatab.SelectedIndex + 1
        Dim st As String = "Select Id, Cliente,Subtotal,Dc,Fe,Uc FROM vg"
        orden = New OleDb.OleDbCommand(st, convent)
        lector = orden.ExecuteReader
        If lector.HasRows Then
            Dim ds As BindingSource = New BindingSource
            ds.DataSource = lector
            dgvg.DataSource = ds
            aceptar.Enabled = True
            dgvg.Columns.Item("Fe").HeaderText = "Entrega"
            dgvg.Columns.Item("Dc").HeaderText = "Creado"
            dgvg.Columns.Item("Uc").HeaderText = "Usuario"
        Else
            aceptar.Enabled = False
        End If
        lector.Close()

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub dg1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgsearch.CellContentClick

    End Sub

    Private Sub tienda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tiendatab.SelectedIndexChanged
        searchvg()
    End Sub

    Private Sub eliminar_Click(sender As Object, e As EventArgs) Handles eliminar.Click



        If dgvg.Rows.Count > 0 Then
            Dim rindex As Integer = dgvg.CurrentCell.RowIndex
            dialogs.Close()

            If dialogs.pregunta("¿Desea eliminar la siguiente venta?" & Chr(13) & "Id:" & dgvg("Id", rindex).Value & " Cliente " & dgvg("Cliente", rindex).Value) = DialogResult.Yes Then
                delventg(dgvg("Id", rindex).Value)
                searchvg()
            End If
        Else
            Beep()
        End If
    End Sub

    Private Sub tabvg_GotFocus(sender As Object, e As EventArgs) Handles tabvg.GotFocus

    End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click, dgvg.CellDoubleClick
        Dim a As Object = newventawin()
        If Not a.Equals(Me) Then
            a.loadervg(dgvg("id", dgvg.CurrentCell.RowIndex).Value, dgvg("Cliente", dgvg.CurrentCell.RowIndex).Value)
        End If
    End Sub

    Private Sub TableLayoutPanel2_Paint(sender As Object, e As PaintEventArgs)

    End Sub
    Private Sub loadterminadas(ByRef dgterminadas As DataGridView)
        Dim ds As BindingSource = New BindingSource
        Dim st As String
        dgterminadas.Rows.Clear()
        dgterminadas.Columns.Item("Entrega").Visible = False
        If orderc.SelectedIndex = 1 Then
            st = "SELECT Folio,Cliente,Fcreacion,Total,Usuario FROM nota WHERE Finalizadaall=TRUE AND bentregada=FALSE AND Almacenada=FALSE And Fcan=False"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            lector = orden.ExecuteReader
            If lector.HasRows Then
                dgterminadas.DataSource = ds
                ds.DataSource = lector
                dgterminadas.Columns.Item("Folio").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dgterminadas.Columns.Item("Fcreacion").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dgterminadas.Columns.Item("Fcreacion").HeaderText = "Fecha"

            End If
            lector.Close()
        Else
            st = "SELECT Folio,Id,Rodada,Marca,Color,Nota,Fhentrega FROM biciconsulta WHERE bentregada=FALSE AND Almacenada=FALSE And bcancelada=FALSE AND bFinalizada=TRUE"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            lector = orden.ExecuteReader

            If lector.HasRows Then
                ds.DataSource = lector
                dgterminadas.DataSource = ds
                dgterminadas.Columns.Item("Folio").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dgterminadas.Columns.Item("Fhentrega").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dgterminadas.Columns.Item("Fhentrega").HeaderText = "Fecha"
                lector.Close()
            End If
        End If


    End Sub
    Private Sub dgterminadas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub ntrb_CheckedChanged(sender As Object, e As EventArgs)

    End Sub
    Public Sub busquedabicis() Handles reparacionc.SelectedIndexChanged, orderc.SelectedIndexChanged, tabvg.Selected
        desal.Visible = False
        finalires.Visible = True
        Select Case reparacionc.SelectedIndex
            Case 0 ''busca reparaciones
                Dim bnota As Boolean
                If orderc.SelectedIndex = 1 Then
                    bnota = False
                Else
                    bnota = True
                End If
                FinalizrToolStripMenuItem.Visible = True
                '  EntregarToolStripMenuItem.Visible = False
                searresf(dgreps, bnota)

            Case 1 ''busca terminadas
                loadterminadas(dgreps)
                FinalizrToolStripMenuItem.Visible = False
                '    EntregarToolStripMenuItem.Visible = True

                finalires.Visible = False
            Case 2 ''busca almacenadas
                Dim bici As Boolean
                FinalizrToolStripMenuItem.Visible = False

                If orderc.SelectedIndex = 0 Then
                    bici = True
                Else
                    bici = False
                End If
                loadalmacenadas(dgreps, bici)
        End Select

        loadapartados()


    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TableLayoutPanel4_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel4.Paint

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        newrepwin()
    End Sub


    Private Sub Button3_MouseEnter(sender As Object, e As EventArgs) Handles aceptar.MouseEnter, eliminar.MouseEnter, buscar.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub Button3_MouseLeave(sender As Object, e As EventArgs) Handles aceptar.MouseLeave, eliminar.MouseLeave, buscar.MouseLeave
        hidenamebutton(sender)

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Ventabici_Click(sender As Object, e As EventArgs)
        newrepwin(True)
    End Sub

    Private Sub busquedabicis(sender As Object, e As EventArgs)

    End Sub

    Private Sub orderclabel_Click(sender As Object, e As EventArgs) Handles orderclabel.Click

    End Sub

    Private Sub AlmacenarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlmacenarToolStripMenuItem.Click
        Dim rindex As Integer = dgreps.CurrentCell.RowIndex
        Dim folio As String = dgreps("Folio", rindex).Value
        Dim id As Integer = 0
        If orderc.SelectedIndex = 0 Then
            id = dgreps("Id", rindex).Value
        End If
        dialogs.Close()

        If dialogs.pregunta("¿Desea almacenar esta bicicleta?") = DialogResult.Yes Then
            If razon.loader = DialogResult.OK Then
                almacenarbici(folio, razon.razont.Text, id)
                busquedabicis()
            End If
        End If

    End Sub

    Private Sub CancelarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelarToolStripMenuItem.Click
        Dim rindex As Integer = dgreps.CurrentCell.RowIndex
        Dim folio As String = dgreps("Folio", rindex).Value
        Dim id As Integer = 0
        If orderc.SelectedIndex = 0 Then
            id = dgreps("Id", rindex).Value
        End If

        If dialogs.pregunta("¿Desea cancelar esta bicicleta?") = DialogResult.Yes Then
            cancelarbici(folio, id)
            busquedabicis()
        End If

    End Sub

    Private Sub RadioButton1_CheckedChanged_1(sender As Object, e As EventArgs) Handles folioradio.CheckedChanged, fecharadio.CheckedChanged, fecharadio.CheckedChanged
        If folioradio.Checked Then
            foliotext.Enabled = True
            foliotext.Focus()
            clientetext.Enabled = False
            fechatext.Enabled = False
            clienteradio.Checked = False
            fecharadio.Checked = False

        ElseIf clienteradio.Checked Then
            foliotext.Enabled = False
            clientetext.Enabled = True
            clientetext.Focus()
            fechatext.Enabled = False
            folioradio.Checked = False
            fecharadio.Checked = False
        ElseIf fecharadio.Checked Then
            foliotext.Enabled = False
            clientetext.Enabled = False
            fechatext.Enabled = True
            fechatext.Focus()
            clienteradio.Checked = False
            folioradio.Checked = False
        End If

    End Sub
    Private Sub acoplardgsearch(tipo As Integer)
        Dim st As String = ""

        dgsearch.Columns.Item("Folio").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgsearch.Columns.Item("Cliente").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgsearch.Columns.Item("Fecha").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgsearch.Columns.Item("Total").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Select Case tipo
            Case 1
                dgsearch.Columns.Item("Cancel").Visible = False
                dgsearch.Columns.Item("Hora").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                st = "Cancel"
                dgsearch.Columns.Item("Cancelar").Visible = True

            Case 2
                dgsearch.Columns.Item("Fcan").Visible = False
                dgsearch.Columns.Item("Cancelar").Visible = False
                st = "Fcan"
        End Select
        For i = 0 To dgsearch.Rows.Count - 1
            If dgsearch(st, i).Value = True Then
                dgsearch.Rows.Item(i).DefaultCellStyle.Font = ftachado
                dgsearch.Rows.Item(i).DefaultCellStyle.BackColor = Drawing.Color.Red

            End If
        Next
    End Sub
    Private Sub buscarnota() Handles buscarventa.Click
        If folioradio.Checked Then
            If folioradio.Text <> "" Then
                Dim st As String = "SELECT Folio,Cliente,Fecha,Hora,Total,Cancel FROM venta WHERE Folio LIKE '" & foliotext.Text & "%' ORDER BY Fechae"
                Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, convent)
                Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
                If lector.HasRows Then
                    Dim ds As BindingSource = New BindingSource
                    ds.DataSource = lector
                    dgsearch.DataSource = ds
                    acoplardgsearch(1)
                End If
                lector.Close()
            End If
        ElseIf clienteradio.Checked Then
            If clientetext.Text <> "" Then
                Dim st As String = "SELECT Folio,Cliente,Fecha,Hora,Total,Cancel FROM venta WHERE Cliente LIKE '%" & clientetext.Text & "%'"
                Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, convent)
                Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
                If lector.HasRows Then
                    Dim ds As BindingSource = New BindingSource
                    ds.DataSource = lector
                    dgsearch.DataSource = ds
                    acoplardgsearch(1)
                End If
                lector.Close()
            End If
        ElseIf fecharadio.Checked Then
            Dim st As String = "SELECT Folio,Cliente,Fecha,Hora,Total,Cancel FROM venta WHERE Fecha='" & fechatext.Value.ToShortDateString & "'"
            Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, convent)
            Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
            If lector.HasRows Then
                Dim ds As BindingSource = New BindingSource
                ds.DataSource = lector
                dgsearch.DataSource = ds
                acoplardgsearch(1)
            End If
            lector.Close()
        End If

    End Sub
    Private Sub buscarreparacion() Handles buscarr.Click
        If folioradior.Checked Then
            If folioradior.Text <> "" Then
                Dim st As String = "SELECT Folio,Cliente,Fecha,Total,Fcan FROM nota WHERE Folio LIKE '" & folior.Text & "%' ORDER BY Fcreacion"
                Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, conbicrep)
                Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
                If lector.HasRows Then
                    Dim ds As BindingSource = New BindingSource
                    ds.DataSource = lector
                    dgsearch.DataSource = ds
                    acoplardgsearch(2)
                End If
                lector.Close()
            End If
        ElseIf fecharadior.Checked Then
            Dim st As String = "SELECT Folio,Cliente,Fecha,Total,Fcan FROM nota WHERE Fecha='" & fechar.Value.ToShortDateString & "' ORDER BY Fcreacion"
            Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, conbicrep)
            Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
            If lector.HasRows Then
                Dim ds As BindingSource = New BindingSource
                ds.DataSource = lector
                dgsearch.DataSource = ds
                acoplardgsearch(2)
            End If
            lector.Close()
        ElseIf clienteradior.Checked Then
            Dim cond As Boolean = False
            Dim st As String = "SELECT Folio,Cliente,Fecha,Total,Fcan FROM bicibusqueda WHERE "
            If cliente.Text <> "" Then
                st &= "Cliente LIKE '%" & cliente.Text & "%' "
                cond = True
            End If
            If tel.Text <> "" Then
                If cond Then
                    st &= "AND "
                End If
                st &= "telcasa='" & tel.Text & "' OR telcel='" & tel.Text & "' "
                cond = True
            End If
            If marca.Text <> "" Then
                If cond Then
                    st &= "AND "
                End If
                st &= "Marca LIKE '%" & marca.Text & "%' "
                cond = True
            End If
            If colorbi.Text <> "" Then
                If cond Then
                    st &= "AND "
                End If
                st &= "Color LIKE '%" & colorbi.Text & "%' "
                cond = True
            End If
            If rodada.Text <> "" Then
                If cond Then
                    st &= "AND "
                End If
                st &= "rodada LIKE '%" & rodada.Text & "%' "
                cond = True
            End If
            st &= "ORDER BY Fcreacion"
            Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, conbicrep)
            Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
            If lector.HasRows Then
                Dim ds As BindingSource = New BindingSource
                ds.DataSource = lector
                dgsearch.DataSource = ds
                acoplardgsearch(2)
            End If
            lector.Close()
        End If

    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs)

        buscarnota()
    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs)
        shownamebutton(sender)
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs)
        hidenamebutton(sender)
    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub opennota_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        acoplarresize()
    End Sub

    Private Sub fecharadio_CheckedChanged(sender As Object, e As EventArgs) Handles fecharadio.CheckedChanged

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub fecharadior_CheckedChanged(sender As Object, e As EventArgs) Handles fecharadior.CheckedChanged

    End Sub

    Private Sub buscarnota(sender As Object, e As EventArgs) Handles buscarventa.Click

    End Sub

    Private Sub panelfolio_Paint(sender As Object, e As PaintEventArgs) Handles panelfolio.Paint

    End Sub

    Private Sub folioradior_CheckedChanged(sender As Object, e As EventArgs) Handles folioradior.CheckedChanged, fecharadior.CheckedChanged, clienteradior.CheckedChanged
        If folioradior.Checked Then
            folior.Enabled = True
            folior.Focus()
            fechar.Enabled = False
            cliente.Enabled = False
            tel.Enabled = False
            rodada.Enabled = False
            marca.Enabled = False
            colorbi.Enabled = False
        ElseIf fecharadior.Checked Then
            folior.Enabled = False
            fechar.Enabled = True
            fechar.Focus()
            cliente.Enabled = False
            tel.Enabled = False
            rodada.Enabled = False
            marca.Enabled = False
            colorbi.Enabled = False
        ElseIf clienteradior.Checked Then
            folior.Enabled = False
            fechar.Enabled = False
            cliente.Enabled = True
            tel.Enabled = True
            rodada.Enabled = True
            marca.Enabled = True
            colorbi.Enabled = True
            cliente.Focus()

        End If
    End Sub
    Public Sub loadapartados()
        If dgapartado.Rows.Count > 1 Then
            dgapartado.Rows.Clear()
        End If

        Dim stwhere As String = ""
        Dim entregado As Boolean = False
        Dim cancelado As Boolean = False
        If nombreapartado.Text <> "" Then
            stwhere = " WHERE Cliente LIKE '%" & nombreapartado.Text & "%' "
        End If
        If folioapartado.Text <> "" Then
            If stwhere <> "" Then
                stwhere &= " AND "
            Else
                stwhere &= " WHERE "
            End If
            stwhere &= " Folio ='" & folioapartado.Text & "' "
        End If
        If stwhere <> "" Then
            stwhere &= " AND "
        Else
            stwhere &= " WHERE "
        End If

        If rapart.Checked Then
            stwhere &= " Fentregado IS NULL AND Fcancelado IS NULL"

        End If
        If rentre.Checked Then
            stwhere &= " Fentregado IS NOT NULL"

        End If

        If rcan.Checked Then
            stwhere &= " Fcancelado IS NOT NULL"

        End If

        Dim st As String = "SELECT Folio,Cliente,Fecha,Fechar,Total,Acuenta,Total-Acuenta,Usuario,Fentregado,Fcancelado FROM Apartado" & stwhere
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, convent)

        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        If Not lector.HasRows Then
            lector.Close()
            Exit Sub
        End If
        Dim ds As BindingSource = New BindingSource
        ds.DataSource = lector
        dgapartado.DataSource = ds

        lector.Close()
        rearrangedg(entregado, cancelado)

    End Sub
    Public Sub rearrangedg(entregado As Boolean, cancelado As Boolean)

        With dgapartado.Columns
            If dgapartado.Rows.Count = 0 Then
                Exit Sub

            End If
            .Item("Folio").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            .Item("Cliente").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            .Item("Fecha").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            .Item("Total").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            .Item("Acuenta").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            .Item("Fechar").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            .Item("Fechar").HeaderText = "Entrega"
            .Item("Acuenta").HeaderText = "A cuenta"
            .Item(dgapartado.ColumnCount - 4).HeaderText = "Resta"
            .Item("Usuario").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            If entregado Then
                .Item("Fentregado").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                .Item("Fentregado").Visible = True
            Else
                .Item("Fentregado").Visible = False
            End If
            If cancelado Then
                .Item("Fcancelado").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                .Item("Fcancelado").Visible = True
            Else
                .Item("Fcancelado").Visible = False
            End If
        End With



    End Sub

    Private Sub Panel3_Paint_1(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub TabPage3_GotFocus(sender As Object, e As EventArgs) Handles tabapartado.GotFocus

    End Sub

    Private Sub dgapartado_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgapartado.CellContentClick
        Dim folio As String = dgapartado("Folio", e.RowIndex).Value
        Dim rindex As Integer = dgapartado.CurrentCell.RowIndex

        Select Case e.ColumnIndex
            Case 0
                If dialogs.pregunta("¿Desea entregar esta bicicleta?") = DialogResult.Yes Then
                    If (dgapartado("Total", rindex).Value - dgapartado("acuenta", rindex).Value) > 0 Then
                        If dialogs.pregunta("Esta bicicleta no se ha liquidado" & vbNewLine & "¿Esta seguro que desea continuar?") = DialogResult.No Then
                            Exit Sub
                        End If
                    End If
                    entregarapartado(folio)
                    loadapartados()
                End If
            Case 1
                Dim notaa As notaventa = New notaventa
                notaa.loadapartado(folio)
                formvent.loaderfin(notaa)
            Case 2
                Dim nota As notaventa = New notaventa
                nota.loadapartado(folio)
                pagof.Close()
                pagof.allowanypersonadedudo = True
                If pagof.loader(nota, nota.resta,, False) = DialogResult.OK Then
                    nota.addpagoapartado()
                    loadapartados()

                End If
            Case 3
                visorpago.loaderapartado(folio)
            Case 4
                If dialogs.pregunta("¿Desea cancelar esta nota de apartado?" & vbNewLine & "Folio:" & folio) = DialogResult.Yes Then
                    cancelarapartado(folio)
                    loadapartados()
                End If
        End Select

    End Sub

    Private Sub dgapartado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgapartado.CellClick

    End Sub


    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub


    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles nombreapartado.TextChanged

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click_3(sender As Object, e As EventArgs) Handles Button1.Click
        loadapartados()

    End Sub

    Private Sub rapart_CheckedChanged(sender As Object, e As EventArgs) Handles rapart.CheckedChanged, rentre.CheckedChanged, rcan.CheckedChanged
        loadapartados()

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub FinalizrToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinalizrToolStripMenuItem.Click
        finalizarbiciclick()
    End Sub

    Private Sub EntregarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EntregarToolStripMenuItem.Click

        entregarbiciclick()

    End Sub

    Private Sub buscarreparacion(sender As Object, e As EventArgs) Handles buscarr.Click

    End Sub

    Private Sub dgvg_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvg.CellContentClick

    End Sub
End Class
