Module funcionesdg
    Public Function getnamesplaces(Optional tienda As Boolean = False) As String()
        Dim st As String = "SELECT Nombre FROM Lugares"
        If tienda Then
            st &= " WHERE Tipo='Tienda'"
        End If
        Dim i As Integer = 0
        Dim nombres() As String
        ReDim Preserve nombres(0)
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, consys)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        While lector.Read
            ReDim Preserve nombres(i)
            nombres(i) = lector("Nombre")
            i += 1
        End While
        lector.Close()
        getnamesplaces = nombres

    End Function
    Public Function getidtablalugar(id As Integer) As String
        Dim st As String = "SELECT Idtable FROM Lugares WHERE Id=" & id
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, consys)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        getidtablalugar = ""
        While lector.Read
            getidtablalugar = lector("Idtable")
        End While
        lector.Close()
        orden.ExecuteReader.Close()

    End Function
    Public Sub dgcopyfromart(ByRef art As articuloventa, ByRef dg As DataGridView, rindex As Integer)

        With art
            dg("des", rindex).Value = .descripcion
            dg("pq", rindex).Value = .pq
            If .tipo = "Trabajo" Then
                dg("tra", rindex).Value = True
            Else
                dg("tra", rindex).Value = False
            End If
            dg("einf", rindex).Value = .einf
            dg("found", rindex).Value = .found
            dg("clave", rindex).Value = .clave
            dg("des", rindex).ToolTipText = .clave
            dg("notas", rindex).Value = .notas
            dg("exis", rindex).Value = .existencias
            dg("can", rindex).ToolTipText = "Existencias: " & .existencias

        End With

    End Sub
    Function sartdg(ByRef dgcant As Object, ByRef dgdes As DataGridViewCell, ByRef dgpq As Object, ByRef dgtp As Object, ByRef dgimp As Object, ByRef dgcaner As Object, ByRef dgclave As Object, ByRef dgtptext As Object, ByVal may As Boolean, ByVal esp As Boolean) As Boolean 'busca y coloca en el dg

        dgimp = ""
        dgpq = ""
        dgcaner = ""
        dgtptext = ""
        sartdg = False
        Dim importe As Decimal = 0
        If dgdes.Value = "" Then
            Exit Function
        End If
        Dim stdes As String = dgdes.Value
        orden = New OleDb.OleDbCommand("SELECT Clave,Descripción,Menudeo,Mayoreo,PME,Especial,Existencias,Paquete,Trabajo,Einf FROM Articulos WHERE Clave LIKE '" & stdes & "%' OR Descripción LIKE '" & stdes & "%' OR cbarra LIKE '" & stdes & "'", conart)
        lector = orden.ExecuteReader
        dgdes.ErrorText = "Artículo no encontrado"
        sartdg = lector.HasRows

        While lector.Read
            dgdes.Value = lector("Descripción")
            dgdes.ErrorText = ""
            dgpq = lector("paquete")
            dgclave = lector("clave")
            Dim menst As String = "1-Men:" & lector("Menudeo")
            Dim mayst As String = "2-May:" & lector("Mayoreo")
            dgtp.add(menst)
            dgtp.add(mayst)
            If may = True Then
                If esp = True Then
                    If Val(lector("PME")) > 0 Then
                        Dim espst As String = "3-Esp:" & lector("Especial")
                        dgtp.add(espst)
                        If dgcant >= Val(lector("PME")) Then
                            importe = lector("Especial") * dgcant

                            dgtptext = espst
                        Else
                            importe = lector("Mayoreo") * dgcant


                            dgtptext = mayst

                        End If
                    Else
                        importe = lector("Mayoreo") * dgcant
                        dgtptext = mayst
                    End If
                Else

                    importe = lector("Mayoreo") * dgcant

                    dgtptext = mayst
                End If
            Else
                importe = lector("Menudeo") * dgcant

                dgtptext = menst
            End If
            If dgcant > lector("existencias") And Not lector("Trabajo") And Not lector("Einf") Then
                dgcaner = "La cantidad excede las existencias"
            End If

            dgimp = importe
        End While
        lector.Close()


    End Function
    Public Sub searartdg(ByRef dg1 As DataGridView, Optional rindex As Integer = 0, Optional mayoreo As Boolean = False, Optional especial As Boolean = False, Optional ByVal allow As Boolean = True, Optional exact As Boolean = False)

        Dim a As DataGridViewComboBoxCell = dg1("tpv", rindex)
        a.Value = "" 'Inicializacion obligatoria para evitar mensajes de error cuando el combo se borra. Probable error de .NET
        a.Items.Clear()
        Dim art As articuloventa = New articuloventa
        art = sart(dg1("des", rindex).Value)
        dgcopyfromart(art, dg1, rindex)
        a.Items.Add("1.-m:" & art.menudeo)
        a.Items.Add("2.-M:" & art.mayoreo)
        a.Items.Add("3.-E:" & art.especial)
        If mayoreo Then
            If especial And art.pme > 0 And dg1("can", rindex).Value >= art.pme Then
                a.Value = "3.-E:" & art.especial
            Else
                a.Value = "2.-M:" & art.mayoreo
            End If
        Else
            a.Value = "1.-m:" & art.menudeo
        End If

        If dg1.Rows.Item(rindex).Index = dg1.Rows.Count - 1 And art.found Then
            dg1.Rows.Add()
        End If
    End Sub
    Public Sub adderdg(dg As Object)
        Dim rindex As Integer = dg.rows.Count - 1

        If rindex = dg.CurrentCell.RowIndex Then
            dg.Rows.Add()
        End If


    End Sub
    Public Sub dgdellrow(ByRef dg As DataGridView) ''borra la filas del boton del
        Dim rindex As Integer = dg.CurrentCell.RowIndex
        Dim cindex As Integer = dg.CurrentCell.ColumnIndex
        If cindex = dg.Columns.Item("del").Index And rindex < dg.Rows.Count - 1 And dg.Rows.Count > 1 Then
            dg.Rows.RemoveAt(rindex)
        End If
    End Sub
    Public Sub dgautocompletar(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) '' Handles dg.EditingControlShowing autocompletar para los dg
        If sender.CurrentCell.ColumnIndex = sender.columns.item("des").index AndAlso TypeOf e.Control Is TextBox Then
            With DirectCast(e.Control, TextBox)
                .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                .AutoCompleteSource = AutoCompleteSource.CustomSource
                .AutoCompleteCustomSource = asd
            End With
        End If
    End Sub
    Public Sub emptycell(dg As Object, e As DataGridViewCellEventArgs)

        If dg.Columns("can").Index = e.ColumnIndex Then
            If Val(dg("can", e.RowIndex).value) = 0 Then
                dg("can", e.RowIndex).value = 1
            End If

        End If


    End Sub
    Public Sub dgerrorfound(ByRef dg As DataGridView, Optional sto As String = "") ''coloca error en el des si no se ha encontrado el artículo
        Dim rindex As Integer = dg.CurrentCell.RowIndex
        If Not dg("found", rindex).Value Then
            If sto = "" Then
                dg("des", rindex).ErrorText = "Artículo no encontrado"
            Else
                dg("des", rindex).ErrorText = sto
            End If

        Else
            dg("des", rindex).ErrorText = ""
        End If
    End Sub
    Public Sub multiimp(ByVal dg1 As DataGridView, ByVal rindex As Integer)
        Dim a As DataGridViewComboBoxCell = dg1("tpv", rindex)
        If a.Value <> "" Then
            dg1("impv", rindex).Value = tpsplit(a.Value) * dg1("can", rindex).Value
        End If
        If Val(dg1("can", rindex).Value) > Val(dg1("exis", rindex).Value) And dg1("einf", rindex).Value = False And dg1("found", rindex).Value And dg1("tra", rindex).Value = False Then
            dg1("can", rindex).ErrorText = "No hay existencias suficientes" & Chr(13) & "Existencias en tienda:" & dg1("exis", rindex).Value
        Else
            dg1("can", rindex).ErrorText = ""

        End If

    End Sub
    Public Function dgsubtotal(dg As DataGridView) As Double

        dgsubtotal = 0
        Dim i As Integer
        For i = 0 To dg.Rows.Count - 1
            If dg("cancel", i).Value = False Then
                dgsubtotal += dg("impv", i).Value

            End If


        Next

    End Function
End Module

