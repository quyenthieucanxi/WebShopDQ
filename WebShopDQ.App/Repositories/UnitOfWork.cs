using Microsoft.EntityFrameworkCore.Storage;
using WebShopDQ.App.Data;

namespace WebShopDQ.App.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DatabaseContext _databaseContext;
        private IDbContextTransaction? _transaction = null;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task SaveChanges(CancellationToken cancellationToken = default)
        {
            return  _databaseContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _databaseContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            _transaction = _databaseContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
            }
        }

        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }
        }
    }

}
