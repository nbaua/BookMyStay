using BookMyStay.DBLoggerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMyStay.DBLoggerAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<DBLogger> DBLoggers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
