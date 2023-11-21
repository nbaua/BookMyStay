namespace BookMyStay.BookingAPI.Models
{
    public class OfferDTO
    {
        public int OfferId { get; set; }
        public string OfferCode { get; set; }
        public string OfferDetail { get; set; }
        public double OfferDiscountPerc { get; set; }
        //public DateTime OfferValidTill{ get; }
        //public DateTime OfferCreated { get; }
    }
}
