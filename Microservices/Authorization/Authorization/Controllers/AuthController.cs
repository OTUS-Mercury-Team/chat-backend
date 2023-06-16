using ACommonAuth.Contracts.Request;
using Authorization.ApplicationServices.User.Ports;
using CommonAuth.Contracts.Response;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public Task<LoginDto> Login(LoginModel loginModel)
        {
            return _userService.Login(loginModel);
        }
    }
}
