using Dapper;
using MyStorageApplication.Database.Dtos;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.Database.UoW;

namespace MyStorageApplication.Database.Repositories
{
    public class BalanceProductStorageReadOnlyRepository(DatabaseSession session) : IBalanceProductStorageReadOnlyRepository
    {
        private readonly DatabaseSession _session = session;

        public async Task<IEnumerable<string>> GetBalanceStoragesByProduct(int productId)
            => await _session
                .Connection.QueryAsync<string>(@"
                    SELECT S.Identification
                    FROM Products P
                    INNER JOIN BalanceProductStorage BPS ON BPS.ProductId = P.ProductId
                    INNER JOIN Storage S ON S.StorageId = BPS.StorageId
                    WHERE BPS.ProductId = @ProductId", new { ProductId = productId });

        public async Task<BalanceProductStorageDto?> GetByIdAsync(int productId, int storageId)
            => await _session
                .Connection.QueryFirstOrDefaultAsync<BalanceProductStorageDto>(@"
                    SELECT B.BalanceProductStorageId, B.ProductId, B.StorageId, B.Balance 
                    FROM BalanceProductStorage B
                    WHERE B.ProductId = @ProductId AND B.StorageId = @StorageId", new { ProductId = productId, StorageId = storageId });
    }
}
