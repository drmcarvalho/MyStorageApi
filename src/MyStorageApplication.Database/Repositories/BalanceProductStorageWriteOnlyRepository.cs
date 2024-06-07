using Dapper;
using MyStorageApplication.Database.Dtos;
using MyStorageApplication.Database.Entities;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.Database.UoW;

namespace MyStorageApplication.Database.Repositories
{
    public class BalanceProductStorageWriteOnlyRepository(DatabaseSession session) : IBalanceProductStorageWriteOnlyRepository
    {        
        private readonly DatabaseSession _session = session;

        public async Task InsertAsync(BalanceProductStorage balance)
            => await _session
                .Connection.ExecuteAsync(@"INSERT INTO BalanceProductStorage (Balance, StorageId, ProductId) VALUES (@Balance, @StorageId, @ProductId)", balance, _session.Transaction);

        public async Task UpdateBalanceAsync(BalanceProductStorageDto balance)
            => await _session
                .Connection.ExecuteAsync(@"UPDATE BalanceProductStorage SET Balance = @Balance WHERE BalanceProductStorageId = @BalanceProductStorageId", new { 
                    balance.Balance, 
                    balance.BalanceProductStorageId 
                }, _session.Transaction);
    }
}
