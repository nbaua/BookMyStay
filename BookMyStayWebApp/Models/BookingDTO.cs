namespace BookMyStay.WebApp.Models
{
    public class BookingDTO
    {
        public BookingItemDTO BookingItemDTO { get; set; }
        public IEnumerable<BookingDetailsDTO>? BookingDetailsDTO { get; set; } // support multiple bookings
    }
}
