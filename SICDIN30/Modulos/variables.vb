Imports Microsoft.VisualBasic.CompilerServices
Module variables
    Public trdef As Integer = 1
    Public tienda As lugar
    Public artmodified As Date
    Public pathbackup As String

    Public asd As AutoCompleteStringCollection = New AutoCompleteStringCollection ''autocompletar articulos
    Public asm As AutoCompleteStringCollection = New AutoCompleteStringCollection ''autocompletar articulos claves
    Public asma As AutoCompleteStringCollection = New AutoCompleteStringCollection ''autocompletar articulos claves altas
    Public asda As AutoCompleteStringCollection = New AutoCompleteStringCollection ''autocompletar articulos altas
    Public aclnt As AutoCompleteStringCollection = New AutoCompleteStringCollection ''autocompletar articulos claves
    Public fcode As Font
    Public ftachado As Font = New Font("Arial", 16, FontStyle.Strikeout)
    Public fnottachado As Font = New Font("Arial", 16)
    Public catar As catalogo = New catalogo
    Public lectorbasebusy As Boolean = False
    Public blockexit As Boolean = False
    Public nart As Integer = 14 ''cantidad maxima de articulos por hoja
    Public nrep As Integer = 7 ''cantidad maxima de articulos en la reparación

    Public user As usuario
    ''Public idti As Integer = 1 ''identificador tienda
    ''Public tienda As String = "Casa Licona"
    Public idti As Integer = 2
    ' Public tienda As String = "Ally's Store"
    Public sep As String = "','" ''separador para base de datos
    ''Public cloudcl As String = "" ''variables de la dirección de las nubes
    ''Public cloudab As String = ""
    Public cloudwrite As String
    Public cloudread As String
    Public cloudpath As String
    Public icono As Icon

    Public notab As Boolean = False

    ''Public file As System.IO.StreamWriter 'streamer del creador de la nube

    '    Public ex1 As String = ""

    'variables de multiventa
    Public ventarep() As Boolean = {True, True, True, True, True}
    Public ventalisopen As Boolean = False
    Public totalventasel(5) As Decimal
    Public adeudolabelsel(5) As Label
    Public subtotallabelsel(5) As Label
    Public totallabelsel(5) As Label
    Public panelmenusel(5) As Object
    Public panelfondosel(5) As Object
    Public botonsel(5) As Object
    Public botonaddcsel(5) As Object
    Public dgventasel(5) As Object
    Public mayoreosel(5) As Boolean
    Public especialsel(5) As Boolean
    Public adeudosel(5) As Decimal
    Public creditosel(5) As Decimal
    Public subtotalsel(5) As Decimal
    Public clienteid(5) As Integer
    Public idventopen(5) As Integer
    ' Public adeudo As Decimal
    Public diaventa As diatrabajo
    Public Const datedefault As Date = #10/15/1967#

    Public codeprint As Boolean = True
    Public nventas As Integer = 0
    Public nventtotal As Integer = 5
    Public nidvent(nventtotal) As Boolean

    Public colorcodes() As Color = {Color.White, Color.FromName("Linen"), Color.FromName("Mistyrose"), Color.FromName("InactiveCaption"), Color.FromName("Controldark")}

    Public notifier As notificacion

    Public dgartcopy As copyarts = New copyarts

    Public versionNumber As Version
End Module
