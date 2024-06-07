using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SQLite;

namespace MyStorageApplication.Database.UoW
{
    public class DatabaseSession
    {        
        public IDbConnection Connection { get; }
        public IDbTransaction? Transaction { get; set; }

        public DatabaseSession(IConfiguration configuration)
        {
            Connection = new SQLiteConnection
            {
                ConnectionString = configuration.GetConnectionString("SqliteConnection")
            };
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
