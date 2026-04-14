Imports System.Data.OleDb
Imports System.Diagnostics.Tracing

Public Class Usuariosform

    Public total As Decimal
    Public totaldestajo As Decimal
    Public user As usuario
    Public lastmonday As Date = Today
    Public loader As Boolean = False
    Public tra As trabajador
    Public Sub loadme()
        dgtrabajo.Rows.Clear()
    End Sub
    Public Sub loadusers()
        Dim st As String = "SELECT Activo,Id,Nombre,Admin,Fu,Telefono FROM Usuario ORDER BY Id"
        Dim orden As OleDbCommand = New OleDbCommand(st, conusu)
        Dim ds As BindingSource = New BindingSource
        Dim lector As OleDbDataReader = orden.ExecuteReader
        ds.DataSource = lector
        dgusers.DataSource = ds
        lector.Close()
        If dgusers.Rows.Count > 1 Then
            dgusers.Columns.Item("Fu").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgusers.Columns.Item("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dgusers.Columns.Item("Fu").HeaderText = "Último acceso"
        End If



    End Sub
    Private Sub colorusers()
        For i = 0 To dgusers.Rows.Count - 1
            If dgusers("activo", i).Value = False Then
                dgusers.Rows.Item(i).DefaultCellStyle.BackColor = Color.Red

            End If
        Next
    End Sub
    Public Sub loadtrabajadores()
        combotra.Items.Clear()
        Dim st As String = "SELECT Id,Nombre,Favorito FROM trabajador"
        If activo.Checked Or destajo.Checked Then
            st &= " WHERE "
            If activo.Checked Then
                st &= "activo=TRUE"
                If destajo.Checked Then
                    st &= " AND destajo=TRUE"
                End If
            Else
                st &= "destajo=TRUE"

            End If
        End If
        Dim orden As OleDbCommand = New OleDbCommand(st, contra)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        Dim i As Integer = 0
        Dim idfav As Integer = 0
        Dim fav As Boolean = False
        While lector.Read
            If Not lector("favorito") Then
                combotra.Items.Add(lector("Id") & ":" & lector("Nombre"))
                If fav = False Then
                    idfav += 1
                End If
            Else
                idfav = lector("Id")
                combotra.Items.Add(lector("Id") & ":" & lector("Nombre") & "*")
                fav = True
            End If
        End While
        lector.Close()
        combotra.SelectedIndex = i
        tra = New trabajador
        tra.load(idfav)
        fechaa.Value = Today
        lastmonday = Today
        If lastmonday.DayOfWeek <> 1 Then
            For i = 1 To 6
                lastmonday = lastmonday.AddDays(-1)
                If lastmonday.DayOfWeek = 1 Then
                    Exit For
                End If
            Next
        Else
            lastmonday = lastmonday.AddDays(-7)
        End If
        fechade.Value = lastmonday
        combotra_SelectedIndexChanged()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles fechade.ValueChanged
        If fechade.Value < tra.ufc Then
            dialogs.Close()

            dialogs.advertencia("Atención la fecha seleccionada es menor a la última de pago")

        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgtrabajo.CellContentClick

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
    Public Sub onlytienda()

        TabControl1.TabPages.Remove(Trabajadores)
    End Sub
    Private Sub activo_CheckedChanged(sender As Object, e As EventArgs) Handles activo.CheckedChanged, destajo.CheckedChanged
        loadtrabajadores()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles nuevomec.Click
        trabajadorform.addnew()
        loadtrabajadores()

    End Sub

    Private Sub combotra_SelectedIndexChanged() Handles combotra.SelectedIndexChanged
        Dim st() As String = combotra.Text.Split(":")
        tra = New trabajador
        tra.load(st(0))
        loadcuentatrabajo()
        fechade.Value = lastmonday
        fechaa.Value = Today

        If tra.activo Then
            altabaja.BackgroundImage = SICDIN30.My.Resources.Resources.cancel_77947
            altabaja.Tag = "Dar de baja"
        Else
            altabaja.BackgroundImage = SICDIN30.My.Resources.Resources.vcsnormal_93488
            altabaja.Tag = "Dar de Alta"
        End If
    End Sub

    Public Sub loadcuentatrabajo()
        If Not loader Then
            Exit Sub
        End If
        Dim st As String = "SELECT * FROM art WHERE Realizo=" & tra.id & " AND Fecha>=#" & onlyfechasql(fechade.Value) & "#" & " AND Fecha<=#" & onlyfechasql(fechaa.Value) & "# AND Trabajo=TRUE AND colocado=TRUE"
        Dim orden As OleDbCommand = New OleDbCommand(st, conbicrep)
        Dim lector As OleDbDataReader = orden.ExecuteReader
        Dim i As Integer = 0
        dgtrabajo.Rows.Clear()
        total = 0
        If lector.HasRows Then
            While lector.Read
                dgtrabajo.Rows.Add()
                Dim dummydate As Date = lector("Fecha")
                dgtrabajo("Fecha", i).Value = dummydate.ToShortDateString
                Dim dummydate2 As Date = lector("hora")
                dgtrabajo("Hora", i).Value = dummydate2.ToShortTimeString
                dgtrabajo("Folio", i).Value = lector("Folio")
                dgtrabajo("Cantidad", i).Value = lector("Cantidad")
                dgtrabajo("Clave", i).Value = lector("Clave")
                dgtrabajo("Des", i).Value = lector("Descripción")
                dgtrabajo("Importe", i).Value = returnpreonly(lector("tp"))
                total += Val(lector("Cantidad")) * returnpreonly(lector("tp"))
                dgtrabajo("Tot", i).Value = Val(lector("Cantidad")) * returnpreonly(lector("tp"))
                dgtrabajo("Fechar", i).Value = dummydate.ToString("yyMMdd") & dummydate2.ToString("HHmmss")
                i += 1
            End While
        End If
        lector.Close()
        st = "SELECT * FROM trabajos WHERE Realizo=" & tra.id & " AND Fecha>=#" & onlyfechasql(fechade.Value) & "#" & " AND Fecha<=#" & onlyfechasql(fechaa.Value) & "# "
        orden = New OleDbCommand(st, contra)
        lector = orden.ExecuteReader
        If lector.HasRows Then
            While lector.Read
                dgtrabajo.Rows.Add()
                Dim dummydate As Date = lector("hora")
                dgtrabajo("Fecha", i).Value = dummydate.ToShortDateString
                dgtrabajo("Hora", i).Value = dummydate.ToShortTimeString
                dgtrabajo("Folio", i).Value = lector("Folio")
                Dim cant As Integer = voiddeleter(lector("Cantidad"))
                If cant = 0 Then
                    cant = 1
                End If
                dgtrabajo("Cantidad", i).Value = cant
                dgtrabajo("Clave", i).Value = lector("Clave")
                dgtrabajo("Des", i).Value = lector("Descripcion")
                dgtrabajo("Importe", i).Value = Val(lector("Importe")) / cant
                dgtrabajo("Fechar", i).Value = dummydate.ToString("yyMMdd") & dummydate.ToString("HHmmss")
                dgtrabajo("Tot", i).Value = lector("importe")
                total += cant * lector("importe")
                i += 1
            End While
        End If
        dgtrabajo.Sort(dgtrabajo.Columns.Item("Fechar"), System.ComponentModel.ListSortDirection.Ascending)

        totaldestajo = total * (tra.pdestajo / 100)
        totallabel.Text = "Total:$" & total & " Total destajo:$" & totaldestajo

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles favomec.Click
        dialogs.Close()

        If dialogs.pregunta("¿Desea colocar a este trabajador como el favorito?") = DialogResult.Yes Then
            Dim st As String = "UPDATE trabajador SET Favorito=FALSE"
            Dim orden As OleDbCommand = New OleDbCommand(st, contra)
            orden.ExecuteNonQuery()
            st = "UPDATE trabajador SET Favorito=TRUE WHERE Id=" & tra.id
            orden = New OleDbCommand(st, contra)
            orden.ExecuteNonQuery()
            loadtrabajadores()
        End If

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub fechaa_ValueChanged(sender As Object, e As EventArgs) Handles fechaa.ValueChanged, fechade.ValueChanged

        If sender.value > Today Then
            sender.value = Today
        End If
        loadcuentatrabajo()
    End Sub

    Private Sub baja_Click(sender As Object, e As EventArgs) Handles altabaja.Click
        If tra.activo Then
            dialogs.Close()

            If dialogs.pregunta("¿Deseas colocar a este trabajador como inactivo?") = DialogResult.Yes Then
                tra.disableme()
                loadtrabajadores()

            End If
        Else
            dialogs.Close()

            If dialogs.pregunta("¿Deseas colocar a este trabajador como activo?") = DialogResult.Yes Then
                tra.enableme()
                loadtrabajadores()

            End If
        End If

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles pagar.Click
        dialogs.Close()

        If dialogs.pregunta("¿Desea pagar a este trabajador el periodo de:" & Chr(13) & fechade.Value & " a " & fechaa.Value & "?") Then
            tra.pagar()
            tra.ufc = Today
            fechade.Value = Today
            loadcuentatrabajo()

        End If
    End Sub

    Private Sub pagar_MouseHover(sender As Object, e As EventArgs) Handles pagar.MouseEnter, altabaja.MouseEnter, favomec.MouseEnter, nuevomec.MouseEnter
        shownamebutton(sender)


    End Sub

    Private Sub nuevomec_MouseLeave(sender As Object, e As EventArgs) Handles nuevomec.MouseLeave, altabaja.MouseLeave, favomec.MouseLeave, pagar.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub adduser_Click(sender As Object, e As EventArgs) Handles adduser.Click
        userform.addnew()
        loadusers()

    End Sub

    Private Sub Block_Click(sender As Object, e As EventArgs) Handles Block.Click

        Dim rindex As Integer = dgusers.CurrentCell.RowIndex
        If dgusers("activo", rindex).Value = True Then
            dialogs.Close()

            If dialogs.pregunta("¿Desea inhabilitar a este usuario:" & Chr(13) & dgusers("Id", rindex).Value & ":" & dgusers("Nombre", rindex).Value & "?") = DialogResult.Yes Then
                user = New usuario

                user.loadid(dgusers("Id", rindex).Value)
                user.blockme()
                loadusers()


            End If
        Else
            dialogs.Close()

            If dialogs.pregunta("¿Desea habilitar a este usuario:" & Chr(13) & dgusers("Id", rindex).Value & ":" & dgusers("Nombre", rindex).Value & "?") = DialogResult.Yes Then
                user = New usuario

                user.loadid(dgusers("Id", rindex).Value)
                user.enableme()
                loadusers()
            End If



        End If



    End Sub

    Private Sub dgusers_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgusers.CellContentClick

    End Sub

    Private Sub dgusers_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgusers.CellValueChanged

    End Sub

    Private Sub modpass_Click(sender As Object, e As EventArgs) Handles modpass.Click
        Dim rindex As Integer = dgusers.CurrentCell.RowIndex
        userform.edit(dgusers("Id", rindex).Value)
        loadusers()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        loader = True
        Timer1.Enabled = False
        loadcuentatrabajo()

    End Sub
End Class