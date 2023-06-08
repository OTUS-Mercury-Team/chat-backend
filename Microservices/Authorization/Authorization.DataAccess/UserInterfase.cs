namespace DataAccess
{
    public interface IGetUser
    {
        UserData[] GetUser();
    }
    public interface IDeleteUser
    {
        bool DeleteUser(UserData user);
    }
    public interface ICreateUser
    {
        bool AddUser(UserData user);
    }
}