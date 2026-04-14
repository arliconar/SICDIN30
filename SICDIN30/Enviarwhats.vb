Public Class Enviarwhats
    Private Sub numero_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles numero.MaskInputRejected

    End Sub

    Private Sub numero_Leave(sender As Object, e As EventArgs) Handles numero.Leave

    End Sub

    Private Sub Enviarwhats_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Enviarwhats_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
        If numero.Text.Length = 10 Then
            aceptar.Enabled = True
        Else
            aceptar.Enabled = False
        End If
    End Sub

    Private Sub numero_KeyPress(sender As Object, e As KeyPressEventArgs) Handles numero.KeyPress
        If e.KeyChar = Chr(13) Then
            If numero.Text.Length = 10 Then
                Me.DialogResult = DialogResult.OK

            End If
        ElseIf e.KeyChar = chr(27) Then
            Me.DialogResult = DialogResult.Cancel

        End If

    End Sub
End Class