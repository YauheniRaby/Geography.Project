using Geography.DAL.Repositories.Abstract;
using System.Data.Common;
using System.Data.SqlClient;

namespace Geography.DAL.Repositories
{
    public class SqlServerConnectionProvider : ISqlServerConnectionProvider
    {
        private readonly string _connectionString;

        public SqlServerConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public string Get()
        {
            return "sdf";
        }
    }
}
