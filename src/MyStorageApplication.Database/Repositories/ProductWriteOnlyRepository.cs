using Dapper;
using MyStorageApplication.Database.Entities;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.Database.UoW;

namespace MyStorageApplication.Database.Repositories
{
    public class ProductWriteOnlyRepository(DatabaseSession session) : IProductWriteOnlyRepository
    {
        private readonly DatabaseSession _session = session;

        public async Task DeleteAsync(int productId)
            => await _session.Connection.ExecuteAsync(@"UPDATE Products SET Deleted = 1 WHERE ProductId = @ProductId", new { ProductId = productId });

        public async Task InsertAsync(Product product)
            => await _session.Connection.ExecuteAsync(@"INSERT INTO Products (Name, Price) VALUES (@Name, @Price)", product);

        public async Task UpdateAsync(Product product)
            => await _session .Connection.ExecuteAsync(@"UPDATE Products SET Name = @Name, Price = @Price WHERE ProductId = @ProductId", product);

        public async Task UpdateStokBalanceAsync(int newStokBalanceAmount, int productId)
            => await _session.Connection.ExecuteAsync(@"UPDATE Products SET StockBalance = @StockBalance WHERE ProductId = @ProductId", new { StockBalance = newStokBalanceAmount, ProductId = productId });
    }
}
