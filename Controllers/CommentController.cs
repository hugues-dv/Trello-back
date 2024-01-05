using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trello_back.Models;

namespace Trello_back.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly TrelloContext _context;

        public CommentController(TrelloContext context)
        {
            _context = context;
        }
        // GET: Comment

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var Comments = await _context.Comments.ToListAsync();
            return Ok(Comments);
        }

        // GET: Comment/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // POST: Comment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment([Bind("id,content,createdAt,idCard,user")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
            }
            return BadRequest(ModelState);
        }

        // POST: Comment/Update/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateComment(int id, [Bind("id,content,createdAt,idCard,user")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(existingComment).CurrentValues.SetValues(comment);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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


        // POST: Comment/Delete/5
        [HttpDelete("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
