using DAL.Repositories.RepositoriesInterfaces.InterfaceGeneric;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        // FIELDS
        // узагальнений контекст
        protected DbContext dbContext;
        protected DbSet<TEntity> dbSet;

        // CONSTRUCTORS
        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        // METHODS
        public virtual int Count()
        {
            return dbSet.Count();
        }
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Count(predicate);
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                string includeProperties = "",
                                                int? page = null, int? amount = null)
        {
            // filter
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // include properties
            foreach (string includeProperty in includeProperties.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            // ordering
            if (orderBy != null) query = orderBy(query);

            // paging
            if (page.HasValue && amount.HasValue) query = query.Skip((page.Value - 1) * amount.Value).Take(amount.Value);

            return query;
        }
        public virtual TEntity Get(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            // find
            if (id == null) throw new ArgumentNullException(nameof(id));
            TEntity entityToDelete = dbSet.Find(id);

            // delete finded
            if (entityToDelete == null) throw new InvalidOperationException("There is no records with such id");
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (entityToDelete == null) throw new ArgumentNullException(nameof(entityToDelete));

            if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null) dbSet.RemoveRange(dbSet.Where(predicate));
            else dbSet.RemoveRange(dbSet);
        }
    }
}
