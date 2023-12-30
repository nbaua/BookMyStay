namespace BookMyStay.DBLoggerAPI.Models
{
    public class BookingItemDTO
    {
        public int BookingItemId { get; set; }

        public string? UserId { get; set; } //Logged-in User GUID

        public string? OfferCode { get; set; } //If code is applied

        public double? Discount { get; set; }

        public double BookingTotal { get; set; }

        //Additional properties for DB logger
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
