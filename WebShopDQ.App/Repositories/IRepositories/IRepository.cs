using System.Linq.Expressions;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        Task Add(IEnumerable<TEntity> entities);

        Task<TEntity?> GetById(object id);

        IQueryable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> criteria, string[]? includes = null);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[]? includes = null);

        Task Update(TEntity entity);

        Task Update(IEnumerable<TEntity> entities);

        Task Remove(int id);

        void Remove(TEntity entity);

        void Remove(params TEntity[] entities);

        void Remove(IEnumerable<TEntity> entities);
    }
}
