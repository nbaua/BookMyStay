namespace BookMyStay.WebApp.Models
{
    public class APIRequestDTO
    {
        public string RequestType { get; set; } = "GET";
        public required string RequestUrl { get; set; }
        public required object Payload { get; set; }
        public required string Token { get; set; }
    }
}
