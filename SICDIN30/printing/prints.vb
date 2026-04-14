Imports System.Windows.Forms.VisualStyles

Module prints
    Dim p2 As PictureBox
    Dim py As Integer = 0
    Dim booleancalp As Boolean = False
    Dim ntventa As notaventa
    Dim leyendaaclaracion1 As String = "Sin validez fiscal. Para cualquier cambio o aclaración favor de presentar esta nota en un lapso no mayor a"
    Dim leyendaaclaracion2 As String = "30 días"
    Dim leyendaaclaracion3 As String = "desde su emisión"
    ''Dim leyendaaclaracion4 As String = ""
    Dim leyendaaclaracion5 As String = "Sin validez fiscal. Para cualquier cambio o aclaración favor de presentar esta orden en un lapso no mayor a"
    Dim leyendaaclaracion6 As String = ""
    Dim leyendaaclaracion7 As String = "No nos hacemos responsables por el deterioro de su bicicleta después de 3 días terminado el trabajo."
    Dim leyendaaclaracion8 As String = "A partir del 3er día de terminado el trabajo causara renta de $10.00 diarios. Rematandose a los 90 díasRematandose a los 90 días"
    Dim clnt As Cliente
    Dim ncart As Integer ''articulos en el dg
    Dim nini As Integer = 1 ''articulos de inicio
    Dim nfin As Integer ''articulos de fin
    Dim nbici As Integer = 0 ''bici actual
    Dim folio As String
    Dim pagac As Integer = 1
    Dim px As Integer = 0
    Dim resta As Decimal = 0
    Dim pago As Decimal = 0
    Dim slp As Point
    Dim pxlim As Integer
    Dim totalst As String = ""
    Dim pyof As Integer = 35
    Dim pxof As Integer = 60
    Dim total As Decimal = 0
    Dim iva As Decimal = 0
    Dim subtotal As Decimal = 0
    Dim ivaon As Boolean = False
    Dim pxlimof As Integer = 60 ''100
    Dim rfcv As Boolean
    Dim dirv As Boolean
    Dim telv As Boolean
    Dim hst1 As Integer = 0
    Dim fp As Font = New Font("Arial", 9)
    Dim fpbic As Font = New Font("Arial", 12)
    Dim fpb As Font = New Font("Arial", 9, FontStyle.Bold)
    Dim fpeque As Font = New Font("Arial", 7)
    Dim fpequebold As Font = New Font("Arial", 7, FontStyle.Bold)
    Dim fpequeapartado As Font = New Font("Arial", 5, FontStyle.Bold)
    Dim fderecha As StringFormat = New StringFormat
    Dim fmid As StringFormat = New StringFormat
    Dim pyapartadolastp As Integer = 0
    Dim tam As SizeF
    Dim npag As Integer = 1 ''numero de paginas
    Dim lp As Point = New Point(0, 0)
    Public printv As PrintDialog = New PrintDialog
    Public p1 As System.Drawing.Printing.PrintDocument = New System.Drawing.Printing.PrintDocument()
    Dim repbicprint As repbic
    Dim artv As dgventa
    Dim telefono As String
    Dim nlinearep As Integer = 0
    Dim recogeper As String
    Dim parent As String
    Dim owner As Boolean
    Dim empty As Boolean = False
    Public Sub printleyendasapartado(ByRef e As Printing.PrintPageEventArgs)
        Dim pxmitad As Integer = pxlim / 2
        px = pxmitad
        Dim tamp As SizeF = e.Graphics.MeasureString("W", fpequeapartado)
        upy(pyapartadolastp)
        pyapartadolastp = py
        e.Graphics.DrawString("Políticas de apartado:", fpequeapartado, Brushes.Black, lp)
        upy(tamp.Height, True)
        e.Graphics.DrawString("1) Los artículos listados en esta nota serán almacenados hasta el día de su entrega en bodega.", fpequeapartado, Brushes.Black, lp)
        upy(tamp.Height, True)
        e.Graphics.DrawString("2) El cliente se compromete a liquidar los artículos listados antes del día de entrega, en 4 abonos recurrentes", fpequeapartado, Brushes.Black, lp)
        upy(tamp.Height, True)
        e.Graphics.DrawString("acordados con el vendedor. En caso de no cumplir con esto, esta nota de apartado quedara cancelada.", fpequeapartado, Brushes.Black, lp)
        upy(tamp.Height, True)
        e.Graphics.DrawString("3) El cliente puede liquidar y retirar el artículo antes de la fecha de entrega, para lo cual tendrá que avisar a la", fpequeapartado, Brushes.Black, lp)
        upy(tamp.Height, True)
        e.Graphics.DrawString("tienda un día hábil antes, en horario laboral.", fpequeapartado, Brushes.Black, lp)
        upy(tamp.Height, True)
        e.Graphics.DrawString("4) El cliente puede cancelar el apartado en cualquier momento, para lo cual tendrá que proveer su copia de esta", fpequeapartado, Brushes.Black, lp)
        upy(tamp.Height, True)
        e.Graphics.DrawString("nota de apartado a la tienda.", fpequeapartado, Brushes.Black, lp)
        upy(tamp.Height, True)
        e.Graphics.DrawString("5) La tienda podrá cancelar el apartado en cualquier momento, previo aviso al cliente.", fpequeapartado, Brushes.Black, lp)
        upy(tamp.Height, True)
        e.Graphics.DrawString("6) En caso de cancelación por políticas 2) y 4), la tienda retendrá un 20% del costo total sobre lo abonado de los", fpequeapartado, Brushes.Black, lp)
        upy(tamp.Height, True)
        e.Graphics.DrawString(" artículos para cubrir costos administrativos y de almacenaje. El resto de lo abonado podrá ser utilizado por el ", fpequeapartado, Brushes.Black, lp)
        upy(tamp.Height, True)
        e.Graphics.DrawString("cliente en artículos o servicios dentro de la tienda. ", fpequeapartado, Brushes.Black, lp)
        upy(tamp.Height, True)


    End Sub
    Public Sub printpagosapartado(ByRef e As Printing.PrintPageEventArgs)
        Dim pxmitad As Integer = pxlim / 2 - 5
        Dim px14 As Integer = pxlim * 1 / 4
        px = pxlimof
        e.Graphics.DrawLine(Pens.Black, pxlimof, py, pxlimof, py)
        upy(tam.Height, True)
        e.Graphics.DrawString("Fecha de entrega: " & ntventa.fechae.ToShortDateString, fpb, Brushes.Black, lp)
        pyapartadolastp = py

        upy(tam.Height, True)
        e.Graphics.DrawLine(Pens.Black, pxlimof, py, pxmitad, py)
        e.Graphics.DrawString("Pago", fpb, Brushes.Black, lp)

        upx(px14)
        e.Graphics.DrawString("Fecha", fpb, Brushes.Black, lp)
        upy(tam.Height, True)
        e.Graphics.DrawLine(Pens.Black, pxlimof, py, pxmitad, py)
        upx(pxlimof)
        e.Graphics.DrawString("$" & ntventa.pago.pagado, fpb, Brushes.Black, lp)
        upx(px14)
        e.Graphics.DrawString(Now.ToShortDateString, fpb, Brushes.Black, lp)
        upx(pxlimof)
        For i = 0 To 3
            upy(tam.Height, True)
            e.Graphics.DrawLine(Pens.Black, pxlimof, py, pxmitad, py)
        Next


    End Sub
    Public Sub calculatepagesrep() ''Calcula el número de paginas en reparación

        npag = 0
        Dim lineas As Integer = 0
        Dim i As Integer
        Dim npage As Boolean = False

        For i = 1 To repbicprint.bici.Count - 1
            npag += 1
            lineas = 0
            Dim dartv = New dgventa
            dartv = repbicprint.bici(i).dg
            npage = False
            For k = 1 To repbicprint.bici(i).dg.articulo.Count - 2
                If npage Then
                    npag += 1
                    npage = False
                End If

                lineas += 1

                If dartv.articulo(k).descripcion.Length > 22 Then
                    lineas += 1
                End If
                If lineas = nart Then
                    lineas = 0
                    npage = True
                ElseIf lineas > nart Then
                    lineas -= nart
                    npage = True
                End If
            Next

        Next
        pagac = 1

    End Sub
    Public Sub printencabezadobmp(nota As notaventa)
        ''imprime en bmp

        selectitiendaprint()
        pagnota.dir1.Location = New Point(pagnota.en1.Location.X - 30, pagnota.dir1.Location.Y)

        clnt = nota.clnt
        folio = nota.folio
        fderecha.Alignment = StringAlignment.Far

        pago = nota.pago.pagado
        resta = nota.pago.resta

        Dim adeudo As Decimal = nota.clnt.adeudo

        pagnota.f1.Text = "Pachuca de Soto,Hgo. a " & " " & nota.fecha.Date.Day & " de " & MonthName(nota.fecha.Date.Month) & " del " & nota.fecha.Date.Year
        total = nota.total
        totalst = num2str(nota.total)
        subtotal = nota.subtotal

        Dim bmpancho As Integer = 750
        Dim lineas As Integer = 0
        For i = 0 To nota.articulos.articulo.Count - 1
            If nota.articulos.articulo(i).descripcion.Length > 33 Then
                lineas += 1
            End If
        Next

        Dim ialto As Integer = 330 + (15 * (nota.articulos.articulo.Count + lineas))
        If ialto < 360 Then
            ialto = 360
        End If
        Using bmp As New Bitmap(bmpancho + 25, ialto)
            Dim e As Graphics = Graphics.FromImage(bmp)
            e.Clear(Color.White)

            If nota.cotizacion Then
                pagnota.labelnota.Text = "Cotización"
            End If
            pagnota.cliente.Text = clnt.Nombre
            pagnota.rfc.Text = clnt.RFC
            pagnota.direccion.Text = clnt.Direccion
            If clnt.RFC = "" Then
                pagnota.rfc.Text = "N/A"
            End If
            If clnt.Direccion = "" Then
                pagnota.direccion.Text = "N/A"
            End If
            pagnota.correrol.Text = "Correo:" & clnt.mail
            pagnota.folio.Text = folio
            telefono = "N/A"
            If clnt.telcasa <> "" Then
                telefono = clnt.telcasa
            End If
            If clnt.telcel <> "" Then
                telefono = clnt.telcel
            End If
            If nota.cotizacion Then
                pagnota.folio.Text = ""
                pagnota.foliobar.Text = ""
            Else
                pagnota.folio.Text = folio
                If codeprint Then
                    pagnota.foliobar.Text = " * " & folio & " * "
                Else
                    pagnota.foliobar.Text = folio
                End If

            End If



            Dim rnrfc As Integer = 0
            Dim rndir As Integer = 0
            Dim p As Point
            Dim palto As Integer = 0
            pagnota.npag.Text = "Pag:" & pagac & "/" & npag
            Select Case idti
                Case 0, 1
                    e.DrawImage(SICDIN30.My.Resources.logocl, pxof + 30, pyof - 5, 85, 85)

                Case 2
                    e.DrawImage(SICDIN30.My.Resources.logoally, pxof, pyof + 10, 150, 50)

            End Select


            For Each st As Label In pagnota.encabezado.Controls
                p = st.Location
                p.Y = p.Y + pyof
                p.X = p.X + pxof
                Dim f As Font = st.Font
                If st.Enabled Then
                    Select Case st.Name
                        Case "folio"
                            If codeprint Then
                                e.DrawString(st.Text, f, Brushes.Black, p)
                            End If
                        Case "foliobar"
                            If codeprint Then
                                e.DrawString(st.Text, fcode, Brushes.Black, p)
                            Else
                                e.DrawString(st.Text, fpbic, Brushes.Black, p.X, p.Y - 20)
                            End If
                        Case "tellabel"

                            e.DrawString(st.Text & telefono, f, Brushes.Black, p)
                        Case "correol"
                            If pagnota.correrol.Text <> "" Then
                                e.DrawString(st.Text & clnt.mail, f, Brushes.Black, p)
                            Else
                                e.DrawString(st.Text & "N/A", f, Brushes.Black, p)
                            End If

                        Case Else
                            e.DrawString(st.Text, f, Brushes.Black, p)
                    End Select
                End If
                If p.Y > palto Then
                    palto = p.Y
                End If
            Next
            px = pxof
            py = pagnota.encabezado.Size.Height
            updatep()
            Select Case idti
                Case 0, 1

                    e.DrawImage(SICDIN30.My.Resources.logocltrans, pxof + 120, lp.Y - 50, 350, 350)
                Case 2
                    e.DrawImage(SICDIN30.My.Resources.allystrans, pxof + 20, lp.Y - 10, 650, 200)
            End Select


            px = pxof
            tam = e.MeasureString("W", fp)
            pxlim = bmpancho
            Dim pxuni As Integer = (pxlim - px) / 100

            py = py + pyof + 10
            updatep()

            'e.Graphics.DrawImage(SICDIN30.My.Resources.allystrans, pxof + 20, lp.Y - 10, 650, 200)
            slp = New Point(pxlim, py)
            e.DrawLine(Pens.Black, lp, slp)
            e.DrawString("Clave", fp, Brushes.Black, lp)
            px = px + (pxuni * 10)
            updatep()
            e.DrawString("Cant.", fp, Brushes.Black, lp)
            px = px + (pxuni * 10)
            updatep()
            e.DrawString("Descripción.", fp, Brushes.Black, lp)
            px = px + (pxuni * 40)
            updatep()
            e.DrawString("Paquete.", fp, Brushes.Black, lp)
            px = px + (pxuni * 20)
            updatep()
            e.DrawString("Importe", fp, Brushes.Black, lp, fderecha)
            px = pxlim
            updatep()
            e.DrawString("Precio", fp, Brushes.Black, lp, fderecha)
            upx(pxof, False)
            upy(tam.Height, True)
            slp = New Point(pxlim, py)
            e.DrawLine(Pens.Black, lp, slp)
            artv = nota.articulos
            leyendaaclaracion6 = ""
            Dim saltos As Integer = 0
            Dim i As Integer = 0
            For i = 0 To nota.articulos.articulo.Count - 1
                If artv.articulo(i).descripcion <> "" Then
                    upx(pxof, False)
                    e.DrawString(artv.articulo(i).clave, fp, Brushes.Black, lp)
                    px = px + (pxuni * 10)
                    updatep()
                    e.DrawString(artv.articulo(i).cantidad, fp, Brushes.Black, lp)
                    px = px + (pxuni * 10)
                    updatep()
                    Dim stdes As String = artv.articulo(i).descripcion
                    stdes = stprintadapter(stdes, tam.Width * 15, fp, e, saltos)

                    ''  stdes = shortenstring(tam.Width * 15, stdes, fp, e) ''12
                    If artv.articulo(i).notas <> "" Then
                        stdes &= " * "
                        If Not leyendaaclaracion6.Contains(artv.articulo(i).notas) Then
                            If leyendaaclaracion6 = "" Then
                                leyendaaclaracion6 = "*"
                            End If
                            leyendaaclaracion6 &= artv.articulo(i).notas & vbNewLine
                        End If
                    End If
                    e.DrawString(stdes, fp, Brushes.Black, lp)
                    px = px + (pxuni * 40)
                    updatep()
                    e.DrawString(artv.articulo(i).pq, fp, Brushes.Black, lp)
                    px = px + (pxuni * 20)
                    updatep()


                    If ivaon Then
                        e.DrawString("$" & adapterprint(artv.articulo(i).precio - ivades(artv.articulo(i).precio).ToString), fp, Brushes.Black, lp, fderecha)
                        px = pxlim
                        updatep()
                        e.DrawString("$" & adapterprint(artv.articulo(i).importe - ivades(artv.articulo(i).importe)), fp, Brushes.Black, lp, fderecha)
                    Else
                        e.DrawString("$" & adapterprint(artv.articulo(i).precio), fp, Brushes.Black, lp, fderecha)
                        px = pxlim
                        updatep()
                        e.DrawString("$" & adapterprint(artv.articulo(i).importe), fp, Brushes.Black, lp, fderecha)
                    End If

                    upx(pxof, False)
                    upy((saltos + 1) * tam.Height, True)



                End If
            Next

            upy(-tam.Height, True)

            upy(tam.Height, True)
            px = pxof
            updatep()
            slp = New Point(pxlim, py)
            e.DrawLine(Pens.Black, lp, slp)
            upx(pxof)
            e.DrawString("Total con letra:", fpb, Brushes.Black, lp)
            upy(tam.Height, True)
            e.DrawString(totalst, fp, Brushes.Black, lp)
            upy(tam.Height, True)
            e.DrawString(leyendaaclaracion1, fpeque, Brushes.Black, lp)
            upx(e.MeasureString(leyendaaclaracion1, fpeque).Width + 50)
            e.DrawString(leyendaaclaracion2, fpequebold, Brushes.Black, lp)
            upx(e.MeasureString(leyendaaclaracion2, fpequebold).Width, True)
            e.DrawString(leyendaaclaracion3, fpeque, Brushes.Black, lp)
            upy(tam.Height, True)
            upx(pxof)
            ''e.DrawString(leyendaaclaracion4, fpeque, Brushes.Black, lp)
            If leyendaaclaracion6 <> "" Then
                upx(pxof)
                upy(tam.Height, True)
                e.DrawString(leyendaaclaracion6, fpeque, Brushes.Black, lp)
            End If
            upy(slp.Y)

            upx(pxlim)

            If clnt.adeudo > 0 And Not nota.pagoadeudo Then
                e.DrawString("Adeudo:" & fpesos(clnt.adeudo), fp, Brushes.Black, lp, fderecha)
                upy(tam.Height, True)
            End If
            If ivaon Then
                e.DrawString("Subtotal:" & fpesos(subtotal - iva), fp, Brushes.Black, lp, fderecha)
                upy(tam.Height, True)
                e.DrawString("I.V.A.:" & fpesos(iva), fp, Brushes.Black, lp, fderecha)
            Else
                If Not nota.pagoadeudo Then
                    e.DrawString("Subtotal:" & fpesos(subtotal), fp, Brushes.Black, lp, fderecha)
                End If
            End If
            ''
            If nota.pago.descuento > 0 Then
                upy(tam.Height, True)
                e.DrawString("Descuento:-" & fpesos(nota.pago.descuento), fp, Brushes.Black, lp, fderecha)
            End If
            upy(tam.Height, True)

            e.DrawString("Total:" & fpesos(total - nota.pago.descuento), fp, Brushes.Black, lp, fderecha)

            If Not nota.apartado And Not nota.cotizacion Then
                If pago > 0 Then
                    If nota.pago.efectivo > 0 Then
                        upy(tam.Height, True)
                        e.DrawString("Efectivo:" & fpesos(nota.pago.efectivo), fp, Brushes.Black, lp, fderecha)
                    End If
                    If nota.pago.tarjeta > 0 Then
                        upy(tam.Height, True)
                        e.DrawString("Tarjetas:" & fpesos(nota.pago.tarjeta), fp, Brushes.Black, lp, fderecha)
                    End If
                    If nota.pago.transferencia > 0 Then
                        upy(tam.Height, True)
                        e.DrawString("Transferencia:" & fpesos(nota.pago.transferencia), fp, Brushes.Black, lp, fderecha)
                    End If
                End If


            End If

            ''
            If resta > 0 Then
                upy(tam.Height, True)
                e.DrawString("Resta:" & fpesos(resta), fp, Brushes.Black, lp, fderecha)
            End If
            Clipboard.SetImage(bmp)
        End Using
    End Sub
    Private Sub printpagos(ByRef e As Printing.PrintPageEventArgs)
        totalst = num2str(repbicprint.total)
        upy(tam.Height, True)
        px = pxof
        updatep()
        slp = New Point(pxlim, py)
        e.Graphics.DrawLine(Pens.Black, lp, slp)
        upx(pxlimof)
        Dim posleyy As Integer = 0
        If repbicprint.bici.Count > 1 Then
            e.Graphics.DrawString("Total con letra de nota", fpb, Brushes.Black, lp)
            upx(pxlim)
            e.Graphics.DrawString("Total de bicicleta:" & fpesos(repbicprint.bici(nbici).total.ToString), fpb, Brushes.Black, lp, fderecha)
            upy(tam.Height, True)
            upx(pxlimof)
            e.Graphics.DrawString(totalst, fpb, Brushes.Black, lp)
            posleyy = lp.Y + tam.Height
            upx(pxlim)
            ' e.Graphics.DrawString("Total de nota:" & fpesos(repbicprint.total.ToString), fpb, Brushes.Black, lp, fderecha)
            upx(pxlim)
            upy(tam.Height, True)
            e.Graphics.DrawString("A cuenta:" & fpesos(repbicprint.tpagado), fpb, Brushes.Black, lp, fderecha)
        Else
            e.Graphics.DrawString("Total con letra", fpb, Brushes.Black, lp)
            upx(pxlim)
            e.Graphics.DrawString("Total:" & fpesos(repbicprint.total.ToString), fpb, Brushes.Black, lp, fderecha)
            upx(pxlimof)
            upy(tam.Height, True)
            e.Graphics.DrawString(totalst, fpb, Brushes.Black, lp)
            posleyy = lp.Y + tam.Height
            upx(pxlim)
            e.Graphics.DrawString("A cuenta:" & fpesos(repbicprint.tpagado), fpb, Brushes.Black, lp, fderecha)
        End If


        upx(pxof)
        upy(posleyy, False)
        e.Graphics.DrawString(leyendaaclaracion5, fpeque, Brushes.Black, lp)
        upx(e.Graphics.MeasureString(leyendaaclaracion5, fpeque).Width + 50)
        e.Graphics.DrawString(leyendaaclaracion2, fpequebold, Brushes.Black, lp)
        upx(e.Graphics.MeasureString(leyendaaclaracion2, fpequebold).Width, True)
        e.Graphics.DrawString(leyendaaclaracion3, fpeque, Brushes.Black, lp)
        ''  upy(tam.Height, True)
        upx(pxof)
        ''  e.Graphics.DrawString(leyendaaclaracion4, fpeque, Brushes.Black, lp)
        If leyendaaclaracion6 <> "" Then
            upx(pxof)
            upy(tam.Height, True)
            e.Graphics.DrawString(leyendaaclaracion6, fpeque, Brushes.Black, lp)
        End If
        upy(tam.Height, True)
        upx(pxof)
        e.Graphics.DrawString(leyendaaclaracion7, fpeque, Brushes.Black, lp)
        upy(tam.Height, True)
        upx(pxof)
        e.Graphics.DrawString(leyendaaclaracion8, fpeque, Brushes.Black, lp)


    End Sub
    Public Sub imprimirrep(repara As repbic)
        repbicprint = New repbic
        selectitiendaprint()
        repbicprint = repara
        clnt = repara.clnt
        loadfromcln(repara.clnt)

        folio = repbicprint.folio
        pagnota.labelnota.Text = "Reparación"

        telefono = "N/A"
        If clnt.telcasa <> "" Then
            telefono = clnt.telcasa
        End If
        If clnt.telcel <> "" Then
            telefono = clnt.telcel
        End If
        printv.PrinterSettings.Copies = 2
        If Not printv.ShowDialog = vbOK Then
            Exit Sub
        End If
        p1.PrinterSettings = printv.PrinterSettings
        'hst1 = pagnota.dir2.Height
        pagac = 1
        nbici = 1
        nfin = 0
        AddHandler p1.PrintPage, AddressOf imprimirrepara
        fderecha.Alignment = StringAlignment.Far

        leyendaaclaracion6 = ""
        printdate()
        calculatepagesrep() ''calcula las paginas para la reparación
        Try
            nini = nfin
            nfin = nrep + nini
            p1.Print()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        RemoveHandler p1.PrintPage, AddressOf imprimirrepara
    End Sub
    Private Sub printdate() ''imprime la fecha 
        Dim fechae As Date = Now
        Select Case idti
            Case 0, 1

                pagnota.f1.Text = "Pachuca de Soto, Hgo. a " & fechae.Date.Day & " de " & MonthName(fechae.Month) & " del " & fechae.Year
            Case 2
                pagnota.f1.Text = "Mineral de la reforma, Hgo. a " & fechae.Date.Day & " de " & MonthName(fechae.Month) & " del " & fechae.Year
        End Select
    End Sub
    Private Sub imprimirrepara(sender As Object, e As Printing.PrintPageEventArgs) ''Evento impresión reparación

        Try
            artv = repbicprint.bici(nbici).dg

        Catch ex As Exception
            Exit Sub
        End Try

        If nfin > repbicprint.bici(nbici).dg.articulo.Count - 1 Then
            nfin = repbicprint.bici(nbici).dg.articulo.Count - 1
        End If
        selectitiendaprint()
        printdate()
        printencabezado(e)
        printbicis(e, repbicprint)


        If nini >= repbicprint.bici(nbici).dg.articulo.Count - 1 Then
            printpagos(e)

            nini = 0
            nbici += 1

        End If
        pagac += 1
        If pagac > npag Then
            e.HasMorePages = False
            '      pagnota.Close()

        Else
            e.HasMorePages = True
        End If


    End Sub
    Private Sub printprueba(ByRef e As Printing.PrintPageEventArgs)
        Dim i As Integer
        tam = e.Graphics.MeasureString("W", fp)
        For i = 1 To 100
            upy(tam.Height, True)
            e.Graphics.DrawString(i, fp, Brushes.Black, lp)

        Next
    End Sub
    Private Sub printbicis(ByRef e As Printing.PrintPageEventArgs, ByRef rep As repbic)
        Dim pprimera As Boolean = True
        px = pxof
        Dim pxlim As Integer = e.PageBounds.Width - pxlimof
        Dim slp As Point = New Point(pxlim, py)
        tam = e.Graphics.MeasureString("W", fp)
        Dim pxuni As Integer = (pxlim - px) / 100
        py = py + pyof
        updatep()
        Dim sts As String = ""
        slp = New Point(pxlim, py)
        If pprimera = False Then
            upy(tam.Height * 2, True)
            slp = New Point(pxlim, py)
        Else
            pprimera = False
        End If
        e.Graphics.DrawLine(Pens.Black, lp, slp)
        With rep.bici(nbici)
            If rep.bici.Count > 2 Then
                sts = "Bicicleta " & .id & " de " & rep.bici.Count & "   "
            Else
                sts = "Bicicleta "
            End If
            If .rodada <> "" Then
                sts &= "Rodada:" & .rodada
            End If
            If .marca <> "" Then
                sts &= "   Marca: " & .marca
            End If
            If .color <> "" Then
                sts &= "  Color: " & .color
            End If
            e.Graphics.DrawString(sts, fpbic, Brushes.Black, lp)
            upy(tam.Height, True)
            upx(pxof)
            sts = "Fecha de entrega: " & .fentrega.ToShortDateString & "  Hora entrega:" & .fentrega.ToString("hh:mm tt")
            e.Graphics.DrawString(sts, fpbic, Brushes.Black, lp)
            upy(tam.Height * 0.5, True)
            upx(pxof)

            If .Nota <> "" Then
                upy(tam.Height * 0.5, True)
                sts = "Nota :" & .Nota
                e.Graphics.DrawString(sts, fpbic, Brushes.Black, lp)
                upy(tam.Height, True)
                upx(pxof)
            End If
            If pagac > 1 Then
                upx(pxof)
                e.Graphics.DrawString("<--- Continuación pagina " & pagac - 1, fp, Brushes.Black, lp.X, lp.Y + 10)
                upy(tam.Height, True)

            End If
            upy(-2 * tam.Height, True)

            nini = printdg(e, nini, nfin)


            If nini < .dg.articulo.Count - 1 Then
                upx(pxlim)
                upy(tam.Height, True)
                e.Graphics.DrawString("Continua pagina " & pagac + 1 & " --->", fp, Brushes.Black, lp, fderecha)
                slp = New Point(pxlim, py)
                Dim asr As Point = New Point(pxof, py)
                e.Graphics.DrawLine(Pens.Black, asr, slp)
            End If
        End With

    End Sub

    Private Sub selecttienda()
        Select Case idti
            Case 0, 1
                ''pagnota.tiendalabel.Text = "''CASA LICONA''"
            Case 2
                '' pagnota.tiendalabel.Text = "''Ally's Bikes''"
        End Select
    End Sub
    Public Sub loadfromcln(ByVal clnt As Cliente)
        pagnota.cliente.Text = clnt.Nombre
        pagnota.rfc.Text = clnt.RFC
        pagnota.direccion.Text = clnt.Direccion
        If clnt.RFC = "" Then
            pagnota.rfc.Text = "N/A"
        End If
        If clnt.Direccion = "" Then
            pagnota.direccion.Text = "N/A"
        End If

    End Sub
    Public Function imprimirventa(ByRef nota As notaventa, ByVal subtotals As Double, ByVal ivaons As Boolean, ByVal pagos As Decimal, ByVal restas As Decimal, Optional emptyr As Boolean = False) ''Impresión de venta
        empty = emptyr

        artv = nota.articulos
        ntventa = nota
        ivaon = ivaons
        pago = pagos
        resta = restas
        imprimirventa = False
        clnt = nota.clnt
        folio = nota.folio
        iva = ivades(subtotals)
        subtotal = subtotals
        leyendaaclaracion6 = ""
        total = subtotals + clnt.adeudo
        totalst = num2str(total.ToString)
        If Not printv.ShowDialog = vbOK Then
            Exit Function
        End If
        If nota.cotizacion Then
            pagnota.labelnota.Text = "Cotización"
            nota.folio = ""
        Else
            Select Case nota.folio(1)
                Case "V"
                    pagnota.labelnota.Text = "Remisión"

                Case "A"
                    pagnota.labelnota.Text = "Apartado"

            End Select


        End If

        p1.PrinterSettings = printv.PrinterSettings
        hst1 = pagnota.dir2.Height
        AddHandler p1.PrintPage, AddressOf imprimire
        fderecha.Alignment = StringAlignment.Far
        Select Case idti
            Case 0, 1
                If Not empty Then
                    pagnota.f1.Text = "Pachuca de soto, Hgo. a " & nota.fecha.Day & " de " & MonthName(nota.fecha.Month) & " del " & nota.fecha.Year
                Else
                    pagnota.f1.Text = "Pachuca de soto, Hgo. a _____ de _____________ del 20__" & nota.fecha.Year

                End If
            Case 2
                If Not empty Then
                    pagnota.f1.Text = "Mineral de la reforma, Hgo. a " & nota.fecha.Day & " de " & MonthName(nota.fecha.Month) & " del " & nota.fecha.Year
                Else
                    pagnota.f1.Text = "Mineral de la reforma, Hgo. a _____ de _____________ del 20__" & nota.fecha.Year

                End If

        End Select

        nini = 0

        If artv.articulo.Count > nart Then
            nfin = nart
        Else
            nfin = artv.articulo.Count
        End If
        booleancalp = False
        p1.Print()
        imprimirventa = True
        RemoveHandler p1.PrintPage, AddressOf imprimire

    End Function

    Private Sub imprimire(sender As Object, e As Printing.PrintPageEventArgs)

        If booleancalp = False Then
            calculatepagesventa(e)
            booleancalp = True
        End If

        printencabezado(e)
        nini = printdg(e, nini, nfin)
        If ntventa.apartado Then
            printpagosapartado(e)
        End If

        If pagac = npag Then
            printfootventa(e)
            pagnota.Close()

            e.HasMorePages = False
        Else
            upx(pxlim)
            upy(tam.Height, True)
            e.Graphics.DrawString("Continua pagina " & pagac + 1 & " --->", fp, Brushes.Black, lp, fderecha)
            slp = New Point(pxlim, py)
            Dim asr As Point = New Point(pxof, py)
            e.Graphics.DrawLine(Pens.Black, asr, slp)
            e.HasMorePages = True
            pagac += 1


        End If
        If ntventa.apartado Then
            printleyendasapartado(e)
        End If
    End Sub
    Private Function printdg(ByRef e As Printing.PrintPageEventArgs, nini As Integer, nfin As Integer) As Integer
        px = pxof
        tam = e.Graphics.MeasureString("W", fp)
        pxlim = e.PageBounds.Width - pxlimof
        Dim pxuni As Integer = (pxlim - px) / 100
        Dim k As Integer = nini

        py = py + pyof + 10
        updatep()

        slp = New Point(pxlim, py)
        e.Graphics.DrawLine(Pens.Black, lp, slp)
        e.Graphics.DrawString("Clave", fp, Brushes.Black, lp)
        px = px + (pxuni * 10)
        updatep()
        e.Graphics.DrawString("Cant.", fp, Brushes.Black, lp)
        px = px + (pxuni * 10)
        updatep()
        e.Graphics.DrawString("Descripción.", fp, Brushes.Black, lp)
        px = px + (pxuni * 50)
        updatep()
        e.Graphics.DrawString("Paquete.", fp, Brushes.Black, lp)
        px = px + (pxuni * 20)
        updatep()
        e.Graphics.DrawString("Importe", fp, Brushes.Black, lp, fderecha)
        px = pxlim
        updatep()
        e.Graphics.DrawString("Precio", fp, Brushes.Black, lp, fderecha)
        upx(pxof, False)
        upy(tam.Height, True)
        slp = New Point(pxlim, py)
        e.Graphics.DrawLine(Pens.Black, lp, slp)

        Dim i As Integer = 0
        Dim naro As Integer = nfin - nini

        If naro < nart Then
            naro = nart - naro
        Else
            naro = 0
        End If

        Dim saltos As Integer = 0
        nfin = nini + nart

        If nfin > artv.articulo.Count Then
            nfin = artv.articulo.Count

        End If


        For i = 0 To nart - 1
            If k < nfin Then
                If artv.articulo(k).clave <> "" Then
                    upx(pxof, False)
                    e.Graphics.DrawString(artv.articulo(k).clave, fp, Brushes.Black, lp)
                    px = px + (pxuni * 10)
                    updatep()
                    e.Graphics.DrawString(artv.articulo(k).cantidad, fp, Brushes.Black, lp)
                    px = px + (pxuni * 10)
                    updatep()
                    Dim stdes As String = artv.articulo(k).descripcion
                    '' stdes = shortenstring(tam.Width * 15, stdes, fp, e) ''12
                    stdes = stprintadapter(stdes, tam.Width * 22, fp, e.Graphics, saltos)
                    If artv.articulo(k).notas <> "" Then
                        stdes &= " * "
                        If Not leyendaaclaracion6.Contains(artv.articulo(k).notas) Then
                            If leyendaaclaracion6 = "" Then
                                leyendaaclaracion6 = "*"
                            End If
                            leyendaaclaracion6 &= artv.articulo(k).notas & vbNewLine
                        End If
                    End If
                    e.Graphics.DrawString(stdes, fp, Brushes.Black, lp)
                    px = px + (pxuni * 50)
                    updatep()
                    e.Graphics.DrawString(artv.articulo(k).pq, fp, Brushes.Black, lp)
                    px = px + (pxuni * 20)
                    updatep()


                    If ivaon Then
                        e.Graphics.DrawString("$" & adapterprint(artv.articulo(k).precio - ivades(artv.articulo(i).precio).ToString), fp, Brushes.Black, lp, fderecha)
                        px = pxlim
                        updatep()
                        e.Graphics.DrawString("$" & adapterprint(artv.articulo(k).importe - ivades(artv.articulo(i).importe)), fp, Brushes.Black, lp, fderecha)
                    Else
                        e.Graphics.DrawString("$" & adapterprint(artv.articulo(k).precio), fp, Brushes.Black, lp, fderecha)
                        px = pxlim
                        updatep()
                        e.Graphics.DrawString("$" & adapterprint(artv.articulo(k).importe), fp, Brushes.Black, lp, fderecha)
                    End If

                    upx(pxof, False)
                    upy((saltos + 1) * tam.Height, True)
                    i += saltos
                End If
            Else
                upy(tam.Height, True)

            End If
            k += 1
        Next
        printdg = k
        upy(-tam.Height, True)

    End Function
    Public Sub printfootventa(ByRef e As Printing.PrintPageEventArgs)
        upy(tam.Height, True)
        px = pxof
        updatep()
        slp = New Point(pxlim, py)
        e.Graphics.DrawLine(Pens.Black, lp, slp)
        upx(pxof)
        e.Graphics.DrawString("Total con letra:", fpb, Brushes.Black, lp)
        upy(tam.Height, True)
        If Not empty Then
            totalst = num2str(total)
            e.Graphics.DrawString(totalst, fp, Brushes.Black, lp)
        End If

        upy(tam.Height, True)
        e.Graphics.DrawString(leyendaaclaracion1, fpeque, Brushes.Black, lp)
        upx(e.Graphics.MeasureString(leyendaaclaracion1, fpeque).Width + 50)
        e.Graphics.DrawString(leyendaaclaracion2, fpequebold, Brushes.Black, lp)
        upx(e.Graphics.MeasureString(leyendaaclaracion2, fpequebold).Width, True)
        e.Graphics.DrawString(leyendaaclaracion3, fpeque, Brushes.Black, lp)
        upy(tam.Height, True)
        upx(pxof)

        upy(slp.Y)
        upx(pxlim)
        If clnt.adeudo > 0 And ntventa.pagoadeudo = False Then
            e.Graphics.DrawString("Adeudo:" & fpesos(clnt.adeudo), fp, Brushes.Black, lp, fderecha)
            upy(tam.Height, True)
        End If
        If ivaon Then
            e.Graphics.DrawString("Subtotal:" & fpesos(subtotal - iva), fp, Brushes.Black, lp, fderecha)
            upy(tam.Height, True)
            e.Graphics.DrawString("I.V.A.:" & fpesos(iva), fp, Brushes.Black, lp, fderecha)
        Else
            If ntventa.pagoadeudo = False Then
                If Not empty Then
                    e.Graphics.DrawString("Subtotal:" & fpesos(subtotal), fp, Brushes.Black, lp, fderecha)

                Else
                    e.Graphics.DrawString("Subtotal:$______", fp, Brushes.Black, lp, fderecha)

                End If
            End If
        End If
        If ntventa.pago.descuento > 0 Then
            upy(tam.Height, True)
            e.Graphics.DrawString("Descuento:-" & fpesos(ntventa.pago.descuento), fp, Brushes.Black, lp, fderecha)
        End If
        upy(tam.Height, True)
        If Not empty Then
            e.Graphics.DrawString("Total:" & fpesos(total - ntventa.pago.descuento), fp, Brushes.Black, lp, fderecha)
        Else
            e.Graphics.DrawString("Total:$______", fp, Brushes.Black, lp, fderecha)

        End If

        If Not ntventa.apartado And Not ntventa.cotizacion Then
            If pago > 0 Then
                If ntventa.pago.efectivo > 0 Then
                    upy(tam.Height, True)
                    e.Graphics.DrawString("Efectivo:" & fpesos(ntventa.pago.efectivo), fp, Brushes.Black, lp, fderecha)
                End If
                If ntventa.pago.tarjeta > 0 Then
                    upy(tam.Height, True)
                    e.Graphics.DrawString("Tarjetas:" & fpesos(ntventa.pago.tarjeta), fp, Brushes.Black, lp, fderecha)
                End If

                If ntventa.pago.transferencia > 0 Then
                    upy(tam.Height, True)
                    e.Graphics.DrawString("Transferencia:" & fpesos(ntventa.pago.transferencia), fp, Brushes.Black, lp, fderecha)

                End If
            End If
            If resta > 0 Then
                upy(tam.Height, True)
                e.Graphics.DrawString("Resta:" & fpesos(resta), fp, Brushes.Black, lp, fderecha)
            End If

        End If

    End Sub
    Public Sub calculatepagesventa(e As Object)
        Dim lineas As Integer = 0
        Dim saltos As Integer = 0
        npag = 1
        lineas = 0

        For i = 0 To artv.articulo.Count - 1

            lineas += 1

            If artv.articulo(i).descripcion.Length > 43 Then
                lineas += 1

            End If
            If i < artv.articulo.Count - 1 Then
                If lineas = nart Then
                    npag += 1
                    lineas = 0
                ElseIf lineas > nart Then
                    npag += 1
                    lineas -= nart
                End If

            End If
        Next

        pagac = 1
    End Sub

    Private Sub printencabezado(ByRef e As Printing.PrintPageEventArgs)
        selectitiendaprint()
        pagnota.dir1.Location = New Point(pagnota.en1.Location.X - 30, pagnota.dir1.Location.Y)
        'imprime el encabezado de la nota 
        pagnota.cliente.Text = clnt.Nombre
        pagnota.rfc.Text = clnt.RFC
        pagnota.direccion.Text = clnt.Direccion
        pagnota.direccion.Text = clnt.Direccion
        If clnt.RFC = "" And Not empty Then
            pagnota.rfc.Text = "N/A"
        End If
        If clnt.Direccion = "" And Not empty Then
            pagnota.direccion.Text = "N/A"
        End If
        If Not empty Then
            telefono = "N/A"

        End If
        If clnt.telcasa <> "" Then
            telefono = clnt.telcasa
        End If
        If clnt.telcel <> "" And Not empty Then
            telefono = clnt.telcel
        End If
        pagnota.correrol.Text = "Correo:" & clnt.mail
        'If ntventa.cotizacion Then
        '    pagnota.folio.Text = ""
        '    pagnota.foliobar.Text = ""

        'Else
        pagnota.folio.Text = folio
            If codeprint Then
                pagnota.foliobar.Text = "*" & folio & "*"
            Else
                pagnota.foliobar.Text = folio
            End If

        'End If
        Dim saltosnombre As Integer = 1
        Dim saltosdire As Integer = 1
        pagnota.cliente.Text = stprintadapter(clnt.Nombre, 360, pagnota.n1.Font, e.Graphics, saltosnombre)
        pagnota.direccion.Text = stprintadapter(clnt.Direccion, 360, pagnota.n1.Font, e.Graphics, saltosdire)

        With pagnota
            .dirl.Location = New Point(.dirl.Location.X, .dirl.Location.Y + (9 * (saltosnombre - 1)))
            .direccion.Location = New Point(.direccion.Location.X, .direccion.Location.Y + (9 * (saltosnombre - 1)))
            .correrol.Location = New Point(.correrol.Location.X, .correrol.Location.Y + (9 * (saltosnombre - 1)))

        End With
        Dim rnrfc As Integer = 0
        Dim rndir As Integer = 0
        Dim p As Point
        Dim palto As Integer = 0
        If Not empty Then
            pagnota.npag.Text = "Pag:" & pagac & "/" & npag
        End If
        Select Case idti
            Case 0, 1
                e.Graphics.DrawImage(SICDIN30.My.Resources.casaliconalogo, pxof + 30, pyof - 5, 85, 85)

            Case 2
                e.Graphics.DrawImage(SICDIN30.My.Resources.logoally, pxof, pyof + 10, 150, 50)
        End Select
        For Each st As Label In pagnota.encabezado.Controls
            p = st.Location
            p.Y = p.Y + pyof
            p.X = p.X + pxof
            Dim f As Font = st.Font
            If st.Enabled Then
                Select Case st.Name

                    Case "folio"
                        If codeprint Then
                            e.Graphics.DrawString(st.Text, f, Brushes.Black, p)
                        End If
                    Case "foliobar"
                        If codeprint Then
                            e.Graphics.DrawString(st.Text, fcode, Brushes.Black, p)
                        Else
                            e.Graphics.DrawString(st.Text, fpbic, Brushes.Black, p.X, p.Y - 20)
                        End If
                    Case "tellabel"
                        e.Graphics.DrawString(st.Text & telefono, f, Brushes.Black, p)
                    Case "correol"
                        If pagnota.correrol.Text <> "" Then
                            e.Graphics.DrawString(st.Text & clnt.mail, f, Brushes.Black, p)
                        Else
                            e.Graphics.DrawString(st.Text & "N/A", f, Brushes.Black, p)
                        End If
                    Case Else
                        e.Graphics.DrawString(st.Text, f, Brushes.Black, p)
                End Select

            End If
            If p.Y > palto Then
                palto = p.Y
            End If
        Next
        px = pxof
        py = pagnota.encabezado.Size.Height
        updatep()
        Select Case idti
            Case 0, 1
                e.Graphics.DrawImage(SICDIN30.My.Resources.logocltrans, pxof + 120, lp.Y - 50, 350, 350)

            Case 2
                e.Graphics.DrawImage(SICDIN30.My.Resources.allystrans, pxof + 20, lp.Y - 10, 650, 200)
        End Select

    End Sub
    Private Sub setp(x As Integer, y As Integer)
        ''establece lp
        lp = New Point(x, y)
    End Sub
    Private Sub updatep()
        ''actualiza lp
        lp = New Point(px, py)
    End Sub
    Private Sub upx(ByVal x As Integer, Optional adder As Boolean = False, Optional pox As Integer = 0)
        ''actualiza el punto en x
        If adder Then
            px = px + x
        Else
            px = x + pox
        End If
        updatep()
    End Sub

    Private Sub upy(ByVal y As Integer, Optional adder As Boolean = False)
        ''actualiza el punto en y
        If adder Then
            py = py + y
        Else
            py = y
        End If
        updatep()
    End Sub
    Function shortenstring(ByVal tammax As Integer, st As String, fuente As Font, ByRef e As Printing.PrintPageEventArgs) As String
        ''recorta la cadena a imprimir al tamaño maximo establecido por tammax
        Dim tam As Integer = e.Graphics.MeasureString(st, fuente).Width
        If tam <= tammax Then
            shortenstring = st
        Else
            Dim i As Integer
            Dim tp As String = "..."
            Dim nst As String = ""
            For i = 0 To st.Length
                If e.Graphics.MeasureString(nst & tp, fuente).Width < tammax Then
                    nst &= st(i)
                Else
                    nst &= tp
                    Exit For
                End If
            Next
            shortenstring = nst
        End If
    End Function
    Function shortenstring(ByVal tammax As Integer, st As String, fuente As Font, ByRef e As Graphics) As String
        ''recorta la cadena a imprimir al tamaño maximo establecido por tammax
        Dim tam As Integer = e.MeasureString(st, fuente).Width
        If tam <= tammax Then
            shortenstring = st
        Else
            Dim i As Integer
            Dim tp As String = "..."
            Dim nst As String = ""
            For i = 0 To st.Length
                If e.MeasureString(nst & tp, fuente).Width < tammax Then
                    nst &= st(i)
                Else
                    nst &= tp
                    Exit For
                End If
            Next
            shortenstring = nst
        End If
    End Function
    Private Sub constnot(ByRef e As Object, ByVal secpag As Boolean, ByVal pag As Integer, ByVal dg1 As DataGridView, ByVal total As Decimal)
        Dim totletra As String = num2str(total)
        p2 = New PictureBox
        p2.Image = Image.FromFile("c:\sc3\rimag\cl.png")
        Dim totpag As Integer = 1
        Dim sep As Integer
        Dim j As Integer

        If secpag = True Then
            sep = Val(e.MarginBounds.Bottom / 2) - 30
        Else
            sep = -50
        End If
        Dim f, fn, fsub, f2, fp As Font
        Dim a As Integer
        Dim al As Integer
        Dim p As Point
        fp = New Font("Arial", 9, System.Drawing.FontStyle.Bold)
        f2 = New Font("Arial", 15, System.Drawing.FontStyle.Bold)
        f = New Font("Arial", 9)
        e.Graphics.DrawImage(p2.Image, e.MarginBounds.Left, e.MarginBounds.Top + sep, 170, 65)
        a = e.MarginBounds.Left + 149
        al = e.MarginBounds.Top + sep
        p = New Point(a + a - 10, al)
        Dim drawFormat As New StringFormat
        drawFormat.Alignment = StringAlignment.Center
        e.Graphics.DrawString("ESPECIALISTA EN PINTURA METALICA DE HORNO PARA BICICLETA", f, Brushes.Black, p, drawFormat)
        al = al + 17
        p = New Point(a + a, al)
        fn = New Font("Arial", 16, System.Drawing.FontStyle.Bold)
        e.Graphics.DrawString("CASA LICONA", fn, Brushes.Black, p, drawFormat)
        al = al + 32
        p = New Point(a + a, al)
        e.Graphics.DrawString("VENTA DE BICICLETAS, REFACCIONES, ACCESORIOS Y REPARACION EN GRAL.", f, Brushes.Black, p, drawFormat)
        If tienda.nombre = "Casa Licona" Then
            al = p2.Image.Size.Height + e.MarginBounds.Top + sep
        Else
            al = p2.Image.Size.Height + e.MarginBounds.Top - 120
        End If

        a = e.MarginBounds.Left
        p = New Point(a, al + 5)
        fn = New Font("Arial", 9, System.Drawing.FontStyle.Bold)
        e.Graphics.DrawString("JOSE RICARDO LICONA CANCINO    R.F.C.:LICR-580628-U23", fn, Brushes.Black, p)
        a = e.MarginBounds.Right - 70 - 30
        p = New Point(a + 45, al + 5)
        e.Graphics.DrawString("COTIZACIÓN", f2, Brushes.Black, p)
        a = e.MarginBounds.Left
        al = al + 19
        p = New Point(a, al)
        e.Graphics.DrawString("Regimen intermedio", fp, Brushes.Black, p)
        a = e.MarginBounds.Left
        al = al + 19
        p = New Point(a, al)
        a = e.MarginBounds.Right - 65 - 19
        p = New Point(a, al - 5)
        Dim cot1 As String = ""
        e.Graphics.DrawString(cot1, f, Brushes.Black, p)
        a = e.MarginBounds.Left
        p = New Point(a, al)
        e.Graphics.DrawString("CTO. VICENTE AGUIRRE DEL CASTILLO                      PACHUCA, HGO.", fn, Brushes.Black, p)
        al = al + 19
        p = New Point(a, al)
        e.Graphics.DrawString("COL. JUAN C. DORIA                                     LOCAL 71         C.P. 42083", fn, Brushes.Black, p)
        a = e.MarginBounds.Right - 17 - 10
        p = New Point(a + 5, al)
        e.Graphics.DrawString("Tel. 71-078-60  Cel. 771-121-60-98", f, Brushes.Black, p)
        a = e.MarginBounds.Left
        al = al + 18
        fsub = New Font("Arial", 9, System.Drawing.FontStyle.Underline)
        p = New Point(a, al)
        e.Graphics.DrawString("", fsub, Brushes.Blue, p)
        a = a + 17 + 35
        p = New Point(a, al)
        e.Graphics.DrawString("    Pachuca de Soto, Hgo. a ", f, Brushes.Black, p)
        al = al + 17
        a = e.MarginBounds.Left
        p = New Point(a, al)
        e.Graphics.DrawString("Nombre:", fn, Brushes.Black, p)
        a = e.MarginBounds.Width - 17
        p = New Point(a, al)
        e.Graphics.DrawString("Pag:", f, Brushes.Black, p)
        al = al + 19
        a = e.MarginBounds.Left
        p = New Point(a, al)
        'If cotizacion.Text = "Nota de venta" Then
        '    e.Graphics.DrawString(Label15.Text, fn, Brushes.Black, p)
        '    a = a + Label15.Width
        '    p = New Point(a, al)
        '    e.Graphics.DrawString(MaskedTextBox1.Text, fn, Brushes.Black, p)
        '    a = e.MarginBounds.Left
        '    al = al + Label15.Height
        '    p = New Point(a, al)
        '    e.Graphics.DrawString(Label17.Text, fn, Brushes.Black, p)
        '    a = a + Label17.Width
        '    p = New Point(a, al)
        '    e.Graphics.DrawString(dire.Text, fn, Brushes.Black, p)
        '    a = e.MarginBounds.Left
        '    al = al + Label17.Height
        'End If
        p = New Point(a, al)
        Dim ll As Integer = 17 * 13
        Dim antal As Integer
        antal = al
        e.Graphics.DrawRectangle(Pens.Black, a, al, e.MarginBounds.Width + 50, ll)
        Dim i As Integer

        For i = 0 To 1
            e.Graphics.DrawLine(Pens.Black, a, al, a, al + ll)
            e.Graphics.DrawString(dg1.Columns(i).HeaderText, f, Brushes.Black, p)
            If i = 1 Then
                a = a + dg1.Columns(i).Width
            Else
                a = a + dg1.Columns(i).Width
            End If
            p = New Point(a, al)
        Next

        a = e.MarginBounds.Left
        al = al + 17
        antal = al
        e.Graphics.DrawLine(Pens.Black, a, al, e.MarginBounds.Width + a + 50, al)
        drawFormat.Alignment = StringAlignment.Far

        f = New Font("Arial", 11)
        For i = 0 To 11
            If i <= dg1.Rows.Count - 1 Then
                For j = 0 To 1
                    If j = 1 Then
                        e.Graphics.DrawString(dg1(j, i).Value, f, Brushes.Black, a + dg1.Columns(j).Width, al, drawFormat)
                    Else
                        e.Graphics.DrawString(dg1(j, i).Value, f, Brushes.Black, a, al)
                    End If
                    a = a + dg1.Columns(j).Width
                    p = New Point(a, al)
                Next
            End If
            a = e.MarginBounds.Left
            al = al + 17
            p = New Point(a, al)
            e.Graphics.DrawLine(Pens.Black, a, al, e.MarginBounds.Width + a + 50, al)
        Next

        Dim nal As Integer = al + 110

        If pag = totpag Then
            e.Graphics.DrawString("Total con letra:", fn, Brushes.Black, 104 + e.MarginBounds.Left, al)
            e.Graphics.DrawString(totletra, fn, Brushes.Black, 103 + e.MarginBounds.Left, al + 17)
            al = antal + ll
            a = e.MarginBounds.Width - 24 - 24 - 10
            p = New Point(a, al)
            e.Graphics.DrawString("SubTotal:", fn, Brushes.Black, e.MarginBounds.Right + 10, al - 10, drawFormat)
            al = al + 24
            p = New Point(a, al)
            'If cotizacion.Text = "Nota de venta" Then
            '    e.Graphics.DrawString(ival.Text, fn, Brushes.Black, e.MarginBounds.Right + 10, al - 10, drawFormat)
            '    al = al + t1.Height
            '    fp = New Font("Arial", 5)
            'End If
            e.Graphics.DrawString("Total:", fn, Brushes.Black, e.MarginBounds.Right + 10, al - 10, drawFormat)

            al = al + 24
            p = New Point(a, al)
        Else
            al = antal + ll
            p = New Point(a, al)
            al = al + 24
            p = New Point(a, al)
            al = al + 24
            fp = New Font("Arial", 5)
        End If



        'If cotizacion.Text <> "Nota de venta" And contarb = True Then
        '    e.Graphics.DrawString("Sin validez fiscal", fn, Brushes.Black, 104 + e.MarginBounds.Left, al)
        '    If resta.Visible And pag = totpag Then
        '        e.Graphics.DrawString(pago.Text, fn, Brushes.Black, e.MarginBounds.Right + 10, al - 10, drawFormat)
        '        e.Graphics.DrawString(resta.Text, fn, Brushes.Black, e.MarginBounds.Right + 10, al - 10 + pago.Height, drawFormat)
        '    End If

        'End If



    End Sub


End Module



