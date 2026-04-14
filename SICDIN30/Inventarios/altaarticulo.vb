Imports System.Security.Policy

Public Class altaarticulo
    Private imagepath As String
    Private estadopor As Integer = 0
    Private fotoclip As Boolean
    Dim imaglist As imagsvisor
    Private modd As Boolean = False
    Private ptim As PictureBox()
    Private articulo As articuloventa = New articuloventa



    Private Sub altaarticulo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub etiquetador()


        If des.Text <> "" Then
            Dim desstring As String = des.Text
            Dim dessep() As String = desstring.Split(" ")
            tags.Text = ""
            Dim i As Integer = 0
            For i = 0 To dessep.Count - 1

                If dessep(i).Length > 3 Or dessep(i).ToLower = "aro" Or IsNumeric(dessep(i)) Then
                    If i > 0 Then
                        tags.Text &= ","
                    End If
                    tags.Text &= dessep(i)
                End If
            Next
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles pmen.KeyPress, pormen.KeyPress, pormay.KeyPress
        e.KeyChar = numblock(e.KeyChar, True, False, sender.text)

    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles stock.Leave, existencias.Leave, pmen.Leave, pmay.Leave, pormay.Leave, pormen.Leave, pme.Leave
        leavenum(sender)
    End Sub

    Private Sub pesp_TextChanged(sender As Object, e As EventArgs) Handles pesp.TextChanged

        articulo.especial = Val(sender.text)

    End Sub

    Private Sub pme_TextChanged(sender As Object, e As EventArgs) Handles pme.TextChanged
        articulo.pme = Val(sender.text)
    End Sub
    Private Sub cbarra_TextChanged(sender As Object, e As EventArgs) Handles cbarra.TextChanged
        articulo.cbarra = sender.text

    End Sub
    Private Sub des_TextChanged(sender As Object, e As EventArgs) Handles des.TextChanged
        articulo.descripcion = sender.text

    End Sub

    Private Sub calpormay() Handles pormay.TextChanged, precio.TextChanged
        ''calcula cuanto el precio mayoreo con el porcentaje de ganancia
        If Val(precio.Text) > 0 And Val(pormay.Text) > 0 And estadopor = 1 Then
            pmay.Text = Math.Round(Val(precio.Text) * ((Val(pormay.Text) / 100) + 1), 2)
        End If
    End Sub
    Private Sub calpormen() Handles pormen.TextChanged, precio.TextChanged
        ''calcula cuanto el precio menudeo con el porcentaje de ganancia

        If Val(precio.Text) > 0 And Val(pormen.Text) > 0 And estadopor = 1 Then
            pmen.Text = Math.Round(Val(precio.Text) * ((Val(pormen.Text) / 100) + 1), 2)
        End If
    End Sub
    Private Sub calcpormay() Handles pmay.TextChanged
        ''calcula cuanto porcentaje hay entre el precio mayoreo y precio de compra
        If Val(precio.Text) > 0 And Val(pmay.Text) > 0 And estadopor = 2 Then
            pormay.Text = Math.Round(((Val(pmay.Text) / Val(precio.Text)) - 1), 2) * 100
        End If

    End Sub
    Private Sub calcpormen() Handles pmen.TextChanged
        ''calcula cuanto porcentaje hay entre el precio menudeo y precio de compra
        If Val(precio.Text) > 0 And Val(pmen.Text) > 0 And estadopor = 2 Then
            pormen.Text = (Math.Round(Val(pmen.Text) / Val(precio.Text), 2) - 1) * 100
        End If

    End Sub
    Private Sub pmen_TextChanged(sender As Object, e As EventArgs) Handles pmen.TextChanged
        articulo.menudeo = Val(sender.text)

    End Sub
    Private Sub pmay_TextChanged(sender As Object, e As EventArgs) Handles pmay.TextChanged
        articulo.mayoreo = Val(sender.text)

    End Sub
    Private Sub pormay_TextChanged(sender As Object, e As EventArgs) Handles pormay.TextChanged
        articulo.pgmay = Val(sender.text)
    End Sub
    Private Sub pormen_TextChanged(sender As Object, e As EventArgs) Handles pormen.TextChanged
        articulo.pgmen = Val(sender.text)
    End Sub
    Private Sub precio_TextChanged(sender As Object, e As EventArgs) Handles precio.TextChanged

        articulo.precio = Val(sender.text)


    End Sub
    Private Sub paquete_TextChanged(sender As Object, e As EventArgs) Handles paquete.TextChanged
        articulo.pq = sender.text


    End Sub
    Private Sub pmen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles pmen.KeyPress, pesp.KeyPress, precio.KeyPress
        e.KeyChar = numblock(e.KeyChar, True, True, sender.text)


    End Sub
    Public Sub loader()
        articulo = New articuloventa
        des.AutoCompleteCustomSource = asda
        clave.AutoCompleteCustomSource = asma
        fotoclip = False
        modd = False
        imaglist = New imagsvisor()
        If Me.ShowDialog() = DialogResult.OK Then
            articulo.fum = Now
            articulo.fuc = Now
            articulo.altaart(articulo.existencias)
            imaglist.saveme()

        End If

        Me.Close()

    End Sub
    Public Sub loadermod(ByRef claveb As String)
        articulo = New articuloventa
        fotoclip = False
        modd = True
        articulo.load(claveb, True)
        With articulo
            clave.Text = .clave
            des.Text = .descripcion
            pme.Text = .pme
            pmen.Text = .menudeo
            pmay.Text = .mayoreo
            pesp.Text = .especial
            tipo.Text = .tipo
            einf.Checked = .einf
            cbarra.Text = .cbarra
            paquete.Text = .pq
            stock.Text = .stock
            tags.Text = .tag
            notas.Text = .notas
            existencias.Text = .existencias
            pormay.Text = .pgmay
            pormen.Text = .pgmen
            precio.Text = .precio
        End With

        Label1.Text = "Modificar artículo"
        clave.Enabled = False
        imaglist = New imagsvisor(claveb)
        foto.Image = imaglist.currentimag
        Label11.Text = imaglist.getlabimag
        foto.SizeMode = PictureBoxSizeMode.Zoom
        If Not imaglist.isnextimag Then
            siguiente.Text = "+"
        End If
        If Me.ShowDialog = DialogResult.OK Then
            articulo.fum = Now
            articulo.updateart(True, Val(existencias.Text))
            imaglist.saveme()


        End If
        Me.Close()
    End Sub
    Private Sub pmen_Leave(sender As Object, e As EventArgs) Handles pmen.Leave, pesp.Leave

        leavenumesp(sender, precio.Text)
    End Sub
    Private Sub pmay_Leave(sender As Object, e As EventArgs) Handles pmay.Leave

        leavenumesp(sender, precio.Text)
    End Sub
    Private Sub clave_TextChanged(sender As Object, e As EventArgs) Handles clave.TextChanged
        articulo.clave = sender.text

    End Sub

    Private Sub clave_KeyPress(sender As Object, e As KeyPressEventArgs) Handles clave.KeyPress
        e.KeyChar = limittextbox(e.KeyChar, 9, sender.text)
    End Sub
    Private Sub blocke()


    End Sub
    Public Sub freee()

    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub einf_CheckedChanged(sender As Object, e As EventArgs) Handles einf.CheckedChanged
        articulo.einf = einf.Checked

    End Sub
    Private Sub cbarra_Leave(sender As Object, e As EventArgs) Handles cbarra.Leave
        If sender.text <> "" Then
            If seardes(sender.text) And Not modd Then
                ErrorProvider1.SetError(sender, "Ya existe el artículo")
            Else
                ErrorProvider1.SetError(sender, "")
            End If
        End If
        verify()
    End Sub

    Private Sub des_Leave(sender As Object, e As EventArgs) Handles des.Leave
        If sender.text <> "" Then
            If seardes(sender.text) And Not modd Then
                ErrorProvider1.SetError(sender, "Ya existe el artículo")
            Else
                ErrorProvider1.SetError(sender, "")
            End If
        Else
            If Not sender.Equals(cbarra) Then
                ErrorProvider1.SetError(sender, "El campo no puede quedar en blanco")
            Else
                ErrorProvider1.SetError(sender, "")

            End If
        End If
        verify()
    End Sub
    Private Sub clave_Leave(sender As Object, e As EventArgs) Handles clave.Leave
        If sender.text <> "" Then
            If searclave(sender.text) And Not modd Then
                ErrorProvider1.SetError(sender, "Ya existe el artículo")
            Else
                ErrorProvider1.SetError(sender, "")
            End If
        Else
            If Not sender.Equals(cbarra) Then
                ErrorProvider1.SetError(sender, "El campo no puede quedar en blanco")
            Else
                ErrorProvider1.SetError(sender, "")

            End If
        End If
        verify()
    End Sub
    Private Sub verify()
        If clave.Text <> "" And ErrorProvider1.GetError(clave) = "" And des.Text <> "" And ErrorProvider1.GetError(des) = "" And ErrorProvider1.GetError(cbarra) = "" Then
            aceptar.Enabled = True
        Else
            aceptar.Enabled = False
        End If
    End Sub


    Private Sub MaskedTextBox1_Leave(sender As Object, e As EventArgs) Handles paquete.Leave
        If sender.text = "" Then
            sender.text = "PZA"
        End If
    End Sub

    Private Sub altaarticulo_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.DialogResult = DialogResult.Cancel
            Case Keys.Enter
                SendKeys.SendWait("{TAB}")
        End Select
    End Sub

    Private Sub ea1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click

    End Sub

    Private Sub pme_Leave(sender As Object, e As EventArgs) Handles pme.Leave

    End Sub

    Private Sub aceptar_MouseEnter(sender As Object, e As EventArgs) Handles aceptar.MouseEnter, cancelar.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub aceptar_MouseLeave(sender As Object, e As EventArgs) Handles aceptar.MouseLeave, cancelar.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles search.Click
        'If buscar.ShowDialog() = DialogResult.OK Then
        '    If buscar.FileName <> "" Then
        '        articulo.imagepath = buscar.FileName
        '        foto.ImageLocation = articulo.imagepath

        '        foto.SizeMode = PictureBoxSizeMode.Zoom
        '    End If
        'End If
        foto.Image = imaglist.setimagepath

    End Sub

    Private Sub tags_TextChanged(sender As Object, e As EventArgs) Handles tags.TextChanged
        articulo.tag = sender.text

    End Sub

    Private Sub tipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tipo.SelectedIndexChanged
        articulo.tipo = sender.text
        If sender.text = "Trabajo" Then
            existencias.Text = 1
            existencias.Enabled = False
        Else
            existencias.Enabled = True
        End If

    End Sub

    Private Sub existencias_TextChanged(sender As Object, e As EventArgs) Handles existencias.TextChanged
        articulo.existencias = Val(sender.text)
    End Sub

    Private Sub stock_TextChanged(sender As Object, e As EventArgs) Handles stock.TextChanged
        articulo.stock = Val(sender.text)
    End Sub

    Private Sub notas_TextChanged(sender As Object, e As EventArgs) Handles notas.TextChanged
        articulo.notas = sender.text
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        etiquetador()

    End Sub

    Private Sub precio_LostFocus(sender As Object, e As EventArgs) Handles precio.LostFocus



    End Sub

    Private Sub delfoto_Click(sender As Object, e As EventArgs) Handles delfoto.Click

        foto.Image = imaglist.deletecurrent
        Label11.Text = imaglist.getlabimag

    End Sub

    Private Sub foto_Click(sender As Object, e As EventArgs) Handles foto.Click
        If fotoclip Then
            Visor.loaderimage(foto.Image)
        Else
            Visor.loaderimagepath(foto.ImageLocation)
        End If
    End Sub

    Private Sub pegar_Click(sender As Object, e As EventArgs) Handles pegar.Click
        'If Clipboard.ContainsImage Then
        '    foto.Image = Clipboard.GetImage()
        '    foto.SizeMode = PictureBoxSizeMode.Zoom
        '    fotoclip = True
        '    foto.Image.Save(pathimage & "temp" & ".jpg", Imaging.ImageFormat.Jpeg)
        '    articulo.imagepath = pathimage & "temp" & ".jpg"
        '    imagepath = pathimage & "temp" & ".jpg"
        'End If
        foto.Image = imaglist.setimageclip
        Label11.Text = imaglist.getlabimag


    End Sub

    Private Sub altaarticulo_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub buscar_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles buscar.FileOk

    End Sub

    Private Sub pormen_EnabledChanged(sender As Object, e As EventArgs) Handles pormen.EnabledChanged

    End Sub

    Private Sub pormen_Enter(sender As Object, e As EventArgs) Handles pormen.Enter, pormay.Enter, precio.Enter
        estadopor = 1
    End Sub

    Private Sub pmen_Enter(sender As Object, e As EventArgs) Handles pmen.Enter, pmay.Enter
        estadopor = 2
    End Sub

    Private Sub calcpormay(sender As Object, e As EventArgs) Handles pmay.TextChanged

    End Sub

    Private Sub iva_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        precio.Text = Math.Round(Val(precio.Text) * 1.16, 2)

    End Sub

    Private Sub pmay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles pmay.KeyPress
        If e.KeyChar = Chr(8) Then
            Exit Sub
        End If
        If e.KeyChar > Chr(64) Then
            e.KeyChar = cla2pre(e.KeyChar.ToString).ToString
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles anterior.Click
        foto.Image = imaglist.lastimag
        Label11.Text = imaglist.getlabimag

        If imaglist.isnextimag Then
            siguiente.Text = ">"
        Else
            siguiente.Text = "+"
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles siguiente.Click
        If siguiente.Text = "+" Then
            foto.Image = imaglist.addnew
            Label11.Text = imaglist.getlabimag

        Else
            foto.Image = imaglist.nextimag
            Label11.Text = imaglist.getlabimag

        End If
        If imaglist.isnextimag Then
            siguiente.Text = ">"
        Else
            siguiente.Text = "+"
        End If

    End Sub
End Class