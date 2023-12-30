namespace BookMyStay.PaymentAPI.Models
{
    public class PaymentItemDetailsDTO
    {
        public int PaymentItemDetailId { get; set; }
        public int PaymentItemId { get; set; }
        public int ListingId { get; set; }
        public double BookingPrice { get; set; } //Amount at which the booking was done.
        public ListingDTO? Listing { get; set; }
        public int DayOfStay { get; set; }
    }
}
