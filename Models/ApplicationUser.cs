namespace Booking_system_API.Models;

using Microsoft.AspNetCore.Identity;


    public class ApplicationUser : IdentityUser
    {

    public List<Booking> Bookings { get; set; } = new();
    }

