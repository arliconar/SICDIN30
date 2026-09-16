Imports System.Threading
Imports System.Data.OleDb
'Imports System.Windows.Forms.Design

'Imports System.IO

'Imports Microsoft.VisualBasic.ApplicationServices

Public Module bases

    Public dgs As BindingSource
    Public foliocancelar As String = ""
    Public idcancelar As Integer = 0

    Public foliocancelar2 As String = ""

    Public Sub autocompletarclnt() 'Iniciarlización Autocompletar de articulos
        Try
            Dim items As New List(Of String)()
            SyncLock conclnt
                Using cmd As New OleDb.OleDbCommand("SELECT Id, Nombre FROM client", conclnt)
                    Using rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()
                        While rdr.Read()
                            items.Add(rdr("Nombre") & ":" & rdr("Id"))
                            items.Add(rdr("Id") & ":" & rdr("Nombre"))
                        End While
                    End Using
                End Using
            End SyncLock
            aclnt.Clear()
            For Each itm In items
                aclnt.Add(itm)
            Next
        Catch ex As Exception
            LogAppError(ex, "basesmod.autocompletarclnt")
        End Try
    End Sub
    Public Function voiddeleterstring(lector As Object, Optional defecto As String = "") As String
        ''elimina el vacio de la base de datos para strings
        If Not DBNull.Value.Equals(lector) Then
            voiddeleterstring = lector
        Else
            voiddeleterstring = defecto
        End If
    End Function

    Public Function voiddeleter(lector As Object) As Decimal
        ''elimina el vacio de la base de datos para numeros
        If Not DBNull.Value.Equals(lector) Then
            voiddeleter = lector
        Else
            voiddeleter = 0
        End If
    End Function
    Enum stcod
        Nada
        Pmenudeo
        Pmayoreo
        Existencias
    End Enum
    Public Function searart(ByVal st As String, existentes As Boolean, etiquetas As Boolean, trabajo As Boolean, fecha As String, ByRef found As Boolean) As BindingSource

        Dim orden As OleDb.OleDbCommand
        Dim lector As OleDb.OleDbDataReader
        Dim stcodigo As stcod = stcod.Nada
        searart = New BindingSource
        Dim sta As String = ""
        Dim st2 As String = st.Replace(" ", ",")
        Dim stcut() As String = st2.Split(",")
        Dim stdes As String = ""
        Dim sttype As String = ""
        Dim sttag As String = ""
        Dim stspecial As String = ""
        For i = 0 To stcut.Count - 1
            If stcut(i) <> "" Then
                If stcut(i)(0) = "$" Then
                    If stcut(i).Contains("$$") Then
                        stspecial = stcut(i).Replace("$$", "")
                        stcodigo = stcod.Pmayoreo
                    Else
                        stspecial = stcut(i).Replace("$", "")
                        stcodigo = stcod.Pmenudeo

                    End If


                ElseIf stcut(i)(0) = "#" Then
                        stspecial = stcut(i).Replace("#", "")
                        stcodigo = stcod.Existencias

                    Else
                        If i > 0 Then
                        stdes &= " AND "
                        sttag &= " AND "
                    End If
                    stdes &= "Descripción LIKE '%" & stcut(i) & "%'"
                    sttag &= "TAGS LIKE '%" & stcut(i) & "%'"
                End If
            End If
        Next


        sta = "SELECT Clave, Descripción,Paquete, Menudeo, Mayoreo, Especial,PME,Existencias, FUM FROM Articulos WHERE "
        If st.Contains(" ") Then
            sta &= "(" & stdes & ")"

        Else
            sta &= "(Clave LIKE '%" & st & "%' OR cbarra='" & st & "'"
            If stdes <> "" Then
                sta &= " OR " & stdes
            End If
            If etiquetas And sttag <> "" Then
                sta &= " AND " & sttag
            End If
            sta &= ")"

        End If

        If existentes Then
            sta &= " AND (Existencias>0 OR Einf=TRUE)"
        End If
        If trabajo Then
            sta &= " AND Tipo='Trabajo'"
        End If
        If stspecial.Length > 2 Then
            Select Case stcodigo
                Case stcod.Pmayoreo
                    sta &= " AND Mayoreo" & stspecial
                Case stcod.Pmenudeo

                    sta &= " AND Menudeo" & stspecial
                Case stcod.Existencias
                    sta &= " AND Existencias" & stspecial
            End Select

        End If

        sta &= fecha & " AND Del=FALSE ORDER BY Descripción"
        orden = New OleDb.OleDbCommand(sta, conart)
        Try
            lector = orden.ExecuteReader
            found = lector.HasRows

            searart.DataSource = lector
            lector.Close()
        Catch ex As Exception

        End Try

    End Function
    Public Sub searresf(ByRef dg As DataGridView, nota As Boolean) ''busca las bicis que aun faltan por reparar
        Dim ds As BindingSource = New BindingSource
        Dim st As String

        If nota Then
            st = "SELECT Cliente,Fhentrega,Folio,Id,Rodada,Marca,Color FROM biciconsulta WHERE bentregada=FALSE AND bfinalizada=FALSE AND bcancelada=FALSE AND Almacenada=FALSE AND folio LIKE '%R%' ORDER BY Fhentrega"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            lector = orden.ExecuteReader
            Dim blector As Boolean = lector.HasRows
            ds.DataSource = lector
            lector.Close()
            dg.Rows.Clear()

            If blector Then
                dg.DataSource = ds
                dg.Columns.Item("Fhentrega").HeaderText = "F.Entrega"
                dg.Columns.Item("Cliente").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dg.Columns.Item("Fhentrega").Visible = False
                dg.Columns.Item("Folio").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dg.Columns.Item("Entrega").Visible = True
                If dg.Rows.Count > 0 Then
                    Dim fechar As Date
                    For i = 0 To dg.Rows.Count - 1
                        fechar = dg("Fhentrega", i).Value
                        Dim ni As Integer = fechar.AddDays(-Now.Day).Day
                        If fechar.Date = Now.Date Then
                            dg("Entrega", i).Value = "Hoy " & fechar.ToString("hh:mm tt")
                        Else
                            If ni <= 6 Then
                                dg("Entrega", i).Value = diasemana(fechar.DayOfWeek) & " " & fechar.ToString("hh:mm tt")
                            Else
                                dg("Entrega", i).Value = fechar
                            End If
                        End If

                        dg("Entrega", i).ToolTipText = fechar


                        If fechar.ToShortDateString = Now.ToShortDateString Then
                            dg.Rows.Item(i).DefaultCellStyle.BackColor = Color.Green
                        End If
                        If fechar < Now Then
                            dg.Rows.Item(i).DefaultCellStyle.BackColor = Color.YellowGreen

                        End If
                        If Date.Compare(fechar.ToShortDateString, Now.ToShortDateString) < 0 Then
                            dg.Rows.Item(i).DefaultCellStyle.BackColor = Color.OrangeRed
                        End If

                    Next
                End If
            End If


        Else
            st = "SELECT Fecha,Folio,Cliente,Total FROM nota WHERE bentregada=FALSE AND Finalizadaall=FALSE AND Fcan=FALSE and Almacenada=FALSE ORDER BY Fcreacion"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            lector = orden.ExecuteReader
            Dim blector As Boolean = lector.HasRows
            ds.DataSource = lector
            lector.Close()
            dg.Rows.Clear()
            dg.Columns.Item("Entrega").Visible = False
            If blector Then
                dg.DataSource = ds
                dg.Columns.Item("Fecha").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dg.Columns.Item("Folio").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dg.Columns.Item("Cliente").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            End If
        End If

    End Sub

    Public Sub loadalmacenadas(ByRef dg As DataGridView, bici As Boolean)
        Dim st As String
        If bici Then
            st = "SELECT Fechae,Horae,Folio,Id,Rodada,Marca,Color,Nota,Fhentrega,Ralmacenada FROM biciconsulta WHERE Almacenada=TRUE ORDER BY Fhentrega"
        Else
            st = "SELECT Fecha,Hora,Folio,Cliente FROM nota WHERE Almacenada=TRUE ORDER BY Fecha"
        End If
        dg.Columns.Item("Entrega").Visible = False
        dg.Rows.Clear()
        Dim ds As BindingSource = New BindingSource
        orden = New OleDb.OleDbCommand(st, conbicrep)
        lector = orden.ExecuteReader
        If lector.HasRows Then
            ds.DataSource = lector
            dg.DataSource = ds
            If bici Then
                dg.Columns.Item("Fechae").HeaderText = "Fecha"
                dg.Columns.Item("Fechae").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dg.Columns.Item("Horae").HeaderText = "Hora"
                dg.Columns.Item("Fhentrega").Visible = False
                dg.Columns.Item("Ralmacenada").HeaderText = "Razón"
            End If
        End If
        lector.Close()

    End Sub
    Public Function delvoiddb(st As Object) As String
        Dim str As String = ""
        If Not DBNull.Value.Equals(st) Then
            str = st
        End If
        Return str
    End Function
    Public Function sart(ByVal sts As String, Optional exact As Boolean = False) As articuloventa
        'Dim orden As OleDb.OleDbCommand
        'Dim lector As OleDb.OleDbDataReader
        'Dim notreg As Boolean = False
        'Dim tnot As Integer = 0
        'Dim st As String = ""
        'Dim cant As Decimal
        'sart = New articuloventa
        'With sart
        '    If sts.IndexOf(":") Then
        '        Dim std() As String = sts.Split(":")
        '        st = std(0)
        '    Else
        '        st = sts
        '    End If
        '    Dim stp As String = st.ToUpper
        '    sart.found = False
        '    artnot.Close()
        '    If stp = "ARTÍCULO NO REGISTRADO" Or stp = "ARNORE" Or stp = "ARTICULO NO REGISTRADO" Then
        '        notreg = True
        '        artnot.loaderart()
        '        tnot = 1
        '    ElseIf stp = "TRNORE" Or stp = "TRABAJO NO REGISTRADO" Then
        '        notreg = True
        '        artnot.loadertra()
        '        tnot = 2
        '    End If

        '    If Not notreg Then
        '        Dim sttemp As String = ""
        '        Dim st2 As String = ""
        '        If Not st.Contains(" ") Or exact Then
        '            st2 = "SELECT * FROM articulos WHERE clave='" & st & "' OR cbarra='" & st & "' AND Del=False"

        '            orden = New OleDb.OleDbCommand(st2, conart)
        '            lector = orden.ExecuteReader
        '            If lector.HasRows Then
        '                copylectortoart(lector, sart)
        '                lector.Close()
        '                Exit Function
        '            End If
        '            lector.Close()
        '            sttemp = "Or descripción Like '%" & st & "%'"
        '        Else
        '            Dim stcut() As String = st.Split(" ")
        '            For i = 0 To stcut.Count - 1
        '                sttemp &= " Or descripción Like '%" & stcut(i) & "%'"
        '            Next
        '        End If
        '        st2 = "SELECT * FROM articulos WHERE clave LIKE '" & st & "%' " & sttemp & " AND Del=FALSE ORDER BY descripción"
        '        orden = New OleDb.OleDbCommand(st2, conart)
        '        lector = orden.ExecuteReader
        '        If lector.HasRows Then
        '            cant = copylectortoart(lector, sart)
        '            If cant > 1 And Not exact Then
        '                sart = catar.loadcatadialog(sts)
        '            End If
        '        End If

        '    Else
        '        If artnot.ShowDialog() = DialogResult.OK Then
        '            Select Case tnot
        '                Case 1
        '                    .clave = "ARNORE"
        '                    .einf = True
        '                    .tipo = "Articulo"

        '                Case 2
        '                    .clave = "TRNORE"
        '                    .einf = False
        '                    .tipo = "Trabajo"
        '            End Select
        '            .found = True

        '        End If
        '    End If
        'End With

        sart = New articuloventa(sts)
        If Not sart.found Then
            sart = catar.loadcatadialog(sts)
            If catar.DialogResult = DialogResult.Cancel Then
                sart.found = False
                sart = New articuloventa

            End If
        End If
    End Function
    Public Function copylectortoart(ByRef lector As OleDb.OleDbDataReader, ByRef art As articuloventa) As Integer
        With art
            copylectortoart = 0
            While lector.Read
                copylectortoart += 1
                .found = True
                .clave = lector("clave")
                .descripcion = lector("descripción")
                .menudeo = lector("Menudeo")
                .mayoreo = lector("Mayoreo")
                If IsDBNull(lector("notas")) Then
                    .notas = ""
                Else
                    .notas = lector("notas")
                End If

                If IsDBNull(lector("Especial")) Then
                    .especial = 0
                Else
                    .especial = Val(lector("Especial"))
                End If
                If IsDBNull(lector("Existencias")) Then
                    .existencias = 0
                Else
                    .existencias = Val(lector("Existencias"))
                End If


                If IsDBNull(lector("Paquete")) Then
                    .pq = "PZA"
                Else
                    .pq = lector("Paquete")
                End If

                If IsDBNull(lector("precio")) Then
                    .precio = 0
                Else
                    .precio = Val(lector("precio"))

                End If

                If IsDBNull(lector("PME")) Then
                    .pme = 0
                Else
                    .pme = Val(lector("PME"))
                End If
                If IsDBNull(lector("ea1")) Then
                    .ea1 = 0
                Else
                    .ea1 = Val(lector("ea1"))

                End If

                If IsDBNull(lector("ea2")) Then
                    .ea2 = 0
                Else
                    .ea2 = Val(lector("ea2"))
                End If
                .tipo = lector("Tipo")

                .einf = Val(lector("einf"))

            End While
        End With

    End Function
    Public Function cancelarbici(ByVal folio As String, Optional id As Integer = 0)

        cancelarbici = False
        dialogs.Close()
        If dialogs.pregunta("Cancelar una bicicleta es irreversible" & vbNewLine & "¿Desea continuar?") = DialogResult.No Then
            Exit Function
        End If

        If Not razon.loadercancel = DialogResult.OK Then
            Exit Function
        End If
        cancelarbici = True


        foliocancelar = folio


        idcancelar = id


        Dim hilo As Thread = New Thread(New ThreadStart(AddressOf cancelarbicithread))
        hilo.Start()
    End Function

    Public Sub entregarapartado(folio As String)
        Dim st As String = "UPDATE Apartado SET Fentregado='" & Now & "' WHERE Folio='" & folio & "'"
        Dim orden As OleDbCommand = New OleDbCommand(st, convent)
        orden.ExecuteNonQuery()

    End Sub
    Public Function getwhatsrep(folio As String) As String
        Dim st As String = "SELECT telcel FROM nota WHERE Folio='" & folio & "'"
        Dim orden As OleDbCommand = New OleDbCommand(st, conbicrep)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        Dim telcel As String = ""
        While lector.Read
            telcel = voiddeleterstring(lector("telcel"))
        End While
        lector.Close()

        getwhatsrep = telcel



    End Function
    Public Sub cancelarapartado(folio As String)
        Dim st As String = "UPDATE Apartado SET Fcancelado='" & Now & "' WHERE Folio='" & folio & "'"
        Dim orden As OleDbCommand = New OleDbCommand(st, convent)
        orden.ExecuteNonQuery()
        st = "SELECT Clave,Cantidad FROM Articulos WHERE Folio='" & folio & "'"
        orden = New OleDbCommand(st, convent)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        While lector.Read
            Dim art As articuloventa = New articuloventa(lector("Clave"))
            art.modexistencias(lector("Cantidad"), False)
            'redexistencias(lector("Clave"), lector("Cantidad"), add)
        End While
        lector.Close()

    End Sub
    Public Sub updatelastventa(clave As String)
        Dim st As String = "UPDATE Articulos SET Fuc='" & Now & "' WHERE Clave='" & clave & "'"
        Dim orden As OleDbCommand = New OleDbCommand(st, conart)
        orden.ExecuteNonQuery()



    End Sub




    Public Sub addretiro(cantidad As Decimal, razon As String)
        Dim st As String = "INSERT INTO lv (Descripcion,Total,usuario,fecha,hora,horast,pagado,pagoefe) VALUES ('Retiro de efectivo" & razon & " '," & -cantidad & ",'" & user.idnameusu & "','" & onlyfechasql(Today) & "','" & horasql(Now) & "','" & horasql(Now) & "'," & -cantidad & "," & -cantidad & ")"
        Dim orden As OleDbCommand = New OleDbCommand(st, convent)
        orden.ExecuteNonQuery()


    End Sub
    Public Sub cancelvenr(fecha As Date)
        Dim st As String = "UPDATE lv SET cancel=true WHERE fecha=#" & onlyfechasql(fecha) & "# AND horast='" & horasql(fecha) & "'"
        Dim orden As OleDbCommand = New OleDbCommand(st, convent)
        orden.ExecuteNonQuery()

    End Sub
    Public Sub delventr(fecha As Date)
        Dim st As String = "DELETE FROM lv WHERE fecha=#" & onlyfechasql(fecha) & "# AND horast='" & horasql(fecha) & "'"
        Dim orden As OleDbCommand = New OleDbCommand(st, convent)
        orden.ExecuteNonQuery()

    End Sub
    Public Sub delart(clave As String) ''elimina un articulo
        Dim art As articuloventa = New articuloventa
        art.load(clave, True)
        dialogs.Close()

        If dialogs.pregunta("¿Desea eliminar este artículo?" & vbNewLine & "Clave:" & art.clave & vbNewLine & "Descripción:" & art.descripcion) = vbYes Then
            art.deleteme()

        End If

    End Sub



    Public Sub loadrazon(folio As String, id As Integer)
        Dim orden As OleDb.OleDbCommand
        Dim lector As OleDb.OleDbDataReader
        Dim st As String = "SELECT Rcancel FROM bici WHERE Folio='" & folio & "' AND Id=" & id & " AND bcancelada=TRUE"
        orden = New OleDb.OleDbCommand(st, conbicrep)
        lector = orden.ExecuteReader
        Dim razonst As String = ""
        While lector.Read
            razonst = lector("Rcancel")
        End While
        lector.Close()
        If razonst <> "" Then
            razon.loaderrazon(razonst)
        Else
            dialogs.Close()
            dialogs.advertencia("Esta bicicleta no ha sido cancelada")
        End If

    End Sub
    Public Function suficientesexist(folio As String, ByRef artdes As String, id As Integer) As Boolean
        ''busca si hay suficientes existencias en las reparaciones antes de finalizar la bici
        Dim orden As OleDb.OleDbCommand
        Dim lector As OleDb.OleDbDataReader
        Dim orden2 As OleDb.OleDbCommand
        Dim lector2 As OleDb.OleDbDataReader
        Dim i As Integer = 0
        Dim existart As Integer = 0

        Dim st As String = "SELECT Cantidad,Clave FROM art WHERE Folio='" & folio & "'"
        Dim st2 As String = ""
        orden = New OleDb.OleDbCommand(st, conbicrep)
        lector = orden.ExecuteReader
        While lector.Read
            ''sumamos todas las existencias de los articulos con el mismo nombre
            existart = 0
            st2 = "SELECT Cantidad FROM art WHERE Folio='" & folio & "' AND Clave='" & lector("Clave") & "'"
            If id <> 0 Then
                st2 &= " AND Id=" & id
            End If
            orden2 = New OleDb.OleDbCommand(st2, conbicrep)
            lector2 = orden2.ExecuteReader
            While lector2.Read
                existart += Val(lector("Cantidad"))
            End While
            lector2.Close()

            ''buscamos en el catalogo el articulo y sus existencias
            st2 = "SELECT Existencias,Descripción,einf,Tipo FROM Articulos WHERE clave='" & lector("Clave") & "'"
            orden2 = New OleDb.OleDbCommand(st2, conart)
            lector2 = orden2.ExecuteReader
            While lector2.Read
                If (lector2("Existencias") < existart) And lector2("einf") = False And lector2("Tipo") <> "Trabajo" Then
                    artdes = "Clave:" & lector("Clave") & vbNewLine & "Descripcíón:" & lector2("Descripción") & vbNewLine & "Requeridos:" & existart & vbNewLine & "Existencias en tienda"
                    suficientesexist = False
                    Exit Function
                End If
            End While
            lector2.Close()
        End While
        lector.Close()
        ''si todo sale bien, regresamos un true
        suficientesexist = True

    End Function


    Public Sub almacenarbici(ByVal folio As String, ByVal razon As String, Optional id As Integer = 0, Optional almacen As Boolean = True)
        'primero actualizo las almacenadas/desalmacenadas
        Dim st As String = "UPDATE bici SET Almacenada=" & almacen & ",Ralmacenada='Usuario " & user.idnameusu & " el " & Now.ToLongDateString & " a las " & Now.ToLongTimeString & ": " & vbNewLine & razon & "' WHERE Folio='" & folio & "'"
        If id <> 0 Then
            st &= " AND Id=" & id
        End If
        orden = New OleDb.OleDbCommand(st, conbicrep)
        orden.ExecuteNonQuery()
        ''busco si alguna si hay desalmacenadas/almacenadas
        st = "SELECT Almacenada FROM bici WHERE Folio='" & folio & "' and Almacenada=" & Not almacen
        orden = New OleDb.OleDbCommand(st, conbicrep)
        lector = orden.ExecuteReader
        Dim bal As Boolean = lector.HasRows
        lector.Close()
        If Not bal Then
            st = "UPDATE nota SET Almacenada=" & almacen & ",Ralmacenada='" & razon & "' WHERE Folio='" & folio & "'"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            orden.ExecuteNonQuery()
        End If

    End Sub
    Public Sub dummyys()

    End Sub
    Public Sub delpago(ByVal folio As String, id As Integer)
        ''agrega pago a una reparación
        Dim st As String = "DELETE FROM pagos WHERE Folio='" & folio & "' AND Id=" & id
        Dim orden As OleDbCommand = New OleDbCommand(st, conbicrep)
        orden.ExecuteNonQuery()



    End Sub

    Public Sub addpago(ByVal folio As String, ByVal cliente As String, ByVal id As Integer, pago As pagos)
        ''agrega pago a una reparación
        Dim st As String = "INSERT INTO pagos(Folio,Id,Cantidad,Fecha,Usuario,Efectivo,Transferencia,Tarjeta) VALUES ('" & folio & "'," & id & "," & pago.pagado & ",'" & nowsql() & "','" & user.idnameusu & "'," & pago.efectivo & "," & pago.transferencia & "," & pago.tarjeta & ")"
        orden = New OleDb.OleDbCommand(st, conbicrep)
        orden.ExecuteNonQuery()
        st = "INSERT INTO lv(Clave,Cliente,Descripcion,cantidad,pagoefe,pagotrans,pagotar,hora,horast,fecha,nota,total,pagado,usuario) VALUES ('"
        st &= folio & "','" & cliente & "','"
        st &= "Abono reparación " & folio & "',1,"
        st &= pago.efectivo & "," & pago.transferencia & "," & pago.tarjeta & ",'"
        st &= horasql(Now) & "','" & horasql(Now) & "','" & fechasql(Today) & "',True," & pago.total & "," & pago.pagado & ",'" & user.idnameusu & "')"
        orden = New OleDb.OleDbCommand(st, convent)
        orden.ExecuteNonQuery()



    End Sub
    Public Sub cancelarbicithread()
        Try
            Dim strazon As String = ""
            Try
                strazon = razon.razont.Text
            Catch
                strazon = ""
            End Try
            Dim safeRazon As String = sqlSafe(strazon)
            Dim stt As String = ""
            If idcancelar <> 0 Then
                stt = " AND ID=" & idcancelar
                Dim stn As String = "UPDATE nota SET Fcan=TRUE,Fcancelada='" & nowsql() & "',Rcancel='" & safeRazon & "' WHERE Folio='" & sqlSafe(foliocancelar) & "'"
                SyncLock conbicrep
                    Using cmd As New OleDb.OleDbCommand(stn, conbicrep)
                        cmd.ExecuteNonQuery()
                    End Using
                End SyncLock
            End If

            Dim st As String = "UPDATE art SET cancel=TRUE WHERE Folio='" & sqlSafe(foliocancelar) & "'" & stt
            SyncLock conbicrep
                Using cmd As New OleDb.OleDbCommand(st, conbicrep)
                    cmd.ExecuteNonQuery()
                End Using
            End SyncLock

            st = "UPDATE bici SET bcancelada=TRUE,Cancelada='" & nowsql() & "',Rcancel='" & safeRazon & "' WHERE Folio='" & sqlSafe(foliocancelar) & "'" & stt
            SyncLock conbicrep
                Using cmd As New OleDb.OleDbCommand(st, conbicrep)
                    cmd.ExecuteNonQuery()
                End Using
            End SyncLock
        Catch ex As Exception
            LogAppError(ex, "basesmod.cancelarbicithread")
        End Try
    End Sub

    Public Sub desfinalizarbici(ByVal folio As String, Optional ByVal id As Integer = 0)
        Dim orden As OleDb.OleDbCommand
        Dim lector As OleDb.OleDbDataReader
        Dim st2 As String = ""
        If id <> 0 Then
            st2 = " AND Id=" & id
        End If
        Dim st As String = "UPDATE nota SET Finalizadaall=FALSE WHERE Folio='" & folio & "'"
        orden = New OleDb.OleDbCommand(st, conbicrep)
        orden.ExecuteNonQuery()
        st = "SELECT Cantidad,Clave FROM Artcancel WHERE Folio='" & folio & "'" & st2
        orden = New OleDb.OleDbCommand(st, conbicrep)
        lector = orden.ExecuteReader
        While lector.Read
            Dim arte As articuloventa = New articuloventa(lector("clave"))
            arte.modexistencias(lector("cantidad"), True)
        End While
        lector.Close()
        st = "UPDATE art SET colocado=FALSE WHERE Folio='" & folio & "'" & st2
        orden = New OleDb.OleDbCommand(st, conbicrep)
        orden.ExecuteReader()
        st = "UPDATE bici SET bfinalizada=FALSE WHERE Folio='" & folio & "'" & st2
        orden = New OleDb.OleDbCommand(st, conbicrep)
        orden.ExecuteReader()


    End Sub
    Public Function finalizarbici(ByVal folio As String, Optional ByVal id As Integer = 0, Optional ByRef finalist As String = "", Optional multi As Boolean = False, Optional ByRef trasel As Integer = 0) As Boolean 'finaliza las bicis
        Dim orden As OleDb.OleDbCommand
        Dim lector As OleDb.OleDbDataReader
        finalizarbici = False
        Dim artdes As String = ""
        If Not suficientesexist(folio, artdes, id) Then
            dialogs.Close()
            dialogs.advertencia("No hay suficientes existencias del artículo " & Chr(13) & artdes)
            Exit Function
        End If

        Dim tra As trabajador = New trabajador
        If trasel > 0 Then
            tra.load(trasel)
        Else
            tra = trabajadorsel.loader()
        End If
        trasel = tra.id
        If tra.loaded Then
            finalist = "Finalizada por " & tra.idname & " el " & Now
            Dim stt As String = ""
            If id <> 0 Then
                stt = " And ID=" & id
            End If
            Dim st As String = "Select cantidad, clave FROM Artconsulta WHERE Folio='" & folio & "'" & stt
            orden = New OleDb.OleDbCommand(st, conbicrep)
            lector = orden.ExecuteReader
            While lector.Read
                Dim art As articuloventa = New articuloventa(lector("clave"))
                art.modexistencias(lector("Cantidad"), False)

            End While
            lector.Close()
            st = "UPDATE bici SET bfinalizada=TRUE, Finalizada ='" & nowsql() & "',Realizo=' " & tra.idname & "' WHERE Folio='" & folio & "'" & stt
            orden = New OleDb.OleDbCommand(st, conbicrep)
            orden.ExecuteNonQuery()
            st = "UPDATE art SET colocado=TRUE,fecha='" & fechasql(Today) & "',Realizo=" & tra.id & ",Hora='" & horasql(Now) & "' WHERE Folio='" & folio & "' AND cancel=FALSE" & stt
            orden = New OleDb.OleDbCommand(st, conbicrep)
            orden.ExecuteNonQuery()

            st = "UPDATE trabajos SET fecha='" & fechasql(Today) & "',Hora='" & fechasql(Now) & "',realizo=" & tra.id & " WHERE Folio='" & folio & "'"
            orden = New OleDb.OleDbCommand(st, contra)
            orden.ExecuteNonQuery()



            st = "SELECT id FROM bici WHERE FOLIO='" & folio & "' AND bfinalizada=FALSE"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            lector = orden.ExecuteReader
            Dim blector As Boolean = lector.HasRows
            lector.Close()
            If Not blector Then
                st = "UPDATE nota SET Finalizadaall=TRUE WHERE Folio='" & folio & "'"
                orden = New OleDb.OleDbCommand(st, conbicrep)
                orden.ExecuteNonQuery()

            End If
            finalizarbici = True
        End If
    End Function

    Public Sub desentregar(ByVal folio As String, id As Integer)
        Dim orden As OleDb.OleDbCommand
        Dim st As String = "UPDATE bici SET bentregada=FALSE WHERE Folio='" & folio & "' AND Id=" & id
        orden = New OleDb.OleDbCommand(st, conbicrep)
        orden.ExecuteNonQuery()
    End Sub
    Public Function getfinalizadabici(folio As String, id As String, ByRef trabajador As String, ByRef fecha As Date)
        Dim orden As OleDb.OleDbCommand
        Dim lector As OleDb.OleDbDataReader
        Dim st As String = "SELECT Realizo,Finalizada FROM bici WHERE Folio='" & folio & "' AND Id=" & id & " AND bfinalizada=TRUE"
        orden = New OleDb.OleDbCommand(st, conbicrep)
        lector = orden.ExecuteReader
        getfinalizadabici = lector.HasRows
        While lector.Read
            trabajador = lector("realizo")
            fecha = lector("Finalizada")
        End While
        lector.Close()

    End Function
    Public Function markentregada(ByVal folio As String, Optional ByVal id As Integer = 0, Optional multi As Boolean = False, Optional ByRef trasel As Integer = 0) As Boolean 'marca todas las bicis como entregadas
        Dim orden As OleDb.OleDbCommand
        Dim orden3 As OleDb.OleDbCommand
        Dim lector3 As OleDb.OleDbDataReader
        Dim trabajador As String = ""
        Dim tra As trabajador = New trabajador
        markentregada = False
        Dim repa As repbic = New repbic
        repa.loadrep(folio)
        '       MessageBox.Show("Total=" & repa.total)
        '      MessageBox.Show("Total=" & repa.tpagado)
        Dim st As String
        If id = 0 Then
            ''primero verificamos si hay alguna bici sin finalizar
            st = "SELECT bfinalizada,Id FROM bici WHERE Folio='" & folio & "' AND bfinalizada=FALSE"
            orden3 = New OleDb.OleDbCommand(st, conbicrep)
            lector3 = orden3.ExecuteReader

            If lector3.HasRows Then
                If trasel > 0 Then
                    tra.load(trasel)
                Else
                    tra = trabajadorsel.loader()
                End If
                trasel = tra.id
                If tra.loaded = False Then
                    Exit Function
                End If
            End If
            While lector3.Read
                If Not finalizarbici(folio, lector3("id"), "") Then
                    Exit Function
                End If
            End While
            lector3.Close()
            st = "UPDATE bici SET bentregada=TRUE,Entrego=' " & user.idnameusu & "',Entregada='" & nowsql() & "' WHERE Folio='" & folio & "' AND bentregada=FALSE"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            orden.ExecuteNonQuery()
            st = "UPDATE nota SET bentregada=TRUE, Fechae='" & nowsql() & "' WHERE Folio='" & folio & "'"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            orden.ExecuteNonQuery()

        Else
            ''primero verificamos si la bici que vamos a entregar ya esta finalizada
            st = "SELECT bfinalizada FROM bici WHERE Folio='" & folio & "' AND bfinalizada=FALSE AND Id=" & id
            orden3 = New OleDb.OleDbCommand(st, conbicrep)
            tra = New trabajador
            lector3 = orden3.ExecuteReader
            If lector3.HasRows() Then
                If Not finalizarbici(folio, id) Then
                    Exit Function
                End If
            End If
            lector3.Close()
            st = "UPDATE bici SET bentregada=TRUE,Entrego='" & user.idnameusu & "',Entregada='" & nowsql() & "' WHERE Folio='" & folio & "' AND Id=" & id
            orden = New OleDb.OleDbCommand(st, conbicrep)
            orden.ExecuteNonQuery()
            ''ahora verificamos si todas las bicis han sido entregadas
            st = "SELECT bentregada FROM bici WHERE Folio='" & folio & "' AND bentregada=FALSE"
            orden3 = New OleDb.OleDbCommand(st, conbicrep)
            lector3 = orden.ExecuteReader
            If Not lector3.HasRows Then
                st = "UPDATE nota SET bentregada=TRUE, Fechae='" & nowsql() & "' WHERE Folio='" & folio & "'"
                orden = New OleDb.OleDbCommand(st, conbicrep)
                orden.ExecuteNonQuery()
            End If
            lector3.Close()

        End If
        markentregada = True
    End Function
    Public Sub searbici(ByRef dg As DataGridView, ByVal folio As String, ByVal name As String, ByVal fc As String, ByVal fe As String) ''busca bicis de reparación
        Dim b As Boolean = False
        Dim ds As BindingSource = New BindingSource
        Dim st As String = "SELECT Folio, Cliente, Fecha, Hora,Total, Fcan, Fcreacion, Fcancelada FROM nota WHERE"
        If folio <> "" Then
            st &= " Folio LIKE '" & folio & "'"
            b = True
        End If
        If name <> "" Then
            If b Then
                st &= " AND"
            End If
            st &= " Cliente LIKE '%" & name & "%'"
            b = True
        End If
        If fc <> "" Then
            If b Then
                st &= " AND"
            End If
            st &= " Fcreacion='" & fechasql(fc) & "'"
            b = True
        End If
        If fe <> "" Then
            If b Then
                st &= " AND"
            End If
            st &= " Fecha='" & fechasql(fe) & "'"
        End If

        orden = New OleDb.OleDbCommand(st, conbicrep)
        lector = orden.ExecuteReader
        If lector.HasRows Then
            ds.DataSource = lector
            dg.DataSource = ds
            lector.Close()
            dg.Columns.Item("Folio").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dg.Columns.Item("Fechae").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dg.Columns.Item("Cliente").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dg.Columns.Item("Fecha").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dg.Columns.Item("Hora").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dg.Columns.Item("Total").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dg.Columns.Item("Fcreacion").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dg.Columns.Item("Fcreacion").HeaderText = "Creada"
            dg.Columns.Item("Fechae").HeaderText = "Entregada"

            Dim i As Integer
            For i = 0 To dg.Rows.Count - 1
                If Not IsDBNull(dg("Fcancelada", i).Value) Then
                    dg.Rows.Item(i).DefaultCellStyle.BackColor = Color.Red
                End If
                If Not IsDBNull(dg("Fentrega", i).Value) Then
                    dg.Rows.Item(i).DefaultCellStyle.BackColor = Color.Blue
                End If

            Next
        End If
    End Sub
    Public Sub delrepbic(folio As String)
        Dim st As String = "SELECT cantidad, clave FROM art WHERE colocado=TRUE AND Trabajo=FALSE AND Clave='" & folio & "'"
        Dim orden As OleDbCommand = New OleDb.OleDbCommand(st, conbicrep)
        Dim lector As OleDbDataReader = orden.ExecuteReader
        While lector.Read
            Dim arte As articuloventa = New articuloventa(lector("Clave"))
            arte.modexistencias(lector("cantidad"), True)

        End While
        lector.Close()
        st = "DELETE FROM nota WHERE Folio='" & folio & "'"
        orden = New OleDb.OleDbCommand(st, conbicrep)
        orden.ExecuteNonQuery()
        st = "DELETE FROM bici WHERE Folio='" & folio & "'"
        orden = New OleDb.OleDbCommand(st, conbicrep)
        orden.ExecuteNonQuery()
        st = "DELETE FROM art WHERE Folio='" & folio & "'"
        orden = New OleDb.OleDbCommand(st, conbicrep)
        orden.ExecuteNonQuery()

    End Sub

    Public Function saverepbic(ByVal nombre As String, ByVal total As Decimal, ByVal tabes As TabControl, ByVal direccion As String, ByVal rfc As String, ByVal telcasa As String, ByVal telcel As String, ByVal mail As String, fechac As Date, Optional folio As String = "", Optional nameaut As String = "", Optional notaut As String = "") As String ''salva la reparación
        Dim st As String = ""
        Dim stmod As String = ""
        Dim i As Integer
        Dim k As Integer
        Dim modificar As Boolean = True
        Dim sep As String = "','"
        If folio = "" Then
            folio = getventrepid(2, Now)
            modificar = False
        End If
        st = "INSERT INTO nota(Folio,Cliente,Fecha,Hora,Total,Direccion,RFC,telcasa,telcel,mail,Fcreacion,Usuario) VALUES ('" & folio & "','" & nombre & "','" & fechac.ToShortDateString & "','" & fechac.ToShortTimeString & "'," & total & ",'" & direccion & "','" & rfc & "','" & telcasa & "','" & telcel & "','" & mail & "','" & fechasql(fechac) & "','" & user.idnameusu & "')"
        saverepbic = folio
        ''agrega cliente
        orden = New OleDb.OleDbCommand(st, conbicrep)
        orden.ExecuteNonQuery()

        If modificar Then ''checamos modificaciones
            With tabes.TabPages.Item(0).Controls.Item("tabecontrol").Controls.Item("datoscl").Controls
                If .Item("direccion").BackColor = Drawing.Color.Blue Then
                    stmod &= "Se modificó la dirección" & vbNewLine
                End If
                If .Item("rfc").BackColor = Drawing.Color.Blue Then
                    stmod &= "Se modificó el RFC" & vbNewLine
                End If
                If .Item("telcel").BackColor = Drawing.Color.Blue Then
                    stmod &= "Se modificó el Teléfono celular" & vbNewLine
                End If
                If .Item("telcasa").BackColor = Drawing.Color.Blue Then
                    stmod &= "Se modificó el Teléfono de casa" & vbNewLine
                End If
                If .Item("mail").BackColor = Drawing.Color.Blue Then
                    stmod &= "Se modificó el mail a " & .Item("mail").Text & vbNewLine
                End If

            End With
        End If
        For i = 1 To tabes.TabPages.Count - 1
            Dim banderabnew As Boolean = False
            With tabes.TabPages.Item(i).Controls.Item("pbicicar").Controls
                ''agrega bici
                st = "INSERT INTO bici(folio,id,rodada,marca,color,nota,fhentrega) VALUES ('"
                st &= folio & "',"
                st &= i & ",'"
                st &= .Item("rodada").Text & sep
                If .Item("rodada").Enabled Then ''Coloca que es una bici nueva
                    banderabnew = True
                    stmod &= vbNewLine & "Se agregó la Bicicleta " & i & vbNewLine
                End If
                st &= .Item("marca").Text & sep
                st &= .Item("color").Text & sep
                st &= .Item("notas").Text & sep
                Dim ob As DateTimePicker = .Item("fe")
                st &= fdateselhour(ob, .Item("hora").Text & ":" & .Item("minutos").Text & " " & .Item("formato").Text) & "')"
                orden = New OleDb.OleDbCommand(st, conbicrep)
                orden.ExecuteNonQuery()
                If modificar Then
                    If .Item("notas").BackColor = Drawing.Color.Blue Then
                        stmod &= "Se modificó la notas" & vbNewLine
                    End If
                End If
                'agrega articulos de bici
                Dim tdg As DataGridView = tabes.TabPages.Item(i).Controls.Item("pventa").Controls.Item("dg1")
                If tdg("des", tdg.Rows.Count - 1).Value = "" Then
                    tdg.Rows.RemoveAt(tdg.Rows.Count - 1)
                End If
                Dim bcancel As Boolean = False
                Dim bartmod As Boolean = False
                For k = 0 To tdg.Rows.Count - 1
                    If tdg.Rows.Item(k).DefaultCellStyle.BackColor = Color.Pink Or tdg.Rows.Item(k).DefaultCellStyle.BackColor = Color.Red Then
                        bcancel = True
                    Else
                        bcancel = False
                    End If
                    st = "INSERT INTO art(Folio,id,Cantidad,Clave,Tp,Trabajo,cancel,paquete,Descripción) VALUES ('"
                    st &= folio & "',"
                    st &= i & ","
                    st &= tdg("can", k).Value & ",'"
                    If tdg("des", k).ToolTipText = "ARNORE" Or tdg("des", k).ToolTipText = "TRNORE" Then
                        st &= tdg("des", k).Value & sep
                    Else
                        st &= tdg("des", k).ToolTipText & sep
                    End If
                    st &= tdg("tpv", k).Value & "',"
                    st &= tdg("tra", k).Value & ","
                    st &= bcancel & ",'"
                    st &= tdg("pq", k).Value & sep
                    st &= tdg("Des", k).Value & "')"
                    If modificar And Not banderabnew Then
                        If Not bartmod Then
                            bartmod = True
                            stmod &= "Modificaciones para Bicicleta " & i & vbNewLine
                        End If
                        If tdg.Rows.Item(k).DefaultCellStyle.BackColor <> Drawing.Color.Yellow And tdg.Rows.Item(k).DefaultCellStyle.BackColor <> Drawing.Color.Green And tdg.Rows.Item(k).DefaultCellStyle.BackColor <> Drawing.Color.Red Then
                            If tdg.Rows.Item(k).DefaultCellStyle.BackColor <> Drawing.Color.Pink Then
                                stmod &= "Se agregó: " & tdg("can", k).Value & " " & tdg("des", k).ToolTipText & " (" & tdg("des", k).Value & ")" & vbNewLine
                                tdg.Rows.Item(k).DefaultCellStyle.BackColor = Color.Yellow
                            Else
                                stmod &= "Se canceló: " & tdg("can", k).Value & " " & tdg("des", k).ToolTipText & " (" & tdg("des", k).Value & ")" & vbNewLine
                                tdg.Rows.Item(k).DefaultCellStyle.BackColor = Drawing.Color.Red
                            End If
                        End If
                    End If
                    orden = New OleDb.OleDbCommand(st, conbicrep)
                    orden.ExecuteNonQuery()




                Next
            End With
        Next
        If modificar And stmod <> "" Then
            st = "INSERT INTO Mod(Folio,Descripcion,Fecha,Usuario,Autorizo,Nota) VALUES ('" & folio & "','" & stmod & "','" & nowsql() & "','" & user.idnameusu & "','" & nameaut & "','" & notaut & "')"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            orden.ExecuteNonQuery()
        End If

    End Function


    Public Function loadclntdg(ByVal nombre As String, ByRef found As Boolean) As BindingSource
        loadclntdg = New BindingSource
        Dim st As String = "SELECT Id,Nombre,Adeudo,Mayoreo,Especial FROM client "
        If nombre <> "" Then
            If IsNumeric(nombre) Then
                st &= "WHERE Id=" & nombre & " "

            Else
                st &= "WHERE Nombre LIKE '%" & nombre & "%'"

            End If

        End If

        st &= "ORDER BY Id"
        orden = New OleDb.OleDbCommand(st, conclnt)
        lector = orden.ExecuteReader
        found = lector.HasRows

        loadclntdg.DataSource = lector
        lector.Close()


    End Function


    Public Sub delventg(id1 As Integer)
        Dim st As String = "DELETE FROM vg WHERE id=" & id1
        orden = New OleDb.OleDbCommand(st, convent)
        lector = orden.ExecuteReader
        lector.Close()

        st = "DELETE FROM vgart WHERE id=" & id1
        orden = New OleDb.OleDbCommand(st, convent)
        lector = orden.ExecuteReader
        lector.Close()

    End Sub

    Public Sub addventg(nombre As String, subtotal As Decimal, fecha As Date, ByRef dgval As DataGridView, Optional idcln As Integer = 0, Optional idvopen As Integer = -1)

        Dim idvg As Integer = 0
        If idvopen > -1 Then
            idvg = idvopen
        Else
            orden = New OleDb.OleDbCommand("SELECT Id FROM vg ORDER BY Id", convent)
            lector = orden.ExecuteReader
            While lector.Read
                idvg = lector("Id") + 1
            End While
            lector.Close()
        End If
        Dim st As String = "INSERT INTO vg(Id,Cliente,Subtotal,Dc,Fe,Tienda,Uc) VALUES ('"
        st = st & idvg
        st = st & sep & nombre
        st = st & sep & subtotal
        st = st & sep & Today
        st = st & sep & fecha
        st = st & sep & idti
        st = st & sep & user.idnameusu & "')"
        orden = New OleDb.OleDbCommand(st, convent)
        lector = orden.ExecuteReader
        lector.Close()
        Dim i As Integer
        For i = 0 To dgval.Rows.Count - 1
            st = "INSERT INTO vgart(Id,Cant,Clave,Tp,Paquete) VALUES ('"
            st = st & idvg
            st = st & sep & dgval("Can", i).Value
            'If dgval("Des", i).ToolTipText <> "ARNORE" Or dgval("Des", i).ToolTipText <> "TRNORE" Then
            '    st = st & sep & dgval("Des", i).Value
            'Else
            '    st = st & sep & dgval("Des", i).ToolTipText
            'End If
            st = st & sep & dgval("Clave", i).Value

            st = st & sep & dgval("tpv", i).Value
            st &= sep & dgval("pq", i).Value & "')"
            orden = New OleDb.OleDbCommand(st, convent)
            lector = orden.ExecuteReader
            lector.Close()
        Next


    End Sub



    Public Sub removeautcompletar(st As String)
        ''se utiliza para quitar los articulos que se hacen 0 al momento de la venta
        Dim sts() As String = st.Split(":")
        asd.Remove(st)
        asd.Remove(sts(1))
        asm.Remove(sts(0))
    End Sub

    Public Sub autocompletarart() 'Inicialización Autocompletar de articulos
        'asd.Clear()
        'asm.Clear()
        'asda.Clear()
        'asma.Clear()
        'orden = New OleDb.OleDbCommand("SELECT Descripción, Clave,einf,Existencias FROM Articulos", conart)
        'lector = orden.ExecuteReader
        'While lector.Read
        '    Try
        '        asda.Add(lector("Descripción"))
        '        asma.Add(lector("Clave"))

        '        If Not DBNull.Value.Equals(lector("Existencias")) Then
        '            If Val(lector("Existencias")) > 0 Or lector("einf") Then
        '                asd.Add(lector("Clave") & ":" & lector("Descripción"))
        '                asm.Add(lector("Clave"))
        '            End If
        '        End If

        '    Catch ex As Exception

        '    End Try

        'End While
        'lector.Close()
        'asd.Add("ARNORE:ARTÍCULO NO REGISTRADO")
        'asd.Add("TRNORE:TRABAJO NO REGISTRADO")
        'asda.Add("ARNORE:ARTÍCULO NO REGISTRADO")
        'asda.Add("TRNORE:TRABAJO NO REGISTRADO")
        'asm.Add("ARNORE")
        'asm.Add("TRNORE")
        'asma.Add("ARNORE")
        'asma.Add("TRNORE")

    End Sub

    ''Busca en el catalogo

    Public Function searartempty() As BindingSource
        searartempty = New BindingSource
        Dim sta As String = "SELECT Clave, Descripción, Menudeo, Mayoreo, Especial, PME,Existencias FROM Articulos ORDER BY Clave"
        orden = New OleDb.OleDbCommand(sta, conart)
        lector = orden.ExecuteReader
        searartempty.DataSource = lector
        lector.Close()
    End Function

    Public Function searbarra(cbarra As String) As Boolean
        Dim st As String = "Select Cbarras FROM articulos WHERE Cbarras='" & cbarra & "'"
        Dim orden As OleDbCommand = New OleDbCommand(st, conart)
        Dim lector As OleDbDataReader = orden.ExecuteReader
        searbarra = lector.HasRows
        lector.Close()

    End Function

    Public Function searclave(clave As String) As Boolean
        Dim st As String = "SELECT clave FROM articulos WHERE clave='" & clave & "'"
        Dim orden As OleDbCommand = New OleDbCommand(st, conart)
        Dim lector As OleDbDataReader = orden.ExecuteReader
        searclave = lector.HasRows
        lector.Close()

    End Function
    Public Function seardes(des As String) As Boolean
        Dim st As String = "SELECT Descripción FROM articulos WHERE Descripción='" & des & "'"
        Dim orden As OleDbCommand = New OleDbCommand(st, conart)
        Dim lector As OleDbDataReader = orden.ExecuteReader
        seardes = lector.HasRows
        lector.Close()

    End Function





End Module
