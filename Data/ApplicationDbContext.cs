using Alaska_Calendar.Models;
using Microsoft.EntityFrameworkCore;

namespace Alaska_Calendar.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<FlightPrice> FlightPrices { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the primary key
            modelBuilder.Entity<FlightPrice>().HasKey(fp => fp.Id);

            // Configure the Price property to use decimal(18,2)
            modelBuilder.Entity<FlightPrice>()
                .Property(fp => fp.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
