Imports System.Data.OleDb

Imports System.Threading
Imports System.Runtime.InteropServices
Public Class Cliente
    Public found As Boolean
    Public id As Integer
    Public Nombre As String
    Public Direccion As String
    Public mayoreo As Boolean
    Public especial As Boolean
    Public adeudo As Decimal
    Public credito As Decimal
    Public dc As Integer
    Public RFC As String
    Public telcasa As String
    Public telcel As String
    Public addcloud As Boolean
    Public delcloud As Boolean
    Public ultmod As Date

    Public mail As String

    Public ultcompra As Date
    Public Sub loadfromnotas(ByRef nombres As String, ByRef rfcs As String, ByRef mails As String, ByRef telcels As String, ByRef telcasas As String)
        Dim st As String = "SELECT * FROM Clientes WHERE "
        Dim b As Boolean = False
        If Nombre <> "" Then
            st &= "Nombre LIKE '%" & nombres & "%'"
            b = True
        End If

        If RFC <> "" Then
            If b Then
                st &= " OR "
            End If
            st &= "RFC='" & rfcs & "'"
        End If

        If mail <> "" Then
            If b Then
                st &= " OR "
            End If
            st &= "mail='" & mails & "'"
        End If

        If telcel <> "" Then
            If b Then
                st &= " OR "
            End If
            st &= "telcel='" & telcels & "'"
        End If

        If telcasa <> "" Then
            If b Then
                st &= " OR "
            End If
            st &= "telcasa='" & telcasas & "'"
        End If

        Dim orden As New OleDbCommand(st, convent)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        If lector.HasRows Then
            found = True
        Else
            Exit Sub
        End If
        While lector.Read
            Nombre = lector("Nombre")
            RFC = lector("rfc")
            telcel = lector("telcel")
            telcasa = lector("telcasa")
            mail = lector("mail")
            Direccion = lector("Dirección")
            mayoreo = False
        End While
        lector.Close()

    End Sub
    Public Sub loadmovs(ByRef dg As DataGridView, op As Integer, n As Integer, d1 As Date, d2 As Date)
        Dim ds As BindingSource = New BindingSource
        Dim st As String = ""
        Select Case op
            Case 1
                st = "SELECT top " & n & " clave, Descripcion, fecha, horast, total, pagado,usuario FROM lv WHERE Cliente='" & idname() & "' ORDER BY fecha DESC"
            Case 2
                st = "SELECT clave, Descripcion, fecha, horast, total, pagado,usuario FROM lv WHERE Cliente='" & idname() & "' AND Fecha>=#" & onlyfechasql(d1) & "# AND Fecha<=#" & onlyfechasql(d2.AddDays(1)) & "# ORDER BY fecha DESC"
            Case 3
                st = "SELECT clave, Descripcion, fecha, horast, total, pagado,usuario FROM lv WHERE Cliente='" & idname() & "' ORDER BY fecha DESC"
        End Select

        Dim orden As OleDbCommand = New OleDbCommand(st, convent)
        Dim lector As OleDbDataReader = orden.ExecuteReader
        If lector.HasRows Then
            ds.DataSource = lector

            dg.DataSource = ds
        Else
            dg.Rows.Clear()
        End If

    End Sub
    Public Sub updatecompra()
        Dim st As String = "UPDATE client SET ultcompra='" & Now & "' WHERE id=" & id
        Dim orden As OleDbCommand = New OleDbCommand(st, conclnt)
        orden.ExecuteNonQuery()

    End Sub
    Public Function idname()

        If id >= 0 Then
            idname = id & ":" & Nombre
        Else
            idname = Nombre
        End If

    End Function
    Public Sub loadmostrador()
        id = 0
        clear()
        Nombre = "Mostrador"
    End Sub
    Public Sub New(Optional id As Integer = 1)
        ultmod = Now
        loadclnt(id)

    End Sub
    Public Sub loadclntidst(ids As String)
        Dim st() As String = ids.Split(":")
        loadclnt(Val(st(0)))

    End Sub
    Public Sub loadclntvent(folio As String)
        Dim st As String = "SELECT * FROM Clientes WHERE Folio='" & folio & "'"
        Dim orden As OleDbCommand = New OleDbCommand(st, convent)
        Dim lector As OleDbDataReader = orden.ExecuteReader
        While lector.Read
            Nombre = lector("Nombre")
            Direccion = voiddeleterstring(lector("Dirección"))
            RFC = voiddeleterstring(lector("RFC"))
            telcasa = voiddeleterstring(lector("Telcasa"))
            telcel = voiddeleterstring(lector("telcel"))
            mail = voiddeleterstring(lector("mail"))
        End While
        lector.Close()
    End Sub

    Public Sub loadclnt(ids As Integer)
        Dim st As String = "SELECT * FROM client WHERE Id=" & ids
        Dim ordenc As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, conclnt)
        Dim lectorc As OleDb.OleDbDataReader = ordenc.ExecuteReader
        found = False
        While lectorc.Read
            found = True
            id = ids
            Nombre = lectorc("Nombre")
            Direccion = lectorc("Dirección").ToString
            mayoreo = lectorc("Mayoreo")
            especial = lectorc("Especial")
            credito = voiddeleter(lectorc("credito"))
            RFC = lectorc("RFC").ToString
            telcasa = lectorc("Telcasa").ToString
            telcel = lectorc("Telcel").ToString
            mail = lectorc("mail").ToString
            dc = lectorc("dc")
            adeudo = voiddeleter(lectorc("Adeudo"))
            If Not DBNull.Value.Equals(lectorc("ultcompra")) Then
                ultcompra = lectorc("ultcompra")
            Else
                ultcompra = Now
            End If
            If Not DBNull.Value.Equals(lectorc("ultmod")) Then
                ultmod = lectorc("ultmod")
            Else
                ultmod = fechasql(Now)
            End If
        End While
        lectorc.Close()
    End Sub
    Public Sub loadclntst(nombres As String)
        Dim sep = nombres.Split(":")
        Dim num As Integer = 0
        If sep.Length > 1 Then
            If IsNumeric(sep(0)) Then
                num = sep(0)
            Else
                num = sep(1)
            End If
            loadclnt(num)
        Else
            found = False
        End If

    End Sub
    Public Sub clear()
        Nombre = ""
        Direccion = ""
        mayoreo = False
        especial = False
        adeudo = 0
        credito = 0
        dc = 0
        RFC = ""
        telcasa = ""
        telcel = ""
        mail = ""
    End Sub
    Public Sub modadeudo(adeudo As Decimal)
        Try
            If id > 1 Then
                Dim st As String = "UPDATE client SET Adeudo=" & adeudo & " Where Id=" & id
                Dim ordenc As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, conclnt)
                ordenc.ExecuteNonQuery()
            End If
        Catch ex As Exception
            MessageBox.Show("Error al actualizar el adeudo")
        End Try



    End Sub
    Public Sub updateme(Optional newdate As Boolean = True)
        Dim orden As OleDbCommand = New OleDbCommand
        Dim sep As String = "',"
        Dim st As String = "UPDATE client  SET "
        st &= "Nombre='" & Nombre & sep
        st &= "Dirección='" & Direccion & sep
        st &= "Mayoreo=" & mayoreo & ","
        st &= "Credito=" & credito & ","
        st &= "dc=" & dc & ","
        st &= "Especial=" & especial & ","
        st &= "RFC='" & RFC & sep
        st &= "Telcasa='" & telcasa & sep
        st &= "Telcel='" & telcel & sep
        If Not newdate Then
            st &= "ultmod='" & fechasql(ultmod) & sep
        End If

        st &= "Mail='" & mail & "' WHERE Id=" & id
        orden = New OleDbCommand(st, conclnt)
        orden.ExecuteNonQuery()

        Try
            If addcloud Then
                nube.QueueClienteDelta(Me, "UPSERT")
            End If
        Catch ex As Exception
            nube.SyncLog("Error al encolar actualización de cliente: " & ex.Message, True)
        End Try

    End Sub
    Public Sub addmov(detalle As String, Optional total As Decimal = 0, Optional pago As Decimal = 0, Optional adeudo As Decimal = 0)
        If found Then
            Dim st As String = "INSERT INTO movimientos (id,Detalle,Usuario,Fecha,Pago,Total,Adeudo) VALUES ("
            st &= id & ",'"
            st &= detalle & "','"
            st &= user.idnameusu & "','"
            st &= Now & "',"
            st &= pago & ","
            st &= total & ","
            st &= adeudo & ")"
            Dim orden As OleDbCommand = New OleDbCommand(st, conclnt)
            orden.ExecuteNonQuery()
        End If

    End Sub
    Private Function returnlastidcln() As Integer
        Dim id As Integer = 0
        orden = New OleDb.OleDbCommand("SELECT Id FROM client ORDER BY id", conclnt)
        lector = orden.ExecuteReader
        While lector.Read
            id = lector("Id")
        End While
        lector.Close()
        returnlastidcln = id + 1
    End Function
    Public Function addme(Optional ids As Integer = -1, Optional addc As Boolean = True) As Integer
        If Nombre = "" Then
            dialogs.advertencia("No ha colocado nombre al cliente")
            Return -1
            Exit Function
        End If
        If ids = -1 Then
            id = returnlastidcln()
        End If
        addcloud = addc
        Dim hilo As Thread = New Thread(New ThreadStart(AddressOf addclntthread))
        hilo.Start()
        Return id
    End Function
    Public Sub addclntthread()
        Dim st As String = "INSERT INTO client (id,Nombre,Dirección,Mayoreo,Credito,Adeudo,dc,Especial,RFC,Telcasa,Telcel,mail,ultmod,ultcompra) VALUES ('" & id
        Dim sep As String = "','"
        st = st & sep & Nombre
        st = st & sep & Direccion
        st = st & "'," & mayoreo
        st = st & ",'" & credito
        st = st & sep & 0
        st = st & sep & dc
        st = st & "'," & especial
        st = st & ",'" & RFC
        st = st & sep & telcasa
        st = st & sep & telcel
        st = st & sep & mail
        st = st & sep & fechasql(ultmod)
        st = st & sep & fechasql(Now)
        st = st & "')"
        orden = New OleDb.OleDbCommand(st, conclnt)
        orden.ExecuteNonQuery()
        autocompletarclnt()
        Try
            If addcloud Then
                nube.QueueClienteDelta(Me, "UPSERT")
            End If
        Catch ex As Exception
            nube.SyncLog("Error al encolar alta de cliente: " & ex.Message, True)
        End Try
    End Sub
    Public Sub deleteme(askme As Boolean, Optional delc As Boolean = True)
        If askme Then
            If dialogs.pregunta("¿Desea eliminar este cliente?" & vbNewLine & "Id:" & id & vbNewLine & "Nombre:" & Nombre) = vbNo Then
                Exit Sub
            End If
        End If
        delcloud = delc
        Dim hilo As Thread = New Thread(New ThreadStart(AddressOf deletemethread))
        hilo.Start()
    End Sub
    Private Sub deletemethread()
        Dim st As String = "DELETE FROM client WHERE Id=" & id
        orden = New OleDb.OleDbCommand(st, conclnt)
        orden.ExecuteNonQuery()
        Try
            If delcloud Then
                nube.QueueClienteDelta(Me, "DELETE")
            End If
        Catch ex As Exception
            nube.SyncLog("Error al encolar baja de cliente: " & ex.Message, True)
        End Try
    End Sub
    Public Function staddcon() As String ''crea la cadena para de conexion para agregar cliente
        staddcon = Nombre & "|" & Direccion & "|" & mayoreo.ToString & "|" & credito & "|" & dc & "|" & especial.ToString & "|" & RFC & "|" & telcasa & "|" & telcel & "|" & mail & "|" & ultcompra.ToString

    End Function
    Public Sub loadmenufromlector(lector As OleDb.OleDbDataReader)
        id = lector("Id")
        Nombre = lector("Nombre")
        Direccion = lector("Dirección")
        mayoreo = False
        especial = False
        credito = 0
        dc = 0
        telcel = lector("Telcel")
        telcasa = lector("Telcasa")
        RFC = lector("RFC")
        mail = lector("mail")

    End Sub
End Class
