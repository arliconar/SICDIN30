Public Class perdida
    Public arti As articuloventa = New articuloventa
    Public Sub loader(art As articuloventa)
        arti = art
        arti.cantidad = 1
        clavelabel.Text = "Clave:" & arti.clave
        Descripcion.Text = "Descripción:" & arti.descripcion
        ErrorProvider1.SetError(NumericUpDown1, "")
        ErrorProvider1.SetError(Label3, "")

        If Me.ShowDialog() = DialogResult.OK Then
            arti.brokenme(TextBox1.Text)
        End If
        Me.Close()

    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, TextBox1.LostFocus
        If TextBox1.Text <> "" Then
            okv.Enabled = True
            ErrorProvider1.SetError(Label3, "")

        Else
            okv.Enabled = False
            ErrorProvider1.SetError(Label3, "Se requiere al menos un motivo")

        End If


    End Sub

    Private Sub perdida_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If NumericUpDown1.Value > arti.existencias Then
            okv.Enabled = False
            ErrorProvider1.SetError(NumericUpDown1, "La perdida no puede ser mayor a las existencias")
        Else
            okv.Enabled = True
            ErrorProvider1.SetError(NumericUpDown1, "")
        End If
        arti.cantidad = NumericUpDown1.Value

    End Sub

    Private Sub okv_Click(sender As Object, e As EventArgs) Handles okv.Click

    End Sub
End Class