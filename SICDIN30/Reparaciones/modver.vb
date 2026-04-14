Public Class modver
    Public Sub loader(cliente As String, nota As String, descripcion As String)
        Clientever.Text = cliente
        Notaver.Text = nota
        des.Text = descripcion
        maintrans()
        Me.ShowDialog()
        mainnormal()



    End Sub
    Private Sub modver_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub des_TextChanged(sender As Object, e As EventArgs) Handles des.TextChanged

    End Sub

    Private Sub des_KeyPress(sender As Object, e As KeyPressEventArgs) Handles des.KeyPress, Clientever.KeyPress, Notaver.KeyPress
        e.KeyChar = ""
    End Sub
End Class