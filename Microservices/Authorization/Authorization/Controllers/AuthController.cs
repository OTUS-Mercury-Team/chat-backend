using Authorization.ApplicationServices.User.Ports;
using Microsoft.AspNetCore.Mvc;
using Authorization.ApplicationServices.User.Ports.Contracts.Request;
using Authorization.ApplicationServices.User.Ports.Contracts.Response;

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
