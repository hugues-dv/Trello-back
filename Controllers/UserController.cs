using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trello_back.Models;

[Route("api/User")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly TrelloContext _context;

    public UserController(TrelloContext context)
    {
        _context = context;
    }

    private bool ValidateUser(string username, string password)
    {
        // Implémentez la logique de validation ici
        return true;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        // Ajoutez l'utilisateur à votre base de données
        // Gérez les erreurs et les validations
        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User user)
    {
        if (ValidateUser(user.Username, user.Password))
        {
            var tokenString = GenerateJWTToken(user.Username);
            return Ok(new { Token = tokenString });
        }
        return Unauthorized();
    }

    private string GenerateJWTToken(string username)
    {
        // Générez ici votre JWT
        return "token";
    }

    private bool UserExists(string username)
    {
        return _context.Users.Any(e => e.Username == username);
    }
}
