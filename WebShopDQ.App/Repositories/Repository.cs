﻿using MailKit.Search;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using WebShopDQ.App.Data;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Common.Constant;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            return  Entities;
             
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(string[]? includes = null)
        {
            IQueryable<TEntity> query = _databaseContext.Set<TEntity>();
            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);
            return await query.ToListAsync();
        }
        public async Task<TEntity?> FindAsync(Expression<Func<TEntity,bool>> criteria, string[]? includes = null )
        {
            IQueryable<TEntity> query = _databaseContext.Set<TEntity>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);
            
            return await query.SingleOrDefaultAsync(criteria);
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[]? includes = null)
        {
            IQueryable<TEntity> query = _databaseContext.Set<TEntity>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(criteria).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, int take, int skip)
        {
            return await Entities.Where(criteria).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, int? take, int? skip,
            Expression<Func<TEntity, object>>? orderBy = null, string orderByDirection = OrderByEF.Ascending)
        {
            IQueryable<TEntity> query = Entities;
            if (orderBy != null)
            {
                if (orderByDirection == OrderByEF.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            return await query.Where(criteria).ToListAsync();
        }

        public async Task<TEntity?> GetById(object id)
        {
 
            return await Entities.FindAsync(id);
        }

        public async Task<bool> Remove(Expression<Func<TEntity, bool>> criteria)
        {
            var entity = Entities.FirstOrDefault(criteria);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
            await _databaseContext.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task Remove(Guid id)
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

        public async Task<int> Count(Expression<Func<TEntity, bool>> criteria)
        {
            var entity = Entities.CountAsync(criteria);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return await entity;
        }

        public async Task<TEntity> CheckExist(Expression<Func<TEntity, bool>> criteria)
        {
            var entity = await Entities.FirstOrDefaultAsync(criteria);
                if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return entity;
        }
    }
}
