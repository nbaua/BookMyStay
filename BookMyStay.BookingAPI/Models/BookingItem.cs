using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyStay.BookingAPI.Models
{
    public class BookingItem
    {
        [Key]
        public int BookingItemId { get; set; }

        [Required]
        public string? UserId { get; set; } //Logged-in User GUID

        public string? OfferCode { get; set; } //If code is applied
        
        [NotMapped]
        public double? Discount { get; set; }

        [NotMapped]
        public double BookingTotal { get; set; } //For now number of people is not implemented so only no of days stay multiplied by price - this will be same as BookingDetail Total for now

    }
}
