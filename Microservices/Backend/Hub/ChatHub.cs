using Microsoft.AspNetCore.SignalR;

namespace Backend.Hab
{
    public class ChatHub: Hub
    {
        public async Task Send(string message)
        {

            /*
             * message - какойто контейнер из фронта содержащий токен  и тело сообщения
            1) надо проверить токен и записать сообщение в базы
            2) обаработать список Clients?
                создавать группы? - Hub.Groups 

            https://learn.microsoft.com/ru-ru/aspnet/signalr/overview/guide-to-the-api/working-with-groups
            https://learn.microsoft.com/ru-ru/aspnet/signalr/overview/guide-to-the-api/mapping-users-to-connections
             * 
             */

            await this.Clients.All.SendAsync("Receive", message);

            
        }
    }
}
