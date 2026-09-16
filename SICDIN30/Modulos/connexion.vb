Imports System.Net.Sockets
Imports System.Net
Imports System.Threading
Module connexion
    Public tipoconexion() As String = {"HAND", "ACK", "FIN", "CERRAR", "TEST"}
    Public Enum tipobase
        ARTICULOS
        VENTA
        REPARACION
        USUARIO
        CLIENTE
    End Enum
    Class tcpconnexion
        Dim ipservidor As String
        Dim tiendas() As lugar
        Dim myip As String
        Dim ipclient As String
        Dim client As TcpClient
        Dim port As Int32 = 6001
        Dim connection As Boolean
        Public Event mensaje()
        Dim stream As NetworkStream
        Dim stoplistener As Boolean
        Dim server As TcpListener
        Private Function inter(st As String) As Boolean ''interprete de funciones
            Try
                Dim stplit() As String = st.Split("|") ''separamos
                If stplit.Length < 3 OrElse stplit(stplit.Length - 1) <> "FIN" Then
                    Return False
                End If
                Dim sttien As String = stplit(0)
                Dim stbase As String = stplit(1) 'que base
                Dim storden As String = stplit(2) 'que orden
                Dim stdata As String = ""
                Select Case stbase
                    Case "ART"
                        Select Case storden
                            Case "MOD"
                                If stplit.Length > 5 Then
                                    stdata = "UPDATE Articulos SET " & stplit(3) & "=" & stplit(4) & " WHERE Clave='" & sqlSafe(stplit(5)) & "'"
                                End If
                            Case "DEL"
                                If stplit.Length > 3 Then
                                    stdata = "DELETE FROM Articulos WHERE Clave='" & sqlSafe(stplit(3)) & "'"
                                End If
                            Case "NOM"
                                If stplit.Length > 18 Then
                                    Dim art As articuloventa = New articuloventa(stplit(3))
                                    If art.found Then
                                        stdata = "UPDATE Articulos SET Descripción='" & sqlSafe(stplit(4)) & "', "
                                        stdata &= "Menudeo=" & Val(stplit(5)) & ", "
                                        stdata &= "Mayoreo=" & Val(stplit(6)) & ", "
                                        stdata &= "PME=" & Val(stplit(7)) & ", "
                                        stdata &= "Especial=" & Val(stplit(8)) & ", "
                                        stdata &= "Precio=" & Val(stplit(9)) & ", "
                                        stdata &= "PGmen=" & Val(stplit(10)) & ", "
                                        stdata &= "PGmay=" & Val(stplit(11)) & ", "
                                        stdata &= "FUM='" & stplit(12) & "', "
                                        stdata &= "Paquete='" & sqlSafe(stplit(13)) & "', "
                                        stdata &= "cbarra='" & sqlSafe(stplit(14)) & "', "
                                        If stplit(15) = "Articulos" Then
                                            stdata &= "Tipo='" & "Artículos" & "', "
                                        Else
                                            stdata &= "Tipo='" & sqlSafe(stplit(15)) & "', "
                                        End If

                                        stdata &= "Einf=" & stplit(16) & ", "
                                        stdata &= "Tags='" & sqlSafe(stplit(17)) & "', "
                                        stdata &= "usuariofum='" & sqlSafe(stplit(18)) & "' WHERE clave='" & sqlSafe(stplit(3)) & "'"
                                    Else
                                        stdata = "INSERT INTO Articulos (Clave,Descripción,Menudeo,Mayoreo,PME,Especial,Precio,PGmen,PGmay,FUM,Paquete,cbarra,Tipo,Einf,Tags,usuariofum) VALUES ('"
                                        stdata &= sqlSafe(stplit(3)) & "','" 'clave
                                        stdata &= sqlSafe(stplit(4)) & "'," 'des
                                        stdata &= Val(stplit(5)) & "," 'men
                                        stdata &= Val(stplit(6)) & "," 'may
                                        stdata &= Val(stplit(7)) & "," 'pme
                                        stdata &= Val(stplit(8)) & "," 'especial
                                        stdata &= Val(stplit(9)) & "," 'precio
                                        stdata &= Val(stplit(10)) & "," 'pgmen
                                        stdata &= Val(stplit(11)) & ",'" 'pgmay
                                        stdata &= stplit(12) & "','" 'fum
                                        stdata &= sqlSafe(stplit(13)) & "','" 'pq
                                        stdata &= sqlSafe(stplit(14)) & "','" 'cbarras
                                        stdata &= sqlSafe(stplit(15)) & "'," 'tipo
                                        stdata &= stplit(16) & ",'" 'einf
                                        stdata &= sqlSafe(stplit(17)) & "','" 'tags
                                        stdata &= sqlSafe(stplit(18)) & "')" 'usuariofum
                                    End If
                                End If
                        End Select
                        If Not String.IsNullOrEmpty(stdata) Then
                            SyncLock conart
                                Using cmd As New OleDb.OleDbCommand(stdata, conart)
                                    cmd.ExecuteNonQuery()
                                End Using
                            End SyncLock
                        End If
                    Case "CLNT"
                        Select Case storden
                            Case "ADD"
                                If stplit.Length > 13 Then
                                    Dim clnt As Cliente = New Cliente
                                    With clnt
                                        .Nombre = stplit(3)
                                        .Direccion = stplit(4)
                                        .mayoreo = Val(stplit(5))
                                        .credito = stplit(6)
                                        .dc = Val(stplit(7))
                                        .especial = Val(stplit(8))
                                        .RFC = stplit(9)
                                        .telcasa = stplit(10)
                                        .telcel = stplit(11)
                                        .mail = stplit(12)
                                        .ultcompra = stplit(13)
                                        clnt.addme(-1, False)
                                    End With
                                End If
                            Case "DEL"
                                If stplit.Length > 3 Then
                                    Dim clnt As Cliente = New Cliente(stplit(3))
                                End If
                        End Select
                    Case "HND"
                End Select
                Return True
            Catch ex As Exception
                LogAppError(ex, "connexion.inter")
                Return False
            End Try
        End Function

        Public Sub startlistener()
            Try
                stoplistener = False
                myip = "127.0.0.1"
                Try
                    Dim hostName As String = Dns.GetHostName
                    For Each address In Dns.GetHostEntry(hostName).AddressList()
                        If address.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                            myip = address.ToString
                            Exit For
                        End If
                    Next
                Catch
                    myip = "127.0.0.1"
                End Try
                Dim hilo As Thread = New Thread(New ThreadStart(AddressOf listener))
                hilo.IsBackground = True
                hilo.Start()
            Catch ex As Exception
                LogAppError(ex, "connexion.startlistener")
            End Try
        End Sub
        Public Sub New(port As Int32)
            Me.port = port


        End Sub

        Public Function enviarping(tclient As lugar) As Boolean

            Dim data As String = Nothing
            enviarping = False
            Dim tcpClient As New System.Net.Sockets.TcpClient()
            Try

                tcpClient.Connect(tclient.website, tclient.port)
                Dim networkStream As NetworkStream = tcpClient.GetStream()
                If networkStream.CanWrite Then
                    Dim sendBytes As [Byte]() = System.Text.Encoding.ASCII.GetBytes(tienda.apodo & "|HND|FIN")
                    networkStream.Write(sendBytes, 0, sendBytes.Length)
                    enviarping = True

                End If
                networkStream.Close()
            Catch ex As Exception

            End Try

        End Function
        Public Function enviar(st As String, tclient As lugar, tipo As tipobase) As Boolean
            enviar = False
            If voiddeleterstring(tclient.website) = "" Then
                Exit Function
            End If
            Dim stba As String = ""
            Select Case tipo
                Case tipobase.ARTICULOS
                    stba = "ART"
                Case tipobase.VENTA
                    stba = "VEN"
                Case tipobase.CLIENTE
                    stba = "CLNT"
                Case tipobase.REPARACION
                    stba = "REP"
                Case tipobase.USUARIO
                    stba = "USU"
            End Select
            st = tienda.apodo & "|" & stba & "|" & st & "|" & tipoconexion(2)
            Try
                Dim data As String = Nothing
                Dim tcpClient As New System.Net.Sockets.TcpClient()
                tcpClient.Connect(tclient.website, tclient.port)
                Dim networkStream As NetworkStream = tcpClient.GetStream()

                If networkStream.CanWrite Then
                    Dim sendBytes As [Byte]() = System.Text.Encoding.ASCII.GetBytes(st)
                    networkStream.Write(sendBytes, 0, sendBytes.Length)
                    enviar = True

                End If
                networkStream.Close()
            Catch ex As Exception

            End Try

        End Function
        Private Sub listener()
            Try
                Dim localAddr As IPAddress = Nothing
                If Not IPAddress.TryParse(myip, localAddr) Then
                    localAddr = IPAddress.Any
                End If
                Dim data As String = Nothing
                server = New TcpListener(localAddr, port)
                Try
                    server.Start()
                Catch ex As Exception
                    LogAppError(ex, "connexion.listener.server.Start")
                    Exit Sub
                End Try

                Dim bytes(2048) As Byte
                While Not stoplistener
                    Try
                        client = server.AcceptTcpClient()
                        stream = client.GetStream()
                        Dim msg As Byte()
                        Dim i As Int32
                        i = stream.Read(bytes, 0, bytes.Length)
                        While i <> 0
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i)
                            If data <> "" Then
                                If inter(data) Then
                                    msg = System.Text.Encoding.ASCII.GetBytes(tipoconexion(1))
                                    stream.Write(msg, 0, msg.Length)
                                End If
                                data = Nothing
                                i = 0
                            End If
                        End While
                        client.Close()
                    Catch exClient As Exception
                        ' Error en comunicación con un cliente específico, no debe matar el servidor
                        Try
                            If client IsNot Nothing Then client.Close()
                        Catch
                        End Try
                    End Try
                End While
            Catch ex As Exception
                LogAppError(ex, "connexion.listener")
            End Try
        End Sub

    End Class

End Module
