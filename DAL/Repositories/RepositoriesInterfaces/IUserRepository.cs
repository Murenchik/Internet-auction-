using DAL.Model;

namespace DAL.RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetByLoginAndPass(string login, string pass);
        bool IsUserExist(string login);
    }
}
