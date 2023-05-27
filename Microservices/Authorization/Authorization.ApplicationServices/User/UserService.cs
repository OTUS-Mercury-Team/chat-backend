using Authorization.ApplicationServices.User.Ports;
using Authorization.ApplicationServices.User.Ports.Contracts.Request;
using Authorization.ApplicationServices.User.Ports.Contracts.Response;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.ApplicationServices.User;

public class UserService : IUserService
{
    IGetUser getUser;
    public Task<LoginDto> Login(LoginModel model)
    {
        return Task.FromResult(new LoginDto()
        {
            UserName = "User1",
            AccessToken = "hrgaoirjhguiar'igja'orihgadjbflyagr;ffhas;rhsg;iaurhgu;iad"
        });
    }
}
