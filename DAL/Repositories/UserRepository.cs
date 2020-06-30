using DAL.Model;
using DAL.RepositoryInterfaces;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext)
            : base(dbContext) { }

        public User GetByLoginAndPass(string login, string pass)
        {
            return dbSet.Where(u => u.Login == login && u.Password == pass).FirstOrDefault();
        }

        public bool IsUserExist(string login)
        {
            return dbSet.Where(u => u.Login == login).FirstOrDefault() == null ? false : true;
        }
    }
}
