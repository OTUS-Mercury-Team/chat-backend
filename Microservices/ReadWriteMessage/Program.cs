
using CommonBack.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQService;
using System.Text;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "dev-queue",
                                 durable: true,
                                 autoDelete: false,
                                 exclusive: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body;
                var messageStr = Encoding.UTF8.GetString(body.ToArray());
                Console.WriteLine("Receved message Str: " + messageStr);
                var message = JsonSerializer.Deserialize<Message>(messageStr);
                Console.WriteLine("Receved message deserialize: " + message);

                using (MessageContext db = new MessageContext())
                {

                    try
                    {
                        db.Messages.Add(message);
                        db.SaveChanges();
                        Console.WriteLine("Новое сообщение добавлено в БД");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error write in DB!");
                        // res = false;
                    }
                }

            };

            channel.BasicConsume(queue: "dev-queue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine("Subscribed");

            Console.ReadLine();
        }
    }
}