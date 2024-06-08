using MyStorageApplication.Database.Entities;

namespace MyStorageApplication.Database.Repositories.Interfaces
{
    public interface IProductWriteOnlyRepository
    {
        Task InsertAsync(Product product);
        Task UpdateAsync(Product product);
        Task UpdateStokBalanceAsync(int newStokBalanceAmount, int productId);
        Task DeleteAsync(int productId);
    }
}
