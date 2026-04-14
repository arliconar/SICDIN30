Public Class addexist
    Public articulo As articuloventa
    Public Function loader(clave As String) As Boolean
        articulo = New articuloventa
        With articulo
            .load(clave, True)
            clavelabel.Text = "Clave:" & .clave
            deslabel.Text = "Descripción:" & .descripcion
            If .existencias < 0 Then
                existencias.Value = 0
            Else
                existencias.Value = .existencias
            End If

        End With
        existencias.Focus()

        If Me.ShowDialog = DialogResult.OK Then
            loader = True
            articulo.modexistencias()
        Else
            loader = False
        End If

    End Function
    Private Sub addexist_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click

    End Sub

    Private Sub existencias_ValueChanged(sender As Object, e As EventArgs) Handles existencias.ValueChanged
        articulo.existencias = existencias.Value

    End Sub

    Private Sub existencias_KeyPress(sender As Object, e As KeyPressEventArgs) Handles existencias.KeyPress
        If e.KeyChar = Chr(13) Then
            Me.DialogResult = DialogResult.OK

        End If
    End Sub

    Private Sub addexist_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        existencias.Focus()

    End Sub
End Class