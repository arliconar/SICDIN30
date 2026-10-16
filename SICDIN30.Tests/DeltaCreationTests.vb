Imports System
Imports System.IO
Imports System.Threading
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports SICDIN30

<TestClass, DoNotParallelize>
Public Class DeltaCreationTests

    Private originalApodo As String
    Private originalCloudPath As String

    <TestInitialize>
    Public Sub Setup()
        originalCloudPath = variables.cloudpath
        If variables.tienda IsNot Nothing Then
            originalApodo = variables.tienda.apodo
        Else
            variables.tienda = New Estructuras.lugar()
            originalApodo = "local"
        End If

        ' Usar una ruta temporal segura para las pruebas
        variables.cloudpath = "C:\sc3\test_cloud"
        If Not Directory.Exists(nube.GetCloudDeltasPath()) Then
            Directory.CreateDirectory(nube.GetCloudDeltasPath())
        End If
        If Not Directory.Exists(nube.GetLocalOutboxPath()) Then
            Directory.CreateDirectory(nube.GetLocalOutboxPath())
        End If
    End Sub

    <TestCleanup>
    Public Sub Cleanup()
        variables.cloudpath = originalCloudPath
        If variables.tienda IsNot Nothing Then
            variables.tienda.apodo = originalApodo
        End If

        Try
            For Each f In Directory.GetFiles(nube.GetLocalOutboxPath(), "test_*.json")
                File.Delete(f)
            Next
            For Each f In Directory.GetFiles(nube.GetCloudDeltasPath(), "test_*.json")
                File.Delete(f)
            Next
        Catch
        End Try
    End Sub

    Private Function FindDeltaFile(pattern As String, Optional maxWaitMs As Integer = 2000) As String
        Dim sw = Stopwatch.StartNew()
        While sw.ElapsedMilliseconds < maxWaitMs
            Dim outFiles = Directory.GetFiles(nube.GetLocalOutboxPath(), pattern)
            If outFiles.Length > 0 AndAlso File.Exists(outFiles(0)) Then
                Try
                    Dim content = File.ReadAllText(outFiles(0))
                    If Not String.IsNullOrEmpty(content) Then Return outFiles(0)
                Catch
                End Try
            End If

            Dim cloudFiles = Directory.GetFiles(nube.GetCloudDeltasPath(), pattern)
            If cloudFiles.Length > 0 AndAlso File.Exists(cloudFiles(0)) Then
                Try
                    Dim content = File.ReadAllText(cloudFiles(0))
                    If Not String.IsNullOrEmpty(content) Then Return cloudFiles(0)
                Catch
                End Try
            End If

            Thread.Sleep(50)
        End While
        Return Nothing
    End Function

    <TestMethod>
    Public Sub Test_QueueArticuloDelta_CreatesValidJsonFile()
        variables.tienda.apodo = "test_artupsert"

        Dim art As New Estructuras.articuloventa()
        art.clave = "TEST_CLV_001"
        art.descripcion = "Articulo Test Creacion Delta"
        art.menudeo = 125.5D
        art.mayoreo = 99.0D
        art.precio = 75.0D
        art.pq = "PZA"
        art.cbarra = "7501234567890"
        art.tipo = "Articulos"
        art.fum = DateTime.Now

        nube.QueueArticuloDelta(art, "UPSERT")

        Dim targetFile As String = FindDeltaFile("test_artupsert_ART_*.json")
        Assert.IsNotNull(targetFile, "No se encontró ningún archivo delta generado para el artículo.")

        Dim jsonContent As String = File.ReadAllText(targetFile)
        Dim packet As JObject = JObject.Parse(jsonContent)

        Assert.AreEqual("test_artupsert", packet("origin").ToString())
        Assert.AreEqual("ARTICULO", packet("entity").ToString())
        Assert.AreEqual("UPSERT", packet("action").ToString())

        Dim data As JObject = CType(packet("data"), JObject)
        Assert.AreEqual("TEST_CLV_001", data("clave").ToString())
        Assert.AreEqual("Articulo Test Creacion Delta", data("descripcion").ToString())
        Assert.AreEqual(125.5D, Convert.ToDecimal(data("menudeo")))
        Assert.IsFalse(Convert.ToBoolean(data("del")))
    End Sub

    <TestMethod>
    Public Sub Test_QueueArticuloDelta_DeleteAction_SetsDelFlagTrue()
        variables.tienda.apodo = "test_artdel"

        Dim art As New Estructuras.articuloventa()
        art.clave = "TEST_CLV_DEL"
        art.descripcion = "Articulo para eliminar"

        nube.QueueArticuloDelta(art, "DELETE")

        Dim targetFile As String = FindDeltaFile("test_artdel_ART_*.json")
        Assert.IsNotNull(targetFile, "No se encontró el archivo delta de baja.")

        Dim packet As JObject = JObject.Parse(File.ReadAllText(targetFile))

        Assert.AreEqual("DELETE", packet("action").ToString())
        Dim data As JObject = CType(packet("data"), JObject)
        Assert.IsTrue(Convert.ToBoolean(data("del")))
        Assert.IsFalse(String.IsNullOrEmpty(data("fechadel").ToString()))
    End Sub

    <TestMethod>
    Public Sub Test_QueueClienteDelta_CreatesValidJsonFile()
        variables.tienda.apodo = "test_clnt"

        Dim clnt As New Cliente()
        clnt.id = 501
        clnt.Nombre = "Roberto Gomez Prueba"
        clnt.Direccion = "Av. Insurgentes 45"
        clnt.RFC = "GORB850101"
        clnt.telcel = "7715551234"
        clnt.mail = "roberto@test.com"

        nube.QueueClienteDelta(clnt, "UPSERT")

        Dim targetFile As String = FindDeltaFile("test_clnt_CLN_*.json")
        Assert.IsNotNull(targetFile, "No se encontró el archivo delta generado para el cliente.")

        Dim packet As JObject = JObject.Parse(File.ReadAllText(targetFile))

        Assert.AreEqual("test_clnt", packet("origin").ToString())
        Assert.AreEqual("CLIENTE", packet("entity").ToString())
        Assert.AreEqual("UPSERT", packet("action").ToString())

        Dim data As JObject = CType(packet("data"), JObject)
        Assert.AreEqual("Roberto Gomez Prueba", data("nombre").ToString())
        Assert.AreEqual("GORB850101", data("rfc").ToString())
        Assert.AreEqual("7715551234", data("telcel").ToString())
    End Sub

    <TestMethod>
    Public Sub Test_PushOutbox_TransfersFilesFromOutboxToCloud()
        Dim testFileName As String = "test_push_" & Guid.NewGuid().ToString("N") & ".json"
        Dim localFile As String = Path.Combine(nube.GetLocalOutboxPath(), testFileName)
        Dim testJson As String = "{""test"": true, ""origin"": ""test_push""}"
        File.WriteAllText(localFile, testJson, System.Text.Encoding.UTF8)

        Assert.IsTrue(File.Exists(localFile), "El archivo local de outbox debe existir antes de empujar.")

        nube.PushOutbox()

        Dim destStores = nube.GetDestinationStoreApodos()
        Dim foundInCloud As Boolean = False
        If destStores.Count > 0 Then
            For Each dest In destStores
                Dim inboxFile As String = Path.Combine(nube.GetStoreInboxPath(dest), testFileName)
                If File.Exists(inboxFile) Then
                    foundInCloud = True
                    Try
                        File.Delete(inboxFile)
                    Catch
                    End Try
                End If
            Next
        Else
            Dim cloudFile As String = Path.Combine(nube.GetCloudDeltasPath(), testFileName)
            If File.Exists(cloudFile) Then
                foundInCloud = True
                Try
                    File.Delete(cloudFile)
                Catch
                End Try
            End If
        End If

        Assert.IsTrue(foundInCloud, "El archivo delta debe haber sido transferido a los buzones de destino en la nube.")
        Assert.IsFalse(File.Exists(localFile), "El archivo delta debe haberse eliminado del outbox local tras la transferencia exitosa.")
    End Sub

End Class
