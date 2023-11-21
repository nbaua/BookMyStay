using BookMyStay.BookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMyStay.BookingAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<BookingItem> BookingItems { get; set; }
        public DbSet<BookingDetails> BookingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
