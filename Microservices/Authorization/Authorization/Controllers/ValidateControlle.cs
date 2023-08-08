using Authorization.ApplicationServices.User.Ports;
using Microsoft.AspNetCore.Mvc;
using Authorization.DataAccessServices;
using ACommonAuth.Contracts.Request;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateController : ControllerBase
    {
        private readonly IDataAcces _dataAcces;

        public ValidateController(IDataAcces dataAcces)
        {
             _dataAcces= dataAcces;
        }

        [HttpPost("validate")]
        public async Task<int?> Validate(ValidModel token)
        {
            var res = await _dataAcces.ValidateToken(token.Jwt);
            return (int)res;
        }
    }
}
