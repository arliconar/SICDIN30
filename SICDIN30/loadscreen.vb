Public Class loadscreen
    Public Sub loader(valmax As Integer, steper As Integer)
        pbar.Maximum = valmax
        pbar.Step = steper
        maintrans()
        Me.Show()

    End Sub
    Public Sub unloadme()
        mainnormal()
        Me.Close()

    End Sub
    Public Sub newtext(st As String)
        loadtext.Text = "Cargando:" & st
    End Sub
    Public Sub stepp()
        pbar.PerformStep()
    End Sub
    Private Sub loadscreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class