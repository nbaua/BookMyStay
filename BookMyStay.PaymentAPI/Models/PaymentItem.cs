using System.ComponentModel.DataAnnotations;

namespace BookMyStay.PaymentAPI.Models
{
    public class PaymentItem
    {
        [Key]
        public int BookingItemId { get; set; }
        public string? UserId { get; set; } //Logged-in User GUID
        public string? OfferCode { get; set; } //If code is applied
        public double? Discount { get; set; }
        public double BookingTotal { get; set; }//Final amout as Payment - we already diducted the discount
        public string? Name { get; set; }
        public string? Email { get; set; }

        public IEnumerable<PaymentItemDetails> PaymentItemDetails { get; set; } //Booking details

        //PAYMENT GATEWAY SPECIFIC PROPERTIES
        public string PaymentStatus { get; set; } = "INPROCESS"; //Ideally this could be an ENUM instead of a magic string.
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string? StripeSessionId{ get; set; } //For Stripe payment gateway
        public string? PaymentIntentId{ get; set; } //For Stripe payment gateway

    }
}
