Public Class foundplus
    Public dger As BindingSource

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub
    Public Function loader(ByVal dge As Object, Optional ByVal cancelbool As Boolean = False) As Integer
        dger = dge
        dg.DataSource = dger
        dg.Columns.Item("clave").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        dg.Columns.Item("descripción").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        If cancelbool = False Then
            cancelar.Visible = False
            aceptar.Dock = DockStyle.Fill
            sear.Hide()
        End If
        If Me.ShowDialog() = DialogResult.OK Then
            loader = dg.CurrentCell.RowIndex
        Else
            loader = -1

        End If

        Me.Close()


    End Function

    Private Sub dg_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg.CellContentClick
        If e.ColumnIndex = dg.Columns.Item("Clave").Index - 1 Then
            Visor.loader(dg("Clave", dg.CurrentCell.RowIndex).Value)
        End If


    End Sub

    Private Sub dg_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg.CellContentDoubleClick
        Me.DialogResult = DialogResult.OK

    End Sub

    Private Sub dg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dg.KeyPress

    End Sub

    Private Sub dg_KeyUp(sender As Object, e As KeyEventArgs) Handles dg.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.Handled = True

            Me.DialogResult = DialogResult.OK

        End If
    End Sub

    Private Sub foundplus_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub


End Class