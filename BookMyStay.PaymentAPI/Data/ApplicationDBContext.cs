using BookMyStay.PaymentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMyStay.PaymentAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<PaymentItem> PaymentItem { get; set; }
        public DbSet<PaymentItemDetails> PaymentItemDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
