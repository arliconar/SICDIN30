
Module database
    ''conexiones

    Public conart As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\sc3\art.dat;Persist Security Info=True;Jet OLEDB:Database Password=sganeo4")
    Public consys As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\sc3\syste.dat;Persist Security Info=True;Jet OLEDB:Database Password=sganeo4")
    Public conclnt As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\sc3\clnt.dat;Persist Security Info=True;Jet OLEDB:Database Password=sganeo4")
    Public convent As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\sc3\venta.dat;Persist Security Info=True;Jet OLEDB:Database Password=vsganeo5")
    Public contra As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\sc3\trabajadores.dat;Persist Security Info=True;Jet OLEDB:Database Password=vsganeo5")
    ' Public condummy As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\sc3\basedummynube.dat;Persist Security Info=True;Jet OLEDB:Database Password=vsganeo5")
    Public conbicrep As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\sc3\bicirep.dat;Persist Security Info=True;Jet OLEDB:Database Password=vsganeo5")
    Public conusu As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\sc3\usu.dat;Persist Security Info=True;Jet OLEDB:Database Password=vsganeo5")
    Public pathimage As String = "C:\sc3\Fotos\"
    Public pathimageusers As String = "C:\sc3\fotousers\"

    ''Public conartcloud As OleDb.OleDbConnection


    ''variables
    Public orden As OleDb.OleDbCommand
    Public lector As OleDb.OleDbDataReader

    Public orden2 As OleDb.OleDbCommand
    Public lector2 As OleDb.OleDbDataReader

    Public orden3 As OleDb.OleDbCommand
    Public lector3 As OleDb.OleDbDataReader

    Public lastupateart As Date = New Date(2020, 7, 28, 15, 0, 0)
    Public lastupateartfuc As Date = New Date(2020, 7, 28, 15, 0, 0)

End Module
