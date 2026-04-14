Public Class sql
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ejec.Click
        Dim contemp As OleDb.OleDbConnection
        If sqlorder.Text = "" Then
            Exit Sub
        End If
        If art.Checked Then
            contemp = conart
        ElseIf cli.Checked Then
            contemp = conclnt
        ElseIf ven.Checked Then
            contemp = convent
        ElseIf rep.Checked Then
            contemp = conbicrep
        ElseIf usu.Checked Then
            contemp = conusu
        Else
            contemp = consys
        End If
        Try
            Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(sqlorder.Text, contemp)
            orden.ExecuteNonQuery()
            dialogs.ok("Instrucción ejecutada sin problemas")
            ErrorProvider1.SetError(ejec, "")
        Catch ex As Exception
            ErrorProvider1.SetError(ejec, "Ocurrio un error en la ejecución de la orden")
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = vbOK

    End Sub
End Class