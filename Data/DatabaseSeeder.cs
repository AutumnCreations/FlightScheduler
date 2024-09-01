using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Alaska_Calendar.Data;
using Alaska_Calendar.Models;

public static class DatabaseSeeder
{
    public static void SeedFlights(ApplicationDbContext context)
    {
        // Clear existing data
        var allFlights = context.FlightPrices.ToList();
        if (allFlights.Any())
        {
            context.FlightPrices.RemoveRange(allFlights);
            context.SaveChanges();
        }

        // Reset the ID sequence for cleaner development data
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('FlightPrices', RESEED, 0)");

        // 12 Month range of available flights
        var startDate = DateTime.Today;
        var endDate = DateTime.Today.AddMonths(12);

        var random = new Random();

        while (startDate <= endDate)
        {
            // Create a random number of flights in a range for the current day
            var flightCount = random.Next(1, 3);

            for (int i = 0; i < flightCount; i++)
            {
                var flight = new FlightPrice
                {
                    Date = startDate.AddHours(random.Next(0, 24)).AddMinutes(random.Next(0, 60)),
                    Price = Math.Round((decimal)(random.NextDouble() * (300 - 50) + 50), 2)
                };

                context.FlightPrices.Add(flight);
            }

            startDate = startDate.AddDays(1);
        }

        context.SaveChanges();
    }
}
