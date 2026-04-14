Imports System.Data.OleDb
Imports System.Xml.Schema

Public Class libreta
    Public totallabel As Boolean = True
    Public totaldia As Decimal = 0
    Public tefectivo As Decimal = 0
    Public totalretiro As Decimal = 0
    Public pagoefe As Decimal = 0
    Public pagotrans As Decimal
    Public pagotar As Decimal
    Public diactual As Date

    'Public pagodeb As Decimal = 0

    Public Sub loader()
        loadday(Now)
        tiendascombo.Items.Clear()

        tiendascombo.Items.AddRange(getnamesplaces(True))
        tiendascombo.Text = tienda.nombre
        If user.admin Then
            tiendascombo.Visible = True

        End If
    End Sub
    Public Sub loaddaytienda(fecha As Date, tiendas As lugar)
        calendario.Value = fecha

        Dim conbackvent As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & pathbackup & tiendas.apodo & "\venta.dat;Persist Security Info=True;Jet OLEDB:Database Password=vsganeo5")
        dglv.Columns.Item("Cancelb").Visible = False
        Dim st As String = "SELECT horast,cantidad,Clave,Descripcion,pagado,Cliente,usuario,cancel,pagoefe,pagotar,pagotrans,descuento,nota FROM lv WHERE fecha=#" & onlyfechasql(fecha) & "# ORDER BY hora"
        Dim orden As OleDbCommand = New OleDbCommand(st, conbackvent)
        conbackvent.Open()

        Dim lector As OleDbDataReader = orden.ExecuteReader

        If lector.HasRows Then
            Dim ds As BindingSource = New BindingSource
            ds.DataSource = lector
            dglv.DataSource = ds
            dglv.Columns.Item("Descripcion").HeaderText = "Descripción"
            dglv.Columns.Item("horast").HeaderText = "Hora"
            dglv.Columns.Item("cancel").HeaderText = "Cancelado"
            dglv.Columns.Item("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dglv.Columns.Item("Clave").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dglv.Columns.Item("horast").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dglv.Columns.Item("cliente").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dglv.Columns.Item("pagoefe").Visible = False
            dglv.Columns.Item("descuento").Visible = False
            dglv.Columns.Item("pagoefe").HeaderText = "Efectivo"
            dglv.Columns.Item("pagotar").Visible = False
            dglv.Columns.Item("pagotar").HeaderText = "Tarjeta"
            dglv.Columns.Item("pagotrans").Visible = False
            dglv.Columns.Item("pagotrans").HeaderText = "Transferencia"
            dglv.Columns.Item("cancel").Visible = False
            dglv.Columns.Item("nota").Visible = False
            dglv.Columns.Item("pagado").HeaderText = "Total"

        Else
            Dim ds As BindingSource = New BindingSource
            dglv.DataSource = ds
        End If
        lector.Close()
        conbackvent.Close()
        dgcalculation()

    End Sub

    Public Sub loadday(fecha As Date)
        diactual = fecha

        dglv.Columns.Item("Cancelb").Visible = True
        Dim st As String = "SELECT horast,Cantidad,Clave,Descripcion,pagado,Cliente,Usuario,Cancel,pagoefe,Pagotar,Pagotrans,descuento,nota FROM lv WHERE fecha=#" & onlyfechasql(fecha) & "# ORDER BY hora"
        Dim orden As OleDbCommand = New OleDbCommand(st, convent)
        Try


            Dim lector As OleDbDataReader = orden.ExecuteReader

            If lector.HasRows Then
                Dim ds As BindingSource = New BindingSource
                ds.DataSource = lector
                dglv.DataSource = ds
                dglv.Columns.Item("Descripcion").HeaderText = "Descripción"
                dglv.Columns.Item("horast").HeaderText = "Hora"
                dglv.Columns.Item("cancel").HeaderText = "Cancelado"
                dglv.Columns.Item("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                dglv.Columns.Item("Clave").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dglv.Columns.Item("horast").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dglv.Columns.Item("cliente").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dglv.Columns.Item("pagoefe").Visible = False
                dglv.Columns.Item("pagoefe").HeaderText = "Efectivo"

                dglv.Columns.Item("pagotar").Visible = False
                dglv.Columns.Item("descuento").Visible = False

                dglv.Columns.Item("Pagotar").HeaderText = "Tarjeta"
                dglv.Columns.Item("Pagotrans").Visible = False
                dglv.Columns.Item("Pagotrans").HeaderText = "Transferencia"
                dglv.Columns.Item("cancel").Visible = False
                dglv.Columns.Item("nota").Visible = False
                dglv.Columns.Item("pagado").HeaderText = "Total"

            Else
                Dim ds As BindingSource = New BindingSource
                dglv.DataSource = ds
            End If
            lector.Close()
        Catch ex As Exception
            dialogs.advertencia("No se puede conectar con base de datos")
        End Try
        dgcalculation()
        calendario.Value = fecha
    End Sub
    Private Sub dgcalculation()
        Dim i As Integer
        totaldia = 0

        pagoefe = 0
        pagotar = 0
        pagotrans = 0
        totalretiro = 0
        For i = 0 To dglv.Rows.Count - 1
            If dglv("cancel", i).Value = False Then
                If dglv("pagado", i).Value > 0 Then
                    totaldia += dglv("pagado", i).Value
                    pagoefe += dglv("pagoefe", i).Value
                    pagotar += dglv("Pagotar", i).Value
                    pagotrans += dglv("Pagotrans", i).Value

                Else
                    totalretiro += -dglv("pagoefe", i).Value
                End If

            Else
                dglv.Rows.Item(i).DefaultCellStyle.Font = ftachado
                dglv.Rows.Item(i).DefaultCellStyle.BackColor = Drawing.Color.Red

            End If

        Next
        totall.Text = "V. Total:$" & totaldia
        totalefe.Text = "Efectivo:$" & pagoefe + diaventa.Eapertura - totalretiro


    End Sub
    Private Sub panelcomando_Paint(sender As Object, e As PaintEventArgs) Handles panelcomando.Paint

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles calendario.ValueChanged
        loadday(calendario.Value)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        main.ppos.Controls.Remove(panellibreta)
        Me.Close()

    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter, hoy.MouseEnter, Cerrardia.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave, hoy.MouseLeave, Cerrardia.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub dglv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dglv.CellContentClick
        If dglv.Rows.Count > 0 Then
            If e.ColumnIndex = dglv.Columns.Item("cancelb").Index Then
                If dglv("cancel", e.RowIndex).Value = False Then
                    Dim fechadel As Date = calendario.Value.Date & " " & dglv("horast", e.RowIndex).Value
                    dialogs.Close()

                    If dialogs.pregunta("¿Desea cancelar esta venta?") = DialogResult.Yes Then
                        If dglv("nota", e.RowIndex).Value = False Then
                            cancelvenr(fechadel)
                            If (Not DBNull.Value.Equals(dglv("clave", e.RowIndex).Value)) Then
                                Dim arte As articuloventa = New articuloventa(dglv("clave", e.RowIndex).Value)
                                arte.modexistencias(dglv("cantidad", e.RowIndex).Value, True)
                                'redexistencias(dglv("clave", e.RowIndex).Value, dglv("cantidad", e.RowIndex).Value, True)
                            End If

                            loadday(calendario.Value.Date)
                        Else
                            Dim st As String = dglv("clave", e.RowIndex).Value(1)
                            Select Case st
                                Case "V"
                                    Dim ventacancel As notaventa = New notaventa
                                    ventacancel.loadventa(dglv("clave", e.RowIndex).Value)
                                    ventacancel.cancelme()
                                    cancelvenr(fechadel)
                                Case "R"
                                    If dialogs.pregunta("Para cancelar reparaciones debe abrirla" & vbNewLine & "¿Desea abrir la reparación?") = DialogResult.Yes Then
                                        Dim ob As Object = newrepwin()
                                        If Not ob.Equals(main) Then
                                            ob.loadervent(dglv("clave", e.RowIndex).Value, 0)
                                        End If
                                        Exit Sub
                                    End If

                            End Select

                            loadday(calendario.Value.Date)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub totall_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles totall.LinkClicked
        If totallabel Then
            totall.Text = ""
            If pagoefe > 0 Then
                totall.Text = "T.Efectivo:$" & pagoefe
            End If
            If pagotar > 0 Then
                totall.Text &= " Tarjeta:$" & pagotar

            End If
            If pagotrans > 0 Then
                totall.Text &= " Transferencia:$" & pagotrans

            End If
            totallabel = False
        Else
            totall.Text = "V. Total:$" & totaldia
            totallabel = True
        End If
    End Sub

    Private Sub hoy_Click(sender As Object, e As EventArgs) Handles hoy.Click
        calendario.Value = Today
        loadday(Today)
    End Sub

    Private Sub Cerrardia_Click(sender As Object, e As EventArgs) Handles Cerrardia.Click
        dialogs.Close()

        If dialogs.pregunta("¿Desea cerrar este día?") = DialogResult.Yes Then
            cierrededia.cerrardia(pagoefe, totalretiro, pagotar, pagotrans)


        End If

    End Sub

    Private Sub Retiro_Click(sender As Object, e As EventArgs) Handles Retiro.Click
        maintrans()

        If retiroefec.ShowDialog() = DialogResult.OK Then
            Dim cant As Decimal = Val(retiroefec.cantidad.Text)
            Dim st As String = retiroefec.razon.Text
            If st <> "" Then
                st = ": " & st
            End If
            If cant > 0 Then
                addretiro(cant, st)
                loadday(Today)
            End If
        End If
        retiroefec.Close()
        mainnormal()

    End Sub

    Private Sub Verp_Click(sender As Object, e As EventArgs) Handles Verp.Click
        If dglv.Rows.Count > 0 Then
            If dglv.Columns.Item("pagoefe").Visible = True Then
                dglv.Columns.Item("pagoefe").Visible = False
                dglv.Columns.Item("Pagotar").Visible = False
                dglv.Columns.Item("Pagotrans").Visible = False

            Else
                dglv.Columns.Item("pagoefe").Visible = True
                dglv.Columns.Item("Pagotar").Visible = True
                dglv.Columns.Item("Pagotrans").Visible = True

            End If


        End If


    End Sub

    Private Sub EditarPagoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditarPagoToolStripMenuItem.Click
        Dim i As Integer = dglv.CurrentCell.RowIndex
        If editpagos.loader(dglv("pagoefe", i).Value, dglv("pagotar", i).Value, dglv("pagotrans", i).Value) = DialogResult.OK Then
            With editpagos
                Dim st As String
                If dglv("nota", i).Value = False Then
                    st = "UPDATE lv SET Pagoefe=" & .efectivo.Text & ", Pagotar=" & .Credito.Text & ",Pagotrans=" & .Debito.Text & " WHERE Fecha=#" & onlyfechasql(diactual) & "# AND horast='" & dglv("horast", i).Value & "'"
                    Dim orden As OleDbCommand = New OleDbCommand(st, convent)
                    orden.ExecuteNonQuery()
                Else
                    Dim folio As String = dglv("Clave", i).Value.ToString

                    If folio.Contains("R") Then ''actualiza reparaciones
                        st = "UPDATE lv SET Pagoefe=" & .efectivo.Text & ", Pagotar=" & .Credito.Text & ",Pagotrans=" & .Debito.Text & " WHERE Clave='" & folio & "'"
                        orden = New OleDbCommand(st, convent)
                        orden.ExecuteNonQuery()
                        st = "UPDATE pagos SET Efectivo=" & .efectivo.Text & ", Tarjeta=" & .Credito.Text & ",Transferencia=" & .Debito.Text & " WHERE Folio='" & folio & "'"
                        orden = New OleDbCommand(st, conbicrep)
                        orden.ExecuteNonQuery()

                    Else ''actualiza ventas
                        st = "UPDATE venta SET Pagoef=" & .efectivo.Text & ", Pagotcr=" & .Credito.Text & ",Pagotran=" & .Debito.Text & " WHERE Folio='" & folio & "'"
                        Dim orden As OleDbCommand = New OleDbCommand(st, convent)
                        orden.ExecuteNonQuery()
                        st = "UPDATE lv SET Pagoefe=" & .efectivo.Text & ", Pagotar=" & .Credito.Text & ",Pagotrans=" & .Debito.Text & " WHERE Clave='" & folio & "'"
                        orden = New OleDbCommand(st, convent)
                        orden.ExecuteNonQuery()
                    End If

                End If
            End With
            loadday(diactual)
            editpagos.Close()
        End If

    End Sub

    Private Sub totalefe_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles totalefe.LinkClicked

    End Sub

    Private Sub AbrirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirToolStripMenuItem.Click, dglv.CellContentDoubleClick
        Dim rindex As Integer = dglv.CurrentCell.RowIndex
        If dglv("nota", rindex).Value Then
            Dim st As String = dglv("Clave", rindex).Value
            Dim stc As String = st(1)
            Select Case stc
                Case "V"
                    Dim notas As notaventa = New notaventa
                    notas.loadventa(st)
                    formvent.loaderfin(notas)
                Case "R"
                    Dim ob As Object = newrepwin()
                    If Not ob.Equals(main) Then
                        ob.loadervent(st, 0)
                    End If
                    Exit Sub
            End Select

        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        Dim rindex As Integer = dglv.CurrentCell.RowIndex
        If dglv("nota", rindex).Value Then
            AbrirToolStripMenuItem.Visible = True
        Else
            AbrirToolStripMenuItem.Visible = False
        End If
        Dim isnota As Boolean = False
        For i = 0 To dglv.SelectedRows.Count - 1
            If dglv.SelectedRows.Item(i).Cells("nota").Value Or voiddeleterstring(dglv.SelectedRows.Item(i).Cells("clave").Value) = "" Then
                isnota = True
            End If
        Next
        If isnota Then
            ConvertirANotaToolStripMenuItem.Visible = False
        Else
            ConvertirANotaToolStripMenuItem.Visible = True
        End If
    End Sub

    Private Sub dglv_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dglv.CellContentDoubleClick

    End Sub

    Private Sub libreta_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub tiendascombo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tiendascombo.SelectedIndexChanged
        If tiendascombo.Text <> tienda.nombre Then
            Dim tien As lugar = New lugar
            tien.load(tiendascombo.Text)
            loaddaytienda(calendario.Value, tien)
        Else
            loadday(calendario.Value)
        End If
    End Sub

    Private Sub ConvertirANotaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertirANotaToolStripMenuItem.Click
        Dim stas As articuloventa()
        Dim fechas As Date()
        Dim pagog As pagos()
        ReDim fechas(dglv.SelectedRows.Count - 1)
        ReDim stas(dglv.SelectedRows.Count - 1)
        ReDim pagog(dglv.SelectedRows.Count - 1)

        For i = 0 To stas.Count - 1
            stas(i) = New articuloventa(dglv.SelectedRows.Item(i).Cells("Clave").Value)
            stas(i).cantidad = dglv.SelectedRows.Item(i).Cells("Cantidad").Value
            fechas(i) = Date.Parse(diactual.ToShortDateString & " " & dglv.SelectedRows.Item(i).Cells("horast").Value)
            pagog(i) = New pagos
            pagog(i).addefectivo(dglv.SelectedRows.Item(i).Cells("pagoefe").Value)
            pagog(i).addtransferencia(dglv.SelectedRows.Item(i).Cells("pagocre").Value)
            pagog(i).addtarjeta(dglv.SelectedRows.Item(i).Cells("pagodeb").Value)
            pagog(i).adddescuento(dglv.SelectedRows.Item(i).Cells("descuento").Value)
        Next

        Dim note As notaventa = New notaventa
        note.convertfromlv(fechas, stas, pagog)

    End Sub
End Class