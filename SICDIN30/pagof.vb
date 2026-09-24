Imports System.Drawing.Printing
Imports System.Net.Http.Headers

Public Class pagof
    Dim clnt As Cliente
    Dim ivaon As Boolean = False
    Dim ntventa As notaventa
    Public allowanypersonadedudo As Boolean = False
    Public subtotal As Decimal
    Public descuento As Decimal
    Public printed As Boolean = False
    Public allowadeudos As Boolean = False
    Public allowanyadeudo As Boolean = False
    Public total As Decimal
    Public pago As Decimal
    Public clntid As Integer
    Public clientest As String
    Public namecl As String
    Public resta As Decimal
    Public adeudo As Decimal


    Public folio As String
    Public dgprint As DataGridView
    Public cambio As Decimal
    Public statepago As Integer = 0
    ''state=0 venta normal
    ''state=1 venta reparación

    Public Function pagorep(folio As String, restarep As Decimal) As pagos
        ntventa = New notaventa
        ntventa.pago = New pagos
        statepago = 1
        addart(1, "Pago nota " & folio)
        dgpago.Rows.Add()
        total = restarep
        Label1.Text = "Total:$" & total
        dgpago("Tipo", 0).Value = "1.-Efectivo"
        dgpago.CurrentCell = dgpago(0, 0)
        dgpago.Focus()
        dgpago("cantidad", 0).Value = total
        ntventa.pago.total = total
        ntventa.pago.adeudo = 0
        Me.ClientSize = New Size(1150, 520)
        dgpanel.Visible = True
        asc.Visible = True
        bnota.Visible = True
        iva.Visible = True
        bticket.Visible = True
        imagen.Visible = True
        nombrec.Location = New Point(CInt((Me.ClientSize.Width / 2) - (nombrec.Size.Width / 2)), 4)
        If Me.ShowDialog = DialogResult.OK Then
            ntventa.pago.loadfromdg(dgpago, False, total)
        End If

        pagorep = ntventa.pago
    End Function

    Public Sub centerscreen()
        Me.StartPosition = FormStartPosition.Manual
        Me.CenterToScreen()
        Me.Location = New Point(0, Me.Location.Y)

    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean 'cambia el enter a tab
        If msg.WParam.ToInt32() = CInt(Keys.Enter) Then
            SendKeys.SendWait("{TAB}")
            Return True
        Else
            Return False
        End If

    End Function
    Public Function loader(ByRef nota As notaventa, ByVal subtotals As Decimal, Optional foliod As String = "", Optional showdg As Boolean = True) As DialogResult  'carga el formulario de pago con las opciones predefinidas 

        statepago = 0
        descuento = 0
        ntventa = nota
        ntventa.pago = New pagos
        clnt = nota.clnt
        folio = nota.folio
        ntventa.pago.total = subtotals
        ntventa.pago.pagado = total
        ntventa.pago.efectivo = total
        dgl.Rows.Clear()
        dgpago.Rows.Clear()
        folio = foliod
        dgpago.Rows.Add()
        dgpago("Tipo", 0).Value = "1.-Efectivo"
        dgpago.CurrentCell = dgpago(0, 0)
        dgpago.Focus()
        subtotal = subtotals
        total = subtotal + clnt.adeudo
        dgpago("cantidad", 0).Value = total
        Label1.Text = "Subtotal:$ &" & subtotal
        Label2.Text = "Adeudo:$ " & clnt.adeudo
        If clnt.credito > 0 Then
            Label2.Text = Label2.Text & " Max:$ " & clnt.credito
        End If
        Label4.Text = "Total:$ " & total
        resta = 0
        cambio = 0
        nombrec.Text = "Cliente " & clnt.Nombre
        If Not showdg Then
            Me.ClientSize = New Size(450, 520)
            dgpanel.Visible = False
            asc.Visible = False
            bnota.Visible = False
            iva.Visible = False
            bticket.Visible = False
            imagen.Visible = False
        Else
            Me.ClientSize = New Size(1150, 520)
            dgpanel.Visible = True
            asc.Visible = True
            bnota.Visible = True
            iva.Visible = True
            bticket.Visible = True
            imagen.Visible = True
        End If
        nombrec.Location = New Point(CInt((Me.ClientSize.Width / 2) - (nombrec.Size.Width / 2)), 4)

        loadprint(ntventa.articulos)
        If ntventa.cotizacion Then
            Cancel.Visible = False
            dgpago.Enabled = False

        Else
            Cancel.Visible = True
            dgpago.Enabled = True
        End If
        Dim diagresult As DialogResult = Me.ShowDialog
        If ntventa.cotizacion Then
            loader = DialogResult.Cancel

        Else
            If diagresult = vbOK Then
                ntventa.pago.pagado = pago
                ntventa.pago.adeudo = adeudo
                ntventa.pago.loadfromdg(dgpago, False, total)

            End If
            loader = diagresult
        End If

    End Function
    Public Sub loaderverap(ByRef art As articuloventa) 'carga el formulario de pago con las opciones predefinidas
        dgl.Rows.Add()
        dgpago.Rows.Add()
        dgpago("Tipo", 0).Value = "1.-Efectivo"
        dgpago.CurrentCell = dgpago(0, 0)
        dgpago.Focus()
        Label1.Text = "Subtotal:$ &" & subtotal
        Label2.Text = "Adeudo:$ " & clnt.adeudo

        Label4.Text = "Total:$ " & total
        resta = 0
        cambio = 0
        nombrec.Text = "Mostrador"
        Me.ClientSize = New Size(1150, 520)
        dgpanel.Visible = True
        asc.Visible = True
        bnota.Visible = True
        iva.Visible = True
        bticket.Visible = True
        imagen.Visible = True
        nombrec.Location = New Point(CInt((Me.ClientSize.Width / 2) - (nombrec.Size.Width / 2)), 4)

    End Sub
    Public Function loadpagoadeudo(clnts As Cliente) As DialogResult

        ntventa = New notaventa
        ntventa.pagoadeudo = True
        ntventa.folio = getventrepid(1, Now)
        ntventa.clnt = clnts
        clnt = clnts
        adeudo = 0
        total = ntventa.clnt.adeudo
        Label4.Text = "Total:$" & total
        Label1.Visible = False
        Label2.Visible = False
        Label6.Visible = False
        Label3.Visible = False
        dgl.Rows.Add()
        dgl("can", 0).Value = 1
        dgl("des", 0).Value = "Pago adeudo del " & clnts.ultcompra.ToShortDateString
        ntventa.pago = New pagos
        dgpago.Rows.Add()
        dgpago("Tipo", 0).Value = "1.-Efectivo"
        dgpago.CurrentCell = dgpago(0, 0)
        dgpago("cantidad", 0).Value = total
        ntventa.pago.addefectivo(total)
        ntventa.pago.total = total
        ntventa.pago.preaduedo = 0
        ntventa.pago.pagado = total
        ntventa.pago.efectivo = total
        ntventa.total = total
        Me.ClientSize = New Size(1150, 520)
        dgpanel.Visible = True
        asc.Visible = True
        bnota.Visible = True
        iva.Visible = True
        bticket.Visible = False
        imagen.Visible = True
        nombrec.Location = New Point(CInt((Me.ClientSize.Width / 2) - (nombrec.Size.Width / 2)), 4)
        dgpago.Focus()
        loadpagoadeudo = DialogResult.Cancel

        If Me.ShowDialog() = DialogResult.OK Then
            ntventa.pago.loadfromdg(dgpago, True, total)
            ntventa.articulos.addart("PAGOAD", 1, "Pago adeudo del " & clnt.ultcompra, 1, ntventa.pago.pagado, ntventa.pago.pagado, "2.-M", "", False, 1)
            ntventa.addventarapida()
            ntventa.clnt.modadeudo(clnt.adeudo - ntventa.pago.pagado)
            loadpagoadeudo = DialogResult.OK

        End If

    End Function

    Private Sub pago_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Public Sub loadprint(venta As dgventa, Optional asc As Boolean = False) ''carga el de la impresin

        ''   Me.Size = New Size(1412, 597)
        dgl.Rows.Clear()
        Dim k As Integer = 0
        If Not asc Then
            For i = venta.articulo.Count - 1 To 0 Step -1
                dgl.Rows.Add()
                dgl("can", k).Value = venta.articulo(i).cantidad
                dgl("des", k).Value = venta.articulo(i).descripcion
                dgl("des", k).ToolTipText = venta.articulo(i).clave
                k += 1
            Next
        Else
            For i = 0 To venta.articulo.Count - 1
                dgl.Rows.Add()
                dgl("can", k).Value = venta.articulo(i).cantidad
                dgl("des", k).Value = venta.articulo(i).descripcion
                dgl("des", k).ToolTipText = venta.articulo(i).clave
                k += 1
            Next
        End If

    End Sub

    Private Sub dgpago_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgpago.CellContentClick 'verifica donde se haya hecho click y realiza la acción
        If e.ColumnIndex = dgpago.Columns("agregar").Index Then 'verifica que se haya hecho click en agregar y que ademas cantidad no sea nulo
            If dgpago("cantidad", dgpago.Rows.Count - 1).Value.ToString <> "" Then
                dgpago.Rows.Add()
                dgpago("cantidad", e.RowIndex + 1).Value = resta
                dgpago("tipo", e.RowIndex + 1).Value = "1.-Efectivo"
                dgpago.CurrentCell = dgpago("cantidad", e.RowIndex + 1)
                restasub()
            End If

        End If
        If e.ColumnIndex = dgpago.Columns("Del").Index And dgpago.Rows.Count > 1 Then 'verifica que haya hecho click en borrar y que ademas que la fila que se intenta borrar no se la última, de lo contrario el usuario no podria agregar más
            dgpago.Rows.RemoveAt(e.RowIndex)
            restasub()

        End If
    End Sub

    Private Sub dgpago_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgpago.EditingControlShowing
        If dgpago.Columns("cantidad").Index = dgpago.CurrentCell.ColumnIndex Then

            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf numpointevalper
        End If


    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub dgpago_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgpago.CellLeave

    End Sub

    Private Sub dgpago_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgpago.CellMouseEnter

    End Sub

    Private Sub dgpago_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgpago.CellMouseUp

    End Sub

    Private Sub dgpago_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgpago.CellEndEdit
        If e.ColumnIndex = dgpago("cantidad", e.RowIndex).ColumnIndex Then
            If Val(dgpago("cantidad", e.RowIndex).Value) = 0 Then
                dgpago("cantidad", e.RowIndex).Value = 0
                restasub()

            End If

            dgpago("cantidad", e.RowIndex).Value = returnpercent(dgpago("cantidad", e.RowIndex).Value, total, True)
            restasub()

        End If

    End Sub
    Private Sub restasub()
        resta = total
        pago = 0
        ntventa.pago.total = total
        ntventa.pago.loadfromdg(dgpago)
        Dim rt As Decimal
        For i = 0 To dgpago.Rows.Count - 1
            rt = Math.Round(Val(dgpago("cantidad", i).Value), 2)
            pago += rt
            resta = resta - rt

        Next
        If resta < 0 Then
            cambio = -resta
            resta = 0
            Label5.Text = "Cambio:$ " & cambio
        ElseIf resta = 0 Then
            cambio = 0
            resta = 0
            Label5.Text = "Resta:$ " & resta
        Else
            cambio = 0
            Label5.Text = "Falta:$ " & resta
        End If
        ErrorProvider1.SetError(Aceptar, "")
        Aceptar.Enabled = True
        ErrorProvider1.SetError(bticket, "")
        bticket.Enabled = True
        ntventa.pago.loadfromdg(dgpago, False, total)

        If allowanypersonadedudo Then
            Exit Sub

        End If
        If resta > 0 And statepago = 0 Then
            If clnt.credito = 0 And Not allowadeudos And Not allowanyadeudo Then
                ErrorProvider1.SetError(Aceptar, "No se permiten adeudos a este cliente")
                Aceptar.Enabled = False
                ErrorProvider1.SetError(bticket, "No se permiten adeudos a este cliente")
                bticket.Enabled = False

            Else
                If resta > clnt.credito And Not allowanyadeudo Then
                    ErrorProvider1.SetError(Aceptar, "El adeudo es mayor al permitido")
                    Aceptar.Enabled = False
                    ErrorProvider1.SetError(bticket, "El adeudo es mayor al permitido")
                    bticket.Enabled = False
                Else

                End If
            End If
        End If


    End Sub

    Private Sub Aceptar_Click(sender As Object, e As EventArgs) Handles Aceptar.Click
        restasub()

        If printed = False Then

        End If
        If ntventa.cotizacion Then
            Me.DialogResult = DialogResult.Cancel

        Else
            Me.DialogResult = DialogResult.OK

        End If




    End Sub
    Public Sub addart(ByVal cant As Integer, ByVal des As String)
        dgl.Rows.Add()
        Dim rindex As Integer = dgl.Rows.Count - 1
        dgl("can", rindex).Value = cant
        dgl("des", rindex).Value = des

    End Sub
    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click


    End Sub

    Private Sub dgl_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgl.CellContentClick

    End Sub

    Private Sub asc_Click(sender As Object, e As EventArgs) Handles asc.Click
        If asc.Text = "Ascendente" Then
            loadprint(ntventa.articulos, True)
            asc.Tag = "Descendente"
            asc.Text = "Descendente"
            asc.BackgroundImage = SICDIN30.My.Resources.Resources.arrowdown_flech_1539
        Else
            loadprint(ntventa.articulos)
            asc.Text = "Ascendente"
            asc.Tag = "Ascendente"
            asc.BackgroundImage = SICDIN30.My.Resources.Resources.uparrow_arriba_1538
        End If
    End Sub
    Public Sub addventapago()
        ''  restasub()
        '' addventa(folio, dgprint, clnt, Today.ToShortDateString, Now.ToShortTimeString, user.idnameusu, pagod)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles bticket.Click
        If printed = False Then
            dialogs.Close()

            If dialogs.pregunta("¿Desea imprimir el ticket?" & vbNewLine & "Despues de esto solo podra finalizar la venta") = vbNo Then
                Exit Sub
            End If
        End If
        If printd.ShowDialog = vbOK Then
            printed = True
            Cancel.Enabled = False
            dgpago.EditMode = DataGridViewEditMode.EditProgrammatically
            dgpago.Columns.Item("agregar").Visible = False
            dgpago.Columns.Item("Del").Visible = False
            'addventapago()
            Try
                Dim ticketp As New PaperSize("Ticket", 220, 1400)
                printticket.PrinterSettings = printd.PrinterSettings
                printticket.DefaultPageSettings.PaperSize = ticketp
                printticket.Print()
            Catch ex As Exception
                LogAppError(ex, "pagof.printticket.Print")
                MessageBox.Show("No se pudo imprimir el ticket:" & vbCrLf & ex.Message & vbCrLf & vbCrLf & "Verifique que la impresora esté encendida y conectada.", "Aviso de Impresión", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If

    End Sub

    Private Sub printticket_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles printticket.PrintPage
        Dim f As Font = New Font("Arial", 8)
        Dim fart As Font = New Font("Arial", 7)
        Dim fcode As Font = New Font("Free 3 of 9 Extended", 16)
        Dim ft As Font = New Font("Forte", 16, FontStyle.Bold)
        Dim xl As Integer = 10
        Dim yl As Integer = 10
        Dim p As Point = New Point(xl, yl)
        Dim b As Brush = Brushes.Black
        Dim ffar As New StringFormat(StringFormatFlags.NoClip)
        Dim fcenter As New StringFormat(StringFormatFlags.NoClip)
        ffar.Alignment = StringAlignment.Far
        fcenter.Alignment = StringAlignment.Center
        clientest = ntventa.clnt.Nombre
        ''If tienda = "Casa Licona" Then
        '' e.Graphics.DrawImage(SICDIN30.My.Resources.Resources.casa, p.X, p.Y, 50, 50)
        ''Else
        '' e.Graphics.DrawImage(SICDIN30.My.Resources.Resources.allys, p.X, p.Y, 50, 50)
        ''End If
        p.X += 50
        e.Graphics.DrawString(tienda.nombre, ft, b, p)
        p.Y += 50 + 10

        Dim st As String
        Dim precio As String
        Dim cantidad As String
        Dim cantl As Integer = 0
        Dim sty As Integer = e.Graphics.MeasureString("A", f).Height
        Dim sty2 As Integer = e.Graphics.MeasureString("A", fart).Height
        folio = ntventa.folio
        p.X = xl - 5  ''agrega folio y fecha
        e.Graphics.DrawString("Fecha:" & Today.ToShortDateString, f, b, p)
        p.Y += sty
        e.Graphics.DrawString("Folio:" & folio, f, b, p)
        p.Y += sty
        e.Graphics.DrawString("*" & folio & "*", fcode, b, p)
        p.Y += e.Graphics.MeasureString("**", fcode).Height

        ''agrega clnt

        Dim impl As Integer = 0
        Dim clients As SizeF = e.Graphics.MeasureString("Cliente:", f)
        Dim nclientemax As Integer = e.PageBounds.Width - xl - 2 * xl - clients.Width
        Dim stcliente As String = stprintadapter(clientest, nclientemax, f, e.Graphics, 0)
        e.Graphics.DrawString("Cliente:", f, b, p)
        p.X += clients.Width
        e.Graphics.DrawString(stcliente, f, b, p)
        p.Y += e.Graphics.MeasureString(stcliente, f).Height + sty
        p.X = xl
        With ntventa.articulos

            For i = 1 To ntventa.articulos.articulo.Count - 1
                If e.Graphics.MeasureString(.articulo(i).cantidad.ToString, f).Width > cantl Then
                    cantl = e.Graphics.MeasureString(.articulo(i).cantidad.ToString, f).Width
                End If
                If e.Graphics.MeasureString(.articulo(i).importe.ToString, f).Width > impl Then
                    impl = e.Graphics.MeasureString(.articulo(i).importe.ToString, f).Width
                End If
            Next
            cantl += xl - 5
            Dim ncan As Integer = e.Graphics.MeasureString("Cant|", f).Width + xl - 5
            If cantl < ncan Then
                cantl = ncan
            End If
            ncan = e.Graphics.MeasureString("|Importe", f).Width
            If impl < ncan Then
                impl = ncan
            End If
            impl = e.PageBounds.Width - 3 * xl - impl
            Dim desl As Integer = impl - cantl
            p.X = cantl
            e.Graphics.DrawString("Cant|", f, b, p, ffar)
            e.Graphics.DrawString("Descripción", f, b, p)
            p.X = impl - 5
            e.Graphics.DrawString("|Importe", f, b, p)
            p.Y += sty
            Dim imptotal As Decimal = 0
            For i = 1 To .articulo.Count - 1 ''impresion de articulos
                precio = Format(.articulo(i).precio, "$0.00")
                imptotal += Val(.articulo(i).importe)
                cantidad = .articulo(i).cantidad
                p.X = cantl
                e.Graphics.DrawString(cantidad & "|", fart, b, p, ffar)
                p.X = impl - 5
                e.Graphics.DrawString("|" & precio, fart, b, p)
                p.X = cantl
                st = stprintadapter(.articulo(i).descripcion, desl, fart, e.Graphics, 0)
                e.Graphics.DrawString(st, fart, b, p)
                p.Y += e.Graphics.MeasureString(st, fart).Height
            Next ''anexo pagos

            p.X = e.PageBounds.Width - 3 * xl
            p.Y += 2 * sty
            Dim subt As Decimal = Math.Round(imptotal - ivades(imptotal), 2)
            Dim iva As Decimal = ivades(imptotal)
            e.Graphics.DrawString("SubTotal: " & fpesos(subt), f, b, p, ffar)
            p.Y += sty
            e.Graphics.DrawString("I.V.A.: " & fpesos(iva), f, b, p, ffar)
            p.Y += sty
            If adeudo > 0 Then
                e.Graphics.DrawString("Adeudo: " & fpesos(adeudo), f, b, p, ffar)
                p.Y += sty
            End If

            e.Graphics.DrawString("Total: " & fpesos(total), f, b, p, ffar)
            p.Y += sty
            If resta > 0 Then
                Dim pagost As Decimal = total - resta
                e.Graphics.DrawString("Pago: " & fpesos(pagost), f, b, p, ffar)
                p.Y += sty
                e.Graphics.DrawString("Resta: " & fpesos(resta), f, b, p, ffar)
                p.Y += sty
            End If
            p.X = e.PageBounds.Width / 2
            e.Graphics.DrawString("Comprobante sin validez fiscal", f, b, p, fcenter)
            p.Y += sty
            e.Graphics.DrawString("Agradecemos su preferencia", f, b, p, fcenter)
            p.Y += sty
        End With

        e.HasMorePages = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles bnota.Click
        restasub()

        imprimirventa(ntventa, subtotal, ivaon, pago, resta)
    End Sub

    Private Sub asc_MouseEnter(sender As Object, e As EventArgs) Handles asc.MouseEnter, Aceptar.MouseEnter, Cancel.MouseEnter, bticket.MouseEnter, bnota.MouseEnter, iva.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub asc_MouseLeave(sender As Object, e As EventArgs) Handles asc.MouseLeave, Aceptar.MouseLeave, Cancel.MouseLeave, bticket.MouseLeave, bnota.MouseLeave, iva.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub iva_Click(sender As Object, e As EventArgs) Handles iva.Click
        If ivaon Then
            ivaon = False
            iva.Tag = "IVA"
            iva.Text = "IVA"
            iva.BackgroundImage = SICDIN30.My.Resources.ivono
        Else
            ivaon = True
            iva.Tag = "No IVA"
            iva.Text = "No IVA"
            iva.BackgroundImage = SICDIN30.My.Resources.iva
        End If
    End Sub
    Private Sub TakeScreenShot(ByVal Control As Control)
        Dim tmpImg As New Bitmap(Control.Width, Control.Height)
        Using g As Graphics = Graphics.FromImage(tmpImg)


        End Using
        Clipboard.SetImage(tmpImg)
    End Sub
    Private Sub imagen_Click(sender As Object, e As EventArgs) Handles imagen.Click
        ntventa.pago.pagado = pago
        ntventa.pago.resta = resta
        restasub()

        printencabezadobmp(ntventa)
        If clnt.telcel <> "" Then ''Envia imagen a whats
            sendtowhats(clnt.telcel)
            Threading.Thread.Sleep(1000)
            SendKeys.Send("^v")
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim pguia As pagos =superdesc.loader(total)
        dgpago.Rows.Clear()
        dgpago.Rows.Add()
        Dim i As Integer = 0
        If pguia.efectivo > 0 Then
            dgpago("cantidad", i).Value = pguia.efectivo
            dgpago("Tipo", i).Value = "1.-Efectivo"
            dgpago.Rows.Add()
            i = +1
        End If
        If pguia.tarjeta > 0 Then
            dgpago("cantidad", i).Value = pguia.tarjeta
            dgpago("Tipo", i).Value = "2.-Tarjeta"
            dgpago.Rows.Add()
            i = +1
        End If

        If pguia.transferencia > 0 Then
            dgpago("cantidad", i).Value = pguia.transferencia
            dgpago("Tipo", i).Value = "3.-Transferencia"
            dgpago.Rows.Add()
            i = +1
        End If
        If pguia.descuento > 0 Then
            dgpago("cantidad", i).Value = pguia.descuento
            dgpago("Tipo", i).Value = "4.-Descuento"
        End If
        restasub()

    End Sub
End Class