using MyStorageApplication.Database.Dtos;

namespace MyStorageApplication.Database.Repositories.Interfaces
{
    public interface IStorageReadOnlyRepository
    {
        Task<IEnumerable<StorageDto>> GetAllAsync();
        Task<StorageDto?> GetByIdAsync(int id);
        Task<IEnumerable<StorageDto>> QueryAsync(string query);
    }
}
