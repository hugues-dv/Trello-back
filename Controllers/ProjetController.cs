using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trello_back.Models;

namespace Trello_back.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ProjetController : Controller
    {
        private readonly TrelloContext _context;

        public ProjetController(TrelloContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjets()
        {
            var Projets = await _context.Projets.ToListAsync();
            return Ok(Projets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjetById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projet = await _context.Projets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projet == null)
            {
                return NotFound();
            }

            return Ok(projet);
        }

        // POST: Projet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProjet(Models.Projet projet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projet);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProjetById), new { id = projet.Id }, projet);
            }
            return BadRequest(ModelState);
        }

        // POST: Projet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProjet(int id, Models.Projet projet)
        {
            if (id != projet.Id)
            {
                return NotFound();
            }

            var existingProjet = await _context.Projets.FindAsync(id);
            if (existingProjet == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(existingProjet).CurrentValues.SetValues(projet);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjetExists(projet.Id))
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

        [HttpDelete("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProjet(int id)
        {
            var projet = await _context.Projets.FindAsync(id);
            if (projet == null)
            {
                return NotFound();
            }

            _context.Projets.Remove(projet);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool ProjetExists(int id)
        {
            return _context.Projets.Any(e => e.Id == id);
        }

    }
}
