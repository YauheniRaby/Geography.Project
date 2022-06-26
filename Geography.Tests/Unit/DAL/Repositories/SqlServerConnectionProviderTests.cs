using Geography.DAL.Repositories;
using Xunit;

namespace Geography.Tests.Unit.DAL.Repositories
{
    public class SqlServerConnectionProviderTests
    {
        private readonly string _connectionString;
        private readonly SqlServerConnectionProvider _sqlServerConnectionProvider;

        public SqlServerConnectionProviderTests()
        {
            _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TestDb";
            _sqlServerConnectionProvider = new SqlServerConnectionProvider(_connectionString);
        }

        [Fact]
        public void GetDbConnection_ReturnedSqlConnection_Success()
        {
            //Act
            var result = _sqlServerConnectionProvider.GetDbConnection();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.ConnectionString, _connectionString);
        }
    }
}
