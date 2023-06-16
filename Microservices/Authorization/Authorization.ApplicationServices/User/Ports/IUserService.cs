
using ACommonAuth.Contracts.Request;
using CommonAuth.Contracts.Response;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.ApplicationServices.User.Ports;

public interface IUserService
{
    Task<LoginDto> Login(LoginModel model);
}
