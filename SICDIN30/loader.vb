Imports System.Threading

Public Class loader
    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub

    Private Sub loader_Load(sender As Object, e As EventArgs) Handles Me.Load
        initvarvent()
        conart.Open()
        consys.Open()
        conclnt.Open()
        convent.Open()
        contra.Open()

        conbicrep.Open()

        autocompletarclnt()
        icono = New Icon("C:\sc3\resources\icono1.ico")
        orden = New OleDb.OleDbCommand("SELECT  * FROM syste", consys)
        lector = orden.ExecuteReader
        While lector.Read
            idti = lector("Tienda")
            Dim stcode As String = voiddeleterstring(lector("Fontbarcode"))
            If stcode <> "" Then
                fcode = New Font(stcode, 24)
                codeprint = True

            Else
                codeprint = False

            End If
            pathbackup = lector("pathback")

            cloudpath = lector("cloudpath")
            pathimage = lector("imagenespath")
            Select Case idti
                Case 0 ''caso maestro

                    'ex1 = "ECasa"
                    'ex2 = "EAllys"
                    icono = New Icon("C:\sc3\resources\icono.ico")

                Case 1
                    '   ex1 = "ECasa"
                    '  ex2 = "EAllys"
                    icono = New Icon("C:\sc3\resources\icono1.ico")

                Case 2
                    ' ex2 = "ECasa"
                    '   ex1 = "EAllys"
                    icono = New Icon("C:\sc3\resources\icono2.ico")
            End Select
        End While
        tienda = New lugar
        tienda.load(idti)

        diaventa = New diatrabajo

        diaventa.abrirdia(user)
        main.Icon = icono
        main.Show()
        main.Text = "SICDIN 3.0: " & user.nombre
        lector.Close()
        ''Cargamos el  lugar

        main.mainloadfirts()


        Me.Close()

    End Sub
End Class
