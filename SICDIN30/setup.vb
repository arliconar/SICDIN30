Imports System.Data.OleDb
Imports System.Globalization
Imports System.Net
Imports System.Threading

Public Class setup
    Public tien As lugar
    Public Sub loader()
        maintrans()
        If codeprint Then
            fuentelabel.Text = "Fuente:" & fcode.Name
        Else
            fuentelabel.Text = "Fuente:No asignada"
        End If
        com.Text = "Compilación:" & My.Application.Info.Version.ToString

        tien = New lugar
        Dim lugares As lugar() = getlugares(True)
        ComboBox1.Items.Clear()
        For i = 0 To lugares.Count - 1
            ComboBox1.Items.Add(lugares(i).nombre)
        Next
        printselect.SelectedIndex = 0

        ComboBox1.SelectedIndex = 0
        Me.ShowDialog()
        mainnormal()

    End Sub
    Private Sub fontb_Click(sender As Object, e As EventArgs) Handles fontb.Click
        FontDialog.ShowEffects = False
        FontDialog.ShowColor = False
        FontDialog.Font = fcode
        If FontDialog.ShowDialog() = DialogResult.OK Then
            fuentelabel.Text = "Fuente:" & FontDialog.Font.Name
            Dim st As String = "UPDATE syste SET Fontbarcode='" & FontDialog.Font.Name & "'"
            Dim orden As OleDbCommand = New OleDbCommand(st, consys)
            orden.ExecuteNonQuery()
            fcode = New Font(FontDialog.Font.Name, 24)
            codeprint = True
        End If

    End Sub

    Private Sub panelsetup_Paint(sender As Object, e As PaintEventArgs) Handles panelsetup.Paint

    End Sub

    Private Sub fontb_MouseEnter(sender As Object, e As EventArgs) Handles fontb.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub fontb_MouseLeave(sender As Object, e As EventArgs) Handles fontb.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim st As String = "UPDATE syste SET Fontbarcode=''"
        Dim orden As OleDbCommand = New OleDbCommand(st, consys)
        orden.ExecuteNonQuery()
        codeprint = False
        fuentelabel.Text = "Fuente:No asignada"

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            MessageBox.Show(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Dim resp = MessageBox.Show("¿Desea sincronizar con la nube ahora?" & vbCrLf & vbCrLf &
                                   "- Seleccione 'Sí' para sincronizar cambios pendientes de otras tiendas." & vbCrLf &
                                   "- Seleccione 'No' para exportar todo el catálogo local de artículos a la nube." & vbCrLf &
                                   "- Seleccione 'Cancelar' para volver.", "Sincronización en la Nube", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        If resp = DialogResult.Yes Then
            Dim t As New Thread(Sub()
                                    Try
                                        nube.lectorbasecloud()
                                        MessageBox.Show("Sincronización finalizada. Revise C:\sc3\sync.log para detalles.", "SICDIN", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Catch ex As Exception
                                        LogAppError(ex, "setup.Button3_Click_1.lectorbasecloud")
                                        MessageBox.Show("Error durante la sincronización: " & ex.Message, "SICDIN", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    End Try
                                End Sub)
            t.IsBackground = True
            t.Start()
        ElseIf resp = DialogResult.No Then
            Dim t As New Thread(Sub()
                                    Try
                                        Dim total = nube.ExportFullCatalogDeltas()
                                        MessageBox.Show("Exportación completa finalizada. Se exportaron " & total & " artículos a la nube." & vbCrLf & "Revise C:\sc3\sync.log para detalles.", "SICDIN", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Catch ex As Exception
                                        LogAppError(ex, "setup.Button3_Click_1.ExportFullCatalogDeltas")
                                        MessageBox.Show("Error durante la exportación: " & ex.Message, "SICDIN", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    End Try
                                End Sub)
            t.IsBackground = True
            t.Start()
        End If
    End Sub
    Private Sub syncall()
        OpenFileDialog1.ShowDialog()
        Dim path As String = OpenFileDialog1.FileName
        If path <> "" Then
            Dim st As String = "SELECT * FROM Articulos"
            Dim orden As OleDbCommand = New OleDbCommand(st, conart)
            Dim lector As OleDbDataReader = orden.ExecuteReader
            Dim i As Integer = 0
            While lector.Read
                i += 1
            End While
            lector.Close()
            syncpr.Maximum = i
            syncpr.Minimum = 0
            syncpr.Value = 0
            syncpr.Step = 1
            Dim consync As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & path & ";Persist Security Info=True;Jet OLEDB:Database Password=sganeo4")
            consync.Open()
            orden = New OleDbCommand(st, consync)
            lector = orden.ExecuteReader
            i = 0
            Dim ia As Integer = 0
            If lector.HasRows Then
                syncpr.Show()
                While lector.Read
                    syncpr.Increment(1)
                    Dim art As articuloventa = New articuloventa
                    With art
                        .load(lector("Clave"), True)
                        .descripcion = lector("Descripción")
                        .menudeo = lector("Menudeo")
                        .mayoreo = lector("Mayoreo")
                        .pme = voiddeleter(lector("pme"))
                        .especial = voiddeleter(lector("Especial"))
                        .precio = voiddeleter(lector("Precio"))
                        .pgmen = voiddeleter(lector("pgmen"))
                        .pgmay = voiddeleter(lector("pgmay"))
                        .stock = voiddeleter(lector("stock"))
                        .pq = voiddeleterstring(lector("paquete"), "PZA")
                        .cbarra = voiddeleterstring(lector("cbarra"))
                        .tipo = voiddeleterstring(lector("tipo"), "Articulos")
                        .notas = voiddeleterstring(lector("notas"))
                        .tag = voiddeleterstring(lector("tags"))
                        If .found Then
                            Dim fum As Date = lector("Fum")
                            If .fum < fum Then
                                .updateart()
                                i += 1
                            End If
                        Else
                            .fum = lector("Fum")
                            .altaart()
                            ia += 1
                        End If
                    End With
                End While
                syncpr.Hide()
                MessageBox.Show("Proceso terminado" & vbNewLine & "Agregados:" & ia & "    Modificados:" & i, "SICDIN 3")
            End If
            consync.Close()
            consync.Dispose()
        End If



    End Sub

    Private Sub pingbutton_MouseEnter(sender As Object, e As EventArgs)
        shownamebutton(sender)

    End Sub

    Private Sub Button3_MouseLeave(sender As Object, e As EventArgs)
        hidenamebutton(sender)
    End Sub

    Private Sub Desde_CheckedChanged(sender As Object, e As EventArgs) Handles Desde.CheckedChanged
        If Desde.Checked Then
            nubecalendar.Enabled = True
        Else
            nubecalendar.Enabled = False
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles printempty.Click
        Select Case printselect.Text
            Case "Venta"
                Dim ntventa As notaventa = New notaventa
                ntventa.clnt.Nombre = ""
                ntventa.folio = Microsoft.VisualBasic.Left(tienda.apodo.ToUpper, 1) & "VM_______"
                imprimirventa(ntventa, 0, 0, 0, 0, True)


            Case "Reparación"
                Dim prep As imprimir = New imprimir
                Dim rep As repbic = New repbic
                rep.clnt.Nombre=""
                rep.folio = Microsoft.VisualBasic.Left(tienda.apodo.ToUpper, 1) & "RM_______"
                prep.printreparacion(rep, True)

        End Select
    End Sub
End Class