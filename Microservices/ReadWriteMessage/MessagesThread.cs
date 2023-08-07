using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQService
{
    internal class MessagesThread
    {
        private Thread thread;
        //private CancellationToken cancelToken;


        public MessagesThread()
        {
            //this.cancelToken = cancelToken;
            thread = new Thread(MessagesThreadExecute);
            Console.WriteLine("MessagesThread created");

            thread.Start();
        }
        private async void MessagesThreadExecute() //Функция потока
        {
            Console.WriteLine("MessagesThread started");

            string url = "http://127.0.0.1";
            string url1 = "http://127.0.0.1";
            string port = "1088";
            string prefix = String.Format("{0}:{1}/", url, port);

            string prefix1 = String.Format("{0}:{1}/", url1, port);


            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            listener.Prefixes.Add(prefix1);


            listener.Start();
            Console.WriteLine("HttpListener listen to " + prefix + "...");
            while (true/*!cancelToken.IsCancellationRequested*/)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                //LogUtils.WriteLog("InfoAsync created");
                MessagesAsync messageAsync = new MessagesAsync(context);
                messageAsync.MessagesExecuteAsync();
            }

        }

    }
}
