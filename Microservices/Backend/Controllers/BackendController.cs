using ACommonAuth.Contracts.Request;
using Backend.db;

using CommonAuth.Contracts.Response;
using CommonBack.Messages;
using CommonBack.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BackendController : ControllerBase
    {

        private readonly ILogger<BackendController> _logger;
        private UserChatContextcs db;

        public BackendController(ILogger<BackendController> logger)
        {
            _logger = logger;
            db = new UserChatContextcs();
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

        }

        [HttpPost("newmessage")] // отправляем в брокер
        public bool NewMessageAsync(Message message)
        {
            string jsonString = JsonSerializer.Serialize(message);
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "dev-queue",
                                     durable: true,
                                     autoDelete: false,
                                     exclusive: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(jsonString);

                channel.BasicPublish(exchange: "", routingKey: "dev-queue", basicProperties: null, body: body);

                Console.WriteLine(message + " - sended");
            }
            return true;
        }

        [HttpGet]
        [Route("chatsbyuser/{id:long}")]
        public List<Chat> GetChatsByUser(long id)
        {
            var t = db.Chats.Where(r => r.Users.Any(i => i.Id == id)).ToList();
            return t;
        }
        //[HttpPost("newchat")]

        //[HttpPost("getchatsbyuser")] - 

        [HttpPost("messagesbychatidanduserid")]
        public async Task<List<Message>> GetMessagesByChatIdandUserId(long userId, long chatId)
        {

            List<Message> messages = new List<Message>();

            HttpClient httpClient = new HttpClient();

            using var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:1088");
            // request.pa
            string param = $"userId={userId}&chatId={chatId}";
            // установка отправляемого содержимого
            HttpContent content = new StringContent(param, Encoding.UTF8, "application/json");

            request.Content = content;

            // отправляем запрос
            using var response = await httpClient.SendAsync(request);
            // получаем ответ
            string responseText = await response.Content.ReadAsStringAsync();

            messages = JsonSerializer.Deserialize<List<Message>>(responseText);

            return messages;
        }

    }
}