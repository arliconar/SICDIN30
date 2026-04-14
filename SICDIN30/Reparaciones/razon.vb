Public Class razon
    Public Function loadercancel() As DialogResult
        razont.Text = ""
        Label2.Text = "Introduzca la razón de la cancelación:"
        maintrans()
        loadercancel = Me.ShowDialog
        mainnormal()
    End Function
    Public Function loader() As DialogResult
        razont.Text = ""
        Label2.Text = "Introduzca la razón de almacenamiento:"
        maintrans()
        loader = Me.ShowDialog
        mainnormal()


    End Function
    Public Sub loaderrazon(razon As String)
        razont.Text = razon
        Label2.Text = "Razón"
        razont.ReadOnly = True
        maintrans()
        cancel.Hide()
        Me.ShowDialog()
        razont.ReadOnly = False
        cancel.Show()
        mainnormal()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles razont.TextChanged

    End Sub

    Private Sub pagoacuenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Sub pago_MarginChanged(sender As Object, e As EventArgs) Handles razont.MarginChanged

    End Sub

    Private Sub pago_LostFocus(sender As Object, e As EventArgs) Handles razont.LostFocus
    End Sub

    Private Sub pago_KeyPress(sender As Object, e As KeyPressEventArgs) Handles razont.KeyPress

    End Sub

    Private Sub pago_KeyUp(sender As Object, e As KeyEventArgs) Handles razont.KeyUp

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles aceptar.Click

    End Sub
End Class