namespace Services
{
    public interface IUserService
    {
        Model.User Register(string login, string pass);
        Model.User Authorize(string login, string pass);
    }
}
