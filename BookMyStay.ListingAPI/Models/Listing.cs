using System.ComponentModel.DataAnnotations;

namespace BookMyStay.ListingAPI.Models
{
    public class Listing
    {
        [Key]
        public int ListingId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        [Range(10, 10000)]
        public double ListingPrice { get; set; }
    }
}
