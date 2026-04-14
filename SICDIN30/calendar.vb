Public Class calendar
    Function loader(ByRef dia As Date, Optional dayplus As Integer = 0) As Boolean
        loader = False
        dia = Today.AddDays(dayplus)
        calendario.Value = dia
        If Me.ShowDialog = DialogResult.OK Then
            dia = calendario.Value.Date
            loader = True
        End If
        Me.Close()

    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub calendario_ValueChanged(sender As Object, e As EventArgs) Handles calendario.ValueChanged
        If calendario.Value < Today Then
            aceptar.Enabled = False
            ErrorProvider1.SetError(aceptar, "La fecha no puede ser menor al día de hoy" & Chr(13) & "A menos que seas Marty Mcfly")
        Else
            aceptar.Enabled = True
            ErrorProvider1.SetError(aceptar, "")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles aceptar.Click

    End Sub

    Private Sub aceptar_MouseEnter(sender As Object, e As EventArgs) Handles aceptar.MouseEnter, cancelar.MouseEnter
        shownamebutton(sender)

    End Sub

    Private Sub cancelar_MouseLeave(sender As Object, e As EventArgs) Handles cancelar.MouseLeave, aceptar.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class