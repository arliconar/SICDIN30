Public Class trabajadorform
    Public tra As trabajador = New trabajador

    Private Sub trabajadorform_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Public Sub addnew()
        tra = New trabajador
        tra.assingid()
        idlabel.Text = "Id:" & tra.id
        TextBox1.Focus()
        If Me.ShowDialog() = DialogResult.OK Then
            tra.addme()

        End If

    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles destajo.CheckedChanged
        If destajo.Checked Then
            destajotext.Visible = True
            deslabel.Visible = True
            tra.pdestajo = Val(destajotext.Text)

        Else
            destajotext.Visible = False
            deslabel.Visible = False
            tra.pdestajo = 0
        End If
        tra.destajo = destajo.Checked


    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        tra.nombre = TextBox1.Text

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        tra.direccion = TextBox2.Text

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        tra.telcasa = TextBox3.Text


    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        tra.telcel = TextBox4.Text

    End Sub

    Private Sub destajotext_ValueChanged(sender As Object, e As EventArgs) Handles destajotext.ValueChanged
        tra.pdestajo = Val(destajotext.Text)

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text = "" Then
            TextBox5.Text = 0
        End If
        tra.salario = Val(TextBox5.Text)

    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        If TextBox1.Text = "" Then
            ErrorProvider1.SetError(TextBox1, "Debe asignar un nombre")
            aceptar.Enabled = False
        Else
            ErrorProvider1.SetError(TextBox1, "")
            aceptar.Enabled = True
        End If
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        e.KeyChar = numblock(e.KeyChar, True)

    End Sub
End Class