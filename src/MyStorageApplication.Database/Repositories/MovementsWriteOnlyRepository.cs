using MyStorageApplication.Database.Entities;
using MyStorageApplication.Database.Repositories.Interfaces;

namespace MyStorageApplication.Database.Repositories
{
    public class MovementsWriteOnlyRepository : IMovementsWriteOnlyRepository
    {
        public Task InsertAsync(Movement movement)
        {
            throw new NotImplementedException();
        }
    }
}
