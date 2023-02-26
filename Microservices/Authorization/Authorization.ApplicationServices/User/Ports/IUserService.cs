﻿using Authorization.ApplicationServices.User.Ports.Contracts.Request;
using Authorization.ApplicationServices.User.Ports.Contracts.Response;

namespace Authorization.ApplicationServices.User.Ports;

public interface IUserService
{
    Task<LoginDto> Login(LoginModel model);
}
