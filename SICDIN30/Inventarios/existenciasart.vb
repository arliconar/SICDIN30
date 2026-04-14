Imports System
Imports System.IO
Public Class existenciasart
    Public art As articuloventa



    Public Sub loader(clave As String)
        art = New articuloventa(clave)
        clavetext.Text = "Clave:" & art.clave
        Descripciontext.Text = "Descripción:" & art.descripcion


        'If art.tipo = "Trabajo" Or art.einf Then
        '    Panel9.Visible = False
        'Else
        '    Panel9.Visible = True
        'End If

        lectorbasecloud()
        Dim files As String() = Directory.GetFiles("C:\sc3\dummybase\", "*.art")

        dg.Rows.Clear()
        Dim i As Integer = 0
        dg.Rows.Add()
        dg("tien", i).Value = tienda.nombre
        dg("Cant", i).Value = art.existencias
        i = 1
        For Each archivo As String In files

            Dim separ As String() = archivo.Split("\")
            Dim nameextonly As String = separ(separ.Count - 1)
            Dim nameonly As String = nameextonly.Split(".")(0)

            Dim tiendas As lugar = New lugar
            tiendas.load(nameonly, True)
            Dim condur As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & archivo & ";Persist Security Info=True;Jet OLEDB:Database Password=sganeo4")
            condur.Open()
            Dim st As String = "SELECT Existencias FROM Articulos WHERE Clave='" & clave & "'"
            Dim orden As New OleDb.OleDbCommand(st, condur)
            Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader

            While lector.Read
                dg.Rows.Add()
                dg("tien", i).Value = tiendas.nombre
                dg("Cant", i).Value = lector("Existencias")
                i += 1
            End While
                lector.Close()
                condur.Close()


        Next
        maintrans()

        Me.ShowDialog()
        mainnormal()

    End Sub

    Private Sub Panel6_Paint(sender As Object, e As PaintEventArgs) Handles Panel6.Paint

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            de.Enabled = True
            a.Enabled = True
            cantidad.Enabled = True
            Transfer.Enabled = True
        Else
            de.Enabled = False
            a.Enabled = False
            cantidad.Enabled = False
            Transfer.Enabled = False
        End If
    End Sub

    Private Sub dg_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg.CellContentClick

    End Sub

    Private Sub de_SelectedIndexChanged(sender As Object, e As EventArgs) Handles de.SelectedIndexChanged, a.SelectedIndexChanged
        If de.Text = a.Text Then
            ErrorProvider1.SetError(cantidad, "No puedes transferir en la misma tienda")
            okv.Enabled = False
        Else
            ErrorProvider1.SetError(cantidad, "")
            okv.Enabled = True
        End If

    End Sub
    Private Sub transferir()
        If cantidad.Value < 0 Then
            dialogs.advertencia("No puedes transferir cero artículos")
            Exit Sub
        End If
        Dim tiende As lugar = New lugar
        Dim tiena As lugar = New lugar

        tiende.load(de.Text)
        tiena.load(a.Text)
        Dim st As String = "SELECT " & tiende.idtablename & " FROM Articulos WHERE Clave='" & art.clave & "'"
        Dim orden As New OleDb.OleDbCommand(st, conart)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        Dim exist As Integer = 0
        While lector.Read
            exist = lector(tiende.idtablename)
        End While
        lector.Close()

        If cantidad.Value > exist Then
            dialogs.advertencia("No puedes transferir más cantidades de las que posee " & de.Text)
            Exit Sub
        End If
        If dialogs.pregunta("¿Desea transferir " & cantidad.Value & " unidades de  " & vbNewLine & art.clave & vbNewLine & art.descripcion & vbNewLine & "Desde " & de.Text & " a " & a.Text & "?") = DialogResult.Yes Then
            'art.transfer(cantidad.Value, tiende, tiena)
            'dialogs.ok("Transferencia ejecutada con éxito" & vbNewLine & "Recuerde que puede tardar un par de horas " & vbNewLine & "antes de reflejarse en la tienda de destino")
            'CheckBox1.Checked = False
            'actuexis()
        End If
    End Sub
    Private Sub Transfer_Click(sender As Object, e As EventArgs) Handles Transfer.Click
        transferir()
    End Sub

    Private Sub Transfer_MouseEnter(sender As Object, e As EventArgs) Handles Transfer.MouseEnter
        shownamebutton(sender)

    End Sub

    Private Sub Transfer_MouseLeave(sender As Object, e As EventArgs) Handles Transfer.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub cantidad_ValueChanged(sender As Object, e As EventArgs) Handles cantidad.ValueChanged

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label4_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel9_Paint(sender As Object, e As PaintEventArgs) Handles Panel9.Paint

    End Sub
End Class