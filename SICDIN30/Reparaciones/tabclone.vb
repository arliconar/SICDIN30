
Public Class tabclone
    Public trabfinal As String

    Public repopen As Boolean = False
    Public datefinal As Date
    Private Sub addtab_Click(sender As Object, e As EventArgs) Handles addtab.Click

    End Sub

    Private Sub deltab_Click(sender As Object, e As EventArgs) Handles deltab.Click
        Dim ob As Object = tc1.Parent
        If ob.TabPages.Count > 2 Then
            Dim st1 As String = Microsoft.VisualBasic.Right(tc1.Text, 1)
            dialogs.Close()

            If dialogs.pregunta("¿Deseas eliminar la Bicicleta " & st1 & "?") = DialogResult.Yes Then
                tc1.Dispose()
            End If

        End If


    End Sub

    Private Sub rodada_TextChanged(sender As Object, e As EventArgs) Handles rodada.TextChanged

    End Sub

    Private Sub hora_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs)

    End Sub



    Private Sub hora_LostFocus(sender As Object, e As EventArgs) Handles hora.LostFocus
        ''trans24()
        If Val(hora.Text) > 12 Then
            hora.Text = (Val(hora.Text) - 12).ToString
            formato.Text = "PM"
        ElseIf Val(hora.text) = 0 Then
            hora.Text = "12"
        End If

        hora.Text = Strings.Format(Val(hora.Text), "0#")
    End Sub

    Private Sub fe_ValueChanged(sender As Object, e As EventArgs) Handles fe.ValueChanged

    End Sub

    Private Sub Entregada_CheckedChanged(sender As Object, e As EventArgs) Handles Entregada.Click
        If Entregada.Checked Then
            If markentregada(foliol.Text, Val(Microsoft.VisualBasic.Right(tc1.Text, 1))) Then
                Finalizada.Enabled = False
                cancelbic.Enabled = False
                Entregada.Text = "Entregada por " & user.idnameusu & " el " & Now
                Dim trabajador As String = ""
                Dim fecha As Date = Now
                If getfinalizadabici(foliol.Text, Val(Microsoft.VisualBasic.Right(tc1.Text, 1)), trabajador, fecha) Then
                    Finalizada.Text = "Finalizada por " & trabajador & " el " & fecha
                    Finalizada.Checked = True
                End If
            Else
                Entregada.Checked = False
                Finalizada.Enabled = True
                cancelbic.Enabled = True
            End If
        Else
            desentregar(foliol.Text, Val(Microsoft.VisualBasic.Right(tc1.Text, 1)))
            Entregada.Text = "Entregar"
            Finalizada.Enabled = True
            cancelbic.Enabled = True
        End If
    End Sub

    Private Sub Entregada_Click(sender As Object, e As EventArgs) Handles Entregada.Click

    End Sub

    Private Sub Finalizada_CheckedChanged(sender As Object, e As EventArgs) Handles Finalizada.CheckedChanged

    End Sub
    Public Sub getfinalizo(ByRef trabajador As String, ByRef fecha As Date)
        trabajador = trabfinal
        fecha = datefinal

    End Sub
    Private Sub Finalizada_Click(sender As Object, e As EventArgs) Handles Finalizada.Click
        Dim st As String = ""
        If Finalizada.Checked Then
            Finalizada.Checked = finalizarbici(foliol.Text, Val(Microsoft.VisualBasic.Right(tc1.Text, 1)), st)
            If Finalizada.Checked Then
                Finalizada.Text = st
            End If
        Else
            desfinalizarbici(foliol.Text, Val(Microsoft.VisualBasic.Right(tc1.Text, 1)))
            Finalizada.Text = "Finalizar"
        End If

    End Sub
    'Public Sub trans24()
    '    Dim horas As Integer = Val(hora.Text)
    '    If formato.SelectedIndex < 2 Then
    '        If horas < 12 Then
    '            If horas = 0 Then
    '                hora.Text = 12
    '            End If
    '        Else
    '            formato.SelectedIndex = 1
    '            If horas > 24 Then
    '                horas = 24
    '            End If
    '            hora.Text = horas - 12
    '        End If
    '    Else
    '        If horas > 23 Then
    '            hora.Text = 0
    '        End If

    '    End If
    '    If hora.Text.Length = 1 Then
    '        hora.Text = "0" & hora.Text
    '    End If
    'End Sub


    Private Sub minutos_LostFocus(sender As Object, e As EventArgs) Handles minutos.LostFocus
        minutos.Text = Strings.Format(Val(minutos.Text), "0#")
        If Val(minutos.Text) > 59 Then
            minutos.Text = "00"


        End If

    End Sub

    Private Sub pbicicar_Paint(sender As Object, e As PaintEventArgs) Handles pbicicar.Paint


    End Sub

    Private Sub formato_SelectedIndexChanged(sender As Object, e As EventArgs) Handles formato.SelectedIndexChanged
        'hora.Items.Clear()
        'Dim k As Integer
        'Dim k2 As Integer
        'Dim i As Integer
        'If formato.SelectedIndex = 2 Then
        '    k = 0
        '    k2 = 23
        'Else
        '    k = 1
        '    k2 = 12
        'End If
        'For i = k To k2
        '    hora.Items.Add(Format(i, "0#"))
        'Next
        ''trans24()
    End Sub
    Public Sub fblock(ByVal ena As Boolean)
        pventa.Enabled = ena
    End Sub
    Private Sub blockbicis()
        rodada.Enabled = False
        marca.Enabled = False
        color.Enabled = False

    End Sub
    Private Sub blockall()
        blockbicis()
        fe.Enabled = False
        pventa.Enabled = False
        hora.Enabled = False
        minutos.Enabled = False
        formato.Enabled = False

    End Sub
    Private Sub unblockall()
        fe.Enabled = True
        pventa.Enabled = True
        hora.Enabled = True
        minutos.Enabled = True
        formato.Enabled = True
    End Sub

    Private Sub notas_TextChanged(sender As Object, e As EventArgs) Handles notas.TextChanged

    End Sub

    Private Sub notas_Enter(sender As Object, e As EventArgs) Handles notas.Enter

    End Sub

    Private Sub subsubtotal_Click(sender As Object, e As EventArgs) Handles subsubtotal.Click

    End Sub



    Private Sub Entregada_CheckedChanged_1(sender As Object, e As EventArgs) Handles Entregada.CheckedChanged

    End Sub

    Private Sub cancelbic_CheckedChanged(sender As Object, e As EventArgs) Handles cancelbic.CheckedChanged

    End Sub

    Private Sub cancelbic_Click(sender As Object, e As EventArgs) Handles cancelbic.Click
        If cancelbic.Checked Then
            If cancelarbici(foliol.Text, Val(Microsoft.VisualBasic.Right(tc1.Text, 1))) = True Then

                cancelme()
            Else
                cancelbic.Checked = False
            End If
        End If
    End Sub
    Public Sub cancelme()
        cancelbic.Enabled = False
        cancelbic.Checked = True
        Dim dgd As DataGridView = pventa.Controls.Item("dg1")
        Dim i As Integer = 0
        For i = 0 To dgd.Rows.Count - 1
            dgd.Rows.Item(i).DefaultCellStyle.Font = ftachado
            dgd.Rows.Item(i).DefaultCellStyle.BackColor = Drawing.Color.Red
        Next
        tc1.Enabled = False
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub pventa_Paint(sender As Object, e As PaintEventArgs) Handles pventa.Paint

    End Sub

    Private Sub hora_SelectedIndexChanged(sender As Object, e As EventArgs) Handles hora.SelectedIndexChanged

    End Sub

    Private Sub addtab_MouseEnter(sender As Object, e As EventArgs) Handles addtab.MouseEnter, deltab.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub deltab_MouseLeave(sender As Object, e As EventArgs) Handles deltab.MouseLeave, addtab.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub minutos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles minutos.SelectedIndexChanged

    End Sub

    Private Sub tabe_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabe.SelectedIndexChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub
End Class