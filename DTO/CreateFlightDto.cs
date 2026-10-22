using System.ComponentModel.DataAnnotations;

namespace Booking_system_API.DTOs;

public class CreateFlightDto
{
    [Required]
    public DateTime DepartureDate { get; set; }

    [Required]
    public DateTime ArrivalDate { get; set; }

    [Required]
    public string DepartureAirport { get; set; } = string.Empty;

    [Required]
    public string ArrivalAirport { get; set; } = string.Empty;
}