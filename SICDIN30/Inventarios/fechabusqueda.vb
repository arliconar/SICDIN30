Public Class fechabusqueda
    Public st As String = ""
    Public st2 As String = ""
    Public Function loader(ByRef fechast As String) As String
        stringfecha()

        If Me.ShowDialog = DialogResult.OK Then
            loader = st
        Else
            loader = ""
            st2 = ""
        End If
        fechast = ":" & st2
    End Function
    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles calendario.DateChanged
        If calendario.SelectionRange.Start > Today Then
            ErrorProvider1.SetError(calendario, "No puedes seleccionar un día futuro")
            aceptar.Enabled = False

        Else
            ErrorProvider1.SetError(calendario, "")
            aceptar.Enabled = True
            stringfecha()
        End If

    End Sub
    Private Sub stringfecha()
        If fum.Checked Then
            st = "AND FUM "
            st2 = "Mod."
        Else
            st = "AND FUC "
            st2 = "Venta"
        End If
        If menor.Checked Then
            st &= "<="
            st2 &= "≤"
        Else
            st &= ">="
            st2 &= "≥"
        End If
        st &= "#" & onlyfechasql(calendario.SelectionRange.Start) & "# "
        st2 &= onlyfechasql(calendario.SelectionRange.Start)
    End Sub
    Private Sub fechabusqueda_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles menor.CheckedChanged
        stringfecha()
    End Sub

    Private Sub fum_CheckedChanged(sender As Object, e As EventArgs) Handles fum.CheckedChanged
        stringfecha()
    End Sub

    Private Sub fuc_CheckedChanged(sender As Object, e As EventArgs) Handles fuc.CheckedChanged
        stringfecha()
    End Sub

    Private Sub mayor_CheckedChanged(sender As Object, e As EventArgs) Handles mayor.CheckedChanged
        stringfecha()
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub
End Class