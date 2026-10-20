using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_system_API.Models;
    public class Ticket
    {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]

    public string PassengerName { get; set; } = string.Empty;

    [Required]
    public int BookingNumber { get; set; }

    [Required]

    [Range(100,1000)]
    public int TicketPrice { get; set; }

    [Required]
    public string TicketStatus { get; set; } = string.Empty;

    [Required]
    [Range(1,200)]
    public int SeatNumber { get; set; }

    [ForeignKey(nameof(Booking))]
    public Guid BookingId { get; set; }


    public Booking Booking { get; set; } = null!;






    }


