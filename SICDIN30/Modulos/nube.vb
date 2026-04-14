Imports System
Imports System.IO
Module nube

    Public Sub backinup()
        loadscreen.Close()

        loadscreen.loader(6, 1)

        loadscreen.stepp()
        loadscreen.loadtext.Text = "Respaldando base de artículos"
        conart.Close()
        Try
            My.Computer.FileSystem.CopyFile("c:\sc3\art.dat", pathbackup & tienda.apodo & "\art.dat", True) ''Copia la base de datos a cada directorio
        Catch ex As Exception

        End Try

        loadscreen.stepp()
        loadscreen.loadtext.Text = "Respaldando base de clientes"
        conclnt.Close()
        Try
            My.Computer.FileSystem.CopyFile("c:\sc3\clnt.dat", pathbackup & tienda.apodo & "\clnt.dat", True)

        Catch ex As Exception

        End Try

        loadscreen.stepp()
        loadscreen.loadtext.Text = "Respaldando base de ventas"
        convent.Close()
        Try
            My.Computer.FileSystem.CopyFile("c:\sc3\venta.dat", pathbackup & tienda.apodo & "\venta.dat", True)

        Catch ex As Exception

        End Try

        loadscreen.stepp()
        loadscreen.loadtext.Text = "Respaldando base de reparaciones"
        conbicrep.Close()
        Try
            My.Computer.FileSystem.CopyFile("c:\sc3\bicirep.dat", pathbackup & tienda.apodo & "\bicirep.dat", True)

        Catch ex As Exception

        End Try

        loadscreen.stepp()
        loadscreen.loadtext.Text = "Respaldando base de trabajadores"
        contra.Close()
        Try
            My.Computer.FileSystem.CopyFile("c:\sc3\trabajadores.dat", pathbackup & tienda.apodo & "\trabajadores.dat", True)

        Catch ex As Exception

        End Try

        loadscreen.stepp()
        loadscreen.loadtext.Text = "Respaldando base de usuarios"
        conusu.Close()
        Try
            My.Computer.FileSystem.CopyFile("c:\sc3\usu.dat", pathbackup & tienda.apodo & "\usu.dat", True)

        Catch ex As Exception

        End Try

        End

    End Sub
    Public Function syncdatabaseclnt(origen As String, fecha As Date) As Boolean
        syncdatabaseclnt = False
        Dim cont As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & origen & ";Persist Security Info=True;Jet OLEDB:Database Password=sganeo4")
        Try


            cont.Open()
        Dim st As String = "SELECT * FROM client WHERE ultmod>#" & fechasql(fecha) & "#"
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, cont)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader

        While lector.Read
            Dim clnt As Cliente = New Cliente(lector("id"))
            With clnt

                .Nombre = lector("Nombre")
                .Direccion = voiddeleterstring(lector("Dirección"))
                .mayoreo = lector("Mayoreo")
                .especial = lector("Especial")
                .credito = lector("Credito")
                .dc = lector("dc")
                .RFC = voiddeleterstring(lector("rfc"))
                .telcasa = voiddeleterstring(lector("telcasa"))
                .telcel = voiddeleterstring(lector("telcel"))
                .mail = voiddeleterstring(lector("mail"))
                .ultmod = lector("ultmod")
                If .found Then
                    .updateme(False)
                Else
                    .addme()
                End If
            End With
        End While
        lector.Close()
            cont.Close()

        Catch ex As Exception

        End Try
        syncdatabaseclnt = True
    End Function
    Public Function syncdatabaseart(origen As String, fecha As Date) As Boolean
        syncdatabaseart = False
        Dim condummod As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & cloudpath & "\" & tienda.apodo & ".art;Persist Security Info=True;Jet OLEDB:Database Password=sganeo4")
        condummod.Open()
        Dim cont As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & origen & ";Persist Security Info=True;Jet OLEDB:Database Password=sganeo4")
        cont.Open()
        Dim st As String = "SELECT * FROM Articulos WHERE FUM>#" & fechasql(fecha) & "#"
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, cont)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        While lector.Read
            Dim arti As articuloventa = New articuloventa(lector("clave"))
            Dim orden2 As OleDb.OleDbCommand
            Dim st2 As String = ""
            If arti.found Then
                st2 = "UPDATE Articulos SET Descripción='" & EscapeSql(lector("Descripción")) & "',"
                st2 &= "Menudeo=" & lector("Menudeo") & ","
                st2 &= "Mayoreo=" & lector("Mayoreo") & ","
                st2 &= "PME=" & voiddeleter(lector("PME")) & ","
                st2 &= "Especial=" & voiddeleter(lector("Especial")) & ","
                st2 &= "Paquete='" & EscapeSql(voiddeleterstring(lector("Paquete"))) & "',"
                st2 &= "Precio=" & voiddeleter(lector("Precio")) & ","
                st2 &= "PGmen=" & voiddeleter(lector("PGmen")) & ","
                st2 &= "PGmay=" & voiddeleter(lector("PGmay")) & ","
                st2 &= "cbarra='" & EscapeSql(voiddeleterstring(lector("cbarra"))) & "',"
                st2 &= "Tipo='" & EscapeSql(voiddeleterstring(lector("Tipo"))) & "',"
                st2 &= "Einf=" & lector("Einf") & ","
                st2 &= "Notas='" & EscapeSql(voiddeleterstring(lector("Notas"))) & "',"
                st2 &= "Tags='" & EscapeSql(voiddeleterstring(lector("Tags"))) & "',"
                st2 &= "FUM='" & EscapeSql(lector("FUM")) & "',"
                If lector("Del") = True Then
                    st2 &= "Del=" & lector("Del") & ","
                    st2 &= "Fechadel='" & EscapeSql(lector("Fechadel")) & "',"
                End If
                st2 &= "usuariofum='" & EscapeSql(voiddeleterstring(lector("usuariofum"))) & "' WHERE Clave='" & EscapeSql(lector("clave")) & "'"
            Else

                st2 = "INSERT INTO Articulos (Clave,Descripción,Menudeo,Mayoreo,PME,Especial,Existencias,Paquete,Precio,PGmen,PGmay,Fum,cbarra,Tipo,Einf,Notas,Tags,usuariofum) VALUES ('"
                st2 &= EscapeSql(lector("clave")) & "','"
                st2 &= EscapeSql(lector("Descripción")) & "',"
                st2 &= lector("Menudeo") & ","
                st2 &= lector("Mayoreo") & ","
                st2 &= voiddeleter(lector("PME")) & ","
                st2 &= voiddeleter(lector("Especial")) & ","
                st2 &= 0 & ",'"
                st2 &= EscapeSql(voiddeleterstring(lector("Paquete"))) & "',"
                st2 &= voiddeleter(lector("Precio")) & ","
                st2 &= voiddeleter(lector("PGmen")) & ","
                st2 &= voiddeleter(lector("PGMay")) & ",'"
                st2 &= EscapeSql(lector("FUM")) & "','"
                st2 &= EscapeSql(voiddeleterstring(lector("Cbarra"))) & "','"
                st2 &= EscapeSql(voiddeleterstring(lector("Tipo"))) & "',"
                st2 &= voiddeleter(lector("Einf")) & ",'"
                st2 &= EscapeSql(voiddeleterstring(lector("Notas"))) & "','"
                st2 &= EscapeSql(voiddeleterstring(lector("Tags"))) & "','"
                st2 &= EscapeSql(voiddeleterstring(lector("usuariofum"))) & "')"
            End If


            orden2 = New OleDb.OleDbCommand(st2, conart)
            Try
                orden2.ExecuteNonQuery()
            Catch ex As Exception
            End Try

            Dim orden3 As OleDb.OleDbCommand = New OleDb.OleDbCommand(st2, condummod)
            Try
                orden3.ExecuteNonQuery()
            Catch ex As Exception
            End Try
        End While

        lector.Close()
        cont.Close()
        condummod.Close()

        syncdatabaseart = True
    End Function

    Public Sub lectorbasecloud(Optional fecha As Date = datedefault)
        'Dim lugares As lugar() = getlugares(True)
        Try


            If lectorbasebusy Then 'Si ya hay un proceso, salimos de este
                Exit Sub
            Else
                lectorbasebusy = True
                blockexit = True
            End If
            Dim files As String() = Directory.GetFiles(cloudpath, "*.art")
            For Each archivo As String In files
                Dim separ As String() = archivo.Split("\")
                Dim nameextonly As String = separ(separ.Count - 1)
                Dim nameonly As String = nameextonly.Split(".")(0)

                If Not nameonly = tienda.apodo Then 'Verificamos que no sea el archivo de la misma tienda para evitar reintroducir
                    Dim lastmodarchivo As Date = File.GetLastWriteTime(archivo)
                    Dim lug As lugar = New lugar
                    lug.load(nameonly, True)
                    If lastmodarchivo > lug.lastmod Then
                        Dim stfilework As String = "C:\sc3\dummybase\" & nameonly
                        Dim bopen As Boolean = False
                        Dim intentos As Integer = 0
                        While bopen = False AndAlso intentos < 10
                            Try
                                File.Copy(archivo, stfilework, True)
                                bopen = True
                            Catch ex As Exception
                                intentos += 1
                                System.Threading.Thread.Sleep(500)
                            End Try
                        End While
                        If bopen Then
                            If Not syncdatabaseart(stfilework, lug.lastmod) Then
                                dialogs.advertencia("Ocurrio un error mientras se sincronizaba con la base" & vbNewLine & archivo)
                            End If
                            Dim st As String = "UPDATE Lugares SET Lastmod='" & fechasql(lastmodarchivo) & "' WHERE Apodo='" & lug.apodo & "'"
                            Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, consys)
                            orden.ExecuteNonQuery()
                        End If

                    End If
                End If

            Next
            ''Sincronizamos los clientes
            files = Directory.GetFiles(cloudpath, "*.cln")
            For Each archivo As String In files
                Dim separ As String() = archivo.Split("\")
                Dim nameextonly As String = separ(separ.Count - 1)
                Dim nameonly As String = nameextonly.Split(".")(0)

                If Not nameonly = tienda.apodo Then 'Verificamos que no sea el archivo de la misma tienda para evitar reintroducir
                    Dim lastmodarchivo As Date = File.GetLastWriteTime(archivo)
                    Dim lug As lugar = New lugar
                    lug.load(nameonly, True)
                    If lastmodarchivo > lug.lastclnmod Then

                        Dim stfilework As String = "C:\sc3\dummybase\" & nameonly
                        Dim bopen As Boolean = False
                        Dim intentos As Integer = 0
                        While bopen = False AndAlso intentos < 10
                            Try
                                File.Copy(archivo, stfilework, True)
                                bopen = True
                            Catch ex As Exception
                                intentos += 1
                                System.Threading.Thread.Sleep(500)
                            End Try
                        End While
                        If bopen Then
                            If Not syncdatabaseclnt(stfilework, lug.lastclnmod) Then
                                dialogs.advertencia("Ocurrio un error mientras se sincronizaba con la base" & vbNewLine & archivo)
                            End If
                            Dim st As String = "UPDATE Lugares SET Lastclnmod='" & fechasql(lastmodarchivo) & "' WHERE Apodo='" & lug.apodo & "'"
                            Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, consys)
                            orden.ExecuteNonQuery()
                        End If

                    End If
                End If
            Next

        Catch ex As Exception

        Finally
            lectorbasebusy = False
            blockexit = False
        End Try
    End Sub

    Private Function EscapeSql(ByVal value As Object) As String
        If IsDBNull(value) OrElse value Is Nothing Then Return ""
        Return value.ToString().Replace("'", "''")
    End Function

End Module
