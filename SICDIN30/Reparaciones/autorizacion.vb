Public Class autorizacion
    Public Function loader(clienter As String) As Boolean
        cliente.Text = clienter
        notas.Text = ""
        medioaut.SelectedIndex = 0
        maintrans()
        If Me.ShowDialog() = DialogResult.OK Then
            loader = True
        Else
            loader = False
        End If
        mainnormal()

    End Function
    Private Sub autorizacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cliente_TextChanged(sender As Object, e As EventArgs) Handles cliente.TextChanged, notas.TextChanged

    End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click
        If notas.Text = "" And cliente.Text = "" Then
            dialogs.Close()

            dialogs.advertencia("Debe especificar al menos el cliente o una nota")
            maintrans()
        Else
            Me.DialogResult = DialogResult.OK

        End If
    End Sub
End Class