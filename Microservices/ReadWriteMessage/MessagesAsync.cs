using System.Net;

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
                    //byte[] b = Encoding.UTF8.GetBytes("ACK");

                    response.ContentType = "application/json";

                    // var ss = context.Request.Url.AbsolutePath;



                    // var responseBody = JsonConvert.SerializeObject(dataManagerState);
                    //int bodylen = responseBody.Length;

                    response.StatusCode = 200;



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
