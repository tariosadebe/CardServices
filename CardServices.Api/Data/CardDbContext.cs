using CardServices.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CardServices.Api.Data
{
    public class CardDbContext : DbContext
    {
        public CardDbContext(DbContextOptions<CardDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set the precision and scale for the Balance property
            modelBuilder.Entity<Card>()
                .Property(c => c.Balance)
                .HasColumnType("decimal(18,2)"); // You can adjust the precision and scale as needed
        }
    }
}