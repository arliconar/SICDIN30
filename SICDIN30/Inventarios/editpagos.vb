Public Class editpagos
    Public total As Decimal
    Public totalcam As Decimal
    Public Function loader(efec As Decimal, tarjeta As Decimal, trans As Decimal) As DialogResult

        total = efec + tarjeta + trans
        totall.Text = "Total:$" & total
        totalcam = total
        efectivo.Text = efec
        Credito.Text = tarjeta
        Debito.Text = trans
        Return Me.ShowDialog()

    End Function
    Private Sub count()
        totalcam = Val(efectivo.Text) + Val(Credito.Text) + Val(Debito.Text)
        If totalcam = total Then
            aceptar.Enabled = True
            adv.Visible = False
        Else
            aceptar.Enabled = False
            adv.Visible = True

        End If
    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles efectivo.TextChanged, Credito.TextChanged, Debito.TextChanged
        count()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles efectivo.KeyPress, Credito.KeyPress, Debito.KeyPress
        e.KeyChar = numblock(e.KeyChar, True, False)

    End Sub

    Private Sub editpagos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click

    End Sub

    Private Sub Debito_Click(sender As Object, e As EventArgs) Handles Debitol.Click

    End Sub

    Private Sub efectivo_LostFocus(sender As Object, e As EventArgs) Handles efectivo.LostFocus, Credito.LostFocus, Debito.LostFocus
        If sender.text = "" Then
            sender.text = 0
        End If
    End Sub

    Private Sub adv_Click(sender As Object, e As EventArgs) Handles adv.Click

    End Sub
End Class