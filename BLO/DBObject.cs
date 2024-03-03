using Dapper;
using MySql.Data.MySqlClient;
using Nest;
using System.Data;
using System.Diagnostics;

namespace BLO
{
    public class DBObject : IDisposable
    {
        private const string connectionString = "server=localhost;port=3306;database=cdn_project;user=root;password=;";

        private string Connect { get; set; } = connectionString;

        private MySqlConnection? sql { get; set; } = null;

        public void Set(string connection = "")
        {
            if (!string.IsNullOrEmpty(connection))
            {
                Connect = connection;
                Dispose();
                Main();
            }
        }

        public void Reset()
        {
            Connect = connectionString;
        }

        public MySqlConnection DB => sql;

        public DBObject(string Con = connectionString)
        {
            Connect = Con;
            Main();
        }

        public DBObject()
        {
            Connect = connectionString;

            Main();
        }

        MySqlConnection Main()
        {
            sql = new MySqlConnection(Connect);
            return sql;
        }

        public void Dispose() { sql?.Close(); sql?.Dispose(); }
    }
}
