Imports System.Data.OleDb
Imports System.IO.File
Imports System.Threading
Imports System.Runtime.InteropServices

Public Module Estructuras
    Public Class copyarts
        Public art() As articuloventa

        Public Sub New()
            ReDim art(0)
            art(0) = New articuloventa

        End Sub
        Public Sub addrowselection(dg As DataGridViewSelectedRowCollection)
            ReDim art(dg.Count - 1)
            For i = 0 To dg.Count - 1
                art(i) = New articuloventa(dg.Item(i).Cells("clave").Value)

                art(i).cantidad = dg.Item(i).Cells("can").Value
            Next


        End Sub
        Public Sub pasteselection(vent As formvent)
            With vent
                .dg1.CurrentCell = .dg1(0, .dg1.Rows.Count - 1)

                For i = 0 To art.Count - 1


                    .additemdg(art(i), .dg1.CurrentCell.RowIndex + i)
                    .dg1.Rows.Add()
                Next
            End With

        End Sub
    End Class


    Public Class imagsvisor
        Public fotos As Image()
        Public directory As String
        Private icurrent As Integer
        Private pathsdelete As String()
        Private path As String()
        Public imagexist As Boolean
        Private claveart As String
        Public islastimag As Boolean
        Public isnextimag As Boolean
        Public listmod As Boolean
        Public Function getlabimag() As String
            getlabimag = "Imagen " & icurrent + 1
            If fotos.Count > 1 Then
                getlabimag &= " de " & fotos.Count
            End If
        End Function
        Public Function currentimageexist() As Boolean
            If fotos(icurrent) Is Nothing Then
                Return False
            Else
                Return True
            End If

        End Function
        Public Sub saveme()
            Try
                Dim hilo As Thread = New Thread(New ThreadStart(AddressOf savemethread))
                hilo.IsBackground = True
                hilo.Start()
            Catch ex As Exception
                LogAppError(ex, "Estructuras.saveme")
            End Try
        End Sub
        Public Sub savemethread()
            Try
                If Not listmod Then
                    Exit Sub
                End If

                If fotos IsNot Nothing AndAlso fotos.Count > 1 Then
                    Dim stdirsys As String = pathimage & claveart & "-dummy"
                    If Not My.Computer.FileSystem.DirectoryExists(stdirsys) Then
                        My.Computer.FileSystem.CreateDirectory(stdirsys)
                    End If

                    For i = 0 To fotos.Count - 1
                        Dim pathimag As String = stdirsys & "\" & claveart & "(" & i + 1 & ")" & ".jpg"
                        If fotos(i) IsNot Nothing Then
                            fotos(i).Save(pathimag, Imaging.ImageFormat.Jpeg)
                            fotos(i).Dispose()
                        End If
                    Next
                    If My.Computer.FileSystem.DirectoryExists(pathimage & claveart) Then
                        Dim files() As String = IO.Directory.GetFiles(pathimage & claveart)
                        For Each file As String In files
                            Try
                                My.Computer.FileSystem.DeleteFile(file, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                            Catch
                            End Try
                        Next
                    End If
                    If My.Computer.FileSystem.FileExists(pathimage & claveart & ".jpg") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(pathimage & claveart & ".jpg", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                        Catch
                        End Try
                    End If
                    My.Computer.FileSystem.RenameDirectory(stdirsys, claveart)

                ElseIf fotos IsNot Nothing AndAlso fotos.Count > 0 Then
                    If fotos(0) IsNot Nothing Then
                        fotos(0).Save(pathimage & claveart & "-dummy" & ".jpg", Imaging.ImageFormat.Jpeg)
                        fotos(0).Dispose()
                    End If

                    If My.Computer.FileSystem.FileExists(pathimage & claveart & ".jpg") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(pathimage & claveart & ".jpg", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                        Catch
                        End Try
                    End If
                    If fotos(0) IsNot Nothing AndAlso My.Computer.FileSystem.FileExists(pathimage & claveart & "-dummy" & ".jpg") Then
                        My.Computer.FileSystem.RenameFile(pathimage & claveart & "-dummy" & ".jpg", claveart & ".jpg")
                    End If
                    If My.Computer.FileSystem.DirectoryExists(pathimage & claveart) Then
                        Dim files() As String = IO.Directory.GetFiles(pathimage & claveart)
                        For Each file As String In files
                            Try
                                My.Computer.FileSystem.DeleteFile(file, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                            Catch
                            End Try
                        Next
                        Try
                            My.Computer.FileSystem.DeleteDirectory(pathimage & claveart, FileIO.DeleteDirectoryOption.DeleteAllContents)
                        Catch
                        End Try
                    End If
                End If
            Catch ex As Exception
                LogAppError(ex, "Estructuras.savemethread")
            End Try
        End Sub
        Public Sub New()
            ReDim fotos(0)
            ReDim path(0)
            icurrent = 0
        End Sub
        Public Sub New(clave As String)
            imagexist = False
            claveart = clave
            ReDim pathsdelete(0)
            If My.Computer.FileSystem.DirectoryExists(pathimage & clave) Then
                Dim stimages As String() = IO.Directory.GetFiles(pathimage & clave)

                ReDim fotos(stimages.Count - 1)
                ReDim path(stimages.Count - 1)
                For i = 0 To stimages.Count - 1
                    fotos(i) = Image.FromFile(stimages(i))
                    path(i) = stimages(i)

                Next
                imagexist = True
            Else
                ReDim fotos(0)
                ReDim path(0)
                If My.Computer.FileSystem.FileExists(pathimage & clave & ".png") Then
                    fotos(0) = Image.FromFile(pathimage & clave & ".png")
                    path(0) = pathimage & clave & ".png"
                    imagexist = True

                ElseIf My.Computer.FileSystem.FileExists(pathimage & clave & ".jpg") Then
                    fotos(0) = Image.FromFile(pathimage & clave & ".jpg")
                    path(0) = pathimage & clave & ".jpg"
                    imagexist = True

                ElseIf My.Computer.FileSystem.FileExists(pathimage & clave & ".bmp") Then
                    fotos(0) = Image.FromFile(pathimage & clave & ".bmp")
                    path(0) = pathimage & clave & ".bmp"
                    imagexist = True
                End If

            End If
            listmod = False
            icurrent = 0
            checklastnext()
        End Sub
        Private Sub checklastnext()
            If fotos.Count > 1 Then
                If icurrent = fotos.Count - 1 Then
                    isnextimag = False
                Else
                    isnextimag = True
                End If
                If icurrent = 0 Then
                    islastimag = False
                Else
                    islastimag = True
                End If
            Else
                isnextimag = False
                islastimag = False
            End If

        End Sub
        Public Function nextimag() As Image
            icurrent += 1
            If icurrent > fotos.Count - 1 Then
                icurrent = fotos.Count - 1
            End If
            checklastnext()
            Return fotos(icurrent)
        End Function
        Public Function lastimag() As Image
            icurrent -= 1
            If icurrent < 0 Then
                icurrent = 0
            End If
            checklastnext()
            Return fotos(icurrent)
        End Function
        Public Function currentimag() As Image
            Return fotos(icurrent)
        End Function
        Public Function deletecurrent() As Image
            If fotos.Count > 1 Then
                If icurrent <> fotos.Count - 1 Then
                    fotos(icurrent).Dispose()

                    For i = icurrent To fotos.Count - 2
                        fotos(i) = fotos(i + 1)

                    Next
                    listmod = True
                End If
                ReDim Preserve fotos(fotos.Count - 2)
                nextimag()
            Else
                icurrent = 0
                fotos(0).Dispose()
                fotos(0) = Nothing
                listmod = True

            End If
            Return fotos(icurrent)
        End Function
        Public Function addnew() As Image
            If fotos(fotos.Count - 1) IsNot Nothing Then
                ReDim Preserve fotos(fotos.Count)
                icurrent = fotos.Count - 1
                checklastnext()
                listmod = True
            End If
            Return fotos(fotos.Count - 1)
        End Function
        Public Function setimageclip() As Image
            fotos(icurrent) = Clipboard.GetImage()
            listmod = True
            Return fotos(icurrent)
        End Function
        Public Function setimagepath() As Image
            Dim buscar As OpenFileDialog = New OpenFileDialog()
            buscar.Filter = "Archivos de imagen |*.jpg;*.bmp;*.png"
            buscar.FilterIndex = 1
            buscar.ShowDialog()
            If buscar.FileName <> "" Then
                fotos(icurrent) = Image.FromFile(buscar.FileName)
                listmod = True
            End If
            Return fotos(icurrent)
        End Function
    End Class


    Class converart
        Class articuloconversion
            Inherits articuloventa
            Public cantconv As Decimal
            Public Sub New(clave As String, factor As Decimal)
                cantconv = factor
                load(clave, True)
            End Sub
        End Class
        Class conversion
            Public artto As articuloconversion()
            Public id As Integer
            Public Sub New(clave As String, factor As Decimal, ids As Integer)
                ReDim artto(0)
                id = ids
                artto(0) = New articuloconversion(clave, factor)
            End Sub
            Public Sub addart(clave As String, cantidad As Decimal)
                ReDim Preserve artto(artto.Count)
                artto(artto.Count - 1) = New articuloconversion(clave, cantidad)

            End Sub
        End Class
        Dim artfrom As articuloconversion
        Dim converts As conversion()
        Public Sub New(artfrom As articuloventa)
            Dim st As String = "SELECT * FROM Conversion WHERE Clavefrom='" & artfrom.clave & "' ORDER BY Id"
            Dim id As Integer
            Dim idl As Integer
            Dim firts As Boolean = True
            Dim orden As New OleDbCommand(st, conart)
            Dim lector As OleDbDataReader = orden.ExecuteReader
            ReDim converts(0)
            While lector.Read
                id = lector("id")
                If firts Then
                    idl = lector("id)")
                    firts = False
                    converts(id - 1) = New conversion(lector("claveto"), lector("Factor"), id)
                Else
                    If idl <> id Then
                        ReDim Preserve converts(converts.Count)
                        converts(id - 1) = New conversion(lector("claveto"), lector("Factor"), id)
                    Else
                        converts(id - 1).addart(lector("claveto"), lector("factor"))
                    End If
                    idl = id
                End If
            End While
        End Sub
        Public Sub applyconversion(ids As Integer, cant As Decimal)
            artfrom.modexistencias(cant, False)
            For i = 0 To converts(ids - 1).artto.Count - 1
                With converts(ids - 1).artto(i)
                    Dim cantcon As Decimal = cant * .cantconv
                    .modexistencias(cantcon, True)
                End With
            Next

        End Sub
    End Class
    Class notificacion
        Dim blinker As Boolean
        Dim bblinker As Boolean
        Dim campreciofecha As Date
        Dim bcambio As Boolean = False
        Dim bext As Boolean = False
        Dim button As System.Windows.Forms.ToolStripDropDownButton = New System.Windows.Forms.ToolStripDropDownButton
        Dim ncp As System.Windows.Forms.ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Dim nsa As System.Windows.Forms.ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Dim contador As System.Windows.Forms.Timer = New System.Windows.Forms.Timer

        Public Sub New()
            contador = New System.Windows.Forms.Timer
            contador.Interval = 500
            contador.Stop()
            button = New System.Windows.Forms.ToolStripDropDownButton
            button.Image = SICDIN30.My.Resources.Resources.notify
            button.BackColor = Color.FromArgb(192, 192, 255)

            ncp = New System.Windows.Forms.ToolStripMenuItem
            ncp.Text = "Cambio de precio a artículos existentes"
            ncp.BackColor = Color.FromArgb(192, 192, 255)
            ncp.Visible = False

            nsa = New System.Windows.Forms.ToolStripMenuItem
            nsa.Text = "Artículos fuera de stock"
            nsa.Visible = False
            nsa.BackColor = Color.FromArgb(192, 192, 255)

            button.DropDownItems.Add(ncp)
            button.DropDownItems.Add(nsa)
            button.Visible = False

            campreciofecha = Now
            AddHandler contador.Tick, AddressOf blink
            AddHandler ncp.Click, AddressOf changeprecio
            AddHandler nsa.Click, AddressOf changeext



            main.StatusStrip.Items.Add(button)
            bcambio = False
            bext = False
            checkoldnot()
        End Sub
        Public Sub checkoldnot()
            Dim st As String = "SELECT Tipo, Fechanot FROM Notification WHERE Fechanot>Fechacheck"
            Dim orden As OleDb.OleDbCommand = New OleDbCommand(st, consys)
            Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
            While lector.Read
                Select Case lector("Tipo")
                    Case 1
                        campreciofecha = lector("fechanot")
                        setnotifycambio()
                    Case 2
                        setnotiext()
                End Select
            End While
            lector.Close()

        End Sub
        Private Sub changeext()
            Dim st As String = "UPDATE Notification Set Fechacheck='" & onlyfechasql(Now) & "' WHERE Tipo=2"
            Dim orden As OleDb.OleDbCommand = New OleDbCommand(st, consys)
            orden.ExecuteNonQuery()
            bext = False
            setnotiext()


        End Sub
        Private Sub setnotiext()
            nsa.BackColor = Color.FromArgb(192, 192, 255)
            ncp.Visible = False
            main.existenciasfront()
            With existencias
                .TabControl1.SelectedIndex = 0
                .cargarfaltantes()
            End With
            checkstop()
        End Sub
        Public Sub changeprecio()
            Dim st As String = "UPDATE Notification SET Fechacheck='" & onlyfechasql(Now) & "' WHERE Tipo=1"
            Dim orden As OleDb.OleDbCommand = New OleDbCommand(st, consys)
            orden.ExecuteNonQuery()
            ncp.BackColor = Color.FromArgb(192, 192, 255)
            main.existenciasfront()
            With existencias
                .TabControl1.SelectedIndex = 2
                .agregar.Checked = False
                .del.Checked = False
                .cambio.Checked = True
                .ext.Checked = False
                .dtlog.Value = campreciofecha
                .buscar()
            End With
            checkstop()
            ncp.Visible = False


        End Sub
        Private Sub checkstop()
            If Not bcambio And Not bext Then
                contador.Stop()
                button.BackColor = Color.FromArgb(192, 192, 255)
                button.Visible = False
            End If
        End Sub

        Private Sub blink()
            button.Visible = True

            If blinker Then
                blinker = False
                button.BackColor = Color.FromArgb(192, 192, 255)
            Else
                blinker = True
                button.BackColor = Color.Red
            End If
        End Sub
        Private Sub notifystartblink()
            contador.Start()

        End Sub
        Public Sub cambiodeprecio(clave As String)
            Dim art As articuloventa = New articuloventa
            art.load(clave, True)
            If art.existencias > 0 Then
                campreciofecha = Now
                setnotifycambio()
            End If
        End Sub
        Private Sub setnotifycambio()
            Try
                Dim st As String = "UPDATE Notification SET Fechanot='" & onlyfechasql(Now) & "' WHERE Tipo=1"
                SyncLock consys
                    Using cmd As New OleDbCommand(st, consys)
                        cmd.ExecuteNonQuery()
                    End Using
                End SyncLock
                campreciofecha = Now
                bcambio = True
                Try
                    If main IsNot Nothing Then
                        If main.InvokeRequired Then
                            main.BeginInvoke(Sub()
                                                notifystartblink()
                                                If ncp IsNot Nothing Then ncp.Visible = True
                                             End Sub)
                        Else
                            notifystartblink()
                            If ncp IsNot Nothing Then ncp.Visible = True
                        End If
                    End If
                Catch
                End Try
            Catch ex As Exception
                LogAppError(ex, "notifier.setnotifycambio")
            End Try
        End Sub
        Public Sub cambiodeexistencias(clave As String)
            Try
                Dim art As articuloventa = New articuloventa
                art.load(clave, True)
                If art.existencias < art.stock And art.stock > 0 Then
                    Dim st As String = "UPDATE Notification SET Fechanot='" & onlyfechasql(Now) & "' WHERE Tipo=2"
                    SyncLock consys
                        Using cmd As New OleDbCommand(st, consys)
                            cmd.ExecuteNonQuery()
                        End Using
                    End SyncLock
                    bext = True
                    Try
                        If main IsNot Nothing Then
                            If main.InvokeRequired Then
                                main.BeginInvoke(Sub()
                                                    notifystartblink()
                                                    If nsa IsNot Nothing Then nsa.Visible = True
                                                 End Sub)
                            Else
                                notifystartblink()
                                If nsa IsNot Nothing Then nsa.Visible = True
                            End If
                        End If
                    Catch
                    End Try
                End If
            Catch ex As Exception
                LogAppError(ex, "notifier.cambiodeexistencias")
            End Try
        End Sub

    End Class

    Class botonventaefrente
        Inherits Button
        Dim fe As Font = New Font("Arial", 14, FontStyle.Bold)
        Public nexit As Button = New Button
        Public contador As System.Windows.Forms.Timer = New System.Windows.Forms.Timer
        Public Sub setbuttonventa(ByRef o As formvent, idvent As Integer)
            With Me
                .Text = "Ven." & idvent
                .BackgroundImage = SICDIN30.My.Resources.Resources.ic_shopping_cart_128_28698

            End With
            setbutton()
            AddHandler Me.Click, AddressOf o.panelventa.BringToFront

            AddHandler nexit.Click, AddressOf o.cancelar

        End Sub
        Public Sub setbuttonrep(ByRef o As reparacion, idvent As Integer)
            With Me
                .Text = "Rep." & idvent
                .BackgroundImage = SICDIN30.My.Resources.Resources.technical_support_2_icon_icons_com_52811

            End With
            setbutton()
            AddHandler Me.Click, AddressOf o.panelrep.BringToFront

            AddHandler nexit.Click, AddressOf o.cerrar

        End Sub


        Private Sub setbutton()
            With Me
                .BackgroundImageLayout = ImageLayout.Zoom
                .Font = fe
                .BackColor = main.invb.BackColor
                .Dock = DockStyle.Right
                main.panelventbut.Controls.Add(Me)
                .AutoSize = True
                .SendToBack()
            End With
            setnexit()
            AddHandler Me.Disposed, AddressOf nexit.Dispose
            AddHandler Me.MouseEnter, AddressOf nexit.Show
            contador = New System.Windows.Forms.Timer
            contador.Interval = 5000
            AddHandler Me.MouseLeave, AddressOf contador.Start
            AddHandler contador.Tick, AddressOf nexit.Hide
            AddHandler contador.Tick, AddressOf contador.Stop


            movenexit()
        End Sub
        Public Sub setnexit()
            nexit = New Button
            With nexit
                .Text = ""
                .Height = Me.Height / 3
                .FlatStyle = FlatStyle.Flat
                .Width = Me.Width / 4
                .BackColor = Color.Red
                .ForeColor = Color.White
                .Text = "X"
            End With
            main.panelventbut.Controls.Add(nexit)
            nexit.BringToFront()
            nexit.Visible = False

        End Sub
        Private Sub movenexit() Handles Me.Move
            nexit.Location = New Point(Me.Location.X + Me.Width - nexit.Width, Me.Location.Y + 5)

        End Sub
    End Class


    Public Class lugar
        Public id As Integer
        Public apodo As String = ""
        Public nombre As String
        Public idtablename As String
        Public telenc As String = ""
        Public website As String = ""
        Public port As Int32
        Public tipo As String
        Public hora As horario
        Public lastcant As Date
        Public lastmod As Date
        Public lastclnmod As Date
        Public apodtiny As String
        Public Function getapodos(omitme As Boolean) As String()
            Dim lugs As String() = getlugares(omitme)
            Dim apodos As String()
            ReDim apodos(lugs.Count)
            For i = 0 To lugs.Count - 1
                load(lugs(i))
                apodos(i) = apodo

            Next
            Return apodos

        End Function
        Public Function getlugares(omitme As Boolean) As String()
            Dim st As String = "SELECT * FROM Lugares"
            Dim orden As OleDbCommand = New OleDbCommand(st, consys)
            Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
            Dim stnombres As String() = {""}
            Dim i As Integer = 0
            While lector.Read
                If voiddeleterstring(lector("Website")) <> "" Then
                    If Not (omitme And tienda.id = lector("Id")) Then
                        ReDim Preserve stnombres(i)
                        stnombres(i) = lector("Id") & ":" & lector("Nombre")
                        i += 1
                    End If


                End If
            End While
            lector.Close()

            Return stnombres

        End Function
        Public Shared Operator =(ByVal l1 As lugar, ByVal l2 As lugar) As Boolean
            If l1.id = l2.id Then
                Return True
            Else
                Return False
            End If
        End Operator

        Public Shared Operator <>(ByVal l1 As lugar, ByVal l2 As lugar) As Boolean
            If l1.id = l2.id Then
                Return False
            Else
                Return True
            End If
        End Operator

        Public Sub New()
            id = 0
            nombre = 0
            idtablename = 0
            tipo = ""
            hora = New horario
            apodo = ""
            telenc = ""
            website = ""
            apodtiny = ""
            lastclnmod = Now
        End Sub
        Public Sub load(idt As Integer)
            Dim st As String = "SELECT * FROM Lugares WHERE Id=" & idt
            Dim orden As OleDb.OleDbCommand = New OleDbCommand(st, consys)
            Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
            While lector.Read
                id = idt
                nombre = lector("Nombre")
                idtablename = lector("Idtabla")
                tipo = lector("Tipo")
                apodo = lector("apodo")
                apodtiny = apodo(0).ToString.ToUpper
                telenc = voiddeleterstring(lector("telenc"))

                website = voiddeleterstring(lector("Website"))
                lastclnmod = voiddeleterstring(lector("lastclnmod"))


                port = voiddeleter(lector("Puerto"))
                lastmod = (lector("Lastmod"))
                lastcant = (lector("Lastcant"))
            End While
            lector.Close()

        End Sub
        Public Sub load(nombres As String, Optional isapodo As Boolean = False)
            nombres = nombres.Replace("'", "''")
            Dim st As String
            If Not isapodo Then
                st = "SELECT * FROM Lugares WHERE nombre='" & nombres & "'"
            Else
                st = "SELECT * FROM Lugares WHERE apodo='" & nombres & "'"
            End If

            Dim orden As OleDb.OleDbCommand = New OleDbCommand(st, consys)
            Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
            While lector.Read
                id = lector("id")
                nombre = lector("Nombre")
                idtablename = lector("Idtabla")
                tipo = lector("Tipo")
                apodo = lector("apodo")
                port = voiddeleter(lector("Puerto"))
                telenc = voiddeleterstring(lector("telenc"))
                lastmod = (lector("Lastmod"))
                lastcant = (lector("Lastcant"))
                lastclnmod = voiddeleterstring(lector("lastclnmod"))
            End While
            lector.Close()

        End Sub

        Public Function getnumberlugar() As Integer
            Dim st As String = "SELECT * FROM Lugares"
            Dim orden As OleDbCommand = New OleDbCommand(st, consys)
            Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
            getnumberlugar = 0
            While lector.Read
                getnumberlugar += 1
            End While
            lector.Close()

        End Function
    End Class
    Public Class horario
        Public dia(7) As diashorario
        Public Sub New()
            For i = 0 To 6
                dia(i) = New diashorario
                dia(i).dia = i
                Select Case i
                    Case 0
                        dia(i).nombre = "Domingo"
                    Case 1
                        dia(i).nombre = "Lunes"
                    Case 2
                        dia(i).nombre = "Martes"
                    Case 3
                        dia(i).nombre = "Miercoles"
                    Case 4
                        dia(i).nombre = "Jueves"
                    Case 5
                        dia(i).nombre = "Viernes"
                    Case 6
                        dia(i).nombre = "Sabado"
                End Select

            Next
        End Sub
    End Class
    Public Class diashorario
        Public dia As DayOfWeek
        Public nombre As String
        Public horaapertura As Date
        Public horacierre As Date
        Public Sub New()
            horaapertura = Now
            horacierre = Now
        End Sub
    End Class
    Public Class printnota
        ''Definiciones
        Private e As Printing.PrintPageEventArgs
        Public p1 As System.Drawing.Printing.PrintDocument
        Public clnt As Cliente
        Public notav As notaventa
        Public notarep As reparacion

        ''Puntos en pagina
        Dim pxof As Integer
        Dim pyof As Integer


        Dim px As Integer
        Dim py As Integer
        Dim lp As Point

        Dim pagac As Integer ''pagina actual

        Dim npag As Integer


        ''Fonts
        Dim fp As Font = New Font("Arial", 9)
        Dim fpbic As Font = New Font("Arial", 12)

        Public folio As String
        Public Sub New()
            clnt = New Cliente
            notav = New notaventa
            notarep = New reparacion
            lp = New Point
            p1 = New System.Drawing.Printing.PrintDocument()


        End Sub
        'Public Function imprimirventa() As Boolean
        '    PV = Print()

        '    If Not PrintDialog.ShowDialog = vbOK Then
        '        Exit Function
        '    End If

        '    p1.Print()
        '    imprimirventa = True
        '    RemoveHandler p1.PrintPage, AddressOf imprimire

        'End Function
        'Private Sub imprimire(sender As Object, e As Printing.PrintPageEventArgs)
        '    selectitiendaprint()
        '    printencabezado(e)
        '    printdg(e, nini, nfin)
        '    If ntventa.apartado Then
        '        printpagosapartado(e)
        '    End If
        '    printfootventa(e)

        '    If pagac = npag Then
        '        e.HasMorePages = False
        '    Else
        '        e.HasMorePages = True
        '        pagac += 1
        '        nini += nart
        '        If (nini + nart) > artv.articulo.Count Then
        '            nfin = artv.articulo.Count
        '        Else
        '            nfin = nini + nart
        '        End If

        '    End If
        '    If ntventa.apartado Then
        '        printleyendasapartado(e)
        '    End If
        'End Sub



        Private Sub updatep()
            ''actualiza lp
            lp = New Point(px, py)
        End Sub
        Private Sub printencabezado(ByRef e As Printing.PrintPageEventArgs)
            'imprime el encabezado de la nota 
            pagnota.cliente.Text = clnt.Nombre
            pagnota.rfc.Text = clnt.RFC
            pagnota.direccion.Text = clnt.Direccion

            If clnt.RFC = "" Then
                pagnota.rfc.Text = "N/A"
            End If
            If clnt.Direccion = "" Then
                pagnota.direccion.Text = "N/A"
            End If
            Dim telefono As String = "N/A"
            If clnt.telcasa <> "" Then
                telefono = clnt.telcasa
            End If
            If clnt.telcel <> "" Then
                telefono = clnt.telcel
            End If
            pagnota.correrol.Text = "Correo:" & clnt.mail
            pagnota.folio.Text = folio
            If codeprint Then
                pagnota.foliobar.Text = "*" & folio & "*"
            Else
                pagnota.foliobar.Text = folio
            End If
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
            pagnota.npag.Text = "Pag:" & pagac & "/" & npag
            Select Case idti
                Case 1
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
                Case 1
                    e.Graphics.DrawImage(SICDIN30.My.Resources.logocltrans, pxof + 120, lp.Y - 50, 350, 350)

                Case 2
                    e.Graphics.DrawImage(SICDIN30.My.Resources.allystrans, pxof + 20, lp.Y - 10, 650, 200)
            End Select
            pagnota.Close()

        End Sub
    End Class

    Public Class diatrabajo
        Public fecha As Date
        Public usuapertura As String
        Public usucierre As String
        Public horapaertura As Date
        Public horacierre As Date
        Public Eapertura As Decimal
        Public Ecierre As Decimal
        Public Tcierre As Decimal
        Public sobrante As Decimal
        Public Sub New()
            fecha = Today

        End Sub
        Public Sub load(fechas As Date)
            Dim st As String = "SELECT * FROM Dias WHERE Fecha=#" & fechasql(fechas) & "#"
            Dim orden As OleDbCommand = New OleDbCommand(st, convent)
            Dim lector As OleDbDataReader = orden.ExecuteReader
            While lector.Read
                fecha = fechas
                horapaertura = voiddeleterstring(lector("Hapertura"), Now.ToShortTimeString)
                horacierre = voiddeleterstring(lector("Hapertura"), Now.ToShortTimeString)
                Ecierre = voiddeleter(lector("Ecierre"))
                Eapertura = voiddeleter(lector("Eapertura"))
                Tcierre = voiddeleter(lector("Tcierre"))
                usuapertura = voiddeleterstring(lector("Usuarioa"))
                usucierre = voiddeleterstring(lector("Usuarioc"))
                sobrante = voiddeleter(lector("Sobrante"))
            End While
            lector.Close()

        End Sub
        Public Sub getventas(ByRef pagoefe As Decimal, ByRef pagotar As Decimal, ByRef pagotrans As Decimal, ByRef pagado As Decimal, ByRef retiros As Decimal)
            Dim st As String = "SELECT pagoefe,pagotar,pagotrans,pagado FROM lv WHERE Fecha=#" & onlyfechasql(fecha) & "#"
            Dim orden As OleDbCommand = New OleDbCommand(st, convent)
            Dim lector As OleDbDataReader = orden.ExecuteReader
            pagoefe = 0
            pagotar = 0
            pagotrans = 0
            pagado = 0
            While lector.Read

                pagoefe += lector("pagoefe")
                pagotar += lector("pagotar")
                pagotrans += lector("pagotrans")
                If lector("pagado") < 0 Then
                    retiros += lector("pagado")
                End If
                pagado += lector("pagado")
            End While
        End Sub
        Public Sub abrirdia(usu As usuario)

            Dim st As String = "SELECT * FROM Dias WHERE Fecha=#" & fechasql(Today) & "#"
            Dim orden As OleDbCommand = New OleDbCommand(st, convent)
            Dim lector As OleDbDataReader = orden.ExecuteReader

            If lector.HasRows = False Then
                lector.Close()
                st = "SELECT TOP 1 Fecha,Ecierre,Hcierre FROM Dias ORDER BY Fecha DESC"
                orden = New OleDbCommand(st, convent)
                Eapertura = 0
                lector = orden.ExecuteReader
                If lector.HasRows Then
                    While lector.Read
                        If Not DBNull.Value.Equals(lector("Hcierre")) Then
                            Eapertura = lector("Ecierre")
                        Else
                            Dim fechas As Date = lector("Fecha")
                            dialogs.Close()

                            If idti > 0 Then
                                If dialogs.pregunta("No se hizo corte de caja el día " & fechas.ToShortDateString & vbNewLine & "¿Desea hacerlo ahora?") = DialogResult.Yes Then
                                    Eapertura = cierrededia.cerrardiaatrasado(fechas)
                                    cierrededia.Close()

                                Else
                                    dialogs.Close()

                                    dialogs.advertencia("El efectivo de caja iniciara en 0" & vbNewLine & "hasta que se haga el corte correspondiente")
                                    Eapertura = 0
                                End If
                            End If

                        End If
                    End While
                Else
                    Eapertura = 0
                End If
                lector.Close()
                st = "INSERT INTO Dias(Fecha,Hapertura,Usuarioa,Eapertura) VALUES ('" & onlyfechasql(Today) & "','" & horasql(Now) & "','" & usu.idnameusu & "'," & Eapertura & ")"
                orden = New OleDbCommand(st, convent)
                orden.ExecuteNonQuery()
            Else
                While lector.Read
                    horapaertura = lector("Hapertura")
                    usuapertura = lector("Usuarioa")
                    Eapertura = lector("Eapertura")
                    fecha = onlyfechasql(Today)
                End While
                lector.Close()
            End If

        End Sub
        Public Sub cerrardia(usu As usuario, Ecierre As Decimal, tcierre As Decimal, sobrante As Decimal)
            Dim st As String = "UPDATE Dias SET Hcierre='" & horasql(Now) & "', Ecierre=" & Ecierre & ", Tcierre=" & tcierre & ", Usuarioc='" & usu.idnameusu & "',Sobrante=" & sobrante & " WHERE Fecha=#" & onlyfechasql(fecha) & "#"
            Dim orden As OleDbCommand = New OleDbCommand(st, convent)
            orden.ExecuteNonQuery()
        End Sub
    End Class
    Public Class trabajador
        Public id As Integer
        Public nombre As String
        Public direccion As String
        Public telcasa As String
        Public telcel As String
        Public loaded As Boolean = False
        Public destajo As Boolean
        Public pdestajo As Decimal
        Public ufc As Date
        Public activo As Boolean
        Public salario As Decimal
        Public favorito As Boolean
        Public Sub New()
            id = 0
            nombre = ""
            loaded = False
            direccion = ""
            telcasa = ""
            telcel = ""
            destajo = False
            pdestajo = 0
            ufc = Today
            favorito = False
            salario = 0
            activo = False
        End Sub
        Public Sub disableme()
            Dim st As String = "UPDATE trabajador SET Activo=FALSE WHERE Id=" & id
            Dim orden As OleDbCommand = New OleDbCommand(st, contra)
            orden.ExecuteNonQuery()
            activo = False

        End Sub
        Public Sub enableme()
            Dim st As String = "UPDATE trabajador SET Activo=TRUE WHERE Id=" & id
            Dim orden As OleDbCommand = New OleDbCommand(st, contra)
            orden.ExecuteNonQuery()
            activo = True

        End Sub
        Public Sub pagar()
            Dim st As String = "UPDATE trabajador SET UFC='" & fechasql(Today) & "' WHERE Id=" & id
            Dim orden As OleDbCommand = New OleDbCommand(st, contra)
            orden.ExecuteNonQuery()

        End Sub
        Public Function assingid() As Integer
            Dim st As String = "SELECT Id FROM trabajador ORDER BY Id"
            Dim orden As OleDbCommand = New OleDbCommand(st, contra)
            Dim lector As OleDbDataReader = orden.ExecuteReader
            While lector.Read
                id = lector("Id") + 1
            End While
            lector.Close()

            assingid = id
        End Function
        Public Sub addme()
            Dim st As String = "INSERT INTO trabajador(Id,Nombre,Dirección,Telefono,Telcel,Destajo,Pdestajo,salario,activo,ufc) VALUES("
            st &= id & ",'"
            st &= nombre & "','"
            st &= direccion & "','"
            st &= telcasa & "','"
            st &= telcel & "',"
            st &= destajo & ","
            st &= pdestajo & ","
            st &= salario & ",true,'"
            st &= fechasql(Today) & "')"
            Dim orden As OleDbCommand = New OleDbCommand(st, contra)
            orden.ExecuteNonQuery()
        End Sub
        Public Function idname() As String
            idname = id & ":" & nombre
        End Function
        Public Sub load(ids As Integer)
            Dim st As String = "SELECT * FROM trabajador WHERE Id=" & ids
            Dim orden As OleDbCommand = New OleDbCommand(st, contra)
            Dim lector As OleDbDataReader = orden.ExecuteReader
            While lector.Read
                id = ids
                nombre = lector("Nombre")
                If Not DBNull.Value.Equals(lector("dirección")) Then
                    direccion = lector("dirección")
                End If
                If Not DBNull.Value.Equals(lector("Telefono")) Then
                    telcasa = lector("Telefono")
                End If
                If Not DBNull.Value.Equals(lector("telcel")) Then
                    telcel = lector("telcel")
                End If



                destajo = lector("destajo")
                pdestajo = lector("pdestajo")
                ufc = lector("ufc")
                favorito = lector("favorito")
                salario = lector("salario")
                activo = lector("activo")
                loaded = True
            End While
            lector.Close()

        End Sub
    End Class
    Public Class usuario
        Public id As Integer
        Public nombre As String
        Public pass As String
        Public fu As Date
        Public admin As Boolean
        Public foto As String
        Public icon As Image
        Public telefono As String
        Public Sub blockme()
            Dim st As String = "UPDATE Usuario SET activo=FALSE WHERE Id=" & id
            Dim orden As OleDbCommand = New OleDbCommand(st, conusu)
            orden.ExecuteNonQuery()

        End Sub
        Public Sub enableme()
            Dim st As String = "UPDATE Usuario SET activo=TRUE WHERE Id=" & id
            Dim orden As OleDbCommand = New OleDbCommand(st, conusu)
            orden.ExecuteNonQuery()

        End Sub
        Public Function repeated() As Boolean
            Dim st As String = "SELECT Nombre FROM Usuario WHERE Nombre='" & nombre & "'"
            Dim orden As OleDbCommand = New OleDbCommand(st, conusu)
            Dim lector As OleDbDataReader = orden.ExecuteReader
            repeated = lector.HasRows
            lector.Close()
        End Function
        Public Function getid() As Integer
            Dim st As String = "SELECT Id FROM Usuario ORDER BY Id"
            Dim orden As OleDbCommand = New OleDbCommand(st, conusu)
            Dim lector As OleDbDataReader = orden.ExecuteReader
            Dim lastnum As Integer = 0
            While lector.Read
                lastnum = lector("Id")
            End While
            lector.Close()
            id = lastnum + 1
            getid = lastnum + 1
        End Function
        Public Function idnameusu()
            Return id & ":" & nombre
        End Function
        Public Sub New()
            id = 1
            nombre = "Angel"
            fu = Today
            admin = True

        End Sub
        Public Sub editme()
            Dim st As String = "UPDATE Usuario SET Nombre='" & nombre & "',Pass='" & pass & "',Admin=" & admin & ",telefono='" & telefono & "',Foto='" & foto & "' WHERE Id=" & id
            Dim orden As OleDbCommand = New OleDbCommand(st, conusu)
            orden.ExecuteNonQuery()
        End Sub
        Public Sub login()
            Dim st As String = "UPDATE Usuario SET Fu='" & Now & "' WHERE Id=" & id
            Dim orden As OleDbCommand = New OleDbCommand(st, conusu)
            orden.ExecuteNonQuery()
        End Sub
        Public Sub loadid(ids As Integer)
            Dim st As String = "SELECT* FROM Usuario WHERE Id=" & ids
            Dim orden As OleDbCommand = New OleDbCommand(st, conusu)
            Dim lector As OleDbDataReader = orden.ExecuteReader
            While lector.Read
                id = lector("id")
                nombre = lector("nombre")
                pass = lector("pass")
                fu = lector("Fu")
                admin = lector("admin")
                If Not DBNull.Value.Equals(lector("telefono")) Then
                    telefono = lector("telefono")
                End If
                foto = lector("foto")
            End While
            lector.Close()
        End Sub
        Public Sub addme()
            Dim st As String = "INSERT INTO Usuario(Id,Nombre,Pass,Fu,Admin,telefono,activo,Foto) VALUES ("
            st &= id & ",'"
            st &= nombre & "','"
            st &= pass & "','"
            st &= Now & "',"
            st &= admin & ",'"
            st &= telefono & "',TRUE,'"
            st &= foto & "')"
            Dim orden As OleDbCommand = New OleDbCommand(st, conusu)
            orden.ExecuteNonQuery()


        End Sub
    End Class
    Public Class articuloventa : Implements IDisposable
        Public imagepathold As String = ""
        Public clave As String = ""
        Public descripcion As String = ""
        Public tp As String = ""
        Public precio As Decimal = 0
        Public menudeo As Decimal
        Public mayoreo As Decimal
        Public especial As Decimal
        Public pgmen As Decimal = 0
        Public pgmay As Decimal = 0
        Public importe As Decimal = 0
        Public tag As String = ""
        Public cbarra As String
        Public cantidad As Integer = 0
        Public tipo As String
        Public stock As Integer
        Public existencias As Decimal
        Public ea1 As Integer
        Public ea2 As Integer
        Public found As Boolean
        Public einf As Boolean
        Public pq As String
        Public paquetes As String()
        Public pme As Integer
        Public notas As String
        Public ecl As Integer
        Public cancel As Boolean
        Public upexis As Boolean
        Public fum As Date
        Public fuc As Date
        Public eca As Integer
        Public colocado As Boolean
        Dim updatefoto As Boolean = True
        Public updatecloud As Boolean = True
        Public trabajo As Boolean ''para reparación

        Public Shared Operator =(ByVal a1 As articuloventa, ByVal a2 As articuloventa) As Boolean
            Dim result As Boolean = True
            With a1
                If .clave <> a2.clave Then
                    result = False
                End If
                If .descripcion <> a2.descripcion Then
                    result = False
                End If
                If .cbarra <> a2.cbarra Then
                    result = False
                End If
                If .pme <> a2.pme Then
                    result = False
                End If
                If .pq <> a2.pq Then
                    result = False
                End If
                If .menudeo <> a2.menudeo Then
                    result = False
                End If
                If .mayoreo <> a2.mayoreo Then
                    result = False
                End If
                If .especial <> a2.especial Then
                    result = False
                End If
                If .pgmay <> a2.pgmay Then
                    result = False
                End If
                If .pgmen <> a2.pgmen Then
                    result = False
                End If

            End With




            Return result

        End Operator

        Public Shared Operator <>(ByVal a1 As articuloventa, ByVal a2 As articuloventa) As Boolean
            Dim result As Boolean = False
            With a1
                If .clave <> a2.clave Then
                    result = True
                End If
                If .descripcion <> a2.descripcion Then
                    result = True
                End If
                If .cbarra <> a2.cbarra Then
                    result = True
                End If
                If .pme <> a2.pme Then
                    result = True
                End If
                If .pq <> a2.pq Then
                    result = True
                End If
                If .menudeo <> a2.menudeo Then
                    result = True
                End If
                If .mayoreo <> a2.mayoreo Then
                    result = True
                End If
                If .especial <> a2.especial Then
                    result = True
                End If
                If .pgmay <> a2.pgmay Then
                    result = True
                End If
                If .pgmen <> a2.pgmen Then
                    result = True
                End If

            End With




            Return result
        End Operator
        Public Function stringcon() As String
            Dim sep As String = "|"
            Dim st As String = clave
            st &= sep & descripcion
            st &= sep & menudeo
            st &= sep & mayoreo
            st &= sep & pme
            st &= sep & especial
            st &= sep & precio
            st &= sep & pgmen
            st &= sep & pgmay
            st &= sep & fum
            st &= sep & pq
            st &= sep & cbarra
            If tipo = "Artículos" Then
                st &= sep & "Articulos"
            Else
                st &= sep & tipo
            End If

            st &= sep & einf
            st &= sep & tag
            st &= sep & user.idnameusu
            Return st
        End Function

        Public Sub fromreader(lector As OleDb.OleDbDataReader)
            artfromreader(Me, lector)
        End Sub


        Public Sub checkallex(ByRef ds As BindingSource)


        End Sub
        Public Sub New()
            clave = ""
            updatecloud = True
            tipo = "Artículos"
            einf = False
            descripcion = ""
            precio = 0
            importe = 0
            cantidad = 0
            pgmen = 0
            pgmay = 0
            tp = ""
            pq = "PZA"
            fum = Now
            ReDim paquetes(0)
            paquetes(0) = pq
            cbarra = ""
            stock = 0
            ea1 = 0
            ea2 = 0
            found = False
        End Sub
        Public Sub New(clave As String)
            load(clave, True)
        End Sub
        Public Sub Dispose() _
           Implements IDisposable.Dispose
            ' Dispose of unmanaged resources.
            Dispose()
            ' Suppress finalization.
            GC.SuppressFinalize(Me)
        End Sub

        Public Sub brokenme(motivo As String)
            Dim st As String = "INSERT INTO Perdidas (Fecha,Cantidad,Clave,Descripcion,Motivo,Usuario) Values ('" & Now & "'," & cantidad & ",'" & clave & "','" & descripcion & "','" & motivo & "','" & user.idnameusu & "')"
            Dim orden As OleDbCommand = New OleDbCommand(st, conart)
            orden.ExecuteNonQuery()
            modexistencias(existencias, False)
        End Sub

        Public Sub load(clodes As String, exact As Boolean)
            Dim orden As OleDb.OleDbCommand
            Dim lector As OleDb.OleDbDataReader
            Dim st As String = "SELECT * FROM articulos WHERE "
            If exact Then
                st &= "clave='" & clodes & "' AND Del=FALSE"
            Else
                st &= "clave LIKE '" & clodes & "%' OR descripción LIKE '%" & clodes & "%' OR cbarra LIKE '" & clodes & "'  AND Del=FALSE ORDER BY descripción"
            End If
            orden = New OleDb.OleDbCommand(st, conart)
            lector = orden.ExecuteReader
            found = False
            If Not lector.HasRows Then
                clave = clodes

            End If
            While lector.Read
                clave = lector("Clave")
                descripcion = lector("Descripción")
                If Not DBNull.Value.Equals(lector("precio")) Then
                    precio = lector("precio")
                Else
                    precio = 0
                End If

                menudeo = lector("Menudeo")
                mayoreo = lector("Mayoreo")
                If Not DBNull.Value.Equals(lector("Especial")) Then
                    especial = lector("Especial")
                Else
                    especial = 0
                End If

                If Not DBNull.Value.Equals(lector("PME")) Then
                    pme = lector("PME")
                Else
                    pme = 0
                End If
                existencias = voiddeleter(lector("Existencias"))

                If Not DBNull.Value.Equals(lector("paquete")) Then
                    pq = lector("paquete")
                Else
                    pq = "PZA"
                End If
                If Not DBNull.Value.Equals(lector("Cbarra")) Then
                    cbarra = lector("Cbarra")
                Else
                    cbarra = ""
                End If
                If Not DBNull.Value.Equals(lector("pgmay")) Then
                    pgmay = lector("pgmay")
                Else
                    pgmay = 0
                End If
                If Not DBNull.Value.Equals(lector("pgmen")) Then

                    pgmen = lector("pgmen")
                Else
                    pgmen = 0
                End If

                tipo = lector("tipo")
                fum = lector("fum")
                If Not DBNull.Value.Equals(lector("stock")) Then
                    stock = lector("stock")
                Else
                    stock = 0
                End If
                einf = lector("einf")
                found = True
                If Not DBNull.Value.Equals(lector("notas")) Then
                    notas = lector("notas")
                Else
                    notas = ""
                End If
                If Not DBNull.Value.Equals(lector("tags")) Then
                    tag = lector("tags")
                Else
                    tag = ""
                End If
            End While
            lector.Close()



        End Sub


        Public Sub altaart(Optional canta As Decimal = 0)
            existencias = canta
            Try
                Dim hilo As Thread = New Thread(New ThreadStart(AddressOf altathread))
                hilo.IsBackground = True
                hilo.Start()
            Catch ex As Exception
                LogAppError(ex, "Estructuras.altaart")
            End Try
        End Sub
        Public Sub altathread()
            Try
                Dim st As String = "INSERT INTO Articulos (Clave,Cbarra,Descripción,Precio,Mayoreo,Menudeo,Especial,PME,Paquete,Existencias,ea1,ea2,stock,Tipo,Einf,PGmen,PGmay,Tags,Fum,Fuc,usuariofum) VALUES ('"
                st &= sqlSafe(clave) & sep
                st &= sqlSafe(cbarra) & sep
                st &= sqlSafe(descripcion) & "',"
                st &= precio & ","
                st &= mayoreo & ","
                st &= menudeo & ","
                st &= especial & ","
                st &= pme & ",'"
                st &= sqlSafe(pq) & "',"
                st &= existencias & ","
                st &= ea1 & ","
                st &= ea2 & ","
                st &= stock & ",'"
                st &= sqlSafe(tipo) & "',"
                st &= einf & ","
                st &= pgmen & ","
                st &= pgmay & ",'"
                st &= sqlSafe(tag) & sep
                st &= fum & sep
                st &= fuc & sep
                st &= sqlSafe(user.idnameusu) & "')"
                SyncLock conart
                    Using cmd As New OleDb.OleDbCommand(st, conart)
                        cmd.ExecuteNonQuery()
                    End Using
                End SyncLock
                Try
                    autocompletarart()
                    notifier.cambiodeexistencias(clave)
                    nube.QueueArticuloDelta(Me, "UPSERT")
                Catch ex As Exception
                    nube.SyncLog("Error al encolar alta de artículo: " & ex.Message, True)
                End Try
            Catch ex As Exception
                LogAppError(ex, "Estructuras.altathread")
            End Try
        End Sub
        Public Sub setexistencias(cantidad As Decimal)
            Try
                Dim st As String = "UPDATE Articulos SET Existencias=" & cantidad & " WHERE Clave='" & sqlSafe(clave) & "'"
                SyncLock conart
                    Using cmd As New OleDb.OleDbCommand(st, conart)
                        cmd.ExecuteNonQuery()
                    End Using
                End SyncLock
            Catch ex As Exception
                LogAppError(ex, "Estructuras.setexistencias")
            End Try
        End Sub
        Public Sub updateart(Optional uexis As Boolean = False, Optional cant As Decimal = 0)
            upexis = uexis
            cantidad = cant
            Try
                Dim hilo As Thread = New Thread(New ThreadStart(AddressOf updateartthread))
                hilo.IsBackground = True
                hilo.Start()
            Catch ex As Exception
                LogAppError(ex, "Estructuras.updateart")
            End Try
        End Sub
        Public Sub updateartthread()
            Try
                Dim sep2 As String = "', "
                Dim st As String = "UPDATE Articulos SET "
                st &= "Descripción='" & sqlSafe(descripcion) & sep2
                st &= "Menudeo=" & menudeo & ","
                st &= "Mayoreo=" & mayoreo & ","
                st &= "Precio=" & precio & ","
                st &= "PME=" & pme & ","
                st &= "Especial=" & especial & ","
                st &= "stock=" & stock & ","
                st &= "paquete='" & sqlSafe(pq) & sep2
                st &= "cbarra='" & sqlSafe(cbarra) & sep2
                st &= "Notas='" & sqlSafe(notas) & sep2
                st &= "Tags='" & sqlSafe(tag) & sep2
                st &= "fum='" & fum & sep2
                st &= "usuariofum='" & sqlSafe(user.idnameusu) & sep2
                st &= "tipo='" & sqlSafe(tipo) & "',"
                st &= "einf=" & einf
                st &= " WHERE clave='" & sqlSafe(clave) & "'"
                SyncLock conart
                    Using cmd As New OleDb.OleDbCommand(st, conart)
                        cmd.ExecuteNonQuery()
                    End Using
                End SyncLock
                If clave = "LL26PA" Then
                    Dim stLog As String = "INSERT INTO Lognube(Fecha,Descripción,Usuario) VALUES ('" & nowsql() & "','Modificacion','" & sqlSafe(user.idnameusu) & "')"
                    SyncLock conart
                        Using cmdLog As New OleDb.OleDbCommand(stLog, conart)
                            cmdLog.ExecuteNonQuery()
                        End Using
                    End SyncLock
                End If
                updateincloud(st)
                If upexis Then
                    setexistencias(cantidad)
                End If

                notifier.cambiodeexistencias(clave)
            Catch ex As Exception
                LogAppError(ex, "Estructuras.updateartthread")
            End Try
        End Sub
        Private Sub updateincloud(st As String)
            Try
                nube.QueueArticuloDelta(Me, "UPSERT")
            Catch ex As Exception
                nube.SyncLog("Error al encolar modificación de artículo: " & ex.Message, True)
            End Try
        End Sub
        Public Sub deleteme(Optional ask As Boolean = True)
            If ask Then
                If dialogs.pregunta("¿Desea eliminar el siguiente artículo?" & vbNewLine & "Clave:" & clave & vbNewLine & "Descripción:" & descripcion) = DialogResult.No Then
                    Exit Sub
                End If
            End If
            Try
                Dim hilo As Thread = New Thread(New ThreadStart(AddressOf deletemetrhead))
                hilo.IsBackground = True
                hilo.Start()
            Catch ex As Exception
                LogAppError(ex, "Estructuras.deleteme")
            End Try
        End Sub

        Public Sub deletemetrhead()
            Try
                Dim st As String = "UPDATE Articulos SET Del=TRUE, Fechadel='" & fechasql(Now) & "' WHERE  Clave='" & sqlSafe(clave) & "'"
                SyncLock conart
                    Using cmd As New OleDb.OleDbCommand(st, conart)
                        cmd.ExecuteNonQuery()
                    End Using
                End SyncLock
                Try
                    nube.QueueArticuloDelta(Me, "DELETE")
                Catch ex As Exception
                    nube.SyncLog("Error al encolar eliminación de artículo: " & ex.Message, True)
                End Try
                If System.IO.File.Exists(pathimage & clave & ".jpg") Then
                    Try
                        System.IO.File.Delete(pathimage & clave & ".jpg")
                    Catch
                    End Try
                End If
                If System.IO.Directory.Exists(pathimage & clave) Then
                    Try
                        System.IO.Directory.Delete(pathimage & clave, True)
                    Catch
                    End Try
                End If
            Catch ex As Exception
                LogAppError(ex, "Estructuras.deletemetrhead")
            End Try
        End Sub
        Private Sub modexistenciasthread()
            Try
                If tipo = "Trabajo" Or einf Then
                    Exit Sub
                End If
                Dim st As String = "UPDATE Articulos SET Existencias=" & existencias & ", FUC='" & Now & "' WHERE clave='" & sqlSafe(clave) & "'"

                SyncLock conart
                    Using cmd As New OleDbCommand(st, conart)
                        cmd.ExecuteNonQuery()
                    End Using
                End SyncLock

                If clave = "LL26PA" Then
                    Dim stLog As String = "INSERT INTO Lognube(Fecha,Descripción,Usuario) VALUES ('" & nowsql() & "','Modificacion','" & sqlSafe(user.idnameusu) & "')"
                    SyncLock consys
                        Using cmdLog As New OleDb.OleDbCommand(stLog, consys)
                            cmdLog.ExecuteNonQuery()
                        End Using
                    End SyncLock
                End If
                notifier.cambiodeexistencias(clave)
            Catch ex As Exception
                LogAppError(ex, "Estructuras.modexistenciasthread")
            End Try
        End Sub
        Public Sub modexistencias(Optional cant As Decimal = 0, Optional add As Boolean = True)
            If add Then
                existencias += cant
            Else
                existencias -= cant
            End If
            If existencias < 0 Then
                existencias = 0
            End If
            Try
                Dim hilo As Thread = New Thread(New ThreadStart(AddressOf modexistenciasthread))
                hilo.IsBackground = True
                hilo.Start()
            Catch ex As Exception
                LogAppError(ex, "Estructuras.modexistencias")
            End Try
        End Sub
    End Class
    Public Class dgventa : Implements IDisposable
        Dim addnew As Boolean = False
        Public articulo() As articuloventa
        Public Sub Dispose() _
           Implements IDisposable.Dispose
            ' Dispose of unmanaged resources.
            Dispose()
            ' Suppress finalization.
            GC.SuppressFinalize(Me)
        End Sub

        Public Overloads Sub addart(claves As String, des As String, tps As String, cantidads As Integer, importes As Decimal, paquete As String)
            ''carga articulos de una venta
            Dim i As Integer
            If addnew Then
                ReDim Preserve articulo(articulo.Count)
            Else
                addnew = True
            End If
            i = articulo.Count - 1
            articulo(i) = New articuloventa
            With articulo(i)
                .load(claves, True)
                .clave = claves
                .descripcion = des
                .tp = tps
                .cantidad = cantidads
                .importe = importes
                .precio = returnpreonly(tps)
                .pq = paquete
            End With
        End Sub
        Public Overloads Sub addart(claves As String)
            Dim i As Integer
            If addnew Then
                ReDim Preserve articulo(articulo.Count)
            Else
                addnew = True
            End If
            i = articulo.Count - 1
            articulo(i) = New articuloventa
            articulo(i).load(claves, True)
        End Sub
        Public Overloads Sub addart(art As articuloventa)
            Dim i As Integer
            If addnew Then
                ReDim Preserve articulo(articulo.Count)
            Else
                addnew = True
            End If
            i = articulo.Count - 1
            articulo(i) = New articuloventa
            articulo(i) = art
        End Sub
        Public Overloads Sub addart(clave As String, cantidad As Integer, descripcion As String, paquete As String, precio As Decimal, importe As Decimal, tpv As String, notas As String, trabajo As Boolean, exis As Decimal)
            Dim i As Integer
            If addnew Then
                ReDim Preserve articulo(articulo.Count)
            Else
                addnew = True
            End If

            i = articulo.Count - 1
            articulo(i) = New articuloventa

            articulo(i).clave = clave

            articulo(i).cantidad = cantidad
            articulo(i).descripcion = descripcion
            articulo(i).pq = paquete
            articulo(i).tp = tpv
            articulo(i).precio = precio
            articulo(i).importe = importe
            articulo(i).notas = notas
            articulo(i).existencias = exis
            If trabajo Then
                articulo(i).tipo = "Trabajo"
            End If
        End Sub
        Public Sub clear()
            ReDim articulo(0)
            articulo(0) = New articuloventa
            addnew = False

        End Sub
        Public Sub New()
            clear()


        End Sub
        Public Function loadfromdg(ByRef dg As DataGridView) As Decimal
            clear()
            loadfromdg = 0
            Dim j As Integer
            For j = 0 To dg.Rows.Count - 1
                If (Not dg("cancel", j).Value) And (dg("clave", j).Value <> "") Then
                    addart(dg("clave", j).Value, Val(dg("can", j).Value), dg("des", j).Value, dg("pq", j).Value, returnpreonly(dg("Tpv", j).Value), dg("impv", j).Value, dg("Tpv", j).Value, dg("notas", j).Value, dg("tra", j).Value, dg("exis", j).Value)
                    loadfromdg += dg("impv", j).Value
                End If

            Next
        End Function
        Public Sub showarts()
            Dim i As Integer = 0
            For i = 1 To articulo.Count - 2
                MessageBox.Show(articulo(i).clave)

            Next
        End Sub
    End Class

    Public Class repbic
        Public folio As String
        Public countpago As Integer
        Public clnt As Cliente
        Public fcreacion As Date
        Public fcancel As Date
        Public finalizadaall As Boolean
        Dim bolnew As Boolean = True
        Public fentrega As Date
        Public bentregada As Boolean
        Public total As Decimal
        Public subtotal As Decimal
        Public resta As Decimal
        Public tpagado As Decimal
        Public bici() As bicicleta
        Public pago() As pagorep
        Public usuario As String
        Public modi As Boolean
        Public Function saverep() As String
            If modi Then
                deleteme()
            End If
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
            st = "INSERT INTO nota(Folio,Cliente,Fecha,Hora,Total,Direccion,RFC,telcasa,telcel,mail,Fcreacion,Usuario) VALUES ('" & folio & "','" & clnt.Nombre & "','" & fcreacion.ToShortDateString & "','" & fcreacion.ToShortTimeString & "'," & total & ",'" & clnt.Direccion & "','" & clnt.RFC & "','" & clnt.telcasa & "','" & clnt.telcel & "','" & clnt.mail & "','" & fechasql(fcreacion) & "','" & user.idnameusu & "')"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            lector = orden.ExecuteReader
            lector.Close()
            saverep = folio
            For i = 1 To bici.Count - 1
                Dim banderabnew As Boolean = False
                With bici(i)
                    ''agrega bici
                    st = "INSERT INTO bici(folio,id,rodada,marca,color,nota,fhentrega,bentregada,bfinalizada,bcancelada,Entregada,Finalizada,Realizo,Almacenada,Ralmacenada,Rcancel,Cancelada,Entrego ) VALUES ('"
                    st &= folio & "',"
                    st &= i & ",'"
                    st &= .rodada & sep
                    st &= .marca & sep
                    st &= .color & sep
                    st &= .Nota & sep
                    st &= .fentrega & "',"
                    st &= .bentregada & ","
                    st &= .bfinalizada & ","
                    st &= .bcancelada & ",'"
                    st &= .fentregada & sep
                    st &= .finalizada & sep
                    st &= .realizo & "',"
                    st &= .almacenada & ",'"
                    st &= .ralmacenada & sep
                    st &= .bcancelada & sep
                    st &= .rcancel & sep
                    st &= .cancelada & sep
                    st &= .entrego & "')"
                    orden = New OleDb.OleDbCommand(st, conbicrep)

                    lector = orden.ExecuteReader
                    'agrega articulos de bici
                    For k = 0 To .dg.articulo.Count - 1
                        If .dg.articulo(k).clave <> "" Then
                            st = "INSERT INTO art(Folio,id,Cantidad,Clave,Tp,Tipo,cancel,paquete,Descripción) VALUES ('"
                            st &= folio & "',"
                            st &= i & ","
                            st &= .dg.articulo(k).cantidad & ",'"
                            st &= .dg.articulo(k).tp & "',"
                            st &= .dg.articulo(k).tipo & ","
                            st &= .dg.articulo(k).cancel & ",'"
                            st &= .dg.articulo(k).pq & sep
                            st &= .dg.articulo(k).descripcion & "')"
                            orden = New OleDb.OleDbCommand(st, conbicrep)
                            orden.ExecuteNonQuery()
                            'If .dg.articulo(k).tipo = "Trabajo" Then

                            '    st = "INSERT INTO trabajos(Folio,Clave,Descripcion,Importe) VALUES ('"
                            '    st &= folio & sep
                            '    st &= .dg.articulo(k).clave & sep
                            '    st &= .dg.articulo(k).descripcion & "',"
                            '    st &= returnpreonly(.dg.articulo(k).tp) & ")"
                            '    orden = New OleDbCommand(st, contra)
                            '    orden.ExecuteNonQuery()
                            'End If
                        End If
                    Next
                End With
            Next
            lector.Close()
            'If modificar And stmod <> "" Then
            '    st = "INSERT INTO Mod(Folio,Descripcion,Fecha,Usuario,Autorizo,Nota) VALUES ('" & folio & "','" & stmod & "','" & nowsql() & "','" & user.idnameusu & "','" & nameaut & "','" & notaut & "')"
            '    orden = New OleDb.OleDbCommand(st, conbicrep)
            '    orden.ExecuteNonQuery()
            'End If
        End Function
        Public Sub deleteme()
            Dim st As String = "DELETE FROM nota WHERE Folio='" & folio & "'"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            orden.ExecuteNonQuery()
            st = "DELETE FROM bici WHERE Folio='" & folio & "'"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            orden.ExecuteNonQuery()
            st = "DELETE FROM art WHERE Folio='" & folio & "'"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            orden.ExecuteNonQuery()
        End Sub
        Public Function lineas() As Integer
            Dim cant As Integer = 0
            Dim i As Integer
            For i = 0 To bici.Count - 1
                cant += bici(i).dg.articulo.Count
            Next
            cant += bici.Count * 3
            lineas = cant
        End Function

        Public Sub addbici(bicis As bicicleta)
            If Not bolnew Then
                ReDim Preserve bici(bici.Count)
            Else
                bolnew = False
            End If
            bici(bici.Count - 1) = New bicicleta
            bici(bici.Count - 1) = bicis
        End Sub
        Public Sub addpago(cantidad As Decimal, fecha As Date)
            Dim i As Integer = countpago
            countpago += 1
            ReDim Preserve pago(countpago)
            pago(countpago) = New pagorep
            pago(countpago).cantidad = cantidad
            pago(countpago).fecha = fecha
        End Sub
        Public Sub New()
            countpago = 0
            clnt = New Cliente
            ReDim bici(0)
            ReDim pago(countpago)
            bici(0) = New bicicleta
            pago(0) = New pagorep
            modi = False
            bolnew = True
        End Sub
        Public Sub loadfromdgvent(rep As reparacion)
            clnt = New Cliente
            ReDim bici(0)

            With rep
                clnt.Nombre = .comboclnt.Text
                clnt.Direccion = .direccion.Text
                clnt.RFC = .rfc.Text
                clnt.mail = .mail.Text
                clnt.telcasa = .telcasa.Text
                clnt.telcel = .telcel.Text
            End With

            For i = 1 To rep.tabe.TabPages.Count - 1
                Dim tab As Object = rep.tabe.TabPages.Item(i).Controls("pbicicar").Controls
                Dim k As Integer = i - 1
                If i > 1 Then
                    ReDim Preserve bici(i)
                End If
                bici(k) = New bicicleta

                With tab
                    bici(k).rodada = .Item("Rodada").text
                    bici(k).marca = .Item("marca").text
                    bici(k).color = .Item("Color").text
                    bici(k).Nota = .Item("Notas").text
                    bici(k).fentrega = .item("fe").Value.ToShortDateString() & " " & .item("hora").text & ":" & .item("minutos").text & " " & .item("formato").text
                    If .item("Finalizada").checked Then
                        Dim stfinalizada As String = ""
                        Dim stnombre As String = ""
                        fecharnamefin(.item("Finalizada").text, stnombre, stfinalizada)

                        ''  MessageBox.Show(stfinalizada)
                    End If

                End With


            Next





        End Sub

        Public Sub loadrep(folios As String) 'carga una reparación de la base de datos
            clnt = New Cliente
            ReDim bici(0)
            ReDim pago(0)
            folio = folio
            ''Leemos datos de cliente y fechas
            Dim st As String = "SELECT * FROM Nota WHERE FOLIO='" & folio & "'"
            Dim orden As OleDbCommand = New OleDbCommand(st, conbicrep)
            Dim lector As OleDbDataReader = orden.ExecuteReader

            While lector.Read
                With clnt
                    .Nombre = lector("Cliente")
                    .Direccion = lector("Direccion")
                    .RFC = lector("RFC")
                    .telcasa = lector("telcasa")
                    .telcel = lector("telcel")
                    .mail = lector("mail")
                End With
                fcreacion = lector("Fcreacion")
                total = lector("Total")
                fcancel = lector("fcancelada")
                usuario = lector("usuario")
                finalizadaall = lector("Finalizadaal")
                bentregada = lector("bentregada")
                fentrega = lector("Fechae")
            End While
            lector.Close()

            ''Leemos datos de cada bicicleta

            Dim i As Integer = 0
            st = "SELECT * FROM bici WHERE Folio='" & folio & "' ORDER BY Id"
            orden = New OleDbCommand(st, conbicrep)
            lector = orden.ExecuteReader
            While lector.Read
                If i > 0 Then
                    ReDim Preserve bici(i + 1)
                End If
                bici(i) = New bicicleta
                With bici(i)
                    .id = lector("id")
                    .marca = lector("Marca")
                    .color = lector("Color")
                    .rodada = lector("Rodada")
                    .Nota = lector("Nota")
                    .fentregada = lector("Entregada")
                    .finalizada = lector("finalizada")
                    .realizo = lector("realizo")
                    .cancelada = lector("cancelada")
                    .bentregada = lector("bentregada")
                    .bfinalizada = lector("bfinalizada")
                    .bcancelada = lector("bcancelada")
                    .fentrega = lector("fhentrega")
                    .almacenada = lector("Almacenada")
                    .ralmacenada = lector("ralmacenada")
                    .rcancel = lector("rcancel")
                    .entrego = lector("entrego")
                End With

                Dim st2 As String = "SELECT * FROM art WHERE Folio='" & folio & " AND Id=" & bici(i).id & ""
                Dim orden2 As OleDbCommand = New OleDbCommand(st2, conbicrep)
                Dim lector2 As OleDbDataReader = orden2.ExecuteReader
                Dim k As Integer = 0
                ReDim bici(i).dg.articulo(0)

                While lector2.Read

                    If k > 0 Then
                        ReDim Preserve bici(i).dg.articulo(k + 1)
                    End If
                    bici(i).dg.articulo(k) = New articuloventa

                    With bici(i).dg.articulo(k)
                        .load(lector2("clave"), True)
                        .cantidad = lector2("cantidad")
                        .cancel = lector2("cancel")
                        .colocado = lector2("colocado")
                        .trabajo = lector2("trabajo")
                    End With
                    k += 1
                End While


                i += 1
            End While
            'cargamos pagos
            st = "SELECT Id,Fecha,Cantidad, Efectivo, Transferencia, Tarjeta, Apartado, Usuario FROM pagos WHERE Folio='" & folio & "'"
            orden = New OleDb.OleDbCommand(st, conbicrep)
            lector = orden.ExecuteReader
            ReDim pago(0)
            Dim b As Boolean = False
            tpagado = 0
            While lector.Read
                If b = False Then
                    pago(0).cantidad = lector("Cantidad")
                    pago(0).fecha = lector("Fecha")
                    b = True
                Else
                    i = pago.Count + 1
                    ReDim Preserve pago(i)
                    pago(i).cantidad = lector("Cantidad")
                    pago(i).fecha = lector("Fecha")

                End If
                tpagado += lector("Cantidad")

            End While
        End Sub

    End Class
    Public Class bicicleta
        Public rodada As String
        Public marca As String
        Public color As String
        Public Nota As String
        Public fentrega As Date
        Public fentregada As Date
        Public finalizada As Date
        Public falmacenada As Date
        Public realizo As String
        Public cancelada As Date
        Public entrego As String
        Public bentregada As Boolean
        Public bfinalizada As Boolean
        Public bcancelada As Boolean
        Public almacenada As Boolean
        Public ralmacenada As String
        Public rcancel As String
        Public total As Decimal
        Public printable As Boolean = True
        Public dg As dgventa
        Public id As Integer
        Public printeable As Boolean = True


        Public Sub addnew(ide As Integer, rodadas As String, marcas As String, colors As String, notas As String, entrega As Date, dgs As DataGridView)
            rodada = rodadas
            id = ide
            marca = marcas
            color = colors
            Nota = notas
            fentrega = entrega
            dg = New dgventa
            printable = True
            total = dg.loadfromdg(dgs)
            printeable = True
        End Sub
        Public Sub New()
            clear()
            dg = New dgventa

        End Sub
        Public Sub clear()
            rodada = ""
            marca = ""
            color = ""
            Nota = ""
            fentrega = Today
            finalizada = Today
            realizo = ""
            cancelada = Today
            entrego = ""
            bentregada = False
            bfinalizada = False
            bcancelada = False
            almacenada = False
            ralmacenada = ""
            rcancel = ""
            total = 0
        End Sub
    End Class
    Public Class pagos : Implements IDisposable

        Public allowadeudo As Boolean
        Public efectivo As Decimal
        Public transferencia As Decimal
        Public tarjeta As Decimal
        Public apartado As Decimal
        Public total As Decimal
        Public adeudo As Decimal
        Public pagado As Decimal
        Public resta As Decimal
        Public descuento As Decimal
        Public fecha As Date
        Public preaduedo As Decimal
        Private disposedValue As Boolean
        Public Function calculatedescuento() As Decimal
            descuento = total - efectivo - tarjeta - transferencia
            If descuento < 0 Then
                descuento = 0
            End If
            Return descuento

        End Function

        Private Sub calculateadeudo()
            If allowadeudo Then
                adeudo = total + preaduedo
                adeudo = adeudo - efectivo
                adeudo = adeudo - transferencia
                adeudo = adeudo - tarjeta
                adeudo = adeudo - apartado
                adeudo = adeudo - descuento
                If adeudo < 0 Then
                    adeudo = 0
                End If
            End If
            pagado = efectivo + transferencia + tarjeta + apartado

        End Sub
        Public Sub addefectivo(cantidad As Decimal)
            efectivo = efectivo + cantidad
            calculateadeudo()
        End Sub
        Public Sub addtransferencia(cantidad As Decimal)
            transferencia = transferencia + cantidad
            calculateadeudo()
        End Sub
        Public Sub addapartado(cantidad As Decimal)
            apartado = apartado + cantidad
            calculateadeudo()
        End Sub
        Public Sub adddescuento(cantidad As Decimal)
            descuento = descuento + cantidad
            calculateadeudo()
        End Sub
        Public Sub addtarjeta(cantidad As Decimal)
            tarjeta = tarjeta + cantidad
            calculateadeudo()
        End Sub
        Public Sub addadeudo(cantidad As Decimal)
            adeudo = adeudo + cantidad
            calculateadeudo()
        End Sub
        Public Sub New(Optional adeudo As Boolean = True)
            efectivo = 0
            resta = 0
            transferencia = 0
            tarjeta = 0
            apartado = 0
            total = 0
            fecha = Now
            adeudo = 0
            allowadeudo = adeudo
            pagado = 0
        End Sub
        Public Sub loadfromdg(dg As DataGridView, Optional allowade As Boolean = True, Optional total As Decimal = 0)
            allowadeudo = allowade
            efectivo = 0
            transferencia = 0
            descuento = 0
            tarjeta = 0
            pagado = 0
            Dim i As Integer = 0
            For i = 0 To dg.Rows.Count - 1
                Dim a As DataGridViewComboBoxCell = dg("Tipo", i)
                Dim cantidad As Decimal = dg("cantidad", i).Value
                Select Case a.Value
                    Case "1.-Efectivo"

                        If (pagado + cantidad) > total Then
                            addefectivo(total - pagado)
                        Else
                            addefectivo(cantidad)
                        End If

                    Case "2.-Tarjeta"
                        addtarjeta(cantidad)
                    Case "3.-Transferencia"
                        addtransferencia(cantidad)
                    Case "4.-Descuento"
                        adddescuento(cantidad)
                End Select


            Next
            If pagado > total Then
                pagado = total
            End If

        End Sub
        Public Sub loadpago(folio As String, id As Integer)
            Dim st As String = "SELECT Efectivo,Transferencia,Tarjeta,Apartado,Fecha FROM pagos WHERE Folio='" & folio & "' AND Id=" & id
            Dim ordenc As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, conbicrep)
            Dim lectorc As OleDb.OleDbDataReader = ordenc.ExecuteReader
            While lectorc.Read
                efectivo = lectorc("Efectivo")
                transferencia = lectorc("Transferencia")
                tarjeta = lectorc("Tarjeta")
                apartado = lectorc("Apartado")
                fecha = lectorc("Fecha")
            End While
            lectorc.Close()
        End Sub

        Public Sub Dispose() _
           Implements IDisposable.Dispose
            ' Dispose of unmanaged resources.
            Dispose()
            ' Suppress finalization.
            GC.SuppressFinalize(Me)
        End Sub

    End Class

    Public Class notaventa : Implements IDisposable
        Public acuenta As Decimal = 0
        Public resta As Decimal = 0
        Public fechae As Date
        Public apartado As Boolean = False
        Public folio As String
        Public fecha As Date
        Public articulos As dgventa
        Public hora As Date
        Public pagoadeudo As Boolean = False
        Public subtotal As Decimal
        Public total As Decimal
        Public iva As Decimal
        Public pago As pagos
        Public pagoarp As pagos()
        Public updateexis As Boolean = True
        Public cotizacion As Boolean
        Public usuario As String
        Public clnt As Cliente
        Public Sub convertfromlv(fechas As Date(), arti As articuloventa(), pagoss As pagos())
            If dialogs.pregunta("Esta a punto de convertir los artículos seleccionados en nota de venta" & vbNewLine & "Este proceso es irreversible." & vbNewLine & "¿Desea continuar?") = DialogResult.Yes Then
                folio = getventrepid(1, fechas(0))
                fecha = fechas(0)
                pago = New pagos
                total = 0
                articulos.articulo = arti
                For i = 0 To arti.Count - 1
                    pago.addefectivo(pagoss(i).efectivo)
                    pago.addtransferencia(pagoss(i).transferencia)
                    pago.addtarjeta(pagoss(i).tarjeta)
                    pago.adddescuento(pagoss(i).descuento)
                    pago.total += (arti(i).menudeo * arti(i).cantidad)
                    pago.pagado = pago.total
                Next
                clnt = New Cliente
                clnt = clienteform.moredetailsventa(clnt)
                updateexis = True


                addventa()
                For i = 0 To fechas.Count - 1
                    delventr(fechas(i))
                Next
            End If

        End Sub
        Public Sub Dispose() _
           Implements IDisposable.Dispose
            ' Dispose of unmanaged resources.
            Dispose()
            ' Suppress finalization.
            GC.SuppressFinalize(Me)
        End Sub

        Public Sub loadapartado(folios As String)
            Dim st As String = "SELECT * FROM Apartado WHERE Folio='" & folios & "'"
            Dim orden As OleDbCommand = New OleDbCommand(st, convent)
            Dim lector As OleDbDataReader = orden.ExecuteReader
            apartado = True
            While lector.Read
                folio = lector("folio")
                total = lector("total")
                fecha = lector("fecha")
                fechae = lector("fechar")
                acuenta = lector("Acuenta")
                resta = lector("total") - lector("Acuenta")
                usuario = lector("Usuario")
                clnt = New Cliente
                Dim clnst As String = lector("Cliente")
                If Not clnst.Contains(":") Then
                    clnt.loadclntvent(folio)
                Else
                    clnt.loadclntidst(clnst)
                End If

            End While
            st = "SELECT * FROM Articulos WHERE Folio='" & folio & "'"
            orden = New OleDbCommand(st, convent)
            lector = orden.ExecuteReader
            While lector.Read
                With articulos
                    .addart(lector("clave"), lector("Descripción"), lector("Tp"), lector("Cantidad"), lector("Importe"), lector("paquete"))
                End With
            End While

        End Sub
        Public Sub addpagoapartado()
            Dim st As String = "INSERT INTO Pagoapartado (Folio,Usuario,Efectivo,Tarjeta,Transferencia,Descuento,Fecha) VALUES('"
            st &= folio & sep
            st &= user.idnameusu & "',"
            st &= pago.efectivo & ","
            st &= pago.tarjeta & ","
            st &= pago.transferencia & ","
            st &= pago.descuento & ",'"
            st &= Now & "')"
            fecha = Now
            Dim orden As OleDbCommand = New OleDbCommand(st, convent)
            orden.ExecuteNonQuery()
            st = "UPDATE Apartado SET Acuenta=Acuenta+" & pago.pagado & " WHERE Folio='" & folio & "'"
            orden = New OleDbCommand(st, convent)
            orden.ExecuteNonQuery()
            addventalv()
        End Sub
        Public Sub addapartado()
            Dim st As String = "INSERT INTO Apartado (Folio,Cliente,Fecha,Fechar,Total,Acuenta,Usuario) VALUES ('"
            st &= folio & sep
            st &= clnt.idname & sep
            st &= Now & sep
            st &= fechae & "',"
            st &= total & ","
            st &= 0 & ",'"
            st &= user.idnameusu & "')"
            fecha = Now
            Dim orden As OleDbCommand = New OleDbCommand(st, convent)
            orden.ExecuteNonQuery()
            insertclnt()
            addartsdb()

            addpagoapartado()
            '' addventalv()
        End Sub
        Public Sub New()
            folio = ""

            fecha = Now
            fechae = Now
            clnt = New Cliente
            articulos = New dgventa
            pago = New pagos
            subtotal = 0
            total = 0
            iva = 0
            ReDim pagoarp(0)
            cotizacion = False
            updateexis = True
            pagoadeudo = False
        End Sub
        Public Sub addartsdbthread()
            Try
                Dim st As String
                Dim tra As trabajador = New trabajador
                For i As Integer = 0 To articulos.articulo.Count - 1
                    With articulos.articulo(i)
                        If articulos.articulo(i).clave <> "" Then
                            If .tipo = "Trabajo" Then
                                If tra.id = 0 Then
                                    Try
                                        If main IsNot Nothing AndAlso main.InvokeRequired Then
                                            main.Invoke(Sub()
                                                            tra = trabajadorsel.loader()
                                                        End Sub)
                                        Else
                                            tra = trabajadorsel.loader()
                                        End If
                                    Catch
                                        tra = New trabajador()
                                    End Try
                                End If
                                st = "INSERT INTO trabajos (Fecha,Hora,Folio,Clave,Descripcion,Importe,Cantidad,Realizo) Values ('"
                                st &= fechasql(Today) & "','"
                                st &= fechasql(Now) & "','"
                                st &= sqlSafe(folio) & "','"
                                st &= sqlSafe(.clave) & "','"
                                st &= sqlSafe(.descripcion) & "',"
                                st &= .importe & ","
                                st &= .cantidad & ","
                                st &= tra.id & ")"
                                SyncLock contra
                                    Using cmd As New OleDbCommand(st, contra)
                                        cmd.ExecuteNonQuery()
                                    End Using
                                End SyncLock
                            End If
                        End If
                    End With
                Next
                For i As Integer = 0 To articulos.articulo.Count - 1
                    With articulos.articulo(i)
                        If articulos.articulo(i).clave <> "" Then
                            st = "INSERT INTO Articulos (Folio,Clave,Descripción,Tp,Cantidad,Importe,Paquete) VALUES ('"
                            st &= sqlSafe(folio) & sep
                            st &= sqlSafe(articulos.articulo(i).clave) & sep
                            st &= sqlSafe(articulos.articulo(i).descripcion) & sep
                            st &= sqlSafe(articulos.articulo(i).tp) & sep
                            st &= articulos.articulo(i).cantidad & sep
                            st &= articulos.articulo(i).importe & "','"
                            st &= sqlSafe(articulos.articulo(i).pq) & "')"
                            SyncLock convent
                                Using cmd As New OleDb.OleDbCommand(st, convent)
                                    cmd.ExecuteNonQuery()
                                End Using
                            End SyncLock
                            If updateexis Then
                                .modexistencias(.cantidad, False)
                                updatelastventa(articulos.articulo(i).clave)
                            End If

                        End If
                    End With
                Next
                If Not apartado Then
                    addventalv()
                End If
            Catch ex As Exception
                LogAppError(ex, "Estructuras.addartsdbthread")
            End Try
        End Sub
        Public Sub addartsdb()
            Try
                Dim hilo As Thread = New Thread(New ThreadStart(AddressOf addartsdbthread))
                hilo.IsBackground = True
                hilo.Start()
            Catch ex As Exception
                LogAppError(ex, "Estructuras.addartsdb")
            End Try
        End Sub
        Public Sub addventarapidathread()
            Try
                Dim st As String = "INSERT INTO lv (Descripcion,Cantidad,Cliente,Total,hora,horast,fecha,clave,Usuario,Pagoefe,Pagotrans,Pagotar,Descuento,pagado) VALUES ('"
                With articulos.articulo(0)
                    st &= sqlSafe(.descripcion) & "',"
                    st &= .cantidad & ",'"
                    st &= sqlSafe(clnt.idname) & "',"
                    st &= total & ",'"
                    st &= horasql(Now) & sep
                    st &= horasql(Now) & sep
                    st &= onlyfechasql(Now) & sep
                    st &= sqlSafe(.clave) & sep
                    st &= sqlSafe(user.idnameusu) & "',"
                    st &= pago.efectivo & ","
                    st &= pago.transferencia & ","
                    st &= pago.tarjeta & ","
                    st &= pago.descuento & ","
                    st &= pago.pagado & ""
                    st &= ")"
                    SyncLock convent
                        Using cmd As New OleDb.OleDbCommand(st, convent)
                            cmd.ExecuteNonQuery()
                        End Using
                    End SyncLock
                    .modexistencias(.cantidad, False)
                    If .tipo = "Trabajo" Then
                        Dim tra As trabajador = New trabajador
                        Try
                            If main IsNot Nothing AndAlso main.InvokeRequired Then
                                main.Invoke(Sub()
                                                tra = trabajadorsel.loader()
                                            End Sub)
                            Else
                                tra = trabajadorsel.loader()
                            End If
                        Catch
                            tra = New trabajador()
                        End Try
                        st = "INSERT INTO trabajos (Fecha,Hora,Folio,Clave,Descripcion,Importe,Cantidad,Realizo) Values ('"
                        st &= fechasql(Today) & "','"
                        st &= fechasql(Now) & "','"
                        st &= "V.Rapida" & "','"
                        st &= sqlSafe(.clave) & "','"
                        st &= sqlSafe(.descripcion) & "',"
                        st &= .menudeo & ","
                        st &= .cantidad & ","
                        st &= tra.id & ")"
                        SyncLock contra
                            Using cmd As New OleDbCommand(st, contra)
                                cmd.ExecuteNonQuery()
                            End Using
                        End SyncLock
                    End If
                End With
            Catch ex As Exception
                LogAppError(ex, "Estructuras.addventarapidathread")
            End Try
        End Sub
        Public Sub addventarapida()
            Try
                Dim hilo As Thread = New Thread(New ThreadStart(AddressOf addventarapidathread))
                hilo.IsBackground = True
                hilo.Start()
            Catch ex As Exception
                LogAppError(ex, "Estructuras.addventarapida")
            End Try
        End Sub
        Public Sub addventa()
            If pago.pagado > pago.total Then
                pago.pagado = pago.total
            End If
            Dim st As String = "INSERT INTO venta (Folio,Cliente,Fechae,Fecha,Hora,Total,Usuario,Pagoef,Pagotran,Pagotcr,Descuento,Pagoap,Resta,Adeudoold) VALUES ('"
            Dim ordenc As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, conclnt)
            st &= folio & sep
            st &= clnt.idname & sep
            st &= fecha.ToString & sep
            st &= fecha.ToShortDateString & sep
            st &= fecha.ToLongTimeString & sep
            st &= pago.total & sep
            st &= user.idnameusu & " ',"
            st &= pago.efectivo & ","
            st &= pago.transferencia & ","
            st &= pago.tarjeta & ","
            st &= pago.descuento & ","
            st &= pago.apartado & ","
            st &= pago.adeudo & ","
            st &= clnt.adeudo & ")"

            orden = New OleDb.OleDbCommand(st, convent)
            If clnt.id <> 0 Then
                clnt.modadeudo(pago.adeudo)
            End If
            orden.ExecuteNonQuery()

            addartsdb()


            If clnt.found Then
                clnt.addmov("Compra nota:" & folio, pago.total, pago.pagado, pago.adeudo)
                clnt.updatecompra()
            Else
                insertclnt()
            End If


        End Sub
        Public Sub insertclnt()
            Dim st As String
            Dim orden As OleDbCommand
            With clnt
                If .Nombre <> "Mostrador" Or .Direccion <> "" Or .RFC <> "" Or .telcasa <> "" Or .telcel <> "" Or .mail <> "" Then
                    st = "INSERT INTO Clientes (Folio,Nombre,Dirección,RFC,Telcasa,Telcel,mail) VALUES ('"
                    st &= folio & "','"
                    st &= .Nombre & "','"
                    st &= .Direccion & "','"
                    st &= .RFC & "','"
                    st &= .telcasa & "','"
                    st &= .telcel & "','"
                    st &= .mail & "')"
                    orden = New OleDb.OleDbCommand(st, convent)
                    orden.ExecuteNonQuery()
                End If
            End With
        End Sub
        Public Overridable Sub addventalv()
            Dim st As String = "INSERT INTO lv(Clave,Descripcion,total,usuario,fecha,hora,horast,cantidad,cliente,Pagoefe,Pagotrans,Pagotar,descuento,pagado,nota) VALUES ('"

            st &= folio & "','"
            If apartado Then
                st &= "Abono apartado de " & clnt.Nombre & "',"
            Else
                st &= "Nota de venta a " & clnt.Nombre & "',"
            End If
            st &= pago.pagado & ",'"
            st &= user.idnameusu & sep
            st &= onlyfechasql(fecha) & sep
            st &= horasql(fecha) & sep
            st &= horasql(fecha) & "',"
            st &= 1 & ",'"
            st &= clnt.idname & "',"
            st &= pago.efectivo & ","
            st &= pago.transferencia & ","
            st &= pago.tarjeta & ","
            st &= pago.descuento & ","
            st &= pago.pagado & ","
            st &= "True)"

            Dim orden = New OleDb.OleDbCommand(st, convent)
            orden.ExecuteNonQuery()

        End Sub
        Public Sub loadventa(folios As String)
            Dim st As String = "SELECT * FROM venta WHERE Folio='" & folios & "'"
            Dim orden As OleDbCommand = New OleDbCommand(st, convent)
            Dim lector As OleDbDataReader = orden.ExecuteReader
            While lector.Read
                folio = lector("folio")
                total = lector("total")
                fecha = lector("fecha")
                hora = lector("hora")
                usuario = lector("Usuario")
                clnt = New Cliente
                Dim clnst As String = lector("Cliente")
                If Not clnst.Contains(":") Then
                    clnt.loadclntvent(folio)
                Else
                    clnt.loadclntidst(clnst)
                End If
                pago.efectivo = lector("pagoef")
                pago.transferencia = lector("Pagotran")
                pago.tarjeta = lector("Pagotcr")
                pago.descuento = lector("descuento")
                pago.pagado = pago.efectivo + pago.transferencia + pago.tarjeta
                pago.resta = lector("resta")
                clnt.adeudo = voiddeleter(lector("Adeudoold"))
            End While
            st = "SELECT * FROM Articulos WHERE Folio='" & folio & "'"
            orden = New OleDbCommand(st, convent)
            lector = orden.ExecuteReader
            While lector.Read
                With articulos
                    Try

                        .addart(lector("clave"), lector("Descripción"), lector("Tp"), lector("Cantidad"), lector("Importe"), lector("paquete"))
                    Catch ex As Exception
                        MessageBox.Show("Ocurrio un error al agregar articulo")
                    End Try

                End With
            End While
        End Sub
        Public Sub cancelme()
            Dim st As String = "UPDATE venta SET cancel=TRUE WHERE Folio='" & folio & "'"
            Dim orden As OleDbCommand = New OleDbCommand(st, convent)
            orden.ExecuteNonQuery()
            For i = 0 To articulos.articulo.Count - 1
                With articulos.articulo(i)
                    'redexistencias(.clave, .cantidad, True)
                    .modexistencias(.cantidad)
                End With
            Next
            st = "UPDATE lv SET cancel=TRUE WHERE Clave='" & folio & "'"
            orden = New OleDbCommand(st, convent)
            orden.ExecuteNonQuery()

        End Sub
    End Class
    Public Class pagorep
        Public cantidad As Decimal
        Public fecha As Date
        Public Sub New()
            cantidad = 0
            fecha = Now
        End Sub
    End Class
    Public Class arttranot
        Public tipo As Integer = 0
        Public Clave As String
        Public descripcion As String
        Public precio As Decimal
        Public Sub New()
            tipo = 0
            Clave = ""
            descripcion = ""
            precio = 0
        End Sub
        Public Sub loadart(des As String, pre As Decimal)
            tipo = 1
            Clave = "ARNORE"
            descripcion = Clave & ":" & des
            precio = pre
        End Sub
        Public Sub loadtra(des As String, pre As Decimal)
            tipo = 2
            Clave = "TRNORE"
            descripcion = Clave & ":" & des
            precio = pre
        End Sub
    End Class

End Module
