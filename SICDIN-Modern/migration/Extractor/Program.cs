using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Extractor;

class Program
{
    record DbTarget(string AccessPath, string Password, string Prefix);

    static void Main(string[] args)
    {
        Console.WriteLine($"[INFO] Extractor iniciado. 64-bit: {Environment.Is64BitProcess}");

        string sqlitePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\..\backend\sicdin.db"));
        string? dir = Path.GetDirectoryName(sqlitePath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        Console.WriteLine($"[INFO] Destino SQLite: {sqlitePath}");

        var databases = new List<DbTarget>
        {
            new(@"C:\sc3\art.dat", "sganeo4", "art_"),
            new(@"C:\sc3\clnt.dat", "sganeo4", "clnt_"),
            new(@"C:\sc3\syste.dat", "sganeo4", "sys_"),
            new(@"C:\sc3\usu.dat", "vsganeo5", "usu_"),
            new(@"C:\sc3\trabajadores.dat", "vsganeo5", "trab_"),
            new(@"C:\sc3\venta.dat", "vsganeo5", "vent_"),
            new(@"C:\sc3\bicirep.dat", "vsganeo5", "rep_")
        };

        // Si ya existe el archivo, lo eliminamos para crear una base limpia y con nombres sin colisión
        if (File.Exists(sqlitePath))
        {
            File.Delete(sqlitePath);
        }

        using var sqliteConn = new SqliteConnection($"Data Source={sqlitePath}");
        sqliteConn.Open();

        foreach (var db in databases)
        {
            if (!File.Exists(db.AccessPath))
            {
                Console.WriteLine($"[WARN] Archivo no encontrado: {db.AccessPath}");
                continue;
            }

            Console.WriteLine($"\n==========================================");
            Console.WriteLine($"Procesando: {db.AccessPath} (Prefijo: {db.Prefix})");
            Console.WriteLine($"==========================================");
            MigrateAccessToSqlite(db.AccessPath, db.Password, db.Prefix, sqliteConn);
        }

        // Crear vistas o alias amigables para las tablas clave
        CreateViews(sqliteConn);

        Console.WriteLine("\n[EXITO] Migración finalizada y vistas creadas.");
    }

    static void MigrateAccessToSqlite(string accessPath, string password, string prefix, SqliteConnection sqliteConn)
    {
        string cs = $"Provider=Microsoft.ACE.OLEDB.16.0;Data Source={accessPath};Persist Security Info=True;Jet OLEDB:Database Password={password};";
        OleDbConnection? accessConn = null;

        try
        {
            var conn = new OleDbConnection(cs);
            conn.Open();
            accessConn = conn;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Conexión fallida: {ex.Message}");
            return;
        }

        using (accessConn)
        {
            DataTable tables = accessConn.GetSchema("Tables");
            foreach (DataRow row in tables.Rows)
            {
                string tableType = row["TABLE_TYPE"]?.ToString() ?? "";
                if (tableType.Equals("TABLE", StringComparison.OrdinalIgnoreCase))
                {
                    string tableName = row["TABLE_NAME"]?.ToString() ?? "";
                    if (tableName.StartsWith("MSys") || tableName.StartsWith("~")) continue;

                    string targetTableName = prefix + tableName.Replace(" ", "_").Replace("-", "_").ToLower();
                    CopyTable(accessConn, tableName, targetTableName, sqliteConn);
                }
            }
        }
    }

    static void CopyTable(OleDbConnection accessConn, string sourceTableName, string targetTableName, SqliteConnection sqliteConn)
    {
        Console.WriteLine($"  -> Migrando [{sourceTableName}] a [{targetTableName}]");
        try
        {
            using var cmd = new OleDbCommand($"SELECT * FROM [{sourceTableName}]", accessConn);
            using var reader = cmd.ExecuteReader();
            var schemaTable = reader.GetSchemaTable();
            if (schemaTable == null) return;

            var colDefs = new List<string>();
            var colNames = new List<string>();
            var paramNames = new List<string>();

            int colIndex = 0;
            foreach (DataRow colRow in schemaTable.Rows)
            {
                string rawColName = colRow["ColumnName"].ToString()!;
                // Normalizar tildes y caracteres extraños en nombres de columna
                string colName = rawColName
                    .Replace(" ", "_")
                    .Replace("-", "_")
                    .Replace("ó", "o")
                    .Replace("Ó", "O")
                    .Replace("í", "i")
                    .Replace("Í", "I")
                    .Replace("á", "a")
                    .Replace("Á", "A")
                    .Replace("é", "e")
                    .Replace("É", "E")
                    .Replace("ú", "u")
                    .Replace("Ú", "U")
                    .Replace("ñ", "n")
                    .Replace("Ñ", "N");

                Type dataType = (Type)colRow["DataType"];

                string colType = "TEXT";
                if (dataType == typeof(int) || dataType == typeof(short) || dataType == typeof(long) || dataType == typeof(byte) || dataType == typeof(bool))
                {
                    colType = "INTEGER";
                }
                else if (dataType == typeof(decimal) || dataType == typeof(double) || dataType == typeof(float))
                {
                    colType = "NUMERIC";
                }

                colDefs.Add($"[{colName}] {colType}");
                colNames.Add($"[{colName}]");
                paramNames.Add($"@p{colIndex}");
                colIndex++;
            }

            string createSql = $"CREATE TABLE [{targetTableName}] ({string.Join(", ", colDefs)});";
            using (var createCmd = new SqliteCommand(createSql, sqliteConn))
            {
                createCmd.ExecuteNonQuery();
            }

            using var tx = sqliteConn.BeginTransaction();
            string insertSql = $"INSERT INTO [{targetTableName}] ({string.Join(", ", colNames)}) VALUES ({string.Join(", ", paramNames)})";
            using var insertCmd = new SqliteCommand(insertSql, sqliteConn, tx);

            for (int i = 0; i < colIndex; i++)
            {
                insertCmd.Parameters.Add(new SqliteParameter($"@p{i}", DbType.Object));
            }

            int count = 0;
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    object val = reader.GetValue(i);
                    if (val is DateTime dt)
                    {
                        insertCmd.Parameters[i].Value = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else if (val is DBNull)
                    {
                        insertCmd.Parameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        insertCmd.Parameters[i].Value = val;
                    }
                }
                insertCmd.ExecuteNonQuery();
                count++;
            }

            tx.Commit();
            Console.WriteLine($"     [OK] {count} registros copiados.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"     [ERROR] Falló en [{sourceTableName}]: {ex.Message}");
        }
    }

    static void CreateViews(SqliteConnection sqliteConn)
    {
        Console.WriteLine("\n[INFO] Creando vistas principales...");
        string[] views = [
            "CREATE VIEW IF NOT EXISTS v_articulos AS SELECT * FROM art_articulos;",
            "CREATE VIEW IF NOT EXISTS v_clientes AS SELECT * FROM clnt_client;",
            "CREATE VIEW IF NOT EXISTS v_usuarios AS SELECT * FROM usu_usuario;",
            "CREATE VIEW IF NOT EXISTS v_ventas AS SELECT * FROM vent_venta;",
            "CREATE VIEW IF NOT EXISTS v_ventas_detalle AS SELECT * FROM vent_lv;",
            "CREATE VIEW IF NOT EXISTS v_reparaciones AS SELECT * FROM rep_bici;",
            "CREATE VIEW IF NOT EXISTS v_lugares AS SELECT * FROM sys_lugares;"
        ];

        foreach (var sql in views)
        {
            using var cmd = new SqliteCommand(sql, sqliteConn);
            cmd.ExecuteNonQuery();
        }
    }
}
