using MyStorageApplication.Database.Dtos;
using MyStorageApplication.Database.Entities;

namespace MyStorageApplication.Database.Repositories.Interfaces
{
    public interface IBalanceProductStorageWriteOnlyRepository
    {
        Task InsertAsync(BalanceProductStorage balance);
        Task UpdateBalanceAsync(BalanceProductStorageDto balance);
    }
}
