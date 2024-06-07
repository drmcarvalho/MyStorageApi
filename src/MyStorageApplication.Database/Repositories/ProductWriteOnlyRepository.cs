using Dapper;
using Dapper.Contrib.Extensions;
using MyStorageApplication.Database.Entities;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.Database.UoW;

namespace MyStorageApplication.Database.Repositories
{
    public class ProductWriteOnlyRepository(DatabaseSession session) : IProductWriteOnlyRepository
    {
        private readonly DatabaseSession _session = session;

        public async Task InsertAsync(Product product)
            => await _session.Connection.InsertAsync(product);

        public async Task UpdateAsync(Product product)
            => await _session .Connection.UpdateAsync(product);

        public async Task UpdateStokBalanceAsync(int newStokBalanceAmount, int productId)
            => await _session.Connection.ExecuteAsync(@"UPDATE Products SET StockBalance = @StockBalance WHERE ProductId = @ProductId", new { StockBalance = newStokBalanceAmount, ProductId = productId });
    }
}
