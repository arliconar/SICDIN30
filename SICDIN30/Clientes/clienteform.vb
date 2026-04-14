Public Class clienteform
    Dim dummydec As Decimal = 0
    Dim creditod As Decimal = 0
    Dim dcd As Integer = 0
    Dim id As Integer
    Dim cliente As Cliente
    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles tcasa.TextChanged
        cliente.telcasa = tcasa.Text
    End Sub
    Public Sub modclnt(ida As Integer)
        cliente = New Cliente
        cliente.loadclnt(ida)
        title.Text = "Modificar cliente"
        idlabel.Text = "Id:" & cliente.id
        With cliente
            Nombre.Text = .Nombre
            direccion.Text = .Direccion
            may.Checked = .mayoreo
            esp.Checked = .especial
            credito.Text = .credito
            dc.Text = .dc
            rfc.Text = .RFC
            tcasa.Text = .telcasa
            tcel.Text = .telcel
            mail.Text = .mail
        End With
        If Me.ShowDialog() = DialogResult.OK Then
            cliente.updateme()

        End If

    End Sub
    Private Sub clean()
        cliente = New Cliente
        Nombre.Text = ""
        direccion.Text = ""
        rfc.Text = ""
        tcasa.Text = ""
        tcel.Text = ""
        mail.Text = ""
        may.Checked = False
        esp.Checked = False
        esp.Enabled = False
        credito.Text = 0
        dc.Text = 0
    End Sub
    Public Sub ver(clnv As Cliente)
        cliente = New Cliente
        cliente = clnv
        title.Text = "Detalles del cliente"
        idlabel.Text = "Id:" & cliente.id
        Nombre.Text = cliente.Nombre
        direccion.Text = cliente.Direccion
        rfc.Text = cliente.RFC
        tcasa.Text = cliente.telcasa
        tcel.Text = cliente.telcel
        may.Checked = cliente.mayoreo
        esp.Checked = cliente.especial
        credito.Text = cliente.credito
        dc.Text = cliente.dc
        mail.Text = cliente.mail
        Nombre.ReadOnly = True
        direccion.ReadOnly = True
        rfc.ReadOnly = True
        tcasa.ReadOnly = True
        tcel.ReadOnly = True
        may.Enabled = False
        esp.Enabled = False
        credito.ReadOnly = True
        dc.ReadOnly = True
        aceptar.Visible = False
        cancelar.Visible = False
        aceptarblock.Visible = True
        mail.Enabled = False
        Me.ShowDialog()
        Me.Close()

    End Sub
    Public Function moredetailsventa(clnv As Cliente) As Cliente
        clean()
        cliente = clnv
        Nombre.Text = cliente.Nombre
        direccion.Text = cliente.Direccion
        rfc.Text = cliente.RFC
        tcasa.Text = cliente.telcasa
        tcel.Text = cliente.telcel
        credito.Text = cliente.credito
        dc.Text = cliente.dc
        mail.Text = cliente.mail
        aceptarblock.Hide()
        idlabel.Hide()
        may.Enabled = False
        esp.Enabled = False
        credito.Enabled = False
        dc.Enabled = False
        aceptar.Visible = False
        cancelar.Visible = False
        aceptarblock.Visible = True
        Buscar.Visible = True
        Me.ShowDialog()

        Return cliente

    End Function


    Public Function addclnt(Optional name As String = "") As String
        clean()
        If user.admin Then
            may.Enabled = True
            esp.Enabled = True
        Else
            may.Enabled = False
            esp.Enabled = False

        End If
        '    id = returnlastidcln()
        cliente.id = id
        idlabel.Text = "Id:" & id
        If Me.ShowDialog() = DialogResult.OK Then
            '    addnewclient(id, Nombre.Text, direccion.Text, may.Checked, credito.Text, dc.Text, esp.Checked, rfc.Text, tcasa.Text, tcel.Text, mail.Text)
            cliente.addme()

            Me.Close()
        Else
            Me.Close()

        End If
        addclnt = Nombre.Text

    End Function


    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tcasa.KeyPress, tcel.KeyPress, dc.KeyPress
        'Allow Numeric values, BackSpace key. Disallows decimal point (i.e. dot) 
        'Dim numbers As Windows.Forms.TextBox = sender
        If InStr("1234567890-", e.KeyChar) = 0 And Asc(e.KeyChar) <> 8 Then
            e.KeyChar = Chr(0)
            e.Handled = True
        End If

    End Sub

    Private Sub credito_TextChanged(sender As Object, e As EventArgs) Handles credito.TextChanged
        cliente.credito = Val(credito.Text)

    End Sub

    Private Sub credito_KeyPress(sender As Object, e As KeyPressEventArgs) Handles credito.KeyPress
        'Dim numbers As Windows.Forms.TextBox = sender
        If InStr("1234567890.", e.KeyChar) = 0 And Asc(e.KeyChar) <> 8 Then
            e.KeyChar = Chr(0)
            e.Handled = True
        End If

    End Sub

    Private Sub may_CheckedChanged(sender As Object, e As EventArgs) Handles may.CheckedChanged
        If may.Checked Then

            esp.Enabled = True
            credito.Enabled = True
            dc.Enabled = True

        Else
            esp.Checked = False
            esp.Enabled = False
            may.Checked = False
            credito.Enabled = False
            dc.Enabled = False
            credito.Text = 0
            dc.Text = 0
        End If
        cliente.mayoreo = may.Checked

    End Sub

    Private Sub credito_LostFocus(sender As Object, e As EventArgs) Handles credito.LostFocus
        If credito.Text = "" Then
            credito.Text = 0
        End If
    End Sub

    Private Sub dc_TextChanged(sender As Object, e As EventArgs) Handles dc.TextChanged
        cliente.dc = dc.Text

    End Sub

    Private Sub dc_LostFocus(sender As Object, e As EventArgs) Handles dc.LostFocus
        If dc.Text = "" Then
            dc.Text = 0
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles aceptar.Click

    End Sub

    Private Sub Nombre_TextChanged(sender As Object, e As EventArgs) Handles Nombre.TextChanged
        cliente.Nombre = Nombre.Text

    End Sub

    Private Sub Nombre_LostFocus(sender As Object, e As EventArgs) Handles Nombre.LostFocus
        If Nombre.Text = "" Then
            lockaceptar()
            aceptarblock.Enabled = False

            ErrorProvider1.SetError(Nombre, "Debe proporcionar un nombre")
        ElseIf Nombre.Text.Contains("Mostrador") Then
            lockaceptar()
            ErrorProvider1.SetError(Nombre, "No se puede utilizar la palabra Mostrador para un cliente")
            aceptarblock.Enabled = False
        Else
            ErrorProvider1.SetError(Nombre, "")
            aceptarblock.Enabled = True
            unlockaceptar()
        End If
    End Sub
    Private Sub lockaceptar()
        aceptar.Enabled = False
    End Sub
    Private Sub mail_TextChanged(sender As Object, e As EventArgs) Handles mail.TextChanged
        If Not mail.Text.Contains("@") And mail.Text <> "" Then
            arrob.Visible = True
        Else
            arrob.Visible = False
        End If
        cliente.mail = mail.Text

    End Sub
    Private Sub unlockaceptar()
        If ErrorProvider1.GetError(mail) = "" And ErrorProvider1.GetError(Nombre) = "" Then
            aceptar.Enabled = True
        End If
    End Sub

    Private Sub mail_LostFocus(sender As Object, e As EventArgs) Handles mail.LostFocus

        If Not mail.Text.Contains("@") And mail.Text <> "" Then
            ErrorProvider1.SetError(mail, "Introduzca un mail valido")
            lockaceptar()

        Else
            ErrorProvider1.SetError(mail, "")
            unlockaceptar()

        End If
    End Sub

    Private Sub clienteform_Closed(sender As Object, e As EventArgs) Handles Me.Closed

    End Sub

    Private Sub clienteform_Load(sender As Object, e As EventArgs) Handles Me.Load
        Nombre.AutoCompleteCustomSource = aclnt



    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles arrob.Click
        mail.Text = mail.Text & "@"
        mail.Focus()
        mail.SelectionLength = 0
        mail.SelectionStart = Len(mail.Text)
    End Sub

    Private Sub aceptar_MouseEnter(sender As Object, e As EventArgs) Handles aceptar.MouseEnter, cancelar.MouseEnter, aceptarblock.MouseEnter
        shownamebutton(sender)

    End Sub

    Private Sub aceptar_MouseLeave(sender As Object, e As EventArgs) Handles aceptar.MouseLeave, cancelar.MouseLeave, aceptarblock.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub direccion_TextChanged(sender As Object, e As EventArgs) Handles direccion.TextChanged
        cliente.Direccion = direccion.Text

    End Sub

    Private Sub rfc_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles rfc.MaskInputRejected

    End Sub

    Private Sub tcel_TextChanged(sender As Object, e As EventArgs) Handles tcel.TextChanged
        cliente.telcel = tcel.Text

    End Sub

    Private Sub esp_CheckedChanged(sender As Object, e As EventArgs) Handles esp.CheckedChanged
        cliente.especial = esp.Checked

    End Sub

    Private Sub rfc_TextChanged(sender As Object, e As EventArgs) Handles rfc.TextChanged
        cliente.RFC = rfc.Text

    End Sub

    Private Sub Buscar_Click(sender As Object, e As EventArgs) Handles Buscar.Click
        cliente = clntre.searclnt(0)
        If cliente.found Then
            Nombre.Text = cliente.Nombre
            direccion.Text = cliente.Direccion
            rfc.Text = cliente.RFC
            tcasa.Text = cliente.telcasa
            tcel.Text = cliente.telcel
            credito.Text = cliente.credito
            dc.Text = cliente.dc
            mail.Text = cliente.mail
            ErrorProvider1.SetError(Nombre, "")
            aceptarblock.Enabled = True
            unlockaceptar()
        End If


    End Sub
End Class