using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookMyStay.WebApp.Models
{
    public class BookingItemDTO
    {
        public int BookingItemId { get; set; }

        public string? UserId { get; set; } //Logged-in User GUID

        public string? OfferCode { get; set; } //If code is applied

        public double? Discount { get; set; }

        public double BookingTotal { get; set; }

        //Additional properties for DB logger
        public string? username { get; set; }
        public string? email { get; set; }

    }
}
