Public Class dialogs
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click


    End Sub
    Public Sub detectnewlines(st As String)
        Dim k As Integer = 0
        For i = 0 To st.Count - 1
            If st(i) = Chr(13) Then
                k += 1
            End If
        Next
        If k > 2 Then
            Me.Height = Me.Height + (Label1.Height * (k - 2))
        End If
    End Sub
    Public Function ok(st As String, Optional najuste As Integer = 0) As DialogResult
        main.Opacity = 0.85
        detectnewlines(st)
        Label1.Text = st
        Dim originalh As Integer = 258
        Me.Size = New Size(Label1.Location.X + Label1.Size.Width + 20, originalh + Label1.Size.Height - 100)
        Me.CenterToScreen()
        Dim wform As Integer = Me.Size.Width / 3
        Panel8.Size = New Size(wform + najuste, Panel8.Height)
        Panel9.Size = New Size(wform + najuste, Panel9.Height)
        PictureBox1.BackgroundImage = SICDIN30.My.Resources.Resources.vcsnormal_93488
        siboton.Hide()
        noboton.Hide()
        aceptarboton.Show()
        aceptarboton.DialogResult = DialogResult.Yes
        Me.Text = "SICDIN 3.0"
        ok = Me.ShowDialog
        main.Opacity = 1
    End Function
    Public Function pregunta(question As String, Optional najuste As Integer = 100) As DialogResult
        main.Opacity = 0.85
        detectnewlines(question)

        Label1.Text = question
        Me.Size = New Size(Label1.Location.X + Label1.Size.Width + 20, Me.Size.Height)
        Me.CenterToScreen()
        Dim wform As Integer = Me.Size.Width / 3
        Panel8.Size = New Size(wform + najuste, Panel8.Height)
        Panel9.Size = New Size(wform + najuste, Panel9.Height)
        PictureBox1.BackgroundImage = SICDIN30.My.Resources.Resources.dudaemo

        siboton.Show()
        noboton.Show()
        aceptarboton.Hide()
        Me.Text = "SICDIN 3.0:Pregunta"
        pregunta = Me.ShowDialog
        main.Opacity = 1

    End Function
    Public Function advertencia(adver As String, Optional najuste As Integer = 0) As DialogResult
        main.Opacity = 0.85
        detectnewlines(adver)
        Label1.Text = adver
        Dim originalh As Integer = 258
        Me.Size = New Size(Label1.Location.X + Label1.Size.Width + 20, originalh + Label1.Size.Height - 100)
        Me.CenterToScreen()
        Dim wform As Integer = Me.Size.Width / 3
        Panel8.Size = New Size(wform + najuste, Panel8.Height)
        Panel9.Size = New Size(wform + najuste, Panel9.Height)
        PictureBox1.BackgroundImage = SICDIN30.My.Resources.Resources.emoji_enfado
        siboton.Hide()
        noboton.Hide()
        aceptarboton.Show()
        Me.Text = "SICDIN 3.0:Advertencia"
        advertencia = Me.ShowDialog
        Main.Opacity = 1
    End Function
    Private Sub dialogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub siboton_Click(sender As Object, e As EventArgs) Handles siboton.Click

    End Sub

    Private Sub Label1_SizeChanged(sender As Object, e As EventArgs) Handles Label1.SizeChanged

    End Sub
End Class