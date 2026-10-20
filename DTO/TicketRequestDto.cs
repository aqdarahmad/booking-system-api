namespace Booking_system_API.DTO
{
    public class TicketRequestDto
    {

        public string PassengerName { get; set; }= string.Empty;

        public int BookingNumber { get; set; }
        public int TicketPrice { get; set; }
        public string TicketStatus { get; set; } = string.Empty;

        public int SeatNumber { get; set; }

        public Guid Bookingid { get; set; }

    }
}
