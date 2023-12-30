using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyStay.PaymentAPI.Models
{
    public class PaymentItemDetails
    {
        [Key]
        public int PaymentItemDetailId { get; set; }
        public int PaymentItemId { get; set; }
        [ForeignKey("PaymentItemId")]
        public PaymentItem? PaymentItem { get; set; }
        public int ListingId { get; set; }
        public double BookingPrice { get; set; } //Amount at which the booking was done.

        [NotMapped]
        public ListingDTO? Listing { get; set; }
        public int DayOfStay { get; set; }
    }

}
