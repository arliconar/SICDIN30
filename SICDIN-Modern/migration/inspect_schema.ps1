$dbs = @(
    @{ Path = "C:\sc3\art.dat"; Pwd = "sganeo4" },
    @{ Path = "C:\sc3\clnt.dat"; Pwd = "sganeo4" },
    @{ Path = "C:\sc3\syste.dat"; Pwd = "sganeo4" },
    @{ Path = "C:\sc3\usu.dat"; Pwd = "vsganeo5" },
    @{ Path = "C:\sc3\trabajadores.dat"; Pwd = "vsganeo5" },
    @{ Path = "C:\sc3\venta.dat"; Pwd = "vsganeo5" },
    @{ Path = "C:\sc3\bicirep.dat"; Pwd = "vsganeo5" }
)

foreach ($db in $dbs) {
    if (Test-Path $db.Path) {
        Write-Output "=== Analizando $($db.Path) ==="
        $connStr = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=$($db.Path);Persist Security Info=True;Jet OLEDB:Database Password=$($db.Pwd)"
        try {
            $conn = New-Object System.Data.OleDb.OleDbConnection($connStr)
            $conn.Open()
            $tables = $conn.GetSchema("Tables")
            foreach ($row in $tables.Rows) {
                if ($row["TABLE_TYPE"] -eq "TABLE") {
                    $tName = $row["TABLE_NAME"]
                    Write-Output " Tabla: $tName"
                    $cols = $conn.GetSchema("Columns", @($null, $null, $tName, $null))
                    foreach ($c in $cols.Rows) {
                        Write-Output "   - $($c['COLUMN_NAME']) ($($c['DATA_TYPE']))"
                    }
                }
            }
            $conn.Close()
        } catch {
            Write-Output " Error conectando a $($db.Path): $_"
        }
    } else {
        Write-Output " No existe $($db.Path)"
    }
}
