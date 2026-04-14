Public Class Calculadora
    Dim innumber As Boolean = True
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles num.TextChanged
        If innumber Then
            clave.Text = pre2cla(Val(num.Text))
        End If

    End Sub

    Private Sub clave_TextChanged(sender As Object, e As EventArgs) Handles clave.TextChanged
        If Not innumber Then
            num.Text = cla2pre(clave.Text)
        End If
    End Sub

    Private Sub clave_KeyPress(sender As Object, e As KeyPressEventArgs) Handles clave.KeyPress
        If InStr("EFGHIJKLMN.efghijklmn", e.KeyChar) = 0 And Asc(e.KeyChar) <> 8 Then
            e.KeyChar = Chr(0)
            e.Handled = True
        End If
    End Sub

    Private Sub num_Enter(sender As Object, e As EventArgs) Handles num.Enter
        innumber = True

    End Sub

    Private Sub clave_Enter(sender As Object, e As EventArgs) Handles clave.Enter
        innumber = False
    End Sub

    Private Sub num_KeyPress(sender As Object, e As KeyPressEventArgs) Handles num.KeyPress
        If InStr("1234567890.", e.KeyChar) = 0 And Asc(e.KeyChar) <> 8 Then
            e.KeyChar = Chr(0)
            e.Handled = True
        End If
    End Sub
End Class