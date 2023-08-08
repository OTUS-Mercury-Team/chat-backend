using CommonBack.Messages;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace RabbitMQService
{
    internal class MessagesAsync
    {
        private HttpListenerContext context;

        public MessagesAsync(HttpListenerContext context)
        {
            this.context = context;
        }

        internal async Task<(bool, MessagesAsync)> MessagesExecuteAsync()
        {
            var execTask = Task.Run(async () =>
            {

                HttpListenerRequest request = context.Request;
                Console.WriteLine("{0} Connected ClienUrl:  {1} ...", DateTime.Now, request.Url);
                // Console.WriteLine("{0} Request {1}...", DateTime.Now, i);
                // request.
                //Объект ответа
                HttpListenerResponse response = context.Response;
                // Console.WriteLine("{0} Response {1}...", DateTime.Now, i);

                if (context.Request.HttpMethod == "POST")
                {
                    int userId = 0;
                    int chatId = 0;
                    var body = new StreamReader(context.Request.InputStream).ReadToEnd();
                    var bodyArr = body.Split('&');
                    if (bodyArr.Count() > 1)
                    {
                        foreach (var item in bodyArr)
                        {
                            var paramArr = item.Split('=');

                            if (paramArr.Count() > 1)
                            {
                                if (paramArr[0] == "userId")
                                {
                                    userId = int.Parse(paramArr[1]);
                                }

                                if (paramArr[0] == "chatId")
                                {
                                    chatId = int.Parse(paramArr[1]);
                                }
                            }
                        }
                    }

                    using (MessageContext db = new MessageContext())
                    {

                        try
                        {
                            var messages = db.Messages.Where(s => s.UserFrom == userId && s.ChatId == chatId).ToList();
                            response.ContentType = "application/json";
                            var responseBody = JsonConvert.SerializeObject(messages);
                            int bodylen = responseBody.Length;
                            var buffer = Encoding.UTF8.GetBytes(responseBody, 0, bodylen);
                            response.StatusCode = 200;

                            try
                            {
                                await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                            }
                            catch (Exception)
                            {
                                context.Response.StatusCode = 404;
                                return (false, this);
                            }

                            Console.WriteLine("Сообщения получены!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error read from DB! " + ex.Message);
                           
                        }
                    }
                    
                    response.OutputStream.Close();
                }
                else
                {
                    context.Response.StatusCode = 404;
                    context.Response.OutputStream.Close();
                }

                return (true, this);
            });

            return await execTask;
        }
    }
}
