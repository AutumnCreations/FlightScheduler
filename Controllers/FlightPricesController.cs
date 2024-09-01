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

        // Get ALL flights: api/FlightPrices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightPrice>>> GetFlightPrices()
        {
            return await _context.FlightPrices.ToListAsync();
        }

        // Get specific flight: api/FlightPrices/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightPrice>> GetFlightPrice(int id)
        {
            var flightPrice = await _context.FlightPrices.FindAsync(id);

            if (flightPrice == null)
            {
                return NotFound();
            }

            return flightPrice;
        }

        // Get flights for a specific month, returning the cheapest flight per each day: api/FlightPrices/monthly?year=2024&month=9
        [HttpGet("monthly")]
        public ActionResult<IEnumerable<FlightPrice>> GetMonthlyFlights(int year, int month)
        {
            var startDate = new DateTime(year, month + 1, 1);
            var endDate = startDate.AddMonths(1);
            Console.WriteLine(startDate);
            Console.WriteLine(endDate);

            var flights = _context.FlightPrices
                .Where(fp => fp.Date >= startDate && fp.Date < endDate)
                .GroupBy(fp => fp.Date.Date)
                .Select(g => new FlightPrice
                {
                    Date = g.Key,
                    Price = g.Min(fp => fp.Price),
                    Id = g.First().Id
                })
                .OrderBy(fp => fp.Date)
                .ToList();
            return Ok(flights);
        }

        // Get flights for a specific week: api/FlightPrices/weekly?year=2024&month=9&day=1
        [HttpGet("weekly")]
        public ActionResult<IEnumerable<FlightPrice>> GetWeeklyFlights(int year, int month, int day)
        {
            var startDate = new DateTime(year, month, day);
            var endDate = startDate.AddDays(7).AddSeconds(-1);

            var flights = _context.FlightPrices
                .Where(fp => fp.Date >= startDate && fp.Date <= endDate)
                .OrderBy(fp => fp.Date)
                .ToList();

            return Ok(flights);
        }

        // Get flights for a specific day: api/FlightPrices/daily?year=2024&month=9&day=1
        [HttpGet("daily")]
        public ActionResult<IEnumerable<FlightPrice>> GetDailyFlights(int year, int month, int day)
        {
            var startDate = new DateTime(year, month, day);
            var endDate = startDate.AddDays(1).AddSeconds(-1);

            var flights = _context.FlightPrices
                .Where(fp => fp.Date >= startDate && fp.Date <= endDate)
                .OrderBy(fp => fp.Date)
                .ToList();

            return Ok(flights);
        }
    }
}
