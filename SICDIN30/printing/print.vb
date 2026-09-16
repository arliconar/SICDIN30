Imports System.Drawing
Imports System.Drawing.Printing
Module print
    Class imprimir
        Dim pd As New PrintDocument()
        Dim reparacion As repbic
        Dim folio As String
        Dim clnt As Cliente
        Dim pdialog As PrintDialog
        Dim pagac As Integer = 0 'pagina actual
        Dim ivaon As Boolean = False
        Dim e As Object
        ' Dim e As PrintPageEventArgs
        Dim empty As Boolean = False
        Dim npag As Integer = 0 'npag
        'Fuentes de letras
        Dim fpbic As Font = New Font("Arial", 12)
        Dim fderecha As StringFormat = New StringFormat
        Dim fp As Font = New Font("Arial", 9)
        Dim fpb As Font = New Font("Arial", 9, FontStyle.Bold)
        Dim fpeque As Font = New Font("Arial", 7)
        Dim fpequebold As Font = New Font("Arial", 7, FontStyle.Bold)
        Dim fpequeapartado As Font = New Font("Arial", 5, FontStyle.Bold)
        'Variables
        Dim lp As Point
        Dim nbici As Integer
        Dim tam As SizeF
        Dim nini As Integer
        Dim nfin As Integer
        ''constantes
        Dim pyof As Integer = 35
        Dim pxof As Integer = 60
        Dim pxlim As Integer
        Dim pxlimof As Integer = 60 ''100
        Dim px As Integer
        Dim ncaracteres As Integer = 22
        Dim slp As Point
        Dim py As Integer

        ''Leyendas
        Dim leyendaaclaracion1 As String = "Sin validez fiscal. Para cualquier cambio o aclaración favor de presentar esta nota en un lapso no mayor a"
        Dim leyendaaclaracion2 As String = "30 días"
        Dim leyendaaclaracion3 As String = "desde su emisión"
        ''Dim leyendaaclaracion4 As String = ""
        Dim leyendaaclaracion5 As String = "Sin validez fiscal. Para cualquier cambio o aclaración favor de presentar esta orden en un lapso no mayor a"
        Dim leyendaaclaracion6 As String = ""
        Dim leyendaaclaracion7 As String = "No nos hacemos responsables por el deterioro de su bicicleta después de 3 días terminado el trabajo."
        Dim leyendaaclaracion8 As String = "A partir del 3er día de terminado el trabajo causara renta de $10.00 diarios. Rematandose a los 90 días"
        Public Sub printbmprep(rep As repbic)

            reparacion = rep
            fderecha.Alignment = StringAlignment.Far
            selectitiendaprint()
            Dim bmpancho As Integer = 800
            Dim lineas As Integer = 0
            For i = 0 To rep.bici.Count - 1
                lineas += 2
                For k = 0 To rep.bici(i).dg.articulo.Count - 1
                    lineas += 1
                    If rep.bici(i).dg.articulo(k).descripcion.Length > 33 Then
                        lineas += 1
                    End If
                Next
            Next
            Dim ialto As Integer = 350 + (15 * lineas)
            If ialto < 380 Then
                ialto = 380
            End If
            Dim bmprep As New Bitmap(bmpancho + 25, ialto)
            Dim ev As Graphics = Graphics.FromImage(bmprep)
            ev.Clear(Color.White)
            clnt = New Cliente
            clnt = rep.clnt
            pagnota.Close()
            selectitiendaprint()
            pagnota.labelnota.Text = "Reparación"
            pagac = 1
            npag = 1
            folio = rep.folio
            printencabezado(ev)

            Dim pprimera As Boolean = True
            px = pxof
            Dim pxlim As Integer = bmprep.Width - 50
            Dim slp As Point = New Point(pxlim, py)
            tam = ev.MeasureString("W", fp)
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


            For i = 0 To rep.bici.Count - 1
                ev.DrawLine(Pens.Black, lp, slp)


                If rep.bici.Count > 2 Then
                    sts = "Bicicleta " & rep.bici(i).id & " de " & rep.bici.Count & "   "
                Else
                    sts = "Bicicleta "
                End If

                If rep.bici(i).rodada <> "" Then
                    sts &= "Rodada:" & rep.bici(i).rodada
                End If
                If rep.bici(i).marca <> "" Then
                    sts &= "   Marca: " & rep.bici(i).marca
                End If
                If rep.bici(i).color <> "" Then
                    sts &= "  Color: " & rep.bici(i).color
                End If
                ev.DrawString(sts, fpbic, Brushes.Black, lp)
                upy(tam.Height, True)
                upx(pxof)
                sts = "Fecha de entrega: " & rep.bici(i).fentrega.ToShortDateString & "  Hora entrega:" & rep.bici(i).fentrega.ToString("hh:mm tt")
                ev.DrawString(sts, fpbic, Brushes.Black, lp)
                upy(tam.Height * 0.5, True)
                upx(pxof)

                If rep.bici(i).Nota <> "" Then
                    upy(tam.Height * 0.5, True)
                    sts = "Nota :" & rep.bici(i).Nota
                    ev.DrawString(sts, fpbic, Brushes.Black, lp)
                    upy(tam.Height, True)
                    upx(pxof)
                End If

                upy(-2 * tam.Height, True)

                Dim k As Integer = nini

                py = py + pyof + 10
                updatep()

                slp = New Point(pxlim, py)
                ev.DrawLine(Pens.Black, lp, slp)
                ev.DrawString("Clave", fp, Brushes.Black, lp)
                px = px + (pxuni * 10)
                updatep()
                ev.DrawString("Cant.", fp, Brushes.Black, lp)
                px = px + (pxuni * 10)
                updatep()
                ev.DrawString("Descripción.", fp, Brushes.Black, lp)
                px = px + (pxuni * 50)
                updatep()
                ev.DrawString("Paquete.", fp, Brushes.Black, lp)
                px = px + (pxuni * 20)
                updatep()
                ev.DrawString("Importe", fp, Brushes.Black, lp, fderecha)
                px = pxlim
                updatep()
                ev.DrawString("Precio", fp, Brushes.Black, lp, fderecha)
                upx(pxof, False)
                upy(tam.Height, True)
                slp = New Point(pxlim, py)
                ev.DrawLine(Pens.Black, lp, slp)


                Dim saltos As Integer = 0

                Dim nstop As Integer = nfin
                Dim naro As Integer = 0
                For i2 = 0 To rep.bici(i).dg.articulo.Count - 1
                    If k < rep.bici(i).dg.articulo.Count Then
                        If rep.bici(i).dg.articulo(k).clave <> "" Then
                            upx(pxof, False)
                            ev.DrawString(rep.bici(i).dg.articulo(k).clave, fp, Brushes.Black, lp)
                            px = px + (pxuni * 10)
                            updatep()
                            ev.DrawString(rep.bici(i).dg.articulo(k).cantidad, fp, Brushes.Black, lp)
                            px = px + (pxuni * 10)
                            updatep()
                            Dim stdes As String = rep.bici(i).dg.articulo(k).descripcion
                            stdes = stprintadapter(stdes, tam.Width * 22, fp, ev, saltos)
                            If rep.bici(i).dg.articulo(k).notas <> "" Then
                                stdes &= " * "
                                If Not leyendaaclaracion6.Contains(rep.bici(i).dg.articulo(k).notas) Then
                                    If leyendaaclaracion6 = "" Then
                                        leyendaaclaracion6 = "*"
                                    End If
                                    leyendaaclaracion6 &= rep.bici(i).dg.articulo(k).notas & vbNewLine
                                End If
                            End If
                            ev.DrawString(stdes, fp, Brushes.Black, lp)
                            px = px + (pxuni * 50)
                            updatep()
                            ev.DrawString(rep.bici(i).dg.articulo(k).pq, fp, Brushes.Black, lp)
                            px = px + (pxuni * 20)
                            updatep()
                            If ivaon Then
                                ev.DrawString("$" & adapterprint(rep.bici(i).dg.articulo(k).precio - ivades(rep.bici(i).dg.articulo(k).precio).ToString), fp, Brushes.Black, lp, fderecha)
                                px = pxlim
                                updatep()
                                ev.DrawString("$" & adapterprint(rep.bici(i).dg.articulo(k).importe - ivades(rep.bici(i).dg.articulo(k).importe)), fp, Brushes.Black, lp, fderecha)
                            Else
                                ev.DrawString("$" & adapterprint(rep.bici(i).dg.articulo(k).precio), fp, Brushes.Black, lp, fderecha)
                                px = pxlim
                                updatep()
                                ev.DrawString("$" & adapterprint(rep.bici(i).dg.articulo(k).importe), fp, Brushes.Black, lp, fderecha)
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
                slp = New Point(pxlim, py)


            Next

            Dim totalst As String = num2str(reparacion.total)
            upy(tam.Height, True)
            px = pxof
            updatep()
            slp = New Point(pxlim, py)
            ev.DrawLine(Pens.Black, lp, slp)
            upx(pxlimof)
            Dim posleyy As Integer = 0
            ev.DrawString("Total con letra", fpb, Brushes.Black, lp)
            upx(pxlim)
            ev.DrawString("Total:" & fpesos(rep.total.ToString), fpb, Brushes.Black, lp, fderecha)
            upx(pxlimof)
            upy(tam.Height, True)
            ev.DrawString(totalst, fpb, Brushes.Black, lp)
            posleyy = lp.Y + tam.Height
            upx(pxlim)
            ev.DrawString("A cuenta:" & fpesos(rep.tpagado), fpb, Brushes.Black, lp, fderecha)


            upx(pxof)
            upy(posleyy, False)
            ev.DrawString(leyendaaclaracion5, fpeque, Brushes.Black, lp)
            upx(ev.MeasureString(leyendaaclaracion5, fpeque).Width + 50)
            ev.DrawString(leyendaaclaracion2, fpequebold, Brushes.Black, lp)
            upx(ev.MeasureString(leyendaaclaracion2, fpequebold).Width, True)
            ev.DrawString(leyendaaclaracion3, fpeque, Brushes.Black, lp)
            upx(pxof)
            If leyendaaclaracion6 <> "" Then
                upx(pxof)
                upy(tam.Height, True)
                ev.DrawString(leyendaaclaracion6, fpeque, Brushes.Black, lp)
            End If
            upy(tam.Height, True)
            upx(pxof)
            ev.DrawString(leyendaaclaracion7, fpeque, Brushes.Black, lp)
            upy(tam.Height, True)
            upx(pxof)
            ev.DrawString(leyendaaclaracion8, fpeque, Brushes.Black, lp)

            Clipboard.SetImage(bmprep)

        End Sub
        Private Sub printencabezado(e As Object)
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
            Dim telefono As String = "N/A"
            If clnt.telcasa <> "" And Not empty Then
                telefono = clnt.telcasa
            End If
            If clnt.telcel <> "" And Not empty Then
                telefono = clnt.telcel
            End If
            pagnota.correrol.Text = "Correo:" & clnt.mail

            pagnota.folio.Text = folio
            If codeprint Then
                pagnota.foliobar.Text = "*" & folio & "*"
            Else
                pagnota.foliobar.Text = folio
            End If

            'End If
            Dim saltosnombre As Integer = 1
            Dim saltosdire As Integer = 1
            pagnota.cliente.Text = stprintadapter(clnt.Nombre, 360, pagnota.n1.Font, e, saltosnombre)
            pagnota.direccion.Text = stprintadapter(clnt.Direccion, 360, pagnota.n1.Font, e, saltosdire)

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
                    e.DrawImage(SICDIN30.My.Resources.casaliconalogo, pxof + 30, pyof - 5, 85, 85)

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
        End Sub
        Public Sub printreparacion(rep As repbic, Optional emptyr As Boolean = False)
            pdialog = New PrintDialog
            empty = emptyr
            reparacion = rep
            fderecha.Alignment = StringAlignment.Far
            clnt = rep.clnt
            folio = rep.folio
            'pdialog.PrinterSettings.Copies = 2 ''Establecemos numero de copias
            If Not pdialog.ShowDialog = vbOK Then ''si dan cancelar cancelamos
                Exit Sub
            End If
            pd.PrinterSettings = pdialog.PrinterSettings
            pagcal(nrep)
            AddHandler pd.PrintPage, AddressOf imprimirrepara
            pagac = 1
            nbici = 0
            nini = 0
            pagnota.labelnota.Text = "Reparación"
            Try
                pd.Print()
            Catch ex As Exception
                LogAppError(ex, "print.pd.Print")
                MessageBox.Show("No se pudo imprimir la reparación:" & vbCrLf & ex.Message, "Aviso de Impresión", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Finally
                RemoveHandler pd.PrintPage, AddressOf imprimirrepara
            End Try

        End Sub
        Public Sub pagcal(narticulos As Integer) ''calcula paginas
            npag = 0
            For i = 0 To reparacion.bici.Count - 1
                Dim nlineas As Integer = 0
                npag += 1

                For k = 0 To reparacion.bici(i).dg.articulo.Count - 1
                    nlineas += 1
                    If reparacion.bici(i).dg.articulo(k).descripcion.Length > 50 Then
                        nlineas += 1
                    End If

                    If nlineas > narticulos And k < reparacion.bici(i).dg.articulo.Count - 1 Then
                        nlineas = 0
                        npag += 1
                    End If

                Next
            Next

        End Sub
        Public Sub imprimirrepara(ByVal sender As Object, ByVal ev As PrintPageEventArgs) ''imprime reparacion

            selectitiendaprint()
            e = ev

            printencabezado(ev.Graphics) ''imprimimos encabezado

            printbicis()
            printpagos(ev.Graphics)

            pagac += 1
            If pagac > npag Then
                ev.HasMorePages = False
            Else
                ev.HasMorePages = True

            End If


        End Sub
        Public Sub New()
            fderecha.Alignment = StringAlignment.Far
            reparacion = New repbic

        End Sub
        Private Sub printbicis()
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
            With reparacion.bici(nbici)
                If reparacion.bici.Count > 2 Then
                    sts = "Bicicleta " & .id & " de " & reparacion.bici.Count & "   "
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
                If Not empty Then
                    sts = "Fecha de entrega: " & .fentrega.ToShortDateString & "  Hora entrega:" & .fentrega.ToString("hh:mm tt")

                Else
                    sts = "Fecha de entrega: ______________ Hora entrega: _____________"

                End If
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
                    e.Graphics.DrawString("<--- Continuación de pagina " & pagac - 1, fp, Brushes.Black, lp.X, lp.Y + 10)
                    upy(tam.Height, True)

                End If
                upy(-2 * tam.Height, True)

                nini = printdg(reparacion.bici(nbici).dg, nini, nrep)


                If nini < .dg.articulo.Count - 1 Then
                    upx(pxlim)
                    upy(tam.Height, True)
                    'e.Graphics.DrawString("Continua pagina " & pagac + 1 & " --->", fp, Brushes.Black, lp, fderecha)
                    slp = New Point(pxlim, py)
                    Dim asr As Point = New Point(pxof, py)
                    'e.Graphics.DrawLine(Pens.Black, asr, slp)
                Else
                  
                    nbici += 1

                    If nbici > reparacion.bici.Count - 1 Then
                        nbici = reparacion.bici.Count - 1
                        ''    nini = 0
                    Else
                        nini = 0
                    End If
                End If

            End With

        End Sub
        Private Function printdg(artv As dgventa, nini As Integer, nfin As Integer) As Integer
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

            Dim saltos As Integer = 0

            Dim nstop As Integer = nfin
            Dim naro As Integer = 0
            For i = 0 To nfin
                If k < artv.articulo.Count Then
                    If artv.articulo(k).clave <> "" Then
                        upx(pxof, False)
                        e.Graphics.DrawString(artv.articulo(k).clave, fp, Brushes.Black, lp)
                        px = px + (pxuni * 10)
                        updatep()
                        e.Graphics.DrawString(artv.articulo(k).cantidad, fp, Brushes.Black, lp)
                        px = px + (pxuni * 10)
                        updatep()
                        Dim stdes As String = artv.articulo(k).descripcion
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
                            e.Graphics.DrawString("$" & adapterprint(artv.articulo(k).precio - ivades(artv.articulo(k).precio).ToString), fp, Brushes.Black, lp, fderecha)
                            px = pxlim
                            updatep()
                            e.Graphics.DrawString("$" & adapterprint(artv.articulo(k).importe - ivades(artv.articulo(k).importe)), fp, Brushes.Black, lp, fderecha)
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
        Private Sub printpagos(e As Object)


            Dim totalst As String = num2str(reparacion.total)
            upy(tam.Height, True)
            px = pxof
            updatep()
            slp = New Point(pxlim, py)
            e.DrawLine(Pens.Black, lp, slp)
            upx(pxlimof)
            Dim posleyy As Integer = 0

            If pagac = npag Then
                e.DrawString("Total con letra", fpb, Brushes.Black, lp)
                upx(pxlim)
                If Not empty Then
                    e.DrawString("Total:" & fpesos(reparacion.total.ToString), fpb, Brushes.Black, lp, fderecha)
                Else
                    e.DrawString("Total:$_______", fpb, Brushes.Black, lp, fderecha)
                End If

                upx(pxlimof)
                upy(tam.Height, True)
                If Not empty Then
                    e.DrawString(totalst, fpb, Brushes.Black, lp)
                End If
                posleyy = lp.Y + tam.Height
                upx(pxlim)
                If Not empty Then
                    e.DrawString("A cuenta:" & fpesos(reparacion.tpagado), fpb, Brushes.Black, lp, fderecha)
                Else
                    e.DrawString("A cuenta:$_______", fpb, Brushes.Black, lp, fderecha)
                End If

            Else
                upx(pxlim)
                posleyy = lp.Y + tam.Height
                e.DrawString("Continua Pag. " & pagac + 1 & "-->", fpb, Brushes.Black, lp, fderecha)

            End If

            ' End If


            upx(pxof)
            upy(posleyy, False)
            e.DrawString(leyendaaclaracion5, fpeque, Brushes.Black, lp)
            upx(e.MeasureString(leyendaaclaracion5, fpeque).Width + 50)
            e.DrawString(leyendaaclaracion2, fpequebold, Brushes.Black, lp)
            upx(e.MeasureString(leyendaaclaracion2, fpequebold).Width, True)
            e.DrawString(leyendaaclaracion3, fpeque, Brushes.Black, lp)
            upx(pxof)
            If leyendaaclaracion6 <> "" Then
                upx(pxof)
                upy(tam.Height, True)
                e.DrawString(leyendaaclaracion6, fpeque, Brushes.Black, lp)
            End If
            upy(tam.Height, True)
            upx(pxof)
            e.DrawString(leyendaaclaracion7, fpeque, Brushes.Black, lp)
            upy(tam.Height, True)
            upx(pxof)
            e.DrawString(leyendaaclaracion8, fpeque, Brushes.Black, lp)


        End Sub
        Private Sub selectitiendaprint()
            pagnota.Close()
            With pagnota
                Select Case tienda.id
                    Case 2
                        ''.tiendalabel.Text = "Bicicletas Ally's Store"
                        .dtienda.Text = ""
                        .dir1.Location = New Point(.dir1.Location.X - 60, .dir1.Location.Y)
                        .dir1.Text = "PLAZA COMERCIAL LOS CEDROS LOCAL 101, BLVD. NUEVO HIDALGO 1123"
                        .dir2.Text = "MINERAL DE LA REFORMA, HGO. Tel: 771 791 4748 WhatsApp:771 231 9332"
                        If Not empty Then
                            .f1.Text = "Mineral de la reforma, Hgo. a " & reparacion.fcreacion.Day & " de " & MonthName(reparacion.fcreacion.Month) & " del " & reparacion.fcreacion.Year
                        Else
                            .f1.Text = "Mineral de la reforma, Hgo. a _____ de _____________del 20__"

                        End If
                        ''.telstien.Location = New Point(222, .telstien.Location.Y)
                        ''.telstien.Text = ""
                End Select
            End With
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
    End Class
End Module
