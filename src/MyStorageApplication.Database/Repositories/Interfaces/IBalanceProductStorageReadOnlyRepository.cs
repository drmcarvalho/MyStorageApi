using MyStorageApplication.Database.Dtos;

namespace MyStorageApplication.Database.Repositories.Interfaces
{
    public interface IBalanceProductStorageReadOnlyRepository
    {        
        Task<BalanceProductStorageDto?> GetByIdAsync(int productId, int storageId);
        Task<IEnumerable<string>> GetBalanceStoragesByProduct(int productId);
        Task<int?> SumBalanceByStorage(int storageId);
    }
}
