Public Class artnot
    Dim ste As String = ""
    Public Sub loaderart()
        titulo.Text = "Introduzca el artículo no registrado"
        des.Text = ""
        ste = "Debe introducir el artículo no registrado"
        precio.Text = 0
        aceptar.Enabled = False
        des.Focus()



    End Sub

    Public Sub loadertra()
        des.Text = ""
        precio.Text = 0
        titulo.Text = "Introduzca el trabajo no registrado"
        ste = "Debe introducir el trabajo no registrado"
        aceptar.Enabled = False
        des.Focus()


    End Sub
    'Public Function loader(tipo As Integer) As articuloventa
    '    With loader
    '        .clave = "ARNORE"
    '    End With

    'End Function
    Private Sub artnot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler precio.KeyPress, AddressOf textnumonlypoint
    End Sub

    Private Sub precio_TextChanged(sender As Object, e As EventArgs) Handles precio.TextChanged

    End Sub

    Private Sub precio_LostFocus(sender As Object, e As EventArgs) Handles precio.LostFocus
        If precio.Text = "" Then
            precio.Text = 0
        End If
    End Sub
    Private Sub eval()
        If des.Text = "" Then
            ErrorProvider1.SetError(des, ste)
            des.Focus()
        Else
            ErrorProvider1.SetError(des, "")

        End If
    End Sub
    Private Sub des_TextChanged(sender As Object, e As EventArgs) Handles des.TextChanged
        If des.Text <> "" Then
            aceptar.Enabled = True
        Else
            aceptar.Enabled = False
        End If
    End Sub

    Private Sub des_LostFocus(sender As Object, e As EventArgs) Handles des.LostFocus
        eval()

    End Sub

    Private Sub des_KeyUp(sender As Object, e As KeyEventArgs) Handles des.KeyUp, precio.KeyUp
        'Select Case e.KeyCode
        '    Case Keys.Enter
        '        eval()
        '        If des.Text <> "" Then
        '            Me.DialogResult = DialogResult.OK
        '        End If
        'End Select
    End Sub

    Private Sub artnot_Shown(sender As Object, e As EventArgs) Handles Me.Shown


    End Sub

    Private Sub artnot_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            maintrans()
        Else
            mainnormal()
        End If
    End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click

    End Sub

    Private Sub MaskedTextBox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles paquete.MaskInputRejected

    End Sub

    Private Sub MaskedTextBox1_LostFocus(sender As Object, e As EventArgs) Handles paquete.LostFocus
        If paquete.Text = "" Then
            paquete.Text = "PZA"
        End If
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub
End Class