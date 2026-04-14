Public Class Ventarapid
    Dim arti As articuloventa = New articuloventa
    Dim Nota As notaventa = New notaventa
    Dim totales As Decimal = 0
    Public Sub loader(clavs As String)
        arti = New articuloventa
        arti.load(clavs, True)
        clave.Text = "Clave:" & arti.clave
        descripcion.Text = "Descripción:" & arti.descripcion
        Precio.Text = "Precio:$" & arti.menudeo
        total.Text = "Total:$" & arti.menudeo
        totales = arti.menudeo
        arti.cantidad = 1
        checker()
        Nota.clnt.loadmostrador()
        Nota.fecha = Now
        If Me.ShowDialog = vbOK Then
            arti.cantidad = Val(cantidad.Text)
            Nota = New notaventa
            Nota.articulos.addart(arti)
            Nota.total = totales

            If pagof.loader(Nota, totales,, False) = vbOK Then
                Nota.pago.loadfromdg(pagof.dgpago, False, totales)
                Nota.addventarapida()

            End If
        End If
        pagof.Close()
        Me.Close()

    End Sub
    Private Sub Ventarapid_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cantidad_TextChanged(sender As Object, e As EventArgs) Handles cantidad.TextChanged
        checker()
    End Sub
    Private Function checker() As Boolean
        checker = False
        totales = Math.Round(Val(cantidad.Text) * arti.menudeo, 2)
        total.Text = "Total:$" & totales
        If Val(cantidad.Text) = 0 Then
            ErrorProvider1.SetError(cantidad, "Debe vender al menos un artículo")
            aceptar.Enabled = False

        Else
            If Val(cantidad.Text) > arti.existencias And arti.einf = False And arti.tipo <> "Trabajo" Then
                ErrorProvider1.SetError(cantidad, "No hay suficientes existencias")
                aceptar.Enabled = False

            Else
                ErrorProvider1.SetError(cantidad, "")
                aceptar.Enabled = True
                checker = True
            End If

        End If
    End Function
    Private Sub cantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cantidad.KeyPress

        e.KeyChar = numblock(e.KeyChar, True, False, sender.text)

    End Sub
    Private Sub cantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles cantidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            If checker() Then
                Me.DialogResult = DialogResult.OK
            End If
        End If
    End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click

    End Sub
End Class