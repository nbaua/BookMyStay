namespace BookMyStay.MessageBroker
{
    public interface IMessageHandler
    {
        Task<string> ConsumeMessage(string QueueName);
        Task PublishMessage<T>(string QueueName, T message);
    }
}