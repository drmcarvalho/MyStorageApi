using MyStorageApplication.Database.Entities;

namespace MyStorageApplication.Database.Repositories.Interfaces
{
    public interface IProductWriteOnlyRepository
    {
        Task Insert(Product product);
        Task Update(Product product);
        Task UpdateStokBalanceAsync(int newStokBalanceAmount);
    }
}
