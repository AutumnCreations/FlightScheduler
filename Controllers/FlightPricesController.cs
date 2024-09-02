using Alaska_Calendar.Data;
using Alaska_Calendar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alaska_Calendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPricesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FlightPricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get flights for a specific month, returning the cheapest flight per each day: api/FlightPrices/monthly?year=2024&month=9
        [HttpGet("monthly")]
        public ActionResult<IEnumerable<FlightPrice>> GetMonthlyFlights(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);
            List<FlightPrice> flights = GetCheapestFlights(startDate, endDate);
            return Ok(flights);
        }

        // Get flights for a specific week: api/FlightPrices/weekly?year=2024&month=9&day=1
        [HttpGet("weekly")]
        public ActionResult<IEnumerable<FlightPrice>> GetWeeklyFlights(int year, int month, int day)
        {
            var startDate = new DateTime(year, month, day);
            var endDate = startDate.AddDays(7);
            var flights = GetCheapestFlights(startDate, endDate);
            return Ok(flights);
        }

        // Get flights for a specific day: api/FlightPrices/daily?year=2024&month=9&day=1
        [HttpGet("daily")]
        public ActionResult<IEnumerable<FlightPrice>> GetDailyFlights(int year, int month, int day)
        {
            var startDate = new DateTime(year, month, day);
            var endDate = startDate.AddDays(1);
            var flights = _context.FlightPrices
                .Include(fp => fp.DepartureAirport)
                .Include(fp => fp.ArrivalAirport)
                .Where(fp => fp.DepartureTime >= startDate && fp.DepartureTime < endDate)
                .OrderBy(fp => fp.DepartureTime)
                .ToList();
            return Ok(flights);
        }

        private List<FlightPrice> GetCheapestFlights(DateTime startDate, DateTime endDate)
        {
            // Get the IDs of the cheapest flights for each date
            var cheapestFlightIds = _context.FlightPrices
                .Where(fp => fp.DepartureTime >= startDate && fp.DepartureTime < endDate)
                .GroupBy(fp => fp.DepartureTime.Date)
                .Select(g => g.OrderBy(fp => fp.Price).First().Id)
                .ToList();

            // Fetch the full details of these cheapest flights
            var cheapestFlights = _context.FlightPrices
                .Include(fp => fp.DepartureAirport)
                .Include(fp => fp.ArrivalAirport)
                .Where(fp => cheapestFlightIds.Contains(fp.Id))
                .OrderBy(fp => fp.DepartureTime)
                .ToList();

            return cheapestFlights;
        }
    }
}