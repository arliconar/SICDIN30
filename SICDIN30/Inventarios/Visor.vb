Imports Microsoft.VisualBasic.Devices

Public Class Visor
    Public control As Boolean
    Dim icurrent As Integer
    Private stimagenes As String()
    Public Sub loaderimage(imagen As Image)
        Me.WindowState = FormWindowState.Normal
        foto.Image = imagen
        foto.SizeMode = PictureBoxSizeMode.Zoom

        Me.ShowDialog()
        Me.Close()
        foto.Image.Dispose()

    End Sub
    Public Sub loaderimagepath(path As String)
        foto.ImageLocation = path
        foto.SizeMode = PictureBoxSizeMode.Zoom

        Me.ShowDialog()
        Me.Close()
        foto.ImageLocation = ""

    End Sub
    Public Sub loader(clave As String)
        Dim iload As Boolean = False
        If My.Computer.FileSystem.DirectoryExists(pathimage & clave) Then
            stimagenes = IO.Directory.GetFiles(pathimage & clave)
            siguiente.Show()
            iload = True
        Else
            ReDim stimagenes(0)
            If My.Computer.FileSystem.FileExists(pathimage & clave & ".png") Then
                stimagenes(0) = pathimage & clave & ".png"
                iload = True
            ElseIf My.Computer.FileSystem.FileExists(pathimage & clave & ".jpg") Then
                stimagenes(0) = pathimage & clave & ".jpg"
                iload = True
            ElseIf My.Computer.FileSystem.FileExists(pathimage & clave & ".bmp") Then
                stimagenes(0) = pathimage & clave & ".bmp"
                iload = True
            End If

        End If
        If iload Then
            foto.ImageLocation = stimagenes(0)
            icurrent = 0
            foto.SizeMode = PictureBoxSizeMode.Zoom
            Me.ShowDialog()
            Me.Close()
        End If
    End Sub

    Private Sub foto_Click(sender As Object, e As EventArgs) Handles foto.Click

    End Sub

    Private Sub okv_Click(sender As Object, e As EventArgs) Handles okv.Click

        foto.ImageLocation = ""


    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Panel3_MouseClick(sender As Object, e As MouseEventArgs) Handles Panel3.MouseClick


    End Sub

    Private Sub Panel3_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel3.MouseMove

    End Sub

    Private Sub foto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles foto.KeyPress

    End Sub



    Private Sub copiar_Click(sender As Object, e As EventArgs) Handles copiar.Click
        Try
            Clipboard.SetImage(foto.Image)
        Catch ex As Exception

        End Try



    End Sub

    Private Sub copiar_MouseLeave(sender As Object, e As EventArgs) Handles copiar.MouseLeave, okv.MouseLeave
        hidenamebutton(sender)
    End Sub

    Private Sub okv_MouseEnter(sender As Object, e As EventArgs) Handles okv.MouseEnter, copiar.MouseEnter
        shownamebutton(sender)
    End Sub
    Private Sub buttonsfit()
        okv.Size = New Size((Me.Size.Width / 2) - Panel1.Size.Width, okv.Size.Height)


    End Sub
    Private Sub foto_DoubleClick(sender As Object, e As EventArgs) Handles foto.DoubleClick
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
            buttonsfit()
        Else
            Me.WindowState = FormWindowState.Normal
            buttonsfit()
        End If

    End Sub
    Private Sub nextpic() Handles siguiente.Click
        If stimagenes.Count = 1 Then
            Exit Sub
        End If

        icurrent += 1
        If icurrent > stimagenes.Count - 1 Then
            icurrent = stimagenes.Count - 1
            Exit Sub
        End If
        foto.ImageLocation = stimagenes(icurrent)
        foto.SizeMode = PictureBoxSizeMode.Zoom
        anterior.Show()
        If icurrent = stimagenes.Count - 1 Then
            siguiente.Hide()
        End If

    End Sub


    Private Sub lastpic() Handles anterior.Click
        If stimagenes.Count = 1 Then
            Exit Sub
        End If
        icurrent -= 1
        If icurrent < 0 Then
            icurrent = 0
            Exit Sub
        End If
        foto.ImageLocation = stimagenes(icurrent)
        foto.SizeMode = PictureBoxSizeMode.Zoom
        siguiente.Show()
        If icurrent = 0 Then
            anterior.Hide()
        End If

    End Sub

    Private Sub foto_KeyUp(sender As Object, e As KeyEventArgs) Handles foto.KeyUp

    End Sub

    Private Sub Visor_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Select Case e.KeyCode
            Case Keys.Right
                nextpic()
                okv.Focus()

            Case Keys.Left
                lastpic()
                okv.Focus()

        End Select
    End Sub
End Class