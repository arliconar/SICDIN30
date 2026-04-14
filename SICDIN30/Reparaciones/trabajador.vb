Public Class trabajadorsel
    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub
    Public Function loader() As trabajador
        Dim tra As trabajador = New trabajador
        dg1.Rows.Clear()
        Dim ds As BindingSource = New BindingSource
        orden = New OleDb.OleDbCommand("SELECT Id, Nombre FROM trabajador WHERE activo=TRUE ORDER BY Id", contra)
        lector = orden.ExecuteReader
        ds.DataSource = lector
        lector.Close()
        dg1.DataSource = ds
        dg1.Columns.Item("Id").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dg1.Columns.Item("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dg1.CurrentCell = dg1("Id", trdef - 1)


        If Me.ShowDialog() = DialogResult.OK Then
            Dim rindex As Integer = dg1.CurrentCell.RowIndex
            tra.load(dg1("Id", rindex).Value)

        End If
        loader = tra
    End Function
    Private Sub trabajadorsel_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            maintrans()

        Else
            mainnormal()

        End If
    End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click

    End Sub

    Private Sub aceptar_MouseEnter(sender As Object, e As EventArgs) Handles aceptar.MouseEnter, cancel.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub cancel_MouseLeave(sender As Object, e As EventArgs) Handles cancel.MouseLeave, aceptar.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub dg1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg1.CellContentClick

    End Sub
End Class