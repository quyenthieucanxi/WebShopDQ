namespace WebShopDQ.App.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void CommitTransaction();
        void RollbackTransaction();
        Task SaveChanges(CancellationToken cancellation = default);
        void BeginTransaction();
    }
}
