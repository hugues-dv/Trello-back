using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trello_back.Models;

namespace Trello_back.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly TrelloContext _context;

        public UserController(TrelloContext context)
        {
            _context = context;
        }

        // GET: User
        [HttpGet]
        public IActionResult GetUsers()
        {
            IQueryable<User> users = _context.Users;
            return Ok(users);
        }

        // GET: User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            return BadRequest(ModelState);
        }

        // POST: User/Update/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            var existingUser = await _context.Users.FindAsync(id);
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
                    if (!UserExists(user.Id))
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

        // POST: User/Delete/5
        [HttpDelete("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private bool PwdIsCorrect(User userClient)
        {
            var user = _context.Users.FirstOrDefault(m => m.Id == userClient.Id);
            return userClient.Password == user.Password;
        }
    }
}
