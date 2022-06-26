using Geography.BL.ModelsDTO;

namespace Geography.BL.Services.Abstract
{
    public interface IDemoSnapshotService
    {
        Task<IEnumerable<DemoSnapshotDTO>> GetAllAsync();

        Task<IEnumerable<DemoSnapshotDTO>> GetIntersectionsAsync(string filter);

        Task RemoveAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task UpdateAsync(DemoSnapshotDTO demoSnapshotDTO);

        bool Validation(string filter);

        public string[] GetSputnicArr();
    }
}
