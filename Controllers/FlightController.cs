using Booking_system_API.Data;
using Booking_system_API.DTO;
using Booking_system_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Booking_system_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{


    private readonly ApplicationDbContext _context;

    public FlightController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpPost]

    public async Task<IActionResult> CreateAFlight([FromBody] FlightRequestDto request)
    {
        var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userid == null)
        {
            return Unauthorized("User Is Not Register In Database");
        }
        var booking = await _context.Bookings.Include(p => p.Tickets).FirstOrDefaultAsync(p => p.Id == request.BookingId && p.UserId==userid);

        if (booking == null)
            return NotFound("Booking Not Found");

        var newflight = new Flight
        {
            DepartureDate = request.DepartureDate,
            ArrivalDate = request.ArrivalDate,
            DepartureAirport = request.DepartureAirport,
            ArrivalAirport = request.ArrivalAirport,
        };

        _context.Flights.Add(newflight);
        await _context.SaveChangesAsync();

        return Ok(newflight);
    }
    [Authorize]
    [HttpGet]

    public async Task<IActionResult> GetAllFlight()
    {
        var tickets = await _context.Flights.Select(
             p => new
          {
                 p.ArrivalDate,
                 p.ArrivalAirport,
                 p.DepartureAirport,
                 p.DepartureDate,
                 p.FlightId
             }).ToListAsync();
        return Ok(tickets);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteFlight([FromRoute] Guid id)
    {
        var findflight = await _context.Flights.FirstOrDefaultAsync(p=>p.FlightId==id);

        if (findflight == null)
            return NotFound("Flight not in flights");

        _context.Flights.Remove(findflight);
        await _context.SaveChangesAsync();

        return NoContent();

    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateFlight(
        Guid id,
        [FromBody] FlightRequestDto dto)
    {
        var flight = await _context.Flights
            .FirstOrDefaultAsync(f => f.FlightId == id);

        if (flight == null)
            return NotFound("Flight Not Found");

        flight.DepartureDate = dto.DepartureDate;
        flight.ArrivalDate = dto.ArrivalDate;
        flight.DepartureAirport = dto.DepartureAirport;
        flight.ArrivalAirport = dto.ArrivalAirport;

        await _context.SaveChangesAsync();

        return Ok(flight);
    }



}