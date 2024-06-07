using MyStorageApplication.Database.Dtos;

namespace MyStorageApplication.Database.Repositories.Interfaces
{
    public interface IBalanceProductStorageReadOnlyRepository
    {        
        Task<BalanceProductStorageDto?> GetByIdAsync(int productId, int storageId);
    }
}
