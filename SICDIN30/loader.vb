Imports System.Threading

Public Class loader
    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub

    Private Sub loader_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            initvarvent()

            Try
                conart.Open()
                consys.Open()
                conclnt.Open()
                convent.Open()
                contra.Open()
                conbicrep.Open()
            Catch exConn As Exception
                LogAppError(exConn, "loader.OpenConnections")
                MessageBox.Show("Advertencia al conectar con bases de datos locales:" & vbCrLf & exConn.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try

            Try
                autocompletarclnt()
            Catch
            End Try

            Try
                If System.IO.File.Exists("C:\sc3\resources\icono1.ico") Then
                    icono = New Icon("C:\sc3\resources\icono1.ico")
                End If
            Catch
            End Try

            Try
                SyncLock consys
                    Using cmd As New OleDb.OleDbCommand("SELECT * FROM syste", consys)
                        Using rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()
                            While rdr.Read()
                                idti = rdr("Tienda")
                                Dim stcode As String = voiddeleterstring(rdr("Fontbarcode"))
                                If stcode <> "" Then
                                    Try
                                        fcode = New Font(stcode, 24)
                                        codeprint = True
                                    Catch
                                        codeprint = False
                                    End Try
                                Else
                                    codeprint = False
                                End If
                                pathbackup = rdr("pathback")
                                cloudpath = rdr("cloudpath")
                                pathimage = rdr("imagenespath")
                                Try
                                    Select Case idti
                                        Case 0
                                            If System.IO.File.Exists("C:\sc3\resources\icono.ico") Then
                                                icono = New Icon("C:\sc3\resources\icono.ico")
                                            End If
                                        Case 1
                                            If System.IO.File.Exists("C:\sc3\resources\icono1.ico") Then
                                                icono = New Icon("C:\sc3\resources\icono1.ico")
                                            End If
                                        Case 2
                                            If System.IO.File.Exists("C:\sc3\resources\icono2.ico") Then
                                                icono = New Icon("C:\sc3\resources\icono2.ico")
                                            End If
                                    End Select
                                Catch
                                End Try
                            End While
                        End Using
                    End Using
                End SyncLock
            Catch exSys As Exception
                LogAppError(exSys, "loader.ReadSyste")
            End Try

            tienda = New lugar
            tienda.load(idti)

            diaventa = New diatrabajo
            diaventa.abrirdia(user)

            If icono IsNot Nothing Then
                main.Icon = icono
            End If
            main.Show()
            main.Text = "SICDIN 3.0: " & user.nombre

            main.mainloadfirts()
        Catch ex As Exception
            LogAppError(ex, "loader.loader_Load")
        Finally
            Me.Close()
        End Try
    End Sub
End Class
