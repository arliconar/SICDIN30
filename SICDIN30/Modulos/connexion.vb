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
            Dim stplit() As String = st.Split("|") ''separamos
            If stplit(stplit.Count - 1) <> "FIN" Then
                inter = False
                Exit Function
            End If
            Dim sttien As String = stplit(0)
            Dim stbase As String = stplit(1) 'que base
            Dim storden As String = stplit(2) 'que orden
            Dim stdata As String = ""
            Select Case stbase
                Case "ART"
                    Select Case storden
                        Case "MOD"
                            stdata = "UPDATE Articulos SET " & stplit(3) & "=" & stplit(4) & " WHERE Clave='" & stplit(5) & "'"
                        Case "DEL"
                            stdata = "DELETE FROM Articulos WHERE Clave='" & stplit(3) & "'"
                        Case "NOM"
                            Dim art As articuloventa = New articuloventa(stplit(3))
                            If art.found Then
                                stdata = "UPDATE Articulos SET Descripción='" & stplit(4) & "', "
                                stdata &= "Menudeo=" & stplit(5) & ", "
                                stdata &= "Mayoreo=" & stplit(6) & ", "
                                stdata &= "PME=" & stplit(7) & ", "
                                stdata &= "Especial=" & stplit(8) & ", "
                                stdata &= "Precio=" & stplit(9) & ", "
                                stdata &= "PGmen=" & stplit(10) & ", "
                                stdata &= "PGmay=" & stplit(11) & ", "
                                stdata &= "FUM='" & stplit(12) & "', "
                                stdata &= "Paquete='" & stplit(13) & "', "
                                stdata &= "cbarra='" & stplit(14) & "', "
                                If stplit(15) = "Articulos" Then
                                    stdata &= "Tipo='" & "Artículos" & "', "
                                Else
                                    stdata &= "Tipo='" & stplit(15) & "', "
                                End If

                                stdata &= "Einf=" & stplit(16) & ", "
                                stdata &= "Tags='" & stplit(17) & "', "
                                stdata &= "usuariofum='" & stplit(18) & "' WHERE clave='" & stplit(3) & "'"

                            Else
                                stdata = "INSERT INTO Articulos (Clave,Descripción,Menudeo,Mayoreo,PME,Especial,Precio,PGmen,PGmay,FUM,Paquete,cbarra,Tipo,Einf,Tags,usuariofum) VALUES ('"
                                stdata &= stplit(3) & "','" 'clave
                                stdata &= stplit(4) & "'," 'des
                                stdata &= stplit(5) & "," 'men
                                stdata &= stplit(6) & "," 'may
                                stdata &= stplit(7) & "," 'pme
                                stdata &= stplit(8) & "," 'especial
                                stdata &= stplit(9) & "," 'precio
                                stdata &= stplit(10) & "," 'pgmen
                                stdata &= stplit(11) & ",'" 'pgmay
                                stdata &= stplit(12) & "','" 'fum
                                stdata &= stplit(13) & "','" 'pq
                                stdata &= stplit(14) & "','" 'cbarras
                                stdata &= stplit(15) & "'," 'tipo
                                stdata &= stplit(16) & ",'" 'einf
                                stdata &= stplit(17) & "','" 'tags
                                stdata &= stplit(18) & "')" 'usuariofum
                            End If
                    End Select
                    Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(stdata, conart)
                    orden.ExecuteReader()
                Case "CLNT"
                    Select Case storden
                        Case "ADD"
                            Dim clnt As Cliente = New Cliente
                            With clnt
                                .Nombre = stplit(3)
                                ' MessageBox.Show(.Nombre)
                                .Direccion = stplit(4)
                                ' MessageBox.Show(.Direccion)
                                .mayoreo = stplit(5)
                                ' MessageBox.Show(.mayoreo)
                                .credito = stplit(6)
                                ' MessageBox.Show(.credito)
                                .dc = stplit(7)
                                '  MessageBox.Show(.dc)
                                .especial = stplit(8)
                                '   MessageBox.Show(.especial)
                                .RFC = stplit(9)
                                '    MessageBox.Show(.RFC)
                                .telcasa = stplit(10)
                                '     MessageBox.Show(.telcasa)
                                .telcel = stplit(11)
                                '      MessageBox.Show(.telcel)
                                .mail = stplit(12)
                                '       MessageBox.Show(.mail)
                                .ultcompra = stplit(13)
                                '        MessageBox.Show(.ultcompra)

                                clnt.addme(-1, False)
                            End With
                        Case "DEL"
                            Dim clnt As Cliente = New Cliente(stplit(3))


                    End Select
                Case "HND"

            End Select
            inter = True


        End Function

        Public Sub startlistener()
            stoplistener = False
            Dim hostName As String = Dns.GetHostName

            For Each address In Dns.GetHostEntry(hostName).AddressList()
                If address.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                    myip = address.ToString
                End If
            Next
            Dim hilo As Thread = New Thread(New ThreadStart(AddressOf listener))
            hilo.Start()
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
            Dim localAddr As IPAddress = IPAddress.Parse(myip)
            Dim data As String = Nothing
            server = New TcpListener(localAddr, port)
            Try
                server.Start()
            Catch ex As Exception
                Exit Sub
            End Try

            Dim state As Integer = 0
            Dim bytes(2048) As Byte
            While Not stoplistener

                client = server.AcceptTcpClient()
                stream = client.GetStream()
                'data = "hand"
                'Dim msg As Byte() = System.Text.Encoding.ASCII.GetBytes(data)
                Dim msg As Byte()

                'stream.Write(msg, 0, msg.Length)
                Dim i As Int32
                i = stream.Read(bytes, 0, bytes.Length)
                While i <> 0

                    ' Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i)
                    '    ' Process the data sent by the client.
                    If data <> "" Then
                        ' MessageBox.Show(data)
                        If inter(data) Then
                            msg = System.Text.Encoding.ASCII.GetBytes(tipoconexion(1))
                            stream.Write(msg, 0, msg.Length)
                        End If
                        data = Nothing
                        i = 0
                    End If
                    '    ' Send bac
                    '    ' k a response.

                End While

                client.Close()
            End While
        End Sub

    End Class

End Module
