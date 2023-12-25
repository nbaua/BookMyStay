using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookMyStay.DBLoggerAPI.Models
{
    public class BookingDetailsDTO
    {
        public int BookingDetailId { get; set; }

        public int BookingItemId { get; set; }

        public BookingItemDTO? BookingItemDTO { get; set; }

        public int ListingId { get; set; }

        public ListingDTO? Listing { get; set; }

        public int DayOfStay { get; set; }
    }
}
