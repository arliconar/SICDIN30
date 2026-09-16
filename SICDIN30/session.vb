Imports System.Reflection
Public Class session
    Dim botonuser() As Button
    Dim widhttotal As Integer = 0
    Private onuser As Boolean = False
    Private activeusera As usuario
    Dim iconu As Image
    Public firttime As Boolean = True

    Private Sub clickbutton(sender As Button, e As EventArgs)
        Dim id As Integer = sender.Tag
        activeusera = New usuario
        activeusera.loadid(id)
        With panelusu.Controls
            For i = 0 To panelusu.Controls.Count - 1
                If .Item(i).Tag <> id Then
                    .Item(i).Hide()
                End If
            Next
        End With
        panelbuttons.Visible = True
        panelbuttons.Location = New Point(sender.Location.X + sender.Width + 10, panelbuttons.Location.Y)
        Panelleft.Width = (Me.Width - sender.Width - panelbuttons.Width - 40) / 2
        Panelri.Width = (Me.Width - sender.Width - panelbuttons.Width - 40) / 2
        iconu = sender.BackgroundImage
        onuser = True
        passbox.Focus()
        passbox.Text = ""

    End Sub
    Private Sub loadusers()
        Try
            Dim st As String = "SELECT Id,Nombre,pass,Foto FROM Usuario WHERE Activo=TRUE ORDER BY FU DESC"
            If firttime Then
                Try
                    conusu.Open()
                Catch exConn As Exception
                    LogAppError(exConn, "session.loadusers.conusu.Open")
                    MessageBox.Show("No se pudo conectar a la base de usuarios (C:\sc3\usu.dat):" & vbCrLf & exConn.Message, "Error de Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If

            Dim i As Integer = 0
            activeusera = New usuario

            SyncLock conusu
                Using cmd As New OleDb.OleDbCommand(st, conusu)
                    Using rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()
                        While rdr.Read()
                            ReDim Preserve botonuser(i)
                            botonuser(i) = New Button

                            With botonuser(i)
                                .TextAlign = ContentAlignment.BottomCenter
                                Select Case rdr("Foto").ToString()
                                    Case "Hombre"
                                        .BackgroundImage = SICDIN30.My.Resources.hombre
                                    Case "Mujer"
                                        .BackgroundImage = SICDIN30.My.Resources.mujeruser
                                    Case "Flor"
                                        .BackgroundImage = SICDIN30.My.Resources.floruser
                                    Case "Carro"
                                        .BackgroundImage = SICDIN30.My.Resources.carrouser
                                    Case "Bicicleta"
                                        .BackgroundImage = SICDIN30.My.Resources.bicicleta
                                    Case Else
                                        Dim fotoPath As String = rdr("Foto").ToString()
                                        If System.IO.File.Exists(fotoPath) Then
                                            Try
                                                Dim imagen As Image = Image.FromFile(fotoPath)
                                                .BackgroundImage = imagen
                                            Catch
                                                .BackgroundImage = SICDIN30.My.Resources.hombre
                                            End Try
                                        Else
                                            .BackgroundImage = SICDIN30.My.Resources.hombre
                                        End If
                                End Select
                                .BackgroundImageLayout = ImageLayout.Zoom
                                .Text = rdr("Nombre").ToString()
                                .Tag = rdr("Id")
                                panelusu.Controls.Add(botonuser(i))
                                .Dock = DockStyle.Left
                                .FlatStyle = FlatStyle.Flat
                                .BringToFront()

                                widhttotal += .Width
                            End With

                            AddHandler botonuser(i).Click, AddressOf clickbutton
                            i += 1
                        End While
                    End Using
                End Using
            End SyncLock
            Panelleft.Width = (Me.Width - widhttotal - 40) / 2
            Panelri.Width = (Me.Width - widhttotal - 40) / 2

        Fechalabel.Text = Now.ToShortDateString

        Select Case Now.Month
            Case 1
                If Now.Day < 5 Then
                    imagen.BackgroundImage = SICDIN30.My.Resources.Resources.navidad
                End If
                If Now.Day = 5 Or Now.Day = 6 Then
                    imagen.BackgroundImage = SICDIN30.My.Resources.Resources.reyes

                End If
            Case 2
                If Now.Day = 2 Then
                    imagen.BackgroundImage = SICDIN30.My.Resources.Resources.tamal
                End If
                If Now.Day = 24 Then
                    imagen.BackgroundImage = SICDIN30.My.Resources.Resources.bandera

                End If
            Case 9
                If Now.Day = 15 Or Now.Day = 16 Then
                    imagen.BackgroundImage = SICDIN30.My.Resources.Resources.bandera

                End If

            Case 10
                If Now.Day = 31 Then
                    imagen.BackgroundImage = SICDIN30.My.Resources.Resources.halloween

                End If
            Case 11
                If Now.Day = 1 Or Now.Day = 2 Then
                    imagen.BackgroundImage = SICDIN30.My.Resources.Resources.halloween

                End If
                If Now.Day = 20 Then
                    imagen.BackgroundImage = SICDIN30.My.Resources.Resources.bandera
                End If
            Case 12
                imagen.BackgroundImage = SICDIN30.My.Resources.Resources.navidad

        End Select
        Catch ex As Exception
            LogAppError(ex, "session.loadusers")
        End Try
    End Sub
    Private Sub session_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadusers()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not firttime Then
            backinup()
        Else
            End
        End If



    End Sub

    Private Sub passbox_TextChanged(sender As Object, e As EventArgs) Handles passbox.TextChanged

    End Sub

    Private Sub cancel_Click(sender As Object, e As EventArgs) Handles cancel.Click
        passbox.Text = ""
        onuser = False
        For i = 0 To panelusu.Controls.Count - 1
            panelusu.Controls.Item(i).Show()
        Next
        panelbuttons.Visible = False
        Panelleft.Width = (Me.Width - widhttotal - 40) / 2
        Panelri.Width = (Me.Width - widhttotal - 40) / 2


    End Sub
    Private Sub okpress()
        If passbox.Text = activeusera.pass Then
            user = New usuario
            user = activeusera

            user.login()
            If firttime Then
                loader.Show()
            Else
                main.loadadmin()
                main.Text = "SICDIN 3.0: " & user.nombre
                Me.DialogResult = DialogResult.OK
                If Not user.admin Then
                    main.ppos.Controls.Item("panelcat").BringToFront()
                End If


            End If


            Me.Close()
        Else
            dialogs.advertencia("Contraseña incorrecta")
            maintrans()
            passbox.SelectAll()
            passbox.Focus()
        End If
    End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click
        okpress()
    End Sub

    Private Sub passbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles passbox.KeyPress
        If e.KeyChar = Chr(13) Then
            okpress()
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub panelusu_Paint(sender As Object, e As PaintEventArgs) Handles panelusu.Paint

    End Sub

    Private Sub session_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim npress As Integer = Asc(e.KeyChar)
        If onuser = False Then
            npress -= 48
            If npress >= 1 And npress <= 9 Then
                If npress <= botonuser.Count Then
                    botonuser(npress - 1).PerformClick()
                    e.KeyChar = ""
                End If
            End If
        Else
            If npress = 27 Then
                cancel.PerformClick()

            End If
        End If
    End Sub
End Class