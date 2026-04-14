Public Class cierrededia
    Public sobra As Decimal = 0
    Dim totalefe As Decimal

    Public Sub cerrardia(efe As Decimal, retiros As Decimal, tarjeta As Decimal, trans As Decimal)
        totalefe = diaventa.Eapertura + efe - retiros
        apertura.Text = diaventa.Eapertura
        ventas.Text = efe
        retiro.Text = retiros
        totalefec.Text = totalefe
        totaltarjeta.Text = tarjeta
        sobra = 0
        maintrans()
        contar()
        If Me.ShowDialog = DialogResult.OK Then
            Dim total As Decimal = efe + tarjeta + trans
            Dim stemoji As String = ""

            If total < 4000 Then
                stemoji = "😔"
            End If
            If (total >= 4000) And (total < 10000) Then
                stemoji = "😀"
            End If
            If (total >= 10000) Then
                stemoji = "😁"
            End If

            Dim stw As String = "Hoy " & Now.ToShortDateString & " " & tienda.nombre & " vendió $" & total & " " & stemoji
            If totalefe > 0 Then
                stw &= " 💵Efectivo=$" & efe
            End If
            If tarjeta > 0 Then
                stw &= "  💳Tarjeta=$" & tarjeta
            End If
            If trans > 0 Then
                stw &= "  📲Transferencias=$" & trans
            End If
            If retiros > 0 Then
                stw &= " 💸 Retiros=$" & retiros
            End If

            sendtowhats(tienda.telenc, stw)
            diaventa.cerrardia(user, Val(tefeccierre.Text), tarjeta, sobra)
            mainnormal()

            backinup()
        Else
            mainnormal()

            Exit Sub
        End If

    End Sub
    Public Function cerrardiaatrasado(fechas As Date) As Decimal
        Dim diaante As diatrabajo = New diatrabajo
        diaante.load(fechas)

        Dim efe As Decimal
        Dim retiros As Decimal = 0
        Dim tarjeta As Decimal = 0
        Dim tcred As Decimal = 0
        Dim tdeb As Decimal = 0
        Dim pagado As Decimal = 0
        diaante.getventas(efe, tcred, tdeb, pagado, retiros)
        tarjeta = tcred + tdeb
        totalefe = diaante.Eapertura + efe - retiros
        apertura.Text = diaante.Eapertura
        ventas.Text = efe
        retiro.Text = retiros
        totalefec.Text = totalefe
        totaltarjeta.Text = tarjeta
        sobra = 0
        maintrans()

        cancelar.Enabled = False
        If Me.ShowDialog = DialogResult.OK Then

            diaante.cerrardia(user, Val(tefeccierre.Text), tarjeta, sobra)
            mainnormal()



        Else
            mainnormal()

        End If
        cerrardiaatrasado = Val(tefeccierre.Text)
    End Function
    Private Sub contar()
        Dim efectivo As Decimal = 0
        efectivo += Val(t1000.Text) * 1000
        efectivo += Val(t500.Text) * 500
        efectivo += Val(t200.Text) * 200
        efectivo += Val(t100.Text) * 100
        efectivo += Val(t50.Text) * 50
        efectivo += Val(t20.Text) * 20
        efectivo += Val(t10.Text) * 10
        efectivo += Val(t5.Text) * 5
        efectivo += Val(t2.Text) * 2
        efectivo += Val(t1.Text)
        efectivo += Val(t50c.Text) * 0.5
        efectivo += Val(t20c.Text) * 0.2
        efectivo += Val(t10c.Text) * 0.1

        tefeccierre.Text = efectivo








    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles tefeccierre.TextChanged
        sobra = Math.Round(Val(tefeccierre.Text) - totalefe, 2)

        sval.Text = sobra
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tefeccierre.KeyPress
        e.KeyChar = numblock(e.KeyChar, True, False)

    End Sub

    Private Sub cierrededia_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Sub okv_Click(sender As Object, e As EventArgs) Handles okv.Click

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub b100_Click(sender As Object, e As EventArgs) Handles b1000.Click

    End Sub

    Private Sub m10_Click(sender As Object, e As EventArgs) Handles m10.Click


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.Size.Height = 405 Then
            Me.Size = New Size(Me.Width, 705)
        Else
            Me.Size = New Size(Me.Width, 405)

        End If
        Me.CenterToScreen()

    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs)
        ''     contar()

    End Sub

    Private Sub t1000_ValueChanged(sender As Object, e As EventArgs) Handles t1000.ValueChanged

    End Sub

    Private Sub t1000_TextChanged(sender As Object, e As EventArgs) Handles t1000.TextChanged, t500.TextChanged, t200.TextChanged, t100.TextChanged, t50.TextChanged, t20.TextChanged, t10.TextChanged, t5.TextChanged, t2.TextChanged, t1.TextChanged, t50c.TextChanged, t20c.TextChanged, t10c.TextChanged
        contar()

    End Sub

    Private Sub tefeccierre_LostFocus(sender As Object, e As EventArgs) Handles tefeccierre.LostFocus
        If tefeccierre.Text = "" Then
            tefeccierre.Text = 0
        End If
    End Sub

    Private Sub t1000_LostFocus(sender As Object, e As EventArgs) Handles t1000.LostFocus, t500.LostFocus, t200.LostFocus, t100.LostFocus, t50.LostFocus, t20.LostFocus, t10.LostFocus, t5.LostFocus, t1.LostFocus, t50c.LostFocus, t20c.LostFocus, t10c.LostFocus
        If sender.text = "" Then
            sender.text = 0
        End If
    End Sub
End Class