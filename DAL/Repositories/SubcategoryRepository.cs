using DAL.Model;
using DAL.RepositoryInterfaces;
using System.Data.Entity;

namespace DAL.Repositories
{
    public class SubcategoryRepository : GenericRepository<Subcategory>, ISubcategoryRepository
    {
        public SubcategoryRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
