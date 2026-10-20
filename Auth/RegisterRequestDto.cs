using System.ComponentModel.DataAnnotations;

namespace Booking_system_API.Auth
{
    public class RegisterRequestDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
