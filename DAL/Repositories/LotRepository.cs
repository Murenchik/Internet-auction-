using DAL.Model;
using DAL.RepositoryInterfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repositories
{
    public class LotRepository : GenericRepository<Lot>, ILotRepository
    {
        public LotRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
