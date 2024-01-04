using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trello_back.Models;

namespace Trello_back.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CommentaireController : Controller
    {
        private readonly TrelloContext _context;

        public CommentaireController(TrelloContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentaires()
        {
            var Commentaires = await _context.Commentaires.ToListAsync();
            return Ok(Commentaires);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentaireById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentaire = await _context.Commentaires
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commentaire == null)
            {
                return NotFound();
            }

            return Ok(commentaire);
        }

        // POST: Commentaire/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCommentaire(Models.Commentaire commentaire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commentaire);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCommentaireById), new { id = commentaire.Id }, commentaire);
            }
            return BadRequest(ModelState);
        }

        // POST: Commentaire/Update/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCommentaire(int id, Models.Commentaire commentaire)
        {
            if (id != commentaire.Id)
            {
                return NotFound();
            }

            var existingCommentaire = await _context.Commentaires.FindAsync(id);
            if (existingCommentaire == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(existingCommentaire).CurrentValues.SetValues(commentaire);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentaireExists(commentaire.Id))
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


        // POST: Commentaire/Delete/5
        [HttpDelete("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCommentaire(int id)
        {
            var commentaire = await _context.Commentaires.FindAsync(id);
            if (commentaire == null)
            {
                return NotFound();
            }

            _context.Commentaires.Remove(commentaire);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CommentaireExists(int id)
        {
            return _context.Commentaires.Any(e => e.Id == id);
        }
    }
}
