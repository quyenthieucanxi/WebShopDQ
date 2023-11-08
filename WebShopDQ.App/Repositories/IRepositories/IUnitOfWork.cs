namespace WebShopDQ.App.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void CommitTransaction();
        void RollbackTransaction();
        int SaveChanges();
        void BeginTransaction();
    }
}
