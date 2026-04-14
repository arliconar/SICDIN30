Public Class wait
    Public Sub loader()
        Timer1.Start()

        maintrans()

        Me.ShowDialog()
        mainnormal()


    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        mainnormal()
        Me.DialogResult = DialogResult.Cancel


    End Sub

    Private Sub wait_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Not blockexit Then

            backinup()


        End If
    End Sub

    Private Sub Button1_MouseHover(sender As Object, e As EventArgs) Handles Button1.MouseHover

    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        shownamebutton(sender)
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        hidenamebutton(sender)
    End Sub
End Class