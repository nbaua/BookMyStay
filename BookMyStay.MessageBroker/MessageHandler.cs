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
        }

        public static dynamic CreateChannel()
        {
            dynamic settings = GetConfig();

            ConnectionFactory connection = new ConnectionFactory()
            {
                UserName = settings["MessageBrokerConfig:UserName"].ToString(),
                Password = settings["MessageBrokerConfig:Password"].ToString(),
                HostName = settings["MessageBrokerConfig:HostName"].ToString(),
                VirtualHost = settings["MessageBrokerConfig:VirtualHost"].ToString(),
            };
            connection.DispatchConsumersAsync = true;
            var channel = connection.CreateConnection();
            return channel;
        }

        Task<string> IMessageHandler.ConsumeMessage(string QueueName)
        {

            var channel = CreateChannel();    // Get the channel - alternatively can use the CreateModel() as well.

            channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false); //exclusive: true, Gives error

            BasicGetResult result = channel.BasicGet(QueueName, false);
            if (result != null)
            {
                //Console.WriteLine($"Message: {Encoding.UTF8.GetString(result.Body.ToArray())}");
                channel.BasicAck(result.DeliveryTag, false);
            }

            return Task.FromResult("Done");
        }

        Task IMessageHandler.PublishMessage<T>(string QueueName, T message)
        {
            var channel = CreateChannel();    // Get the channel - alternatively can use the CreateModel() as well.

            var messageString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(messageString);

            channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false);
            channel.BasicPublish("", QueueName, body: body);

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
