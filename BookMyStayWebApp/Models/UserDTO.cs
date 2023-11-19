namespace BookMyStay.WebApp.Models
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
