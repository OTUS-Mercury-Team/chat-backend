using Authorization.ApplicationServices.User.Ports;
using Microsoft.AspNetCore.Mvc;
using Authorization.DataAccessServices;

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
        public Task<uint?> Validate(string token)
        {
            return _dataAcces.ValidateToken(token);
        }
    }
}
