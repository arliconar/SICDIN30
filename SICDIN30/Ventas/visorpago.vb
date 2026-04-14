Public Class visorpago
    Public Sub loaderapartado(folio As String)
        Dim st As String = "SELECT * FROM Pagoapartado WHERE Folio='" & folio & "'"
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, convent)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        lista.Items.Clear()
        Dim i As Integer = 0
        While lector.Read
            i += 1
            With lista.Items
                If i > 1 Then
                    .Add("------------------------")

                End If
                Dim fecha As Date = lector("Fecha")
                .Add("Pago " & i & " el " & fecha.ToShortDateString() & " a las " & fecha.ToShortTimeString())
                .Add("Usuario" & lector("Usuario"))
                If Val(lector("Efectivo")) > 0 Then
                    .Add("Efectivo=$" & lector("Efectivo"))
                End If
                If Val(lector("Tarjeta")) > 0 Then
                    .Add("Tarjeta=$" & lector("Tarjeta"))
                End If
                If Val(lector("Transferencia")) > 0 Then
                    .Add("Trabsferencia=$" & lector("Transferencia"))
                End If
                If Val(lector("Descuento")) > 0 Then
                    .Add("Descuento=$" & lector("Descuento"))
                End If

            End With
        End While
        foliol.Text = folio
        idlt.Hide()
        idl.Hide()
        fechal.Hide()
        Label2.Hide()
        Label4.Hide()
        userl.Hide()
        Me.ShowDialog()

    End Sub
    Public Sub loaderpago(folio As String, usuario As String, pago As pagos, Optional id As Integer = 0)
        Dim total As Decimal = 0
        Dim fecha As Date = pago.fecha
        Dim i As Integer = 0
        If id = 0 Then
            idl.Visible = False
            idlt.Visible = False
        Else
            idl.Text = id
            idl.Visible = True
            idlt.Visible = True
        End If
        lista.Items.Clear()
        foliol.Text = folio
        fechal.Text = fecha.ToShortDateString & " " & fecha.ToShortTimeString
        userl.Text = usuario
        If pago.efectivo > 0 Then
            i += 1
            lista.Items.Add(i & ".-Efectivo:$" & pago.efectivo)
            total += pago.efectivo
        End If
        If pago.transferencia > 0 Then
            i += 1
            lista.Items.Add(i & ".-T.Credito:$" & pago.transferencia)
            total += pago.transferencia
        End If
        If pago.tarjeta > 0 Then
            i += 1
            lista.Items.Add(i & ".-T.Debito:$" & pago.tarjeta)
            total += pago.tarjeta
        End If
        If pago.apartado > 0 Then
            i += 1
            lista.Items.Add(i & ".-Apartado:$" & pago.apartado)
            total += pago.apartado
        End If
        lista.Items.Add("Total=$" & total)
        Me.ShowDialog()

    End Sub

    Private Sub lista_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lista.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub visorpago_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class