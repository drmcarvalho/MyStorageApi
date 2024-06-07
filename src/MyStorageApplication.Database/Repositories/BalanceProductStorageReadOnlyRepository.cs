using Dapper;
using MyStorageApplication.Database.Dtos;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.Database.UoW;

namespace MyStorageApplication.Database.Repositories
{
    public class BalanceProductStorageReadOnlyRepository(DatabaseSession session) : IBalanceProductStorageReadOnlyRepository
    {
        private readonly DatabaseSession _session = session;

        public async Task<BalanceProductStorageDto?> GetByIdAsync(int productId, int storageId)
            => await _session
                .Connection.QueryFirstOrDefaultAsync<BalanceProductStorageDto>(@"
                    SELECT B.BalanceProductStorageId, B.ProductId, B.StorageId, B.Balance 
                    FROM BalanceProductStorage B
                    WHERE B.ProductId = @ProductId AND B.StorageId = @StorageId", new { ProductId = productId, StorageId = storageId });
    }
}
