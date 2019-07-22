using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WebAppWithSearchFilters.Models2.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal DbContext _context;
        internal DbSet<TEntity> _dbSet;
        
        public GenericRepository(DbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity>Get(
            List<Expression<Func<TEntity, bool>>> searchFilters = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includedEntities = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if(searchFilters != null)
            {
                foreach (var filter in searchFilters)
                    query = query.Where(filter);
            }

            foreach(var includedEntity in includedEntities)
            {
                query = query.Include(includedEntity);
            }

            if(orderBy != null)
                return orderBy(query);
            return query;
        }

        public virtual TEntity Find(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void DeleteById(object id)
        {
            TEntity entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if(_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }
    }
}