using Dapper;
using MyStorageApplication.Database.Dtos;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.Database.UoW;

namespace MyStorageApplication.Database.Repositories
{
    public class ProductReadOnlyRepository(DatabaseSession session) : IProductReadOnlyRepository
    {
        private readonly DatabaseSession _session = session;

        public async Task<bool> ExitsByNameAsync(int productId, string name)
            => await _session
                .Connection.QueryFirstOrDefaultAsync<int>(@"SELECT COUNT(ProductId) FROM Products WHERE ProductId <> @ProductId AND Name = @Name", new { ProductId = productId, Name = name }) > 0;

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
            => await _session
                .Connection.QueryAsync<ProductDto>(@"SELECT ProductId, Name AS ""ProductName"", StockBalance, Price FROM Products WHERE Deleted = 0");

        public async Task<ProductDto?> GetByIdAsync(int id)
        => await _session
                .Connection.QueryFirstOrDefaultAsync<ProductDto>(@"SELECT ProductId, Name AS ""ProductName"", StockBalance, Price FROM Products WHERE ProductId = @ProductId", new { ProductId = id });

        public Task<string> GetProductNameAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDto>> QueryAsync(string q)
            => await _session
                .Connection.QueryAsync<ProductDto>(@"SELECT ProductId, Name AS ""ProductName"", StockBalance, Price FROM Products WHERE Deleted = 0 AND Name LIKE @WhereLike", new { WhereLike = $"%{q}%" });
    }
}
