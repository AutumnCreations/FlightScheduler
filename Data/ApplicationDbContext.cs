using Alaska_Calendar.Models;
using Microsoft.EntityFrameworkCore;

namespace Alaska_Calendar.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<FlightPrice> FlightPrices { get; set; }
        public DbSet<Airport> Airports { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Airport>()
                 .HasIndex(a => a.Abbreviation)
                 .IsUnique();

            modelBuilder.Entity<FlightPrice>().HasKey(fp => fp.Id);

            // Configure the Price property to use decimal(18,2)
            modelBuilder.Entity<FlightPrice>()
                .Property(fp => fp.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<FlightPrice>()
                .HasOne(f => f.DepartureAirport)
                .WithMany()
                .HasForeignKey(f => f.DepartureAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FlightPrice>()
                .HasOne(f => f.ArrivalAirport)
                .WithMany()
                .HasForeignKey(f => f.ArrivalAirportId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
