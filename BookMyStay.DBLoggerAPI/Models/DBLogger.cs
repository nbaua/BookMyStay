namespace BookMyStay.DBLoggerAPI.Models
{
    public class DBLogger
    {
        public int Id {  get; set; }
        public string UserId { get; set; }
        public string QueueName {  get; set; }

        public string Payload { get; set; }

        public DateTime LogDateTime { get; set; } = DateTime.Now;

    }
}
