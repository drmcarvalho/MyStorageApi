using Dapper;
using MyStorageApplication.Database.Entities;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.Database.UoW;

namespace MyStorageApplication.Database.Repositories
{
    public class StorageWriteOnlyRepository(DatabaseSession session) : IStorageWriteOnlyRepository
    {
        private readonly DatabaseSession _session = session;

        public async Task InsertAsync(Storage storage)
            => await _session.Connection.ExecuteAsync("INSERT INTO Storage(Identification) VALUES (@Identification)", storage);

        public async Task UpdateAsync(Storage storage)
            => await _session.Connection.ExecuteAsync("UPDATE Storage SET Identification = @Identification WHERE StorageId = @StorageId", storage);
    }
}
