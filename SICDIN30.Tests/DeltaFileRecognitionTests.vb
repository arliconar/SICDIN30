Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Data.OleDb
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Newtonsoft.Json
Imports SICDIN30

<TestClass, DoNotParallelize>
Public Class DeltaFileRecognitionTests

    Private originalApodo As String
    Private originalCloudPath As String
    Private testCloudDir As String
    Private testProcessedLog As String

    <TestInitialize>
    Public Sub Setup()
        originalCloudPath = variables.cloudpath
        If variables.tienda IsNot Nothing Then
            originalApodo = variables.tienda.apodo
        Else
            variables.tienda = New Estructuras.lugar()
            originalApodo = "local"
        End If

        testCloudDir = "C:\sc3\test_cloud"
        variables.cloudpath = testCloudDir
        variables.tienda.apodo = "tienda_al"

        If Not Directory.Exists(nube.GetCloudDeltasPath()) Then
            Directory.CreateDirectory(nube.GetCloudDeltasPath())
        End If
        If Not Directory.Exists(nube.GetLocalOutboxPath()) Then
            Directory.CreateDirectory(nube.GetLocalOutboxPath())
        End If

        testProcessedLog = nube.GetProcessedLogPath()
    End Sub

    <TestCleanup>
    Public Sub Cleanup()
        variables.cloudpath = originalCloudPath
        If variables.tienda IsNot Nothing Then
            variables.tienda.apodo = originalApodo
        End If

        Try
            For Each f In Directory.GetFiles(nube.GetCloudDeltasPath(), "*testrecog*.json")
                File.Delete(f)
            Next
            For Each f In Directory.GetFiles(nube.GetCloudDeltasPath(), "*ownstore*.json")
                File.Delete(f)
            Next
            For Each f In Directory.GetFiles(nube.GetCloudDeltasPath(), "*dup*.json")
                File.Delete(f)
            Next
            For Each f In Directory.GetFiles(nube.GetCloudDeltasPath(), "*batchrecog*.json")
                File.Delete(f)
            Next
        Catch
        End Try
    End Sub

    <TestMethod>
    Public Sub Test_LectorBaseCloud_RecognizesAndProcessesRemoteDeltas()
        ' 1. Arrange: Crear un delta remoto emitido por la tienda "cl" (Casa Licona)
        Dim remoteFilename As String = "cl_ART_testrecog1_" & Guid.NewGuid().ToString("N").Substring(0, 6) & ".json"
        Dim remotePath As String = Path.Combine(nube.GetCloudDeltasPath(), remoteFilename)

        Dim packet As New Dictionary(Of String, Object)()
        packet("version") = 1
        packet("id") = Guid.NewGuid().ToString("N")
        packet("origin") = "cl"
        packet("timestamp") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        packet("entity") = "ARTICULO"
        packet("action") = "UPSERT"

        Dim data As New Dictionary(Of String, Object)()
        data("clave") = "ART_RECOG_001"
        data("descripcion") = "Articulo Reconocido Remoto"
        data("menudeo") = 150.0D
        data("mayoreo") = 120.0D
        data("fum") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        packet("data") = data

        File.WriteAllText(remotePath, JsonConvert.SerializeObject(packet), System.Text.Encoding.UTF8)
        Assert.IsTrue(File.Exists(remotePath), "El archivo delta remoto debe existir en la nube.")

        ' 2. Act: Ejecutar lectorbasecloud desde la tienda "al"
        nube.lectorbasecloud()

        ' 3. Assert: Debe reconocer el archivo de "cl" y registrarlo como procesado
        Dim processedLines As String() = If(File.Exists(testProcessedLog), File.ReadAllLines(testProcessedLog), Array.Empty(Of String)())
        Dim fueProcesado As Boolean = False
        For Each line In processedLines
            If line.Trim().Equals(remoteFilename, StringComparison.OrdinalIgnoreCase) Then
                fueProcesado = True
                Exit For
            End If
        Next

        Assert.IsTrue(fueProcesado, "El archivo delta remoto " & remoteFilename & " debió haber sido reconocido y registrado en sync_processed.txt.")

        ' Limpiar registro de prueba en base de datos
        Try
            Dim cs As String = nube.GetOleDbConnectionString("C:\sc3\art.dat", "sganeo4")
            Using conn As New OleDbConnection(cs)
                conn.Open()
                Using cmd As New OleDbCommand("DELETE FROM Articulos WHERE Clave = 'ART_RECOG_001'", conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch
        End Try
    End Sub

    <TestMethod>
    Public Sub Test_LectorBaseCloud_IgnoresOwnStoreDeltas()
        ' 1. Arrange: Crear un delta cuyo nombre comienza con el prefijo de la tienda local (tienda_al)
        Dim ownFilename As String = "tienda_al_ART_ownstore_" & Guid.NewGuid().ToString("N").Substring(0, 6) & ".json"
        Dim ownPath As String = Path.Combine(nube.GetCloudDeltasPath(), ownFilename)

        Dim packet As New Dictionary(Of String, Object)()
        packet("version") = 1
        packet("id") = Guid.NewGuid().ToString("N")
        packet("origin") = "tienda_al"
        packet("timestamp") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        packet("entity") = "ARTICULO"
        packet("action") = "UPSERT"
        Dim data As New Dictionary(Of String, Object)()
        data("clave") = "ART_OWN_001"
        data("descripcion") = "Articulo Propio"
        packet("data") = data

        File.WriteAllText(ownPath, JsonConvert.SerializeObject(packet), System.Text.Encoding.UTF8)

        ' 2. Act: Ejecutar sincronización
        nube.lectorbasecloud()

        ' 3. Assert: NO debe procesar ni registrar en sync_processed.txt archivos de su propia tienda
        Dim processedLines As String() = If(File.Exists(testProcessedLog), File.ReadAllLines(testProcessedLog), Array.Empty(Of String)())
        Dim fueProcesado As Boolean = False
        For Each line In processedLines
            If line.Trim().Equals(ownFilename, StringComparison.OrdinalIgnoreCase) Then
                fueProcesado = True
                Exit For
            End If
        Next

        Assert.IsFalse(fueProcesado, "El sincronizador NO debe procesar archivos de su propia tienda (debe ignorarlos para evitar bucle de eco).")
    End Sub

    <TestMethod>
    Public Sub Test_LectorBaseCloud_SkipsAlreadyProcessedFiles()
        ' 1. Arrange: Crear archivo delta y marcarlo como previamente procesado
        Dim dupFilename As String = "cl_ART_dup_" & Guid.NewGuid().ToString("N").Substring(0, 6) & ".json"
        Dim dupPath As String = Path.Combine(nube.GetCloudDeltasPath(), dupFilename)

        Dim packet As New Dictionary(Of String, Object)()
        packet("version") = 1
        packet("id") = Guid.NewGuid().ToString("N")
        packet("origin") = "cl"
        packet("entity") = "ARTICULO"
        packet("action") = "UPSERT"
        Dim data As New Dictionary(Of String, Object)()
        data("clave") = "ART_DUP_001"
        data("descripcion") = "Articulo Duplicado"
        packet("data") = data

        File.WriteAllText(dupPath, JsonConvert.SerializeObject(packet), System.Text.Encoding.UTF8)

        ' Pre-registrarlo en processed
        File.AppendAllText(testProcessedLog, dupFilename & Environment.NewLine, System.Text.Encoding.UTF8)
        Dim countBefore As Integer = File.ReadAllLines(testProcessedLog).Length

        ' 2. Act: Ejecutar lectorbasecloud
        nube.lectorbasecloud()

        ' 3. Assert: No debe volver a procesarlo ni duplicar la entrada en sync_processed.txt
        Dim countAfter As Integer = File.ReadAllLines(testProcessedLog).Length
        Assert.AreEqual(countBefore, countAfter, "No debe registrarse dos veces el mismo archivo delta ya procesado.")
    End Sub

    <TestMethod>
    Public Sub Test_LectorBaseCloud_RecognizesBatchPacket()
        ' 1. Arrange: Crear un paquete por lotes (ARTICULO_BATCH)
        Dim batchFilename As String = "cl_BATCH_batchrecog_" & Guid.NewGuid().ToString("N").Substring(0, 6) & ".json"
        Dim batchPath As String = Path.Combine(nube.GetCloudDeltasPath(), batchFilename)

        Dim items As New List(Of Dictionary(Of String, Object))()
        Dim item1 As New Dictionary(Of String, Object)()
        item1("clave") = "ART_BATCH_001"
        item1("descripcion") = "Articulo Lote 1"
        item1("menudeo") = 50.0D
        item1("fum") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        items.Add(item1)

        Dim packet As New Dictionary(Of String, Object)()
        packet("version") = 1
        packet("id") = Guid.NewGuid().ToString("N")
        packet("origin") = "cl"
        packet("timestamp") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        packet("entity") = "ARTICULO_BATCH"
        packet("action") = "UPSERT"
        Dim data As New Dictionary(Of String, Object)()
        data("items") = items.ToArray()
        packet("data") = data

        File.WriteAllText(batchPath, JsonConvert.SerializeObject(packet), System.Text.Encoding.UTF8)

        ' 2. Act: Ejecutar sincronización
        nube.lectorbasecloud()

        ' 3. Assert: El paquete por lotes debe ser reconocido y procesado
        Dim processedLines As String() = If(File.Exists(testProcessedLog), File.ReadAllLines(testProcessedLog), Array.Empty(Of String)())
        Dim fueProcesado As Boolean = False
        For Each line In processedLines
            If line.Trim().Equals(batchFilename, StringComparison.OrdinalIgnoreCase) Then
                fueProcesado = True
                Exit For
            End If
        Next

        Assert.IsTrue(fueProcesado, "El paquete de lote ARTICULO_BATCH debió haber sido reconocido y marcado como procesado.")

        ' Limpiar base
        Try
            Dim cs As String = nube.GetOleDbConnectionString("C:\sc3\art.dat", "sganeo4")
            Using conn As New OleDbConnection(cs)
                conn.Open()
                Using cmd As New OleDbCommand("DELETE FROM Articulos WHERE Clave = 'ART_BATCH_001'", conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch
        End Try
    End Sub

    <TestMethod>
    Public Sub Test_LectorBaseCloud_ConsumesAndDeletesFromPersonalInbox()
        ' 1. Arrange: Simular la llegada de un delta en el buzón personal de la tienda activa (tienda_al)
        Dim myInbox As String = nube.GetMyInboxPath()
        Dim remoteFilename As String = "cl_ART_inbox_" & Guid.NewGuid().ToString("N").Substring(0, 6) & ".json"
        Dim inboxFilePath As String = Path.Combine(myInbox, remoteFilename)

        Dim packet As New Dictionary(Of String, Object)()
        packet("version") = 1
        packet("id") = Guid.NewGuid().ToString("N")
        packet("origin") = "cl"
        packet("timestamp") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        packet("entity") = "ARTICULO"
        packet("action") = "UPSERT"

        Dim data As New Dictionary(Of String, Object)()
        data("clave") = "ART_INBOX_TEST_001"
        data("descripcion") = "Articulo en Buzon"
        data("menudeo") = 250.0D
        data("fum") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        packet("data") = data

        File.WriteAllText(inboxFilePath, JsonConvert.SerializeObject(packet), System.Text.Encoding.UTF8)
        Assert.IsTrue(File.Exists(inboxFilePath), "El archivo delta debe existir en el buzón personal antes del procesamiento.")

        ' 2. Act: Ejecutar lectorbasecloud
        nube.lectorbasecloud()

        ' 3. Assert: El archivo DEBE haber sido eliminado del buzón tras ser procesado
        Assert.IsFalse(File.Exists(inboxFilePath), "El archivo delta en el buzón personal debe haber sido eliminado automáticamente tras ser procesado.")

        ' Verificar que sí fue registrado como procesado
        Dim processedLines As String() = If(File.Exists(testProcessedLog), File.ReadAllLines(testProcessedLog), Array.Empty(Of String)())
        Dim fueProcesado As Boolean = False
        For Each line In processedLines
            If line.Trim().Equals(remoteFilename, StringComparison.OrdinalIgnoreCase) Then
                fueProcesado = True
                Exit For
            End If
        Next
        Assert.IsTrue(fueProcesado, "El archivo procesado desde el buzón debe figurar en sync_processed.txt.")

        ' Limpiar base
        Try
            Dim cs As String = nube.GetOleDbConnectionString("C:\sc3\art.dat", "sganeo4")
            Using conn As New OleDbConnection(cs)
                conn.Open()
                Using cmd As New OleDbCommand("DELETE FROM Articulos WHERE Clave = 'ART_INBOX_TEST_001'", conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch
        End Try
    End Sub

End Class
