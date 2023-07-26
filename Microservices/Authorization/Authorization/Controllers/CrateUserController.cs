using ACommonAuth.Contracts.Request;
using Authorization.ApplicationServices.User.Ports;
using CommonAuth.Contracts.Request;
using CommonAuth.Contracts.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrateUserController : ControllerBase
    {
        private readonly IUserService _userService;

        public CrateUserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<bool> Create(CreateUserModel createUserModel)
        {
            return await _userService.CreateUserModel(createUserModel);
        }
    }
}
