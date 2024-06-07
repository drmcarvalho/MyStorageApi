using MyStorageApplication.Database.Dtos;

namespace MyStorageApplication.Database.Repositories.Interfaces
{
    public interface IMovementsReadOnlyRepository
    {
        Task<IEnumerable<HistoryMovementDto>> GetAllAsync();
        Task<IEnumerable<HistoryMovementDto>> QueryAsync(string q);
    }
}
