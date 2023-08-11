using ACommonAuth.Contracts.Request;
using Backend.Services;
using CommonAuth.Contracts.Response;
using CommonBack.Messages;
using CommonBack.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BackendController : ControllerBase
    {

        private readonly ILogger<BackendController> _logger;
        private readonly IMessageService _messageService;

        public BackendController(ILogger<BackendController> logger, IMessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }

        [HttpPost("login")]
        public async Task<LoginDto> LoginAsync(LoginModel loginModel)
        {
            var loginDto = await _messageService.LoginAsync(loginModel);
            return loginDto;
        }

        [HttpPost("newmessage")] // отправляем в брокер\
        public IActionResult NewMessage(Message message)
        {
            bool res = _messageService.NewMessage(message);
            if (res)
            {
                return Ok(res);
            }
            else return StatusCode(404, "Error write in RabbitMQ queue!");

        }

        [HttpGet]
        [Route("chatsbyuser/{id:long}")]
        public List<Chat> GetChatsByUser(long id)
        {
            var chats = _messageService.GetChatsByUser(id);
            return chats;

        }


        [HttpPost("messagesbychatidanduserid")]
        public async Task<List<Message>> GetMessagesByChatIdandUserIdAsync(long userId, long chatId)
        {

            var messages = await _messageService.GetMessagesByChatIdandUserIdAsync(userId, chatId);
            return messages;
        }

    }
}