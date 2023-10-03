using Microsoft.EntityFrameworkCore;
using WebShopDQ.App.Common;
using WebShopDQ.App.Data;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;

namespace WebShopDQ.App.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DatabaseContext _databaseContext;
        public DbSet<TEntity> Entities { get; set; }
        public Repository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            Entities = _databaseContext.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Add(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Add(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.AddRange(entities);
            await _databaseContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return Entities;
        }

        public async Task<TEntity> GetById(object id)
        {
            var entity = await Entities.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException(Messages.UserNotFound);
            return entity;
        }

        public async void Remove(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
        }

        public void Remove(params TEntity[] entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.RemoveRange(entities);
        }

        public void Remove(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.RemoveRange(entities);
        }

        public async Task Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Update(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.UpdateRange(entities);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
