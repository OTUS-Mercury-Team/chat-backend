using ACommonAuth.Contracts.Request;
using CommonAuth.Contracts.Response;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BackendController : ControllerBase
    {


        private readonly ILogger<BackendController> _logger;

        public BackendController(ILogger<BackendController> logger)
        {
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<LoginDto> GetMessageAsync(LoginModel loginModel)
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:8227/api/Auth/login");
            var response = await client.PostAsJsonAsync("https://localhost:8227/api/Auth/login", loginModel);
            LoginDto? loginDto = await response.Content.ReadFromJsonAsync<LoginDto>();

            //LoginDto loginDto = new LoginDto();
            //loginDto.UserName = loginModel.UserName;
            return loginDto;


            //var user = await customerRepository.GetAsync(id);
            //if (user == null)
            //{
            //    return NotFound("Customer not found!");
            //}
            //return Ok(t);
        }
    }
}