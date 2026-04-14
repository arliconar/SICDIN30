Public Class clntre
    Public Function searclnt(Optional tipo As Integer = 0) As Cliente
        searclnt = New Cliente
        searclnt.found = False
        With searclnt
            maintrans()
            searchclnt()
            ComboBox1.SelectedIndex = 0
            Select Case tipo
                Case 0
                    Venta.Checked = True
                    Reparacion.Checked = True
                Case 1
                    Venta.Checked = True
                    Reparacion.Checked = False
                Case 2
                    Venta.Checked = False
                    Reparacion.Checked = True
            End Select
            caja.Focus()
            If Me.ShowDialog = DialogResult.OK Then
                If dg.Rows.Count > 0 Then
                    Dim rindex As Integer = dg.CurrentCell.RowIndex
                    .Nombre = dg("nclnt", rindex).Value
                    .Direccion = dg("Dir", rindex).Value
                    .telcasa = dg("Telcasa", rindex).Value
                    .telcel = dg("Telcel", rindex).Value
                    .RFC = dg("RFC", rindex).Value
                    .found = True
                End If
            End If
        End With
        mainnormal()

    End Function
    Private Sub clntrep_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cancelar_Click(sender As Object, e As EventArgs) Handles cancelar.Click

    End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click

    End Sub
    Public Sub searchclnt()
        Dim orden As OleDb.OleDbCommand
        Dim lector As OleDb.OleDbDataReader
        Dim st As String
        Dim i As Integer = 0
        dg.Rows.Clear()

        If caja.Text <> "" Then

            caja.Text = caja.Text.Replace("'", "")
            Dim stwhere As String = ""
            If Venta.Checked Then
                Select Case ComboBox1.SelectedIndex
                    Case 0
                        stwhere = "Nombre LIKE '%" & caja.Text & "%' AND (telcel IS NOT NULL OR telcasa IS NOT NULL OR RFC IS NOT NULL)"
                    Case 1
                        stwhere = "telcel='" & caja.Text & "' or telcasa='" & caja.Text & "' AND RFC IS NOT NULL"

                    Case 2
                        stwhere = "RFC='" & caja.Text & "' AND (telcel IS NOT NULL OR telcasa IS NOT NULL OR RFC IS NOT NULL)"
                End Select
                st = "SELECT DISTINCT * FROM Clientes WHERE " & stwhere
            orden = New OleDb.OleDbCommand(st, convent)
            lector = orden.ExecuteReader

            While lector.Read

                dg.Rows.Add()
                dg("nclnt", i).Value = lector("Nombre")
                dg("Telcel", i).Value = lector("telcel")
                dg("Telcasa", i).Value = lector("telcasa")
                dg("Dir", i).Value = lector("Dirección")
                dg("RFC", i).Value = lector("RFC")
                dg("Base", i).Value = "Venta"
                i += 1
                aceptar.Enabled = True


            End While
            lector.Close()

            End If
            If Reparacion.Checked Then

                Select Case ComboBox1.SelectedIndex
                    Case 0
                        stwhere = "Cliente LIKE '%" & caja.Text & "%' AND (telcel IS NOT NULL OR telcasa IS NOT NULL OR RFC IS NOT NULL)"
                    Case 1
                        stwhere = "telcel='" & caja.Text & "' or telcasa='" & caja.Text & "' AND RFC IS NOT NULL"

                    Case 2
                        stwhere = "RFC='" & caja.Text & "' AND (telcel IS NOT NULL OR telcasa IS NOT NULL OR RFC IS NOT NULL)"
                End Select

                st = "SELECT DISTINCT * FROM nota WHERE " & stwhere
                orden = New OleDb.OleDbCommand(st, conbicrep)
                lector = orden.ExecuteReader
                aceptar.Enabled = False


                While lector.Read
                    dg.Rows.Add()
                    dg("nclnt", i).Value = lector("Cliente")
                    dg("Telcel", i).Value = lector("telcel")
                    dg("Telcasa", i).Value = lector("telcasa")
                    dg("Dir", i).Value = lector("Direccion")
                    dg("RFC", i).Value = lector("RFC")
                    dg("Base", i).Value = "Reparación"
                    i += 1
                    aceptar.Enabled = True
                End While
                lector.Close()
            End If

        End If




    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles caja.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        searchclnt()
    End Sub

    Private Sub nombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles caja.KeyPress
        If e.KeyChar = Chr(13) Then
            searchclnt()
        End If
    End Sub

    Private Sub dg_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg.CellContentClick

    End Sub

    Private Sub dg_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg.CellContentDoubleClick
        If dg.Rows.Count > 0 Then
            Me.DialogResult = DialogResult.OK

        End If
    End Sub

    Private Sub dg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dg.KeyPress
        If e.KeyChar = Chr(13) Then
            If dg.Rows.Count > 0 Then
                Me.DialogResult = DialogResult.OK

            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Venta.CheckedChanged
        If Venta.Checked = False Then
            If Reparacion.Checked = False Then
                Venta.Checked = True

            End If
        End If
    End Sub

    Private Sub Reparacion_CheckedChanged(sender As Object, e As EventArgs) Handles Reparacion.CheckedChanged
        If Reparacion.Checked = False Then
            If Venta.Checked = False Then
                Reparacion.Checked = True
            End If
        End If
    End Sub
End Class