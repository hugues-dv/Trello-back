using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trello_back.Models;

namespace Trello_back.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ListeController : Controller
    {
        private readonly TrelloContext _context;

        public ListeController(TrelloContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetListes()
        {
            var Listes = await _context.Listes.ToListAsync();
            return Ok(Listes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetListeById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liste = await _context.Listes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (liste == null)
            {
                return NotFound();
            }

            return Ok(liste);
        }

        // GET: Liste/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateListe(Models.Liste liste)
        {
            if (ModelState.IsValid)
            {
                _context.Add(liste);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetListeById), new { id = liste.Id }, liste);
            }
            return BadRequest(ModelState);
        }

        // POST: Liste/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditListe(int id, Models.Liste liste)
        {
            if (id != liste.Id)
            {
                return NotFound();
            }

            var existingListe = await _context.Listes.FindAsync(id);
            if (existingListe == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(existingListe).CurrentValues.SetValues(liste);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListeExists(liste.Id))
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteListe(int id)
        {
            var liste = await _context.Listes.FindAsync(id);
            if (liste == null)
            {
                return NotFound();
            }

            _context.Listes.Remove(liste);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ListeExists(int id)
        {
            return _context.Listes.Any(e => e.Id == id);
        }
    }
}
