Public Class copiador
    Public descripcion As New List(Of String)
    Public nombre As New List(Of String)
    Public i As Integer

    Public Sub New()
        descripcion = New List(Of String)
        nombre = New List(Of String)
        Dim st As String = "SELECT * FROM Copy"
        Dim orden As OleDb.OleDbCommand = New OleDb.OleDbCommand(st, consys)
        Dim lector As OleDb.OleDbDataReader = orden.ExecuteReader
        While lector.Read
            descripcion.Add(lector("Descripcion"))
            nombre.Add(lector("Texto"))
        End While
        lector.Close()
        i = descripcion.Count

    End Sub
    Public Sub copy(text As String)

        Clipboard.SetText(nombre(descripcion.IndexOf(text)))

    End Sub
End Class
