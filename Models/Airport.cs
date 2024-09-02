using System.ComponentModel.DataAnnotations;

namespace Alaska_Calendar.Models
{
    public class Airport
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [StringLength(3)]
        public required string Abbreviation { get; set; }

        [Required]
        public required string City { get; set; }

        [Required]
        public required string State { get; set; }
    }
}