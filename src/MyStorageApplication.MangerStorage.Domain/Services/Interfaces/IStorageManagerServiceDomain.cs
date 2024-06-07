using MyStorageApplication.Database.Dtos;
using MyStorageApplication.StorageManager.Domain.Dtos;

namespace MyStorageApplication.StorageManager.Domain.Services.Interfaces
{
    public interface IStorageManagerServiceDomain
    {        
        Task<StorageDto?> GetByIdAsync(int id);
        Task<IEnumerable<StorageDto>> GetAllAsync();
        Task<ValidationResult> CreateStorageAsync(CreateStorageDto storageDto);
        Task<ValidationResult> UpdateStorageAsync(UpdateStorageDto updateStorageDto);
        Task<IEnumerable<HistoryMovementDto>> GetAllHistoryMovimentsAsync();
        Task<ValidationResult> RegisterMovementInStorage(RegisterMovementInStorageDto registerMovementInStorageDto);
    }
}
