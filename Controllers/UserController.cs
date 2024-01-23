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

    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<User>> GetUsers()
    {
        var Users = await _context.Users.ToArrayAsync();
        return Ok(Users);
    }

    // GET: api/User/5
    [HttpGet("{username}")]
    public async Task<ActionResult<User>> GetUser(string username)
    {
        var User = await _context.Users.FindAsync(username);
        if (User == null)
        {
            return NotFound();
        }
        return User;
    }

    // PUT: api/User/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{username}")]
    public async Task<IActionResult> PutUser(string username)
    {
        if (username == null)
        {
            return NotFound();
        }
        var user = await _context.Users
            .FirstOrDefaultAsync(m => m.Username == username);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // POST: api/User
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([Bind("Username, Password")] User user)
    {
        if (ModelState.IsValid)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { username = user.Username }, user);
        }
        return BadRequest(ModelState);
    }

    // POST: User/Update/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPut("{username}")]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateUser(string username, [Bind("Username,Password")] User user)
    {
        if (username != user.Username)
        {
            return NotFound();
        }

        var existingUser = await _context.Users.FindAsync(username);
        if (existingUser == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Entry(existingUser).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Username))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        return BadRequest(ModelState);
    }

    // DELETE: api/User/5
    [HttpDelete("{username}")]
    public async Task<IActionResult> DeleteUser(int username)
    {
        var User = await _context.Users.FindAsync(username);
        if (User == null)
        {
            return NotFound();
        }
        _context.Users.Remove(User);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool UserExists(string username)
    {
        return _context.Users.Any(e => e.Username == username);
    }
}