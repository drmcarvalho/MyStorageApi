using Dapper;
using MyStorageApplication.Database.Entities;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.Database.UoW;

namespace MyStorageApplication.Database.Repositories
{
    public class MovementsWriteOnlyRepository(DatabaseSession session) : IMovementsWriteOnlyRepository
    {
        private readonly DatabaseSession _session = session;
        public async Task InsertAsync(Movement movement)
            => await _session
                .Connection.ExecuteAsync(@"
                    INSERT INTO Movements (Amount, ProductId, StorageId, ProductName, CreatedAt, Type) 
                    VALUES (@Amount, @ProductId, @StorageId, @ProductName, @CreatedAt, @Type)", movement);
    }
}
