using Geography.DAL.Models;
using System.Data.Entity.Spatial;

namespace Geography.DAL.Repositories.Abstract
{
    public interface IDemoSnapshotRepository
    {
        Task<IEnumerable<DemoSnapshot>> GetAllAsync();

        Task<IEnumerable<DemoSnapshot>> GetIntersectionsAsync(DbGeography filter);

        Task RemoveAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task UpdateAsync(DemoSnapshot demoSnapshot);
    }
}
