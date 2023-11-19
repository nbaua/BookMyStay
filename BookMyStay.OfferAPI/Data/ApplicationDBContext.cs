using BookMyStay.OfferAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMyStay.OfferAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Offer>().HasData(new Offer
            {
                OfferId = 1,
                OfferCode = "FLAT05P",
                OfferDetail = "Get flat 5 percent discount on your stay.",
                OfferDiscountPerc = 5.0,
                OfferValidTill = DateTime.Now.AddDays(25),
            });
            modelBuilder.Entity<Offer>().HasData(new Offer
            {
                OfferId = 2,
                OfferCode = "FLAT10P",
                OfferDetail = "Get flat 10 percent discount on your stay.",
                OfferDiscountPerc = 10.0,
                OfferValidTill = DateTime.Now.AddDays(20)
            });
            modelBuilder.Entity<Offer>().HasData(new Offer
            {
                OfferId = 3,
                OfferCode = "FLAT15P",
                OfferDetail = "Get flat 15 percent discount on your stay.",
                OfferDiscountPerc = 15.0,
                OfferValidTill = DateTime.Now.AddDays(15)
            });
            modelBuilder.Entity<Offer>().HasData(new Offer
            {
                OfferId = 4,
                OfferCode = "FLAT20P",
                OfferDetail = "Get flat 20 percent discount on your stay.",
                OfferDiscountPerc = 20.0,
                OfferValidTill = DateTime.Now.AddDays(10)
            });
            modelBuilder.Entity<Offer>().HasData(new Offer
            {
                OfferId = 5,
                OfferCode = "FLAT25P",
                OfferDetail = "Get flat 25 percent discount on your stay.",
                OfferDiscountPerc = 25.0,
                OfferValidTill = DateTime.Now.AddDays(5)
            });
        }
    }
}
