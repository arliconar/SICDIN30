Public Class clientes
    Dim clnt As Cliente

    Dim found As Boolean
    Public Sub loader()

        dg.DataSource = loadclntdg(sbox.Text, found)
        AddHandler nmov.KeyPress, AddressOf textnumonly

        resizedg()


    End Sub
    Private Sub resizedg()
        If dg.Rows.Count > 1 Then
            dg.Columns.Item("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        End If



    End Sub
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub sbox_TextChanged(sender As Object, e As EventArgs) Handles sbox.TextChanged
        Dim ds As BindingSource = New BindingSource
        ds.DataSource = loadclntdg(sbox.Text, found)


        If found Then
            dg.DataSource = ds
            sbox.BackColor = SystemColors.InactiveCaption
            sbox.ForeColor = Color.Black

        Else
            sbox.BackColor = Color.Red
            sbox.ForeColor = Color.White

        End If



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles delclnt.Click
        If dg.Rows.Count > 0 Then
            Dim rindex As Integer = dg.CurrentCell.RowIndex
            dialogs.Close()

            If rindex > 0 Then
                If dialogs.pregunta("¿Deseas eliminar a este cliente?" & Chr(13) & dg("Id", rindex).Value & ":" & dg("Nombre", rindex).Value) = DialogResult.Yes And dg.Rows.Count > 0 Then
                    Dim clnt As Cliente = New Cliente(dg("Id", rindex).Value)
                    clnt.deleteme(False, True)
                    sbox.Text = ""
                    dg.DataSource = loadclntdg("", found)
                End If
            Else
                dialogs.advertencia("No puedes eliminar al Mostrador")
            End If

        End If

    End Sub

    Private Sub Agregar_Click(sender As Object, e As EventArgs) Handles Agregar.Click
        clienteform.addclnt("")
        sbox.Text = ""
        dg.DataSource = loadclntdg("", found)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        dialogs.Close()

        If dg.Rows.Count > 0 Then
            Dim rindex As Integer = dg.CurrentCell.RowIndex
            If dg("id", rindex).Value > 1 Then
                Dim clnt As Cliente = New Cliente
                clnt.loadclnt(dg("Id", rindex).Value)
                clienteform.ver(clnt)
            Else
                dialogs.advertencia("No se pueden ver los datos del Mostrador")
            End If


        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles modclnt.Click
        If dg.Rows.Count > 0 Then
            Dim rindex As Integer = dg.CurrentCell.RowIndex
            If dg("id", rindex).Value > 1 Then
                clienteform.modclnt(dg("id", rindex).Value)
                sbox.Text = ""
                dg.DataSource = loadclntdg("", found)
            Else
                dialogs.Close()
                dialogs.advertencia("No puedes editar al Mostrador")
            End If

        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        main.ppos.Controls.Remove(panelclientes)
        main.clntbutton.Tag = "0"
        butnoactivecolor(main.clntbutton)
        Me.Close()
    End Sub

    Private Sub Button4_MouseEnter(sender As Object, e As EventArgs) Handles Button4.MouseEnter, Agregar.MouseEnter, modadeudo.MouseEnter, Button5.MouseEnter, pagaradeudo.MouseEnter, delclnt.MouseEnter, modclnt.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub Button4_MouseLeave(sender As Object, e As EventArgs) Handles Button4.MouseLeave, Agregar.MouseLeave, modadeudo.MouseLeave, Button5.MouseLeave, pagaradeudo.MouseLeave, delclnt.MouseLeave, modclnt.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub sbox_TextChanged_1(sender As Object, e As EventArgs) Handles sbox.TextChanged

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles modadeudo.Click

    End Sub

    Private Sub pagaradeudo_Click(sender As Object, e As EventArgs) Handles pagaradeudo.Click, PagarAdeudoToolStripMenuItem.Click
        Dim rindex As Integer = dg.CurrentCell.RowIndex

        If dg("Adeudo", rindex).Value > 0 Then
            Dim clnt As Cliente = New Cliente
            clnt.loadclnt(dg("id", rindex).Value)
            If pagof.loadpagoadeudo(clnt) = DialogResult.OK Then
                If sbox.BackColor = Color.Red Then
                    sbox.Text = ""
                End If
                loader()
            End If
            pagof.Close()
        Else
            dialogs.Close()

            dialogs.advertencia("El cliente no tiene adeudo que pagar")
        End If
    End Sub

    Private Sub dg_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg.CellContentClick

    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        Dim rindex As Integer = dg.CurrentCell.RowIndex

        If dg("Adeudo", rindex).Value > 0 Then
            PagarAdeudoToolStripMenuItem.Enabled = True
        Else
            PagarAdeudoToolStripMenuItem.Enabled = False

        End If
    End Sub

    Private Sub dg_SelectionChanged(sender As Object, e As EventArgs) Handles dg.SelectionChanged
        If dg("id", dg.CurrentCell.RowIndex).Value > 1 Then
            loadclntmov()
        Else
            clntmov.Rows.Clear()

        End If


    End Sub
    Private Sub loadclntmov()
        If dg.Rows.Count > 0 And dg("id", dg.CurrentCell.RowIndex).Value > 1 Then
            clnt = New Cliente()
            clnt.loadclnt(dg("id", dg.CurrentCell.RowIndex).Value)
            Dim n As Integer = 1
            If nmov.Text <> "" Then
                n = Val(nmov.Text)
            End If
            If RadioButton1.Checked Then
                clnt.loadmovs(clntmov, 1, n, Today, Today)
            End If

            If RadioButton2.Checked Then
                clnt.loadmovs(clntmov, 2, 1, dp1.Value.Date, dp2.Value.Date)
            End If

            If RadioButton3.Checked Then
                clnt.loadmovs(clntmov, 3, 1, Today, Today)
            End If

            If clntmov.Rows.Count > 0 Then
                With clntmov
                    .Columns.Item("clave").HeaderText = "Clave"
                    .Columns.Item("Descripcion").HeaderText = "Descripción"
                    .Columns.Item("fecha").HeaderText = "Fecha"
                    .Columns.Item("horast").HeaderText = "Hora"
                    .Columns.Item("total").HeaderText = "Total"
                    .Columns.Item("pagado").HeaderText = "Pagado"
                    .Columns.Item("usuario").HeaderText = "Usuario"
                    .Columns.Item("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    .Columns.Item("fecha").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    .Columns.Item("horast").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    .Columns.Item("clave").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

                End With


            End If


        End If
    End Sub
    Private Sub clntmov_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles nmov.TextChanged

    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked Then
            nmov.Enabled = False
            dp1.Enabled = False
            dp2.Enabled = False
            filtros.Enabled = True

        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            nmov.Enabled = False
            dp1.Enabled = True
            dp2.Enabled = True
        End If


    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            nmov.Enabled = True
            dp1.Enabled = False
            dp2.Enabled = False
            filtros.Enabled = True

        End If
    End Sub

    Private Sub nmov_MarginChanged(sender As Object, e As EventArgs) Handles nmov.MarginChanged

    End Sub

    Private Sub nmov_LostFocus(sender As Object, e As EventArgs) Handles nmov.LostFocus
        If nmov.Text = "" Then
            nmov.Text = 1
        End If
    End Sub

    Private Sub nmov_KeyPress(sender As Object, e As KeyPressEventArgs) Handles nmov.KeyPress

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles filtros.Click
        loadclntmov()

    End Sub

    Private Sub dp1_ValueChanged(sender As Object, e As EventArgs) Handles dp1.ValueChanged, dp2.ValueChanged

    End Sub

    Private Sub dp2_ClientSizeChanged(sender As Object, e As EventArgs) Handles dp2.ClientSizeChanged

    End Sub

    Private Sub clntmov_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles clntmov.CellContentClick
        If e.ColumnIndex = 0 And clntmov.Rows.Count > 1 Then
            If clntmov("clave", e.RowIndex).Value <> "PAGOAD" Then
                Dim notas As notaventa = New notaventa
                notas.loadventa(clntmov("clave", e.RowIndex).Value)
                formvent.loaderfin(notas)
            End If
        End If
    End Sub
End Class