Public Class ordentrabajo
    Public modificado As Boolean
    Public Sub loader(ByVal folio As String, id As Integer)
        dg.Rows.Clear()
        modificado = False
        Dim st As String = "SELECT Cantidad,Clave,colocado FROM art WHERE Folio='" & folio & "' AND Id=" & id & " AND cancel=FALSE"
        orden3 = New OleDb.OleDbCommand(st, conbicrep)
        lector3 = orden3.ExecuteReader
        Dim i As Integer = 0
        Dim artv As articuloventa = New articuloventa
        While lector3.Read
            dg.Rows.Add()
            artv = sart(lector3("Clave"))
            dgcopyfromart(artv, dg, i)
            dg("can", i).Value = lector3("cantidad")
            dg("col", i).Value = lector3("colocado")
            i += 1
        End While
        lector3.Close()
        Label1.Text = "Folio " & folio & " Bicicleta " & id
        maintrans()
        Me.ShowDialog()
        mainnormal()
        If modificado Then
            Dim art As String
            Dim colob As Boolean
            For i = 0 To dg.Rows.Count - 1
                art = dg("des", i).ToolTipText
                colob = dg("col", i).Value
                st = "UPDATE art SET colocado=" & colob & " WHERE Folio='" & folio & "' AND Id=" & id & "AND Clave='" & art & "'"
                orden = New OleDb.OleDbCommand(st, conbicrep)
                lector = orden.ExecuteReader
                lector.Close()
            Next
        End If
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg.CellContentClick

    End Sub

    Private Sub dg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg.CellClick
        If dg.Rows.Count > 0 And e.RowIndex > -1 Then
            If e.ColumnIndex = dg.Columns.Item("col").Index Then
                dg("col", e.RowIndex).Value = Not dg("col", e.RowIndex).Value
                modificado = True
            End If
        End If
    End Sub
End Class