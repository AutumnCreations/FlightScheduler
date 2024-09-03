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

        var airports = context.Airports.ToList();
        if (!airports.Any())
        {
            Console.WriteLine("No airports found in the database. Run the SeedAirports command first.");
            return;
        }

        // 12 Month range of available flights
        var startDate = DateTime.Today;
        var endDate = DateTime.Today.AddMonths(12);

        var random = new Random();

        while (startDate <= endDate)
        {
            // Create a random number of flights in a range for the current day
            var flightCount = random.Next(1, 10);

            for (int i = 0; i < flightCount; i++)
            {
                var departureTime = startDate.AddHours(random.Next(0, 24)).AddMinutes(random.Next(0, 60));
                // Random flight duration within reasonable range for a domestic flight
                var flightDuration = TimeSpan.FromMinutes(random.Next(80, 299));
                var arrivalTime = departureTime.Add(flightDuration);

                var departureAirport = airports[random.Next(airports.Count)];
                var arrivalAirport = airports[random.Next(airports.Count)];

                // Make sure departure and arrival airports are different
                while (arrivalAirport == departureAirport)
                {
                    arrivalAirport = airports[random.Next(airports.Count)];
                }

                var flight = new FlightPrice
                {
                    DepartureTime = departureTime,
                    ArrivalTime = arrivalTime,
                    // Random price within a reasonable range for a domestic flight ticket (economy though because we're broke)
                    Price = Math.Round((decimal)(random.NextDouble() * (500 - 50) + 50), 2),
                    DepartureAirport = departureAirport,
                    ArrivalAirport = arrivalAirport
                };

                context.FlightPrices.Add(flight);
            }

            startDate = startDate.AddDays(1);
        }

        context.SaveChanges();
    }
}