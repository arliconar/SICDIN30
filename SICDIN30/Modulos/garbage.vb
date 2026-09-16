Imports System.IO
Imports System.Threading


Module garbage
    Public timeoldart As Integer = 30
    Public timeoldvent As Integer = 1825

    Public Sub startgarbagecolector()
        Try
            Dim hilo As Thread = New Thread(New ThreadStart(AddressOf threadgarbage))
            hilo.IsBackground = True
            hilo.Start()
        Catch ex As Exception
            LogAppError(ex, "garbage.startgarbagecolector")
        End Try
    End Sub

    Public Sub threadgarbage()
        Try
            deloldbici()
            deloldapar()
            deloldart()
            deloldlv()
            deloldvent()
        Catch ex As Exception
            LogAppError(ex, "garbage.threadgarbage")
        End Try
    End Sub

    Public Sub deloldbici()
        Try
            Dim folios As New List(Of String)()
            Dim st As String = "SELECT Folio FROM nota WHERE (Fechae<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#' AND bentregada=TRUE) OR (Fcan=TRUE AND Fcancelada<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#')"
            SyncLock conbicrep
                Using cmd As New OleDb.OleDbCommand(st, conbicrep)
                    Using rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()
                        While rdr.Read()
                            folios.Add(rdr("Folio").ToString())
                        End While
                    End Using
                End Using

                For Each folio In folios
                    Dim safeFolio = sqlSafe(folio)
                    Using cmd2 As New OleDb.OleDbCommand("DELETE FROM art WHERE Folio='" & safeFolio & "'", conbicrep)
                        cmd2.ExecuteNonQuery()
                    End Using
                    Using cmd2 As New OleDb.OleDbCommand("DELETE FROM bici WHERE Folio='" & safeFolio & "'", conbicrep)
                        cmd2.ExecuteNonQuery()
                    End Using
                    Using cmd2 As New OleDb.OleDbCommand("DELETE FROM Mod WHERE Folio='" & safeFolio & "'", conbicrep)
                        cmd2.ExecuteNonQuery()
                    End Using
                    Using cmd2 As New OleDb.OleDbCommand("DELETE FROM pagos WHERE Folio='" & safeFolio & "'", conbicrep)
                        cmd2.ExecuteNonQuery()
                    End Using
                Next

                Using cmdDel As New OleDb.OleDbCommand(st.Replace("SELECT Folio", "DELETE"), conbicrep)
                    cmdDel.ExecuteNonQuery()
                End Using
            End SyncLock
        Catch ex As Exception
            LogAppError(ex, "garbage.deloldbici")
        End Try
    End Sub

    Public Sub deloldart()
        Try
            Dim st As String = "DELETE FROM Articulos WHERE Del=TRUE AND Fechadel<'#" & fechasql(Now.AddDays(-timeoldart)) & "#'"
            SyncLock conart
                Using cmd As New OleDb.OleDbCommand(st, conart)
                    cmd.ExecuteNonQuery()
                End Using
            End SyncLock
        Catch ex As Exception
            LogAppError(ex, "garbage.deloldart")
        End Try
    End Sub

    Public Sub deloldapar()
        Try
            Dim folios As New List(Of String)()
            Dim st As String = "SELECT Folio FROM Apartado WHERE Fentregado<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#'"
            SyncLock convent
                Using cmd As New OleDb.OleDbCommand(st, convent)
                    Using rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()
                        While rdr.Read()
                            folios.Add(rdr("Folio").ToString())
                        End While
                    End Using
                End Using

                For Each folio In folios
                    Dim safeFolio = sqlSafe(folio)
                    Using cmd2 As New OleDb.OleDbCommand("DELETE FROM Articulos WHERE FOLIO='" & safeFolio & "'", convent)
                        cmd2.ExecuteNonQuery()
                    End Using
                    Using cmd2 As New OleDb.OleDbCommand("DELETE FROM pagoapartado WHERE Folio='" & safeFolio & "'", convent)
                        cmd2.ExecuteNonQuery()
                    End Using
                Next

                st = "DELETE FROM Apartado WHERE Fechae<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#'"
                Using cmdDel As New OleDb.OleDbCommand(st, convent)
                    cmdDel.ExecuteNonQuery()
                End Using
            End SyncLock
        Catch ex As Exception
            LogAppError(ex, "garbage.deloldapar")
        End Try
    End Sub

    Public Sub deloldvent()
        Try
            Dim folios As New List(Of String)()
            Dim st As String = "SELECT Folio FROM venta WHERE Fechae<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#'"
            SyncLock convent
                Using cmd As New OleDb.OleDbCommand(st, convent)
                    Using rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()
                        While rdr.Read()
                            folios.Add(rdr("Folio").ToString())
                        End While
                    End Using
                End Using

                For Each folio In folios
                    Dim safeFolio = sqlSafe(folio)
                    Using cmd2 As New OleDb.OleDbCommand("DELETE FROM Articulos WHERE FOLIO='" & safeFolio & "'", convent)
                        cmd2.ExecuteNonQuery()
                    End Using
                    Using cmd2 As New OleDb.OleDbCommand("DELETE FROM Clientes WHERE Folio='" & safeFolio & "'", convent)
                        cmd2.ExecuteNonQuery()
                    End Using
                Next

                st = "DELETE FROM venta WHERE Fechae<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#'"
                Using cmdDel As New OleDb.OleDbCommand(st, convent)
                    cmdDel.ExecuteNonQuery()
                End Using
            End SyncLock
        Catch ex As Exception
            LogAppError(ex, "garbage.deloldvent")
        End Try
    End Sub

    Public Sub deloldlv()
        Try
            SyncLock convent
                Dim st As String = "DELETE FROM lv WHERE fecha<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#'"
                Using cmd As New OleDb.OleDbCommand(st, convent)
                    cmd.ExecuteNonQuery()
                End Using
                st = "DELETE FROM Dias WHERE Fecha<'#" & fechasql(Now.AddDays(-timeoldvent)) & "#'"
                Using cmd As New OleDb.OleDbCommand(st, convent)
                    cmd.ExecuteNonQuery()
                End Using
            End SyncLock
        Catch ex As Exception
            LogAppError(ex, "garbage.deloldlv")
        End Try
    End Sub
End Module
