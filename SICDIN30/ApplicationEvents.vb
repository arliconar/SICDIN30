Namespace My
    ' Los siguientes eventos están disponibles para MyApplication:
    ' Inicio: Se genera cuando se inicia la aplicación, antes de que se cree el formulario de inicio.
    ' Apagado: Se genera después de haberse cerrado todos los formularios de aplicación.  Este evento no se genera si la aplicación termina de forma anómala.
    ' UnhandledException: Se genera si la aplicación encuentra una excepción no controlada.
    ' StartupNextInstance: Se genera cuando se inicia una aplicación de instancia única y dicha aplicación está ya activa. 
    ' NetworkAvailabilityChanged: Se genera cuando se conecta o desconecta la conexión de red.
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            ' Captura de excepciones en el bucle de mensajes de UI
            Try
                AddHandler System.Windows.Forms.Application.ThreadException, AddressOf UIThreadExceptionHandler
            Catch ex As Exception
                LogAppError(ex, "MyApplication_Startup.ThreadException")
            End Try

            ' Registro de excepciones no capturadas a nivel de AppDomain
            Try
                AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf AppDomain_UnhandledException
            Catch ex As Exception
                LogAppError(ex, "MyApplication_Startup.AppDomain")
            End Try

            ' Captura de excepciones no observadas en Tasks asíncronas
            Try
                AddHandler System.Threading.Tasks.TaskScheduler.UnobservedTaskException, AddressOf TaskScheduler_UnobservedTaskException
            Catch ex As Exception
                LogAppError(ex, "MyApplication_Startup.TaskScheduler")
            End Try
        End Sub

        Private Sub UIThreadExceptionHandler(sender As Object, t As System.Threading.ThreadExceptionEventArgs)
            Try
                LogAppError(t.Exception, "Application.ThreadException")
                Dim exMsg As String = If(t.Exception IsNot Nothing, t.Exception.Message, "Error no especificado")
                If TypeOf t.Exception Is System.Data.OleDb.OleDbException Then
                    System.Windows.Forms.MessageBox.Show("Ocurrió un error en la conexión o consulta a la base de datos:" & vbCrLf & vbCrLf & exMsg, "Error de Base de Datos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
                Else
                    System.Windows.Forms.MessageBox.Show("Ocurrió un error en la interfaz del sistema:" & vbCrLf & vbCrLf & exMsg, "Aviso del Sistema", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
                End If
            Catch
            End Try
        End Sub

        Private Sub AppDomain_UnhandledException(sender As Object, e As System.UnhandledExceptionEventArgs)
            Try
                Dim ex As Exception = TryCast(e.ExceptionObject, Exception)
                LogAppError(ex, "AppDomain.UnhandledException (IsTerminating=" & e.IsTerminating.ToString() & ")")
            Catch
            End Try
        End Sub

        Private Sub TaskScheduler_UnobservedTaskException(sender As Object, e As System.Threading.Tasks.UnobservedTaskExceptionEventArgs)
            Try
                LogAppError(e.Exception, "TaskScheduler.UnobservedTaskException")
                e.SetObserved()
            Catch
            End Try
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            ' Evita que el programa se cierre
            e.ExitApplication = False

            Try
                LogAppError(e.Exception, "MyApplication_UnhandledException")
                Dim exMsg As String = If(e.Exception IsNot Nothing, e.Exception.Message, "Error no especificado")
                ' Comprueba si el error proviene de una operación de base de datos OleDb
                If TypeOf e.Exception Is System.Data.OleDb.OleDbException Then
                    System.Windows.Forms.MessageBox.Show("Ocurrió un error en la conexión o consulta a la base de datos:" & vbCrLf & vbCrLf & exMsg, "Error de Base de Datos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
                Else
                    System.Windows.Forms.MessageBox.Show("Ocurrió un error inesperado en el sistema:" & vbCrLf & vbCrLf & exMsg, "Error del Sistema", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
                End If
            Catch
            End Try
        End Sub
    End Class
End Namespace
