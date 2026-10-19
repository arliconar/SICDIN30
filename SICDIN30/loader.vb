Imports System.Threading

Public Class loader
    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub

    Private Sub loader_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ' Mostrar estado en la barra de carga
            UpdateLoaderStatus("Comprobando actualizaciones...", 10)

            ' 1. Verificación de actualización en GitHub
            Dim updating As Boolean = CheckAndPromptUpdate()
            If updating Then
                ' Si el usuario aceptó y se descargó la actualización, el script auxiliar
                ' se encargará de reemplazar archivos y relanzar el sistema. Cerramos de inmediato.
                Application.Exit()
                Return
            End If

            UpdateLoaderStatus("Inicializando variables del sistema...", 25)
            initvarvent()

            UpdateLoaderStatus("Conectando con bases de datos...", 45)
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

            UpdateLoaderStatus("Cargando clientes y artículos...", 65)
            Try
                autocompletarclnt()
            Catch
            End Try

            Try
                autocompletarart()
            Catch ex As Exception
                LogAppError(ex, "loader.autocompletarart")
            End Try

            Try
                If System.IO.File.Exists("C:\sc3\resources\icono1.ico") Then
                    icono = New Icon("C:\sc3\resources\icono1.ico")
                End If
            Catch
            End Try

            UpdateLoaderStatus("Cargando configuración de tienda...", 80)
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

            UpdateLoaderStatus("Preparando ventana principal...", 100)
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

    ''' <summary>
    ''' Actualiza el texto descriptivo y porcentaje visual del loader.
    ''' </summary>
    Private Sub UpdateLoaderStatus(ByVal statusText As String, ByVal progressPercent As Integer)
        Try
            If LabelStatus IsNot Nothing Then
                LabelStatus.Text = statusText
            End If
            If ProgressBar1 IsNot Nothing Then
                Dim clamped As Integer = Math.Max(0, Math.Min(100, progressPercent))
                ProgressBar1.Value = clamped
            End If
            Application.DoEvents()
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Consulta GitHub Releases y si hay una versión superior pregunta al usuario si desea actualizar.
    ''' Devuelve True si se inició el proceso de actualización y la app debe cerrarse.
    ''' </summary>
    Private Function CheckAndPromptUpdate() As Boolean
        Try
            Dim curVer As Version = AutoUpdater.GetCurrentVersion()
            Dim latestRelease As ReleaseInfo = AutoUpdater.CheckForUpdates(timeoutSeconds:=5)

            If latestRelease Is Nothing OrElse String.IsNullOrEmpty(latestRelease.TagName) Then
                Return False
            End If

            ' Comprobar si la versión de GitHub es estrictamente mayor que la local
            If AutoUpdater.IsNewerVersion(curVer, latestRelease.TagName) Then
                Dim msg As String = $"Hay una nueva versión disponible de SICDIN 3.0:{vbCrLf}{vbCrLf}" &
                                    $"• Versión instalada: v{curVer.ToString()}{vbCrLf}" &
                                    $"• Nueva versión: {latestRelease.TagName} ({latestRelease.Name}){vbCrLf}{vbCrLf}"

                If Not String.IsNullOrWhiteSpace(latestRelease.Body) Then
                    Dim previewBody As String = latestRelease.Body.Trim()
                    If previewBody.Length > 200 Then previewBody = previewBody.Substring(0, 200) & "..."
                    msg &= $"Novedades:{vbCrLf}{previewBody}{vbCrLf}{vbCrLf}"
                End If

                msg &= "¿Desea descargar e instalar la actualización ahora?"

                Dim result As DialogResult = MessageBox.Show(msg, "Actualización disponible", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If result = DialogResult.Yes Then
                    UpdateLoaderStatus($"Descargando actualización ({latestRelease.TagName})... 0%", 10)

                    Dim tempZip As String = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"sicdin_update_{latestRelease.TagName}.zip")

                    Dim downloaded As Boolean = AutoUpdater.DownloadUpdate(latestRelease.DownloadUrl, tempZip, Sub(pct)
                        UpdateLoaderStatus($"Descargando actualización ({latestRelease.TagName})... {pct}%", pct)
                    End Sub)

                    If downloaded AndAlso System.IO.File.Exists(tempZip) Then
                        UpdateLoaderStatus("Instalando actualización y reiniciando...", 100)
                        Dim appDir As String = AppDomain.CurrentDomain.BaseDirectory
                        Dim curPid As Integer = Process.GetCurrentProcess().Id

                        AutoUpdater.LaunchUpdateScript(tempZip, appDir, curPid)
                        Return True
                    Else
                        MessageBox.Show("No se pudo descargar el archivo de actualización. Se iniciará el sistema con la versión actual.", "Aviso de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If
            End If
        Catch ex As Exception
            LogAppError(ex, "loader.CheckAndPromptUpdate")
        End Try

        Return False
    End Function
End Class
