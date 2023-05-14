using Microsoft.Data.Sqlite;
using System.IO;
using System.Threading.Tasks;

namespace SQLiteEncrypter.DAL
{
    public class Database
    {
        public async Task Encrypt(string path, string password)
        {
            string name = Path.GetFileName(path);

            string connectionString = new SqliteConnectionStringBuilder(path)
            {
                Mode = SqliteOpenMode.ReadWriteCreate,
                Cache = SqliteCacheMode.Shared
            }.ToString();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = $@"ATTACH DATABASE '{name}' AS encrypted KEY '{password}';";
                command.CommandText += $@"SELECT sqlcipher_export('encrypted');";
                command.CommandText += $@"DETACH DATABASE encrypted;";

                await command.ExecuteNonQueryAsync();

                SqliteConnection.ClearAllPools();
            }
        }

        public async Task Decrypt(string path, string password)
        {
            string name = Path.GetFileName(path);

            string connectionString = new SqliteConnectionStringBuilder(path)
            {
                Mode = SqliteOpenMode.ReadWriteCreate,
                Cache = SqliteCacheMode.Shared
            }.ToString();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = $@"PRAGMA key = '{password}'";
                command.CommandText += $@"ATTACH DATABASE '{name}' AS decrypted KEY '';";
                command.CommandText += $@"SELECT sqlcipher_export('decrypted');";
                command.CommandText += $@"DETACH DATABASE decrypted;";

                await command.ExecuteNonQueryAsync();

                SqliteConnection.ClearAllPools();
            }
        }
    }
}
