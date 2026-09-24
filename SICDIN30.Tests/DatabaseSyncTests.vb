Imports System
Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Newtonsoft.Json
Imports SICDIN30

<TestClass, DoNotParallelize>
Public Class DatabaseSyncTests

    Private originalApodo As String
    Private originalCloudPath As String
    Private artConnStr As String
    Private clntConnStr As String

    <TestInitialize>
    Public Sub Setup()
        originalCloudPath = variables.cloudpath
        If variables.tienda IsNot Nothing Then
            originalApodo = variables.tienda.apodo
        Else
            variables.tienda = New Estructuras.lugar()
            originalApodo = "local"
        End If

        variables.cloudpath = "C:\sc3\test_cloud"
        variables.tienda.apodo = "tienda_local"

        If Not Directory.Exists(nube.GetCloudDeltasPath()) Then
            Directory.CreateDirectory(nube.GetCloudDeltasPath())
        End If

        artConnStr = nube.GetOleDbConnectionString("C:\sc3\art.dat", "sganeo4")
        clntConnStr = nube.GetOleDbConnectionString("C:\sc3\clnt.dat", "sganeo4")
    End Sub

    <TestCleanup>
    Public Sub Cleanup()
        variables.cloudpath = originalCloudPath
        If variables.tienda IsNot Nothing Then
            variables.tienda.apodo = originalApodo
        End If

        Try
            For Each f In Directory.GetFiles(nube.GetCloudDeltasPath(), "*testdb*.json")
                File.Delete(f)
            Next
        Catch
        End Try
    End Sub

    Private Sub CreateRemoteDelta(filename As String, entity As String, action As String, data As Dictionary(Of String, Object))
        Dim packet As New Dictionary(Of String, Object)()
        packet("version") = 1
        packet("id") = Guid.NewGuid().ToString("N")
        packet("origin") = "tienda_remota"
        packet("timestamp") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        packet("entity") = entity
        packet("action") = action
        packet("data") = data

        Dim filePath As String = Path.Combine(nube.GetCloudDeltasPath(), filename)
        File.WriteAllText(filePath, JsonConvert.SerializeObject(packet), System.Text.Encoding.UTF8)
    End Sub

    <TestMethod>
    Public Sub Test_DatabaseSync_InsertsNewArticle_WithPricesAndMetadata()
        Dim testClave As String = "TEST_DB_INS_001"
        Dim deltaName As String = "tienda_remota_ART_testdb_ins_" & Guid.NewGuid().ToString("N").Substring(0, 6) & ".json"

        ' 1. Asegurar que no exista previamente
        Using conn As New OleDbConnection(artConnStr)
            conn.Open()
            Using cmdDel As New OleDbCommand("DELETE FROM Articulos WHERE Clave = '" & testClave & "'", conn)
                cmdDel.ExecuteNonQuery()
            End Using
        End Using

        ' 2. Crear delta remoto para este artículo
        Dim data As New Dictionary(Of String, Object)()
        data("clave") = testClave
        data("descripcion") = "Articulo Nuevo Sincronizado Test"
        data("menudeo") = 210.0D
        data("mayoreo") = 175.0D
        data("pme") = 160.0D
        data("precio") = 130.0D
        data("cbarra") = "7509876543210"
        data("paquete") = "PZA"
        data("tipo") = "Articulos"
        data("fum") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        data("del") = False

        CreateRemoteDelta(deltaName, "ARTICULO", "UPSERT", data)

        ' 3. Ejecutar sincronización
        nube.lectorbasecloud()

        ' 4. Verificar en base de datos local
        Using conn As New OleDbConnection(artConnStr)
            conn.Open()
            Using cmd As New OleDbCommand("SELECT Descripción, Menudeo, Mayoreo, Existencias, Del FROM Articulos WHERE Clave = '" & testClave & "'", conn)
                Using reader = cmd.ExecuteReader()
                    Assert.IsTrue(reader.Read(), "El artículo nuevo " & testClave & " no fue insertado en art.dat.")
                    Assert.AreEqual("Articulo Nuevo Sincronizado Test", reader("Descripción").ToString(), "La descripción no coincide.")
                    Assert.AreEqual(210.0D, Convert.ToDecimal(reader("Menudeo")), "El precio menudeo no coincide.")
                    Assert.AreEqual(175.0D, Convert.ToDecimal(reader("Mayoreo")), "El precio mayoreo no coincide.")
                    Assert.AreEqual(0, Convert.ToInt32(reader("Existencias")), "Las existencias iniciales deben ser 0 (aislamiento de inventario por tienda).")
                    Assert.IsFalse(Convert.ToBoolean(reader("Del")), "El flag Del debe ser False.")
                End Using
            End Using

            ' Limpieza
            Using cmdClean As New OleDbCommand("DELETE FROM Articulos WHERE Clave = '" & testClave & "'", conn)
                cmdClean.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    <TestMethod>
    Public Sub Test_DatabaseSync_UpdatesExistingArticle_WhenRemoteFumIsNewer()
        Dim testClave As String = "TEST_DB_UPD_001"
        Dim deltaName As String = "tienda_remota_ART_testdb_upd_" & Guid.NewGuid().ToString("N").Substring(0, 6) & ".json"

        ' 1. Insertar artículo base con FUM antigua
        Using conn As New OleDbConnection(artConnStr)
            conn.Open()
            Using cmdDel As New OleDbCommand("DELETE FROM Articulos WHERE Clave = '" & testClave & "'", conn)
                cmdDel.ExecuteNonQuery()
            End Using

            Dim sqlBase As String = "INSERT INTO Articulos (Clave, Descripción, Menudeo, Mayoreo, Fum, Del) VALUES (?, ?, ?, ?, ?, ?)"
            Using cmdBase As New OleDbCommand(sqlBase, conn)
                cmdBase.Parameters.Add("?", OleDbType.VarWChar).Value = testClave
                cmdBase.Parameters.Add("?", OleDbType.VarWChar).Value = "Descripcion Antigua"
                cmdBase.Parameters.Add("?", OleDbType.Currency).Value = 100.0D
                cmdBase.Parameters.Add("?", OleDbType.Currency).Value = 80.0D
                cmdBase.Parameters.Add("?", OleDbType.Date).Value = New DateTime(2020, 1, 1, 10, 0, 0)
                cmdBase.Parameters.Add("?", OleDbType.Boolean).Value = False
                cmdBase.ExecuteNonQuery()
            End Using
        End Using

        ' 2. Crear delta remoto con FUM más reciente
        Dim data As New Dictionary(Of String, Object)()
        data("clave") = testClave
        data("descripcion") = "Descripcion Nueva Actualizada por Nube"
        data("menudeo") = 145.0D
        data("mayoreo") = 115.0D
        data("fum") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        data("del") = False

        CreateRemoteDelta(deltaName, "ARTICULO", "UPSERT", data)

        ' 3. Ejecutar sincronización
        nube.lectorbasecloud()

        ' 4. Verificar que se actualizó
        Using conn As New OleDbConnection(artConnStr)
            conn.Open()
            Using cmd As New OleDbCommand("SELECT Descripción, Menudeo FROM Articulos WHERE Clave = '" & testClave & "'", conn)
                Using reader = cmd.ExecuteReader()
                    Assert.IsTrue(reader.Read(), "El artículo debe existir.")
                    Assert.AreEqual("Descripcion Nueva Actualizada por Nube", reader("Descripción").ToString())
                    Assert.AreEqual(145.0D, Convert.ToDecimal(reader("Menudeo")))
                End Using
            End Using

            ' Limpieza
            Using cmdClean As New OleDbCommand("DELETE FROM Articulos WHERE Clave = '" & testClave & "'", conn)
                cmdClean.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    <TestMethod>
    Public Sub Test_DatabaseSync_DoesNotOverwriteExistingArticle_WhenRemoteFumIsOlder()
        Dim testClave As String = "TEST_DB_OLD_001"
        Dim deltaName As String = "tienda_remota_ART_testdb_old_" & Guid.NewGuid().ToString("N").Substring(0, 6) & ".json"

        ' 1. Insertar artículo base con fecha reciente (hoy)
        Using conn As New OleDbConnection(artConnStr)
            conn.Open()
            Using cmdDel As New OleDbCommand("DELETE FROM Articulos WHERE Clave = '" & testClave & "'", conn)
                cmdDel.ExecuteNonQuery()
            End Using

            Dim sqlBase As String = "INSERT INTO Articulos (Clave, Descripción, Menudeo, Fum, Del) VALUES (?, ?, ?, ?, ?)"
            Using cmdBase As New OleDbCommand(sqlBase, conn)
                cmdBase.Parameters.Add("?", OleDbType.VarWChar).Value = testClave
                cmdBase.Parameters.Add("?", OleDbType.VarWChar).Value = "Cambio Local Reciente"
                cmdBase.Parameters.Add("?", OleDbType.Currency).Value = 300.0D
                cmdBase.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now
                cmdBase.Parameters.Add("?", OleDbType.Boolean).Value = False
                cmdBase.ExecuteNonQuery()
            End Using
        End Using

        ' 2. Crear delta remoto con FUM más antigua (año 2020)
        Dim data As New Dictionary(Of String, Object)()
        data("clave") = testClave
        data("descripcion") = "Cambio Remoto Antiguo Que Debe Ser Ignorado"
        data("menudeo") = 50.0D
        data("fum") = New DateTime(2020, 5, 1).ToString("yyyy-MM-dd HH:mm:ss")
        data("del") = False

        CreateRemoteDelta(deltaName, "ARTICULO", "UPSERT", data)

        ' 3. Ejecutar sincronización
        nube.lectorbasecloud()

        ' 4. Verificar que se conservó el valor local más reciente
        Using conn As New OleDbConnection(artConnStr)
            conn.Open()
            Using cmd As New OleDbCommand("SELECT Descripción, Menudeo FROM Articulos WHERE Clave = '" & testClave & "'", conn)
                Using reader = cmd.ExecuteReader()
                    Assert.IsTrue(reader.Read())
                    Assert.AreEqual("Cambio Local Reciente", reader("Descripción").ToString(), "No debe sobreescribirse si el delta remoto es más antiguo que el FUM local.")
                    Assert.AreEqual(300.0D, Convert.ToDecimal(reader("Menudeo")))
                End Using
            End Using

            Using cmdClean As New OleDbCommand("DELETE FROM Articulos WHERE Clave = '" & testClave & "'", conn)
                cmdClean.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    <TestMethod>
    Public Sub Test_DatabaseSync_SoftDeletesArticle()
        Dim testClave As String = "TEST_DB_DEL_001"
        Dim deltaName As String = "tienda_remota_ART_testdb_del_" & Guid.NewGuid().ToString("N").Substring(0, 6) & ".json"

        ' 1. Insertar artículo base activo
        Using conn As New OleDbConnection(artConnStr)
            conn.Open()
            Using cmdDel As New OleDbCommand("DELETE FROM Articulos WHERE Clave = '" & testClave & "'", conn)
                cmdDel.ExecuteNonQuery()
            End Using

            Dim sqlBase As String = "INSERT INTO Articulos (Clave, Descripción, Del) VALUES (?, ?, ?)"
            Using cmdBase As New OleDbCommand(sqlBase, conn)
                cmdBase.Parameters.Add("?", OleDbType.VarWChar).Value = testClave
                cmdBase.Parameters.Add("?", OleDbType.VarWChar).Value = "Articulo a dar de baja"
                cmdBase.Parameters.Add("?", OleDbType.Boolean).Value = False
                cmdBase.ExecuteNonQuery()
            End Using
        End Using

        ' 2. Crear delta de DELETE
        Dim data As New Dictionary(Of String, Object)()
        data("clave") = testClave
        data("del") = True
        data("fechadel") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        CreateRemoteDelta(deltaName, "ARTICULO", "DELETE", data)

        ' 3. Ejecutar sincronización
        nube.lectorbasecloud()

        ' 4. Verificar que Del = True en base de datos
        Using conn As New OleDbConnection(artConnStr)
            conn.Open()
            Using cmd As New OleDbCommand("SELECT Del FROM Articulos WHERE Clave = '" & testClave & "'", conn)
                Using reader = cmd.ExecuteReader()
                    Assert.IsTrue(reader.Read())
                    Assert.IsTrue(Convert.ToBoolean(reader("Del")), "El artículo debe tener Del = True tras la sincronización de baja.")
                End Using
            End Using

            Using cmdClean As New OleDbCommand("DELETE FROM Articulos WHERE Clave = '" & testClave & "'", conn)
                cmdClean.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    <TestMethod>
    Public Sub Test_DatabaseSync_InsertsClientSafely_WithoutIdCollision()
        Dim testRFC As String = "CLTE888801"
        Dim testNombre As String = "Cliente Test BaseDatos Unico"
        Dim deltaName1 As String = "tienda_remota_CLN_testdb_clnt1_" & Guid.NewGuid().ToString("N").Substring(0, 6) & ".json"

        ' 1. Asegurar limpieza previa
        Using conn As New OleDbConnection(clntConnStr)
            conn.Open()
            Using cmdDel As New OleDbCommand("DELETE FROM client WHERE RFC = '" & testRFC & "' OR Nombre = '" & testNombre & "'", conn)
                cmdDel.ExecuteNonQuery()
            End Using
        End Using

        ' 2. Crear delta remoto con un remoteId ficticio (ej. 99999) para probar que no colisiona
        Dim data1 As New Dictionary(Of String, Object)()
        data1("remoteId") = 99999
        data1("nombre") = testNombre
        data1("rfc") = testRFC
        data1("telcel") = "7714449999"
        data1("direccion") = "Calle Original 10"
        data1("mayoreo") = True
        data1("ultmod") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        CreateRemoteDelta(deltaName1, "CLIENTE", "UPSERT", data1)

        ' 3. Ejecutar sincronización (alta de cliente)
        nube.lectorbasecloud()

        Dim assignedId As Integer = 0
        Using conn As New OleDbConnection(clntConnStr)
            conn.Open()
            Using cmd As New OleDbCommand("SELECT Id, Nombre, Dirección, Telcel FROM client WHERE RFC = '" & testRFC & "'", conn)
                Using reader = cmd.ExecuteReader()
                    Assert.IsTrue(reader.Read(), "El cliente debe haber sido insertado en clnt.dat.")
                    assignedId = Convert.ToInt32(reader("Id"))
                    Assert.IsGreaterThan(0, assignedId, "El ID asignado debe ser un número entero positivo.")
                    Assert.AreNotEqual(99999, assignedId, "El ID local seguro no debe pisar con el remoteId arbitrario.")
                    Assert.AreEqual("Calle Original 10", reader("Dirección").ToString())
                    Assert.AreEqual("7714449999", reader("Telcel").ToString())
                End Using
            End Using
        End Using

        ' 4. Probar actualización del mismo cliente (mismo RFC, nueva dirección)
        Dim deltaName2 As String = "tienda_remota_CLN_testdb_clnt2_" & Guid.NewGuid().ToString("N").Substring(0, 6) & ".json"
        Dim data2 As New Dictionary(Of String, Object)()
        data2("remoteId") = 99999
        data2("nombre") = testNombre
        data2("rfc") = testRFC
        data2("telcel") = "7714449999"
        data2("direccion") = "Nueva Direccion Actualizada 200"
        data2("mayoreo") = True
        data2("ultmod") = DateTime.Now.AddSeconds(5).ToString("yyyy-MM-dd HH:mm:ss")

        CreateRemoteDelta(deltaName2, "CLIENTE", "UPSERT", data2)
        nube.lectorbasecloud()

        Using conn As New OleDbConnection(clntConnStr)
            conn.Open()
            Using cmd As New OleDbCommand("SELECT Id, Dirección FROM client WHERE RFC = '" & testRFC & "'", conn)
                Using reader = cmd.ExecuteReader()
                    Assert.IsTrue(reader.Read())
                    Assert.AreEqual(assignedId, Convert.ToInt32(reader("Id")), "El ID local del cliente debe preservarse.")
                    Assert.AreEqual("Nueva Direccion Actualizada 200", reader("Dirección").ToString(), "La dirección debe haberse actualizado sin duplicar el cliente.")
                    Assert.IsFalse(reader.Read(), "No debe existir más de un registro para el mismo cliente.")
                End Using
            End Using

            ' Limpieza
            Using cmdClean As New OleDbCommand("DELETE FROM client WHERE RFC = '" & testRFC & "'", conn)
                cmdClean.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class
