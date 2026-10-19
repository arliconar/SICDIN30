Imports System.IO
Imports System.Net.Http
Imports System.Reflection
Imports Newtonsoft.Json.Linq

Public Class ReleaseInfo
    Public Property TagName As String = ""
    Public Property Name As String = ""
    Public Property Body As String = ""
    Public Property DownloadUrl As String = ""
    Public Property AssetName As String = ""
    Public Property AssetSize As Long = 0
End Class

Public Module AutoUpdater
    Private Const RepoOwner As String = "arliconar"
    Private Const RepoName As String = "SICDIN30"
    Private ReadOnly ApiUrl As String = $"https://api.github.com/repos/{RepoOwner}/{RepoName}/releases"

    ''' <summary>
    ''' Obtiene la versión actual de la aplicación.
    ''' </summary>
    Public Function GetCurrentVersion() As Version
        Try
            Dim ver As Version = Assembly.GetExecutingAssembly().GetName().Version
            If ver IsNot Nothing Then Return ver
        Catch
        End Try
        Return New Version(1, 0, 0, 0)
    End Function

    ''' <summary>
    ''' Limpia cadenas de versión como 'v1.5' o 'v1.5.0.0' para convertirlas a Version.
    ''' </summary>
    Public Function ParseVersion(ByVal rawTag As String) As Version
        If String.IsNullOrWhiteSpace(rawTag) Then
            Return New Version(0, 0, 0, 0)
        End If

        Dim clean As String = rawTag.Trim()
        If clean.StartsWith("v", StringComparison.OrdinalIgnoreCase) Then
            clean = clean.Substring(1).Trim()
        End If

        ' Si viene algo como 1.5, System.Version requiere al menos major.minor
        Dim parts As String() = clean.Split("."c)
        Dim numbers As New List(Of Integer)
        For Each p In parts
            Dim n As Integer
            If Integer.TryParse(p, n) Then
                numbers.Add(n)
            Else
                Exit For
            End If
        Next

        While numbers.Count < 2
            numbers.Add(0)
        End While

        Select Case numbers.Count
            Case 2
                Return New Version(numbers(0), numbers(1))
            Case 3
                Return New Version(numbers(0), numbers(1), numbers(2))
            Case Else
                Return New Version(numbers(0), numbers(1), numbers(2), numbers(3))
        End Select
    End Function

    ''' <summary>
    ''' Determina si remoteTag es más reciente que currentVersion.
    ''' </summary>
    Public Function IsNewerVersion(ByVal currentVersion As Version, ByVal remoteTag As String) As Boolean
        Try
            Dim remoteVer As Version = ParseVersion(remoteTag)
            Return remoteVer > currentVersion
        Catch ex As Exception
            LogAppError(ex, "AutoUpdater.IsNewerVersion")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Consulta GitHub API para obtener los datos de la última versión y su paquete ZIP completo.
    ''' </summary>
    Public Function CheckForUpdates(Optional ByVal timeoutSeconds As Integer = 8) As ReleaseInfo
        Try
            Using client As New HttpClient()
                client.Timeout = TimeSpan.FromSeconds(timeoutSeconds)
                client.DefaultRequestHeaders.Add("User-Agent", "SICDIN30-AutoUpdater")
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json")

                Dim response As HttpResponseMessage = client.GetAsync(ApiUrl).GetAwaiter().GetResult()
                If Not response.IsSuccessStatusCode Then
                    Return Nothing
                End If

                Dim jsonStr As String = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                Dim releases As JArray = JArray.Parse(jsonStr)
                If releases.Count = 0 Then
                    Return Nothing
                End If

                ' Buscar la release más reciente válida (no borrador / no prerelease si hay estables)
                For Each item In releases
                    Dim isDraft As Boolean = item.Value(Of Boolean)("draft")
                    If isDraft Then Continue For

                    Dim tag As String = item.Value(Of String)("tag_name")
                    Dim name As String = item.Value(Of String)("name")
                    Dim body As String = item.Value(Of String)("body")
                    Dim assets As JArray = item("assets")

                    ' Buscamos primero el zip completo: SICDIN30-Full-*.zip o cualquier .zip
                    Dim targetDownloadUrl As String = ""
                    Dim targetAssetName As String = ""
                    Dim targetSize As Long = 0

                    If assets IsNot Nothing Then
                        ' Prioridad 1: SICDIN30-Full-*.zip
                        For Each asset In assets
                            Dim aName As String = asset.Value(Of String)("name")
                            If aName.StartsWith("SICDIN30-Full-", StringComparison.OrdinalIgnoreCase) AndAlso aName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) Then
                                targetDownloadUrl = asset.Value(Of String)("browser_download_url")
                                targetAssetName = aName
                                targetSize = asset.Value(Of Long)("size")
                                Exit For
                            End If
                        Next

                        ' Prioridad 2: Cualquier .zip que no sea solo resources.zip
                        If String.IsNullOrEmpty(targetDownloadUrl) Then
                            For Each asset In assets
                                Dim aName As String = asset.Value(Of String)("name")
                                If aName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) AndAlso Not aName.Equals("resources.zip", StringComparison.OrdinalIgnoreCase) Then
                                    targetDownloadUrl = asset.Value(Of String)("browser_download_url")
                                    targetAssetName = aName
                                    targetSize = asset.Value(Of Long)("size")
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    If Not String.IsNullOrEmpty(targetDownloadUrl) Then
                        Dim rel As New ReleaseInfo With {
                            .TagName = tag,
                            .Name = If(String.IsNullOrEmpty(name), tag, name),
                            .Body = If(body, ""),
                            .DownloadUrl = targetDownloadUrl,
                            .AssetName = targetAssetName,
                            .AssetSize = targetSize
                        }
                        Return rel
                    End If
                Next

            End Using
        Catch ex As Exception
            LogAppError(ex, "AutoUpdater.CheckForUpdates")
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' Descarga el archivo de actualización a una carpeta temporal con reporte de progreso opcional.
    ''' </summary>
    Public Function DownloadUpdate(ByVal downloadUrl As String, ByVal destinationPath As String, Optional ByVal progressCallback As Action(Of Integer) = Nothing) As Boolean
        Try
            Dim targetDir As String = Path.GetDirectoryName(destinationPath)
            If Not Directory.Exists(targetDir) Then
                Directory.CreateDirectory(targetDir)
            End If

            If File.Exists(destinationPath) Then
                File.Delete(destinationPath)
            End If

            Using client As New HttpClient()
                client.Timeout = TimeSpan.FromMinutes(10)
                client.DefaultRequestHeaders.Add("User-Agent", "SICDIN30-AutoUpdater")

                Using response As HttpResponseMessage = client.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult()
                    response.EnsureSuccessStatusCode()

                    Dim totalBytes As Long = If(response.Content.Headers.ContentLength.HasValue, response.Content.Headers.ContentLength.Value, -1L)

                    Using sourceStream As Stream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult(),
                          destStream As New FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 81920, True)

                        Dim buffer(81919) As Byte
                        Dim bytesRead As Integer = 0
                        Dim totalDownloaded As Long = 0
                        Dim lastPercent As Integer = -1

                        Do
                            bytesRead = sourceStream.Read(buffer, 0, buffer.Length)
                            If bytesRead = 0 Then Exit Do

                            destStream.Write(buffer, 0, bytesRead)
                            totalDownloaded += bytesRead

                            If totalBytes > 0 AndAlso progressCallback IsNot Nothing Then
                                Dim pct As Integer = CInt((totalDownloaded * 100L) \ totalBytes)
                                If pct <> lastPercent Then
                                    lastPercent = pct
                                    progressCallback(pct)
                                End If
                            End If
                        Loop
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            LogAppError(ex, "AutoUpdater.DownloadUpdate")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Genera un script PowerShell que espera el cierre de SICDIN30, extrae el ZIP sobrescribiendo la instalación y vuelve a iniciar la aplicación.
    ''' </summary>
    Public Function LaunchUpdateScript(ByVal zipPath As String, ByVal appDirectory As String, ByVal currentProcessId As Integer) As Boolean
        Try
            Dim scriptPath As String = Path.Combine(Path.GetTempPath(), "sicdin_apply_update.ps1")
            Dim currentExeName As String = "SICDIN30.exe"

            ' Script de actualización seguro y resiliente
            Dim psScript As String = $"# Auto-generado por SICDIN30 AutoUpdater
param()
`$ErrorActionPreference = 'Stop'

`$appDir = '{appDirectory.Replace("'", "''")}'
`$zipFile = '{zipPath.Replace("'", "''")}'
`$procId = {currentProcessId}
`$exePath = Join-Path `$appDir '{currentExeName}'
`$logFile = 'C:\sc3\update.log'

Function Write-UpLog(`$msg) {{
    `$ts = (Get-Date).ToString('yyyy-MM-dd HH:mm:ss')
    try {{ Add-Content -Path `$logFile -Value ""[`$ts] `$msg"" -Encoding UTF8 }} catch {{}}
}}

Write-UpLog ""Iniciando actualizador de SICDIN30...""

# 1. Esperar a que el proceso SICDIN30 se cierre completamente
Write-UpLog ""Esperando cierre del proceso PID `$procId...""
`$timeoutSec = 20
`$waited = 0
while (`$waited -lt `$timeoutSec) {{
    `$p = Get-Process -Id `$procId -ErrorAction SilentlyContinue
    if (`$null -eq `$p) {{ break }}
    Start-Sleep -Milliseconds 500
    `$waited += 1
}}

# Asegurar que no quede ningún proceso SICDIN30 abierto en esa ruta
Start-Sleep -Seconds 1
`$running = Get-Process -Name 'SICDIN30' -ErrorAction SilentlyContinue
if (`$running) {{
    Write-UpLog ""Terminando procesos SICDIN30 remanentes...""
    `$running | Stop-Process -Force -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 1
}}

# 2. Descomprimir el ZIP sobre el directorio de la aplicación (excluyendo bases de datos .dat)
Write-UpLog ""Extrayendo actualización sobre `$appDir...""
Add-Type -AssemblyName 'System.IO.Compression.FileSystem'
`$archive = [System.IO.Compression.ZipFile]::OpenRead(`$zipFile)
try {{
    foreach (`$entry in `$archive.Entries) {{
        `$relName = `$entry.FullName
        # Proteger bases de datos existentes: nunca sobreescribir archivos .dat
        if (`$relName.EndsWith('.dat', [System.StringComparison]::OrdinalIgnoreCase)) {{
            Write-UpLog ""Omitiendo archivo de datos: `$relName""
            continue
        }}

        `$target = Join-Path `$appDir `$relName
        `$parent = Split-Path `$target -Parent
        if (-not (Test-Path `$parent)) {{
            New-Item -ItemType Directory -Path `$parent -Force | Out-Null
        }}

        # Si no es un directorio (las entradas que terminan en / son carpetas)
        if (-not [string]::IsNullOrEmpty(`$entry.Name)) {{
            [System.IO.Compression.ZipFileExtensions]::ExtractToFile(`$entry, `$target, `$true)
        }}
    }}
    Write-UpLog ""Extracción completada con éxito.""
}} catch {{
    Write-UpLog ""Error al extraer: `$(`$_.Exception.Message)""
    throw
}} finally {{
    `$archive.Dispose()
}}

# 3. Eliminar el archivo ZIP temporal
try {{
    Remove-Item -Force `$zipFile -ErrorAction SilentlyContinue
}} catch {{}}

# 4. Volver a iniciar la aplicación actualizada
Write-UpLog ""Iniciando versión actualizada `$exePath...""
Start-Process -FilePath `$exePath -WorkingDirectory `$appDir

Write-UpLog ""Actualización finalizada exitosamente.""
"
            File.WriteAllText(scriptPath, psScript, System.Text.Encoding.UTF8)

            ' Iniciar PowerShell en modo oculto / minimizado
            Dim psi As New ProcessStartInfo()
            psi.FileName = "powershell.exe"
            psi.Arguments = $"-NoProfile -ExecutionPolicy Bypass -File ""{scriptPath}"""
            psi.UseShellExecute = True
            psi.WindowStyle = ProcessWindowStyle.Hidden
            Process.Start(psi)

            Return True
        Catch ex As Exception
            LogAppError(ex, "AutoUpdater.LaunchUpdateScript")
            Return False
        End Try
    End Function
End Module

