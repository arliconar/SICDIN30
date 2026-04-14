Imports System.IO
Imports System.Threading


Module garbage
    Public timeoldart As Integer = 30
    Public timeoldvent As Integer = 1825
    Public Sub startgarbagecolector()
        Dim hilo As Thread = New Thread(New ThreadStart(AddressOf threadgarbage))

    End Sub
    Public Sub threadgarbage()
        deloldbici()
        deloldapar()
        deloldart()
        deloldlv()
        deloldvent()
    End Sub
    Public Sub deloldbici()
        Dim st As String = "SELECT Folio FROM nota WHERE (Fechae<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#' AND bentregada=TRUE) OR (Fcan=TRUE AND Fcancelada<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#')"
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, conbicrep)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        While lector.Read
            Dim folio As String = lector("Folio")
            Dim st2 As String = "DELETE FROM art WHERE Folio='" & folio & "'"
            Dim orden2 As OleDb.OleDbCommand = New OleDb.OleDbCommand(st2, conbicrep)
            orden2.ExecuteNonQuery()
            st2 = "DELETE FROM bici WHERE Folio='" & folio & "'"
            orden2 = New OleDb.OleDbCommand(st2, conbicrep)
            orden2.ExecuteNonQuery()
            st2 = "DELETE FROM Mod WHERE Folio='" & folio & "'"
            orden2 = New OleDb.OleDbCommand(st2, conbicrep)
            orden2.ExecuteNonQuery()
            st2 = "DELETE FROM pagos WHERE Folio='" & folio & "'"
            orden2 = New OleDb.OleDbCommand(st2, conbicrep)
            orden2.ExecuteNonQuery()
        End While
        lector.Close()
        st = "DELETE FROM nota WHERE (Fechae<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#' AND bentregada=TRUE) OR (Fcan=TRUE AND Fcancelada<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#')"
        orden = New OleDb.OleDbCommand(st, conbicrep)
        orden.ExecuteNonQuery()

    End Sub

    Public Sub deloldart()
        Dim st As String = "DELETE FROM Articulos WHERE Del=TRUE AND Fechadel<'#" & fechasql(Now.AddDays(-timeoldart)) & "#'"
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, conart)
        orden.ExecuteNonQuery()

    End Sub
    Public Sub deloldapar()
        Dim st As String = "SELECT Folio FROM Apartado WHERE Fentregado<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#'"
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, convent)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        While lector.Read
            Dim st2 As String = "DELETE FROM Articulos WHERE FOLIO='" & lector("Folio") & "'"
            Dim orden2 As OleDb.OleDbCommand = New OleDb.OleDbCommand(st2, convent)
            orden2.ExecuteNonQuery()
            st2 = "DELETE FROM pagoapartado WHERE Folio='" & lector("Folio") & "'" ''Borramos clientes de la nota
            orden2 = New OleDb.OleDbCommand(st2, convent)
            orden2.ExecuteNonQuery()
        End While
        lector.Close()

        st = "DELETE FROM Apartado WHERE Fechae<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#'" 'Borramos la venta
        orden = New OleDb.OleDbCommand(st)
        orden.ExecuteNonQuery()
    End Sub
    Public Sub deloldvent()
        Dim st As String = "SELECT Folio FROM venta WHERE Fechae<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#'"
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, convent)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        While lector.Read
            Dim st2 As String = "DELETE FROM Articulos WHERE FOLIO='" & lector("Folio") & "'" 'Borramos articulos de la nota
            Dim orden2 As OleDb.OleDbCommand = New OleDb.OleDbCommand(st2, convent)
            orden2.ExecuteNonQuery()
            st2 = "DELETE FROM Clientes WHERE Folio='" & lector("Folio") & "'" ''Borramos clientes de la nota
            orden2 = New OleDb.OleDbCommand(st2, convent)
            orden2.ExecuteNonQuery()

        End While
        lector.Close()
        st = "DELETE FROM venta WHERE Fechae<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#'" 'Borramos la venta
        orden = New OleDb.OleDbCommand(st)
        orden.ExecuteNonQuery()
    End Sub
    Public Sub deloldlv()
        Dim st As String = "DELETE FROM lv WHERE fecha<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#'"
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, convent)
        orden.ExecuteNonQuery()
        st = "DELETE FROM Dias WHERE Fecha<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#'"
        orden = New OleDb.OleDbCommand(st, convent)
        orden.ExecuteNonQuery()

    End Sub
End Module
