using ACommonAuth.Contracts.Request;
using Backend.Controllers;
using CommonBack.Messages;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SignalRChat.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string name, string message,string jwt, string chat_id)
    {

        //var elem = new BackendController();

        int? id = await Validate(jwt);
        if (id != null)
        {
            Message m_message = new();
            m_message.Body = message;
            m_message.UserFrom = Convert.ToInt32(id);
            int id_chat = 0;
            if (int.TryParse(chat_id, out id_chat)) {
                m_message.ChatId = id_chat;
            }

            bool res = BackendController.NewMessageAsync(m_message);

            await Clients.All.SendAsync("ReceiveMessage", name, message);
        }

    }
        public async Task<int?> Validate(string jwt)
        {
            int? id = null;
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("http://localhost:5088/api/Validate/validate");
            var jwtmodel = new ValidModel(){ Jwt = jwt};

                var response = await client.PostAsJsonAsync("http://localhost:5088/api/Validate/validate", jwtmodel);
                id = await response.Content.ReadFromJsonAsync<int>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return id;

        }
}