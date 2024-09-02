using System;
using System.IO;
using System.Text.Json;
using Alaska_Calendar.Data;
using Alaska_Calendar.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Alaska_Calendar.Commands
{
    public static class SeedAirportsCommand
    {
        public static void Execute(IServiceProvider serviceProvider, string jsonFilePath)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"Airport data file not found: {jsonFilePath}");
            }

            string jsonString = File.ReadAllText(jsonFilePath);
            var airportData = JsonSerializer.Deserialize<AirportData>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (airportData?.Airports == null)
            {
                throw new InvalidOperationException("Failed to deserialize airport data or Airports array is null.");
            }

            foreach (var airport in airportData.Airports)
            {
                if (airport == null)
                {
                    Console.WriteLine("Encountered a null airport entry. Skipping.");
                    continue;
                }

                context.Airports.Add(new Airport
                {
                    Name = airport.Name ?? throw new InvalidOperationException("Airport Name is null"),
                    Abbreviation = airport.Abbreviation ?? throw new InvalidOperationException("Airport Abbreviation is null"),
                    City = airport.City ?? throw new InvalidOperationException("Airport City is null"),
                    State = airport.State ?? throw new InvalidOperationException("Airport State is null")
                });
            }

            context.SaveChanges();
            Console.WriteLine($"Successfully seeded {airportData.Airports.Length} airports.");
        }
    }

    public class AirportData
    {
        public required Airport[] Airports { get; set; }
    }
}