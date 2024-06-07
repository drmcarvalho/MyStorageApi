using MyStorageApplication.Database.Entities;
using MyStorageApplication.Database.Repositories.Interfaces;

namespace MyStorageApplication.Database.Repositories
{
    public class ProductWriteOnlyRepository : IProductWriteOnlyRepository
    {
        public Task Insert(Product product)
        {
            throw new NotImplementedException();
        }

        public Task Update(Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStokBalanceAsync(int newStokBalanceAmount)
        {
            throw new NotImplementedException();
        }
    }
}
