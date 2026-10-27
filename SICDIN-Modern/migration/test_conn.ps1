$cs = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\sc3\syste.dat;Jet OLEDB:Database Password=sganeo4"
$c = New-Object System.Data.OleDb.OleDbConnection($cs)
try {
    $c.Open()
    Write-Output "Conexion exitosa con ACE 16.0!"
    $tables = $c.GetSchema("Tables")
    foreach ($row in $tables.Rows) {
        if ($row["TABLE_TYPE"] -eq "TABLE") {
            Write-Output "Tabla: $($row['TABLE_NAME'])"
        }
    }
    $c.Close()
} catch {
    Write-Output "Error: $($_.Exception.Message)"
}
