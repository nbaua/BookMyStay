using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Configuration;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace BookMyStay.MessageBroker
{
    public class MessageHandler : IMessageHandler
    {
        private string _hostname = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _virtualHost = string.Empty;

        public MessageHandler()
        {
            dynamic settings = GetConfig();

            //var appConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            _hostname = settings["MessageBrokerConfig:HostName"].ToString();
            _username = settings["MessageBrokerConfig:UserName"].ToString();
            _password = settings["MessageBrokerConfig:Password"].ToString();
            _virtualHost = settings["MessageBrokerConfig:VirtualHost"].ToString();
        }

        Task<string> IMessageHandler.ConsumeMessage(string QueueName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password,
                VirtualHost = _virtualHost
            };

            var connection = factory.CreateConnection();         // Get connection
            var channel = connection.CreateChannel();    // Get the channel - alternatively can use the CreateModel() as well.

            channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false); //exclusive: true, Gives error
            //channel.BasicQos(100,10,true); // Per consumer limit

            BasicGetResult result = channel.BasicGet(QueueName, false);
            if (result != null)
            {
                Console.WriteLine($"Message: {Encoding.UTF8.GetString(result.Body.ToArray())}");
                channel.BasicAck(result.DeliveryTag, false);
                Console.WriteLine("Press any key to stop consuming messages.");
            }

            //channel.Dispose();
            connection.Dispose();

            return Task.FromResult("Done");
        }

        private static void OnNewMessageReceived(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine($"Message: {Encoding.UTF8.GetString(e.Body.ToArray())}");
            Console.WriteLine("Press any key to stop consuming message.");
        }

        Task IMessageHandler.PublishMessage<T>(string QueueName, T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password,
                VirtualHost = _virtualHost
            };

            var connection = factory.CreateConnection();         // Get connection
            var channel = connection.CreateChannel();    // Get the channel - alternatively can use the CreateModel() as well.

            var messageString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(messageString);

            channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false);
            channel.BasicPublish("", QueueName, body: body);

            connection.Dispose();
            return Task.CompletedTask;
        }


        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.AppContext.BaseDirectory)
                .AddJsonFile("AppSettings.json",
                optional: true,
                reloadOnChange: true);

            return builder.Build();
        }
    }
}
