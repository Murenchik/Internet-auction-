using DAL.Model;
using DAL.RepositoryInterfaces;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
