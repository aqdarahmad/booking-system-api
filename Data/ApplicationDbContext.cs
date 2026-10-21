using Booking_system_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;


namespace Booking_system_API.Data;
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {


           

        }
        public DbSet<Booking> Bookings{ get; set; }
        public DbSet<Ticket>Tickets { get; set; }

        public DbSet<Flight> Flights { get; set; }
}

