Public Class Formulas
    Public Function loader() As String
        maintrans()
        Dim st1 As String = ""
        Dim st2 As String = ""
        If Me.ShowDialog() = DialogResult.OK Then
            If pmen.Checked Then
                st1 = "$"
            End If
            If pmay.Checked Then
                st1 = "$$"
            End If
            If exis.Checked Then
                st1 = "#"
            End If
            If less.Checked Then
                st2 = "<"
            End If
            If equal.Checked Then
                st2 = "="
            End If
            If greater.Checked Then
                st2 = ">"
            End If
        End If
        loader = st1 & st2
        mainnormal()
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class