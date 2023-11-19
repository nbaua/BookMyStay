using System.ComponentModel.DataAnnotations;

namespace BookMyStay.OfferAPI.Models
{
    public class Offer
    {
        [Key]
        public int OfferId { get; set; }
        [Required]
        public string OfferCode{ get; set; }
        public string OfferDetail { get; set; }
        [Required]
        public double OfferDiscountPerc { get; set; }
        [Required]
        public DateTime OfferValidTill { get; set; }
    }
}
