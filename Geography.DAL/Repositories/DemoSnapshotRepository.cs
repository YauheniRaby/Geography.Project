using Dapper;
using Geography.DAL.Models;
using Geography.DAL.Repositories.Abstract;
using System.Data.Entity.Spatial;

namespace Geography.DAL.Repositories
{
    public class DemoSnapshotRepository : IDemoSnapshotRepository
    {
        private readonly ISqlServerConnectionProvider _provider;

        public DemoSnapshotRepository (ISqlServerConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task<IEnumerable<DemoSnapshot>> GetAllAsync()
        {
            using (var db = _provider.GetDbConnection())
            {
                return await db.QueryAsync<DemoSnapshot>("SELECT * FROM DemoSnapshots");
            }
        }

        public async Task<IEnumerable<DemoSnapshot>> GetIntersectionsAsync(DbGeography filter)
        {
            using (var db = _provider.GetDbConnection())
            {
                return await db.QueryAsync<DemoSnapshot>(
                    "SELECT * FROM DemoSnapshots Where Geography.STIntersects(@Geography)>0",
                    new { geography = filter});
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            using (var db = _provider.GetDbConnection())
            {
                var result = await db.QueryFirstOrDefaultAsync<int>(
                    "SELECT Count(*) FROM DemoSnapshots WHERE Id = @Id",
                    new { Id = id });
                return Convert.ToBoolean(result);
            }
        }

        public async Task RemoveAsync(int id)
        {
            using (var db = _provider.GetDbConnection())
            {
                await db.QueryAsync(
                    "DELETE FROM DemoSnapshots WHERE Id = @Id",
                    new { Id = id });
            }
        }

        public async Task UpdateAsync(DemoSnapshot demoSnapshot)
        {
            using (var db = _provider.GetDbConnection())
            {
                await db.QueryAsync(
                    "UPDATE DemoSnapshots " +
                    "SET Coil = @Coil, " +
                        "Sputnik = @Sputnik, " +
                        "DateSnapshot = @DateSnapshot, " +
                        "Cloudiness = @Cloudiness, " +
                        "Geography = @Geography " +
                    "WHERE Id = @Id",
                    demoSnapshot);
            }
        }
    }
}
