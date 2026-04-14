Imports System.Reflection.Emit

Public Class userform
    Public user As usuario
    Public ineditmode As Boolean
    Public Sub edit(id As Integer)
        user = New usuario
        ineditmode = True
        With user
            .loadid(id)
            Idlabel.Text = "Id:" & .id
            nombre.Text = .nombre
            contra1.Text = .pass
            contra2.Text = .pass
            admin.Checked = .admin
            telefono.Text = .telefono
            Select Case .foto
                Case "Hombre"
                    foto.BackgroundImage = SICDIN30.My.Resources.Resources.hombre
                Case "Mujer"
                    foto.BackgroundImage = SICDIN30.My.Resources.Resources.mujeruser
                Case "Carro"
                    foto.BackgroundImage = SICDIN30.My.Resources.Resources.carrouser
                Case "Bicicleta"
                    foto.BackgroundImage = SICDIN30.My.Resources.Resources.bicicleta
                Case "Flor"
                    foto.BackgroundImage = SICDIN30.My.Resources.Resources.floruser
                Case Else
                    If My.Computer.FileSystem.FileExists(.foto) Then
                        foto.BackgroundImage = Nothing
                        foto.ImageLocation = .foto
                        foto.SizeMode = PictureBoxSizeMode.Zoom
                    Else
                        .foto = "Hombre"
                        foto.BackgroundImage = SICDIN30.My.Resources.Resources.hombre

                    End If

            End Select
        End With
        If Me.ShowDialog() = DialogResult.OK Then
            user.editme()

        End If
        Me.Close()

    End Sub
    Public Sub addnew()
        user = New usuario
        ineditmode = False
        Idlabel.Text = "Id:" & user.getid
        ComboBox1.SelectedIndex = 1
        user.foto = "Hombre"
        If Me.ShowDialog() = DialogResult.OK Then
            user.addme()

        End If
        Me.Close()

    End Sub
    Private Sub userform_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Sub contra2_LostFocus(sender As Object, e As EventArgs) Handles contra2.LostFocus

    End Sub

    Private Sub nombre_TextChanged(sender As Object, e As EventArgs) Handles nombre.TextChanged
        user.nombre = nombre.Text

    End Sub

    Private Sub contra1_TextChanged(sender As Object, e As EventArgs) Handles contra1.TextChanged
        user.pass = contra1.Text

    End Sub

    Private Sub admin_CheckedChanged(sender As Object, e As EventArgs) Handles admin.CheckedChanged
        user.admin = admin.Checked

    End Sub

    Private Sub telefono_TextChanged(sender As Object, e As EventArgs) Handles telefono.TextChanged
        user.telefono = telefono.Text

    End Sub


    Private Sub checker() Handles nombre.LostFocus, contra1.LostFocus, contra2.LostFocus
        If nombre.Text <> "" Then
            If user.repeated And ineditmode = False Then
                ErrorProvider1.SetError(nombre, "Nombre repetido")
                aceptar.Enabled = False
                Exit Sub
            Else
                ErrorProvider1.SetError(nombre, "")
            End If
        Else
            Exit Sub
        End If
        If contra1.Text = "" Or contra2.Text = "" Then
            aceptar.Enabled = False
            Exit Sub
        Else
            If contra1.Text = contra2.Text Then
                ErrorProvider1.SetError(contra1, "")
            Else
                ErrorProvider1.SetError(contra1, "No coinciden las contraseñas")
                aceptar.Enabled = False
                Exit Sub
            End If
        End If
        aceptar.Enabled = True
    End Sub
    Private Sub nombre_LostFocus(sender As Object, e As EventArgs) Handles nombre.LostFocus

    End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.Text
            Case "Hombre"
                foto.Image = Nothing
                foto.BackgroundImage = SICDIN30.My.Resources.Resources.hombre
                user.foto = "Hombre"
            Case "Mujer"
                foto.Image = Nothing
                foto.BackgroundImage = SICDIN30.My.Resources.Resources.mujeruser
                user.foto = "Mujer"
            Case "Carro"
                foto.Image = Nothing
                foto.BackgroundImage = SICDIN30.My.Resources.Resources.carrouser
                user.foto = "Carro"
            Case "Bicicleta"
                foto.Image = Nothing
                foto.BackgroundImage = SICDIN30.My.Resources.Resources.bicicleta
                user.foto = "Bicicleta"
            Case "Flor"
                foto.Image = Nothing
                foto.BackgroundImage = SICDIN30.My.Resources.Resources.floruser
                user.foto = "Flor"
            Case "Buscar"
                If buscar.ShowDialog() = DialogResult.OK Then
                    foto.ImageLocation = buscar.FileName
                    foto.SizeMode = PictureBoxSizeMode.Zoom
                    foto.BackgroundImage = Nothing
                    user.foto = buscar.FileName
                Else
                    ComboBox1.SelectedIndex = 1
                End If
        End Select

    End Sub
End Class