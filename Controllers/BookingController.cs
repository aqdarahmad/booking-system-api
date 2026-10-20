using Booking_system_API.Data;
using Microsoft.AspNetCore.Mvc;
using Booking_system_API.DTO;
using Booking_system_API.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Booking_system_API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly ApplicationDbContext _context;


        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }


    [HttpPost]

    public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDto request)
    {

        var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if(userid == null)
        {
            return Unauthorized("User Is Not Register In Database");
        }
        if (request.NumberofTickets <= 0)
            return BadRequest("Number of tickets must be greater than 0");

        if (request.TravelDate <= DateTime.UtcNow)
            return BadRequest("Travel date must be in the future");
        var newBooking = new Booking
        {   UserId=userid,
            TravelDate = request.TravelDate,
            NumberofTickets = request.NumberofTickets,
            Destination = request.Destination,

        };

        _context.Bookings.Add(newBooking);
       await _context.SaveChangesAsync();

        return Ok(newBooking);

    }
    [Authorize]
    [HttpGet]

    public async Task<IActionResult> GetBooking([FromQuery] string? destination)
    {
        var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

       if (userid == null)
        {
            return Unauthorized("User Is Not Register In Database");
        }

        var query = _context.Bookings.Where(p=> p.UserId == userid);

        if (!string.IsNullOrWhiteSpace(destination))
           query = query.Where(p => p.Destination == destination) ;

        var booking = await query.Select(p => new
        {   p.Id,
            p.NumberofTickets,
            p.Destination,
            p.TravelDate
        }).ToListAsync();

        return Ok(booking);

       

    }
    [Authorize]
    [HttpDelete("{id:guid}")]

    public async Task<IActionResult> CancellBooking([FromRoute] Guid id)

    {

        var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userid == null)
        {
            return Unauthorized("User is not Register");
        }

        var booking = await _context.Bookings.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userid);
        if(booking== null)
            return NotFound();
        _context.Bookings.Remove(booking);
         await _context.SaveChangesAsync();

        return NoContent();


    }
    [Authorize]
    [HttpPut("{id:guid}")]

    public async Task<IActionResult> UpdateBookingDate([FromRoute] Guid id , UpdateBookingDateDto dto)
    {

        var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userid == null)
        {
            return Unauthorized("User is not Register");
        }

        var booking = await _context.Bookings.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userid);

        if (booking == null)
            return NotFound();


        if (dto.NewDate <= DateTime.UtcNow)
            return BadRequest("New date must be in the future");

       
            booking.TravelDate = dto.NewDate;
      

        await _context.SaveChangesAsync();

        return NoContent();
    }

    }

