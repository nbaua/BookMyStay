namespace BookMyStay.BookingAPI.Models
{
    public class ListingDTO
    {
        public int ListingId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public double ListingPrice { get; set; }
    }
}
