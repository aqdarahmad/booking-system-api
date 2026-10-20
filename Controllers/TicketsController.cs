using Booking_system_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Booking_system_API.DTO;
using Booking_system_API.Models;

namespace Booking_system_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{

    public readonly ApplicationDbContext _context;

    public TicketsController(ApplicationDbContext context)

    {
        _context = context;
    }
    [Authorize(Roles = "Admin")]

    [HttpGet]

    public async Task<IActionResult> GetAllTickets()
    {
        var tickets = await _context.Tickets.Select( p=>new
        {
            p.Id,
            p.BookingId,
            p.BookingNumber,
            p.Booking.TravelDate,
            p.Booking.Destination,
            p.TicketPrice,
            p.TicketStatus,
            p.PassengerName

        }).ToListAsync();
        return Ok(tickets);
    }

    [HttpGet("{id:guid}")]

    public async Task<IActionResult> GetTicketById([FromRoute] Guid id)
    {
        

        var userforticket = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userforticket == null)
            return Unauthorized("User is not Register in Database");


        var findticket = await _context.Tickets.Where(p => p.Id == id && p.Booking.UserId == userforticket).Select(

            p => new
            {
                p.SeatNumber,
                p.BookingNumber,
                p.PassengerName,
                p.TicketPrice,
                p.TicketStatus,
                Destination=p.Booking.Destination,
                TravelDate=p.Booking.TravelDate,

            }).FirstOrDefaultAsync();
        if (findticket == null)
            return NotFound("Ticket Not Found");


        return Ok(findticket);


    }

    [Authorize(Roles = "Admin")]
    [HttpPost]

    public async Task<IActionResult> CreateNewTicket([FromBody] TicketRequestDto dto)
    {
        var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (user == null)
            return Unauthorized("User is not Register in Database");

        var booking = await _context.Bookings.Include(p => p.Tickets).FirstOrDefaultAsync(p=>p.Id==dto.Bookingid);

        if (booking == null)
            return NotFound("Booking Not Found");

        if (booking.Tickets.Count >= booking.NumberofTickets)
            return BadRequest("Invalid number of Ticket");


        var newticket = new Ticket
        {
            TicketPrice = dto.TicketPrice,
            BookingId = dto.Bookingid,
            SeatNumber = dto.SeatNumber,
            PassengerName = dto.PassengerName,
            BookingNumber = booking.Tickets.Count + 1,
            TicketStatus = "Confirmed",


        };

        _context.Tickets.Add(newticket);
       await  _context.SaveChangesAsync();

        return Ok( new {newticket.PassengerName,newticket.BookingNumber,newticket.TicketStatus,newticket.TicketPrice});

    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]

    public async Task<IActionResult> UpdateTicketStatus([FromBody] UpdateTicketStatusDto request, [FromRoute]Guid id)
    {
        var findticket = await _context.Tickets.FirstOrDefaultAsync(p => p.Id==id);

        if (findticket == null)
            return NotFound("Ticket Not Found In Database");

        findticket.TicketStatus= request.NewTicketStatus;
        await _context.SaveChangesAsync();

        return NoContent();


    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]

    public async Task<IActionResult> DeleteTicket([FromRoute] Guid id)
    {
        var findticket = await _context.Tickets.FirstOrDefaultAsync(p => p.Id == id);

        if (findticket == null)
            return NotFound("Ticket Not Found In Database");

        _context.Tickets.Remove(findticket);
        await _context.SaveChangesAsync();

        return NoContent();


    }







}