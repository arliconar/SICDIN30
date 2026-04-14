Namespace My
    ' Los siguientes eventos están disponibles para MyApplication:
    ' Inicio: Se genera cuando se inicia la aplicación, antes de que se cree el formulario de inicio.
    ' Apagado: Se genera después de haberse cerrado todos los formularios de aplicación.  Este evento no se genera si la aplicación termina de forma anómala.
    ' UnhandledException: Se genera si la aplicación encuentra una excepción no controlada.
    ' StartupNextInstance: Se genera cuando se inicia una aplicación de instancia única y dicha aplicación está ya activa. 
    ' NetworkAvailabilityChanged: Se genera cuando se conecta o desconecta la conexión de red.
    Partial Friend Class MyApplication
        Private Sub MyApplication_UnhandledException(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            ' Evita que el programa se cierre
            e.ExitApplication = False

            Dim exMsg As String = e.Exception.Message
            ' Comprueba si el error proviene de una operación de base de datos OleDb
            If TypeOf e.Exception Is System.Data.OleDb.OleDbException Then
                System.Windows.Forms.MessageBox.Show("Ocurrió un error en la conexión o consulta a la base de datos:" & vbCrLf & vbCrLf & exMsg, "Error de Base de Datos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
            Else
                System.Windows.Forms.MessageBox.Show("Ocurrió un error inesperado en el sistema:" & vbCrLf & vbCrLf & exMsg, "Error del Sistema", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        End Sub
    End Class
End Namespace
