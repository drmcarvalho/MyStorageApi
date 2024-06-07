using MyStorageApplication.Database.Entities;

namespace MyStorageApplication.Database.Repositories.Interfaces
{
    public interface IStorageWriteOnlyRepository
    {
        Task InsertAsync(Storage storage);
        Task UpdateAsync(Storage storage);
    }
}
