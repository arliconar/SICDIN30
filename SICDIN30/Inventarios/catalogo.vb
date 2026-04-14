
Public Class catalogo
    Dim iamdialog As Boolean = False
    Dim fechabus As String = ""
    Dim font1 As Font = New Font("Arial", 14, FontStyle.Regular)
    Dim font2 As Font = New Font("Arial", 14, FontStyle.Bold)

    Public Function loadcatadialog(st As String) As articuloventa

        loadcatadialog = New articuloventa
        sbox.Text = st
        reloadcat()

        Panel1.Show()
        Panel2.Show()
        Panel3.Show()
        Panel4.Show()
        panelcat.SendToBack()
        sbox.Focus()
        iamdialog = True
        notab = True
        modicarcontext.Visible = False
        aee.Visible = False
        menusecundario.Enabled = False
        sbox.Focus()
        vrap.Visible = False

        iamdialog = True
        If Me.ShowDialog() = DialogResult.OK And dgcat.Rows.Count > 0 Then
            Dim rindex As Integer = dgcat.SelectedRows.Item(0).Index
            loadcatadialog.load(dgcat("clave", rindex).Value, True)

        Else
            loadcatadialog.clave = st
            loadcatadialog.found = False

        End If
        sbox.Text = ""
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Close()

    End Sub

    Private Sub dgcat_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgcat.CellContentClick

        If (e.ColumnIndex) = dgcat.Columns.Item("verc").Index Then
            Visor.loader(dgcat("Clave", dgcat.CurrentCell.RowIndex).Value)
        End If

        If (e.ColumnIndex) = dgcat.Columns.Item("Editar").Index Then
            If dgcat("clave", dgcat.CurrentCell.RowIndex).Value <> "" Then
                altaarticulo.loadermod(dgcat("clave", dgcat.CurrentCell.RowIndex).Value)
                reloadcat()
            End If
        End If
        If (e.ColumnIndex) = dgcat.Columns.Item("vrap").Index Then
            If dgcat("clave", dgcat.CurrentCell.RowIndex).Value <> "" Then
                Ventarapid.loader(dgcat("clave", dgcat.CurrentCell.RowIndex).Value)
                reloadcat()

            End If
        End If
        If (e.ColumnIndex) = dgcat.Columns.Item("AE").Index Then
            If dgcat("clave", dgcat.CurrentCell.RowIndex).Value <> "" Then
                addexist.loader(dgcat("clave", dgcat.CurrentCell.RowIndex).Value)
                reloadcat()
            End If

        End If
    End Sub
    Private Sub catcolresize()
        dgcat.Columns.Item("Descripción").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgcat.Columns.Item("Clave").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgcat.Columns.Item("FUM").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgcat.Columns.Item("Mayoreo").Visible = may.Checked
        If Not Trabajo.Checked Then
            dgcat.Columns.Item("Especial").Visible = may.Checked
            dgcat.Columns.Item("PME").Visible = may.Checked

        End If


    End Sub
    Public Function reloadcat()
        Dim dsn As BindingSource = New BindingSource
        Dim found As Boolean = False
        dsn.DataSource = searart(sbox.Text, existentes.Checked, etiquetas.Checked, Trabajo.Checked, fechabus, found)
        If found Then
            dgcat.DataSource = dsn
            catcolresize()
            sbox.Font = font1
            sbox.BackColor = SystemColors.InactiveCaption
            sbox.ForeColor = Color.Black

        Else
            sbox.Font = font2
            sbox.BackColor = Color.Red
            sbox.ForeColor = Color.White

        End If

        colorimag()
        Return 0
    End Function
    Private Sub colorimag()


        For i = dgcat.FirstDisplayedScrollingRowIndex To dgcat.FirstDisplayedScrollingRowIndex + dgcat.DisplayedRowCount(False) - 1
            Dim stfile As String = pathimage & dgcat("Clave", i).Value & ".jpg"
            Dim stdir As String = pathimage & dgcat("Clave", i).Value
            Select Case checkimag(dgcat("Clave", i).Value)
                Case 1
                    dgcat("Verc", i).Style.BackColor = Color.ForestGreen
                Case 2
                    dgcat("Verc", i).Style.BackColor = Color.BlueViolet
            End Select

        Next


    End Sub
    Public Function checkimag(clave As String) As Integer
        checkimag = 0
        Dim stfile As String = pathimage & clave & ".jpg"
        Dim stdir As String = pathimage & clave

        If System.IO.File.Exists(stfile) Then
            checkimag = 1
        End If
        If System.IO.Directory.Exists(stdir) Then
            checkimag = 2
        End If
    End Function
    Private Sub sbox_TextChanged(sender As Object, e As EventArgs) Handles busqueda.Click, sbox.TextChanged


        reloadcat()



    End Sub

    Private Sub dgcat_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgcat.DataError

    End Sub

    Private Sub ModificarArtículoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles modicarcontext.Click
        If dgcat("clave", dgcat.CurrentCell.RowIndex).Value <> "" Then
            altaarticulo.loadermod(dgcat("clave", dgcat.CurrentCell.RowIndex).Value)
            ' reloadcat()

        End If
    End Sub

    Private Sub catalogo_StyleChanged(sender As Object, e As EventArgs) Handles Me.StyleChanged

    End Sub

    Private Sub catalogo_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged


    End Sub

    Private Sub catalogo_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Select Case e.KeyCode
            Case Keys.Escape
                Me.DialogResult = DialogResult.Cancel


        End Select
    End Sub

    Private Sub dgcat_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgcat.CellContentDoubleClick
        Me.DialogResult = DialogResult.OK

    End Sub

    Private Sub sbox_KeyUp(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub sbox_KeyDown(sender As Object, e As KeyEventArgs) Handles dgcat.KeyDown, sbox.KeyDown

        Try
            If dgcat.Rows.Count > 0 Then


                Dim rindex As Integer = dgcat.CurrentCell.RowIndex
                Dim cindex As Integer = dgcat.CurrentCell.ColumnIndex
                Select Case e.KeyCode
                    Case Keys.Up
                        If rindex > 0 Then
                            dgcat.CurrentCell = dgcat(cindex, rindex - 1)
                        End If
                    Case Keys.Down

                        If rindex < dgcat.Rows.Count - 1 Then
                            dgcat.CurrentCell = dgcat(cindex, rindex + 1)
                        End If
                    Case Keys.Enter
                        e.SuppressKeyPress = True
                        If iamdialog Then
                            Me.DialogResult = vbOK

                        End If
                    Case Keys.Escape
                        If iamdialog Then
                            Me.DialogResult = DialogResult.Cancel

                        End If
                End Select

            End If


        Catch ex As Exception

        End Try


    End Sub

    Private Sub catalogo_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    Private Sub dgcat_DataSourceChanged(sender As Object, e As EventArgs) Handles dgcat.DataSourceChanged
    End Sub

    Private Sub catalogo_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            sbox.Focus()

        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        dgcat.DataSource = searartempty()
        catcolresize()
        sbox.Focus()

        Timer1.Enabled = False

    End Sub
    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles menusecundario.Opening, menusecundario.Opening
        ''oculta el ver imagen cuando se hace click secundario
        VerImagenToolStripMenuItem.Visible = False
        If dgcat("clave", dgcat.CurrentCell.RowIndex).Value <> "" Then
            If existimage(dgcat("clave", dgcat.CurrentCell.RowIndex).Value) Then
                VerImagenToolStripMenuItem.Visible = True
            End If
        End If
    End Sub

    Private Sub VerImagenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerImagenToolStripMenuItem.Click
        Visor.loader(dgcat("Clave", dgcat.CurrentCell.RowIndex).Value)

    End Sub

    Private Sub sbox_TextChanged_3(sender As Object, e As EventArgs) Handles sbox.TextChanged

    End Sub

    Private Sub existentes_CheckedChanged(sender As Object, e As EventArgs) Handles existentes.Click, Trabajo.Click, etiquetas.Click

        reloadcat()


    End Sub

    Private Sub Panel6_Paint(sender As Object, e As PaintEventArgs) Handles Panel6.Paint

    End Sub

    Private Sub Trabajo_CheckedChanged(sender As Object, e As EventArgs) Handles Trabajo.CheckedChanged
        If Trabajo.Checked Then
            existentes.Checked = False
            existentes.Enabled = False
        Else
            existentes.Enabled = True
            existentes.Checked = True


        End If
    End Sub

    Private Sub Precio_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub AltaArtículoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AltaArtículoToolStripMenuItem.Click
        altaarticulo.loader()

    End Sub

    Private Sub EliminarArtículoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarArtículoToolStripMenuItem.Click
        If dgcat("clave", dgcat.CurrentCell.RowIndex).Value <> "" Then
            Dim art As articuloventa = New articuloventa(dgcat("clave", dgcat.CurrentCell.RowIndex).Value)
            art.deleteme()

            reloadcat()

        End If
    End Sub

    Private Sub VentaRapiidaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VentaRapiidaToolStripMenuItem.Click
        If dgcat("clave", dgcat.CurrentCell.RowIndex).Value <> "" Then
            Ventarapid.loader(dgcat("clave", dgcat.CurrentCell.RowIndex).Value)
            reloadcat()

        End If
    End Sub

    Private Sub ClaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClaveToolStripMenuItem.Click
        If dgcat.SelectedRows.Count > 1 Then
            Dim stc As String = ""
            For i = dgcat.SelectedRows.Count - 1 To 0 Step -1
                stc &= dgcat.SelectedRows.Item(i).Cells.Item("Clave").Value & vbNewLine
            Next
            Clipboard.SetText(stc)

        Else
            Dim rindex As Integer = dgcat.CurrentCell.RowIndex
            Clipboard.SetText(dgcat("clave", rindex).Value)
        End If


    End Sub

    Private Sub DescripciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DescripciónToolStripMenuItem.Click

        If dgcat.SelectedRows.Count > 1 Then
            Dim stc As String = ""
            For i = dgcat.SelectedRows.Count - 1 To 0 Step -1
                stc &= dgcat.SelectedRows.Item(i).Cells.Item("Descripción").Value & vbNewLine
            Next
            Clipboard.SetText(stc)

        Else
            Dim rindex As Integer = dgcat.CurrentCell.RowIndex
            Clipboard.SetText(dgcat("Descripción", rindex).Value)
        End If

    End Sub

    Private Sub ClaveDescripciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClaveDescripciónToolStripMenuItem.Click
        If dgcat.Rows.Count > 0 Then
            If dgcat.SelectedRows.Count > 1 Then
                Dim stc As String = ""
                For i = dgcat.SelectedRows.Count - 1 To 0 Step -1
                    With dgcat.SelectedRows.Item(i).Cells
                        stc &= "Clave:" & .Item("Clave").Value & vbTab & "Descripción:" & .Item("Descripción").Value & vbTab & "Menudeo:$" & .Item("Menudeo").Value & vbTab & "Mayoreo:$" & .Item("Mayoreo").Value & vbNewLine
                    End With

                Next
                Clipboard.SetText(stc)

            Else
                Dim rindex As Integer = dgcat.CurrentCell.RowIndex

                Clipboard.SetText("Clave:" & dgcat("Clave", rindex).Value & vbTab & "Descripción:" & dgcat("Descripción", rindex).Value & vbTab & "Menudeo:$" & dgcat("Menudeo", rindex).Value & vbTab & "Mayoreo:$" & dgcat("Mayoreo", rindex).Value)
            End If

        End If

    End Sub

    Private Sub aee_CheckedChanged(sender As Object, e As EventArgs) Handles aee.CheckedChanged
        dgcat.Columns.Item("AE").Visible = aee.Checked
        dgcat.Columns.Item("Editar").Visible = aee.Checked


    End Sub

    Private Sub may_CheckedChanged(sender As Object, e As EventArgs) Handles may.CheckedChanged
        dgcat.Columns.Item("Mayoreo").Visible = may.Checked
        If Not Trabajo.Checked Then
            dgcat.Columns.Item("Especial").Visible = may.Checked
            dgcat.Columns.Item("PME").Visible = may.Checked

        End If

    End Sub


    Private Sub PedidaDevoluciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PedidaDevoluciónToolStripMenuItem.Click
        If dgcat("clave", dgcat.CurrentCell.RowIndex).Value <> "" Then
            Dim art As articuloventa = New articuloventa
            art.load(dgcat("clave", dgcat.CurrentCell.RowIndex).Value, True)
            perdida.loader(art)
            reloadcat()

        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub catalogo_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        sbox.Focus()
        sbox.Select(sbox.Text.Length, 0)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles fecha.CheckedChanged
        Dim stfec As String = ":"
        If fecha.Checked Then

            fechabus = fechabusqueda.loader(stfec)
            If fechabus = "" Then
                fecha.Checked = False
            End If
        Else
            fechabus = ""
            stfec = ""
        End If
        fecha.Text = "Fecha" & stfec
        reloadcat()



    End Sub

    Private Sub dgcat_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgcat.CellPainting

    End Sub

    Private Sub dgcat_Scroll(sender As Object, e As ScrollEventArgs) Handles dgcat.Scroll
        colorimag()

    End Sub

    Private Sub dgcat_SelectionChanged(sender As Object, e As EventArgs) Handles dgcat.SelectionChanged
        Select Case checkimag(dgcat("clave", dgcat.CurrentCell.RowIndex).Value)
            Case 0
                dgcat.DefaultCellStyle.SelectionBackColor = Color.FromName("Highlight")
            Case 1
                dgcat.DefaultCellStyle.SelectionBackColor = Color.ForestGreen

            Case 2
                dgcat.DefaultCellStyle.SelectionBackColor = Color.BlueViolet
        End Select



    End Sub

    Private Sub EtiquetaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EtiquetaToolStripMenuItem.Click

        Dim stet As RichTextBox = New RichTextBox
        stet.Paste()
        Dim f1 As Font = New Font("Calibri", 20, FontStyle.Bold)
        Dim f2 As Font = New Font("Calibri", 11, FontStyle.Bold)
        stet.Font = f1
        Dim preciom As Decimal = Val(dgcat("menudeo", dgcat.CurrentCell.RowIndex).Value)
        Dim st1 As String = "$" & preciom.ToString(".00")
        Dim mayoreo As String = pre2cla(dgcat("mayoreo", dgcat.CurrentCell.RowIndex).Value).ToUpper
        Dim st2 As String = mayoreo & "-" & dgcat("clave", dgcat.CurrentCell.RowIndex).Value
        stet.Text = st1 & vbNewLine & st2

        stet.SelectionStart = st1.Length
        stet.SelectionLength = st2.Length + 1
        stet.SelectionFont = f2



        stet.SelectAll()

        stet.Copy()

    End Sub

    Private Sub AVentaNuevaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AVentaNuevaToolStripMenuItem.Click
        newventawin(dgcat("clave", dgcat.CurrentCell.RowIndex).Value)

    End Sub

    Private Sub VerificarExistenciasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerificarExistenciasToolStripMenuItem.Click
        existenciasart.loader(dgcat("Clave", dgcat.CurrentCell.RowIndex).Value)
        reloadcat()

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CopiarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopiarToolStripMenuItem.Click

    End Sub

    Private Sub CopiarToolStripMenuItem_DropDownOpened(sender As Object, e As EventArgs) Handles CopiarToolStripMenuItem.DropDownOpened

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        sbox.Text &= Formulas.loader
        sbox.Focus()
        sbox.SelectionStart = sbox.Text.Length

    End Sub
End Class