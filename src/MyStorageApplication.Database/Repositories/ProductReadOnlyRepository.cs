using MyStorageApplication.Database.Dtos;
using MyStorageApplication.Database.Repositories.Interfaces;

namespace MyStorageApplication.Database.Repositories
{
    public class ProductReadOnlyRepository : IProductReadOnlyRepository
    {
        public Task<bool> ExitsByNameAsync(int productId, string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetProductNameAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> QueryAsync(string q)
        {
            throw new NotImplementedException();
        }
    }
}
