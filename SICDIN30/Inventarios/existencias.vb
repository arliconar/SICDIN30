Imports System.Data.OleDb

Public Class existencias
    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Public Sub cargarperdidas() Handles recargar.Click
        perd.DataSource = Nothing

        Dim st As String = "SELECT Fecha,Clave,Descripcion,Cantidad,Motivo,Usuario FROM Perdidas"
        Dim orden As OleDbCommand = New OleDbCommand(st, conart)
        Dim ds As BindingSource = New BindingSource
        Dim lector As OleDbDataReader = orden.ExecuteReader
        If lector.HasRows Then
            ds.DataSource = lector
            perd.DataSource = ds
            lector.Close()
            perd.Columns.Item("Descripcion").HeaderText = "Descripción"
            perd.Columns.Item("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            perd.Columns.Item("Motivo").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            perd.Columns.Item("Fecha").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            perd.Columns.Item("Usuario").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            perd.Columns.Item("Ver").Visible = True
            perd.Columns.Item("Borrar").Visible = True

        Else
            perd.Columns.Item("Ver").Visible = False
            perd.Columns.Item("Borrar").Visible = False


        End If


    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        dgdellrow(sender)
    End Sub

    Private Sub existencias_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub


    Private Sub Cancelar_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub dgexis_KeyPress(sender As Object, e As KeyPressEventArgs)
    End Sub

    Private Sub dgexis_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs)
    End Sub

    Private Sub existencias_MaximumSizeChanged(sender As Object, e As EventArgs) Handles Me.MaximumSizeChanged

    End Sub

    Private Sub existencias_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

    End Sub

    Private Sub dgexis_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub existencias_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

    End Sub

    Private Sub dgexis_KeyUp(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub dgexis_CellEnter(sender As Object, e As DataGridViewCellEventArgs)


    End Sub


    Private Sub dgexis_CellLeave(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles dardealta.Click
        altaarticulo.loader()

    End Sub
    Public Sub cargarfaltantes() Handles recargar.Click
        falt.Rows.Clear()
        Dim orden As OleDbCommand
        Dim lector As OleDb.OleDbDataReader
        Dim st As String = "SELECT Clave, Descripción,Existencias,stock,stock-Existencias FROM Articulos WHERE  Existencias<stock"
        Dim ds As BindingSource = New BindingSource
        orden = New OleDbCommand(st, conart)
        lector = orden.ExecuteReader

        If lector.HasRows Then
            ds.DataSource = lector
            falt.DataSource = ds
            falt.Columns.Item("Descripción").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            falt.Columns.Item("Clave").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            falt.Columns.Item(4).HeaderText = "Faltantes"
            falt.Columns.Item("Existencias").HeaderText = tienda.nombre
            falt.SelectAll()
        End If
        lector.Close()

    End Sub
    Private Sub Aceptar_Click(sender As Object, e As EventArgs) Handles recargar.Click


    End Sub

    Private Sub dgexis_CellToolTipTextChanged(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub dgexis_CellValidated(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub dardealta_MouseEnter(sender As Object, e As EventArgs) Handles dardealta.MouseEnter, recargar.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub dardealta_MouseLeave(sender As Object, e As EventArgs) Handles dardealta.MouseLeave, recargar.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub falt_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub falt_MouseWheel(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub cargarfaltantes(sender As Object, e As EventArgs) Handles recargar.Click

    End Sub

    Private Sub Copiarp_Click(sender As Object, e As EventArgs) Handles Copiarp.Click
        If falt.Rows.Count > 0 Then
            Dim stc As String = ""
            stc = "Faltantes" & vbTab & "Clave" & vbTab & "Descripción" & vbNewLine
            For i = 0 To falt.Rows.Count - 1
                If falt.Rows.Item(i).Selected Then
                    stc &= (Val(falt("stock", i).Value) - Val(falt("Existencias", i).Value)) & vbTab & falt("Clave", i).Value & vbTab & falt("Descripción", i).Value & vbNewLine
                End If
            Next
            Clipboard.SetText(stc)
        End If
    End Sub

    Private Sub SeleccionaTodoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SeleccionaTodoToolStripMenuItem.Click
        falt.SelectAll()

    End Sub

    Private Sub panelexis_Paint(sender As Object, e As PaintEventArgs) Handles panelexis.Paint

    End Sub

    Private Sub perd_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles perd.CellContentClick
        If e.ColumnIndex = perd.Columns.Item("Borrar").Index Then
            dialogs.Close()

            If dialogs.pregunta("¿Desea borrar este registro de perdida?") = DialogResult.Yes Then
                Dim st As String = "DELETE FROM Perdidas WHERE Fecha=#" & fechasql(perd("Fecha", e.RowIndex).Value) & "#"
                Dim orden As OleDbCommand = New OleDbCommand(st, conart)
                orden.ExecuteNonQuery()
                cargarperdidas()

            End If
        End If
    End Sub
    Public Sub buscar()
        Dim st As String = "SELECT * FROM Lognube WHERE "
        Dim b As Boolean = False
        dglog.Rows.Clear()

        If Not (agregar.Checked Or cambio.Checked Or del.Checked Or ext.Checked) Then
            Exit Sub
        End If

        If agregar.Checked Then
            st &= " Tipo='Nuevo'"
            b = True
        End If
        If cambio.Checked Then
            If b Then
                st &= " OR"
            End If
            st &= " Tipo='Cambio'"
            b = True
        End If
        If del.Checked Then
            If b Then
                st &= " OR"
            End If
            st &= " Tipo='Borrar'"
            b = True
        End If
        If ext.Checked Then
            If b Then
                st &= " OR"
            End If
            st &= " Tipo='Existencias'"
            b = True
        End If
        If b Then
            st &= " AND "
        End If
        st &= "Fecha>=#" & onlyfechasql(dtlog.Value) & "#"

        Dim orden As OleDbCommand = New OleDbCommand(st, consys)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        If Not lector.HasRows Then
            Exit Sub
        End If
        Dim ds As BindingSource = New BindingSource
        ds.DataSource = lector
        dglog.DataSource = ds
        lector.Close()


    End Sub
    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles Button1.Click
        buscar()
    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        shownamebutton(sender)

    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        hidenamebutton(sender)
    End Sub
End Class