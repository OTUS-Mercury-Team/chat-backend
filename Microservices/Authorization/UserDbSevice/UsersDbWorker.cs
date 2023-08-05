using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace UserDbSevice
{
    public class UsersDbWorker : IGetUser, IDeleteUser, ICreateUser
    {
        private readonly UserDbSeviceContext db;

        public UsersDbWorker()
        {
            db = new();
            if (FillingTeamUsers())
            {
                Trace.WriteLine("FilldTeam Users success");
            };
        }

        public async Task<bool> AddUser(UserData user)
        {
            bool res = true;
            try
            {
                var t = await db.Users.AddAsync(user);
                var i =  await db.SaveChangesAsync();
                if (i == 0)
                    res = false;
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error! " + e.Message);
                res = false;
            }

            return res;
        }

        public bool DeleteUser(UserData user)
        {

            bool res = true;
            try
            {
                var t = db.Users.Remove(user);
                var i = db.SaveChanges();
                if (i == 0)
                    res = false;
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error! " + e.Message);
                res = false;
            }
            return res;
        }

        public async Task<UserData[]> GetUser()
        {
            bool res = true;
            try
            {

                return await db.Users.ToArrayAsync();
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error! " + e.Message);

            }
            return new UserData[] { };
        }


        private bool FillingTeamUsers()
        {
            bool res = true;
            bool IsNeedCeateTeam = false;
            try
            {
                UserData[] users = new UserData[]
                {
                    new UserData { Username = "Serg", Password = "otus" },
                    new UserData { Username = "Alex", Password = "otus" },
                    new UserData { Username = "Max", Password = "otus" },
                    new UserData { Username = "string", Password = "string" }
                };

                foreach (UserData user in users)
                {
                    if (db.Users.FirstOrDefault(u => u.Username == user.Username) == null)
                        IsNeedCeateTeam = true;
                }
                if (IsNeedCeateTeam)
                {
                    db.Users.AddRange(entities: users);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                res = false;
                Trace.WriteLine("Error " + e.Message);
            }

            return res;
        }
    }
}
