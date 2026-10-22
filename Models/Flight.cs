using System.ComponentModel.DataAnnotations;

namespace Booking_system_API.Models
{
    public class Flight
    {
        [Key]
        public Guid FlightId { get; set; } = Guid.NewGuid();

        [Required]

        public DateTime DepartureDate { get; set; }

        [Required]

        public DateTime ArrivalDate { get; set; }

        [Required]

        public string DepartureAirport { get; set; } = string.Empty;


        [Required]

        public string ArrivalAirport { get; set; } = string.Empty;

        public List<Booking> Bookings { get; set; } = new();

        public List<Ticket> Tickets { get; set; } = new();

        public string UserId { get; set; } = string.Empty;


        public ApplicationUser User { get; set; } = null!;





    }
}
