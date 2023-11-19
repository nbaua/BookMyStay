using BookMyStay.ListingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMyStay.ListingAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Listing> Listings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Listing>().HasData(new Listing
            {
                ListingId = 1,
                Name = "Vintage Houses In Jungle",
                Description = "Stay away from the city nuisance in midst of green lush. Choose from 5 small vintage houses in middle of the Bawana Jungle, which welcomes you for a cozy stay.",
                Category = "Small Villa",
                ListingPrice = 599.99,
                ImageUrl = "https://i.ibb.co/C5x9cS4/pic1.jpg"
            });
            modelBuilder.Entity<Listing>().HasData(new Listing
            {
                ListingId = 2,
                Name = "Wooden Cabins On Mountain",
                Description = "Pick a nice wooden cabin for your next camping on top of the Hidler mountain range. Enough for 4 people's stay with lots of fun time for youe camping without a tent.",
                Category = "Small Cabin",
                ListingPrice = 499.99,
                ImageUrl = "https://i.ibb.co/0XgMrWj/pic2.jpg"
            });
            modelBuilder.Entity<Listing>().HasData(new Listing
            {
                ListingId = 3,
                Name = "Villa Houses On Rent",
                Description = "Beautiful raw villa houses for a pleasant weekend spend near the Kilana lake, comes with all modern amenities and yet gives a rural feeling on your stay. Ideal for a family of 2-6 people.",
                Category = "Large Villa",
                ListingPrice = 1299.99,
                ImageUrl = "https://i.ibb.co/9NMM4vz/pic3.jpg"
            });
            modelBuilder.Entity<Listing>().HasData(new Listing
            {
                ListingId = 4,
                Name = "Lush Houses For Business People",
                Description = "Beautiful modern villa houses for a lavish stay during your business trip, comes with all modern amenities and comfort. Very accessible from the Arila and Garmdos airports and Firana city railway stations. Ideal for corporate meetings and small events",
                Category = "Large Villa",
                ListingPrice = 1999.99,
                ImageUrl = "https://i.ibb.co/cNgwSyw/pic4.jpg"
            });
            modelBuilder.Entity<Listing>().HasData(new Listing
            {
                ListingId = 5,
                Name = "Bunglow For Events",
                Description = "Beautiful modern bunglow for throwing lavish parties or arrange team events.Very accessible from the Arila and Garmdos airports and Firana city railway stations. Best choice for large corporate or family events.",
                Category = "Large Villa",
                ListingPrice = 3499.99,
                ImageUrl = "https://i.ibb.co/JtVSssw/pic5.jpg"
            });
            modelBuilder.Entity<Listing>().HasData(new Listing
            {
                ListingId = 6,
                Name = "Corporate Guest Houses",
                Description = "Beautiful modern guest houses with all modern equipments, suitable for both business and pleasure of the guests. Accessible and affordable providing best value for money.",
                Category = "Small Villa",
                ListingPrice = 899.99,
                ImageUrl = "https://i.ibb.co/sKv6Sr9/pic6.jpg"
            });
        }
    }
}
