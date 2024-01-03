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
    public class CarteController : Controller
    {
        private readonly TrelloContext _context;

        public CarteController(TrelloContext context)
        {
            _context = context;
        }

        // GET: Carte
        public async Task<IActionResult> Index()
        {
            var trelloContext = _context.Cartes.Include(c => c.IdListeNavigation);
            return View(await trelloContext.ToListAsync());
        }

        // GET: Carte/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carte = await _context.Cartes
                .Include(c => c.IdListeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carte == null)
            {
                return NotFound();
            }

            return View(carte);
        }

        // GET: Carte/Create
        public IActionResult Create()
        {
            ViewData["IdListe"] = new SelectList(_context.Listes, "Id", "Id");
            return View();
        }

        // POST: Carte/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titre,Description,DateCreation,IdListe")] Carte carte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdListe"] = new SelectList(_context.Listes, "Id", "Id", carte.IdListe);
            return View(carte);
        }

        // GET: Carte/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carte = await _context.Cartes.FindAsync(id);
            if (carte == null)
            {
                return NotFound();
            }
            ViewData["IdListe"] = new SelectList(_context.Listes, "Id", "Id", carte.IdListe);
            return View(carte);
        }

        // POST: Carte/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Description,DateCreation,IdListe")] Carte carte)
        {
            if (id != carte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carte);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdListe"] = new SelectList(_context.Listes, "Id", "Id", carte.IdListe);
            return View(carte);
        }

        // GET: Carte/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carte = await _context.Cartes
                .Include(c => c.IdListeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carte == null)
            {
                return NotFound();
            }

            return View(carte);
        }

        // POST: Carte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carte = await _context.Cartes.FindAsync(id);
            if (carte != null)
            {
                _context.Cartes.Remove(carte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarteExists(int id)
        {
            return _context.Cartes.Any(e => e.Id == id);
        }
    }
}
