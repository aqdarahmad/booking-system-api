using Booking_system_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


}