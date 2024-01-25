using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Trello_back.Models;

namespace Trello_back.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TrelloContext _context;
        private readonly IConfiguration _configuration;

        public UserController(TrelloContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private bool ValidateUser(string username, string password)
        {
            // Implémentez la logique de validation ici
            return true;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            Console.WriteLine(JsonSerializer.Serialize(user));
            if (ModelState.IsValid && !string.IsNullOrEmpty(user.Password))
            {
                // Vérifie si l'utilisateur existe déjà
                var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
                if (existingUser != null)
                {
                    return BadRequest("Ce nom d'utilisateur est déjà utilisé.");
                }

                // Hachez le mot de passe avant de le stocker
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                // Créez un nouvel utilisateur avec le mot de passe haché
                User newUser = new User { Username = user.Username, Password = hashedPassword };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return Ok("Inscription réussie.");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);

            if (
                existingUser != null
                && BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password)
            )
            {
                var tokenString = GenerateJWTToken(existingUser.Username);
                return Ok(new { Token = tokenString });
            }
            return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");
        }

        private string GenerateJWTToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
