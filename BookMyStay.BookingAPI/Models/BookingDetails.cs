using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyStay.BookingAPI.Models
{
    public class BookingDetails
    {
        [Key]
        public int BookingDetailId { get; set; }

        public int BookingItemId { get; set; }

        [ForeignKey(nameof(BookingItemId))]
        public BookingItem BookingItem { get; set; }

        public int ListingId { get; set; }

        [NotMapped]
        public ListingDTO Listing { get; set; }

        public int DayOfStay { get; set; }

    }
}
