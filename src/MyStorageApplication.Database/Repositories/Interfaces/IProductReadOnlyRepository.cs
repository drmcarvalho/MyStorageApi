using MyStorageApplication.Database.Dtos;

namespace MyStorageApplication.Database.Repositories.Interfaces
{
    public interface IProductReadOnlyRepository
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> QueryAsync(string q);
        Task<bool> ExitsByNameAsync(int productId, string name);
        Task<string> GetProductNameAsync(int productId);
    }
}
