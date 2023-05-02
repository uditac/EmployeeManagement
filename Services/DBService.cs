using Dapper;
using Npgsql;
using System.Data;

namespace EmployeeManagement.Services
{
    public class DBService 
    {
        private readonly IDbConnection _db;

        public DBService(IConfiguration config)
        {
            _db = new NpgsqlConnection(config.GetConnectionString("PostgreSQL"));
        }
    }
}
