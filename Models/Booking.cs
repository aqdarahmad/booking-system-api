using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_system_API.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Range(1,10)]
        public int NumberofTickets { get; set; }

        [Required]
        public DateTime TravelDate { get; set; }


        [Required]
        public string Destination { get; set; } = string.Empty;

        // foregin key
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;


        public ApplicationUser User { get; set; } = null!;


        public List<Ticket> Tickets { get; set; } = new();

    }
}
