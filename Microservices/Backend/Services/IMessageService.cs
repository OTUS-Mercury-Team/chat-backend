using ACommonAuth.Contracts.Request;
using CommonAuth.Contracts.Response;
using CommonBack.Messages;
using CommonBack.Models;

namespace Backend.Services
{
    public interface IMessageService
    {
        public Task<List<Message>> GetMessagesByChatIdandUserIdAsync(long userId, long chatId);

        public List<Chat> GetChatsByUser(long id);

        public bool NewMessageAsync(Message message);

        public Task<LoginDto> LoginAsync(LoginModel loginModel);
    }
}
