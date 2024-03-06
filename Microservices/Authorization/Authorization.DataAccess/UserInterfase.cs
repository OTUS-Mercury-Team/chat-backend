namespace DataAccess
{
    public interface IGetUser
    {
       Task< UserData[]> GetUser();
    }
    public interface IDeleteUser
    {
        bool DeleteUser(UserData user);
    }
    public interface ICreateUser
    {
        Task<bool> AddUser(UserData user);
    }
}