using System.ComponentModel.DataAnnotations;

namespace Booking_system_API.DTO
{
    public class BookingRequestDto
    {
        [Required]
        [Range(1, 10)]
        public int NumberofTickets { get; set; }

        [Required]
        public DateTime TravelDate { get; set; }

        [Required]
        public string Destination { get; set; } = string.Empty;


        public Guid FlightId { get; set; }


    }
}
