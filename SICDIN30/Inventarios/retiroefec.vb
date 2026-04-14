Public Class retiroefec
    Private Sub retiroefec_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles cantidad.TextChanged

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cantidad.KeyPress
        If e.KeyChar <> Chr(13) Then
            e.KeyChar = numblock(e.KeyChar, True, False)
        Else
            Dim cant As Decimal = Val(cantidad.Text)
            If cant > 0 Then
                Me.DialogResult = DialogResult.OK

            End If
        End If


    End Sub

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles razon.TextChanged

    End Sub
End Class