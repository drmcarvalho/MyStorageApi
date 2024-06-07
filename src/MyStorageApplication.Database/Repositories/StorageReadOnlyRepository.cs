using Dapper;
using MyStorageApplication.Database.Dtos;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.Database.UoW;

namespace MyStorageApplication.Database.Repositories
{
    public class StorageReadOnlyRepository(DatabaseSession session) : IStorageReadOnlyRepository
    {
        private readonly DatabaseSession _session = session;

        public async Task<IEnumerable<StorageDto>> GetAllAsync()
            => await _session
                .Connection.QueryAsync<StorageDto>(@"SELECT StorageId, Identification FROM Storage");

        public async Task<StorageDto?> GetByIdAsync(int id)
            => await _session
                .Connection.QueryFirstOrDefaultAsync<StorageDto>("SELECT StorageId, Identification FROM Storage WHERE StorageId = @StorageId", 
                    new { StorageId = id });
    }
}
