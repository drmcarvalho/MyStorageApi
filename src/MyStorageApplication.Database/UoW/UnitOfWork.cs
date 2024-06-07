namespace MyStorageApplication.Database.UoW
{
    public sealed class UnitOfWork(DatabaseSession session): IUnitOfWork
    {
        private readonly DatabaseSession _session = session;

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction?.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session.Transaction?.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
