using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories.RepositoriesInterfaces.InterfaceGeneric
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    string includeProperties = "",
                                    int? page = null, int? amount = null);
        TEntity Get(int id);

        void Insert(TEntity entity);

        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Delete(Expression<Func<TEntity, bool>> predicate);
    }
}
