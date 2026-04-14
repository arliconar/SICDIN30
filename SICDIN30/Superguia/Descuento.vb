Public Class superdesc
    Private pag As pagos
    Private estatus As Integer
    Public Function loader(total As Decimal) As pagos
        pag = New pagos
        AddHandler TextBox1.KeyPress, AddressOf textnumonlypoint
        Label3.Text = "Total=$" & total
        estatus = 0
        pag.total = total
        If Me.ShowDialog() = DialogResult.Cancel Then
            pag = New pagos
        End If


        loader = pag
        Me.Close()

    End Function

    Private Sub Descuento_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles nex.Click
        pag.calculatedescuento()
        Select Case estatus

            Case 0
                pag.efectivo = Val(TextBox1.Text)
                estatus = 1
                TextBox1.Text = pag.tarjeta
                Label1.Text = "Pago con tarjeta"

            Case 1
                pag.tarjeta = Val(TextBox1.Text)
                estatus = 2
                TextBox1.Text = pag.transferencia
                Label1.Text = "Pago con transferencia"
            Case 2
                pag.transferencia = Val(TextBox1.Text)
                estatus = 3
                TextBox1.Text = pag.descuento
                Label1.Text = "Descuento"
                nex.Text = ""
                nex.BackgroundImage = SICDIN30.My.Resources.Resources.vcsnormal_93488

            Case 3
                If pag.total = (pag.descuento + pag.efectivo + pag.tarjeta + pag.transferencia) Then
                    ErrorProvider1.SetError(Label3, "")

                    Me.DialogResult = DialogResult.OK
                Else
                    ErrorProvider1.SetError(Label3, "Los pagos y descuentos no concuerdan con el total")

                    Beep()

                End If
        End Select
    End Sub

    Private Sub back_Click(sender As Object, e As EventArgs) Handles back.Click
        pag.calculatedescuento()
        Select Case estatus

            Case 1
                pag.tarjeta = Val(TextBox1.Text)
                estatus = 0
                TextBox1.Text = pag.efectivo
                Label1.Text = "Pago en efectivo"
            Case 2
                pag.transferencia = Val(TextBox1.Text)
                estatus = 1
                TextBox1.Text = pag.tarjeta
                Label1.Text = "Pago con tarjeta"
            Case 3
                pag.descuento = Val(TextBox1.Text)
                estatus = 2
                TextBox1.Text = pag.transferencia
                Label1.Text = "Pago en transferencia"
                nex.BackgroundImage = Nothing
                nex.Text = ">"

        End Select

    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        TextBox1.Text = Val(TextBox1.Text)
    End Sub
End Class