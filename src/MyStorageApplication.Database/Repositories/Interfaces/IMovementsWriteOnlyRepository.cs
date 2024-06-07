using MyStorageApplication.Database.Entities;

namespace MyStorageApplication.Database.Repositories.Interfaces
{
    public interface IMovementsWriteOnlyRepository
    {
        Task InsertAsync(Movement movement);
    }
}
