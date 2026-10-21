using Booking_system_API.Auth;
using Booking_system_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Booking_system_API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IConfiguration _configuration;

        public AuthController (UserManager<ApplicationUser>userManager, IConfiguration configuration)
            {
               _userManager= userManager;
        _configuration= configuration; ;
    }
    [HttpPost("register")]

    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("User Register Successfully");
    }

    ///

    [HttpPost("Login")]

    public async Task<IActionResult>Login([FromBody] LoginRequestDto request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user == null)
            return Unauthorized("Invalid email ");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
            return Unauthorized("Inavlid Password");


        string issuer = _configuration["Jwt:Issuer"];

        string audience = _configuration["Jwt:Audience"]; ;

        string jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is missing.");

        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),

        };
        var roles = await _userManager.GetRolesAsync(user);
        
        foreach (var role in roles )
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var credentials = new SigningCredentials(
           Key,
           SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken(
        issuer: issuer,
        audience: audience,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(30),
        signingCredentials: credentials);


        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new
        {
            message = "Login successful",
            token = tokenString
        });

    }



    }

