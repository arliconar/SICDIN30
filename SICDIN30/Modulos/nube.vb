Imports System
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Data.OleDb
Imports System.Web.Script.Serialization
Imports System.Collections.Generic

Module nube

    Private ReadOnly syncMutex As New Object()
    Private ReadOnly logMutex As New Object()

    ''' <summary>
    ''' Registra un mensaje en C:\sc3\sync.log para trazabilidad y diagnóstico.
    ''' </summary>
    Public Sub SyncLog(message As String, Optional isError As Boolean = False)
        Try
            SyncLock logMutex
                Dim logDir As String = "C:\sc3"
                If Not Directory.Exists(logDir) Then Directory.CreateDirectory(logDir)
                Dim logFile As String = Path.Combine(logDir, "sync.log")

                ' Rotación básica de log si supera 5 MB
                If File.Exists(logFile) Then
                    Dim fi As New FileInfo(logFile)
                    If fi.Length > 5 * 1024 * 1024 Then
                        Dim bakFile As String = Path.Combine(logDir, "sync.log.bak")
                        If File.Exists(bakFile) Then File.Delete(bakFile)
                        File.Move(logFile, bakFile)
                    End If
                End If

                Dim prefix As String = If(isError, "[ERROR] ", "[INFO]  ")
                Dim line As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " " & prefix & message & Environment.NewLine
                File.AppendAllText(logFile, line, Encoding.UTF8)
            End SyncLock
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene la cadena de conexión OLEDB intentando ACE 16.0 o retrocediendo a 12.0 si no está instalado.
    ''' </summary>
    Public Function GetOleDbConnectionString(dbPath As String, password As String) As String
        Dim providers As String() = {"Microsoft.ACE.OLEDB.16.0", "Microsoft.ACE.OLEDB.12.0"}
        For Each prov In providers
            Dim cs As String = "Provider=" & prov & ";Data Source=" & dbPath & ";Persist Security Info=True;Jet OLEDB:Database Password=" & password
            Try
                Using testConn As New OleDbConnection(cs)
                    testConn.Open()
                    Return cs
                End Using
            Catch
            End Try
        Next
        ' Por defecto usamos 16.0
        Return "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" & dbPath & ";Persist Security Info=True;Jet OLEDB:Database Password=" & password
    End Function

    ''' <summary>
    ''' Ruta de la carpeta compartida en la nube para intercambio de deltas JSON.
    ''' </summary>
    Public Function GetCloudDeltasPath() As String
        Dim baseCloud As String = cloudpath
        If String.IsNullOrWhiteSpace(baseCloud) Then
            baseCloud = "C:\sc3\sync_deltas"
        Else
            baseCloud = Path.Combine(baseCloud, "sync_deltas")
        End If

        Try
            If Not Directory.Exists(baseCloud) Then
                Directory.CreateDirectory(baseCloud)
            End If
        Catch ex As Exception
            SyncLog("No se pudo crear o acceder a la carpeta de nube: " & baseCloud & " (" & ex.Message & ")", True)
        End Try

        Return baseCloud
    End Function

    ''' <summary>
    ''' Carpeta local temporal de salida (outbox) para almacenar deltas antes de enviarlos a la nube.
    ''' </summary>
    Public Function GetLocalOutboxPath() As String
        Dim p As String = "C:\sc3\sync_outbox"
        If Not Directory.Exists(p) Then Directory.CreateDirectory(p)
        Return p
    End Function

    ''' <summary>
    ''' Archivo donde se registran los nombres de archivos delta ya procesados por este equipo.
    ''' </summary>
    Public Function GetProcessedLogPath() As String
        Dim p As String = "C:\sc3\sync_processed.txt"
        Return p
    End Function

    ''' <summary>
    ''' Copia de seguridad de bases locales en la ruta de respaldo (al cerrar la app).
    ''' </summary>
    Public Sub backinup()
        Try
            loadscreen.Close()
            loadscreen.loader(6, 1)

            Dim targetDir As String = pathbackup
            If Not String.IsNullOrWhiteSpace(targetDir) AndAlso tienda IsNot Nothing AndAlso Not String.IsNullOrEmpty(tienda.apodo) Then
                targetDir = Path.Combine(targetDir, tienda.apodo)
            Else
                targetDir = "C:\sc3\backup"
            End If

            If Not Directory.Exists(targetDir) Then Directory.CreateDirectory(targetDir)

            loadscreen.stepp()
            loadscreen.loadtext.Text = "Respaldando base de artículos"
            conart.Close()
            Try
                File.Copy("C:\sc3\art.dat", Path.Combine(targetDir, "art.dat"), True)
            Catch ex As Exception
                SyncLog("Error al respaldar art.dat: " & ex.Message, True)
            End Try

            loadscreen.stepp()
            loadscreen.loadtext.Text = "Respaldando base de clientes"
            conclnt.Close()
            Try
                File.Copy("C:\sc3\clnt.dat", Path.Combine(targetDir, "clnt.dat"), True)
            Catch ex As Exception
                SyncLog("Error al respaldar clnt.dat: " & ex.Message, True)
            End Try

            loadscreen.stepp()
            loadscreen.loadtext.Text = "Respaldando base de ventas"
            convent.Close()
            Try
                File.Copy("C:\sc3\venta.dat", Path.Combine(targetDir, "venta.dat"), True)
            Catch ex As Exception
                SyncLog("Error al respaldar venta.dat: " & ex.Message, True)
            End Try

            loadscreen.stepp()
            loadscreen.loadtext.Text = "Respaldando base de reparaciones"
            conbicrep.Close()
            Try
                File.Copy("C:\sc3\bicirep.dat", Path.Combine(targetDir, "bicirep.dat"), True)
            Catch ex As Exception
                SyncLog("Error al respaldar bicirep.dat: " & ex.Message, True)
            End Try

            loadscreen.stepp()
            loadscreen.loadtext.Text = "Respaldando base de trabajadores"
            contra.Close()
            Try
                File.Copy("C:\sc3\trabajadores.dat", Path.Combine(targetDir, "trabajadores.dat"), True)
            Catch ex As Exception
                SyncLog("Error al respaldar trabajadores.dat: " & ex.Message, True)
            End Try

            loadscreen.stepp()
            loadscreen.loadtext.Text = "Respaldando base de usuarios"
            conusu.Close()
            Try
                File.Copy("C:\sc3\usu.dat", Path.Combine(targetDir, "usu.dat"), True)
            Catch ex As Exception
                SyncLog("Error al respaldar usu.dat: " & ex.Message, True)
            End Try

        Catch ex As Exception
            SyncLog("Error general en backinup: " & ex.Message, True)
        Finally
            End
        End Try
    End Sub

    ''' <summary>
    ''' Encola un cambio de artículo (UPSERT o DELETE) para ser enviado a la nube.
    ''' </summary>
    Public Sub QueueArticuloDelta(art As articuloventa, action As String)
        Try
            If art Is Nothing OrElse String.IsNullOrEmpty(art.clave) Then Exit Sub
            Dim apodoStore As String = If(tienda IsNot Nothing AndAlso Not String.IsNullOrEmpty(tienda.apodo), tienda.apodo, "local")

            Dim data As New Dictionary(Of String, Object)()
            data("clave") = art.clave
            data("descripcion") = art.descripcion
            data("menudeo") = art.menudeo
            data("mayoreo") = art.mayoreo
            data("pme") = art.pme
            data("especial") = art.especial
            data("precio") = art.precio
            data("pgmen") = art.pgmen
            data("pgmay") = art.pgmay
            data("paquete") = If(String.IsNullOrEmpty(art.pq), "PZA", art.pq)
            data("cbarra") = If(art.cbarra, "")
            data("tipo") = If(String.IsNullOrEmpty(art.tipo), "Articulos", art.tipo)
            data("einf") = art.einf
            data("notas") = If(art.notas, "")
            data("tags") = If(art.tag, "")
            data("fum") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            data("usuariofum") = If(user IsNot Nothing, user.idnameusu, "")
            data("del") = (action = "DELETE")
            data("fechadel") = If(action = "DELETE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "")

            Dim packet As New Dictionary(Of String, Object)()
            Dim packetId As String = Guid.NewGuid().ToString("N")
            packet("version") = 1
            packet("id") = packetId
            packet("origin") = apodoStore
            packet("timestamp") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            packet("entity") = "ARTICULO"
            packet("action") = action
            packet("data") = data

            Dim serializer As New JavaScriptSerializer()
            Dim json As String = serializer.Serialize(packet)

            Dim filename As String = apodoStore & "_ART_" & DateTime.Now.ToString("yyyyMMdd_HHmmssfff") & "_" & packetId.Substring(0, 6) & ".json"
            Dim outboxFile As String = Path.Combine(GetLocalOutboxPath(), filename)
            File.WriteAllText(outboxFile, json, Encoding.UTF8)

            SyncLog("Encolado delta de artículo: " & art.clave & " [" & action & "]")

            ' Lanzar intento de empuje a la nube en segundo plano
            Dim tPush As New Thread(AddressOf PushOutbox)
            tPush.IsBackground = True
            tPush.Start()

        Catch ex As Exception
            SyncLog("Error al encolar delta de artículo: " & ex.Message, True)
        End Try
    End Sub

    ''' <summary>
    ''' Encola un cambio de cliente (UPSERT o DELETE) para ser enviado a la nube.
    ''' </summary>
    Public Sub QueueClienteDelta(clnt As Cliente, action As String)
        Try
            If clnt Is Nothing OrElse String.IsNullOrEmpty(clnt.Nombre) Then Exit Sub
            Dim apodoStore As String = If(tienda IsNot Nothing AndAlso Not String.IsNullOrEmpty(tienda.apodo), tienda.apodo, "local")

            Dim data As New Dictionary(Of String, Object)()
            data("remoteId") = clnt.id
            data("nombre") = clnt.Nombre
            data("direccion") = If(clnt.Direccion, "")
            data("mayoreo") = clnt.mayoreo
            data("especial") = clnt.especial
            data("adeudo") = clnt.adeudo
            data("credito") = clnt.credito
            data("dc") = clnt.dc
            data("rfc") = If(clnt.RFC, "")
            data("telcasa") = If(clnt.telcasa, "")
            data("telcel") = If(clnt.telcel, "")
            data("mail") = If(clnt.mail, "")
            data("ultmod") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            data("ultcompra") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

            Dim packet As New Dictionary(Of String, Object)()
            Dim packetId As String = Guid.NewGuid().ToString("N")
            packet("version") = 1
            packet("id") = packetId
            packet("origin") = apodoStore
            packet("timestamp") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            packet("entity") = "CLIENTE"
            packet("action") = action
            packet("data") = data

            Dim serializer As New JavaScriptSerializer()
            Dim json As String = serializer.Serialize(packet)

            Dim filename As String = apodoStore & "_CLN_" & DateTime.Now.ToString("yyyyMMdd_HHmmssfff") & "_" & packetId.Substring(0, 6) & ".json"
            Dim outboxFile As String = Path.Combine(GetLocalOutboxPath(), filename)
            File.WriteAllText(outboxFile, json, Encoding.UTF8)

            SyncLog("Encolado delta de cliente: " & clnt.Nombre & " [" & action & "]")

            ' Lanzar intento de empuje a la nube en segundo plano
            Dim tPush As New Thread(AddressOf PushOutbox)
            tPush.IsBackground = True
            tPush.Start()

        Catch ex As Exception
            SyncLog("Error al encolar delta de cliente: " & ex.Message, True)
        End Try
    End Sub

    ''' <summary>
    ''' Empuja todos los archivos acumulados en el outbox local hacia la carpeta de deltas en la nube.
    ''' </summary>
    Public Sub PushOutbox()
        Try
            Dim outboxDir As String = GetLocalOutboxPath()
            Dim files As String() = Directory.GetFiles(outboxDir, "*.json")
            If files.Length = 0 Then Exit Sub

            Dim cloudDir As String = GetCloudDeltasPath()
            If Not Directory.Exists(cloudDir) Then Exit Sub

            Dim enviados As Integer = 0
            For Each f In files
                Try
                    Dim dest As String = Path.Combine(cloudDir, Path.GetFileName(f))
                    File.Copy(f, dest, True)
                    File.Delete(f)
                    enviados += 1
                Catch ex As Exception
                    ' Si OneDrive tiene bloqueado o sin red, quedará en outbox para el siguiente ciclo
                End Try
            Next

            If enviados > 0 Then
                SyncLog("Enviados con éxito " & enviados & " paquetes delta a la nube.")
            End If
        Catch ex As Exception
            SyncLog("Error en PushOutbox: " & ex.Message, True)
        End Try
    End Sub

    ''' <summary>
    ''' Lee los deltas generados por otras sucursales desde la carpeta de la nube y los aplica localmente.
    ''' </summary>
    Public Sub lectorbasecloud(Optional fecha As Date = datedefault)
        If lectorbasebusy Then Exit Sub

        SyncLock syncMutex
            If lectorbasebusy Then Exit Sub
            lectorbasebusy = True
            blockexit = True
        End SyncLock

        Try
            ' 1. Intentar vaciar el outbox local por si hay paquetes pendientes
            PushOutbox()

            Dim cloudDir As String = GetCloudDeltasPath()
            If Not Directory.Exists(cloudDir) Then
                SyncLog("Carpeta de nube no disponible o inaccesible: " & cloudDir, True)
                Exit Sub
            End If

            ' 2. Cargar historial de deltas ya procesados
            Dim logProcessed As String = GetProcessedLogPath()
            Dim processedSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            If File.Exists(logProcessed) Then
                Dim lines As String() = File.ReadAllLines(logProcessed, Encoding.UTF8)
                For Each line In lines
                    Dim trimmed = line.Trim()
                    If Not String.IsNullOrEmpty(trimmed) Then
                        processedSet.Add(trimmed)
                    End If
                Next
            End If

            ' 3. Buscar archivos JSON en la nube
            Dim deltaFiles As String() = Directory.GetFiles(cloudDir, "*.json")
            If deltaFiles.Length = 0 Then Exit Sub

            ' Ordenar por nombre (que incluye timestamp)
            Array.Sort(deltaFiles)

            Dim myApodo As String = If(tienda IsNot Nothing AndAlso Not String.IsNullOrEmpty(tienda.apodo), tienda.apodo.ToLower(), "local")
            Dim serializer As New JavaScriptSerializer()

            Dim nArtUpdated As Integer = 0
            Dim nClntUpdated As Integer = 0
            Dim processedNew As New List(Of String)()

            For Each archivo In deltaFiles
                Dim fname As String = Path.GetFileName(archivo)

                ' Ignorar si es de nuestra propia tienda para evitar bucles
                If fname.ToLower().StartsWith(myApodo & "_") Then
                    Continue For
                End If

                ' Ignorar si ya fue procesado con anterioridad
                If processedSet.Contains(fname) Then
                    Continue For
                End If

                Try
                    Dim json As String = File.ReadAllText(archivo, Encoding.UTF8)
                    Dim packet As Dictionary(Of String, Object) = serializer.Deserialize(Of Dictionary(Of String, Object))(json)

                    If packet IsNot Nothing AndAlso packet.ContainsKey("entity") Then
                        Dim entity As String = packet("entity").ToString().ToUpper()
                        Dim action As String = If(packet.ContainsKey("action"), packet("action").ToString().ToUpper(), "UPSERT")
                        Dim origin As String = If(packet.ContainsKey("origin"), packet("origin").ToString(), "")

                        If entity = "ARTICULO" Then
                            If packet.ContainsKey("data") Then
                                Dim data As Dictionary(Of String, Object) = TryCast(packet("data"), Dictionary(Of String, Object))
                                If data IsNot Nothing Then
                                    ApplyArticuloDelta(data, action)
                                    nArtUpdated += 1
                                End If
                            End If

                        ElseIf entity = "ARTICULO_BATCH" Then
                            If packet.ContainsKey("data") Then
                                Dim dataBatch As Dictionary(Of String, Object) = TryCast(packet("data"), Dictionary(Of String, Object))
                                If dataBatch IsNot Nothing AndAlso dataBatch.ContainsKey("items") Then
                                    Dim items = TryCast(dataBatch("items"), Object())
                                    If items IsNot Nothing Then
                                        For Each item In items
                                            Dim itemDict = TryCast(item, Dictionary(Of String, Object))
                                            If itemDict IsNot Nothing Then
                                                ApplyArticuloDelta(itemDict, "UPSERT")
                                                nArtUpdated += 1
                                            End If
                                        Next
                                    End If
                                End If
                            End If

                        ElseIf entity = "CLIENTE" Then
                            If packet.ContainsKey("data") Then
                                Dim data As Dictionary(Of String, Object) = TryCast(packet("data"), Dictionary(Of String, Object))
                                If data IsNot Nothing Then
                                    ApplyClienteDelta(data, action)
                                    nClntUpdated += 1
                                End If
                            End If
                        End If

                        ' Registrar delta como procesado
                        processedSet.Add(fname)
                        processedNew.Add(fname)
                        SyncLog("Procesado paquete " & fname & " (Origen: " & origin & ", Entidad: " & entity & ")")
                    End If

                Catch exFile As Exception
                    SyncLog("Error al procesar delta " & fname & ": " & exFile.Message, True)
                End Try
            Next

            ' Guardar deltas procesados en el archivo local
            If processedNew.Count > 0 Then
                File.AppendAllLines(logProcessed, processedNew, Encoding.UTF8)
                SyncLog("Sincronización completada. Artículos actualizados: " & nArtUpdated & ", Clientes: " & nClntUpdated)
            End If

            If nClntUpdated > 0 Then
                Try
                    autocompletarclnt()
                Catch
                End Try
            End If

        Catch ex As Exception
            SyncLog("Excepción en lectorbasecloud: " & ex.Message & vbCrLf & ex.StackTrace, True)
        Finally
            lectorbasebusy = False
            blockexit = False
        End Try
    End Sub

    ''' <summary>
    ''' Aplica un cambio de artículo recibido en la base de datos local (art.dat) mediante una conexión aislada.
    ''' </summary>
    Private Sub ApplyArticuloDelta(data As Dictionary(Of String, Object), action As String)
        Dim clave As String = GetString(data, "clave")
        If String.IsNullOrEmpty(clave) Then Exit Sub

        Dim cs As String = GetOleDbConnectionString("C:\sc3\art.dat", "sganeo4")
        Using conn As New OleDbConnection(cs)
            conn.Open()

            If action = "DELETE" Then
                Dim fdel As Date = GetDate(data, "fechadel", DateTime.Now)
                Dim sqlDel As String = "UPDATE Articulos SET Del = True, Fechadel = ? WHERE Clave = ?"
                Using cmdDel As New OleDbCommand(sqlDel, conn)
                    cmdDel.Parameters.Add("?", OleDbType.Date).Value = fdel
                    cmdDel.Parameters.Add("?", OleDbType.VarWChar).Value = clave
                    cmdDel.ExecuteNonQuery()
                End Using
                Exit Sub
            End If

            ' Para UPSERT:
            Dim exists As Boolean = False
            Dim localFum As Date = DateTime.MinValue

            Using cmdCheck As New OleDbCommand("SELECT Fum FROM Articulos WHERE Clave = ?", conn)
                cmdCheck.Parameters.Add("?", OleDbType.VarWChar).Value = clave
                Using reader = cmdCheck.ExecuteReader()
                    If reader.Read() Then
                        exists = True
                        If Not DBNull.Value.Equals(reader("Fum")) Then
                            localFum = Convert.ToDateTime(reader("Fum"))
                        End If
                    End If
                End Using
            End Using

            Dim remoteFum As Date = GetDate(data, "fum", DateTime.Now)

            If exists Then
                ' Actualizamos si el cambio remoto es igual o más reciente
                If remoteFum >= localFum Then
                    Dim sqlUpd As String = "UPDATE Articulos SET Descripción = ?, Menudeo = ?, Mayoreo = ?, PME = ?, Especial = ?, " &
                                           "Precio = ?, PGmen = ?, PGmay = ?, Paquete = ?, cbarra = ?, Tipo = ?, Einf = ?, " &
                                           "Notas = ?, Tags = ?, Fum = ?, usuariofum = ?, Del = ?, Fechadel = ? WHERE Clave = ?"
                    Using cmdUpd As New OleDbCommand(sqlUpd, conn)
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "descripcion")
                        cmdUpd.Parameters.Add("?", OleDbType.Currency).Value = GetDecimal(data, "menudeo")
                        cmdUpd.Parameters.Add("?", OleDbType.Currency).Value = GetDecimal(data, "mayoreo")
                        cmdUpd.Parameters.Add("?", OleDbType.Numeric).Value = GetDecimal(data, "pme")
                        cmdUpd.Parameters.Add("?", OleDbType.Numeric).Value = GetDecimal(data, "especial")
                        cmdUpd.Parameters.Add("?", OleDbType.Numeric).Value = GetDecimal(data, "precio")
                        cmdUpd.Parameters.Add("?", OleDbType.Numeric).Value = GetDecimal(data, "pgmen")
                        cmdUpd.Parameters.Add("?", OleDbType.Numeric).Value = GetDecimal(data, "pgmay")
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "paquete", "PZA")
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "cbarra")
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "tipo", "Articulos")
                        cmdUpd.Parameters.Add("?", OleDbType.Boolean).Value = GetBool(data, "einf")
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "notas")
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "tags")
                        cmdUpd.Parameters.Add("?", OleDbType.Date).Value = remoteFum
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "usuariofum")
                        cmdUpd.Parameters.Add("?", OleDbType.Boolean).Value = GetBool(data, "del", False)
                        Dim fdelStr As String = GetString(data, "fechadel")
                        If Not String.IsNullOrEmpty(fdelStr) Then
                            cmdUpd.Parameters.Add("?", OleDbType.Date).Value = GetDate(data, "fechadel")
                        Else
                            cmdUpd.Parameters.Add("?", OleDbType.Date).Value = DBNull.Value
                        End If
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = clave
                        cmdUpd.ExecuteNonQuery()
                    End Using
                End If
            Else
                ' INSERT nuevo artículo
                Dim sqlIns As String = "INSERT INTO Articulos (Clave, Descripción, Menudeo, Mayoreo, PME, Especial, Existencias, " &
                                       "Paquete, Precio, PGmen, PGmay, Fum, cbarra, Tipo, Einf, Notas, Tags, usuariofum, Del, Fechadel) " &
                                       "VALUES (?, ?, ?, ?, ?, ?, 0, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
                Using cmdIns As New OleDbCommand(sqlIns, conn)
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = clave
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "descripcion")
                    cmdIns.Parameters.Add("?", OleDbType.Currency).Value = GetDecimal(data, "menudeo")
                    cmdIns.Parameters.Add("?", OleDbType.Currency).Value = GetDecimal(data, "mayoreo")
                    cmdIns.Parameters.Add("?", OleDbType.Numeric).Value = GetDecimal(data, "pme")
                    cmdIns.Parameters.Add("?", OleDbType.Numeric).Value = GetDecimal(data, "especial")
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "paquete", "PZA")
                    cmdIns.Parameters.Add("?", OleDbType.Numeric).Value = GetDecimal(data, "precio")
                    cmdIns.Parameters.Add("?", OleDbType.Numeric).Value = GetDecimal(data, "pgmen")
                    cmdIns.Parameters.Add("?", OleDbType.Numeric).Value = GetDecimal(data, "pgmay")
                    cmdIns.Parameters.Add("?", OleDbType.Date).Value = remoteFum
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "cbarra")
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "tipo", "Articulos")
                    cmdIns.Parameters.Add("?", OleDbType.Boolean).Value = GetBool(data, "einf")
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "notas")
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "tags")
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "usuariofum")
                    cmdIns.Parameters.Add("?", OleDbType.Boolean).Value = GetBool(data, "del", False)
                    Dim fdelStr As String = GetString(data, "fechadel")
                    If Not String.IsNullOrEmpty(fdelStr) Then
                        cmdIns.Parameters.Add("?", OleDbType.Date).Value = GetDate(data, "fechadel")
                    Else
                        cmdIns.Parameters.Add("?", OleDbType.Date).Value = DBNull.Value
                    End If
                    cmdIns.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Aplica un cambio de cliente recibido en la base de datos local (clnt.dat) de forma segura contra colisiones de ID.
    ''' </summary>
    Private Sub ApplyClienteDelta(data As Dictionary(Of String, Object), action As String)
        Dim nombre As String = GetString(data, "nombre")
        If String.IsNullOrEmpty(nombre) Then Exit Sub

        Dim rfc As String = GetString(data, "rfc")
        Dim telcel As String = GetString(data, "telcel")

        Dim cs As String = GetOleDbConnectionString("C:\sc3\clnt.dat", "sganeo4")
        Using conn As New OleDbConnection(cs)
            conn.Open()

            If action = "DELETE" Then
                Dim sqlDel As String = "DELETE FROM client WHERE Nombre = ?"
                Using cmdDel As New OleDbCommand(sqlDel, conn)
                    cmdDel.Parameters.Add("?", OleDbType.VarWChar).Value = nombre
                    cmdDel.ExecuteNonQuery()
                End Using
                Exit Sub
            End If

            ' Buscar si existe por RFC, Telcel o Nombre
            Dim existingId As Integer = -1
            Dim localUltmod As Date = DateTime.MinValue

            Dim sqlSearch As String = "SELECT Id, ultmod FROM client WHERE Nombre = ?"
            If Not String.IsNullOrEmpty(rfc) Then
                sqlSearch = "SELECT Id, ultmod FROM client WHERE RFC = ?"
            ElseIf Not String.IsNullOrEmpty(telcel) Then
                sqlSearch = "SELECT Id, ultmod FROM client WHERE Telcel = ?"
            End If

            Using cmdSearch As New OleDbCommand(sqlSearch, conn)
                If Not String.IsNullOrEmpty(rfc) Then
                    cmdSearch.Parameters.Add("?", OleDbType.VarWChar).Value = rfc
                ElseIf Not String.IsNullOrEmpty(telcel) Then
                    cmdSearch.Parameters.Add("?", OleDbType.VarWChar).Value = telcel
                Else
                    cmdSearch.Parameters.Add("?", OleDbType.VarWChar).Value = nombre
                End If

                Using reader = cmdSearch.ExecuteReader()
                    If reader.Read() Then
                        existingId = Convert.ToInt32(reader("Id"))
                        If Not DBNull.Value.Equals(reader("ultmod")) Then
                            localUltmod = Convert.ToDateTime(reader("ultmod"))
                        End If
                    End If
                End Using
            End Using

            Dim remoteUltmod As Date = GetDate(data, "ultmod", DateTime.Now)

            If existingId > 0 Then
                If remoteUltmod >= localUltmod Then
                    Dim sqlUpd As String = "UPDATE client SET Nombre = ?, Dirección = ?, Mayoreo = ?, Especial = ?, " &
                                           "RFC = ?, Telcasa = ?, Telcel = ?, mail = ?, ultmod = ? WHERE Id = ?"
                    Using cmdUpd As New OleDbCommand(sqlUpd, conn)
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = nombre
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "direccion")
                        cmdUpd.Parameters.Add("?", OleDbType.Boolean).Value = GetBool(data, "mayoreo")
                        cmdUpd.Parameters.Add("?", OleDbType.Boolean).Value = GetBool(data, "especial")
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = rfc
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "telcasa")
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = telcel
                        cmdUpd.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "mail")
                        cmdUpd.Parameters.Add("?", OleDbType.Date).Value = remoteUltmod
                        cmdUpd.Parameters.Add("?", OleDbType.Integer).Value = existingId
                        cmdUpd.ExecuteNonQuery()
                    End Using
                End If
            Else
                ' INSERT: Asignar nuevo ID local seguro
                Dim nextId As Integer = 1
                Using cmdMax As New OleDbCommand("SELECT MAX(Id) FROM client", conn)
                    Dim maxVal = cmdMax.ExecuteScalar()
                    If maxVal IsNot Nothing AndAlso Not DBNull.Value.Equals(maxVal) Then
                        nextId = Convert.ToInt32(maxVal) + 1
                    End If
                End Using

                Dim sqlIns As String = "INSERT INTO client (Id, Nombre, Dirección, Mayoreo, Credito, Adeudo, dc, Especial, RFC, Telcasa, Telcel, mail, ultmod, ultcompra) " &
                                       "VALUES (?, ?, ?, ?, 0, 0, 0, ?, ?, ?, ?, ?, ?, ?)"
                Using cmdIns As New OleDbCommand(sqlIns, conn)
                    cmdIns.Parameters.Add("?", OleDbType.Integer).Value = nextId
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = nombre
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "direccion")
                    cmdIns.Parameters.Add("?", OleDbType.Boolean).Value = GetBool(data, "mayoreo")
                    cmdIns.Parameters.Add("?", OleDbType.Boolean).Value = GetBool(data, "especial")
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = rfc
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "telcasa")
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = telcel
                    cmdIns.Parameters.Add("?", OleDbType.VarWChar).Value = GetString(data, "mail")
                    cmdIns.Parameters.Add("?", OleDbType.Date).Value = remoteUltmod
                    cmdIns.Parameters.Add("?", OleDbType.Date).Value = GetDate(data, "ultcompra", DateTime.Now)
                    cmdIns.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Exporta todo el catálogo local de artículos en paquetes JSON a la nube para alineación masiva inicial.
    ''' </summary>
    Public Function ExportFullCatalogDeltas(Optional batchSize As Integer = 300) As Integer
        Dim totalExported As Integer = 0
        Try
            Dim apodoStore As String = If(tienda IsNot Nothing AndAlso Not String.IsNullOrEmpty(tienda.apodo), tienda.apodo, "local")
            Dim cloudDir As String = GetCloudDeltasPath()
            Dim serializer As New JavaScriptSerializer()

            Dim cs As String = GetOleDbConnectionString("C:\sc3\art.dat", "sganeo4")
            Using conn As New OleDbConnection(cs)
                conn.Open()
                Dim sql As String = "SELECT * FROM Articulos"
                Using cmd As New OleDbCommand(sql, conn)
                    Using reader = cmd.ExecuteReader()
                        Dim items As New List(Of Dictionary(Of String, Object))()
                        Dim batchIndex As Integer = 1

                        While reader.Read()
                            Dim row As New Dictionary(Of String, Object)()
                            row("clave") = If(DBNull.Value.Equals(reader("Clave")), "", reader("Clave").ToString())
                            row("descripcion") = If(DBNull.Value.Equals(reader("Descripción")), "", reader("Descripción").ToString())
                            row("menudeo") = If(DBNull.Value.Equals(reader("Menudeo")), 0, Convert.ToDecimal(reader("Menudeo")))
                            row("mayoreo") = If(DBNull.Value.Equals(reader("Mayoreo")), 0, Convert.ToDecimal(reader("Mayoreo")))
                            row("pme") = If(DBNull.Value.Equals(reader("PME")), 0, Convert.ToDecimal(reader("PME")))
                            row("especial") = If(DBNull.Value.Equals(reader("Especial")), 0, Convert.ToDecimal(reader("Especial")))
                            row("precio") = If(DBNull.Value.Equals(reader("Precio")), 0, Convert.ToDecimal(reader("Precio")))
                            row("pgmen") = If(DBNull.Value.Equals(reader("PGmen")), 0, Convert.ToDecimal(reader("PGmen")))
                            row("pgmay") = If(DBNull.Value.Equals(reader("PGmay")), 0, Convert.ToDecimal(reader("PGmay")))
                            row("paquete") = If(DBNull.Value.Equals(reader("Paquete")), "PZA", reader("Paquete").ToString())
                            row("cbarra") = If(DBNull.Value.Equals(reader("cbarra")), "", reader("cbarra").ToString())
                            row("tipo") = If(DBNull.Value.Equals(reader("Tipo")), "Articulos", reader("Tipo").ToString())
                            row("einf") = If(DBNull.Value.Equals(reader("Einf")), False, Convert.ToBoolean(reader("Einf")))
                            row("notas") = If(DBNull.Value.Equals(reader("Notas")), "", reader("Notas").ToString())
                            row("tags") = If(DBNull.Value.Equals(reader("Tags")), "", reader("Tags").ToString())
                            row("fum") = If(DBNull.Value.Equals(reader("Fum")), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToDateTime(reader("Fum")).ToString("yyyy-MM-dd HH:mm:ss"))
                            row("usuariofum") = If(DBNull.Value.Equals(reader("usuariofum")), "", reader("usuariofum").ToString())
                            row("del") = If(DBNull.Value.Equals(reader("Del")), False, Convert.ToBoolean(reader("Del")))
                            row("fechadel") = If(DBNull.Value.Equals(reader("Fechadel")), "", Convert.ToDateTime(reader("Fechadel")).ToString("yyyy-MM-dd HH:mm:ss"))

                            items.Add(row)
                            totalExported += 1

                            If items.Count >= batchSize Then
                                WriteBatchFile(apodoStore, cloudDir, serializer, items, batchIndex)
                                batchIndex += 1
                                items.Clear()
                            End If
                        End While

                        If items.Count > 0 Then
                            WriteBatchFile(apodoStore, cloudDir, serializer, items, batchIndex)
                        End If
                    End Using
                End Using
            End Using

            SyncLog("Exportación completa de catálogo finalizada: " & totalExported & " artículos exportados a la nube.")
        Catch ex As Exception
            SyncLog("Error en ExportFullCatalogDeltas: " & ex.Message, True)
        End Try

        Return totalExported
    End Function

    Private Sub WriteBatchFile(apodo As String, cloudDir As String, serializer As JavaScriptSerializer, items As List(Of Dictionary(Of String, Object)), batchIndex As Integer)
        Dim packet As New Dictionary(Of String, Object)()
        Dim packetId As String = Guid.NewGuid().ToString("N")
        packet("version") = 1
        packet("id") = packetId
        packet("origin") = apodo
        packet("timestamp") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        packet("entity") = "ARTICULO_BATCH"
        packet("action") = "UPSERT"

        Dim data As New Dictionary(Of String, Object)()
        data("items") = items.ToArray()
        packet("data") = data

        Dim json As String = serializer.Serialize(packet)
        Dim fname As String = apodo & "_BATCH_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & "_" & batchIndex.ToString("D3") & ".json"
        Dim targetPath As String = Path.Combine(cloudDir, fname)
        File.WriteAllText(targetPath, json, Encoding.UTF8)
    End Sub

    ' --- Funciones auxiliares de lectura segura de diccionarios ---
    Private Function GetString(d As Dictionary(Of String, Object), key As String, Optional def As String = "") As String
        If d Is Nothing OrElse Not d.ContainsKey(key) OrElse d(key) Is Nothing Then Return def
        Return d(key).ToString()
    End Function

    Private Function GetDecimal(d As Dictionary(Of String, Object), key As String, Optional def As Decimal = 0) As Decimal
        If d Is Nothing OrElse Not d.ContainsKey(key) OrElse d(key) Is Nothing Then Return def
        Dim v = d(key).ToString()
        Dim res As Decimal = 0
        If Decimal.TryParse(v, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, res) Then
            Return res
        ElseIf Decimal.TryParse(v, res) Then
            Return res
        End If
        Return def
    End Function

    Private Function GetDate(d As Dictionary(Of String, Object), key As String, Optional def As Date = Nothing) As Date
        If d Is Nothing OrElse Not d.ContainsKey(key) OrElse d(key) Is Nothing Then Return If(def = Nothing, DateTime.Now, def)
        Dim v = d(key).ToString()
        Dim res As Date
        If DateTime.TryParse(v, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, res) Then
            Return res
        ElseIf DateTime.TryParse(v, res) Then
            Return res
        End If
        Return If(def = Nothing, DateTime.Now, def)
    End Function

    Private Function GetBool(d As Dictionary(Of String, Object), key As String, Optional def As Boolean = False) As Boolean
        If d Is Nothing OrElse Not d.ContainsKey(key) OrElse d(key) Is Nothing Then Return def
        Dim v = d(key).ToString()
        Dim res As Boolean = False
        If Boolean.TryParse(v, res) Then Return res
        If v = "1" OrElse v.ToLower() = "true" OrElse v.ToLower() = "verdadero" Then Return True
        Return False
    End Function

End Module
