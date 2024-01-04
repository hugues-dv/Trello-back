using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trello_back.Models;

namespace Trello_back.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CarteController : Controller
    {
        private readonly TrelloContext _context;

        public CarteController(TrelloContext context)
        {
            _context = context;
        }

        // GET: Carte
        [HttpGet]
        public async Task<IActionResult> GetCartes()
        {
            var Cartes = await _context.Cartes.ToListAsync();
            return Ok(Cartes);
        }

        // GET: Carte/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarteById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carte = await _context.Cartes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carte == null)
            {
                return NotFound();
            }

            return Ok(carte);
        }

        // POST: Carte/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCarte(Models.Carte carte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carte);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCarteById), new { id = carte.Id }, carte);
            }
            return BadRequest(ModelState);
        }

        // POST: Carte/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCarte(int id, Models.Carte carte)
        {
            if (id != carte.Id)
            {
                return NotFound();
            }

            var existingCarte = await _context.Cartes.FindAsync(id);
            if (existingCarte == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(existingCarte).CurrentValues.SetValues(carte);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarteExists(carte.Id))
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

        // POST: Carte/Delete/5
        [HttpDelete("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCarte(int id)
        {
            var carte = await _context.Cartes.FindAsync(id);
            if (carte == null)
            {
                return NotFound();
            }

            _context.Cartes.Remove(carte);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool CarteExists(int id)
        {
            return _context.Cartes.Any(e => e.Id == id);
        }
    }
}
