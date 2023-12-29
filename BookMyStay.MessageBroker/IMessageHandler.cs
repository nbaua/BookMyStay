namespace BookMyStay.MessageBroker
{
    public interface IMessageHandler
    {
        Task<dynamic> ConsumeMessage(string QueueName);
        Task PublishMessage<T>(string QueueName, T message);
    }
}