using System.Linq.Expressions;
using WebShopDQ.App.Common.Constant;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);

        Task Add(IEnumerable<TEntity> entities);

        Task<TEntity?> GetById(object id);

        IQueryable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync(string[]? includes = null);

        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> criteria, string[]? includes = null);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[]? includes = null);
        
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, int take, int skip);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, int? take, int? skip,
            Expression<Func<TEntity, object>>? orderBy = null, string orderByDirection = OrderByEF.Ascending);

        Task Update(TEntity entity);

        Task Update(IEnumerable<TEntity> entities);

        Task<bool> Remove(Expression<Func<TEntity, bool>> criteria);

        Task Remove(Guid id);

        void Remove(TEntity entity);

        void Remove(params TEntity[] entities);

        void Remove(IEnumerable<TEntity> entities);

        Task<int> Count(Expression<Func<TEntity, bool>> criteria);

        Task<TEntity> CheckExist(Expression<Func<TEntity, bool>> criteria);
    }
}
