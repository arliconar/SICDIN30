Public Class closesic
    Private Sub Cancelb_Click(sender As Object, e As EventArgs) Handles Cancelb.Click

    End Sub

    Private Sub Cancelb_MouseEnter(sender As Object, e As EventArgs) Handles Cancelb.MouseEnter, salirbut.MouseEnter, userbut.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub userbut_MouseLeave(sender As Object, e As EventArgs) Handles Cancelb.MouseLeave, salirbut.MouseLeave, userbut.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub userbut_Click(sender As Object, e As EventArgs) Handles userbut.Click

    End Sub
End Class