using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alaska_Calendar.Models
{
    public class FlightPrice
    {
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }

        public int DepartureAirportId { get; set; }
        public required Airport DepartureAirport { get; set; }

        public int ArrivalAirportId { get; set; }
        public required Airport ArrivalAirport { get; set; }
    }
}