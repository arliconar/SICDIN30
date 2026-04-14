Public Class Reportes
    Dim cbuttoncaso As Integer = 0
    Dim cothertienda As Boolean = False
    Dim tien As lugar = New lugar

    Private Sub tiendas_SelectedIndexChanged(sender As Object, e As EventArgs)
    End Sub
    Public Sub loader()
        tiendas.Items.Clear()
        tiendas.Items.AddRange(getnamesplaces(True))
        tiendas.Text = tienda.nombre
    End Sub
    Private Sub psup_Paint(sender As Object, e As PaintEventArgs) Handles psup.Paint

    End Sub
    Public Sub ventasdia()
        Dim sttienda As String = ""
        tien = New lugar
        tien.load(tiendas.Text)
        Dim conbackvent As OleDb.OleDbConnection
        If tienda.nombre <> tien.nombre Then
            conbackvent = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & pathbackup & tien.apodo & "\venta.dat;Persist Security Info=True;Jet OLEDB:Database Password=vsganeo5")
            cothertienda = True
        Else
            conbackvent = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\sc3\venta.dat;Persist Security Info=True;Jet OLEDB:Database Password=vsganeo5")
            cothertienda = False
        End If

        conbackvent.Open()
        cbuttoncaso = 0
        Dim st As String = "SELECT Pagoefe,Pagotar,Pagotrans,Fecha FROM lv WHERE Fecha>=#" & onlyfechasql(detp.Value) & "# AND Fecha<=#" & onlyfechasql(atp.Value) & "# AND Pagoefe>=0 AND Cancel=False"
        Dim orden As New OleDb.OleDbCommand(st, conbackvent)
        dgventas.Columns.Item(0).Visible = True
        dgventas.Columns.Item(0).HeaderText = "Día semana"
        dgventas.Columns.Item(1).Visible = True
        dgventas.Columns.Item(1).HeaderText = "Día"
        dgventas.Columns.Item(2).Visible = True
        dgventas.Columns.Item(2).HeaderText = "Total efectivo"
        dgventas.Columns.Item(3).Visible = True
        dgventas.Columns.Item(3).HeaderText = "Total tarjeta"
        dgventas.Columns.Item(4).Visible = True
        dgventas.Columns.Item(4).HeaderText = "Total transferencias"
        dgventas.Columns.Item(5).Visible = True
        dgventas.Columns.Item(5).HeaderText = "Total ventas"
        dgventas.Columns.Item(6).Visible = True
        dgventas.Columns.Item(6).HeaderText = "Abrir día"
        Dim btomlv As DataGridViewImageColumn = dgventas.Columns.Item(6)
        btomlv.Image = SICDIN30.My.Resources.Resources.libreta
        btomlv.ImageLayout = DataGridViewImageCellLayout.Zoom



        dgventas.Rows.Clear()

        Dim totalefe As Decimal = 0
        Dim totaltar As Decimal = 0
        Dim totaltrans As Decimal = 0
        Dim totalefedias As Decimal = 0
        Dim totalventadia As Decimal = 0
        Dim totaltransdia As Decimal = 0

        Dim total As Decimal = 0
        Dim totaltardias As Decimal = 0
        Dim fechaa As Date = Now
        Dim bfirst As Boolean = True
        Dim i As Integer = 0
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        While lector.Read
            If bfirst Then
                fechaa = lector("Fecha")
                bfirst = False
            End If
            If lector("fecha") <> fechaa Then
                dgventas.Rows.Add()
                dgventas(0, i).Value = diasemana(fechaa.DayOfWeek)
                dgventas(1, i).Value = fechaa.ToShortDateString()
                dgventas(2, i).Value = totalefe
                dgventas(3, i).Value = totaltar
                dgventas(4, i).Value = totaltrans
                dgventas(5, i).Value = totalventadia
                fechaa = lector("fecha")
                i += 1
                totalefe = 0
                totaltar = 0
                totaltrans = 0
                totalventadia = 0
            End If
            totalefe += lector("pagoefe")
            totaltar += lector("Pagotar")
            totaltrans += lector("Pagotrans")
            totalventadia += lector("pagoefe") + lector("Pagotar") + lector("Pagotrans")
            total += lector("pagoefe") + lector("Pagotrans") + lector("Pagotar")
            totalefedias += lector("pagoefe")
            totaltardias += lector("Pagotar")
            totaltransdia += lector("Pagotrans")
        End While
        dgventas.Rows.Add()
        dgventas(0, i).Value = diasemana(fechaa.DayOfWeek)
        dgventas(1, i).Value = fechaa.ToShortDateString()
        dgventas(2, i).Value = totalefe
        dgventas(3, i).Value = totaltar
        dgventas(4, i).Value = totaltrans
        dgventas(5, i).Value = totalventadia

        dgventas(0, i + 1).Value = "Total"
        dgventas(2, i + 1).Value = totalefedias
        dgventas(3, i + 1).Value = totaltardias
        dgventas(4, i + 1).Value = total

        lector.Close()
        conbackvent.Close()


    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ventasdia()


    End Sub

    Private Sub dgventas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgventas.CellContentClick
        If e.ColumnIndex = 6 And dgventas.Rows.Count > 0 Then
            Select Case cbuttoncaso
                Case 0
                    If e.RowIndex < dgventas.Rows.Count - 1 Then
                        main.libretaloader()
                        If cothertienda Then
                            libreta.loaddaytienda(dgventas(1, e.RowIndex).Value, tien)
                        Else
                            libreta.loadday(dgventas(1, e.RowIndex).Value)

                        End If

                    End If

            End Select
        End If

    End Sub

    Private Sub tiendas_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles tiendas.SelectedIndexChanged

    End Sub
End Class