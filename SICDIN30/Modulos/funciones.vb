Imports System
Imports System.IO
Module funciones
    Dim nomprevbut As String = ""
    Dim fe As Font = New Font("Arial", 14, FontStyle.Bold)

    Public Sub msgdebug(Optional msg As String = "Entre")

        MessageBox.Show(msg)


    End Sub

    Public Function getlugares(ommitme As Boolean) As lugar()
        Dim st As String = "Select * FROM Lugares"
        Dim lugares As lugar()

        Dim orden As New OleDb.OleDbCommand(st, consys)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        Dim i As Integer = 0
        ReDim lugares(0)
        While lector.Read
            If lector("Id") <> tienda.id Then
                If i > 0 Then
                    ReDim Preserve lugares(i)
                End If
                lugares(i) = New lugar
                lugares(i).load(lector("Id"))
                i += 1
            End If

        End While
        lector.Close()

        Return lugares

    End Function

    Public Function diasemana(dia As Integer) As String

        Select Case dia
            Case 1
                diasemana = "Lunes"
            Case 2
                diasemana = "Martes"
            Case 3
                diasemana = "Miercoles"
            Case 4
                diasemana = "Jueves"
            Case 5
                diasemana = "Viernes"
            Case 6
                diasemana = "Sábado"
            Case Else
                diasemana = "Domingo"
        End Select

    End Function
    Public Sub artfromreader(ByRef art As articuloventa, lector As OleDb.OleDbDataReader)
        With art
            .clave = lector("Clave")
            .descripcion = lector("Descripcion")
            .mayoreo = lector("Mayoreo")
            .menudeo = lector("menudeo")
            .cbarra = voiddeleterstring(lector("cbarras"))
            .pme = voiddeleter(lector("pme"))
            .especial = voiddeleter(lector("especial"))
            .precio = voiddeleter(lector("precio"))
            .pgmen = voiddeleter(lector("pgmen"))
            .pgmay = voiddeleter(lector("pgmay"))
            .fum = lector("fum")
            .pq = voiddeleterstring(lector("paquete"))

        End With
    End Sub


    Public Sub sendtowhats(numero As String, Optional mensage As String = "") ''Abre whats para enviar un mensaje
        ''  Dim webAddress As String = "whatsapp://send/?phone=+52" & numero
        numero = numero.Replace(" ", "")

        Dim webAddress As String = "whatsapp://send/?phone=+52" & numero
        If mensage <> "" Then
            mensage = mensage.Replace(vbNewLine, "%0A")
            mensage = mensage.Replace(" ", "%20")
            webAddress &= "&text=" & mensage
        End If
        '  MessageBox.Show(webAddress)
        Process.Start(webAddress)
    End Sub
    Public Sub fecharnamefin(st As String, ByRef name As String, ByRef fecha As String)
        Dim st2 As String = st.Replace("Finalizada por", "")
        Dim st3() As String = st2.Split("el")
        name = st3(0).Replace(" ", "")
        MessageBox.Show(st2)
        st3(1) = st3(1).Replace("l", "")
        fecha = st3(1)

    End Sub
    Public Function cla2pre(cla1 As String) As Decimal
        Dim cla As String = cla1.ToUpper

        Dim numst As String = ""

        For i = 0 To cla.Length - 1
            If cla(i) <> "." Then
                If Asc(cla(i)) < 78 Then
                    numst &= Chr(Asc(cla(i)) - 20)
                Else
                    numst &= "0"
                End If
            Else
                numst &= "."
            End If

        Next
        cla2pre = Val(numst)
    End Function
    Public Function pre2cla(num As Decimal) As String
        Dim numst As String = num.ToString
        pre2cla = ""
        For i = 0 To numst.Length - 1
            If numst(i) <> "." Then
                If Asc(numst(i)) > 48 Then
                    pre2cla &= Chr(Asc(numst(i)) + 20)
                Else
                    pre2cla &= "N"
                End If
            Else
                pre2cla &= "."
            End If

        Next

    End Function
    Public Sub selectitiendaprint()

        With pagnota
            Select Case idti
                Case 2
                    ''.tiendalabel.Text = "Bicicletas Ally's Store"
                    .dtienda.Text = ""
                    .dir1.Location = New Point(.dir1.Location.X - 60, .dir1.Location.Y)
                    .dir1.Text = "PLAZA COMERCIAL LOS CEDROS LOCAL 101, BLVD. NUEVO HIDALGO 1123"
                    .dir2.Text = "MINERAL DE LA REFORMA, HGO. Tel: 771 791 4748 WhatsApp:771 231 9332"
                    ''.telstien.Location = New Point(222, .telstien.Location.Y)
                    ''.telstien.Text = ""
            End Select
        End With
    End Sub
    Public Function existimage(clave As String) As Boolean

        If System.IO.File.Exists(pathimage & clave & ".png") Then
            existimage = True
        ElseIf System.IO.File.Exists(pathimage & clave & ".jpg") Then
            existimage = True
        ElseIf System.IO.File.Exists(pathimage & clave & ".bmp") Then
            existimage = True
        Else
            existimage = False
        End If
    End Function
    Public Function fdateselhour(fe As DateTimePicker, st As String) As Date
        Dim idate As String = fe.Value.ToShortDateString & " " & st
        fdateselhour = DateTime.Parse(idate)
    End Function
    Public Sub hidenamebutton(ByRef boton As Object) 'oculta el nombre del boton
        boton.text = nomprevbut

    End Sub
    Public Sub shownamebutton(ByRef boton As Object) 'muestra el nombre del boton
        nomprevbut = boton.text

        boton.text = boton.tag
    End Sub
    Function identnum(ByVal num As String) As String ''identificar numero
        identnum = ""
        Select Case num
            Case "0"
                identnum = ""
            Case "1"
                identnum = "Uno"
            Case "2"
                identnum = "Dos"
            Case "3"
                identnum = "Tres"
            Case "4"
                identnum = "Cuatro"
            Case "5"
                identnum = "Cinco"
            Case "6"
                identnum = "Seis"
            Case "7"
                identnum = "Siete"
            Case "8"
                identnum = "Ocho"
            Case "9"
                identnum = "Nueve"
            Case "10"
                identnum = "Diez"
            Case "11"
                identnum = "Once"
            Case "12"
                identnum = "Doce"
            Case "13"
                identnum = "Trece"
            Case "14"
                identnum = "Catorce"
            Case "15"
                identnum = "Quince"
            Case "16"
                identnum = "Dieciseis"
            Case "17"
                identnum = "Diescisiete"
            Case "18"
                identnum = "Diesciocho"
            Case "19"
                identnum = "Diescinueve"
            Case "20"
                identnum = "Veinte"
            Case "30"
                identnum = "Treinta"
            Case "40"
                identnum = "Cuarenta"
            Case "50"
                identnum = "Cincuenta"
            Case "60"
                identnum = "Sesenta"
            Case "70"
                identnum = "Setenta"
            Case "80"
                identnum = "Ochenta"
            Case "90"
                identnum = "Noventa"
            Case "100"
                identnum = "Ciento"
            Case "500"
                identnum = "Quinientos"
            Case "700"
                identnum = "Setecientos"
            Case "900"
                identnum = "Novecientos"
        End Select
    End Function


    Function tranum2str(ByVal work As String) As String ''seccionar numero
        Dim w2 As String = ""
        Dim dec, uni As Integer
        Dim str1 As String = Microsoft.VisualBasic.Left(work, 1)
        If Val(work) <> 100 Then
            If Len(work) > 2 Then
                If Val(str1) <> 1 And Val(str1) <> 5 And Val(str1) <> 7 And Val(str1) <> 9 And Val(str1) <> 0 Then
                    w2 = identnum(str1) & "cientos"
                Else
                    w2 = identnum(Val(str1) * 100)
                End If
            End If
            tranum2str = w2 & " "
            str1 = Val(Right(work, 2))
            If str1 < 20 Then
                w2 = identnum(str1)
            Else
                uni = Val(str1) Mod 10
                dec = Val(str1) - uni
                w2 = identnum(dec)
                If uni <> 0 Then
                    w2 = w2 & " y " & identnum(uni)
                End If
            End If

            tranum2str = tranum2str & w2
        Else
            tranum2str = "Cien"
        End If

    End Function
    Function num2str(ByVal num As String) As String ''convertir numero a cadena
        Dim index As Integer = num.IndexOf(".")
        Dim centavos As String = "00"
        If index <> -1 Then
            centavos = Val(Mid(num, index + 1)) * 100
            If Val(centavos) < 10 Then
                centavos = "0" & centavos
            End If
            num = Left(num, index)
        End If
        Dim llen, red As Integer
        red = Len(num) Mod 3
        llen = (Len(num) - red) / 3
        If red > 0 Then
            llen += 1
        End If
        num2str = ""
        If red = 0 Then
            red = 3
        End If
        For i = llen To 1 Step -1
            Select Case i
                Case 2
                    If Left(num, red) <> "1" Then
                        num2str = tranum2str(Left(num, red)) & " mil "
                    Else
                        num2str = "Un mil "
                    End If
                Case 1
                    num2str = num2str & tranum2str(Right(num, 3))
            End Select
        Next
        If num2str = "" Or num2str = " " Then
            num2str = "Cero"
        End If
        centavos = Microsoft.VisualBasic.Left(centavos, 2)
        num2str = num2str & " pesos " & centavos & "/100 centavos"
        If Len(num2str) > 68 Then
            Dim b As Boolean = True
            Dim i As Integer = 68
            While b = True
                If num2str.Chars(i) <> " " Then
                    i -= 1
                Else
                    b = False
                End If
            End While
            num2str = Microsoft.VisualBasic.Left(num2str, i) & Chr(13) & Right(num2str, Len(num2str) - i)
        End If
    End Function
    Public Sub delbutton(idventa As String) ''borrar el boton de venta
        For Each c As Button In main.panelventbut.Controls
            If c.Text = idventa Then
                Dim ide As Integer = Val(Microsoft.VisualBasic.Right(c.Text, 1))
                '   c.Tag.dispose()
                c.Dispose()
                freeventspace(ide)
                Exit Sub
            End If
        Next
    End Sub
    Public Function onlyfechasql(ByVal fecha As Date) As String
        onlyfechasql = fecha.ToString("yyyy-MM-dd")
    End Function
    Public Function nowsql() As String ''coloca now en format fecha sql
        nowsql = Now.ToString("yyyy-MM-dd HH:mm:ss tt")
    End Function
    Public Function horasql(fecha As Date) As String
        horasql = fecha.ToString("hh:mm:ss tt")

    End Function
    Public Function fechasql(ByVal fecha As Date) As String ''coloca la fecha en formato sql
        fechasql = fecha.ToString("yyyy-MM-dd HH:mm:ss")
    End Function

    Public Function formatdate(fechas As Date)
        formatdate = Format(fechas, "d/M/yy H:mm tt")
    End Function

    Public Function achora(hora As Date) As String
        Dim st As String
        If hora.Hour < 10 Then
            st = "0" & hora.Hour
        Else
            st = hora.Hour
        End If
        st &= ":" & hora.Minute
        achora = st
    End Function

    Public Sub colorventcode(ByVal id As Integer, ByRef ob As Object, ByRef boton As Button)
        ob.Ventaopen.BackColor = colorcodes(id - 1)
        ob.dg1.BackgroundColor = colorcodes(id - 1)
        boton.BackColor = colorcodes(id - 1)

    End Sub

    Public Sub colorrepcode(ByVal id As Integer, ByRef ob As Object, ByRef boton As Button)
        ob.panelsup.BackColor = colorcodes(id - 1)
        '  ob.tabe.BackgroundColor = colorcodes(id - 1)

        boton.BackColor = colorcodes(id - 1)

    End Sub

    Public Sub initvarvent()
        Dim i As Integer
        For i = 0 To nventtotal - 1
            nidvent(i) = False
        Next
    End Sub
    Public Function foundventab() As Integer
        foundventab = 0
        Dim i As Integer
        For i = 0 To nventtotal - 1
            If Not nidvent(i) Then
                foundventab = i + 1
                Exit Function
            End If
        Next
    End Function
    Public Sub markventspace(ByVal id As Integer)
        nidvent(id - 1) = True
    End Sub
    Public Sub freeventspace(ByVal id As Integer)
        nidvent(id - 1) = False
    End Sub
    Public Function newrepwin(Optional venta As Boolean = False) As Object
        Dim idvent As Integer = foundventab()
        If idvent > 0 Then
            Dim nbutton As botonventaefrente = New botonventaefrente

            Dim o As reparacion = New reparacion
            nbutton.setbuttonrep(o, idvent)
            main.panelventbut.Controls.Add(nbutton)

            ' nbutton.setbutton(o, 2, idvent)
            '  AddHandler nbutton.Click, AddressOf o.panelrep.BringToFront
            o.loader()
            o.idventa = idvent
            o.idventa = nbutton.Text
            '  nbutton.Tag = o.panelrep
            markventspace(idvent)
            colorrepcode(idvent, o, nbutton)
            newrepwin = o
        Else
            dialogs.advertencia("Se alcazaron el máximo de ventas o reparaciones")
            newrepwin = main
        End If

    End Function

    Public Function newventawin(Optional clave As String = "") As Object
        newventawin = main
        Dim idvent As Integer = foundventab()
        If idvent > 0 Then
            Dim nbutton As botonventaefrente = New botonventaefrente


            Dim o As formvent = New formvent
            'AddHandler nbutton.Click, AddressOf o.panelventa.BringToFront
            nbutton.setbuttonventa(o, idvent)
            main.ppos.Controls.Add(o.panelventa)
            o.panelventa.BringToFront()
            o.loadervennew()
            o.idventa = nbutton.Text
            '  nbutton.Tag = o.panelventa
            markventspace(idvent)
            colorventcode(idvent, o, nbutton)
            newventawin = o
            If clave <> "" Then
                Dim art As articuloventa = New articuloventa(clave)
                art.cantidad = 1
                o.additemdg(art, 0)
                o.dg1.Rows.Add()
                o.dg1.CurrentCell = o.dg1(0, 1)
            End If
        Else
            dialogs.Close()

            dialogs.advertencia("Se alcazaron el máximo de ventas o reparaciones")
        End If



    End Function
    'Public Sub nbuttonclick(sender As Object, e As EventArgs)
    '    Dim b As Object = sender.tag
    '    b.BringToFront()

    'End Sub
    Public Sub butopencolor(ByRef buttone As Button) ''cambia a rojo el boton. Es para saber cual es el activo
        buttone.BackColor = Color.Red
        buttone.ForeColor = Color.White
    End Sub

    Public Sub butwaitcolor(ByRef buttone As Button)
        buttone.BackColor = Color.SlateGray
        buttone.ForeColor = Color.White
    End Sub
    Public Sub butnoactivecolor(ByRef buttone As Button)
        buttone.BackColor = Color.FromName("Control")
        buttone.ForeColor = Color.Black
    End Sub

    Public Sub maintrans() ''opaca el formulario main
        main.Opacity = 0.85
    End Sub
    Public Sub mainnormal() ''vuelve a la normalidad el formulario main
        main.Opacity = 1
    End Sub
    Function tpsplit(ByVal st As String) As Decimal 'regresa solo el precio del tipo de precio
        If st <> "" Then
            Dim sep = st.Split(":")
            tpsplit = sep(1)
        Else
            tpsplit = 0
        End If
    End Function
    Public Sub loadchild(ByRef ventana As Form)
        If ventana.Visible = False Then


        Else
            ventana.BringToFront()
        End If

    End Sub

    Public Function stprintadapter(st As String, nmax As Integer, f As Font, ByVal e As Object, ByRef saltos As Integer) As String ''adapta el texto al tamaño especificado, solo impresión

        Dim stn As Integer = e.MeasureString(st, f).Width
        saltos = 0
        If stn <= nmax Then
            stprintadapter = st
        Else
            Dim st2() As String = st.Split(" ")
            Dim st3 As String = ""
            Dim nst As String = 0
            For i = 0 To st2.Count - 1
                If (e.MeasureString(st2(i), f).Width + nst) > nmax Then
                    st3 &= vbNewLine & st2(i) & " "
                    nst = e.MeasureString(st2(i) & " ", f).Width
                    saltos += 1

                Else
                    st3 &= st2(i) & " "
                    nst += e.MeasureString(st2(i) & " ", f).Width
                End If
            Next
            stprintadapter = st3
        End If
    End Function
    Public Function getclnname(cliente As String) As String ''obtiene el nombre del cliente
        getclnname = cliente
        If cliente.IndexOf(":") >= 0 Then
            Dim st() As String = cliente.Split(":")
            getclnname = st(1)
        End If


    End Function
    Public Function getclnidint(cliente As String) As Integer ''Separa el id del cliente y el nombre
        Dim st() As String = cliente.Split(":")
        getclnidint = st(0)

    End Function
    Public Sub cerrarventasabiertas()
        '     panelventabiert.Dispose()
        ventalisopen = False
    End Sub
    Public Sub numpointevalper(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        'Allow Numeric values, BackSpace key. Disallows decimal point (i.e. dot) 
        'Dim numbers As Windows.Forms.TextBox = sender
        If InStr("1234567890.%pxsSaAPXtT", e.KeyChar) = 0 And Asc(e.KeyChar) <> 8 Then
            e.KeyChar = Chr(0)
            e.Handled = True
        End If
    End Sub
    Public Function fpesos(st As Decimal) As String ''formatea a pesos
        fpesos = Format(st, "$0.00")
    End Function
    Public Function ivades(n As Decimal) As Decimal ''desglosa el iva
        ivades = Math.Round(n - (n / 1.16), 2)
    End Function
    Public Function getventrepid(typeid As Integer, fecha As Date) As String ''obtiene el ultimo id de venta o reparacion
        Dim idlastnum As Integer = 0
        Dim stnumid As String = fecha.ToString("yyMMdd")
        '   Dim ttienda As String = ""
        Dim typenota As String = ""
        Dim st As String = ""

        Select Case typeid
            Case 1
                typenota = "V"
                st = "SELECT Folio FROM venta WHERE Folio LIKE '" & tienda.apodtiny & typenota & stnumid & "%' ORDER BY Folio ASC"
                orden = New OleDb.OleDbCommand(st, convent)

            Case 2
                typenota = "R"
                st = "SELECT Folio FROM nota WHERE Folio LIKE '" & tienda.apodtiny & typenota & stnumid & "%' ORDER BY Folio ASC"
                orden = New OleDb.OleDbCommand(st, conbicrep)
            Case 3
                typenota = "A"
                st = "SELECT Folio FROM Apartado WHERE Folio LIKE '" & tienda.apodtiny & typenota & stnumid & "%' ORDER BY Folio ASC"
                orden = New OleDb.OleDbCommand(st, convent)

        End Select
        lector = orden.ExecuteReader
        getventrepid = ""
        While lector.Read
            idlastnum = Val(Microsoft.VisualBasic.Right(lector("Folio"), 3))
        End While
        lector.Close()
        idlastnum = idlastnum + 1
        getventrepid = tienda.apodtiny & typenota & stnumid & idlastnum.ToString("000")


    End Function

    Function checkerventa(ByRef dgc As DataGridView) As Boolean ''checa la venta si hay errores
        checkerventa = False
        If dgc.Rows.Count = 0 Then
            dialogs.Close()

            dialogs.advertencia("No ha introducido artículos")
            Exit Function
        End If
        For i = 0 To dgc.Columns.Count - 1
            For j = 0 To dgc.Rows.Count - 1
                If dgc(i, j).ErrorText <> "" Then
                    dialogs.Close()

                    dialogs.advertencia("Resuelva primero los errores en la venta")
                    dgc.CurrentCell = dgc(i, j)
                    Exit Function
                End If
            Next
        Next
        checkerventa = True
    End Function
    Public Sub sumarbicirep(dg As DataGridView) ''suma los datos de los datagrid y los coloca en el venta correspondiente
        Dim i As Integer = dg.Tag
        '  Dim a As Object = panelsel.controls.item("bicis").SelectedTab.controls.item("panelbicides").controls.item("totallabeld")
        '  Dim b As Object = panelsel.controls.item("bicis")
        Dim totalt As Decimal = 0
        Dim sdummy As Label = New Label
        Dim tdummy As Label = New Label
        sumaventa(dg, 0, 0, totalt, sdummy, tdummy)
        '  a.text = "$ " & totalt
        Dim totalrep As Decimal = 0
        'For i = 0 To b.TabCount - 1
        '     totalrep += labelprixnum(b.TabPages.Item(i).Controls.Item("panelbicides").Controls.Item("totallabeld").Text)
        ' Next
        'panelmenuselected.controls.item("totalgb").controls.item("total").text = "$ " & totalrep


    End Sub


    Public Sub sumaventa(ByVal dg As DataGridView, ByVal adeudo As Decimal, ByRef subtotal As Decimal, ByRef total As Decimal, Optional ByRef subtotallabel As Object = Nothing, Optional ByRef totallabel As Object = Nothing)
        Dim suma As Decimal = 0
        For i = 0 To dg.Rows.Count - 1
            If Val(dg("impv", i).Value) <> 0 Then
                suma = suma + dg("impv", i).Value
            End If
        Next
        subtotal = suma
        ' If Not subtotallabel.Equals(Nothing) Then
        'subtotallabel.text = "$ " & subtotal
        'End If

        total = suma + adeudo
        '  If Not totallabel.Equals(Nothing) Then
        '  totallabel.Text = total
        ' End If

    End Sub
    Public Sub textnumonly(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        'Allow Numeric values, BackSpace key. Disallows decimal point (i.e. dot) 
        '  Dim numbers As Windows.Forms.TextBox = sender
        If InStr("1234567890.", e.KeyChar) = 0 And Asc(e.KeyChar) <> 8 Then
            e.KeyChar = Chr(0)
            e.Handled = True
        End If
    End Sub
    Public Sub textnumonlypoint(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        'Allow Numeric values, BackSpace key. Disallows decimal point (i.e. dot) 
        '  Dim numbers As Windows.Forms.TextBox = sender
        If InStr("1234567890.,", e.KeyChar) = 0 And Asc(e.KeyChar) <> 8 Then

            e.KeyChar = Chr(0)
            e.Handled = True
        End If
    End Sub
    Public Sub tpchanger(ByVal cant As Integer, ByVal tp As String, ByRef datimp As Decimal) 'obtiene el valor del tipo de precio y lo multiplica por la cantidad para sacar el importe
        If tp <> "" Then
            Dim sep = tp.Split(":")
            Dim precio As Decimal = sep(1)
            Dim total As Decimal = cant * precio
            datimp = total
        End If
    End Sub
    Function daystring(ByVal idcre As Boolean) As String
        Dim mes As String = Now.Month.ToString
        Dim day As String = Now.Day.ToString
        If Len(mes) = 1 Then
            mes = "0" & mes
        End If
        If Len(day) = 1 Then
            day = "0" & day
        End If
        If idcre Then
            daystring = day & mes & Now.Year
        Else
            daystring = day & "/" & mes & "/" & Now.Year
        End If

    End Function
    Function fecha() As String
        fecha = Now.ToShortDateString

    End Function
    Function limittextbox(ByVal chara As String, ByVal nlimit As Integer, ByVal st As String)
        limittextbox = chara
        If Asc(chara) <> 8 Then
            If st.Length >= nlimit Then
                limittextbox = ""
            End If
        End If
    End Function
    Function returnpercent(st As String, precio As Decimal, Optional pagof As Boolean = False) As Decimal ''regresa el porcentaje
        Dim stper As String = st
        If stper.Contains("%") Or stper.Contains("x") Or stper.Contains("X") Or stper.Contains("p") Or stper.Contains("P") Then
            stper = stper.Replace("%", "")
            stper = stper.Replace("p", "")
            stper = stper.Replace("P", "")
            stper = stper.Replace("x", "")
            stper = stper.Replace("X", "")
            returnpercent = precio * Val(stper) / 100
            If pagof = False Then
                returnpercent += precio
            End If
        ElseIf stper.Contains("+") Then
            stper = stper.Replace("+", "")
            returnpercent = Val(stper) + precio
        Else
            returnpercent = st
        End If


    End Function
    Public Sub leavenum(ByRef box As Object)
        box.text = Val(box.Text)
    End Sub
    Public Sub leavenumesp(ByRef box As Object, ByVal stv As String)
        Dim st As String = box.text
        If InStr("+%pPxX", st) = 0 Then
            box.text = Val(returnpercent(st, Val(stv)))
        End If

    End Sub
    Function numblock(ByRef st As String, Optional point As Boolean = False, Optional cesp As Boolean = False, Optional ByVal sttext As String = "") As String ''permite solo aceptar numeros
        numblock = ""
        If IsNumeric(st) Or Asc(st) = 8 Then
            numblock = st
        End If
        If point = True And st = "." Then
            numblock = checkpoint(st, sttext)
        End If
        If cesp Then
            If InStr("+%pPxX", st) >= 0 Then
                numblock = st
            End If
        End If
    End Function
    Public Function adapterprint(st As String) As String
        ''adapta el precio para la impresión
        Dim uni As String = st
        Dim dec As String = "00"
        If st.IndexOf(".") > 0 Then
            Dim s = st.Split(".")
            uni = s(0)
            dec = Left(s(1), 2)
            If Len(dec) = 1 Then
                dec = dec & "0"
            End If


        End If
        adapterprint = uni & "." & dec
    End Function
    Public Function returnpreonly(st As String) As Decimal
        ''Regresa el precio solamente
        If st <> "" Then
            Dim s = st.Split(":")
            returnpreonly = Val(s(1))
        Else
            returnpreonly = 0
        End If

    End Function
    Public Function checkpoint(ByRef chare As String, ByVal st As String) As String
        If st.IndexOf(".") > -1 And chare = "." Then
            checkpoint = ""
        Else
            checkpoint = chare
        End If
    End Function

    ''' <summary>
    ''' Sanitiza una cadena para incluirla de forma segura en consultas SQL, escapando apóstrofes dobles.
    ''' </summary>
    Public Function sqlSafe(ByVal texto As Object) As String
        If texto Is Nothing OrElse DBNull.Value.Equals(texto) Then
            Return ""
        End If
        Return texto.ToString().Replace("'", "''")
    End Function

    ''' <summary>
    ''' Registra un error de manera segura en un archivo de log C:\sc3\crash.log sin lanzar excepciones secundarias.
    ''' </summary>
    Public Sub LogAppError(ByVal ex As Exception, Optional ByVal origen As String = "")
        Try
            Dim logDir As String = "C:\sc3"
            If Not System.IO.Directory.Exists(logDir) Then
                System.IO.Directory.CreateDirectory(logDir)
            End If
            Dim logPath As String = System.IO.Path.Combine(logDir, "crash.log")
            Dim msg As String = String.Format("[{0:yyyy-MM-dd HH:mm:ss}] [{1}] {2}{3}{4}{5}",
                                              DateTime.Now,
                                              If(String.IsNullOrEmpty(origen), "GENERAL", origen),
                                              If(ex IsNot Nothing, ex.Message, "Desconocido"),
                                              Environment.NewLine,
                                              If(ex IsNot Nothing, ex.StackTrace, ""),
                                              Environment.NewLine & New String("-"c, 60) & Environment.NewLine)
            System.IO.File.AppendAllText(logPath, msg, System.Text.Encoding.UTF8)
        Catch
            ' Silencioso para prevenir errores en cascada
        End Try
    End Sub

End Module